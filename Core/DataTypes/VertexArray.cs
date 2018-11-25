using OpenGL;
using System;

namespace Uriel.DataTypes
{
    /// <summary>
    /// Vertex array abstraction.
    /// </summary>
    public class VertexArray : IDisposable
    {
        public VertexArray(StandardFragmentShaderProgram program, float[] positions)
        {
            if (program == null)
            {
                throw new ArgumentNullException(nameof(program));
            }

            // Allocate buffers referenced by this vertex array
            _BufferPosition = new GlBuffer<float>(positions, BufferTarget.ArrayBuffer);
            
            // Generate VAO name
            ArrayName = Gl.GenVertexArray();
            // First bind create the VAO
            Gl.BindVertexArray(ArrayName);

            // Set position attribute

            // Select the buffer object
            Gl.BindBuffer(BufferTarget.ArrayBuffer, _BufferPosition.BufferName);
            // Format the vertex information: 2 floats from the current buffer
            Gl.VertexAttribPointer((uint)program.LocationPosition, 2, VertexAttribType.Float, false, 0, IntPtr.Zero);
            // Enable attribute
            Gl.EnableVertexAttribArray((uint)program.LocationPosition);
        }

        public readonly uint ArrayName;

        private readonly GlBuffer<float> _BufferPosition;


        public void Dispose()
        {
            Gl.DeleteVertexArrays(ArrayName);

            _BufferPosition.Dispose();
        }
    }
}
