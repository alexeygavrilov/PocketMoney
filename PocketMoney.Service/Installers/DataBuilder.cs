using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Castle.Services.Transaction;
using PocketMoney.FileSystem;
using PocketMoney.Model.External.Requests;
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

        public DataBuilder(
            IFamilyService familyService,
            IFileService fileService,
            ISettingService settingService)
        {
            _familyService = familyService;
            _fileService = fileService;
            _settingService = settingService;
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
                FamilyName = "Гавриловы",
                UserName = "Папа",
                Email = "alexey.gavrilov@gmail.com",
                Password = "include",
                ConfirmPassword = "include",
                CountryCode = 7
            });

            if (!result.Success) throw new ArgumentException(result.Message);

            result = _familyService.ConfirmUser(new ConfirmUserRequest
            {
                ConfirmCode = result.Data.Id.ToBase32Url()
            });

            if (!result.Success) throw new ArgumentException(result.Message);

            result = _familyService.AddUser(new AddUserRequest
            {
                Family = result.Data.Family,
                UserName = "Костя",
                Connections = new ConnectionRequest[1] 
                { 
                    new ConnectionRequest 
                    { 
                        ConnectionType = Model.ClientType.VK, 
                        Identity = "144225561" 
                    }
                }
            });

            if (!result.Success) throw new ArgumentException(result.Message);
        }
    }
}
