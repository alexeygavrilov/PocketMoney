namespace PocketMoney.Util.Events
{
    public interface IEventAggregator
    {
        TEvent GetEvent<TEvent>() where TEvent : EventBase;
    }
}