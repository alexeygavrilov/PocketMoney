using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using System.Threading.Tasks;
using Castle.Services.Transaction;
using PocketMoney.Data;
using PocketMoney.Model.External.Requests;
using PocketMoney.Model.External.Results;
using PocketMoney.Model.Internal;
using PocketMoney.Service.Interfaces;
using PocketMoney.Model;
using PocketMoney.Util.Serialization;

namespace PocketMoney.Service
{
    [Transactional]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class DutyService : BaseService, IDutyService
    {
        private readonly IRepository<Duty, DutyId, Guid> _dutyRepository;

        public DutyService(
            IRepository<Duty, DutyId, Guid> dutyRepository,
            IRepository<User, UserId, Guid> userRepository,
            IRepository<Family, FamilyId, Guid> familyRepository,
            ICurrentUserProvider currentUserProvider)
            : base(userRepository, familyRepository, currentUserProvider)
        {
            _dutyRepository = dutyRepository;
        }

        [Transaction(TransactionMode.Requires)]
        [MethodImpl(MethodImplOptions.Synchronized)]
        [OperationBehavior(TransactionScopeRequired = true)]
        public DutyTaskResult AddDutyTask(AddDutyTaskRequest model)
        {
            var currentUser = _currentUserProvider.GetCurrentUser().To();
            var assignedTo = model.AssignedTo.To();
            Duty duty = new Duty(
                assignedTo,
                currentUser,
                model.Name,
                Convert.ToBase64String(BinarySerializer.Serialaize(model.Form)),
                model.Reward,
                model.DutyDays.ToList());

            _dutyRepository.Add(duty);

            return new DutyTaskResult
            {
                AssignedTo = assignedTo,
                CreatedBy = currentUser,
                Days = model.DutyDays,
                Form = model.Form,
                Name = model.Name,
                Reward = model.Reward
            };
        }
    }
}
