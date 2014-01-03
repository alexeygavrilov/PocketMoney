using PocketMoney.Util.ExtensionMethods;
using Microsoft.WindowsAzure.StorageClient;

namespace PocketMoney.FileSystem.Providers
{
    public static class BlobExtensions
    {
        public static bool Exists(this CloudBlob blob)
        {
            try
            {
                blob.FetchAttributes();
                return true;
            }
            catch (StorageClientException e)
            {
                e.LogError();
                if (e.ErrorCode == StorageErrorCode.ResourceNotFound)
                {
                    return false;
                }
                throw;
            }
        }
    }
}
