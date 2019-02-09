using OpenGL;
using System;
using Uriel.DataTypes;
using Uriel.ShaderTypes;
using Uriel.Support;

namespace Uriel.GLInteractions
{
    public class ShaderBuilder
    {
        private readonly ShaderCreationArguments badShaderArguments;

        private ShaderBlob BadShader;

        public ShaderBuilder(ShaderCreationArguments badShader)
        {
            this.badShaderArguments = badShader;
        }

        public ShaderBlob BuildProgram(ShaderCreationArguments args)
        {
            if (BadShader == null)
            {
                BadShader = BuildProgramInternal(this.badShaderArguments);
            }

            return BuildProgramInternal(args);
        }

        private ShaderBlob BuildProgramInternal(ShaderCreationArguments args)
        {
            try
            {
                PngTexture texture = null;

                if (args.Type.UseTexture)
                {
                    texture = new PngTexture(args.TexturePath);
                    texture.Load();
                    texture.Create();
                    GlErrorLogger.Check();

                    // Do I even need this?
                    Gl.BindTexture(TextureTarget.Texture2d, texture.TextureName);
                    GlErrorLogger.Check();
                }

                var vertexSource = VertexShaderSource.VertexSourceLookup(args.Type.VertexFormat, args.Type.VertexShaderVersion);

                var _Program = new ShaderProgram(args.FragmentShaderSource, vertexSource, args.Type.Uniforms, args.Type.VertexFormat);
                GlErrorLogger.Check();

                _Program.Link();
                GlErrorLogger.Check();

                IVertexArray _VertexArray = BuildVertexArray(_Program.VertexLocations, args);

                GlErrorLogger.Check();

                return new ShaderBlob()
                {
                    DisplayName = args.ConvenientName(),
                    CreationArguments = args,
                    TreatAsGood = true,
                    Program = _Program,
                    VertexArray = _VertexArray,
                    TextureName = !args.Type.UseTexture ? 0 : texture.TextureName,
                };
            }
            catch (Exception e)
            {
                StaticLogger.Logger.ErrorFormat("{1} - Shader Error {0}", args.SimpleName, e.ToString());

                if (BadShader == null)
                {
                    throw new InvalidOperationException("Could not build BadShader!");
                }

                return new ShaderBlob()
                {
                    DisplayName = args.ConvenientName(),
                    CreationArguments = BadShader.CreationArguments,
                    TreatAsGood = false,
                    ErrorMessage = e.ToString(),
                    Program = BadShader.Program,
                    VertexArray = BadShader.VertexArray
                };
            }

        }

        private IVertexArray BuildVertexArray(VertexLocations vertexLocations, ShaderCreationArguments args)
        {
            if (!args.Type.UseIndexing)
            {
                return new VertexArray(vertexLocations, args.Type, RawVertexData.NonIndexed);
            }
            else
            {
                return new VertexArray(vertexLocations, args.Type, RawVertexData.Indexed);
            }
        }
    }
}
