using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;
using PocketMoney.Util.ExtensionMethods;

namespace PocketMoney.Configuration.Web
{
    public class FilterRegistryActionItem<TController> : FilterRegistryItem where TController : Controller
    {
        private readonly ReflectedActionDescriptor _reflectedActionDescriptor;

        public FilterRegistryActionItem(Expression<Action<TController>> action, IEnumerable<Func<IMvcFilter>> filters)
            : base(filters)
        {
            var methodCall = action.Body as MethodCallExpression;

            if ((methodCall == null) || (methodCall.Object == null) ||
                !typeof (ActionResult).IsAssignableFrom(methodCall.Method.ReturnType))
            {
                throw new InvalidOperationException("Expression must be a valid controller action.").LogError();
            }

            _reflectedActionDescriptor = new ReflectedActionDescriptor(methodCall.Method, methodCall.Method.Name,
                                                                      new ReflectedControllerDescriptor(
                                                                          methodCall.Object.Type));
        }

        public override bool IsMatching(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
        {
            if ((controllerContext != null) && (actionDescriptor != null))
            {
                var matchingDescriptor = actionDescriptor as ReflectedActionDescriptor;

                return (matchingDescriptor != null)
                           ? _reflectedActionDescriptor.MethodInfo == matchingDescriptor.MethodInfo
                           : IsSameAction(_reflectedActionDescriptor, actionDescriptor);
            }

            return false;
        }

        private static bool IsSameAction(ActionDescriptor descriptor1, ActionDescriptor descriptor2)
        {
            ParameterDescriptor[] parameters1 = descriptor1.GetParameters();
            ParameterDescriptor[] parameters2 = descriptor2.GetParameters();

            bool same =
                descriptor1.ControllerDescriptor.ControllerName.Equals(descriptor2.ControllerDescriptor.ControllerName,
                                                                       StringComparison.OrdinalIgnoreCase) &&
                descriptor1.ActionName.Equals(descriptor2.ActionName, StringComparison.OrdinalIgnoreCase) &&
                (parameters1.Length == parameters2.Length);

            if (same)
            {
                for (int i = parameters1.Length - 1; i >= 0; i--)
                {
                    if (parameters1[i].ParameterType == parameters2[i].ParameterType)
                    {
                        continue;
                    }

                    same = false;
                    break;
                }
            }

            return same;
        }
    }
}