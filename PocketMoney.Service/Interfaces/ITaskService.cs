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
    public interface ITaskService : IBaseService
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

        [Process, OperationContract]
        Result UpdateOneTimeTask(UpdateOneTimeTaskRequest model);

        [Process, OperationContract]
        Result UpdateRepeatTask(UpdateRepeatTaskRequest model);

        [Process, OperationContract]
        Result UpdateHomeworkTask(UpdateHomeworkTaskRequest model);

        [Process, OperationContract]
        Result UpdateCleanTask(UpdateCleanTaskRequest model);

        [Process, OperationContract]
        Result UpdateShoppingTask(UpdateShoppingTaskRequest model);

        [Process, OperationContract]
        OneTimeTaskResult GetOneTimeTask(GuidRequest taskId);

        [Process, OperationContract]
        RepeatTaskResult GetRepeatTask(GuidRequest taskId);

        [Process, OperationContract]
        HomeworkTaskResult GetHomeworkTask(GuidRequest taskId);

        [Process, OperationContract]
        CleanTaskResult GetCleanTask(GuidRequest taskId);

        [Process, OperationContract]
        ShoppingTaskResult GetShoppingTask(GuidRequest taskId);

        [Process, OperationContract]
        TaskListResult AllTasks(Request model);

        [Process, OperationContract]
        TaskListResult MyTasks(Request model);
    }
}
