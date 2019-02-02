using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using Uriel.ShaderTypes;
using Uriel.Support;

namespace Uriel
{
    public class ShaderFileWatcher
    {
        private const string glslPattern = "*.glsl";

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

            // Only watch glsl files.
            watcher.Filter = glslPattern;

            // Add event handlers.
            watcher.Changed += new FileSystemEventHandler(OnChanged);
            watcher.Created += new FileSystemEventHandler(OnChanged);

            // Begin watching.
            watcher.EnableRaisingEvents = true;
        }

        // Define the event handlers.
        private static void OnChanged(object source, FileSystemEventArgs eventArgs)
        {
            LoadFile(eventArgs.FullPath);
        }

        public void LoadAll()
        {
            var allFiles = Directory.EnumerateFiles(rootDirectory, glslPattern);
            
            foreach(var file in allFiles)
            {
                LoadFile(file);
            }
        }

        private static void LoadFile(string fullPath)
        {
            try
            {
                using (StreamReader sr = new StreamReader(fullPath))
                {
                    // Read the stream to a string, and write the string to the console.
                    string fileContent = sr.ReadToEnd();
                    ShaderStore.Shaders.Push(new ShaderInfo()
                    {
                        Source = fileContent,
                        SourceFileName = fullPath,
                        Changed = DateTime.UtcNow
                    });
                }
            }
            catch (Exception ex)
            {
                StaticLogger.Logger.ErrorFormat("Could not read {0} : {1}", fullPath, ex.ToString());
            }

        }

        public ShaderCreationArguments GetNew()
        {
            if (!ShaderStore.Shaders.IsEmpty)
            {
                StaticLogger.Logger.Info("New Shader Detected.");

                ShaderInfo newShader;
                // don't bother checking success.
                ShaderStore.Shaders.TryPop(out newShader);

                StaticLogger.Logger.Info(newShader);

                IEnumerable<string> shaderStringBase = newShader.Source.Trim().Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
                IEnumerable<string> shaderStringPostPendNewlines = shaderStringBase.Select(x => x + "\n");

                var shaderStringActual = ShaderToyConverter.TranslateShader(shaderStringPostPendNewlines.ToList());

                return new ShaderCreationArguments()
                {
                    Type = ShaderBlobType.Standard,
                    DisplayName = newShader.ConvenientName(),
                    FragmentShaderSource = shaderStringActual,
                };
            }
            else
            {
                return null;
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
