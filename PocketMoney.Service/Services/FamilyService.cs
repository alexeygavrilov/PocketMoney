using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
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
using PocketMoney.Util;

namespace PocketMoney.Service
{
    [Transactional]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class FamilyService : BaseService, IFamilyService
    {
        private readonly IRepository<Email, EmailId, Guid> _emailRepository;
        private readonly IRepository<PhoneNumber, PhoneNumberId, Guid> _phoneRepository;
        private readonly IRepository<UserConnection, UserConnectionId, Guid> _connectionRepository;
        private readonly IRepository<Country, CountryId, int> _countryRepository;
        private readonly IMessageService _messageService;

        public FamilyService(
            IMessageService messageService,
            IRepository<Email, EmailId, Guid> emailRepository,
            IRepository<UserConnection, UserConnectionId, Guid> connectionRepository,
            IRepository<PhoneNumber, PhoneNumberId, Guid> phoneRepository,
            IRepository<Country, CountryId, int> countryRepository,
            IRepository<User, UserId, Guid> userRepository,
            IRepository<Family, FamilyId, Guid> familyRepository,
            ICurrentUserProvider currentUserProvider)
            : base(userRepository, familyRepository, currentUserProvider)
        {
            _emailRepository = emailRepository;
            _phoneRepository = phoneRepository;
            _messageService = messageService;
            _connectionRepository = connectionRepository;
            _countryRepository = countryRepository;
        }

        [Transaction(TransactionMode.Requires)]
        [MethodImpl(MethodImplOptions.Synchronized)]
        [OperationBehavior(TransactionScopeRequired = true)]
        public virtual UserResult RegisterUser(RegisterUserRequest model)
        {
            if (_familyRepository.Exists(x => x.Name == model.FamilyName))
                throw new InvalidDataException("Наименование семьи '{0}' уже существует в системе. Попробуйте другое имя", model.FamilyName);

            if (_emailRepository.Exists(x => x.Address == model.Email))
                throw new InvalidDataException("Эл. почта '{0}' уже существует в системе, попробуйте восстановить пароль в системе или обратитесь к администратору.", model.Email);

            var country = _countryRepository.One(new CountryId(model.CountryCode));
            if (country == null)
                throw new InvalidDataException("Страны с кодом '{0}' не существует в системе", model.CountryCode);

            var family = new Family(model.FamilyName, country);

            _familyRepository.Add(family);

            var email = new Email(model.Email, model.UserName);

            _emailRepository.Add(email);

            var user = new User(family, model.UserName, email);

            user.SetPassword(model.Password);
            var code = user.GenerateConfirmCode();

            user.AddRole(Roles.Parent);
            user.AddRole(Roles.FamilyAdmin);

            user.GenerateTokenKey();

            _userRepository.Add(user);

            var messResult = _messageService.SendEmail(new EmailMessageRequest(
                user,
                email.Address,
                "Подтверждение пользователя",
                string.Format("Привет {0} \r\nВаш код подтверждения: {1}", user.FullName(), code)));

            UserResult result = new UserResult();

            if (!messResult.Success)
            {
                result.SetErrorMessage<UserResult>(messResult.Message);
            }
            else
            {
                result.Data = user.From();
                result.Password = model.Password;
                result.Login = email.Address;
            }
            return result;
        }

        [Transaction(TransactionMode.Requires)]
        [MethodImpl(MethodImplOptions.Synchronized)]
        [OperationBehavior(TransactionScopeRequired = true)]
        public virtual UserResult ConfirmUser(ConfirmUserRequest model)
        {
            var user = _userRepository.FindOne(x => x.ConfirmCode == model.ConfirmCode && !x.Active);
            if (user != null)
            {
                user.Active = true;
                _userRepository.Update(user);
                return new UserResult { Data = user.From() };
            }
            else
                throw new InvalidDataException("Некорректный код подтверждения");

        }

        [Transaction(TransactionMode.Requires)]
        [MethodImpl(MethodImplOptions.Synchronized)]
        [OperationBehavior(TransactionScopeRequired = true)]
        public virtual UserResult AddUser(AddUserRequest model)
        {
            if (_userRepository.Exists(x => x.Family.Id == model.Family.Id && x.UserName == model.UserName))
                throw new InvalidDataException("Пользователь с именем '{0}' уже существует в вашей семье", model.UserName);

            if (!string.IsNullOrEmpty(model.Email) && _emailRepository.Exists(x => x.Address == model.Email))
                throw new InvalidDataException("Эл. почта '{0}' уже существует в системе.", model.Email);

            var family = model.Family.To();

            User user = new User(family, model.UserName);

            if (!string.IsNullOrEmpty(model.Email))
            {
                var email = new Email(model.Email, model.UserName);

                _emailRepository.Add(email);

                user.Email = email;
            }

            user.AddRole(Roles.Children);

            user.Active = true;

            var password = user.GeneratePassword();

            user.GenerateConfirmCode();

            _userRepository.Add(user);

            UserResult result = new UserResult
            {
                Data = user,
                Login = user.Email != null ? user.Email.Address : user.UserName,
                Password = password
            };

            if (model.SendNotification & !string.IsNullOrEmpty(model.Email))
            {
                var messResult = _messageService.SendEmail(new EmailMessageRequest(
                    user,
                    model.Email,
                    "Регистрация в приложение 'Карманные деньги'.",
                    string.Format("Привет {0}, это {1}.\r\nЯ добавил тебя в приложение 'Карманные деньги', пожалуйста открой ссылку {2} и выполни установку.\r\nЛогин: {3}\r\nПароль: {4}\r\nСпасибо.",
                        user.FullName(),
                        _currentUserProvider.GetCurrentUser().FullName(),
                        string.Format(Properties.Settings.Default.VK_AppUrl, Properties.Settings.Default.VK_ApiId),
                        result.Login,
                        result.Password
                        )));

                if (!messResult.Success)
                {
                    result.SetErrorMessage<UserResult>(messResult.Message);
                }
            }
            return result;
        }


        [Transaction(TransactionMode.Requires)]
        [MethodImpl(MethodImplOptions.Synchronized)]
        [OperationBehavior(TransactionScopeRequired = true)]
        public UserResult Login(LoginRequest model)
        {
            IList<User> users = new List<User>();
            if (model.UserName.Contains("@"))
            {
                var email = _emailRepository.FindOne(x => x.Address == model.UserName);
                if (email != null)
                {
                    var user = _userRepository.FindOne(x => x.Email.Id == email.Id);
                    if (user != null)
                        users.Add(user);
                }
            }
            if (users.Count == 0)
            {
                users.AddAll(_userRepository.FindAll(x => x.UserName == model.UserName).AsEnumerable());
            }
            if (users.Count == 0)
            {
                throw new InvalidDataException("Пользователь с логином '{0}' не найден", model.UserName);
            }
            foreach (var user in users)
            {
                if (user.IsValid(model.Password) && user.Active && user.IsInRole(Roles.Parent))
                {
                    user.LastLoginDate = Clock.UtcNow();
                    _userRepository.Update(user);
                    return new UserResult
                    {
                        Data = user,
                        Login = model.UserName,
                        Password = model.Password,
                        AuthToken = user.TokenKey
                    };
                }
            }
            throw new InvalidDataException("Некорректный пароль или нет прав доступа");
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        [OperationBehavior]
        public UserListResult GetUsers(FamilyRequest model)
        {
            var users = _userRepository
                .FindAll(x => x.Family.Id == model.Data.Id)
                .Select(x => new
                {
                    UserId = x.Id,
                    UserName = x.UserName,
                    Points = x.Points
                })
                .ToList();

            return new UserListResult
            {
                TotalCount = users.Count,
                List = users.SelectTo<UserInfo>().ToArray()
            };
        }
    }
}
