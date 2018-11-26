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

        public static string[] BaseShaderAlternate = {
            "#version 150 compatibility\n",
            "uniform float u_time;\n",
            "uniform vec2 resolution;\n",
            "void main() {\n",
            "   vec2 normalized = gl_FragCoord.xy/resolution;\n",
            "   vec3 col = 0.5 + 0.5*cos(u_time+normalized.xyx+vec3(0,0,0));\n",
            "	gl_FragColor = vec4(col, 1.0);\n",
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
    }
}
