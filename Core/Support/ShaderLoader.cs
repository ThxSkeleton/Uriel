using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Uriel.ShaderTypes;

namespace Uriel.Support
{
    public static class ShaderLoader
    {
        public static ShaderCreationArguments LoadFromFile(string fullPath)
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

                StaticLogger.Logger.DebugFormat("Successfully Loaded and translated File {0} into a ShaderArgument object", fullPath);

                return translatedShader;
            }
        }

    }
}
