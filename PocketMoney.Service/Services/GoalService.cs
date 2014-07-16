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
        private IRepository<ActionLog, ActionLogId, Guid> _auditLogRepository;
        #endregion

        #region Ctor
        public GoalService(
            IRepository<Performer, PerformerId, Guid> performerRepository,
            IRepository<Attainment, AttainmentId, Guid> attainmentRepository,
            IRepository<Task, TaskId, Guid> taskRepository,
            IRepository<User, UserId, Guid> userRepository,
            IRepository<Family, FamilyId, Guid> familyRepository,
            IRepository<ActionLog, ActionLogId, Guid> auditLogRepository,
            ICurrentUserProvider currentUserProvider)
            : base(userRepository, familyRepository, currentUserProvider)
        {
            _taskRepository = taskRepository;
            _attainmentRepository = attainmentRepository;
            _performerRepository = performerRepository;
            _auditLogRepository = auditLogRepository;
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
                throw new InvalidDataException("Goal with identifier {0} has not been found.", model.Id);
            }

            //goal.Active = true;
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

        [Transaction(TransactionMode.Requires)]
        [MethodImpl(MethodImplOptions.Synchronized)]
        [OperationBehavior(TransactionScopeRequired = true)]
        public GuidResult PostNewAttainment(AddAttainmentRequest model)
        {
            var currentUser = _currentUserProvider.GetCurrentUser().To();

            Attainment attainment = new Attainment(model.Text, currentUser);

            _attainmentRepository.Add(attainment);

            currentUser.Counts.GoodDeed(x => _auditLogRepository.Add(new ActionLog(attainment, x, ActionValueType.GoodDeed)));

            _userRepository.Update(currentUser);

            return new GuidResult(attainment.Id);
        }

        [Transaction(TransactionMode.Requires)]
        [MethodImpl(MethodImplOptions.Synchronized)]
        [OperationBehavior(TransactionScopeRequired = true)]
        public Result AppointReward(AppointRewardRequest model)
        {
            Attainment attainment = _attainmentRepository.One(new AttainmentId(model.Id));

            if (attainment == null)
            {
                throw new InvalidDataException("Attainment with identifier {0} has not been found.", model.Id);
            }

            var reward = new Reward(attainment, model.Points, model.Gift);

            attainment.Process(reward);

            var user = attainment.Creator;

            user.Points.Deposit(reward, (p, v) => _auditLogRepository.Add(new ActionLog(attainment, p, v)));

            _userRepository.Update(user);

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
                throw new InvalidDataException("Cannot found task.");
            }
        }

        [OperationBehavior, MethodImpl(MethodImplOptions.Synchronized)]
        public GoalListResult AllGoals(Request model)
        {
            var currentUser = _currentUserProvider.GetCurrentUser();

            Performer performer = null;
            User user = null;
            GoalViewInQuery goal = null;

            var list = _taskRepository.QueryOverOf<Goal, Task, TaskId, Guid>()
                .JoinAlias(x => x.AssignedTo, () => performer, JoinType.LeftOuterJoin)
                .JoinAlias(() => performer.User, () => user, JoinType.LeftOuterJoin)
                .Where(x => x.Family.Id == currentUser.Family.Id && x.Active && x.Type.Id == TaskType.GOAL_TYPE)
                .Where(() => performer.Status != eTaskStatus.Closed)
                .Select(
                    Projections.Property<Goal>(x => x.Id).WithAlias(() => goal.Id),
                    Projections.Property<Goal>(x => x.Details).WithAlias(() => goal.Details),
                    Projections.Property<Goal>(x => x.Reward).WithAlias(() => goal.Reward),
                    Projections.Property(() => user.Id).WithAlias(() => goal.UserId),
                    Projections.Property(() => user.UserName).WithAlias(() => goal.UserName),
                    Projections.Property(() => user.AdditionalName).WithAlias(() => goal.AdditionalName)
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

        [OperationBehavior, MethodImpl(MethodImplOptions.Synchronized)]
        public AttainmentResult GetAttainment(GuidRequest attainmentId)
        {
            Attainment attainment = _attainmentRepository.One(new AttainmentId(attainmentId.Data));
            if (attainment != null)
            {
                return new AttainmentResult(new AttainmentView(attainment));
            }
            else
            {
                throw new InvalidDataException("Cannot found attainment.");
            }
        }

        [OperationBehavior, MethodImpl(MethodImplOptions.Synchronized)]
        public AttainmentListResult AllAttainments(Request model)
        {
            var currentUser = _currentUserProvider.GetCurrentUser();

            var list = _attainmentRepository
                .FindAll(x => x.Family.Id == currentUser.Family.Id)
                .OrderBy(x => x.Processed)
                .OrderByDescending(x => x.DateCreated)
                .AsEnumerable()
                .Select(x => new AttainmentView(x))
                .ToArray();

            return new AttainmentListResult(list, list.Length);
        }
        #endregion

    }
}
