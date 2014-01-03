using System;
using System.Diagnostics;
using PocketMoney.Util.ExtensionMethods;

namespace PocketMoney.Util.Events
{
    public class EventSubscription<TPayload> : IEventSubscription
    {
        private readonly IDelegateReference actionReference;
        private readonly IDelegateReference filterReference;

        public EventSubscription(IDelegateReference actionReference, IDelegateReference filterReference)
        {
            if (!(actionReference.Target is Action<TPayload>))
            {
                throw new ArgumentException("Incorrect action reference type.", "actionReference").LogError();
            }

            if (!(filterReference.Target is Predicate<TPayload>))
            {
                throw new ArgumentException("Incorrect filter reference type.", "filterReference").LogError();
            }

            this.actionReference = actionReference;
            this.filterReference = filterReference;
        }

        public Action<TPayload> Action
        {
            [DebuggerStepThrough]
            get { return (Action<TPayload>) actionReference.Target; }
        }

        public Predicate<TPayload> Filter
        {
            [DebuggerStepThrough]
            get { return (Predicate<TPayload>) filterReference.Target; }
        }

        #region IEventSubscription Members

        public SubscriptionToken SubscriptionToken { get; set; }

        public virtual Action<object[]> GetExecutionStrategy()
        {
            Action<TPayload> action = Action;
            Predicate<TPayload> filter = Filter;

            if (action != null && filter != null)
            {
                return arguments =>
                           {
                               TPayload argument = default(TPayload);

                               if (arguments != null && arguments.Length > 0 && arguments[0] != null)
                               {
                                   argument = (TPayload) arguments[0];
                               }

                               if (filter(argument))
                               {
                                   InvokeAction(action, argument);
                               }
                           };
            }

            return null;
        }

        #endregion

        [DebuggerStepThrough]
        protected virtual void InvokeAction(Action<TPayload> action, TPayload argument)
        {
            action(argument);
        }
    }
}