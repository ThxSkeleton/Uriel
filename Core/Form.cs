
// Copyright (C) 2016-2018 Luca Piccioni
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using System;
using System.Windows.Forms;
using System.Text;

using Khronos;
using OpenGL;
using HelloTriangle.DataTypes;

namespace HelloTriangle
{
	/// <summary>
	/// Sample drawing a simple, rotating and colored triangle.
	/// </summary>
	/// <remarks>
	/// Supports:
	/// - OpenGL 3.2
	/// - OpenGL 1.1/1.0 (deprecated)
	/// - OpenGL ES2
	/// </remarks>
	public partial class Form : System.Windows.Forms.Form
	{
        private TriangleCore triangleCore;


		#region Constructors

		/// <summary>
		/// Construct a SampleForm.
		/// </summary>
		public Form()
		{
			InitializeComponent();
		}

		#endregion

		#region Event Handling

		/// <summary>
		/// Allocate GL resources or GL states.
		/// </summary>
		/// <param name="sender">
		/// The <see cref="object"/> that has rasied the event.
		/// </param>
		/// <param name="e">
		/// The <see cref="GlControlEventArgs"/> that specifies the event arguments.
		/// </param>
		private void RenderControl_ContextCreated(object sender, GlControlEventArgs e)
		{
			GlControl glControl = (GlControl)sender;

			// GL Debugging
			if (Gl.CurrentExtensions != null && Gl.CurrentExtensions.DebugOutput_ARB) {
				Gl.DebugMessageCallback(GLDebugProc, IntPtr.Zero);
				Gl.DebugMessageControl(DebugSource.DontCare, DebugType.DontCare, DebugSeverity.DontCare, 0, null, true);
			}

            this.triangleCore = new TriangleCore();
	
			// Uses multisampling, if available
			if (Gl.CurrentVersion != null && Gl.CurrentVersion.Api == KhronosVersion.ApiGl && glControl.MultisampleBits > 0)
				Gl.Enable(EnableCap.Multisample);
		}

		private void RenderControl_Render(object sender, GlControlEventArgs e)
		{
			// Common GL commands
			Control senderControl = (Control)sender;

			Gl.Viewport(0, 0, senderControl.ClientSize.Width, senderControl.ClientSize.Height);
			Gl.Clear(ClearBufferMask.ColorBufferBit);

			triangleCore.Render();
		}

		private void RenderControl_ContextUpdate(object sender, GlControlEventArgs e)
		{

		}

		private void RenderControl_ContextDestroying(object sender, GlControlEventArgs e)
		{
            if (triangleCore != null)
            {
                this.triangleCore.Dispose();
            }
		}

		private static void GLDebugProc(DebugSource source, DebugType type, uint id, DebugSeverity severity, int length, IntPtr message, IntPtr userParam)
		{
			string strMessage;

			// Decode message string
			unsafe {
				strMessage = Encoding.ASCII.GetString((byte*)message.ToPointer(), length);
			}

			Console.WriteLine($"{source}, {type}, {severity}: {strMessage}");
		}

		#endregion
	}
}
