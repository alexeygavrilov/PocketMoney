using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PocketMoney.Data
{
    public interface IActivable
    {
        bool Active { get; set; }
    }
}
