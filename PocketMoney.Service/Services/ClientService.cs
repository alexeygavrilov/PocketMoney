using Castle.Services.Transaction;
using Microsoft.Practices.ServiceLocation;
using NHibernate.Criterion;
using NHibernate.Linq;
using NHibernate.SqlCommand;
using NHibernate.Transform;
using PocketMoney.Data;
using PocketMoney.Data.NHibernate;
using PocketMoney.Model;
using PocketMoney.Model.External.Requests;
using PocketMoney.Model.External.Results.Clients;
using PocketMoney.Model.Internal;
using PocketMoney.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.ServiceModel;
using System.ServiceModel.Activation;
using Results = PocketMoney.Model.External.Results;

namespace PocketMoney.Service
{
    [Transactional]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class ClientService : BaseService, IClientService
    {
        private readonly IRepository<Performer, PerformerId, Guid> _performerRepository = null;
        private readonly IRepository<Task, TaskId, Guid> _taskRepository = null;
        private readonly IRepository<TaskDate, TaskDateId, Guid> _taskDateRepository = null;
        private readonly IRepository<TaskAction, TaskActionId, Guid> _taskActionRepository = null;
        private readonly IRepository<Attainment, AttainmentId, Guid> _attainmentRepository = null;
        private IRepository<ActionLog, ActionLogId, Guid> _auditLogRepository = null;
        
        public ClientService(
            IRepository<User, UserId, Guid> userRepository,
            IRepository<Family, FamilyId, Guid> familyRepository,
            IRepository<Performer, PerformerId, Guid> performerRepository,
            IRepository<Task, TaskId, Guid> taskRepository,
            IRepository<TaskDate, TaskDateId, Guid> taskDateRepository,
            IRepository<TaskAction, TaskActionId, Guid> taskActionRepository,
            IRepository<ActionLog, ActionLogId, Guid> auditLogRepository,
            IRepository<Attainment, AttainmentId, Guid> attainmentRepository,
            ICurrentUserProvider currentUserProvider)
            : base(userRepository, familyRepository, currentUserProvider)
        {
            _performerRepository = performerRepository;
            _taskDateRepository = taskDateRepository;
            _taskRepository = taskRepository;
            _taskActionRepository = taskActionRepository;
            _auditLogRepository = auditLogRepository;
            _attainmentRepository = attainmentRepository;
        }

        [Transaction(TransactionMode.Requires)]
        [MethodImpl(MethodImplOptions.Synchronized)]
        [OperationBehavior(TransactionScopeRequired = true)]
        public Results.AuthResult Login(LoginRequest model)
        {
            throw new NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        [OperationBehavior]
        public Results.UserResult GetCurrentUser(Request model)
        {
            User user = _currentUserProvider.GetCurrentUser().To();
            if (user != null)
            {
                return new Results.UserResult(new Results.UserView(user));
            }
            else
            {
                throw new InvalidDataException("Cannot found current user");
            }
        }

        private IList<TaskView> FindDates(IUser user, IList<TaskViewInQuery> taskList)
        {
            var dateRange = new CurrentDates(_currentUserProvider.GetCurrentDate());

            var actions = _taskActionRepository
                .FindAll(x =>
                    x.NewStatus == eTaskStatus.Closed &&
                    x.Performer.User.Id == user.Id &&
                    x.TaskDate.Task.Active &&
                    x.Performer.Status != eTaskStatus.Closed &&
                    x.TaskDate.Date.Value >= dateRange.Yesterday.Value &&
                    x.TaskDate.Date.Value <= dateRange.Tomorrow.Value)
                .Select(x => new
                {
                    TaskId = x.TaskDate.Task.Id,
                    x.TaskDate.Date
                }).ToList();

            var dates = _taskDateRepository
                .FindAll(x =>
                    x.Date.Value >= dateRange.Yesterday.Value &&
                    x.Date.Value <= dateRange.Tomorrow.Value &&
                    x.Task.Family.Id == user.Family.Id &&
                    x.Task.Active && x.Task.HasDates)
                .Select(x => new
                {
                    TaskId = x.Task.Id,
                    x.Date
                }).ToList();

            IList<TaskView> resultList = new List<TaskView>();

            foreach (var item in taskList)
            {
                foreach (var task in item.Create(
                    d =>
                        actions.Any(a => a.TaskId == item.Id && a.Date == d),
                    d =>
                        dates.Any(x => x.TaskId == item.Id && x.Date == d),
                    dateRange))
                {
                    resultList.Add(task);
                }
            }

            return resultList;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        [OperationBehavior]
        public TaskListResult GetTaskList(Request model)
        {
            var user = _currentUserProvider.GetCurrentUser();

            TaskViewInQuery result = null;
            Performer performer = null;

            var taskList = _taskRepository.QueryOver<Task, TaskId, Guid>()
                .JoinAlias(x => x.AssignedTo, () => performer, JoinType.InnerJoin)
                .Where(x => x.Active == true &&
                    performer.Status != eTaskStatus.Closed &&
                    performer.User.Id == user.Id &&
                    x.Type.Id != TaskType.GOAL_TYPE)
                .Select(
                    Projections.Property<Task>(x => x.Id).WithAlias(() => result.Id),
                    Projections.Property<Task>(x => x.Type).WithAlias(() => result.TaskType),
                    Projections.Property<Task>(x => x.Details).WithAlias(() => result.Desctiption),
                    Projections.Property<Task>(x => x.Reward).WithAlias(() => result.Reward),
                    Projections.Property<Task>(x => x.HasDates).WithAlias(() => result.HasDates),
                    Projections.Property<CleanTask>(x => x.RoomName).WithAlias(() => result.Clean_RoomName),
                    Projections.Property<CleanTask>(x => x.DaysOfWeek).WithAlias(() => result.Clean_DaysOfWeek),
                    Projections.Property<ShopTask>(s => s.ShopName).WithAlias(() => result.Shop_Name),
                    Projections.Property<ShopTask>(s => s.DeadlineDate).WithAlias(() => result.Shop_DeadlineDate),
                    Projections.Property<RepeatTask>(r => r.RepeatName).WithAlias(() => result.Repeat_Name),
                    Projections.Property<OneTimeTask>(o => o.OneTimeName).WithAlias(() => result.OneTime_Name),
                    Projections.Property<OneTimeTask>(o => o.DeadlineDate).WithAlias(() => result.OneTime_DeadlineDate),
                    Projections.Property<HomeworkTask>(o => o.Lesson).WithAlias(() => result.Homework_LessonName)
                    )
                .TransformUsing(Transformers.AliasToBean<TaskViewInQuery>())
                .List<TaskViewInQuery>();

            var resultList = this.FindDates(user, taskList).ToArray();

            return new TaskListResult(resultList, resultList.Length);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        [OperationBehavior]
        public TaskListResult GetFloatingTaskList(Request model)
        {
            TaskViewInQuery result = null;

            var taskList = _taskRepository.QueryOver<Task, TaskId, Guid>()
                .Where(x => x.Active == true && x.Type.Id != TaskType.GOAL_TYPE)
                .WhereRestrictionOn(x => x.AssignedTo).IsEmpty()
                .Select(
                    Projections.Property<Task>(x => x.Id).WithAlias(() => result.Id),
                    Projections.Property<Task>(x => x.Type).WithAlias(() => result.TaskType),
                    Projections.Property<Task>(x => x.Details).WithAlias(() => result.Desctiption),
                    Projections.Property<Task>(x => x.Reward).WithAlias(() => result.Reward),
                    Projections.Property<Task>(x => x.HasDates).WithAlias(() => result.HasDates),
                    Projections.Property<CleanTask>(x => x.RoomName).WithAlias(() => result.Clean_RoomName),
                    Projections.Property<CleanTask>(x => x.DaysOfWeek).WithAlias(() => result.Clean_DaysOfWeek),
                    Projections.Property<ShopTask>(s => s.ShopName).WithAlias(() => result.Shop_Name),
                    Projections.Property<ShopTask>(s => s.DeadlineDate).WithAlias(() => result.Shop_DeadlineDate),
                    Projections.Property<RepeatTask>(r => r.RepeatName).WithAlias(() => result.Repeat_Name),
                    Projections.Property<OneTimeTask>(o => o.OneTimeName).WithAlias(() => result.OneTime_Name),
                    Projections.Property<OneTimeTask>(o => o.DeadlineDate).WithAlias(() => result.OneTime_DeadlineDate),
                    Projections.Property<HomeworkTask>(o => o.Lesson).WithAlias(() => result.Homework_LessonName)
                    )
                .TransformUsing(Transformers.AliasToBean<TaskViewInQuery>())
                .List<TaskViewInQuery>();

            var resultList = this.FindDates(_currentUserProvider.GetCurrentUser(), taskList)
                .GroupBy(k => k.Id)
                .Select(x => x.First())
                .Distinct()
                .ToArray();

            return new TaskListResult(resultList, resultList.Length);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        [OperationBehavior]
        public GoalListResult GetGoalList(Request model)
        {
            var currentUser = _currentUserProvider.GetCurrentUser();

            Performer performer = null;
            User user = null;

            var list = _taskRepository
                .QueryOverOf<Goal, Task, TaskId, Guid>()
                .JoinAlias(x => x.AssignedTo, () => performer, JoinType.InnerJoin)
                .JoinAlias(() => performer.User, () => user, JoinType.InnerJoin)
                .Where(x => x.Family.Id == currentUser.Family.Id && x.Active && x.Type.Id == TaskType.GOAL_TYPE)
                .Where(() => performer.Status != eTaskStatus.Closed && user.Id == currentUser.Id)
                .OrderBy(x => x.DateCreated).Asc
                .List();

            var result = list.Select(g => new GoalView(g)).ToArray();

            return new GoalListResult(result, result.Length);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        [OperationBehavior]
        public AttainmentListResult GetGoodDeedList(Request model)
        {
            var user = _currentUserProvider.GetCurrentUser();

            var list = _attainmentRepository
                .FindAll(x => x.Creator.Id == user.Id)
                .OrderBy(x => x.Processed)
                .OrderByDescending(x => x.DateCreated)
                .AsEnumerable()
                .Select(a => new AttainmentView(a))
                .ToArray();

            return new AttainmentListResult(list, list.Length);
        }

        [Transaction(TransactionMode.Requires)]
        [MethodImpl(MethodImplOptions.Synchronized)]
        [OperationBehavior(TransactionScopeRequired = true)]
        public Result DoneTask(DoneTaskRequest model)
        {
            var user = _currentUserProvider.GetCurrentUser().To();

            var task = _taskRepository.One(new TaskId(model.Id));
            if (task == null)
            {
                throw new InvalidDataException("Task with identifier {0} has not been found.", model.Id);
            }
            if (!task.Active)
            {
                throw new InvalidDataException("You cannot done inactive task '{0}'.", task.Name);
            }
            Performer performer = _performerRepository.FindOne(x => x.User.Id == user.Id && x.Task.Id == model.Id);
            if (performer == null)
            {
                throw new InvalidDataException("'{0}' task is not assigned to you.", task.Name);
            }
            if (performer.Status == eTaskStatus.Closed)
            {
                throw new InvalidDataException("You already done this '{0}' task.", task.Name);
            }

            TaskAction action = new TaskAction(eTaskStatus.Closed, performer, model.Note);

            if (task.HasDates)
            {
                DayOfOne input = new DayOfOne(_currentUserProvider.GetCurrentDate());

                if (model.DateType == eDateType.Yesterday)
                {
                    input--;
                }
                else if (model.DateType == eDateType.Tomorrow)
                {
                    input++;
                }

                var taskDate = _taskDateRepository.FindOne(x => x.Date.Value == input.Value && x.Task.Id == model.Id);
                if (taskDate == null)
                {
                    throw new InvalidDataException("Cannot found task date at {0}.", model.DateType);
                }

                action.TaskDate = taskDate;
            }
            else
            {
                performer.Status = eTaskStatus.Closed;
                _performerRepository.Update(performer);
            }

            _taskActionRepository.Add(action);

            user.Counts.CompleteTask(task.Type,
                c => _auditLogRepository.Add(new ActionLog(task, c, ActionValueType.CompleteTask)));

            user.Points.Deposit(task.Reward,
                (p, v) => _auditLogRepository.Add(new ActionLog(task, p, v)));

            _userRepository.Update(user);

            return Result.Successfully();
        }

        [Transaction(TransactionMode.Requires)]
        [MethodImpl(MethodImplOptions.Synchronized)]
        [OperationBehavior(TransactionScopeRequired = true)]
        public Result GrabbTask(ProcessRequest model)
        {
            var user = _currentUserProvider.GetCurrentUser().To();

            var task = _taskRepository.One(new TaskId(model.Id));
            if (task == null)
            {
                throw new InvalidDataException("Task with identifier {0} has not been found.", model.Id);
            }
            if (!task.Active)
            {
                throw new InvalidDataException("You cannot done inactive task '{0}'.", task.Name);
            }

            if (_performerRepository.Exists(x => x.User.Id == user.Id && x.Task.Id == model.Id))
            {
                throw new InvalidDataException("'{0}' already assigned to you.", task.Name);
            }

            var performer = new Performer(task, user);

            _performerRepository.Add(performer);

            _taskActionRepository.Add(new TaskAction(eTaskStatus.New, performer, model.Note));

            user.Counts.GrabTask(
                c => _auditLogRepository.Add(new ActionLog(task, c, ActionValueType.GrabTask)));

            _userRepository.Update(user);

            return Result.Successfully();
        }

        [Transaction(TransactionMode.Requires)]
        [MethodImpl(MethodImplOptions.Synchronized)]
        [OperationBehavior(TransactionScopeRequired = true)]
        public Result AchieveGoal(ProcessRequest model)
        {
            var user = _currentUserProvider.GetCurrentUser().To();

            var goal = _taskRepository.One(new TaskId(model.Id)).As<Goal>();
            if (goal == null)
            {
                throw new InvalidDataException("Goal with identifier {0} has not been found.", model.Id);
            }
            if (!goal.Active)
            {
                throw new InvalidDataException("You cannot done inactive goal '{0}'.", goal.Name);
            }

            Performer performer = _performerRepository.FindOne(x => x.User.Id == user.Id && x.Task.Id == model.Id);
            if (performer == null)
            {
                throw new InvalidDataException("'{0}' goal is not assigned to you.", goal.Name);
            }
            if (performer.Status == eTaskStatus.Closed)
            {
                throw new InvalidDataException("You already achieved this '{0}' goal.", goal.Name);
            }

            performer.Status = eTaskStatus.Closed;

            _performerRepository.Update(performer);

            _taskActionRepository.Add(new TaskAction(eTaskStatus.Closed, performer, model.Note));

            user.Counts.CompleteGoal(c => _auditLogRepository.Add(new ActionLog(goal, c, ActionValueType.CompleteGoal)));

            user.Points.Deposit(goal.Reward,
                (p, v) => _auditLogRepository.Add(new ActionLog(goal, p, v)));

            _userRepository.Update(user);

            return Result.Successfully();
        }

        [Transaction(TransactionMode.Requires)]
        [MethodImpl(MethodImplOptions.Synchronized)]
        [OperationBehavior(TransactionScopeRequired = true)]
        public Result AddGoodDeed(AddAttainmentRequest model)
        {
            var user = _currentUserProvider.GetCurrentUser().To();

            Attainment attainment = new Attainment(model.Text, user);

            _attainmentRepository.Add(attainment);

            return Result.Successfully();
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        [OperationBehavior]
        public ShopItemListResult GetShoppingList(GuidRequest taskId)
        {
            var shopItemRepository = ServiceLocator.Current.GetInstance<IRepository<ShopItem, ShopItemId, Guid>>();
            var result = shopItemRepository
                .FindAll(x => x.Task.Id == taskId.Data)
                .OrderBy(x => x.OrderNumber)
                .AsEnumerable()
                .Select(s => new PocketMoney.Model.External.ShopItem(s))
                .ToArray();

            return new ShopItemListResult(result, result.Length);
        }


        [Transaction(TransactionMode.Requires)]
        [MethodImpl(MethodImplOptions.Synchronized)]
        [OperationBehavior(TransactionScopeRequired = true)]
        public Result CheckShopItem(CheckShopItemRequest model)
        {
            var shopItemRepository = ServiceLocator.Current.GetInstance<IRepository<ShopItem, ShopItemId, Guid>>();
            var item = shopItemRepository.FindOne(x => x.Task.Id == model.TaskId && x.OrderNumber == model.OrderNumber);
            if (item != null)
            {
                item.Processed = model.Checked;
                
                shopItemRepository.Update(item);

                return Result.Successfully();
            }
            else
            {
                throw new InvalidDataException("Shopping item with Task identifier {0} has not been found.", model.TaskId);
            }
        }
    }
}
