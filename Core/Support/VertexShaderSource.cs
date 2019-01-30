using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uriel.ShaderTypes;

namespace Uriel.Support
{
    class VertexShaderSource
    {
        public static List<string> VertexSourceLookup(VertexFormat vertexFormat, ShaderVersion vertexShaderVersion)
        {
            var toReturn = new List<string>() { VersionTag(vertexShaderVersion) };

            if (vertexFormat == VertexFormat.Plain)
            {
                toReturn.AddRange(_VertexSourceGL_Simplest);
            }
            else if (vertexFormat == VertexFormat.WithColor)
            {
                toReturn.AddRange(_VertexSourceGL_Color);
            }
            else if (vertexFormat == VertexFormat.WithTexture)
            {
                toReturn.AddRange(_VertexSourceGL_Tex);
            }
            else if (vertexFormat == VertexFormat.WithColorAndTexture)
            {
                toReturn.AddRange(_VertexSourceGL_ColorAndTex);
            }

            return toReturn;
        }

        public static string VersionTag(ShaderVersion vertexShaderVersion)
        {
            if (vertexShaderVersion == ShaderVersion.Version150Compatability)
            {
                return "#version 150 compatibility\n";
            }
            else if (vertexShaderVersion == ShaderVersion.Version300Core)
            {
                return "#version 300 core\n";
            }
            else
            {
                throw new System.InvalidOperationException("Unsupported Shader Type.");
            }
        }


        private static string[] _VertexSourceGL_Simplest = {
            "in vec2 aPosition;\n",
            "void main() {\n",
            "	gl_Position = vec4(aPosition, 0.0, 1.0);\n",
            "}\n"
        };

        private static string[] _VertexSourceGL_Color = {
            "in vec2 aPosition;\n",
            "in vec3 aColor;\n",
            "out vec3 vColor;\n",
            "void main() {\n",
            "	gl_Position = vec4(aPosition, 0.0, 1.0);\n",
            "	vColor = aColor;\n",
            "}\n"
        };

        private static string[] _VertexSourceGL_Tex = {
            "in vec2 aPosition;\n",
            "in vec2 aTexCoord;\n",
            "out vec2 fTexCoord;\n",
            "void main() {\n",
            "	gl_Position = vec4(aPosition, 0.0, 1.0);\n",
            "   fTexCoord = aTexCoord;",
            "}\n"
        };

        private static string[] _VertexSourceGL_ColorAndTex = {
            "#version 150 compatibility\n",
            "in vec2 aPosition;\n",
            "in vec2 aTexCoord;\n",
            "in vec3 aColor;\n",
            "out vec2 fTexCoord;\n",
            "out vec3 vColor;\n",
            "void main() {\n",
            "	gl_Position = vec4(aPosition, 0.0, 1.0);\n",
            "	vColor = aColor;\n",
            "   fTexCoord = aTexCoord;",
            "}\n"
        };

    }
}
