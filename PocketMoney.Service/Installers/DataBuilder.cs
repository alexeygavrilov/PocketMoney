using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Castle.Services.Transaction;
using PocketMoney.Data;
using PocketMoney.FileSystem;
using PocketMoney.Model.External.Requests;
using PocketMoney.Model.Internal;
using PocketMoney.Service.Interfaces;
using PocketMoney.Util.Bootstrapping;
using PocketMoney.Util.ExtensionMethods;
using PocketMoney.Model;
using Moq;
using PocketMoney.Data.Wrappers;

namespace PocketMoney.Service.Installers
{
    public class DataBuilder : BootstrappingTask
    {
        private IFamilyService _familyService;
        private readonly IFileService _fileService;
        private readonly ISettingService _settingService;
        private readonly IMessageService _messageService;
        private readonly IRepository<User, UserId, Guid> _userRepository;
        private readonly IRepository<Holiday, HolidayId, Guid> _holidayRepository;
        private readonly IRepository<Country, CountryId, int> _countryRepository;
        private readonly IRepository<Performer, PerformerId, Guid> _performerRepository;
        private readonly IRepository<Attainment, AttainmentId, Guid> _attainmentRepository;
        private readonly IRepository<Task, TaskId, Guid> _taskRepository;
        private readonly IRepository<Family, FamilyId, Guid> _familyRepository;
        private readonly IRepository<ActionLog, ActionLogId, Guid> _auditLogRepository;
        private readonly IRepository<Email, EmailId, Guid> _emailRepository;
        private readonly IRepository<PhoneNumber, PhoneNumberId, Guid> _phoneRepository;
        private readonly Mock<ICurrentUserProvider> _currentUserProvider;

        public DataBuilder(
            IFamilyService familyService,
            IFileService fileService,
            IMessageService messageService,
            ISettingService settingService,
            IRepository<User, UserId, Guid> userRepository,
            IRepository<Performer, PerformerId, Guid> performerRepository,
            IRepository<Attainment, AttainmentId, Guid> attainmentRepository,
            IRepository<Task, TaskId, Guid> taskRepository,
            IRepository<Family, FamilyId, Guid> familyRepository,
            IRepository<Country, CountryId, int> countryRepository,
            IRepository<Holiday, HolidayId, Guid> holidayRepository,
            IRepository<Email, EmailId, Guid> emailRepository,
            IRepository<PhoneNumber, PhoneNumberId, Guid> phoneRepository,
            IRepository<ActionLog, ActionLogId, Guid> auditLogRepository)
        {
            _familyService = familyService;
            _fileService = fileService;
            _settingService = settingService;
            _messageService = messageService;
            _userRepository = userRepository;
            _holidayRepository = holidayRepository;
            _taskRepository = taskRepository;
            _attainmentRepository = attainmentRepository;
            _performerRepository = performerRepository;
            _countryRepository = countryRepository;
            _familyRepository = familyRepository;
            _auditLogRepository = auditLogRepository;
            _emailRepository = emailRepository;
            _phoneRepository = phoneRepository;
            _currentUserProvider = new Mock<ICurrentUserProvider>();
        }


        [Transaction(TransactionMode.Requires)]
        public void AddSettings()
        {
            _settingService.AddCountry(new AddCountryRequest { Code = 7, Name = "Россия" });

            _settingService.AddCountry(new AddCountryRequest { Code = 380, Name = "Україна" });

            _settingService.AddCountry(new AddCountryRequest { Code = 375, Name = "Беларусь" });

            var usaResult = _settingService.AddCountry(new AddCountryRequest { Code = 1, Name = "USA" });

            var usa = _countryRepository.One(new CountryId(usaResult.Data));

            _holidayRepository.Add(new Holiday(usa, "Mother's Day", new DayOfOne(14, 5, 11)));
            _holidayRepository.Add(new Holiday(usa, "Memorial Day", new DayOfOne(14, 5, 26)));
            _holidayRepository.Add(new Holiday(usa, "Flag Day", new DayOfOne(14, 6, 14)));
            _holidayRepository.Add(new Holiday(usa, "Father's Day", new DayOfOne(14, 6, 15)));
            _holidayRepository.Add(new Holiday(usa, "Independence Day", new DayOfOne(14, 7, 4)));
            _holidayRepository.Add(new Holiday(usa, "Labor Day", new DayOfOne(14, 9, 1)));
            _holidayRepository.Add(new Holiday(usa, "Columbus Day", new DayOfOne(14, 10, 13)));
            _holidayRepository.Add(new Holiday(usa, "Veterans Day", new DayOfOne(14, 11, 11)));
            _holidayRepository.Add(new Holiday(usa, "Thanksgiving Day", new DayOfOne(14, 11, 27)));
            _holidayRepository.Add(new Holiday(usa, "Christmas Day", new DayOfOne(14, 12, 25)));
        }


