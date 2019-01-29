using OpenGL;
using System;
using Uriel.ShaderTypes;

namespace Uriel.DataTypes
{
    /// <summary>
    /// Vertex array abstraction.
    /// </summary>
    public class VertexArray : IVertexArray, IDisposable
    {
        public VertexArray(ShaderLocations programLocations, ShaderBlobType type, VertexInformation vertexInformation)
        {
            if (programLocations == null)
            {
                throw new ArgumentNullException(nameof(programLocations));
            }

            // Allocate buffers referenced by this vertex array
            _BufferPosition = new GlBuffer<float>(vertexInformation.positions, BufferTarget.ArrayBuffer);

            Count = (vertexInformation.positions.Length / 2);

            // Generate VAO name
            ArrayName = Gl.GenVertexArray();
            // First bind create the VAO
            Gl.BindVertexArray(ArrayName);

            // Select the buffer object
            Gl.BindBuffer(BufferTarget.ArrayBuffer, _BufferPosition.BufferName);
            // Format the vertex information: 2 floats from the current buffer
            Gl.VertexAttribPointer((uint)programLocations.LocationPosition, 2, VertexAttribType.Float, false, 0, IntPtr.Zero);
            // Enable attribute
            Gl.EnableVertexAttribArray((uint)programLocations.LocationPosition);
        }

        private readonly GlBuffer<float> _BufferPosition;

        public uint ArrayName { get; private set; }

        public int Count { get; private set; }

        public void Dispose()
        {
            Gl.DeleteVertexArrays(ArrayName);

            _BufferPosition.Dispose();
        }
    }
}
