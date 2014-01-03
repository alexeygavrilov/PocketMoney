using System;
using System.Collections.Generic;

using System.ServiceModel.Channels;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Collections.ObjectModel;
using System.ServiceModel.Configuration;
using System.Runtime.Serialization;
using System.ServiceModel.Dispatcher;
using PocketMoney.Util.ExtensionMethods;

namespace PocketMoney.Service.Behaviors
{
    [AttributeUsage(AttributeTargets.Interface)]
    public class ErrorPolicyBehaviorAttribute : Attribute, IContractBehavior, IErrorHandler
    {
        public void AddBindingParameters(ContractDescription contractDescription, ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {

        }

        public void ApplyClientBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {

        }

        public void ApplyDispatchBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, DispatchRuntime dispatchRuntime)
        {
            dispatchRuntime.ChannelDispatcher.ErrorHandlers.Add(this);
        }

        public void Validate(ContractDescription contractDescription, ServiceEndpoint endpoint)
        {

        }

        public bool HandleError(Exception error)
        {
            error.LogFatal();
            return !(error is FaultException);
        }

        public void ProvideFault(Exception error, MessageVersion version, ref Message fault)
        {
            if (error is FaultException)
            {
                return;	//WCF will handle this
            }

            var messageFault = MessageFault.CreateFault(new FaultCode("Service"), new FaultReason(error.Message), error, new NetDataContractSerializer());
            fault = Message.CreateMessage(version, messageFault, null);

            //fault = Message.CreateMessage(
            //   version,
            //   new FaultException<string>(error.Message, new FaultReason(error.Message)).CreateMessageFault(),
            //   "http://gaziboo.eleph.org/service/ErrorExceptionHandler");
        }
    }
}
