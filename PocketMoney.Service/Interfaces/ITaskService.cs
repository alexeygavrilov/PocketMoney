using System.ServiceModel;
using PocketMoney.Model.External.Requests;
using PocketMoney.Model.External.Results;
using PocketMoney.Service.Behaviors;

namespace PocketMoney.Service.Interfaces
{
    public interface ITaskService
    {
        [Process, OperationContract]
        OneTimeTaskResult AddOneTimeTask(AddOneTimeTaskRequest model);

    }
}
