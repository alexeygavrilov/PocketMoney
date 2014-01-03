using System;
using System.Diagnostics;
using System.Reflection;

namespace PocketMoney.Util.Events
{
    public class DelegateReference : IDelegateReference
    {
        private readonly Delegate @delegate;
        private readonly Type delegateType;
        private readonly MethodInfo method;
        private readonly WeakReference weakReference;

        public DelegateReference(Delegate @delegate, bool keepReferenceAlive)
        {
            if (keepReferenceAlive)
            {
                this.@delegate = @delegate;
            }
            else
            {
                weakReference = new WeakReference(@delegate.Target);
                method = @delegate.Method;
                delegateType = @delegate.GetType();
            }
        }

        #region IDelegateReference Members

        public Delegate Target
        {
            [DebuggerStepThrough]
            get { return @delegate ?? TryGetDelegate(); }
        }

        #endregion

        private Delegate TryGetDelegate()
        {
            if (method.IsStatic)
            {
                return Delegate.CreateDelegate(delegateType, null, method);
            }

            object target = weakReference.Target;

            return (target != null) ? Delegate.CreateDelegate(delegateType, target, method) : null;
        }
    }
}