using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uriel.DataTypes;

namespace Uriel.ShaderTypes
{

    public class ShaderBlob
    {
        public string DisplayName { get; set; }
        public ShaderCreationArguments CreationArguments { get; set; }
        public bool TreatAsGood { get; set; }
        public string ErrorMessage { get; set; }
        public IShaderProgram Program { get; set; }
        public IVertexArray VertexArray { get; set; }
        public uint TextureName { get; set; }
    }

    public class ShaderCreationArguments
    {
        public ShaderBlobType Type { get; set; }

        public String SimpleName { get; set; }

        public List<string> FragmentShaderSource { get; set; }

        public String TexturePath { get; set; }

        public DateTime Changed { get; set; }

        public string FileName { get; set; }

        public string ConvenientName()
        {
            if (this.Type.ShaderSource == ShaderSource.FromFile_ShaderToy || this.Type.ShaderSource == ShaderSource.FromFile_Other)
            {
                return String.Format("{0}-{1}", FileName, Changed.ToString());
            }
            else
            {
                return this.SimpleName;
            }
        }

        public override string ToString()
        {
            return ConvenientName(); // string.Join(",", FragmentShaderSource);
        }
    }

    /// <summary>
    /// Type information for the Shader - this should be a "big enum" 
    /// </summary>
    public class ShaderBlobType
    {
        #region PredeterminedTypes

        public static ShaderBlobType PlainVertexNoUniforms
        {
            get
            {
                return new ShaderBlobType()
                {
                    UseTexture = false,
                    VertexFormat = VertexFormat.Plain,
                    VertexShaderVersion = ShaderVersion.Version150Compatability,
                    UseIndexing = false,
                    Uniforms = UniformSet.None(),
                    FragmentShaderVersion = ShaderVersion.Version150Compatability
                };
            }
        }

        public static ShaderBlobType Time
        {
            get
            {
                return new ShaderBlobType()
                {
                    UseTexture = false,
                    VertexFormat = VertexFormat.Plain,
                    VertexShaderVersion = ShaderVersion.Version150Compatability,
                    UseIndexing = false,
                    Uniforms = UniformSet.Time(),
                    FragmentShaderVersion = ShaderVersion.Version150Compatability
                };
            }
        }

        public static ShaderBlobType DimensionAndTime
        {
            get
            {
                return new ShaderBlobType()
                {
                    UseTexture = false,
                    VertexFormat = VertexFormat.Plain,
                    VertexShaderVersion = ShaderVersion.Version150Compatability,
                    UseIndexing = false,
                    Uniforms = UniformSet.DimensionAndTime(),
                    FragmentShaderVersion = ShaderVersion.Version150Compatability
                };
            }
        }

        public static ShaderBlobType Standard
        {
            get
            {
                return new ShaderBlobType()
                {
                    UseTexture = false,
                    VertexFormat = VertexFormat.Plain,
                    VertexShaderVersion = ShaderVersion.Version150Compatability,
                    UseIndexing = true,
                    Uniforms = UniformSet.DimensionAndTime(),
                    FragmentShaderVersion = ShaderVersion.Version150Compatability
                };
            }
        }

        public static ShaderBlobType Standard_FromFile
        {
            get
            {
                return new ShaderBlobType()
                {
                    ShaderSource = ShaderSource.FromFile_ShaderToy,
                    UseTexture = false,
                    VertexFormat = VertexFormat.Plain,
                    VertexShaderVersion = ShaderVersion.Version150Compatability,
                    UseIndexing = true,
                    Uniforms = UniformSet.ShaderToyStandard(),
                    FragmentShaderVersion = ShaderVersion.Version150Compatability
                };
            }
        }

        public static ShaderBlobType Color_NoIndex
        {
            get
            {
                return new ShaderBlobType()
                {
                    UseTexture = false,
                    VertexFormat = VertexFormat.WithColor,
                    VertexShaderVersion = ShaderVersion.Version150Compatability,
                    UseIndexing = false,
                    Uniforms = UniformSet.Time(),
                    FragmentShaderVersion = ShaderVersion.Version150Compatability
                };
            }
        }

        public static ShaderBlobType Color_WithIndex
        {
            get
            {
                return new ShaderBlobType()
                {
                    UseTexture = false,
                    VertexFormat = VertexFormat.WithColor,
                    VertexShaderVersion = ShaderVersion.Version150Compatability,
                    UseIndexing = true,
                    Uniforms = UniformSet.Time(),
                    FragmentShaderVersion = ShaderVersion.Version150Compatability
                };
            }
        }

        public static ShaderBlobType TextureStandard
        {
            get
            {
                return new ShaderBlobType()
                {
                    UseTexture = true,
                    VertexFormat = VertexFormat.WithTexture,
                    VertexShaderVersion = ShaderVersion.Version150Compatability,
                    UseIndexing = true,
                    Uniforms = UniformSet.None(),
                    FragmentShaderVersion = ShaderVersion.Version150Compatability
                };
            }
        }

