using System;
using System.IO;
using System.Security;
using System.Threading;
using PocketMoney.Util.ExtensionMethods;

namespace PocketMoney.Util.IO
{
    public class FileSystem
    {
        /// <summary>
        /// Asynchronously deletes all empty directories on the way up the path.
        /// </summary>
        /// <param name="path">Path to follow</param>
        /// <remarks>Usually used when you delete last file in the directory and want to delete the directory, if its empty, including its parent directories.</remarks>
        public static void PruneDirectoriesAsync(string path)
        {
            ThreadPool.QueueUserWorkItem(state =>
                                             {
                                                 try
                                                 {
                                                     DoPruneDirectories(state);
                                                 }
                                                 catch (Exception ex)
                                                 {
                                                     ex.LogError();
                                                     //TODO: Log error here
                                                 }
                                             }, Directory.GetParent(path));
        }

        public static void PruneDirectories(string path)
        {
            DoPruneDirectories(new DirectoryInfo(path));
        }


        private static void DoPruneDirectories(object directory)
        {
            try
            {
                var targetDirectory = (DirectoryInfo) directory;
                if (Directory.Exists(targetDirectory.FullName))
                {
                    DirectoryInfo parentDirectoryInfo = Directory.GetParent(targetDirectory.FullName);
                    // Check attributes and reset if necessary
                    if ((targetDirectory.Attributes & FileAttributes.ReadOnly) > 0)
                        targetDirectory.Attributes = (targetDirectory.Attributes -= FileAttributes.ReadOnly);
                    targetDirectory.Delete();
                    DoPruneDirectories(parentDirectoryInfo);
                }
            }
            catch (UnauthorizedAccessException e)
            {
                e.LogDebug();
            }
            catch (DirectoryNotFoundException e)
            {
                e.LogDebug();
            }
            catch (IOException e)
            {
                e.LogDebug();
            }
            catch (SecurityException e)
            {
                e.LogDebug();
            }
        }
    }
}