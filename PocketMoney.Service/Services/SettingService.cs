using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
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
        private readonly IRepository<Holiday, HolidayId, Guid> _holidaysRepository;
        private readonly IRepository<Task, TaskId, Guid> _taskRepository;

        public SettingService(
            IRepository<Country, CountryId, int> countryRepository,
            IRepository<Holiday, HolidayId, Guid> holidaysRepository,
            IRepository<Task, TaskId, Guid> taskRepository,
            IRepository<User, UserId, Guid> userRepository,
            IRepository<Family, FamilyId, Guid> familyRepository,
            ICurrentUserProvider currentUserProvider)
            : base(userRepository, familyRepository, currentUserProvider)
        {
            _countryRepository = countryRepository;
            _holidaysRepository = holidaysRepository;
            _taskRepository = taskRepository;
        }

        [Transaction(TransactionMode.Requires)]
        [MethodImpl(MethodImplOptions.Synchronized)]
        [OperationBehavior(TransactionScopeRequired = true)]
        public virtual IntResult AddCountry(AddCountryRequest model)
        {
            if (_countryRepository.Exists(x => x.Id == model.Code))
                throw new InvalidDataException("Страна с кодом '{0}' уже существует в системе.", model.Code);
            if (_countryRepository.Exists(x => x.Name == model.Name))
                throw new InvalidDataException("Страна с наименованием '{0}' уже существует в системе.", model.Name);

            var country = new Country(model.Code, model.Name);
            _countryRepository.Add(country);

            return new IntResult(country.Id);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        [OperationBehavior(TransactionScopeRequired = true)]
        public CountryListResult GetCountries(Request model)
        {
            var list = _countryRepository
                .All()
                .AsEnumerable()
                .Select(x => new CountryInfo
                {
                    Code = x.Id,
                    Name = x.Name
                }).ToArray();

            return new CountryListResult(list, list.Length);
        }

        [Transaction(TransactionMode.Requires)]
        [MethodImpl(MethodImplOptions.Synchronized)]
        [OperationBehavior(TransactionScopeRequired = true)]
        public GuidResult AddHoliday(AddHolidayRequest model)
        {
            if (_holidaysRepository.Exists(x => x.Name == model.Name && x.Country.Id == model.CountryCode))
                throw new InvalidDataException("Праздник с именем '{0}' на дату '{1}' уже существует в системе.", model.Name);

            var country = _countryRepository.One(new CountryId(model.CountryCode));
            if (country == null)
                throw new InvalidDataException("Страны с кодом '{0}' не существует в системе.", model.CountryCode);

            var holiday = new Holiday(country, model.Name, model.Date);
            _holidaysRepository.Add(holiday);

            return new GuidResult(holiday.Id);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        [OperationBehavior(TransactionScopeRequired = true)]
        public HolidayListResult GetHolidays(Request model)
        {
            var family = _currentUserProvider.GetCurrentUser().Family.To();
            var list = _holidaysRepository
                .FindAll(x => x.Country.Id == family.Country.Id)
                .AsEnumerable()
                .ToDictionary(k => k.Date, e => e.Name, EqualityComparer<DayOfOne>.Default);

            return new HolidayListResult { TotalCount = list.Count, Dictionary = list };
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        [OperationBehavior(TransactionScopeRequired = true)]
        public StringListResult GetLessons(Request model)
        {
            var list = _taskRepository.AllOf<HomeworkTask>()
                .Select(x => x.Lesson)
                .Distinct()
                .ToArray();

            return new StringListResult(list, list.Length);
        }
    }
}
