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

        [Process, OperationContract]
        Clients.GoalListResult GetGoalList(Request model);

        [Process, OperationContract]
        Clients.AttainmentListResult GetGoodDeedList(Request model);

        [Process, OperationContract]
        ShopItemListResult GetShoppingList(GuidRequest taskId);

        [Process, OperationContract]
        Result DoneTask(DoneTaskRequest model);

        [Process, OperationContract]
        Result GrabbTask(ProcessRequest model);

        [Process, OperationContract]
        Result AchieveGoal(ProcessRequest model);

        [Process, OperationContract]
        Result AddGoodDeed(AddAttainmentRequest model);

        [Process, OperationContract]
        Result CheckShopItem(CheckShopItemRequest model);


    }
}
