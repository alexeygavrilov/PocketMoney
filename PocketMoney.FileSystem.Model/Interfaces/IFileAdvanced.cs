using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PocketMoney.Data;

namespace PocketMoney.FileSystem
{
    public interface IFileAdvanced
    {
        string DeviceName { get; }
        long MD5HashHI { get; }
        long MD5HashLO { get; }
        DateTime? DateLastAccessed { get; }
        long AccessCount { get; }
        bool Indexed { get; }
        DateTime? IndexedOn { get; }
        IFamily FileOwner { get; }
        IUser FileCreatedBy { get; }
    }
}
