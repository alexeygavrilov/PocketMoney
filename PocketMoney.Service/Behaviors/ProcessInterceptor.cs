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

        private void SetResult(string message, Type type, ref Result result)
        {
            if (result == null)
            {
                if (type.IsAbstract)
                    result = Result.Empty;
                else
                    result = (Result)Activator.CreateInstance(type);
            }
            result.SetErrorMessage(message);
        }

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

                Result result = null; 
                try
                {
                    var request = invocation.GetArgumentValue(0) as Request;
                    if (request == null)
                    {
                        this.SetResult(REQUEST_ERROR_MESSAGE_NULL, invocation.Method.ReturnType, ref result);
                    }
                    else
                    {
                        foreach (var varRes in request.Validate(null))
                        {
                            this.SetResult(varRes.ErrorMessage, invocation.Method.ReturnType, ref result);
                        }
                    }

                    //invocation.SetArgumentValue(0, request);

                    if (result == null)
                    {
                        invocation.Proceed();
                    }
                }
                catch (UserLevelException ue)
                {
                    this.SetResult(ue.Message, invocation.Method.ReturnType, ref result);
                }
                catch (SystemLevelException se)
                {
                    this.SetResult(se.Message, invocation.Method.ReturnType, ref result);
                    se.LogDebug();
                }
                catch (Exception e)
                {
                    this.SetResult(e.Message, invocation.Method.ReturnType, ref result);
                    e.LogError();
                }
                if (result != null && !result.Success)
                {
                    invocation.ReturnValue = result;
                }
            }
        }
    }
}
