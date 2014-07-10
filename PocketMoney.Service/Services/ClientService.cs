using Castle.Services.Transaction;
using PocketMoney.Data;
using PocketMoney.Model;
using PocketMoney.Model.External.Requests;
using PocketMoney.Model.External.Results.Clients;
using PocketMoney.Model.Internal;
using PocketMoney.Service.Interfaces;
using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.ServiceModel;
using System.ServiceModel.Activation;
using Results = PocketMoney.Model.External.Results;
using NHibernate;
using NHibernate.Linq;
using PocketMoney.Data.NHibernate;
using NHibernate.SqlCommand;
using PocketMoney.Util;
using NHibernate.Criterion;
using NHibernate.Transform;
using System.Collections.Generic;

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

        public ClientService(
            IRepository<User, UserId, Guid> userRepository,
            IRepository<Family, FamilyId, Guid> familyRepository,
            IRepository<Performer, PerformerId, Guid> performerRepository,
            IRepository<Task, TaskId, Guid> taskRepository,
            IRepository<TaskDate, TaskDateId, Guid> taskDateRepository,
            IRepository<TaskAction, TaskActionId, Guid> taskActionRepository,
            ICurrentUserProvider currentUserProvider)
            : base(userRepository, familyRepository, currentUserProvider)
        {
            _performerRepository = performerRepository;
            _taskDateRepository = taskDateRepository;
            _taskRepository = taskRepository;
            _taskActionRepository = taskActionRepository;
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
            var dateRange = new CurrentDates(Clock.UtcNow());

            var actions = _taskActionRepository
                .FindAll(x =>
                    x.NewStatus == eTaskStatus.Closed &&
                    x.Performer.User.Id == user.Id &&
                    x.Performer.Active &&
                    x.TaskDate.Task.Status != eTaskStatus.Closed &&
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
                    x.Task.Status != eTaskStatus.Closed &&
                    x.Task.HasDates)
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
                .Where(x => x.Status != eTaskStatus.Closed && performer.User.Id == user.Id && performer.Active)
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
                .Where(x => x.Status == eTaskStatus.New)
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
                .GroupBy(k => k.TaskId)
                .Select(x => x.First())
                .Distinct()
                .ToArray();

            return new TaskListResult(resultList, resultList.Length);
        }
    }
}
