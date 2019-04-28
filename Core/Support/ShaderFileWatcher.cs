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

        private List<string> rootDirectories;

        public ShaderFileWatcher(List<string> directories)
        {
            this.rootDirectories = directories;
        }

        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        public void Run()
        {
            foreach (var directory in rootDirectories)
            {
                // Create a new FileSystemWatcher and set its properties.
                FileSystemWatcher watcher = new FileSystemWatcher();
                watcher.Path = directory;

                watcher.NotifyFilter = NotifyFilters.LastWrite;

                // Only watch glsl files.
                watcher.Filter = glslPattern;

                // Add event handlers.
                watcher.Changed += new FileSystemEventHandler(OnChanged);
                watcher.Created += new FileSystemEventHandler(OnChanged);

                // Begin watching.
                watcher.EnableRaisingEvents = true;
            }
        }

        // Define the event handlers.
        private static void OnChanged(object source, FileSystemEventArgs eventArgs)
        {
            LoadFile(eventArgs.FullPath);
        }

        public void LoadAll()
        {
            foreach (var directory in rootDirectories)
            {
                var allFiles = Directory.EnumerateFiles(directory, glslPattern);

                StaticLogger.Logger.DebugFormat("{0} has {1} files matching the pattern.", directory, allFiles.Count());

                foreach (var file in allFiles)
                {
                    LoadFile(file);
                }
            }
        }

        private static void LoadFile(string fullPath)
        {
            try
            {
                StaticLogger.Logger.DebugFormat("Opening Filestream for {0}", fullPath);

                FileStream fileStream = new FileStream(fullPath, FileMode.Open, FileAccess.Read);

                StaticLogger.Logger.DebugFormat("Successfully opened Filestream for {0}", fullPath);

                using (StreamReader sr = new StreamReader(fileStream))
                {
                    // Read the stream to a string, and write the string to the console.
                    string fileContent = sr.ReadToEnd();

                    List<string> shaderLines = fileContent.Trim().Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None).Select(x => x + "\n").ToList();

                    StaticLogger.Logger.DebugFormat("File {0} has {1} lines.", fullPath, shaderLines.Count);

                    var translatedShader = ModifyLines.TranslateShader(shaderLines, fullPath, DateTime.UtcNow);

                    ShaderStore.Shaders.Push(translatedShader);
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

                ShaderCreationArguments newShader;
                // don't bother checking success.
                ShaderStore.Shaders.TryPop(out newShader);

                StaticLogger.Logger.Info(newShader);

                return newShader;
            }
            else
            {
                return null;
            }
        }

    }

    public static class ShaderStore
    {
        public static ConcurrentStack<ShaderCreationArguments> Shaders = new ConcurrentStack<ShaderCreationArguments>();
    }

}
