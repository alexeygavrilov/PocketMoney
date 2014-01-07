using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using PocketMoney.Data;

namespace PocketMoney.Model
{
    [DataContract]
    public class ScheduleForm : ObjectBase
    {
        [DataMember]
        [Display(Name = "Праздники")]
        [Details]
        public bool Holidays { get; set; }

        [DataMember]
        [Display(Name = "Год")]
        [Details]
        public int Year { get; set; }

        [DataMember]
        [Display(Name = "Понедельник")]
        [Details]
        public bool Monday { get; set; }

        [DataMember]
        [Display(Name = "Вторник")]
        [Details]
        public bool Tuesday { get; set; }

        [DataMember]
        [Display(Name = "Среда")]
        [Details]
        public bool Wednesday { get; set; }

        [DataMember]
        [Display(Name = "Четверг")]
        [Details]
        public bool Thursday { get; set; }

        [DataMember]
        [Display(Name = "Пятница")]
        [Details]
        public bool Friday { get; set; }

        [DataMember]
        [Display(Name = "Суббота")]
        [Details]
        public bool Saturday { get; set; }

        [DataMember]
        [Display(Name = "Воскресенье")]
        [Details]
        public bool Sunday { get; set; }
}
}
