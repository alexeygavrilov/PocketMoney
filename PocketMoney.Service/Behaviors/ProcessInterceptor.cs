using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.DynamicProxy;
using PocketMoney.Data;
using PocketMoney.Util;
using PocketMoney.Util.ExtensionMethods;

namespace PocketMoney.Service.Behaviors
{
    public class ProcessInterceptor : IInterceptor
    {
        const string REQUEST_ERROR_MESSAGE_NULL = "Request is null";

        public void Intercept(IInvocation invocation)
        {
            var methodInfo = invocation.Method;
            var attributes = methodInfo.GetCustomAttributes(typeof(ProcessAttribute), true);
            if (attributes.Length == 0)
            {
                invocation.Proceed();
            }
            else
            {
                var processAttribute = (ProcessAttribute)attributes[0];

                Result result = (Result)Activator.CreateInstance(invocation.Method.ReturnType);
                try
                {
                    var request = invocation.GetArgumentValue(0) as Request;
                    if (request == null)
                    {
                        result.SetErrorMessage(REQUEST_ERROR_MESSAGE_NULL);
                    }
                    else
                    {
                        foreach (var varRes in request.Validate(null))
                        {
                            result.SetErrorMessage(varRes.ErrorMessage);
                        }
                    }

                    //invocation.SetArgumentValue(0, request);

                    if (result.Success)
                    {
                        invocation.Proceed();
                    }
                }
                catch (UserLevelException ue)
                {
                    result.SetErrorMessage(ue.Message);
                }
                catch (SystemLevelException se)
                {
                    result.SetErrorMessage(se.Message);
                }
                catch (Exception e)
                {
                    result.SetErrorMessage(e.Message);
                    e.LogError();
                }
                if (!result.Success)
                {
                    invocation.ReturnValue = result;
                }
            }
        }
    }
}
