using System;
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

namespace PocketMoney.Service
{
    [Transactional]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class TaskService : BaseService, ITaskService
    {
        private IRepository<Task, TaskId, Guid> _taskRepository;
        private IRepository<Performer, PerformerId, Guid> _performerRepository;

        public TaskService(
            IRepository<Task, TaskId, Guid> taskRepository,
            IRepository<Performer, PerformerId, Guid> performerRepository,
            IRepository<User, UserId, Guid> userRepository,
            IRepository<Family, FamilyId, Guid> familyRepository,
            ICurrentUserProvider currentUserProvider)
            : base(userRepository, familyRepository, currentUserProvider)
        {
            _taskRepository = taskRepository;
            _performerRepository = performerRepository;
        }

        [Transaction(TransactionMode.Requires)]
        [MethodImpl(MethodImplOptions.Synchronized)]
        [OperationBehavior(TransactionScopeRequired = true)]
        public OneTimeTaskResult AddOneTimeTask(AddOneTimeTaskRequest model)
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

            return new OneTimeTaskResult() { TaskId = task.Id };
        }
    }
}
