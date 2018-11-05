using HelloTriangle.DataTypes;
using OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloTriangle
{
    public class TriangleCore : IDisposable
    {

        #region Common Shading

        // Note: abstractions for drawing using programmable pipeline.

        /// <summary>
        /// The program used for drawing the triangle.
        /// </summary>
        private readonly ShaderProgram _Program;

        /// <summary>
        /// The vertex arrays used for drawing the triangle.
        /// </summary>
        private readonly VertexArray _VertexArray;
        private readonly VertexArray _VertexArray2;

        public TriangleCore()
        {
            _Program = new ShaderProgram(_VertexSourceGL, _FragmentSourceGL);
            _VertexArray = new VertexArray(_Program, _ArrayPosition, _ArrayColor);
            _VertexArray2 = new VertexArray(_Program, _ArrayPosition2, _ArrayColor);

        }

        public void Render()
        {
            // Compute the model-view-projection on CPU
            Matrix4x4f projection = Matrix4x4f.Ortho2D(-1.0f, +1.0f, -1.0f, +1.0f);
            Matrix4x4f modelview = Matrix4x4f.Translated(-0.5f, -0.5f, 0.0f);

            // Select the program for drawing
            Gl.UseProgram(_Program.ProgramName);
            // Set uniform state
            Gl.UniformMatrix4f(_Program.LocationMVP, 1, false, projection * modelview);
            // Use the vertex array
            Gl.BindVertexArray(_VertexArray.ArrayName);
            // Draw triangle
            // Note: vertex attributes are streamed from GPU memory
            Gl.DrawArrays(PrimitiveType.Triangles, 0, 3);

            //// Use the vertex array
            //Gl.BindVertexArray(_VertexArray2.ArrayName);
            //// Draw triangle
            //// Note: vertex attributes are streamed from GPU memory
            //Gl.DrawArrays(PrimitiveType.Triangles, 0, 3);
        }

        private readonly string[] _VertexSourceGL_OG = {
            "#version 150 compatibility\n",
            "uniform mat4 uMVP;\n",
            "in vec2 aPosition;\n",
            "in vec3 aColor;\n",
            "out vec3 vColor;\n",
            "void main() {\n",
            "	gl_Position = uMVP * vec4(aPosition, 0.0, 1.0);\n",
            "	vColor = aColor;\n",
            "}\n"
        };

        private readonly string[] _VertexSourceGL = {
            "#version 150 compatibility\n",
            "uniform mat4 uMVP;\n",
            "in vec2 aPosition;\n",
            "in vec3 aColor;\n",
            "out vec3 vColor;\n",
            "void main() {\n",
            "	gl_Position = uMVP * vec4(aPosition, 0.0, 1.0);\n",
            "	vColor = aColor;\n",
            "}\n"
        };

        private readonly string[] _FragmentSourceGL_OG = {
            "#version 150 compatibility\n",
            "in vec3 vColor;\n",
            "void main() {\n",
            "	gl_FragColor = vec4(vColor, 1.0);\n",
            "}\n"
        };

        private readonly string[] _FragmentSourceGL = {
            "#version 150 compatibility\n",
            "in vec3 vColor;\n",
            "void main() {\n",
            "	gl_FragColor = vec4(vColor, 1.0);\n",
            "}\n"
        };

        #endregion

        #region Common Data

        /// <summary>
        /// Vertex position array.
        /// </summary>
        private static readonly float[] _ArrayPosition = new float[] {
            0.0f, 0.0f,
            1.0f, 1.0f,
            2.0f, 2.5f
        };

        /// <summary>
        /// Vertex position array.
        /// </summary>
        private static readonly float[] _ArrayPosition2 = new float[] {
            0.25f, 0.25f,
            .3f, .3f,
            0.0f, 1.0f
        };

        /// <summary>
        /// Vertex color array.
        /// </summary>
        private static readonly float[] _ArrayColor = new float[] {
            1.0f, 0.0f, 0.0f,
            0.0f, 1.0f, 0.0f,
            0.0f, 0.0f, 1.0f
        };

        public void Dispose()
        {
            _Program?.Dispose();
            _VertexArray?.Dispose();
        }

        #endregion
    }
}
