﻿using OpenGL;
using System;

namespace Uriel.DataTypes
{
    /// <summary>
    /// Vertex array abstraction.
    /// </summary>
    public class IndexedVertexArray : IDisposable
    {
        public IndexedVertexArray(ShaderProgram program, float[] positions, uint[] indexes)
        {
            this.Count = indexes.Length;

            if (program == null)
            {
                throw new ArgumentNullException(nameof(program));
            }

            // Allocate buffers referenced by this vertex array
            _BufferPosition = new GlBuffer<float>(positions, BufferTarget.ArrayBuffer);
            _BufferIndex = new GlBuffer<uint>(indexes, BufferTarget.ElementArrayBuffer);

            GlErrorLogger.Check();

            // Generate VAO name
            ArrayName = Gl.GenVertexArray();

            GlErrorLogger.Check();
            // First bind create the VAO
            Gl.BindVertexArray(ArrayName);

            GlErrorLogger.Check();

            // Set position attribute

            // Select the buffer object
            Gl.BindBuffer(BufferTarget.ArrayBuffer, _BufferPosition.BufferName);

            // Format the vertex information: 2 floats from the current buffer
            Gl.VertexAttribPointer((uint)program.LocationPosition, 2, VertexAttribType.Float, false, 0, IntPtr.Zero);


            // Enable attribute
            Gl.EnableVertexAttribArray((uint)program.LocationPosition);

            GlErrorLogger.Check();

            Gl.BindBuffer(BufferTarget.ElementArrayBuffer, _BufferIndex.BufferName);
        }

        public readonly uint ArrayName;

        private readonly GlBuffer<float> _BufferPosition;

        private readonly GlBuffer<uint> _BufferIndex;

        public int Count { get; }

        public void Dispose()
        {
            Gl.DeleteVertexArrays(ArrayName);

            _BufferPosition.Dispose();
            _BufferIndex.Dispose();
        }
    }
}