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

namespace PocketMoney.Service
{
    [Transactional]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class SettingService : BaseService, ISettingService
    {
        private readonly IRepository<Country, CountryId, int> _countryRepository;

        public SettingService(
            IRepository<User, UserId, Guid> userRepository,
            IRepository<Family, FamilyId, Guid> familyRepository,
            IRepository<Country, CountryId, int> countryRepository)
            : base(userRepository, familyRepository)
        {
            _countryRepository = countryRepository;
        }

        [Transaction(TransactionMode.Requires)]
        [MethodImpl(MethodImplOptions.Synchronized)]
        [OperationBehavior(TransactionScopeRequired = true)]
        public virtual Result AddCountry(AddCountryRequest model)
        {
            if (_countryRepository.Exists(x => x.Id == model.Code))
                throw new InvalidDataException("Страна с кодом '{0}' уже существует в системе.", model.Code);
            if (_countryRepository.Exists(x => x.Name == model.Name))
                throw new InvalidDataException("Страна с наименованием '{0}' уже существует в системе.", model.Name);

            var country = new Country(model.Code, model.Name);
            _countryRepository.Add(country);

            return new Result();
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        [OperationBehavior(TransactionScopeRequired = true)]
        public CountryListResult GetCountries(EmptyRequest model)
        {
            var list = _countryRepository.All().Select(x => new CountryInfo
            {
                Code = x.Id,
                Name = x.Name
            }).ToArray();
            return new CountryListResult { Data = list, TotalCount = list.Length };
        }
    }
}
