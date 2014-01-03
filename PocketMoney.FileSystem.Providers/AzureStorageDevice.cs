using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.StorageClient;
using PocketMoney.Data;
using PocketMoney.Data.Wrappers;
using PocketMoney.Util.ExtensionMethods;
using PocketMoney.Util.IO;

namespace PocketMoney.FileSystem.Providers
{
    public class AzureStorageDevice : DeviceBase
    {
        private IDeviceInfo _info;

        public override IDeviceInfo StorageInfo
        {
            get { return _info; }
        }

        private CloudStorageAccount StorageAccount
        {
            get
            {
                CloudStorageAccount storageAccount;
                if (CloudStorageAccount.TryParse(Settings.Settings, out storageAccount))
                {
                    return storageAccount;
                }
                return null;
            }
        }

        public override void Initialize(IDeviceSettings settings)
        {
            base.Initialize(settings);

            if (string.IsNullOrWhiteSpace(settings.Settings))
                throw new ArgumentException("You must specify a device connection string in provider settings.",
                                            "settings").LogError();

            if (StorageAccount == null)
                throw new ArgumentException("Invalid device connection string in Settings string", "settings").LogError();

            long spaceTotal = 0;
            //CloudBlobClient blobClient = StorageAccount.CreateCloudBlobClient();
            //foreach (CloudBlobContainer blobContainer in blobClient.ListContainers())
            //{
            //    foreach (CloudBlob blob in blobContainer.ListBlobs())
            //    {
            //        blob.FetchAttributes();
            //        spaceTotal += blob.Properties.Length;
            //    }
            //}

            _info = new AzureStorageDeviceInfo(true, long.MaxValue, spaceTotal);
        }

        public override void Store(File file, IStreamProvider stream)
        {
            if (stream == null) throw new ArgumentNullException("stream").LogError();
            if (file == null) throw new ArgumentNullException("file").LogError();

            CloudBlobClient blobClient = StorageAccount.CreateCloudBlobClient();

            //Create a reference to a container, and create the container if it does not already exist.
            CloudBlobContainer blobContainer =
                blobClient.GetContainerReference(file.FileOwner.Id.ToBase32Url().ToLowerInvariant());
            blobContainer.CreateIfNotExist();

            string blobAddress = GetBlobAddress(file);
            CloudBlob blob = blobContainer.GetBlobReference(blobAddress);

            Func<Stream, Stream> transform = (s) => s;
            if (file.IsEncrypted)
                transform = (s) => { return ReadOnlyStream.Encrypt(s, file.FileOwner.Id.ToByteArray(), file.Id.ToByteArray()); };
            //Upload stream to the blob. If the blob does not yet exist, it will be created. 
            //If the blob does exist, its existing content will be overwritten.
            using (Stream fileStream = transform(stream.GetStream()))
            {
                blob.UploadFromStream(fileStream);
            }
            StoreMetadata(file, blob);
        }

        private static void StoreMetadata(File file, CloudBlob blob)
        {
            try
            {
                blob.Properties.ContentType = ((IFileFormatInfo)file).FileFormat.MimeTypes;
                blob.SetProperties();
                if (!string.IsNullOrWhiteSpace(file.FileNameWithExtension))
                    blob.Metadata["FileName"] = file.FileNameWithExtension;
                if (file.FileCreatedBy != null)
                    blob.Metadata["CreatedBy"] = file.FileCreatedBy.Id.ToBase32Url();
                blob.Metadata["Format"] = file.Format.ToString();
                blob.Metadata["Encrypted"] = file.IsEncrypted ? "true" : "false";
                if (!String.IsNullOrWhiteSpace(file.MetaInfo))
                    blob.Metadata["MetaInfo"] = file.MetaInfo;
                blob.SetMetadata();
            }
            catch (Exception ex)
            {
                ex.LogError();
            }
        }

        public override IStreamProvider Retrieve(File file)
        {
            if (file == null) throw new ArgumentNullException("file").LogError();
            CloudBlobClient blobClient = StorageAccount.CreateCloudBlobClient();

            string blobAddress = GetBlobAddress(file);
            CloudBlobContainer blobContainer =
                blobClient.GetContainerReference(file.FileOwner.Id.ToBase32Url().ToLowerInvariant());
            CloudBlob blob = blobContainer.GetBlobReference(blobAddress);
            if (!blob.Exists())
            {
                throw new FileNotFoundException(blob.ToString()).LogError();
            }

            byte[] data = blob.DownloadByteArray();

            // TODO: add the option to check file content MD5. For now I disabled it because it is being processed too slowly for much threads receiving. 
            //using (Stream f = new MemoryStream(data))
            //{
            //    long md5Lo, md5Hi;
            //    f.GetMD5(out md5Lo, out md5Hi);
            //    if (file.MD5HashHI != md5Hi ||
            //        file.MD5HashLO != md5Lo)
            //        throw new InvalidOperationException(String.Format(CultureInfo.CurrentCulture,
            //                                                          "Invalid MD5 hash for '{0}' file",
            //                                                          file.FileNameWithExtension)).LogError();
            //}
            Func<Stream, Stream> transform = (s) => s;
            if (file.IsEncrypted)
                transform = (s) => { return ReadOnlyStream.Decrypt(s, file.FileOwner.Id.ToByteArray(), file.Id.ToByteArray()); };
            return new MemoryStreamProvider(data, transform);
        }

        public override void DeleteFile(File file)
        {
            if (file == null) throw new ArgumentNullException("file").LogError();
            CloudBlobClient blobClient = StorageAccount.CreateCloudBlobClient();

            string blobAddress = GetBlobAddress(file);
            CloudBlobContainer blobContainer =
                blobClient.GetContainerReference(file.FileOwner.Id.ToBase32Url().ToLowerInvariant());
            CloudBlob blob = blobContainer.GetBlobReference(blobAddress);
            if (!blob.Exists())
                throw new FileNotFoundException(blob.ToString()).LogError();
            blob.Delete();
        }

        public override void DeleteCatalog(IFamily family)
        {
            if (family == null) throw new ArgumentNullException("family").LogError();
            CloudBlobClient blobClient = StorageAccount.CreateCloudBlobClient();
            CloudBlobContainer blobContainer =
                blobClient.ListContainers().SingleOrDefault(
                    cbc => cbc.Name == family.Id.ToBase32Url().ToLowerInvariant());
            if (blobContainer != null)
            {
                blobContainer.Delete();
            }
        }

        /// <summary>
        /// Gets a list of containers.
        /// </summary>
        /// <returns></returns>
        public override IEnumerable<IFamily> GetCatalogs()
        {
            CloudBlobClient blobClient = StorageAccount.CreateCloudBlobClient();
            IEnumerable<CloudBlobContainer> containerList = blobClient.ListContainers();
            var websiteIds = new List<IFamily>();

            if (containerList != null && containerList.Count() > 0)
            {
                websiteIds.AddRange(from c in containerList where c.Name != "vsdeploy" select new WrapperFamily(c.Name.FromBase32Url(), "Wrapper Family"));
            }
            return websiteIds;
        }

        private string GetBlobAddress(IFile file)
        {
            return file.Id.ToBase32Url();
        }

        public override IStreamProvider Thumbnail(File file, ThumbnailOptions option)
        {
            throw new NotImplementedException();
        }

        public override void ClearAllData()
        {
            CloudBlobClient blobClient = StorageAccount.CreateCloudBlobClient();
            foreach (var container in blobClient.ListContainers())
            {
                container.Delete();
            }
        }
    }
}