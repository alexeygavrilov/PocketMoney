using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.ServiceLocation;
using PocketMoney.Data;
using PocketMoney.Data.Wrappers;
using PocketMoney.FileSystem;

namespace PocketMoney.Model
{
    public static class IFileExtensions
    {
        public static File To(this IFile file)
        {
            if (file == null) return null;
            if (file is File)
                return (File)file;
            else
            {
                var fileRepository = ServiceLocator.Current.GetInstance<IRepository<File, FileId, Guid>>();
                var store = fileRepository.One(new FileId(file.Id));
                if (store == null)
                    throw new InvalidDataException("File not found");
                return store;
            }
        }

        public static WrapperFile From(this IFile file)
        {
            if (file == null) return null;
            if (file is WrapperFile)
                return (WrapperFile)file;
            else
                return new WrapperFile(file);
        }

    }
}
