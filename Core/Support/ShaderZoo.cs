using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uriel.ShaderTypes;

namespace Uriel.Support
{
    public static class ShaderZoo
    {
        public static List<ShaderCreationArguments> BuildZoo()
        {
            var toReturn = new List<ShaderCreationArguments>();

            var SimplestShader_Args = new ShaderCreationArguments()
            {
                Type = ShaderBlobType.BabysFirstShader,
                DisplayName = "SimplestShader",
                FragmentShaderSource = new List<string>(BuiltInShaderSource.SimpleShader),
            };

            var SimplestShader_WithTime_Args = new ShaderCreationArguments()
            {
                Type = ShaderBlobType.BabysSecondShader,
                DisplayName = "SimplestShader_WithTime",
                FragmentShaderSource = new List<string>(BuiltInShaderSource.ShaderNoResolution),
            };

            var BaseShader_Args = new ShaderCreationArguments()
            {
                Type = ShaderBlobType.Standard,
                DisplayName = "BaseShader",
                FragmentShaderSource = new List<string>(BuiltInShaderSource.BaseShader),
            };

            var BaseShader2_Args = new ShaderCreationArguments()
            {
                Type = ShaderBlobType.Standard,
                DisplayName = "BaseShader2",
                FragmentShaderSource = new List<string>(BuiltInShaderSource.BaseShader2),
            };

            var BaseShaderAlternate_Args = new ShaderCreationArguments()
            {
                Type = ShaderBlobType.Standard,
                DisplayName = "BaseShaderAlternate",
                FragmentShaderSource = new List<string>(BuiltInShaderSource.BaseShaderAlternate),
            };

            toReturn.Add(SimplestShader_Args);
            toReturn.Add(SimplestShader_WithTime_Args);
            toReturn.Add(BaseShader_Args);
            toReturn.Add(BaseShader2_Args);
            toReturn.Add(BaseShaderAlternate_Args);

            return toReturn;
        }
    }
}
