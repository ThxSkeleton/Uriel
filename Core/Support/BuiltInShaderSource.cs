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
            "uniform float iTime;\n",
            "void main() {\n",
            "   vec3 col = 0.5 + 0.5*cos(iTime)*vec3(0,2,4);\n",
            "   gl_FragColor = vec4(col, 1.0);\n",
            "}\n"
        };

        public static string[] BaseShader = {
            "#version 150 compatibility\n",
            "uniform float iTime;\n",
            "uniform vec2 iResolution;\n",
            "void main() {\n",
            "   vec2 normalized = gl_FragCoord.xy/iResolution;\n",
            "   vec3 col = 0.5 + 0.5*cos(iTime+normalized.xyx+vec3(0,2,4));\n",
            "   gl_FragColor = vec4(col, 1.0);\n",
            "}\n"
        };

        public static string[] BaseShader2 = {
            "#version 150 compatibility\n",
            "uniform float iTime;\n",
            "uniform vec2 iResolution;\n",
            "void main() {\n",
            "   vec2 normalized = gl_FragCoord.xy/iResolution;\n",
            "   vec3 col = 0.5 + 0.5*cos(iTime+normalized.xyx+vec3(1,4,2));\n",
            "	gl_FragColor = vec4(col, 1.0);\n",
            "}\n"
        };

        public static string[] BaseShaderAlternate = {
            "#version 330 core\n",
            "layout(location = 0) out vec4 frag_color;\n",
            "uniform float iTime;\n",
            "uniform vec2 iResolution;\n",
            "void main() {\n",
            "   vec2 normalized = gl_FragCoord.xy/iResolution;\n",
            "   vec3 col = 0.5 + 0.5*cos(iTime+normalized.xyx+vec3(0,0,0));\n",
            "	frag_color = vec4(col, 1.0);\n",
            "}\n"
        };

        public static string[] BadShader = {
            "#version 150 compatibility\n",
            "uniform float iTime;\n",
            "void main() {\n",
            "   vec3 col = vec3(.5f, .1f, .1f)*sin(iTime);\n",
            "	gl_FragColor = vec4(col, 1.0);\n",
            "}\n"
        };

        public static string[] ColorTest =
        {
            "#version 150 compatibility\n",
            "uniform float iTime;\n",
            "in vec3 vColor;\n",
            "void main() {\n",
            "   vec3 col = vColor*sin(iTime);\n",
            "	gl_FragColor = vec4(col, 1.0);\n",
            "}\n"
        };

        public static string[] TextureTest = {
            "#version 150 compatibility\n",
            "in vec2 fTexCoord;\n",
            "uniform sampler2D myTextureSampler;\n",
            "void main() {\n",
            "    vec3 col = texture2D(myTextureSampler, fTexCoord).xyz;\n",
            "    gl_FragColor = vec4(col, 0);\n",
            "}\n"
        };
    }
}
