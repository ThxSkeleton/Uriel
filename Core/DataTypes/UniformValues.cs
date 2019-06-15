using OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uriel.DataTypes
{
    public class UniformValues : PrettyPrintObject
    {
        public double Time { get; set; }

        public Vertex2f Resolution { get; set; }

        public Vertex3f CursorPosition { get; set; }

        public Vertex3f CursorMovement { get; set; }
    }

    public enum KnownFragmentShaderUniform
    {
        Dimension,
        Time,
        Mouse,
        Texture,
        CursorPosition,
        CursorMovement
    }

    public class UniformDefinition<T> : PrettyPrintObject
    {
        public KnownFragmentShaderUniform UrielName { get; set; }

        public string InShaderName { get; set; }

        public string Declaration()
        {
            return string.Format("uniform {0} {1};\n", TypeToGLSLType(typeof(T)), this.InShaderName);
        }

        private object TypeToGLSLType(Type underlyingType)
        {
            throw new NotImplementedException();
        }
    }
    
    public static class UniformTypeToUniformDeclaration
    {
        public static readonly Dictionary<KnownFragmentShaderUniform, String> Definitions = new Dictionary<KnownFragmentShaderUniform, String>()
        {
            { KnownFragmentShaderUniform.Dimension, "uniform vec2 iResolution;\n" },
            { KnownFragmentShaderUniform.Mouse, "uniform vec4 iMouse;\n" },
            { KnownFragmentShaderUniform.Time,  "uniform float iTime;\n" },
            { KnownFragmentShaderUniform.Texture, "uniform sampler2D iTexture;\n" },
            { KnownFragmentShaderUniform.CursorPosition, "uniform vec3 iCursorPosition;\n" },
            { KnownFragmentShaderUniform.CursorMovement, "uniform vec3 iCursorMovement;\n" }
        };
    }

    public static class UniformSet
    {

        public static List<KnownFragmentShaderUniform> None()
        {
            return new List<KnownFragmentShaderUniform>();
        }

        public static List<KnownFragmentShaderUniform> Time()
        {
            return new List<KnownFragmentShaderUniform>() { KnownFragmentShaderUniform.Time };
        }

        public static List<KnownFragmentShaderUniform> Dimension()
        {
            return new List<KnownFragmentShaderUniform>() { KnownFragmentShaderUniform.Dimension };
        }

        public static List<KnownFragmentShaderUniform> DimensionAndTime()
        {
            return new List<KnownFragmentShaderUniform>() { KnownFragmentShaderUniform.Dimension, KnownFragmentShaderUniform.Time };
        }

        public static List<KnownFragmentShaderUniform> ShaderToyStandard()
        {
            return new List<KnownFragmentShaderUniform>() { KnownFragmentShaderUniform.Dimension, KnownFragmentShaderUniform.Time, KnownFragmentShaderUniform.Mouse };
        }

        public static List<KnownFragmentShaderUniform> ShaderToyStandardPlusTexture()
        {
            return new List<KnownFragmentShaderUniform>() { KnownFragmentShaderUniform.Dimension, KnownFragmentShaderUniform.Time, KnownFragmentShaderUniform.Mouse, KnownFragmentShaderUniform.Texture };
        }

        public static List<KnownFragmentShaderUniform> UrielStandard()
        {
            return new List<KnownFragmentShaderUniform>() { KnownFragmentShaderUniform.Dimension, KnownFragmentShaderUniform.Time, KnownFragmentShaderUniform.CursorPosition, KnownFragmentShaderUniform.CursorMovement };
        }
    }

}
