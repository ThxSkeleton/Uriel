﻿using Khronos;
using OpenGL;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Uriel.DataTypes;

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

        public DateTime StartTime { get; private set; }

        public UrielForm(UrielConfiguration configuration)
        {

            this.configuration = configuration;
            InitializeComponent();
        }

        private void SetFPS(float f)
        {
            FpsLabel.Text = "FPS: " + f.ToString("0.00");
        }

        private void SetUTime(double f)
        {
            U_timeLabel.Text = "Time: " + f.ToString("0.0000");
        }

        private void InitializeComponent()
        {
            FrameTracker = new FrameTracker();

            this.RenderControl = new OpenGL.GlControl();
            this.StatusStrip = new StatusStrip();
            this.SuspendLayout();
            // 
            // RenderControl
            // 

            RenderControlConfiguration.Configure(RenderControl, configuration);

            StatusStrip.BackColor = System.Drawing.Color.LightGray;
            StatusStrip.Dock = System.Windows.Forms.DockStyle.Bottom;
            StatusStrip.Name = "StatusStrip";
            StatusStrip.SizingGrip = false;

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

            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            // Set the MaximizeBox to false to remove the maximize box.
            this.MaximizeBox = false;

            // Set the MinimizeBox to false to remove the minimize box.
            this.MinimizeBox = false;

            // Set the start position of the form to the center of the screen.
            this.StartPosition = FormStartPosition.CenterScreen;

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

        private void StatusStrip_Update()
        {
            StatusStrip.Invalidate();
            SetFPS(this.FrameTracker.averageFramePerSecond);
            StatusStrip.Refresh();
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

            _Program = new ShaderProgram(new List<string>(_VertexSourceGL_Simplest), new List<string>(_FragmentSourceGL));
            _VertexArray = new IndexedVertexArray(_Program, _ArrayPosition, _ArrayIndex);

            _Program.Link();

            StartTime = DateTime.UtcNow;

            GlErrorLogger.Check();
        }

        private void Destroy()
        {

        }

        private void Render(int width, int height)
        {
            this.FrameTracker.StartFrame();

            Gl.Viewport(0, 0, width, height);
            Gl.Clear(ClearBufferMask.ColorBufferBit);

            //Matrix4x4f projection = Matrix4x4f.Ortho2D(-1.0f, +1.0f, -1.0f, +1.0f);
            //Matrix4x4f modelview = Matrix4x4f.Translated(-0.5f, -0.5f, 0.0f);

            // Select the program for drawing
            Gl.UseProgram(_Program.ProgramName);
            //// Set uniform state
            //Gl.UniformMatrix4f(_Program.LocationMVP, 1, false, projection * modelview);

            double time = (DateTime.UtcNow - StartTime).TotalSeconds;

            SetUTime(time);

            Gl.Uniform1f<float>(_Program.LocationU_Time, 1, (float) time);


            Vertex2f resolution = new Vertex2f(configuration.Length, configuration.Height);

            Gl.Uniform2f(_Program.LocationResolution, 1, resolution);

            // Use the vertex array
            Gl.BindVertexArray(_VertexArray.ArrayName);
            GlErrorLogger.Check();
            // Draw triangle
            // Note: vertex attributes are streamed from GPU memory
            Gl.DrawElements(PrimitiveType.Triangles, _VertexArray.Count, DrawElementsType.UnsignedInt, IntPtr.Zero);
            GlErrorLogger.Check();

            this.FrameTracker.EndFrame();
            StatusStrip_Update();
        }

        #region Common Data

        /// <summary>
        /// Vertex position array.
        /// </summary>
        private static readonly float[] _ArrayPosition = new float[] {
            -1.0f, -1.0f,
            1.0f, -1.0f,
            -1.0f, 1.0f,
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

        private readonly string[] _VertexSourceGL_Simplest = {
            "#version 150 compatibility\n",
            "in vec2 aPosition;\n",
            "void main() {\n",
            "	gl_Position = vec4(aPosition, 0.0, 1.0);\n",
            "}\n"
        };

        private readonly string[] _FragmentSourceGL_OG = {
            "#version 150 compatibility\n",
            "in vec3 vColor;\n",
            "void main() {\n",
            "	gl_FragColor = vec4(vColor, 1.0);\n",
            "}\n"
        };

        private string[] _FragmentSourceGL = {
            "#version 150 compatibility\n",
            "uniform float u_time;\n",
            "uniform vec2 resolution;\n",
            "void main() {\n",
            "   vec2 normalized = gl_FragCoord.xy/resolution;\n",
            "   vec3 col = 0.5 + 0.5*cos(u_time+normalized.xyx+vec3(0,2,4));\n",
            "	gl_FragColor = vec4(col, 1.0);\n",
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