        public static ShaderBlobType Texture_FromFile
        {
            get
            {
                return new ShaderBlobType()
                {
                    ShaderSource = ShaderSource.FromFile_ShaderToy,
                    UseTexture = true,
                    VertexFormat = VertexFormat.Plain,
                    VertexShaderVersion = ShaderVersion.Version150Compatability,
                    UseIndexing = true,
                    Uniforms = UniformSet.ShaderToyStandardPlusTexture(),
                    FragmentShaderVersion = ShaderVersion.Version150Compatability
                };
            }
        }

        public static ShaderBlobType UrielStandard_FromFile
        {
            get
            {
                return new ShaderBlobType()
                {
                    ShaderSource = ShaderSource.FromFile_Other,
                    UseTexture = false,
                    VertexFormat = VertexFormat.Plain,
                    VertexShaderVersion = ShaderVersion.Version150Compatability,
                    UseIndexing = true,
                    Uniforms = UniformSet.UrielStandard(),
                    FragmentShaderVersion = ShaderVersion.Version150Compatability
                };
            }
        }

        #endregion

        public ShaderSource ShaderSource { get; set; }

        public bool UseTexture { get; private set; }
        public VertexFormat VertexFormat { get; private set; }
        public ShaderVersion VertexShaderVersion { get; private set; }

        public bool UseIndexing { get; private set; }

        public List<FragmentShaderUniformType> Uniforms { get; private set; }

        public ShaderVersion FragmentShaderVersion { get; private set; }


    }

    public enum ShaderSource
    {
        BuiltIn,
        FromFile_ShaderToy,
        FromFile_Other
    }

    public enum VertexFormat
    {
        Plain,
        WithColor,
        WithTexture,
        WithColorAndTexture
    }

    public enum ShaderVersion
    {
        Version150Compatability,
        Version300Core
    }

    public static class ShaderVersionToString
    {
        public static string ToShaderVersionString(this ShaderVersion versionEnum)
        {
            if (versionEnum == ShaderVersion.Version150Compatability)
            {
                return "#version 150 compatibility\n";
            }
            else if (versionEnum == ShaderVersion.Version300Core)
            {
                return "#version 300 core\n";
            }
            else
            {
                throw new InvalidOperationException("Unsupported Version.");
            }
        }

    }

    public enum FragmentShaderUniformType
    {
        Dimension,
        Time,
        Mouse,
        Texture,
        CursorPosition,
        CursorMovement
    }

    public static class UniformTypeToUniformDeclaration
    {
        public static readonly Dictionary<FragmentShaderUniformType, String> Definitions = new Dictionary<FragmentShaderUniformType, String>()
        {
            { FragmentShaderUniformType.Dimension, "uniform vec2 iResolution;\n" },
            { FragmentShaderUniformType.Mouse, "uniform vec4 iMouse;\n" },
            { FragmentShaderUniformType.Time,  "uniform float iTime;\n" },
            { FragmentShaderUniformType.Texture, "uniform sampler2D iTexture;\n" },
            { FragmentShaderUniformType.CursorPosition, "uniform vec3 iCursorPosition;\n" },
            { FragmentShaderUniformType.CursorMovement, "uniform vec3 iCursorMovement;\n" }
        };
    }

    public static class UniformSet {
        
        public static List<FragmentShaderUniformType> None()
        {
            return new List<FragmentShaderUniformType>();
        }

        public static List<FragmentShaderUniformType> Time()
        {
            return new List<FragmentShaderUniformType>() { FragmentShaderUniformType.Time };
        }

        public static List<FragmentShaderUniformType> Dimension()
        {
            return new List<FragmentShaderUniformType>() { FragmentShaderUniformType.Dimension };
        }

        public static List<FragmentShaderUniformType> DimensionAndTime()
        {
            return new List<FragmentShaderUniformType>() { FragmentShaderUniformType.Dimension, FragmentShaderUniformType.Time };
        }

        public static List<FragmentShaderUniformType> ShaderToyStandard()
        {
            return new List<FragmentShaderUniformType>() { FragmentShaderUniformType.Dimension, FragmentShaderUniformType.Time, FragmentShaderUniformType.Mouse };
        }

        public static List<FragmentShaderUniformType> ShaderToyStandardPlusTexture()
        {
            return new List<FragmentShaderUniformType>() { FragmentShaderUniformType.Dimension, FragmentShaderUniformType.Time, FragmentShaderUniformType.Mouse, FragmentShaderUniformType.Texture };
        }

        public static List<FragmentShaderUniformType> UrielStandard()
        {
            return new List<FragmentShaderUniformType>() { FragmentShaderUniformType.Dimension, FragmentShaderUniformType.Time, FragmentShaderUniformType.CursorPosition, FragmentShaderUniformType.CursorMovement };
        }

    }


}
