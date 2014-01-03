using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PocketMoney.Data;

namespace PocketMoney.Model.Internal
{
    public static class Roles
    {
        public static Role Empty = new Role();
        public static Role Children = new Role(0x1, "Children");
        public static Role Parent = new Role(0x2, "Parent");
        public static Role FamilyAdmin = new Role(0x4, "FamilyAdmin");
        public static Role Admin = new Role(0x8, "Admin");
    }
}
