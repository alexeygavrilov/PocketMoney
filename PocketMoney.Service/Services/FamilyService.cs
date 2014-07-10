using Castle.Services.Transaction;
using PocketMoney.Data;
using PocketMoney.Model;
using PocketMoney.Model.External.Requests;
using PocketMoney.Model.External.Results;
using PocketMoney.Model.Internal;
using PocketMoney.Service.Interfaces;
using PocketMoney.Util;
using PocketMoney.Util.ExtensionMethods;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.ServiceModel;
using System.ServiceModel.Activation;

namespace PocketMoney.Service
{
    [Transactional]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class FamilyService : BaseService, IFamilyService
    {
        private readonly IRepository<Email, EmailId, Guid> _emailRepository;
        private readonly IRepository<PhoneNumber, PhoneNumberId, Guid> _phoneRepository;
        private readonly IRepository<Country, CountryId, int> _countryRepository;
        private readonly IRepository<ActionLog, ActionLogId, Guid> _actionLogRepository;
        private readonly IMessageService _messageService;

        public FamilyService(
            IMessageService messageService,
            IRepository<Email, EmailId, Guid> emailRepository,
            IRepository<PhoneNumber, PhoneNumberId, Guid> phoneRepository,
            IRepository<Country, CountryId, int> countryRepository,
            IRepository<User, UserId, Guid> userRepository,
            IRepository<Family, FamilyId, Guid> familyRepository,
            IRepository<ActionLog, ActionLogId, Guid> actionLogRepository,
            ICurrentUserProvider currentUserProvider)
            : base(userRepository, familyRepository, currentUserProvider)
        {
            _emailRepository = emailRepository;
            _phoneRepository = phoneRepository;
            _messageService = messageService;
            _countryRepository = countryRepository;
            _actionLogRepository = actionLogRepository;
        }

        [Transaction(TransactionMode.Requires)]
        [MethodImpl(MethodImplOptions.Synchronized)]
        [OperationBehavior(TransactionScopeRequired = true)]
        public virtual AuthResult RegisterUser(RegisterUserRequest model)
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

            AuthResult result = new AuthResult(user.From())
            {
                Password = model.Password,
                Login = email.Address
            };

            var messResult = _messageService.SendEmail(new EmailMessageRequest(
                user,
                email.Address,
                "Подтверждение пользователя",
                string.Format("Привет {0} \r\nВаш код подтверждения: {1}", user.FullName(), code)));

            if (!messResult.Success)
            {
                result.SetErrorMessage<AuthResult>(messResult.Message);
            }
            return result;
        }

        [Transaction(TransactionMode.Requires)]
        [MethodImpl(MethodImplOptions.Synchronized)]
        [OperationBehavior(TransactionScopeRequired = true)]
        public virtual AuthResult ConfirmUser(ConfirmUserRequest model)
        {
            var user = _userRepository.FindOne(x => x.ConfirmCode == model.ConfirmCode && !x.Active);
            if (user != null)
            {
                user.Active = true;
                _userRepository.Update(user);
                return new AuthResult(user.From());
            }
            else
                throw new InvalidDataException("Некорректный код подтверждения");

        }

        [Transaction(TransactionMode.Requires)]
        [MethodImpl(MethodImplOptions.Synchronized)]
        [OperationBehavior(TransactionScopeRequired = true)]
        public virtual GuidResult AddUser(AddUserRequest model)
        {
            var currentUser = _currentUserProvider.GetCurrentUser();

            if (_userRepository.Exists(x => x.Family.Id == currentUser.Family.Id && x.UserName == model.UserName))
                throw new InvalidDataException("User with '{0}' name already exist in family. Please enter another name.", model.UserName);

            if (!string.IsNullOrEmpty(model.Email) && _emailRepository.Exists(x => x.Address == model.Email))
                throw new InvalidDataException("Email '{0}' already exist in system.", model.Email);

            if (!string.IsNullOrEmpty(model.Phone) && _phoneRepository.Exists(x => x.Number == model.Phone))
                throw new InvalidDataException("Phone number '{0}' already exist in system.", model.Phone);

            var family = currentUser.Family.To();

            User user = new User(family, model.UserName);

            if (!string.IsNullOrEmpty(model.Email))
            {
                var email = new Email(model.Email, model.UserName);

                _emailRepository.Add(email);

                user.Email = email;
            }
            if (!string.IsNullOrEmpty(model.Phone))
            {
                var phone = new PhoneNumber(model.Phone);

                _phoneRepository.Add(phone);
            }

            user.AddRole(Roles.Define(model.RoleId));

            user.Active = true;

            user.SetPassword(model.ConfirmPassword);

            user.GenerateConfirmCode();

            user.GenerateTokenKey();

            _userRepository.Add(user);

            //if (model.SendNotification & !string.IsNullOrEmpty(model.Email))
            //{
            //    var messResult = _messageService.SendEmail(new EmailMessageRequest(
            //        user,
            //        model.Email,
            //        "Регистрация в приложение 'Карманные деньги'.",
            //        string.Format("Привет {0}, это {1}.\r\nЯ добавил тебя в приложение 'Карманные деньги', пожалуйста открой ссылку {2} и выполни установку.\r\nЛогин: {3}\r\nПароль: {4}\r\nСпасибо.",
            //            user.FullName(),
            //            _currentUserProvider.GetCurrentUser().FullName(),
            //            string.Format(Properties.Settings.Default.VK_AppUrl, Properties.Settings.Default.VK_ApiId),
            //            result.Login,
            //            result.Password
            //            )));

            //    if (!messResult.Success)
            //    {
            //        result.SetErrorMessage<AuthResult>(messResult.Message);
            //    }
            //}
            return new GuidResult(user.Id);
        }

