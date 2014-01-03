using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using PocketMoney.Util.ExtensionMethods;

namespace PocketMoney.Util.Events
{
    public abstract class EventBase : Disposable
    {
        private readonly IList<IEventSubscription> subscriptions = new List<IEventSubscription>();
        private readonly ReaderWriterLockSlim syncLock = new ReaderWriterLockSlim();

        protected ICollection<IEventSubscription> Subscriptions
        {
            [DebuggerStepThrough]
            get { return subscriptions; }
        }

        protected ReaderWriterLockSlim SyncLock
        {
            get { return syncLock; }
        }

        public virtual void Unsubscribe(SubscriptionToken token)
        {
            using (SyncLock.ReadAndMaybeWrite())
            {
                IEventSubscription subscription = subscriptions.FirstOrDefault(evt => evt.SubscriptionToken == token);

                if (subscription == null)
                {
                    return;
                }

                using (SyncLock.Write())
                {
                    subscriptions.Remove(subscription);
                }
            }
        }

        public virtual bool Contains(SubscriptionToken token)
        {
            using (SyncLock.Read())
            {
                return subscriptions.Any(evt => evt.SubscriptionToken == token);
            }
        }

        protected virtual void Publish(params object[] arguments)
        {
            IEnumerable<Action<object[]>> executionStrategies = PruneAndReturnStrategies();

            foreach (var executionStrategy in executionStrategies)
            {
                executionStrategy(arguments);
            }
        }

        protected virtual SubscriptionToken Subscribe(IEventSubscription eventSubscription)
        {
            eventSubscription.SubscriptionToken = new SubscriptionToken();

            using (SyncLock.Write())
            {
                subscriptions.Add(eventSubscription);
            }

            return eventSubscription.SubscriptionToken;
        }

        protected override void DisposeCore()
        {
            using (SyncLock.Write())
            {
                subscriptions.Clear();
            }

            SyncLock.Dispose();
        }

        private IEnumerable<Action<object[]>> PruneAndReturnStrategies()
        {
            IList<Action<object[]>> returnList = new List<Action<object[]>>();

            using (SyncLock.ReadAndMaybeWrite())
            {
                for (int i = subscriptions.Count - 1; i >= 0; i--)
                {
                    Action<object[]> subscriptionAction = subscriptions[i].GetExecutionStrategy();

                    if (subscriptionAction == null)
                    {
                        using (SyncLock.Write())
                        {
                            subscriptions.RemoveAt(i);
                        }
                    }
                    else
                    {
                        returnList.Add(subscriptionAction);
                    }
                }
            }

            return returnList;
        }
    }
}