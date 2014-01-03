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
    public interface IFamilyService
    {
        [OperationContract]
        UserResult RegisterUser(RegisterUserRequest model);

        [OperationContract]
        UserResult ConfirmUser(ConfirmUserRequest model);

        [OperationContract]
        UserResult AddUser(AddUserRequest model);


    }
}
