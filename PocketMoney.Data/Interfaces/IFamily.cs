using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocketMoney.Data
{
    public interface IFamily
    {
        Guid Id { get; set; }
        string Name { get; }
        bool IsAnonymous { get; }
        IList<IUser> Members { get; }
        int CountryCode { get; }
        //IAddress Address { get; }
    }
}