        [Transaction(TransactionMode.Requires)]
        [MethodImpl(MethodImplOptions.Synchronized)]
        [OperationBehavior(TransactionScopeRequired = true)]
        public virtual Result UpdateUser(UpdateUserRequest model)
        {
            var currentUser = _currentUserProvider.GetCurrentUser();

            var user = _userRepository.One(new UserId(model.UserId));

            if (user == null)
            {
                throw new InvalidDataException("Cannot found user");
            }

            if (_userRepository.Exists(x => x.Family.Id == currentUser.Family.Id && x.Id != user.Id && x.UserName == model.UserName))
            {
                throw new InvalidDataException("User with '{0}' name already exist in family. Please enter another name.", model.UserName);
            }

            user.UserName = model.UserName;
            user.AdditionalName = model.AdditionalName;

            // update email
            Email email = user.Email;
            if (!string.IsNullOrEmpty(model.Email))
            {
                if (email != null)
                {
                    if (email.Address != model.Email)
                    {
                        email.Address = model.Email;
                        _emailRepository.Update(email);
                    }
                }
                else
                {
                    email = new Email(model.Email, user, true);
                    _emailRepository.Add(email);
                    user.Email = email;
                }
            }
            else if (email != null)
            {
                _emailRepository.Remove(email);
                user.Email = null;
            }

            // update phone
            PhoneNumber phone = user.Phone;
            if (!string.IsNullOrEmpty(model.Phone))
            {
                if (phone != null)
                {
                    if (phone.Number != model.Phone)
                    {
                        phone.Number = model.Phone;
                        _phoneRepository.Update(phone);
                    }
                }
                else
                {
                    phone = new PhoneNumber(model.Phone);
                    _phoneRepository.Add(phone);
                    user.Phone = phone;
                }
            }
            else if (phone != null)
            {
                _phoneRepository.Remove(phone);
                user.Phone = null;
            }

            _userRepository.Update(user);

            return Result.Successfully();
        }

        [Transaction(TransactionMode.Requires)]
        [MethodImpl(MethodImplOptions.Synchronized)]
        [OperationBehavior(TransactionScopeRequired = true)]
        public AuthResult Login(LoginRequest model)
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
                    return new AuthResult(user)
                    {
                        Login = model.UserName,
                        Password = model.Password,
                        Token = user.TokenKey
                    };
                }
            }
            throw new InvalidDataException("Некорректный пароль или нет прав доступа");
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        [OperationBehavior]
        public UserListResult GetUsers(Request model)
        {
            var currentUser = _currentUserProvider.GetCurrentUser();

            var users = _userRepository
                .FindAll(x => x.Family.Id == currentUser.Family.Id)
                .AsEnumerable()
                .Select(x => new UserView(x))
                .ToList();

            return new UserListResult(users.ToArray(), users.Count);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        [OperationBehavior]
        public UserFullResult GetUser(GuidRequest userId)
        {
            User user = _userRepository.One(new UserId(userId.Data));
            if (user != null)
            {
                return new UserFullResult(
                    new UserFullView(
                        user,
                        () => string.Join(Environment.NewLine, _actionLogRepository
                                .FindAll(x => x.ObjectId == user.Id)
                                .OrderByDescending(x => x.ChangeDate)
                                .AsEnumerable()
                                .Select(x => x.GetText())
                                .ToArray())));
            }
            else
            {
                throw new InvalidDataException("Cannot found user");
            }
        }


        [Transaction(TransactionMode.Requires)]
        [MethodImpl(MethodImplOptions.Synchronized)]
        [OperationBehavior(TransactionScopeRequired = true)]
        public Result Withdraw(WithdrawRequest model)
        {
            User user = _userRepository.One(new UserId(model.UserId));
            if (user != null)
            {
                user.Points.Withdraw(new Point(model.Points),
                    (p, v) => _actionLogRepository.Add(new ActionLog(p, -v)));
                
                _userRepository.Update(user);
                
                return Result.Successfully();
            }
            else
            {
                throw new InvalidDataException("Cannot found user");
            }
        }

    }
}
