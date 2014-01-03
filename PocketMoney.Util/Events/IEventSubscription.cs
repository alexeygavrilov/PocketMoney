using System;

namespace PocketMoney.Util.Events
{
    public interface IEventSubscription
    {
        SubscriptionToken SubscriptionToken { get; set; }

        Action<object[]> GetExecutionStrategy();
    }
}