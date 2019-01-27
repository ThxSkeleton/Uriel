using OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uriel.DataTypes;

namespace Uriel
{
    public class StandardFragmentShaderProgramPlusTexture : IShaderProgram
    {
        public uint ProgramName { get; private set; }
        public ShaderLocations StandardUniforms { get; private set; }

        /// <summary>
        /// These are used for validation only.
        /// </summary>
        private bool LinkedStatus;

        private readonly List<string> FragmentSource;
        private readonly List<string> VertexSource;

        public StandardFragmentShaderProgramPlusTexture(List<string> fragmentSource, List<string> vertexSource) 
        {
            this.FragmentSource = fragmentSource;
            this.VertexSource = vertexSource;
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

                this.LinkedStatus = linked != 0;

                this.StandardUniforms = new ShaderLocations()
                {
                    Location_u_time = Gl.GetUniformLocation(ProgramName, "u_time"),
                    Location_resolution = Gl.GetUniformLocation(ProgramName, "resolution"),
                    LocationPosition = Gl.GetAttribLocation(ProgramName, "aPosition"),
                    LocationColor = Gl.GetAttribLocation(ProgramName, "aColor"),
                    LocationTexture = Gl.GetAttribLocation(ProgramName, "aTexCoord")
                };
            }

            Validate();
        }

        private void Validate()
        {
            if (!LinkedStatus)
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
