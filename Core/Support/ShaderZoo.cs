using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uriel.DataTypes;
using Uriel.ShaderTypes;
using static Uriel.DataTypes.PngTexture;

namespace Uriel.Support
{
    public static class ShaderZoo
    {
        public static ShaderCreationArguments BadShaderArguments()
        {
            return new ShaderCreationArguments()
            {
                Type = ShaderBlobType.Time,
                SimpleName = "BadShader",
                FragmentShaderSource = new List<string>(BuiltInShaderSource.BadShader),
            };

        }

        public static List<ShaderCreationArguments> BuildZoo()
        {
            var toReturn = new List<ShaderCreationArguments>();

            var SimplestShader_Args = new ShaderCreationArguments()
            {
                Type = ShaderBlobType.PlainVertexNoUniforms,
                SimpleName = "SimplestShader",
                FragmentShaderSource = new List<string>(BuiltInShaderSource.SimpleShader),
            };

            var SimplestShader_WithTime_Args = new ShaderCreationArguments()
            {
                Type = ShaderBlobType.Time,
                SimpleName = "SimplestShader_WithTime",
                FragmentShaderSource = new List<string>(BuiltInShaderSource.ShaderNoResolution),
            };

            var BaseShader_Args = new ShaderCreationArguments()
            {
                Type = ShaderBlobType.Standard,
                SimpleName = "BaseShader",
                FragmentShaderSource = new List<string>(BuiltInShaderSource.BaseShader),
            };

            var BaseShader2_Args = new ShaderCreationArguments()
            {
                Type = ShaderBlobType.Standard,
                SimpleName = "BaseShader2",
                FragmentShaderSource = new List<string>(BuiltInShaderSource.BaseShader2),
            };

            var BaseShaderAlternate_Args = new ShaderCreationArguments()
            {
                Type = ShaderBlobType.Standard,
                SimpleName = "BaseShaderAlternate",
                FragmentShaderSource = new List<string>(BuiltInShaderSource.BaseShaderAlternate),
            };

            var ColorNoIndex_Args = new ShaderCreationArguments()
            {
                Type = ShaderBlobType.Color_NoIndex,
                SimpleName = "Color_NoIndex",
                FragmentShaderSource = new List<string>(BuiltInShaderSource.ColorTest),
            };

            var ColorIndex_Args = new ShaderCreationArguments()
            {
                Type = ShaderBlobType.Color_WithIndex,
                SimpleName = "Color_Index",
                FragmentShaderSource = new List<string>(BuiltInShaderSource.ColorTest),
            };

            var ColorTest_Args = new ShaderCreationArguments()
            {
                Type = ShaderBlobType.TextureStandard,
                SimpleName = "TexTest",
                FragmentShaderSource = new List<string>(BuiltInShaderSource.TextureTest),
                TexturePath = @"Z:\ShaderStore\MultiColorTest.png"
            };

            var TexTest_Args = new ShaderCreationArguments()
            {
                Type = ShaderBlobType.TextureStandard,
                SimpleName = "TexTest",
                FragmentShaderSource = new List<string>(BuiltInShaderSource.TextureTest),
                TexturePath = @"Z:\ShaderStore\ColorTest.png"
            };

            var TexTest2_Args = new ShaderCreationArguments()
            {
                Type = ShaderBlobType.TextureStandard,
                SimpleName = "TexTest2",
                FragmentShaderSource = new List<string>(BuiltInShaderSource.TextureTest),
                TexturePath = @"Z:\ShaderStore\ColorTest2.png"
            };

            var TexTest3_Args = new ShaderCreationArguments()
            {
                Type = ShaderBlobType.TextureStandard,
                SimpleName = "TexTest3",
                FragmentShaderSource = new List<string>(BuiltInShaderSource.TextureTest),
                TexturePath = @"Z:\ShaderStore\ColorTest3.png"
            };

            var TexTest4_Args = new ShaderCreationArguments()
            {
                Type = ShaderBlobType.TextureStandard,
                SimpleName = "TexTest4",
                FragmentShaderSource = new List<string>(BuiltInShaderSource.TextureTest),
                TexturePath = @"Z:\ShaderStore\ColorTest4.png"
            };

            var TexTest5_Args = new ShaderCreationArguments()
            {
                Type = ShaderBlobType.TextureStandard,
                SimpleName = "TexTest5",
                FragmentShaderSource = new List<string>(BuiltInShaderSource.TextureTest),
                TexturePath = @"Z:\ShaderStore\ColorTest5.png"
            };

            var TexTestSkull_Args = new ShaderCreationArguments()
            {
                Type = ShaderBlobType.TextureStandard,
                SimpleName = "TexTestSkull",
                FragmentShaderSource = new List<string>(BuiltInShaderSource.TextureTest),
                TexturePath = @"Z:\ShaderStore\Mega-Skull.png"
            };

            var TexTestSkull2_Args = new ShaderCreationArguments()
            {
                Type = ShaderBlobType.TextureStandard,
                SimpleName = "TexTestSkull2",
                FragmentShaderSource = new List<string>(BuiltInShaderSource.TextureTest),
                TexturePath = @"Z:\ShaderStore\Mega-Skull2.png"
            };

            var TexTestSkull3_Args = new ShaderCreationArguments()
            {
                Type = ShaderBlobType.TextureStandard,
                SimpleName = "TexTestSkull2",
                FragmentShaderSource = new List<string>(BuiltInShaderSource.TextureTest),
                TexturePath = @"Z:\ShaderStore\Mega-Skull3.png"
            };

            toReturn.Add(SimplestShader_Args);
            toReturn.Add(SimplestShader_WithTime_Args);
            toReturn.Add(BaseShader_Args);
            toReturn.Add(BaseShader2_Args);
            toReturn.Add(BaseShaderAlternate_Args);
            toReturn.Add(ColorNoIndex_Args);
            toReturn.Add(ColorIndex_Args);
            toReturn.Add(ColorTest_Args);
            toReturn.Add(TexTest_Args);
            toReturn.Add(TexTest2_Args);
            toReturn.Add(TexTest3_Args);
            toReturn.Add(TexTest4_Args);
            toReturn.Add(TexTest5_Args);
            toReturn.Add(TexTestSkull_Args);
            toReturn.Add(TexTestSkull2_Args);
            toReturn.Add(TexTestSkull3_Args);

            return toReturn;
        }
    }
}
