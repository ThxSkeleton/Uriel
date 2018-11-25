using OpenGL;
using System;
using System.Collections.Generic;
using System.Text;

namespace Uriel.DataTypes
{

    /// <summary>
    /// Program abstraction.
    /// </summary>
    public class ShaderProgram : IDisposable
    {
        private readonly List<string> VertexSource;
        private readonly List<string> FragmentSource;

        public bool LinkedStatus;
        public uint ProgramName;
        public int LocationMVP;
        public int LocationPosition;
        public int LocationU_Time;
        public int LocationResolution;

        public ShaderProgram(List<string> vertexSource, List<string> fragmentSource)
        {
            this.VertexSource = vertexSource;
            this.FragmentSource = fragmentSource;
        }

        public void Link() { 
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

                LocationPosition = Gl.GetAttribLocation(ProgramName, "aPosition");
                LocationU_Time = Gl.GetUniformLocation(ProgramName, "u_time");
                LocationResolution = Gl.GetUniformLocation(ProgramName, "resolution");
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

            // Get attributes locations
            if (LocationPosition < 0)
            {
                throw new InvalidOperationException("no attribute aPosition");
            }

            // Get attributes locations
            if (LocationU_Time < 0)
            {
                throw new InvalidOperationException("no attribute u_time");
            }

            // Get attributes locations
            if (LocationResolution < 0)
            {
                throw new InvalidOperationException("no attribute resolution");
            }
        }

        public void Dispose()
        {
            Gl.DeleteProgram(ProgramName);
        }
    }
}

