﻿using OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uriel.DataTypes;

namespace Uriel
{
    public class StandardFragmentShaderProgram
    {
        public uint ProgramName;

        // What does the "position" attribute really mean?
        public int LocationPosition;


        public StandardUniforms StandardUniforms { get; private set; }

        /// <summary>
        /// These are used for validation only.
        /// </summary>
        private bool LinkedStatus;

        //
        // The original source, with a Model View Matrix
        //
        //private readonly string[] _VertexSourceGL_OG = {
        //    "#version 150 compatibility\n",
        //    "uniform mat4 uMVP;\n",
        //    "in vec2 aPosition;\n",
        //    "in vec3 aColor;\n",
        //    "void main() {\n",
        //    "	gl_Position = uMVP * vec4(aPosition, 0.0, 1.0);\n",
        //    "	vColor = aColor;\n",
        //    "}\n"
        //};

        private readonly string[] _VertexSourceGL_Simplest = {
            "#version 150 compatibility\n",
            "in vec2 aPosition;\n",
            "void main() {\n",
            "	gl_Position = vec4(aPosition, 0.0, 1.0);\n",
            "}\n"
        };

        private readonly List<string> FragmentSource;

        public StandardFragmentShaderProgram(List<string> fragmentSource)
        {
            this.FragmentSource = fragmentSource;
        }

        public void Link()
        {
            // Create vertex and frament shaders
            // Note: they can be disposed after linking to program; resources are freed when deleting the program
            using (ShaderObject vObject = new ShaderObject(ShaderType.VertexShader, _VertexSourceGL_Simplest))
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

                this.StandardUniforms = new StandardUniforms()
                {
                    Location_u_time = Gl.GetUniformLocation(ProgramName, "u_time"),
                    Location_resolution = Gl.GetUniformLocation(ProgramName, "resolution")
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

            // Get attributes locations
            if (LocationPosition < 0)
            {
                throw new InvalidOperationException("no attribute aPosition");
            }

            // Get attributes locations
            if (this.StandardUniforms.Location_u_time < 0)
            {
                throw new InvalidOperationException("no attribute u_time");
            }

            // Get attributes locations
            if (this.StandardUniforms.Location_u_time < 0)
            {
                throw new InvalidOperationException("no attribute resolution");
            }
        }
    }
}