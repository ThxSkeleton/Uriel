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

        public String DisplayName { get; set; }

        public List<string> FragmentShaderSource { get; set; }

        public String TexturePath { get; set; }
    }

    public class ShaderBlobType
    {
        public static ShaderBlobType BabysFirstShader
        {
            get
            {
                return new ShaderBlobType()
                {
                    UseTexture = false,
                    VertexFormat = VertexFormat.Plain,
                    VertexShaderVersion = ShaderVersion.Version150Compatability,
                    UseIndexing = false,
                    FragmentShaderUniformType = FragmentShaderUniformType.None,
                    FragmentShaderVersion = ShaderVersion.Version150Compatability
                };
            }
        }

        public static ShaderBlobType BabysSecondShader
        {
            get
            {
                return new ShaderBlobType()
                {
                    UseTexture = false,
                    VertexFormat = VertexFormat.Plain,
                    VertexShaderVersion = ShaderVersion.Version150Compatability,
                    UseIndexing = false,
                    FragmentShaderUniformType = FragmentShaderUniformType.DimensionAndTime,
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
                    FragmentShaderUniformType = FragmentShaderUniformType.DimensionAndTime,
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
                    FragmentShaderUniformType = FragmentShaderUniformType.DimensionAndTime,
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
                    FragmentShaderUniformType = FragmentShaderUniformType.DimensionAndTime,
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
                    FragmentShaderUniformType = FragmentShaderUniformType.DimensionAndTime,
                    FragmentShaderVersion = ShaderVersion.Version150Compatability
                };
            }
        }

        public bool UseTexture { get; private set; }
        public VertexFormat VertexFormat { get; private set; }
        public ShaderVersion VertexShaderVersion { get; private set; }

        public bool UseIndexing { get; private set; }

        public FragmentShaderUniformType FragmentShaderUniformType { get; private set; }

        public ShaderVersion FragmentShaderVersion { get; private set; }
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

    public enum FragmentShaderUniformType
    {
        None,
        Dimension,
        DimensionAndTime,
        ShaderToy,
    }
}
