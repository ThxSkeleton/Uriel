using OpenGL;
using System;

namespace Uriel.DataTypes
{
    /// <summary>
    /// Vertex array abstraction.
    /// </summary>
    public class IndexedVertexArrayWithTexture : IVertexArray
    {
        public IndexedVertexArrayWithTexture(IShaderProgram program, float[] position, float[] tex, uint[] indexes)
        {
            if (program == null)
            {
                throw new ArgumentNullException(nameof(program));
            }

            // Allocate buffers referenced by this vertex array
            _BufferPosition = new GlBuffer<float>(position, BufferTarget.ArrayBuffer);
            _BufferTex = new GlBuffer<float>(tex, BufferTarget.ArrayBuffer);
            _BufferIndex = new GlBuffer<uint>(indexes, BufferTarget.ElementArrayBuffer);

            GlErrorLogger.Check();

            // Generate VAO name
            ArrayName = Gl.GenVertexArray();

            GlErrorLogger.Check();
            // First bind create the VAO
            Gl.BindVertexArray(ArrayName);

            GlErrorLogger.Check();

            // Position
            int position_VERTEX_ELEMENTCOUNT = 2;
            int TextureCoordinate_VERTEX_ELEMENTCOUNT = 2;

            // Set position attribute

            // Fill Position
            Gl.BindBuffer(BufferTarget.ArrayBuffer, _BufferPosition.BufferName);
            GlErrorLogger.Check();
            Gl.VertexAttribPointer((uint)program.StandardUniforms.LocationPosition, position_VERTEX_ELEMENTCOUNT, VertexAttribType.Float, false, 0, IntPtr.Zero);
            GlErrorLogger.Check();

            // Enable attribute
            Gl.EnableVertexAttribArray((uint)program.StandardUniforms.LocationPosition);
            GlErrorLogger.Check();

            // Fill texture
            Gl.BindBuffer(BufferTarget.ArrayBuffer, _BufferTex.BufferName);
            GlErrorLogger.Check();
            Gl.VertexAttribPointer((uint)program.StandardUniforms.LocationTexture, TextureCoordinate_VERTEX_ELEMENTCOUNT, VertexAttribType.Float, false, 0, IntPtr.Zero);
            GlErrorLogger.Check();

            // Enable attribute
            Gl.EnableVertexAttribArray((uint)program.StandardUniforms.LocationTexture);
            GlErrorLogger.Check();

            Gl.BindBuffer(BufferTarget.ElementArrayBuffer, _BufferIndex.BufferName);

            GlErrorLogger.Check();
        }

        public uint ArrayName { get; private set; }

        private readonly GlBuffer<float> _BufferPosition;
        private readonly GlBuffer<float> _BufferTex;
        private readonly GlBuffer<uint> _BufferIndex;

        public int Count { get; private set; }

        public void Dispose()
        {
            Gl.DeleteVertexArrays(ArrayName);

            _BufferPosition.Dispose();
            _BufferIndex.Dispose();
        }
    }
}
