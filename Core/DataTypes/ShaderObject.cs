﻿using OpenGL;
using System;
using System.Text;

namespace Uriel.DataTypes
{
    /// <summary>
    /// Shader object abstraction.
    /// </summary>
    public class ShaderObject : IDisposable
    {
        public ShaderObject(ShaderType shaderType, string[] source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            // Create
            ShaderName = Gl.CreateShader(shaderType);
            // Submit source code
            Gl.ShaderSource(ShaderName, source);
            // Compile
            Gl.CompileShader(ShaderName);
            // Check compilation status
            int compiled;

            Gl.GetShader(ShaderName, ShaderParameterName.CompileStatus, out compiled);

            if (compiled != 1)
            {
                OnError();
            }
        }

        public void OnError()
        {
            // Throw exception on compilation errors
            const int logMaxLength = 1024;

            StringBuilder infolog = new StringBuilder(logMaxLength);
            int infologLength;

            Gl.GetShaderInfoLog(ShaderName, logMaxLength, out infologLength, infolog);

            throw new InvalidOperationException($"unable to compile shader: {infolog}");
        }


        public readonly uint ShaderName;

        public void Dispose()
        {
            Gl.DeleteShader(ShaderName);
        }
    }
}
