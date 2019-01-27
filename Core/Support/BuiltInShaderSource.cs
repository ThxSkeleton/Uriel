namespace Uriel
{
    public static class BuiltInFragmentShaderSource
    {
        public static string[] BaseShader = {
            "#version 150 compatibility\n",
            "uniform float u_time;\n",
            "uniform vec2 resolution;\n",
            "void main() {\n",
            "   vec2 normalized = gl_FragCoord.xy/resolution;\n",
            "   vec3 col = 0.5 + 0.5*cos(u_time+normalized.xyx+vec3(0,2,4));\n",
            "	gl_FragColor = vec4(col, 1.0);\n",
            "}\n"
        };

        public static string[] BaseShader2 = {
            "#version 150 compatibility\n",
            "uniform float u_time;\n",
            "uniform vec2 resolution;\n",
            "void main() {\n",
            "   vec2 normalized = gl_FragCoord.xy/resolution;\n",
            "   vec3 col = 0.5 + 0.5*cos(u_time+normalized.xyx+vec3(0,2,4));\n",
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


        public static string[] _VertexSourceGL_Simplest = {
            "#version 150 compatibility\n",
            "in vec2 aPosition;\n",
            "void main() {\n",
            "	gl_Position = vec4(aPosition, 0.0, 1.0);\n",
            "}\n"
        };

        public static string[] _VertexSourceGL_Simplest_Tex = {
            "#version 150 compatibility\n",
            "in vec2 aPosition;\n",
            "in vec2 aTexCoord;\n",
            "out vec2 fTexCoord;\n",
            "void main() {\n",
            "	gl_Position = vec4(aPosition, 0.0, 1.0);\n",
            "   fTexCoord = aTexCoord;",
            "}\n"
        };
    }
}
