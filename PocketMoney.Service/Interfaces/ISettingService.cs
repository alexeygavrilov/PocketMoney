using System.ServiceModel;
using PocketMoney.Data;
using PocketMoney.Data.Wrappers;
using PocketMoney.Model.External.Requests;
using PocketMoney.Model.External.Results;
using PocketMoney.Service.Behaviors;

namespace PocketMoney.Service.Interfaces
{
    [ServiceContract, ErrorPolicyBehavior]
    [ServiceKnownType(typeof(WrapperUser))]
    [ServiceKnownType(typeof(WrapperFamily))]
    [ServiceKnownType(typeof(WrapperFile))]
    [ServiceKnownType(typeof(Role))]
    public interface ISettingService : IBaseService
    {
        [Process, OperationContract]
        IntResult AddCountry(AddCountryRequest model);

        [Process, OperationContract]
        CountryListResult GetCountries(Request model);

        [Process, OperationContract]
        GuidResult AddHoliday(AddHolidayRequest model);

        [Process, OperationContract]
        HolidayListResult GetHolidays(Request model);

        [Process, OperationContract]
        StringListResult GetLessons(Request model);
    }
}
