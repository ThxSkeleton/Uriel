using Khronos;
using OpenGL;
using System;
using System.Text;
using System.Windows.Forms;

namespace Uriel
{
    public class UrielForm : Form
    {
        private System.ComponentModel.IContainer components = null;
        private GlControl RenderControl;
        private TriangleCore triangleCore;

        public UrielForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.RenderControl = new OpenGL.GlControl();
            this.SuspendLayout();
            // 
            // RenderControl
            // 

            RenderControlConfiguration.Configure(RenderControl);

            this.RenderControl.ContextCreated += new EventHandler<GlControlEventArgs>(this.RenderControl_ContextCreated);
            this.RenderControl.ContextDestroying += new EventHandler<GlControlEventArgs>(this.RenderControl_ContextDestroying);
            this.RenderControl.Render += new EventHandler<GlControlEventArgs>(this.RenderControl_Render);
            this.RenderControl.ContextUpdate += new EventHandler<GlControlEventArgs>(this.RenderControl_ContextUpdate);
            // 
            // SampleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1080, 768);
            this.Controls.Add(this.RenderControl);
            this.Name = "Uriel SampleForm";
            this.Text = "Uriel";
            this.ResumeLayout(false);
        }

        private void RenderControl_ContextCreated(object sender, GlControlEventArgs e)
        {
            GlControl glControl = (GlControl)sender;

            // GL Debugging
            if (Gl.CurrentExtensions != null && Gl.CurrentExtensions.DebugOutput_ARB)
            {
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
            unsafe
            {
                strMessage = Encoding.ASCII.GetString((byte*)message.ToPointer(), length);
            }

            Console.WriteLine($"{source}, {type}, {severity}: {strMessage}");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}