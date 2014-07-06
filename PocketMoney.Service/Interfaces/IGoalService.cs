using PocketMoney.Data;
using PocketMoney.Data.Wrappers;
using PocketMoney.Model.External.Requests;
using PocketMoney.Model.External.Results;
using PocketMoney.Service.Behaviors;
using System.ServiceModel;

namespace PocketMoney.Service.Interfaces
{
    [ServiceContract, ErrorPolicyBehavior]
    [ServiceKnownType(typeof(WrapperUser))]
    [ServiceKnownType(typeof(WrapperFamily))]
    [ServiceKnownType(typeof(WrapperFile))]
    public interface IGoalService : IBaseService
    {
        [Process, OperationContract]
        GuidResult AddGoal(AddGoalRequest model);

        [Process, OperationContract]
        Result UpdateGoal(UpdateGoalRequest model);

        [Process, OperationContract]
        GoalResult GetGoal(GuidRequest goalId);

        [Process, OperationContract]
        GoalListResult AllGoals(Request model);

        [Process, OperationContract]
        GuidResult PostNewAttainment(AddAttainmentRequest model);

        [Process, OperationContract]
        Result AppointReward(AppointRewardRequest model);

        AttainmentResult GetAttainment(GuidRequest attainmentId);
        
        [Process, OperationContract]
        AttainmentListResult AllAttainments(Request model);

    }
}
