using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace PocketMoney.FileSystem
{
    public interface IStreamProvider
    {
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        Stream GetStream();

        byte[] Content { get; set; }
    }


}