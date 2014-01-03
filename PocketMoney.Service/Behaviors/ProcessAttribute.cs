using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocketMoney.Service.Behaviors
{
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class ProcessAttribute : Attribute
    {
        public ProcessAttribute() { }
        //public ProcessAttribute(Type requestType, Type resultType)
        //{
        //    this.RequestType = requestType;
        //    this.ResultType = resultType;
        //}

        //public Type RequestType { get; set; }

        //public Type ResultType { get; set; }
    }
}
