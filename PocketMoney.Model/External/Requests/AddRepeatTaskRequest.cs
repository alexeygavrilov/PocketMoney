using PocketMoney.Data;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;


namespace PocketMoney.Model.External.Requests
{
    [DataContract]
    public class AddRepeatTaskRequest : BaseTaskRequest
    {
        [DataMember, Details]
        public HomeworkForm Form { get; set; }

    }
}
