using System.ServiceModel;
using PocketMoney.Model.External.Requests;
using PocketMoney.Model.External.Results;
using PocketMoney.Model.Network;
using PocketMoney.Service.Behaviors;

namespace PocketMoney.Service.Interfaces
{
    [ServiceContract, ErrorPolicyBehavior]
    public interface IConnector
    {
        [Process, OperationContract]
        NetworkAccountResult GetAccount(StringNetworkRequest identity);

        [Process, OperationContract]
        NetworkAccountList SearchAccount(StringNetworkRequest query);
    }
}
