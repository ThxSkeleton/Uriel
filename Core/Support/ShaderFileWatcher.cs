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
            LoadAndAddToShaderStore(eventArgs.FullPath);
        }

        public void LoadAll()
        {
            foreach (var directory in rootDirectories)
            {
                var allFiles = Directory.EnumerateFiles(directory, glslPattern);

                StaticLogger.Logger.DebugFormat("{0} has {1} files matching the pattern.", directory, allFiles.Count());

                foreach (var file in allFiles)
                {
                    LoadAndAddToShaderStore(file);
                }
            }
        }

        private static void LoadAndAddToShaderStore(string fileName)
        {
            try
            {
                ShaderCreationArguments fromFileArguments = ShaderLoader.LoadFromFile(fileName);
                ShaderStore.Shaders.Push(fromFileArguments);
            }
            catch (Exception ex)
            {
                StaticLogger.Logger.ErrorFormat("Could not read {0} : {1}", fileName, ex.ToString());
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
                // Don't log here - it will be invoked every single frame.
                return null;
            }
        }

    }

    public static class ShaderStore
    {
        public static ConcurrentStack<ShaderCreationArguments> Shaders = new ConcurrentStack<ShaderCreationArguments>();
    }

}
