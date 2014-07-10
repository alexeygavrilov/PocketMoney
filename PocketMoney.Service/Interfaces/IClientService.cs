using PocketMoney.Data;
using PocketMoney.Data.Wrappers;
using PocketMoney.Model.External.Requests;
using PocketMoney.Model.External.Results;
using PocketMoney.Service.Behaviors;
using System.ServiceModel;
using Clients = PocketMoney.Model.External.Results.Clients;

namespace PocketMoney.Service.Interfaces
{
    [ServiceContract, ErrorPolicyBehavior]
    [ServiceKnownType(typeof(WrapperUser))]
    [ServiceKnownType(typeof(WrapperFamily))]
    [ServiceKnownType(typeof(WrapperFile))]
    [ServiceKnownType(typeof(Role))]
    public interface IClientService
    {
        [Process, OperationContract]
        AuthResult Login(LoginRequest model);

        [Process, OperationContract]
        UserResult GetCurrentUser(Request model);

        [Process, OperationContract]
        Clients.TaskListResult GetTaskList(Request model);

        [Process, OperationContract]
        Clients.TaskListResult GetFloatingTaskList(Request model);
    }
}