        private void AddAttainment(IUser user, string text)
        {
            _currentUserProvider.Setup(x => x.GetCurrentUser()).Returns(user);

            IGoalService goalService = new GoalService(
                _performerRepository,
                _attainmentRepository,
                _taskRepository,
                _userRepository,
                _familyRepository,
                _auditLogRepository,
                _currentUserProvider.Object);

            var result = goalService.PostNewAttainment(new AddAttainmentRequest { Text = text });
            if (!result.Success) throw new ArgumentException(result.Message);

        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public override void Execute()
        {
            _fileService.RemoveAll();

            AddSettings();

            var result = _familyService.RegisterUser(new RegisterUserRequest
            {
                FamilyName = "Gavrilov",
                UserName = "Dad",
                Email = "alexey.gavrilov@gmail.com",
                Password = "include",
                ConfirmPassword = "include",
                CountryCode = 1
            });

            if (!result.Success) throw new ArgumentException(result.Message);

            var user = _userRepository.One(new UserId(result.Data.Id));
            if (user == null) throw new ArgumentNullException("user");

            var family = user.Family;

            result = _familyService.ConfirmUser(new ConfirmUserRequest
            {
                ConfirmCode = user.ConfirmCode
            });

            if (!result.Success) throw new ArgumentException(result.Message);

            _currentUserProvider.Setup(x => x.GetCurrentUser()).Returns(user);

            _familyService.CurrentUserProvider = _currentUserProvider.Object;

            var resultGuid = _familyService.AddUser(new AddUserRequest
            {
                UserName = "Mom",
                Password = "12345",
                ConfirmPassword = "12345",
                RoleId = Roles.Parent.Id,
                Email = "mom@localhost.com",
                SendNotification = false
            });

            if (!resultGuid.Success) throw new ArgumentException(resultGuid.Message);

            resultGuid = _familyService.AddUser(new AddUserRequest
            {
                UserName = "Konstantin",
                Email = "user1@localhost.com",
                Password = "12345",
                ConfirmPassword = "12345",
                RoleId = Roles.Children.Id,
                SendNotification = false
            });

            if (!resultGuid.Success) throw new ArgumentException(resultGuid.Message);

            AddAttainment(new WrapperUser("Konstantin", resultGuid.Data, family.Id), "I repaired Mom's computer");

            resultGuid = _familyService.AddUser(new AddUserRequest
            {
                UserName = "Alexander",
                Email = "user2@localhost.com",
                Password = "12345",
                ConfirmPassword = "12345",
                RoleId = Roles.Children.Id,
                SendNotification = false
            });

            if (!resultGuid.Success) throw new ArgumentException(resultGuid.Message);
            
            var alex = new WrapperUser("Alexander", resultGuid.Data, family.Id);
            AddAttainment(alex, "Helped old lady at shop");
            AddAttainment(alex, "I weed the flowers in garden for Mom");

            resultGuid = _familyService.AddUser(new AddUserRequest
            {
                UserName = "Xeniya",
                Email = "user3@localhost.com",
                Password = "12345",
                ConfirmPassword = "12345",
                RoleId = Roles.Children.Id,
                SendNotification = false
            });

            if (!resultGuid.Success) throw new ArgumentException(resultGuid.Message);


        }
    }
}
