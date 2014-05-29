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
    public interface ITaskService
    {
        [Process, OperationContract]
        GuidResult AddOneTimeTask(AddOneTimeTaskRequest model);

        [Process, OperationContract]
        GuidResult AddRepeatTask(AddRepeatTaskRequest model);

        [Process, OperationContract]
        GuidResult AddHomeworkTask(AddHomeworkTaskRequest model);

        [Process, OperationContract]
        GuidResult AddCleanTask(AddCleanTaskRequest model);

        [Process, OperationContract]
        GuidResult AddShoppingTask(AddShoppingTaskRequest model);

    }
}
