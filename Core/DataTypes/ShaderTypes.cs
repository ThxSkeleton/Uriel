using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uriel.DataTypes;

namespace Uriel.ShaderTypes
{

    public class ShaderBlob : PrettyPrintObject
    {
        public string DisplayName { get; set; }
        public ShaderCreationArguments CreationArguments { get; set; }
        public bool TreatAsGood { get; set; }
        public string ErrorMessage { get; set; }
        public IShaderProgram Program { get; set; }
        public IVertexArray VertexArray { get; set; }
        public uint TextureName { get; set; }
    }

    public class ShaderCreationArguments : PrettyPrintObject
    {
        public ShaderBlobType Type { get; set; }

        public String SimpleName { get; set; }

        public List<string> FragmentShaderSource { get; set; }

        public String TexturePath { get; set; }

        public DateTime Changed { get; set; }

        public string FileName { get; set; }

        public string ConvenientName()
        {
            if (this.Type.ShaderSource == ShaderSource.FromFile)
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
    public class ShaderBlobType : PrettyPrintObject
    {
        #region PredeterminedTypes

        public static ShaderBlobType PlainVertexNoUniforms()
        {
            return new ShaderBlobType()
            {
                UseTexture = false,
                VertexFormat = VertexFormat.Plain,
                VertexShaderVersion =  ShaderVersion.Version150Compatability,
                UseIndexing = false,
                Uniforms = UniformSet.None(),
                FragmentShaderVersion =  ShaderVersion.Version150Compatability
            };
        }

        public static ShaderBlobType Time()
        {
            return new ShaderBlobType()
            {
                UseTexture = false,
                VertexFormat = VertexFormat.Plain,
                VertexShaderVersion =  ShaderVersion.Version150Compatability,
                UseIndexing = false,
                Uniforms = UniformSet.Time(),
                FragmentShaderVersion =  ShaderVersion.Version150Compatability
            };
        }

        public static ShaderBlobType Standard()
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

        public static ShaderBlobType Color_NoIndex()
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

        public static ShaderBlobType Color_WithIndex()
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

        public static ShaderBlobType TextureStandard()
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

        public static ShaderBlobType Texture_FromFile()
        {
            return new ShaderBlobType()
            {
                ShaderSource = ShaderSource.FromFile,
                UseTexture = true,
                VertexFormat = VertexFormat.Plain,
                VertexShaderVersion = ShaderVersion.Version150Compatability,
                UseIndexing = true,
                Uniforms = UniformSet.ShaderToyStandardPlusTexture(),
                FragmentShaderVersion = ShaderVersion.Version150Compatability
            };
        }

        public static ShaderBlobType UrielStandard_FromFile()
        {
            return new ShaderBlobType()
            {
                ShaderSource = ShaderSource.FromFile,
                UseTexture = false,
                VertexFormat = VertexFormat.Plain,
                VertexShaderVersion = ShaderVersion.Version150Compatability,
                UseIndexing = true,
                Uniforms = UniformSet.UrielStandard(),
                FragmentShaderVersion = ShaderVersion.Version150Compatability
            };
        }

        #endregion

        public ShaderSource ShaderSource { get; set; }
        public bool UseTexture { get; private set; }

        /// <summary>
        /// Vertex Formatting Information
        /// </summary>
        public VertexFormat VertexFormat { get; private set; }
        public ShaderVersion VertexShaderVersion { get; private set; }
        public bool UseIndexing { get; private set; }

        /// <summary>
        /// Shader Version Information
        /// </summary>
        public ShaderVersion FragmentShaderVersion { get; private set; }

        /// <summary>
        /// The list of uniforms to use
        /// </summary>
        public List<KnownFragmentShaderUniform> Uniforms { get; private set; }
    }

    public enum ShaderSource
    {
        BuiltIn,
        FromFile,
    }

    public enum VertexFormat
    {
        Plain,
        WithColor,
        WithTexture,
        WithColorAndTexture
    }
}
