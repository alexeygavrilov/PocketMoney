using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Castle.Services.Transaction;
using PocketMoney.Data;
using PocketMoney.FileSystem;
using PocketMoney.Model.External.Requests;
using PocketMoney.Model.Internal;
using PocketMoney.Service.Interfaces;
using PocketMoney.Util.Bootstrapping;
using PocketMoney.Util.ExtensionMethods;
using PocketMoney.Model;

namespace PocketMoney.Service.Installers
{
    public class DataBuilder : BootstrappingTask
    {
        private readonly IFamilyService _familyService;
        private readonly IFileService _fileService;
        private readonly ISettingService _settingService;
        private readonly IRepository<User, UserId, Guid> _userRepository;
        private readonly IRepository<Holiday, HolidayId, Guid> _holidayRepository;
        private readonly IRepository<Country, CountryId, int> _countryRepository;

        public DataBuilder(
            IFamilyService familyService,
            IFileService fileService,
            ISettingService settingService,
            IRepository<User, UserId, Guid> userRepository,
            IRepository<Country, CountryId, int> countryRepository,
            IRepository<Holiday, HolidayId, Guid> holidayRepository)
        {
            _familyService = familyService;
            _fileService = fileService;
            _settingService = settingService;
            _userRepository = userRepository;
            _holidayRepository = holidayRepository;
            _countryRepository = countryRepository;
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

            result = _familyService.AddUser(new AddUserRequest
            {
                Family = family,
                UserName = "Mom",
                Email = "mom@localhost.com",
                SendNotification = false
            });

            if (!result.Success) throw new ArgumentException(result.Message);

            result = _familyService.AddUser(new AddUserRequest
            {
                Family = family,
                UserName = "Konstantin",
                Email = "user1@localhost.com",
                SendNotification = false
            });

            if (!result.Success) throw new ArgumentException(result.Message);

            result = _familyService.AddUser(new AddUserRequest
            {
                Family = family,
                UserName = "Alexander",
                Email = "user2@localhost.com",
                SendNotification = false
            });

            if (!result.Success) throw new ArgumentException(result.Message);

            result = _familyService.AddUser(new AddUserRequest
            {
                Family = family,
                UserName = "Xeniya",
                Email = "user3@localhost.com",
                SendNotification = false
            });

            if (!result.Success) throw new ArgumentException(result.Message);
        }
    }
}
