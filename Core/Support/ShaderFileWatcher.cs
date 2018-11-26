using System;
using System.Collections.Concurrent;
using System.IO;
using System.Security.Permissions;

namespace Uriel
{
    public class ShaderFileWatcher
    {
        private string rootDirectory;

        public ShaderFileWatcher(string rootDirectory)
        {
            this.rootDirectory = rootDirectory;
        }

        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        public void Run()
        {
            // Create a new FileSystemWatcher and set its properties.
            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Path = this.rootDirectory;

            watcher.NotifyFilter = NotifyFilters.LastWrite;

            // Old example Aerts
            // NotifyFilters.LastAccess | NotifyFilters.LastWrite
            //| NotifyFilters.FileName | NotifyFilters.DirectoryName;

            // Only watch glsl files.
            watcher.Filter = "*.glsl";

            // Add event handlers.
            watcher.Changed += new FileSystemEventHandler(OnChanged);
            watcher.Created += new FileSystemEventHandler(OnChanged);

            // Begin watching.
            watcher.EnableRaisingEvents = true;
        }

        // Define the event handlers.
        private static void OnChanged(object source, FileSystemEventArgs eventArgs)
        {
            try
            {
                using (StreamReader sr = new StreamReader(eventArgs.FullPath))
                {
                    // Read the stream to a string, and write the string to the console.
                    string glsl = sr.ReadToEnd();
                    ShaderStore.Shaders.Push(new ShaderInfo()
                    {
                        Source = glsl,
                        SourceFileName = eventArgs.Name,
                        Changed = DateTime.UtcNow
                    });
                }
            } 
            catch (Exception ex)
            {
                StaticLogger.Logger.ErrorFormat("Could not read {0} : {1}", eventArgs.FullPath, ex.ToString());
            }
        }
    }

    public static class ShaderStore
    {
        public static ConcurrentStack<ShaderInfo> Shaders = new ConcurrentStack<ShaderInfo>();
    }

    public class ShaderInfo
    {
        public string Source { get; set; }

        public string SourceFileName { get; set; }

        public DateTime Changed { get; set; }

        public string ConvenientName()
        {
            return String.Format("{0}-{1}", SourceFileName, Changed.ToString());
        }

    }

}
