using OpenGL;
using System;
using System.Runtime.InteropServices;

namespace Uriel.DataTypes
{
    /// <summary>
    /// Buffer abstraction.
    /// </summary>
    public class GlBuffer<T> : IDisposable
    {
        public GlBuffer(T[] buffer, BufferTarget bufferType)
        {
            if (buffer == null)
            {
                throw new ArgumentNullException(nameof(buffer));
            }

            // Generate a buffer name: buffer does not exists yet
            BufferName = Gl.GenBuffer();
            // First bind create the buffer, determining its type
            Gl.BindBuffer(bufferType, BufferName);

            int size = Marshal.SizeOf(default(T));

            // Set buffer information, 'buffer' is pinned automatically
            Gl.BufferData(bufferType, (uint)(size * buffer.Length), buffer, BufferUsage.StaticDraw);
        }

        public readonly uint BufferName;

        public void Dispose()
        {
            Gl.DeleteBuffers(BufferName);
        }
    }

}
