using PocketMoney.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;

namespace PocketMoney.Model.External.Requests
{
    [DataContract]
    public class AddShoppingTaskRequest : BaseTaskRequest
    {
        [DataMember, Details]
        public string ShopName { get; set; }

        [DataMember, Details]
        [Display(Name = "Deadline Date")]
        public DateTime? DeadlineDate { get; set; }

        [DataMember, Details]
        [Display(Name = "Shopping List")]
        public ShopItem[] ShoppingList { get; set; }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.ShoppingList == null || this.ShoppingList.Length == 0)
                yield return new ValidationResult("The task should contain at least one item in shopping list.");

            if(this.ShoppingList.Any(x => string.IsNullOrWhiteSpace(x.ItemName)))
                yield return new ValidationResult("Item name is required field in shopping list.");

            foreach (var val in base.Validate(validationContext))
                yield return val;

        }
    }

    [DataContract]
    public class UpdateShoppingTaskRequest : AddShoppingTaskRequest, IIdentity
    {
        [DataMember, Details]
        public Guid Id { get; set; }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.Id == Guid.Empty)
                yield return new ValidationResult("Task identifier is required");

            foreach (var val in base.Validate(validationContext))
                yield return val;

        }
    }

}
