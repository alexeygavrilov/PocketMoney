using System.IO;

namespace PocketMoney.Util.Serialization
{
    public interface ISerializer
    {
        void Serialize(Stream stream, object o);
        object Deserialize(Stream stream);
    }
}