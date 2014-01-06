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

namespace PocketMoney.Service
{
    [Transactional]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class SettingService : BaseService, ISettingService
    {
        private readonly IRepository<Country, CountryId, int> _countryRepository;
        private readonly IRepository<DutyType, DutyTypeId, int> _dutyTypeRepository;

        public SettingService(
            IRepository<Country, CountryId, int> countryRepository,
            IRepository<DutyType, DutyTypeId, int> dutyTypeRepository,
            IRepository<User, UserId, Guid> userRepository,
            IRepository<Family, FamilyId, Guid> familyRepository,
            ICurrentUserProvider currentUserProvider)
            : base(userRepository, familyRepository, currentUserProvider)
        {
            _countryRepository = countryRepository;
            _dutyTypeRepository = dutyTypeRepository;
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
            var list = _countryRepository
                .All()
                .AsEnumerable()
                .Select(x => new CountryInfo
                {
                    Code = x.Id,
                    Name = x.Name
                }).ToArray();
            return new CountryListResult { List = list, TotalCount = list.Length };
        }


        [Transaction(TransactionMode.Requires)]
        [MethodImpl(MethodImplOptions.Synchronized)]
        [OperationBehavior(TransactionScopeRequired = true)]
        public Result AddDutyType(AddDutyTypeRequest model)
        {
            if (_dutyTypeRepository.Exists(x => x.Name == model.Name && x.Country.Id == model.CountryCode))
                throw new InvalidDataException("Тип обязаности с именем '{0}' уже существует в системе.", model.Name);

            var country = _countryRepository.One(new CountryId(model.CountryCode));
            if (country == null)
                throw new InvalidDataException("Страны с кодом '{0}' не существует в системе.", model.CountryCode);

            var dutyType = new DutyType(model.Name, country);
            _dutyTypeRepository.Add(dutyType);

            return new Result();
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        [OperationBehavior(TransactionScopeRequired = true)]
        public DutyTypeListResult GetDutyTypes(EmptyRequest model)
        {
            var family = _currentUserProvider.GetCurrentUser().Family.To();
            var list = _dutyTypeRepository
                .FindAll(x => x.Country.Id == family.Country.Id)
                .AsEnumerable()
                .ToDictionary(k => k.Id, e => e.Name, EqualityComparer<int>.Default);

            return new DutyTypeListResult { TotalCount = list.Count, Dictionary = list };
        }
    }
}
