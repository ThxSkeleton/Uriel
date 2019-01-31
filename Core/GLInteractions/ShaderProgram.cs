using OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uriel.DataTypes;
using Uriel.ShaderTypes;

namespace Uriel
{
    public class ShaderProgram : IShaderProgram
    {
        public uint ProgramName { get; private set; }
        public UniformLocations UniformLocations { get; private set; }
        public VertexLocations VertexLocations { get; private set; }

        private readonly List<string> FragmentSource;
        private readonly List<string> VertexSource;
        private readonly FragmentShaderUniformType expectedUniforms;
        private readonly VertexFormat expectedVertexes;

        public ShaderProgram(List<string> fragmentSource, List<string> vertexSource, FragmentShaderUniformType expectedUniforms, VertexFormat expectedVertexes) 
        {
            this.FragmentSource = fragmentSource;
            this.VertexSource = vertexSource;
            this.expectedUniforms = expectedUniforms;
            this.expectedVertexes = expectedVertexes;
        }

        public void Link()
        {
            // Create vertex and frament shaders
            // Note: they can be disposed after linking to program; resources are freed when deleting the program
            using (ShaderObject vObject = new ShaderObject(ShaderType.VertexShader, VertexSource.ToArray()))
            using (ShaderObject fObject = new ShaderObject(ShaderType.FragmentShader, FragmentSource.ToArray()))
            {
                // Create program
                ProgramName = Gl.CreateProgram();
                // Attach shaders
                Gl.AttachShader(ProgramName, vObject.ShaderName);
                Gl.AttachShader(ProgramName, fObject.ShaderName);
                // Link program
                Gl.LinkProgram(ProgramName);

                int linked;

                Gl.GetProgram(ProgramName, ProgramProperty.LinkStatus, out linked);

                var LinkedStatus = linked != 0;

                Validate(LinkedStatus);

                this.UniformLocations = new UniformLocations()
                {
                    Location_iTime = Gl.GetUniformLocation(ProgramName, "iTime"),
                    Location_iResolution = Gl.GetUniformLocation(ProgramName, "iResolution"),
                };

                this.UniformLocations.ValidateAllPresent(expectedUniforms);

                this.VertexLocations = new VertexLocations()
                {
                    Location_Position = Gl.GetAttribLocation(ProgramName, "aPosition"),
                    Location_Color = Gl.GetAttribLocation(ProgramName, "aColor"),
                    Location_Texture = Gl.GetAttribLocation(ProgramName, "aTexCoord")
                };

                this.VertexLocations.ValidateAllPresent(expectedVertexes);
            }

        }

        private void Validate(bool linked)
        {
            if (!linked)
            {
                const int logMaxLength = 1024;

                StringBuilder infolog = new StringBuilder(logMaxLength);
                int infologLength;

                Gl.GetProgramInfoLog(ProgramName, 1024, out infologLength, infolog);

                throw new InvalidOperationException($"unable to link program: {infolog}");
            }
        }
    }
}
