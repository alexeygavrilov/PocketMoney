using PocketMoney.Data;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace PocketMoney.Model.External.Requests
{
    [DataContract]
    public class AddOneTimeTaskRequest : BaseTaskRequest
    {
        [DataMember, Details]
        [Display(Name = "Deadline Date")]
        public DateTime? DeadlineDate { get; set; }
    }
}
