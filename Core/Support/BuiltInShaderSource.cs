using System.Collections.Generic;
using Uriel.ShaderTypes;

namespace Uriel
{
    public static class BuiltInShaderSource
    {
        public static string[] SimpleShader = {
            "#version 150 compatibility\n",
            "void main() {\n",
            "   vec3 col = vec3(0,.7,.3);\n",
            "	gl_FragColor = vec4(col, 1.0);\n",
            "}\n"
        };

        public static string[] ShaderNoResolution = {
            "#version 150 compatibility\n",
            "uniform float u_time;\n",
            "uniform vec2 resolution;\n",
            "void main() {\n",
            "   vec3 col = 0.5 + 0.5*cos(u_time)*vec3(0,2,4);\n",
            "   gl_FragColor = vec4(col, 1.0);\n",
            "}\n"
        };

        public static string[] BaseShader = {
            "#version 150 compatibility\n",
            "uniform float u_time;\n",
            "uniform vec2 resolution;\n",
            "void main() {\n",
            "   vec2 normalized = gl_FragCoord.xy/resolution;\n",
            "   vec3 col = 0.5 + 0.5*cos(u_time+normalized.xyx+vec3(0,2,4));\n",
            "   gl_FragColor = vec4(col, 1.0);\n",
            "}\n"
        };

        public static string[] BaseShader2 = {
            "#version 150 compatibility\n",
            "uniform float u_time;\n",
            "uniform vec2 resolution;\n",
            "void main() {\n",
            "   vec2 normalized = gl_FragCoord.xy/resolution;\n",
            "   vec3 col = 0.5 + 0.5*cos(u_time+normalized.xyx+vec3(1,4,2));\n",
            "	gl_FragColor = vec4(col, 1.0);\n",
            "}\n"
        };

        public static string[] BaseShaderAlternate = {
            "#version 330 core\n",
            "layout(location = 0) out vec4 frag_color;\n",
            "uniform float u_time;\n",
            "uniform vec2 resolution;\n",
            "void main() {\n",
            "   vec2 normalized = gl_FragCoord.xy/resolution;\n",
            "   vec3 col = 0.5 + 0.5*cos(u_time+normalized.xyx+vec3(0,0,0));\n",
            "	frag_color = vec4(col, 1.0);\n",
            "}\n"
        };

        public static string[] BadShader = {
            "#version 150 compatibility\n",
            "uniform float u_time;\n",
            "uniform vec2 resolution;\n",
            "void main() {\n",
            "   vec2 normalized = gl_FragCoord.xy/resolution;\n",
            "   vec3 col = vec3(.5f, .1f, .1f)*sin(u_time);\n",
            "	gl_FragColor = vec4(col, 1.0);\n",
            "}\n"
        };

        public static string[] ColorTest =
        {
            "#version 150 compatibility\n",
            "uniform float u_time;\n",
            "uniform vec2 resolution;\n",
            "in vec3 vColor;\n",
            "void main() {\n",
            "   vec2 normalized = gl_FragCoord.xy/resolution;\n",
            "   vec3 col = vColor*sin(u_time);\n",
            "	gl_FragColor = vec4(col, 1.0);\n",
            "}\n"
        };

        public static string[] TextureTest = {
            "#version 330 core\n",
            "layout(location = 0) out vec4 frag_color;\n",
            "in vec2 fTexCoord;\n",
            "uniform sampler2D myTextureSampler;\n",
            "void main() {\n",
            "    vec3 col = vec3(.5f, .1f, .1f);\n",
            "    frag_color = vec4(col, 0);\n",
            "}\n"
        };


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
            "#version 150 compatibility\n",
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
