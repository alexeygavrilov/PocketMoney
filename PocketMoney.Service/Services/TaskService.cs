﻿using System;
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

namespace PocketMoney.Service
{
    [Transactional]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class TaskService : BaseService, ITaskService
    {
        private IRepository<Task, TaskId, Guid> _taskRepository;
        private IRepository<Performer, PerformerId, Guid> _performerRepository;
        private IRepository<TaskDate, TaskDateId, Guid> _taskDateRepository;

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

        [Transaction(TransactionMode.Requires)]
        [MethodImpl(MethodImplOptions.Synchronized)]
        [OperationBehavior(TransactionScopeRequired = true)]
        public GuidResult AddOneTimeTask(AddOneTimeTaskRequest model)
        {
            var currentUser = _currentUserProvider.GetCurrentUser();

            OneTimeTask task = new OneTimeTask(
                model.Text,
                model.Points,
                model.DeadlineDate,
                currentUser.To()
                );

            _taskRepository.Add(task);

            foreach (var userId in model.AssignedTo)
            {
                var user = _userRepository.One(new UserId(userId));
                Performer performer = new Performer(task, user);
                _performerRepository.Add(performer);
            }

            return new GuidResult(task.Id);
        }

        [Transaction(TransactionMode.Requires)]
        [MethodImpl(MethodImplOptions.Synchronized)]
        [OperationBehavior(TransactionScopeRequired = true)]
        public GuidResult AddHomeworkTask(AddHomeworkTaskRequest model)
        {
            var currentUser = _currentUserProvider.GetCurrentUser();

            HomeworkTask task = new HomeworkTask(
                model.Text,
                model.Points,
                currentUser.To(),
                Convert.ToBase64String(BinarySerializer.Serialaize(model.Form))
                );

            _taskRepository.Add(task);

            foreach (var userId in model.AssignedTo)
            {
                var user = _userRepository.One(new UserId(userId));
                Performer performer = new Performer(task, user);
                _performerRepository.Add(performer);
            }

            foreach (var dateOfOne in model.Form.CalculateDates())
            {
                TaskDate taskDate = new TaskDate(task, dateOfOne);
                _taskDateRepository.Add(taskDate);
            }
            
            return new GuidResult(task.Id);
        }
    }
}
