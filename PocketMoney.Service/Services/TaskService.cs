using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.ServiceModel;
using System.ServiceModel.Activation;
using Castle.Services.Transaction;
using PocketMoney.Data;
using PocketMoney.Model;
using PocketMoney.Model.External.Requests;
using PocketMoney.Model.External.Results;
using PocketMoney.Model.Internal;
using PocketMoney.Service.Interfaces;
using PocketMoney.Util.Serialization;
using Microsoft.Practices.ServiceLocation;
using I = PocketMoney.Model.Internal;
using E = PocketMoney.Model.External;
using System.Collections.Generic;
using NHibernate.Linq;
using NHibernate;
using PocketMoney.Data.NHibernate;
using NHibernate.SqlCommand;
using NHibernate.Criterion;
using NHibernate.Transform;
using PocketMoney.Model.External;

namespace PocketMoney.Service
{
    [Transactional]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class TaskService : BaseService, ITaskService
    {
        #region Members
        private IRepository<Task, TaskId, Guid> _taskRepository;
        private IRepository<Performer, PerformerId, Guid> _performerRepository;
        private IRepository<TaskDate, TaskDateId, Guid> _taskDateRepository;
        #endregion

        #region Ctor
        public TaskService(
            IRepository<Task, TaskId, Guid> taskRepository,
            IRepository<Performer, PerformerId, Guid> performerRepository,
            IRepository<TaskDate, TaskDateId, Guid> taskDateRepository,
            IRepository<User, UserId, Guid> userRepository,
            IRepository<Family, FamilyId, Guid> familyRepository,
            ICurrentUserProvider currentUserProvider)
            : base(userRepository, familyRepository, currentUserProvider)
        {
            _taskRepository = taskRepository;
            _performerRepository = performerRepository;
            _taskDateRepository = taskDateRepository;
        }
        #endregion

        #region Private Methods
        private GuidResult AddTask<TModel, TEntity>(TModel model, Func<IUser, TEntity> createEntity, Action<TEntity> afterCreate = null)
            where TEntity : Task
            where TModel : BaseTaskRequest
        {
            var currentUser = _currentUserProvider.GetCurrentUser();

            TEntity task = createEntity(currentUser);

            if (model.ReminderTime.HasValue)
            {
                task.Reminder = (int)model.ReminderTime.Value.TotalMinutes;
            }

            _taskRepository.Add(task);

            foreach (var userId in model.AssignedTo)
            {
                var user = _userRepository.One(new UserId(userId));
                Performer performer = new Performer(task, user);
                _performerRepository.Add(performer);
                task.AssignedTo.Add(performer);
            }

            if (afterCreate != null)
            {
                afterCreate(task);
            }

            return new GuidResult(task.Id);
        }

        private Result UpdateTask<TModel, TEntity>(TModel model, Action<TEntity> updateEntity)
            where TEntity : Task
            where TModel : BaseTaskRequest, IIdentity
        {
            var currentUser = _currentUserProvider.GetCurrentUser();

            TEntity task = _taskRepository.One(new TaskId(model.Id)).As<TEntity>();

            if (task == null)
            {
                throw new InvalidDataException("Task with identifier {0} has not been found.", model.Id);
            }

            updateEntity(task);

            task.Active = true;
            task.Details = model.Text;
            task.Reward = new Reward(task, model.Points, model.Gift);

            if (model.ReminderTime.HasValue)
            {
                task.Reminder = (int)model.ReminderTime.Value.TotalMinutes;
            }

            _taskRepository.Update(task);

            foreach (var user in _userRepository.FindAll(x => x.Family.Id == currentUser.Family.Id))
            {
                if (model.AssignedTo.Any(x => x == user.Id) && !task.AssignedTo.Any(x => x.User.Id == user.Id))
                {
                    Performer performer = new Performer(task, user);
                    _performerRepository.Add(performer);
                    task.AssignedTo.Add(performer);
                }
                else if (!model.AssignedTo.Any(x => x == user.Id) && task.AssignedTo.Any(x => x.User.Id == user.Id))
                {
                    var performer = task.AssignedTo.First(x => x.User.Id == user.Id);
                    task.AssignedTo.Remove(performer);
                    _performerRepository.Remove(performer);
                }
            }

            return Result.Successfully();
        }

        private TResult GetTask<TResult, TView, TEntity>(GuidRequest taskId, Func<TEntity, TView> view)
            where TResult : ResultData<TView>, new()
            where TView : TaskView
            where TEntity : Task
        {
            TResult result = new TResult();
            TEntity task = _taskRepository.One(new TaskId(taskId.Data)).As<TEntity>();
            if (task != null)
            {
                result.Data = view(task);
            }
            else
            {
                result.SetErrorMessage("Cannot found task.");
            }
            return result;
        }

        private IList<TaskDate> UpdateDates(IScheduleForm form, Task task)
        {
            var modelDates = form.CalculateDates();

            var taskDates = _taskDateRepository.FindAll(x => x.Task.Id == task.Id).ToList();

            for (var i = taskDates.Count - 1; i >= 0; i--)
            {
                var date = taskDates[i];
                if (!modelDates.Any(x => x == date.Date))
                {
                    _taskDateRepository.Remove(date);
                    taskDates.RemoveAt(i);
                }
            }

            foreach (var dateOfOne in modelDates)
            {
                if (!taskDates.Any(x => x.Date == dateOfOne))
                {
                    TaskDate taskDate = new TaskDate(task, dateOfOne);
                    _taskDateRepository.Add(taskDate);
                    taskDates.Add(taskDate);
                }
            }

            return taskDates;
        }

        #endregion

        #region Add Methods
        [Transaction(TransactionMode.Requires)]
        [MethodImpl(MethodImplOptions.Synchronized)]
        [OperationBehavior(TransactionScopeRequired = true)]
        public GuidResult AddOneTimeTask(AddOneTimeTaskRequest model)
        {
            return this.AddTask<AddOneTimeTaskRequest, OneTimeTask>(model,
                currentUser => new OneTimeTask(
                                model.Name,
                                model.Text,
                                new Reward(model.Points, model.Gift),
                                model.DeadlineDate,
                                currentUser.To()));
        }

        [Transaction(TransactionMode.Requires)]
        [MethodImpl(MethodImplOptions.Synchronized)]
        [OperationBehavior(TransactionScopeRequired = true)]
        public GuidResult AddHomeworkTask(AddHomeworkTaskRequest model)
        {
            return this.AddTask<AddHomeworkTaskRequest, HomeworkTask>(model,
                currentUser => new HomeworkTask(
                                model.Text,
                                new Reward(model.Points, model.Gift),
                                currentUser.To(),
                                model.Lesson,
                                Convert.ToBase64String(BinarySerializer.Serialaize(model.Form))),
                task =>
                {
                    foreach (var dateOfOne in model.Form.CalculateDates())
                    {
                        TaskDate taskDate = new TaskDate(task, dateOfOne);
                        _taskDateRepository.Add(taskDate);
                    }
                });
        }

        [Transaction(TransactionMode.Requires)]
        [MethodImpl(MethodImplOptions.Synchronized)]
        [OperationBehavior(TransactionScopeRequired = true)]
        public GuidResult AddCleanTask(AddCleanTaskRequest model)
        {
            return this.AddTask<AddCleanTaskRequest, CleanTask>(model,
                currentUser => new CleanTask(
                                model.RoomName,
                                model.Text,
                                new Reward(model.Points, model.Gift),
                                currentUser.To(),
                                model.GetDaysOfWeek()));
        }

        [Transaction(TransactionMode.Requires)]
        [MethodImpl(MethodImplOptions.Synchronized)]
        [OperationBehavior(TransactionScopeRequired = true)]
        public GuidResult AddRepeatTask(AddRepeatTaskRequest model)
        {
            return this.AddTask<AddRepeatTaskRequest, RepeatTask>(model,
                currentUser => new RepeatTask(
                            model.Name,
                            model.Text,
                            new Reward(model.Points, model.Gift),
                            currentUser.To(),
                            Convert.ToBase64String(BinarySerializer.Serialaize(model.Form))),
                task =>
                {
                    foreach (var dateOfOne in model.Form.CalculateDates())
                    {
                        TaskDate taskDate = new TaskDate(task, dateOfOne);
                        _taskDateRepository.Add(taskDate);
                    }
                });
        }


        [Transaction(TransactionMode.Requires)]
        [MethodImpl(MethodImplOptions.Synchronized)]
        [OperationBehavior(TransactionScopeRequired = true)]
        public GuidResult AddShoppingTask(AddShoppingTaskRequest model)
        {
            return this.AddTask<AddShoppingTaskRequest, ShopTask>(model,
                currentUser => new ShopTask(model.ShopName,
                                model.Text,
                                new Reward(model.Points, model.Gift),
                                model.DeadlineDate,
                                currentUser.To()),
                task =>
                {
                    var shopItemRepository = ServiceLocator.Current.GetInstance<IRepository<I.ShopItem, I.ShopItemId, Guid>>();
                    foreach (var item in model.ShoppingList)
                    {
                        shopItemRepository.Add(new I.ShopItem(task, item.ItemName, item.Qty, item.OrderNumber));
                    }
                });
        }
        #endregion

        #region Update Methods
        [Transaction(TransactionMode.Requires)]
        [MethodImpl(MethodImplOptions.Synchronized)]
        [OperationBehavior(TransactionScopeRequired = true)]
        public Result UpdateOneTimeTask(UpdateOneTimeTaskRequest model)
        {
            return this.UpdateTask<UpdateOneTimeTaskRequest, OneTimeTask>(model, task =>
            {
                task.OneTimeName = model.Name;
                task.DeadlineDate = model.DeadlineDate;
            });
        }

        [Transaction(TransactionMode.Requires)]
        [MethodImpl(MethodImplOptions.Synchronized)]
        [OperationBehavior(TransactionScopeRequired = true)]
        public Result UpdateRepeatTask(UpdateRepeatTaskRequest model)
        {
            return this.UpdateTask<UpdateRepeatTaskRequest, RepeatTask>(model, task =>
            {
                task.RepeatName = model.Name;
                task.Form = Convert.ToBase64String(BinarySerializer.Serialaize(model.Form));
                task.Dates = this.UpdateDates(model.Form, task);
            });
        }

        [Transaction(TransactionMode.Requires)]
        [MethodImpl(MethodImplOptions.Synchronized)]
        [OperationBehavior(TransactionScopeRequired = true)]
        public Result UpdateHomeworkTask(UpdateHomeworkTaskRequest model)
        {
            return this.UpdateTask<UpdateHomeworkTaskRequest, HomeworkTask>(model, task =>
            {
                task.Form = Convert.ToBase64String(BinarySerializer.Serialaize(model.Form));

                task.Dates = this.UpdateDates(model.Form, task);
            });
        }

        [Transaction(TransactionMode.Requires)]
        [MethodImpl(MethodImplOptions.Synchronized)]
        [OperationBehavior(TransactionScopeRequired = true)]
        public Result UpdateCleanTask(UpdateCleanTaskRequest model)
        {
            return this.UpdateTask<UpdateCleanTaskRequest, CleanTask>(model, task =>
            {
                task.RoomName = model.RoomName;
                task.DaysOfWeek = model.GetDaysOfWeek();
            });
        }

        [Transaction(TransactionMode.Requires)]
        [MethodImpl(MethodImplOptions.Synchronized)]
        [OperationBehavior(TransactionScopeRequired = true)]
        public Result UpdateShoppingTask(UpdateShoppingTaskRequest model)
        {
            return this.UpdateTask<UpdateShoppingTaskRequest, ShopTask>(model, task =>
            {
                task.ShopName = model.ShopName;
                task.DeadlineDate = model.DeadlineDate;

                var shopItemRepository = ServiceLocator.Current.GetInstance<IRepository<I.ShopItem, I.ShopItemId, Guid>>();

                var taskList = task.ShoppingList;

                for (var i = taskList.Count - 1; i >= 0; i--)
                {
                    var shopItem = taskList[i];
                    if (!model.ShoppingList.Any(x => x.ItemName == shopItem.Name))
                    {
                        shopItemRepository.Remove(shopItem);
                        taskList.RemoveAt(i);
                    }
                }

                foreach (var item in model.ShoppingList)
                {
                    var shopItem = taskList.FirstOrDefault(x => x.Name == item.ItemName);
                    if (shopItem != null)
                    {
                        if (shopItem.Qty != item.Qty || shopItem.OrderNumber != item.OrderNumber)
                        {
                            shopItem.Qty = item.Qty;
                            shopItem.OrderNumber = item.OrderNumber;
                            shopItemRepository.Update(shopItem);
                        }
                    }
                    else
                    {
                        shopItem = new I.ShopItem(task, item.ItemName, item.Qty, item.OrderNumber);
                        shopItemRepository.Add(shopItem);
                        taskList.Add(shopItem);
                    }
                }
            });
        }
        #endregion

        #region Get Methods
        [OperationBehavior, MethodImpl(MethodImplOptions.Synchronized)]
        public OneTimeTaskResult GetOneTimeTask(GuidRequest taskId)
        {
            return this.GetTask<OneTimeTaskResult, OneTimeTaskView, OneTimeTask>(taskId, task => new OneTimeTaskView(task));
        }

        [OperationBehavior, MethodImpl(MethodImplOptions.Synchronized)]
        public RepeatTaskResult GetRepeatTask(GuidRequest taskId)
        {
            return this.GetTask<RepeatTaskResult, RepeatTaskView, RepeatTask>(taskId, task => new RepeatTaskView(task));
        }

        [OperationBehavior, MethodImpl(MethodImplOptions.Synchronized)]
        public HomeworkTaskResult GetHomeworkTask(GuidRequest taskId)
        {
            return this.GetTask<HomeworkTaskResult, HomeworkTaskView, HomeworkTask>(taskId, task => new HomeworkTaskView(task));
        }

        [OperationBehavior, MethodImpl(MethodImplOptions.Synchronized)]
        public CleanTaskResult GetCleanTask(GuidRequest taskId)
        {
            return this.GetTask<CleanTaskResult, CleanTaskView, CleanTask>(taskId, task => new CleanTaskView(task));
        }

        [OperationBehavior, MethodImpl(MethodImplOptions.Synchronized)]
        public ShoppingTaskResult GetShoppingTask(GuidRequest taskId)
        {
            return this.GetTask<ShoppingTaskResult, ShoppingTaskView, ShopTask>(taskId, task => new ShoppingTaskView(task));
        }
        #endregion

        #region List Methods

        [OperationBehavior, MethodImpl(MethodImplOptions.Synchronized)]
        public TaskListResult AllTasks(Request model)
        {
            var currentUser = _currentUserProvider.GetCurrentUser();

            Performer performer = null;
            User user = null;
            TaskViewInQuery task = null;

            var list = _taskRepository.QueryOver<Task, TaskId, Guid>()
                .JoinAlias(x => x.AssignedTo, () => performer, JoinType.LeftOuterJoin)
                .JoinAlias(() => performer.User, () => user, JoinType.LeftOuterJoin)
                .Where(x => x.Family.Id == currentUser.Family.Id && x.Active && x.Type.Id != TaskType.Goal.Id)
                .Select(
                    Projections.Property<Task>(x => x.Id).WithAlias(() => task.Id),
                    Projections.Property<Task>(x => x.Type).WithAlias(() => task.TaskType),
                    Projections.Property<Task>(x => x.Details).WithAlias(() => task.Details),
                    Projections.Property<Task>(x => x.Reward).WithAlias(() => task.Reward),
                    Projections.Property<Task>(x => x.Reminder).WithAlias(() => task.Reminder),
                    Projections.Property<CleanTask>(c => c.RoomName).WithAlias(() => task.RoomName),
                    Projections.Property<ShopTask>(s => s.ShopName).WithAlias(() => task.ShopName),
                    Projections.Property<RepeatTask>(r => r.RepeatName).WithAlias(() => task.RepeatName),
                    Projections.Property<OneTimeTask>(o => o.OneTimeName).WithAlias(() => task.OneTimeName),
                    Projections.Property<HomeworkTask>(o => o.Lesson).WithAlias(() => task.LessonName),
                    Projections.Property(() => user.Id).WithAlias(() => task.UserId),
                    Projections.Property(() => user.UserName).WithAlias(() => task.UserName),
                    Projections.Property(() => user.AdditionalName).WithAlias(() => task.AdditionalName)
                    )
                .OrderBy(x => x.DateCreated).Asc
                .TransformUsing(Transformers.AliasToBean<TaskViewInQuery>())
                .List<TaskViewInQuery>();

            var result = list
                .GroupBy(g => g.Id)
                .Select(x => x.First().Create(x.ToList<UserViewInQuery>()))
                .ToArray();

            return new TaskListResult(result, result.Length);
        }

        [OperationBehavior, MethodImpl(MethodImplOptions.Synchronized)]
        public TaskListResult MyTasks(Request model)
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}
