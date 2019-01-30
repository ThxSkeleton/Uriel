using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uriel.Support
{
    public class ShaderToyConverter
    {
        private class ToyTranslation
        {
            public ToyTranslation(string toy, string uriel)
            {
                this.ToyTerm = toy;
                this.UrielTerm = uriel;
            }

            public string ToyTerm { get; set; }
            public string UrielTerm { get; set; }
        }

        private static List<ToyTranslation> Translations
        {
            get
            {
                return new List<ToyTranslation>()
                {
                    // new ToyTranslation("void mainImage( out vec4 fragColor, in vec2 fragCoord )", "void main()"),
                    // new ToyTranslation("fragColor", "gl_FragColor"),
                    // new ToyTranslation("fragCoord", "gl_FragCoord"),
                };
            }
        }

        private static List<string> Prepends
        {
            get
            {
                return new List<string>(){
                    "#version 150 compatibility\n",
                    "uniform float u_time;\n",
                    "uniform vec2 resolution;\n",
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
                    "    mainImage(col, gl_FragCoord);\n",
                    "    gl_FragColor = col;\n",
                    "}\n"
                };
            }
        }

        private static string FindReplace(string input, ToyTranslation translation)
        {
            return input.Replace(translation.ToyTerm, translation.UrielTerm);
        }

        private static string FindReplace_All(string input)
        {
            string temp = input;
            foreach(ToyTranslation t in Translations)
            {
                temp = FindReplace(temp, t);
            }

            return temp;
        }

        public static List<string> TranslateShader(List<string> inputShader)
        {
            List<string> toReturn = Prepends;
            List<String> newShader = inputShader.Select(FindReplace_All).ToList();
            toReturn.AddRange(newShader);
            toReturn.AddRange(Postpends);
            return toReturn;
        }

    }
}
