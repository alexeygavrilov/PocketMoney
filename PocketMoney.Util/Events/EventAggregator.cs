using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using PocketMoney.Util.ExtensionMethods;

namespace PocketMoney.Util.Events
{
    public class EventAggregator : Disposable, IEventAggregator
    {
        private readonly IList<EventBase> events = new List<EventBase>();
        private readonly ReaderWriterLockSlim syncLock = new ReaderWriterLockSlim();

        #region IEventAggregator Members

        public TEvent GetEvent<TEvent>() where TEvent : EventBase
        {
            TEvent eventInstance;

            using (syncLock.ReadAndMaybeWrite())
            {
                eventInstance = events.Where(evt => evt.GetType() == typeof (TEvent))
                    .Cast<TEvent>()
                    .SingleOrDefault();

                if (eventInstance == null)
                {
                    using (syncLock.Write())
                    {
                        eventInstance = Activator.CreateInstance<TEvent>();
                        events.Add(eventInstance);
                    }
                }
            }

            return eventInstance;
        }

        #endregion

        protected override void DisposeCore()
        {
            using (syncLock.Write())
            {
                for (int i = events.Count - 1; i >= 0; i--)
                {
                    events[i].Dispose();
                }

                events.Clear();
            }

            syncLock.Dispose();
        }
    }
}