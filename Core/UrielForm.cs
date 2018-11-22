using Khronos;
using OpenGL;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Uriel.DataTypes;
using log4net;

namespace Uriel
{
    public class UrielForm : Form
    {
        private GlControl RenderControl;
        private StatusStrip StatusStrip;
        private readonly UrielConfiguration configuration;
        ShaderProgram _Program;
        IndexedVertexArray _VertexArray;
        private FrameTracker FrameTracker;
        private ToolStripStatusLabel FpsLabel;
        private ToolStripStatusLabel U_timeLabel;

        public UrielForm(UrielConfiguration configuration)
        {

            this.configuration = configuration;
            InitializeComponent();
        }

        private void SetFPS(float f)
        {
            FpsLabel.Text = "FPS: " + f.ToString("0.00");
        }

        private void SetUTime(float f)
        {
            FpsLabel.Text = "Time: " + f.ToString("0.0000");
        }

        private void InitializeComponent()
        {
            this.RenderControl = new OpenGL.GlControl();
            this.StatusStrip = new StatusStrip();
            this.SuspendLayout();
            // 
            // RenderControl
            // 

            RenderControlConfiguration.Configure(RenderControl, configuration);

            StatusStrip.BackColor = System.Drawing.Color.DimGray;
            StatusStrip.Dock = System.Windows.Forms.DockStyle.Bottom;
            StatusStrip.Name = "StatusStrip";

            FpsLabel = new System.Windows.Forms.ToolStripStatusLabel();
            U_timeLabel = new System.Windows.Forms.ToolStripStatusLabel();

            StatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                FpsLabel,
                new ToolStripSeparator(),
                U_timeLabel
            });

            FpsLabel.Name = "fpsLabel";
            FpsLabel.Size = new System.Drawing.Size(109, 17);
            FpsLabel.Text = "---";

            U_timeLabel.Name = "u_timeLabel";
            U_timeLabel.Size = new System.Drawing.Size(109, 17);
            U_timeLabel.Text = "---";

            this.RenderControl.ContextCreated += new EventHandler<GlControlEventArgs>(this.RenderControl_ContextCreated);
            this.RenderControl.ContextDestroying += new EventHandler<GlControlEventArgs>(this.RenderControl_ContextDestroying);
            this.RenderControl.Render += new EventHandler<GlControlEventArgs>(this.RenderControl_Render);
            this.RenderControl.ContextUpdate += new EventHandler<GlControlEventArgs>(this.RenderControl_ContextUpdate);

            this.StatusStrip.Paint += new PaintEventHandler(this.StatusStrip_Update);

            // 
            // SampleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(this.configuration.Length, this.configuration.Height);
            this.Controls.Add(this.RenderControl);
            this.Controls.Add(this.StatusStrip);
            this.Name = "Uriel SampleForm";
            this.Text = "Uriel";
            this.ResumeLayout(false);
        }

        private void StatusStrip_Update(object sender, PaintEventArgs e)
        {
            SetFPS(this.FrameTracker.averageFramePerSecond);
        }

        #region winforms stuff

        private void RenderControl_ContextCreated(object sender, GlControlEventArgs e)
        {
            GlControl glControl = (GlControl)sender;

            ContextCreated(glControl.MultisampleBits);
        }

        private void RenderControl_Render(object sender, GlControlEventArgs e)
        {
            // Common GL commands
            Control senderControl = (Control)sender;

            Render(senderControl.ClientSize.Width, senderControl.ClientSize.Height);
        }

        private void RenderControl_ContextUpdate(object sender, GlControlEventArgs e)
        {
            // Nothing - what does this mean?
        }

        private void RenderControl_ContextDestroying(object sender, GlControlEventArgs e)
        {
            Destroy();
        }

        #endregion

        private void ContextCreated(uint multisampleBits)
        {
            // GL Debugging
            if (Gl.CurrentExtensions != null && Gl.CurrentExtensions.DebugOutput_ARB)
            {
                Gl.DebugMessageCallback(GLDebugProc, IntPtr.Zero);
                Gl.DebugMessageControl(DebugSource.DontCare, DebugType.DontCare, DebugSeverity.DontCare, 0, null, true);
            }

            // Uses multisampling, if available
            if (Gl.CurrentVersion != null && Gl.CurrentVersion.Api == KhronosVersion.ApiGl && multisampleBits > 0)
            {
                Gl.Enable(EnableCap.Multisample);
            }

            _Program = new ShaderProgram(new List<string>(_VertexSourceGL), new List<string>(_FragmentSourceGL));
            _VertexArray = new IndexedVertexArray(_Program, _ArrayPosition, _ArrayIndex);

            _Program.Link();

            GlErrorLogger.Check();

            FrameTracker = new FrameTracker();
        }

        private void Destroy()
        {

        }

        private void Render(int width, int height)
        {
            this.FrameTracker.StartFrame();

            Gl.Viewport(0, 0, width, height);
            Gl.Clear(ClearBufferMask.ColorBufferBit);

            Matrix4x4f projection = Matrix4x4f.Ortho2D(-1.0f, +1.0f, -1.0f, +1.0f);
            Matrix4x4f modelview = Matrix4x4f.Translated(-0.5f, -0.5f, 0.0f);

            // Select the program for drawing
            Gl.UseProgram(_Program.ProgramName);
            // Set uniform state
            Gl.UniformMatrix4f(_Program.LocationMVP, 1, false, projection * modelview);
            // Use the vertex array
            Gl.BindVertexArray(_VertexArray.ArrayName);
            GlErrorLogger.Check();
            // Draw triangle
            // Note: vertex attributes are streamed from GPU memory
            Gl.DrawElements(PrimitiveType.Triangles, _VertexArray.Count, DrawElementsType.UnsignedInt, IntPtr.Zero);
            GlErrorLogger.Check();

            this.FrameTracker.EndFrame();
        }

        #region Common Data

        /// <summary>
        /// Vertex position array.
        /// </summary>
        private static readonly float[] _ArrayPosition = new float[] {
            0.0f, 0.0f,
            1.0f, 0.0f,
            0.0f, 1.0f,
            1.0f, 1.0f
        };

        /// <summary>
        /// Vertex Index array.
        /// </summary>
        private static readonly uint[] _ArrayIndex = new uint[] {
            0, 1, 2,
            2, 1, 3
        };

        #endregion

        #region Shader Source

        private readonly string[] _VertexSourceGL_OG = {
            "#version 150 compatibility\n",
            "uniform mat4 uMVP;\n",
            "in vec2 aPosition;\n",
            "in vec3 aColor;\n",
            "void main() {\n",
            "	gl_Position = uMVP * vec4(aPosition, 0.0, 1.0);\n",
            "	vColor = aColor;\n",
            "}\n"
        };

        private readonly string[] _VertexSourceGL = {
            "#version 150 compatibility\n",
            "uniform mat4 uMVP;\n",
            "in vec2 aPosition;\n",
            "void main() {\n",
            "	gl_Position = uMVP * vec4(aPosition, 0.0, 1.0);\n",
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
            "void main() {\n",
            "   vec3 green = vec3(0.0f, 1.0f, 0.0f);\n",
            "	gl_FragColor = vec4(green, 1.0);\n",
            "}\n"
        };

        #endregion


        private void GLDebugProc(DebugSource source, DebugType type, uint id, DebugSeverity severity, int length, IntPtr message, IntPtr userParam)
        {
            string strMessage;

            // Decode message string
            unsafe
            {
                strMessage = Encoding.ASCII.GetString((byte*)message.ToPointer(), length);
            }

            StaticLogger.Logger.Info($"{source}, {type}, {severity}: {strMessage}");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}