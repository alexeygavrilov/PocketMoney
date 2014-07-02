using Castle.Services.Transaction;
using NHibernate.Criterion;
using NHibernate.Linq;
using NHibernate.SqlCommand;
using NHibernate.Transform;
using PocketMoney.Data;
using PocketMoney.Data.NHibernate;
using PocketMoney.Model;
using PocketMoney.Model.External;
using PocketMoney.Model.External.Requests;
using PocketMoney.Model.External.Results;
using PocketMoney.Model.Internal;
using PocketMoney.Service.Interfaces;
using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.ServiceModel;
using System.ServiceModel.Activation;

namespace PocketMoney.Service
{
    [Transactional]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class GoalService : BaseService, IGoalService
    {
        #region Members
        private IRepository<Task, TaskId, Guid> _taskRepository;
        private IRepository<Attainment, AttainmentId, Guid> _attainmentRepository;
        private IRepository<Performer, PerformerId, Guid> _performerRepository;
        #endregion

        #region Ctor
        public GoalService(
            IRepository<Performer, PerformerId, Guid> performerRepository,
            IRepository<Attainment, AttainmentId, Guid> attainmentRepository,
            IRepository<Task, TaskId, Guid> taskRepository,
            IRepository<User, UserId, Guid> userRepository,
            IRepository<Family, FamilyId, Guid> familyRepository,
            ICurrentUserProvider currentUserProvider)
            : base(userRepository, familyRepository, currentUserProvider)
        {
            _taskRepository = taskRepository;
            _attainmentRepository = attainmentRepository;
            _performerRepository = performerRepository;
        }
        #endregion

        #region Set Methods

        [Transaction(TransactionMode.Requires)]
        [MethodImpl(MethodImplOptions.Synchronized)]
        [OperationBehavior(TransactionScopeRequired = true)]
        public GuidResult AddGoal(AddGoalRequest model)
        {
            var currentUser = _currentUserProvider.GetCurrentUser();

            Goal goal = new Goal(model.Text, new Reward(model.Points, model.Gift), currentUser.To());

            _taskRepository.Add(goal);

            foreach (var userId in model.AssignedTo)
            {
                var user = _userRepository.One(new UserId(userId));
                Performer performer = new Performer(goal, user);
                _performerRepository.Add(performer);
                goal.AssignedTo.Add(performer);
            }

            return new GuidResult(goal.Id);
        }

        [Transaction(TransactionMode.Requires)]
        [MethodImpl(MethodImplOptions.Synchronized)]
        [OperationBehavior(TransactionScopeRequired = true)]
        public Result UpdateGoal(UpdateGoalRequest model)
        {
            var currentUser = _currentUserProvider.GetCurrentUser();

            Goal goal = _taskRepository.One(new TaskId(model.Id)).As<Goal>();

            if (goal == null)
            {
                return Result.Unsuccessfully(string.Format("Goal with identifier {0} has not been found.", model.Id));
            }

            goal.Active = true;
            goal.Details = model.Text;
            goal.Reward = new Reward(goal, model.Points, model.Gift);
            _taskRepository.Update(goal);

            foreach (var user in _userRepository.FindAll(x => x.Family.Id == currentUser.Family.Id))
            {
                if (model.AssignedTo.Any(x => x == user.Id) && !goal.AssignedTo.Any(x => x.User.Id == user.Id))
                {
                    Performer performer = new Performer(goal, user);
                    _performerRepository.Add(performer);
                    goal.AssignedTo.Add(performer);
                }
                else if (!model.AssignedTo.Any(x => x == user.Id) && goal.AssignedTo.Any(x => x.User.Id == user.Id))
                {
                    var performer = goal.AssignedTo.First(x => x.User.Id == user.Id);
                    goal.AssignedTo.Remove(performer);
                    _performerRepository.Remove(performer);
                }
            }

            return Result.Successfully();

        }
        #endregion

        #region Get Methods

        [OperationBehavior, MethodImpl(MethodImplOptions.Synchronized)]
        public GoalResult GetGoal(GuidRequest goalId)
        {
            Goal goal = _taskRepository.One(new TaskId(goalId.Data)).As<Goal>();
            if (goal != null)
            {
                return new GoalResult(new GoalView(goal));
            }
            else
            {
                return new GoalResult("Cannot found task.");
            }
        }

        [OperationBehavior, MethodImpl(MethodImplOptions.Synchronized)]
        public GoalListResult AllGoals(Request model)
        {
            var currentUser = _currentUserProvider.GetCurrentUser();

            Performer performer = null;
            User user = null;
            GoalViewInQuery task = null;

            var list = _taskRepository.QueryOverOf<Goal, Task, TaskId, Guid>()
                .JoinAlias(x => x.AssignedTo, () => performer, JoinType.LeftOuterJoin)
                .JoinAlias(() => performer.User, () => user, JoinType.LeftOuterJoin)
                .Where(x => x.Family.Id == currentUser.Family.Id && x.Active && x.Type.Id == TaskType.Goal.Id)
                .Select(
                    Projections.Property<Goal>(x => x.Id).WithAlias(() => task.Id),
                    Projections.Property<Goal>(x => x.Details).WithAlias(() => task.Details),
                    Projections.Property<Goal>(x => x.Reward).WithAlias(() => task.Reward),
                    Projections.Property(() => user.Id).WithAlias(() => task.UserId),
                    Projections.Property(() => user.UserName).WithAlias(() => task.UserName),
                    Projections.Property(() => user.AdditionalName).WithAlias(() => task.AdditionalName)
                    )
                .OrderBy(x => x.DateCreated).Asc
                .TransformUsing(Transformers.AliasToBean<GoalViewInQuery>())
                .List<GoalViewInQuery>();

            var result = list
                .GroupBy(g => g.Id)
                .Select(x => x.First().Create(x.ToList<UserViewInQuery>()))
                .ToArray();

            return new GoalListResult(result, result.Length);
        }
        #endregion
    }
}
