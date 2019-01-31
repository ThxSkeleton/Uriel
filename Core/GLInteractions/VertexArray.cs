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
        private const int floats_per_position = 2;
        private const int floats_per_color = 3;
        private const int floats_per_textureCoordinate = 2;

        // In a vertex array where different attributes are mixed, there is space between the attributes
        // Not the case here.
        private const int stride = 0;

        // where to start in all the arrays to read from 
        private static IntPtr startingLocation = IntPtr.Zero;

        public VertexArray(VertexLocations vertexLocations, ShaderBlobType type, RawVertexData vertexInformation)
        {
            if (vertexLocations == null)
            {
                throw new ArgumentNullException(nameof(vertexLocations));
            }

            // Allocate buffers referenced by this vertex array
            _BufferPosition = new GlBuffer<float>(vertexInformation.positions, BufferTarget.ArrayBuffer);

            // Generate VAO name
            ArrayName = Gl.GenVertexArray();
            // First bind create the VAO
            Gl.BindVertexArray(ArrayName);

            // Select the buffer object
            Gl.BindBuffer(BufferTarget.ArrayBuffer, _BufferPosition.BufferName);
            // Format the vertex information: 2 floats from the current buffer
            Gl.VertexAttribPointer((uint)vertexLocations.Location_Position, floats_per_position, VertexAttribType.Float, false, stride, startingLocation);
            // Enable attribute
            Gl.EnableVertexAttribArray((uint)vertexLocations.Location_Position);

            if (type.UseIndexing)
            {
                _BufferIndex = new GlBuffer<uint>(vertexInformation.indexes, BufferTarget.ElementArrayBuffer);
                Gl.BindBuffer(BufferTarget.ElementArrayBuffer, _BufferIndex.BufferName);

                Count = vertexInformation.indexes.Length;
            }
            else
            {
                Count = (vertexInformation.positions.Length / floats_per_position);
            }

            if (type.VertexFormat == VertexFormat.WithColor || type.VertexFormat == VertexFormat.WithColorAndTexture)
            {
                _BufferColor = new GlBuffer<float>(vertexInformation.colors, BufferTarget.ArrayBuffer);

                Gl.BindBuffer(BufferTarget.ArrayBuffer, _BufferColor.BufferName);
                // Format the vertex information: 3 floats from the current buffer
                Gl.VertexAttribPointer((uint)vertexLocations.Location_Color, floats_per_color, VertexAttribType.Float, false, stride, startingLocation);
                // Enable attribute
                Gl.EnableVertexAttribArray((uint)vertexLocations.Location_Color);
            }

            if (type.VertexFormat == VertexFormat.WithTexture || type.VertexFormat == VertexFormat.WithColorAndTexture)
            {
                _BufferTex = new GlBuffer<float>(vertexInformation.textures, BufferTarget.ArrayBuffer);

                Gl.BindBuffer(BufferTarget.ArrayBuffer, _BufferTex.BufferName);
                // Format the vertex information: 2 floats from the current buffer
                Gl.VertexAttribPointer((uint)vertexLocations.Location_Texture, floats_per_textureCoordinate, VertexAttribType.Float, false, stride, startingLocation);
                // Enable attribute
                Gl.EnableVertexAttribArray((uint)vertexLocations.Location_Texture);
            }

        }

        private readonly GlBuffer<uint> _BufferIndex;
        private readonly GlBuffer<float> _BufferPosition;
        private readonly GlBuffer<float> _BufferColor;
        private readonly GlBuffer<float> _BufferTex;

        public uint ArrayName { get; private set; }

        public int Count { get; private set; }

        public void Dispose()
        {
            Gl.DeleteVertexArrays(ArrayName);

            _BufferPosition.Dispose();
        }
    }
}
