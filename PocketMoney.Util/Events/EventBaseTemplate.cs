using System;
using System.Linq;
using PocketMoney.Util.ExtensionMethods;

namespace PocketMoney.Util.Events
{
    public abstract class EventBase<TPayload> : EventBase
    {
        public virtual SubscriptionToken Subscribe(Action<TPayload> action)
        {
            return Subscribe(action, false);
        }

        public virtual SubscriptionToken Subscribe(Action<TPayload> action, bool keepSubscriberReferenceAlive)
        {
            return Subscribe(action, keepSubscriberReferenceAlive, obj => true);
        }

        public virtual SubscriptionToken Subscribe(Action<TPayload> action, bool keepSubscriberReferenceAlive,
                                                   Predicate<TPayload> filter)
        {
            IDelegateReference actionReference = new DelegateReference(action, keepSubscriberReferenceAlive);
            IDelegateReference filterReference = new DelegateReference(filter, keepSubscriberReferenceAlive);

            var subscription = new EventSubscription<TPayload>(actionReference, filterReference);

            return base.Subscribe(subscription);
        }

        public virtual void Publish(TPayload payload)
        {
            base.Publish(payload);
        }

        public virtual void Unsubscribe(Action<TPayload> subscriber)
        {
            using (SyncLock.ReadAndMaybeWrite())
            {
                IEventSubscription eventSubscription =
                    Subscriptions.Cast<EventSubscription<TPayload>>().FirstOrDefault(evt => evt.Action == subscriber);

                if (eventSubscription == null)
                {
                    return;
                }

                using (SyncLock.Write())
                {
                    Subscriptions.Remove(eventSubscription);
                }
            }
        }

        public virtual bool Contains(Action<TPayload> subscriber)
        {
            using (SyncLock.Read())
            {
                return
                    Subscriptions.Cast<EventSubscription<TPayload>>().FirstOrDefault(evt => evt.Action == subscriber) !=
                    null;
            }
        }
    }
}