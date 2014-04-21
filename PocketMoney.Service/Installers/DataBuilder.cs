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

namespace PocketMoney.Service.Installers
{
    public class DataBuilder : BootstrappingTask
    {
        private readonly IFamilyService _familyService;
        private readonly IFileService _fileService;
        private readonly ISettingService _settingService;
        private readonly IRepository<User, UserId, Guid> _userRepository;

        public DataBuilder(
            IFamilyService familyService,
            IFileService fileService,
            ISettingService settingService,
            IRepository<User, UserId, Guid> userRepository)
        {
            _familyService = familyService;
            _fileService = fileService;
            _settingService = settingService;
            _userRepository = userRepository;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public override void Execute()
        {
            _fileService.RemoveAll();

            _settingService.AddCountry(new AddCountryRequest { Code = 7, Name = "Россия" });

            _settingService.AddCountry(new AddCountryRequest { Code = 380, Name = "Україна" });

            _settingService.AddCountry(new AddCountryRequest { Code = 375, Name = "Беларусь" });

            _settingService.AddCountry(new AddCountryRequest { Code = 1, Name = "USA" });

            var result = _familyService.RegisterUser(new RegisterUserRequest
            {
                FamilyName = "Gavrilov",
                UserName = "Dad",
                Email = "alexey.gavrilov@gmail.com",
                Password = "include",
                ConfirmPassword = "include",
                CountryCode = 7
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
