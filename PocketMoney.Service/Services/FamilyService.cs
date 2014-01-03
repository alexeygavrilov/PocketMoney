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
using PocketMoney.Service.Behaviors;
using PocketMoney.Service.Interfaces;
using PocketMoney.Util.ExtensionMethods;

namespace PocketMoney.Service
{
    [Transactional]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class FamilyService : BaseService, IFamilyService
    {
        private readonly IRepository<Email, EmailId, Guid> _emailRepository;
        private readonly IRepository<PhoneNumber, PhoneNumberId, Guid> _phoneRepository;
        private readonly IRepository<UserConnection, UserConnectionId, Guid> _connectionRepository;
        private readonly IMessageService _messageService;

        public FamilyService(
            IMessageService messageService,
            IRepository<User, UserId, Guid> userRepository,
            IRepository<Family, FamilyId, Guid> familyRepository,
            IRepository<Email, EmailId, Guid> emailRepository,
            IRepository<UserConnection, UserConnectionId, Guid> connectionRepository,
            IRepository<PhoneNumber, PhoneNumberId, Guid> phoneRepository)
            : base(userRepository, familyRepository)
        {
            _emailRepository = emailRepository;
            _phoneRepository = phoneRepository;
            _messageService = messageService;
            _connectionRepository = connectionRepository;
        }

        [Process, Transaction(TransactionMode.Requires)]
        [MethodImpl(MethodImplOptions.Synchronized)]
        [OperationBehavior(TransactionScopeRequired = true)]
        public virtual UserResult RegisterUser(RegisterUserRequest model)
        {
            //return this.Process<RegisterUserRequest, UserResult>(
            //    ref model,
            //    () => new UserResult(),
            //    (ref UserResult result) =>
            //    {
            if (_familyRepository.Exists(x => x.Name == model.FamilyName))
                throw new InvalidDataException("Наименование семьи '{0}' уже существует в системе. Попробуйте другое имя", model.FamilyName);

            if (_emailRepository.Exists(x => x.Address == model.Email))
                throw new InvalidDataException("Эл. почта '{0}' уже существует в системе, попробуйте восстановить войти в систему или обратитесь к администратору.", model.Email);

            var family = new Family(model.FamilyName);

            _familyRepository.Add(family);

            var email = new Email(model.Email, model.UserName);

            _emailRepository.Add(email);

            var user = new User(family, model.UserName, email);

            user.SetPassword(model.Password);

            user.AddRole(Roles.Parent);
            user.AddRole(Roles.FamilyAdmin);

            _userRepository.Add(user);

            var messResult = _messageService.SendEmail(new EmailMessageRequest(
                user,
                email.Address,
                "Подтверждение пользователя",
                string.Format("Привет {0} \r\nВаш код подтверждения: {1}", user.FullName(), user.Id.ToBase32Url())));

            UserResult result = new UserResult();

            if (!messResult.Success)
            {
                result.SetErrorMessage<UserResult>(messResult.Message);
            }
            else
            {
                result.Data = user.From();
            }
            return result;
            //                });
        }

        [Process, Transaction(TransactionMode.Requires)]
        [MethodImpl(MethodImplOptions.Synchronized)]
        [OperationBehavior(TransactionScopeRequired = true)]
        public virtual UserResult ConfirmUser(ConfirmUserRequest model)
        {
            //return this.Process<ConfirmUserRequest, UserResult>(
            //    ref model,
            //    () => new UserResult(),
            //    (ref UserResult result) =>
            //    {
            Guid userId = Guid.Empty;
            if (model.ConfirmCode.TryCreateFromBase32Url(out userId))
            {
                var member = _userRepository.One(new UserId(userId));
                if (member == null)
                {
                    throw new InvalidDataException("Некорректный код подтверждения");
                }
                member.Active = true;
                _userRepository.Update(member);
                return new UserResult { Data = member.From() };
            }
            else
                throw new InvalidDataException("Некорректный код подтверждения");

            //});
        }

        [Process, Transaction(TransactionMode.Requires)]
        [MethodImpl(MethodImplOptions.Synchronized)]
        [OperationBehavior(TransactionScopeRequired = true)]
        public virtual UserResult AddUser(AddUserRequest model)
        {
            //return this.Process<AddUserRequest, UserResult>(
            //    ref model,
            //    () => new UserResult(),
            //    (ref UserResult result) =>
            //    {
            if (_userRepository.Exists(x => x.Family.Id == model.Family.Id && x.FirstName == model.FirstName && x.LastName == null))
            {
                throw new InvalidDataException("Пользователь с именем '{0}' уже существует в вашей семье", model.FirstName);
            }

            var family = model.Family.To();

            var user = new User(family, model.FirstName);
            _userRepository.Add(user);
            foreach (var conn in model.Connections)
            {
                if (_connectionRepository.Exists(x => x.Identity == conn.Identity && x.ClientType == conn.ConnectionType))
                {
                    throw new InvalidDataException("Пользователь '{0}' уже существует в системе", model.FirstName);
                }

                var connection = new UserConnection(user, conn.ConnectionType, conn.Identity);
                _connectionRepository.Add(connection);
                user.Connections.Add(connection);
            }
            return new UserResult { Data = user };
            //                });
        }
    }
}
