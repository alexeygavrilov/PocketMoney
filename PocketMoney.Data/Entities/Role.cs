using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PocketMoney.Data
{
    [DataContract]
    public class Role : ObjectBase, IRole, IEquatable<Role>
    {
        public Role()
        {
            this.Id = 0x0;
            this.Name = string.Empty;
        }

        public Role(byte id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        [Details]
        [DataMember]
        public string Name { get; set; }

        [Details]
        [DataMember(IsRequired = true)]
        public byte Id { get; set; }

        public bool Equals(Role other)
        {
            return this.Id == other.Id;
        }
    }
}
