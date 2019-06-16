using OpenGL;
using System;
using System.Drawing;
using Uriel.DataTypes;
using Uriel.ShaderTypes;

namespace Uriel.GLInteractions
{
    public class RenderLoop
    {
        public void Render(ShaderBlob toRender, UniformValues uniforms, Size s)
        {
            Gl.Viewport(0, 0, s.Width, s.Height);
            Gl.Clear(ClearBufferMask.ColorBufferBit);

            // Select the program for drawing
            Gl.UseProgram(toRender.Program.ProgramName);

            Gl.UseProgram(toRender.Program.ProgramName);

            SetUniforms(toRender.Program.UniformLocations, uniforms);

            if (toRender.CreationArguments.Type.UseTexture)
            {
                Gl.BindTexture(TextureTarget.Texture2d, toRender.TextureName);
            }

            // Use the vertex array
            Gl.BindVertexArray(toRender.VertexArray.ArrayName);
            GlErrorLogger.Check();
            // Draw triangle
            if (toRender.CreationArguments.Type.UseIndexing)
            {
                Gl.DrawElements(PrimitiveType.Triangles, toRender.VertexArray.Count, DrawElementsType.UnsignedInt, IntPtr.Zero);
            }
            else
            {
                Gl.DrawArrays(PrimitiveType.Triangles, 0, toRender.VertexArray.Count);
            }
            GlErrorLogger.Check();
        }

        private void SetUniforms(UniformLocations uniformsLocations, UniformValues uniformsValues)
        {
            if (LocationValidation.Enabled(uniformsLocations.Location_iTime))
            {
                Gl.Uniform1f<float>(uniformsLocations.Location_iTime, 1, (float)uniformsValues.Time);
            }

            if (LocationValidation.Enabled(uniformsLocations.Location_iResolution))
            {
                Gl.Uniform2f(uniformsLocations.Location_iResolution, 1, uniformsValues.Resolution);
            }

            if (LocationValidation.Enabled(uniformsLocations.Location_iCursorPosition))
            {
                Gl.Uniform3f(uniformsLocations.Location_iCursorPosition, 1, uniformsValues.CursorPosition);
            }

            if (LocationValidation.Enabled(uniformsLocations.Location_iCursorMovement))
            {
                Gl.Uniform3f(uniformsLocations.Location_iCursorMovement, 1, uniformsValues.CursorMovement);
            }
        }
    }
}
