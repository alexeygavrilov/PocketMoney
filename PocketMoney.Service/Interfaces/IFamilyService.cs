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
    public interface IFamilyService : IBaseService
    {
        [Process, OperationContract]
        AuthResult RegisterUser(RegisterUserRequest model);

        [Process, OperationContract]
        AuthResult ConfirmUser(ConfirmUserRequest model);

        [Process, OperationContract]
        GuidResult AddUser(AddUserRequest model);

        [Process, OperationContract]
        Result UpdateUser(UpdateUserRequest model);

        [Process, OperationContract]
        AuthResult Login(LoginRequest model);

        [Process, OperationContract]
        UserListResult GetUsers(FamilyRequest model);

        [Process, OperationContract]
        UserResult GetUser(GuidRequest userId);

        [Process, OperationContract]
        Result Withdraw(WithdrawRequest model);
    }
}
