using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uriel.ShaderTypes;

namespace Uriel.Support
{
    public class ShaderToyConverter
    {
        private static List<string> UniformsPrepends
        {
            get
            {
                return new List<string>() {
                    "uniform float iTime;\n",
                    "uniform vec2 iResolution;\n",
                    "const vec4 iMouse = vec4(-2.0f);\n"
                };
            }
        }

        private static List<string> Postpends
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

        public static List<string> TranslateShader(List<string> inputShader, ShaderBlobType type)
        {
            List<string> toReturn = UniformsPrepends;
            toReturn.AddRange(inputShader);
            toReturn.AddRange(Postpends);
            return toReturn;
        }
    }
}
