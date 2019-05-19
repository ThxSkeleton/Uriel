using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uriel.ShaderTypes;

namespace Uriel.Support
{
    public class ModifyLines
    {

        private static List<string> mainFunctionPostPend
        {
            get
            {
                return new List<string>(){
                    "void main()\n",
                    "{\n",
                    "    vec4 col;\n",
                    "    mainImage(col, gl_FragCoord.xy);\n",
                    "    gl_FragColor = col;\n",
                    "}\n"
                };
            }
        }

        const string UrielShaderDirective_LineStart = @"//URIEL";
        private const char UrielShaderDirective_Separator = ';';

        public static ShaderCreationArguments TranslateShader(List<string> inputShaderLines, string fullPath, DateTime createdDate)
        {
            string firstLine = inputShaderLines[0];

            StaticLogger.Logger.DebugFormat("Shader {0} has first line {1}", fullPath, firstLine);

            bool urielInterpretable = firstLine.StartsWith(UrielShaderDirective_LineStart);

            if (!urielInterpretable)
            {
                ShaderBlobType defaultType = ShaderBlobType.UrielStandard_FromFile;

                StaticLogger.Logger.DebugFormat("Shader is not Uriel Interpretable - proceeding with conversion to default ShaderBlobType {0}", defaultType);

                var alteredLines = ModifyLinesForShaderToy(inputShaderLines, defaultType.FragmentShaderVersion, defaultType.Uniforms);

                return new ShaderCreationArguments()
                {
                    Type = defaultType,
                    SimpleName = "FromFile",
                    FragmentShaderSource = alteredLines,
                    TexturePath = string.Empty,
                    Changed = createdDate,
                    FileName = fullPath
                };             
            }
            else
            {
                // TODO: the type should eventually come from the uriel directive
                var defaultnonInterpretableType = ShaderBlobType.Texture_FromFile;

                StaticLogger.Logger.DebugFormat("Shader is Uriel Interpretable - proceeding with conversion to default ShaderBlobType {0}", defaultnonInterpretableType);

                var alteredLines = ModifyLinesForShaderToy(inputShaderLines, defaultnonInterpretableType.FragmentShaderVersion, defaultnonInterpretableType.Uniforms);

                var urielFields = firstLine.Split(new char[] { UrielShaderDirective_Separator }).ToList();

                StaticLogger.Logger.DebugFormat("UrielFields has {0} members", urielFields.Count);

                // urielFields[0]; @"//URIEL"
                // urielFields[1]; Z:\TextureStore\UrielTexture.png
              
                var possibleTextureAbsolutePath = urielFields[1].Trim();

                StaticLogger.Logger.DebugFormat("Texture Path is {0}", possibleTextureAbsolutePath);

                bool useTexture = !string.IsNullOrWhiteSpace(possibleTextureAbsolutePath);

                return new ShaderCreationArguments()
                {
                    Type = ShaderBlobType.Texture_FromFile,
                    SimpleName = "FromFile",
                    FragmentShaderSource = alteredLines,
                    TexturePath = possibleTextureAbsolutePath,
                    Changed = createdDate,
                    FileName = fullPath
                };
            }
        }

        private static List<string> ModifyLinesForShaderToy(List<string> inputLines, ShaderVersion fragmentShaderVersion, List<FragmentShaderUniformType> uniforms )
        {
            List<string> toReturn = new List<string>() { fragmentShaderVersion.ToShaderVersionString() };
            toReturn.AddRange(uniforms.Select(x => UniformTypeToUniformDeclaration.Definitions[x]));
            toReturn.AddRange(inputLines);
            toReturn.AddRange(mainFunctionPostPend);
            return toReturn;
        }
    }
}
