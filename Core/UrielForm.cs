using Khronos;
using OpenGL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Uriel.DataTypes;
using Uriel.ShaderTypes;
using Uriel.Support;

namespace Uriel
{
    public class UrielForm : Form
    {
        private GlControl RenderControl;
        private StatusStrip StatusStrip;
        private readonly UrielConfiguration configuration;
        private FrameTracker FrameTracker;
        private ToolStripStatusLabel FpsLabel;
        private ToolStripStatusLabel U_timeLabel;

        private Panel LeftPanel;
        private ListBox ShaderSelector;

        public ShaderBlob BadShader;

        public BindingList<ShaderBlob> ShaderBlobs = new BindingList<ShaderBlob>();

        public ShaderBlob Previous;
        private TextBox ErrorBox;

        private const int LEFT_PANEL_WIDTH = 300;

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
            this.SuspendLayout();
            // 
            // RenderControl
            // 

            this.RenderControl = new OpenGL.GlControl();

            RenderControl.Animation = true;
            RenderControl.AnimationTimer = false;
            RenderControl.BackColor = System.Drawing.Color.DimGray;
            RenderControl.ColorBits = ((uint)(24u));
            RenderControl.DepthBits = ((uint)(0u));
            RenderControl.Dock = System.Windows.Forms.DockStyle.Fill;
            RenderControl.Location = new System.Drawing.Point(0, 0);
            RenderControl.MultisampleBits = ((uint)(0u));
            RenderControl.Name = "RenderControl";
            RenderControl.Size = new System.Drawing.Size(configuration.Length, configuration.Height);
            RenderControl.StencilBits = ((uint)(0u));
            RenderControl.TabIndex = 0;

            // Label

            this.StatusStrip = new StatusStrip();

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

            // ListBar

            LeftPanel = new Panel();
            LeftPanel.Dock = DockStyle.Left;
            LeftPanel.Width = LEFT_PANEL_WIDTH;

            ShaderSelector = new ListBox();

            LeftPanel.Controls.Add(ShaderSelector);

            ShaderSelector.Width = LEFT_PANEL_WIDTH;
            ShaderSelector.Height = 400;
            ShaderSelector.DataSource = this.ShaderBlobs;
            ShaderSelector.DisplayMember = "DisplayName";

            ErrorBox = new TextBox();

            LeftPanel.Controls.Add(ErrorBox);

            ErrorBox.Multiline = true;
            ErrorBox.ReadOnly = true;

            ErrorBox.Dock = DockStyle.Bottom;
            ErrorBox.Width = LEFT_PANEL_WIDTH;
            ErrorBox.Height = 400;

            FrameTracker = new FrameTracker();


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
            this.ClientSize = new System.Drawing.Size(this.configuration.Length + 200, this.configuration.Height);
            this.Controls.Add(this.RenderControl);
            this.Controls.Add(this.StatusStrip);
            this.Controls.Add(this.LeftPanel);
            this.Name = "Uriel SampleForm";
            this.Text = "Uriel";
            this.ResumeLayout(false);
        }

        private void RefreshShaderSelector(object sender, EventArgs e)
        {
            ShaderSelector.DataSource = ShaderBlobs;
            ShaderSelector.Refresh();
            ShaderSelector.SetSelected(ShaderBlobs.Count() - 1, true);
            ShaderSelector.Refresh();
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
            // Uses multisampling, if available
            if (Gl.CurrentVersion != null && Gl.CurrentVersion.Api == KhronosVersion.ApiGl && multisampleBits > 0)
            {
                Gl.Enable(EnableCap.Multisample);
            }

            var BadShader_Args = new ShaderCreationArguments()
            {
                Type = ShaderBlobType.Time,
                DisplayName = "BadShader",
                FragmentShaderSource = new List<string>(BuiltInShaderSource.BadShader),
            };

            BadShader = BuildProgram(BadShader_Args);

            foreach (var shaderBlob in ShaderZoo.BuildZoo())
            {
                ShaderBlobs.Add(BuildProgram(shaderBlob));
            }

            GlErrorLogger.Check();

            StartTime = DateTime.UtcNow;
        }

        private ShaderBlob BuildProgram(ShaderCreationArguments args)
        {
            try
            {
                PngTexture texture = null;

                if (args.Type.UseTexture)
                {
                    texture = new PngTexture(args.TexturePath);
                    texture.Load();
                    texture.Create();
                    GlErrorLogger.Check();

                    // Do I even need this?
                    Gl.BindTexture(TextureTarget.Texture2d, texture.TextureName);
                    GlErrorLogger.Check();
                }

                var vertexSource = VertexShaderSource.VertexSourceLookup(args.Type.VertexFormat, args.Type.VertexShaderVersion);

                var _Program = new ShaderProgram(args.FragmentShaderSource, vertexSource, args.Type.FragmentShaderUniformType, args.Type.VertexFormat);
                GlErrorLogger.Check();

                _Program.Link();
                GlErrorLogger.Check();

                IVertexArray _VertexArray = BuildVertexArray(_Program.VertexLocations, args); 

                GlErrorLogger.Check();

                return new ShaderBlob()
                {
                    DisplayName = args.DisplayName,
                    CreationArguments = args,
                    TreatAsGood = true,
                    Program = _Program,
                    VertexArray = _VertexArray,
                    TextureName = !args.Type.UseTexture ? 0 : texture.TextureName,
                };
            }
            catch (Exception e)
            {
                StaticLogger.Logger.ErrorFormat("{1} - Shader Error {0}", args.DisplayName, e.ToString());

                return new ShaderBlob()
                {
                    DisplayName = args.DisplayName,
                    CreationArguments = BadShader.CreationArguments,
                    TreatAsGood = false,
                    ErrorMessage = e.ToString(),
                    Program = BadShader.Program,
                    VertexArray = BadShader.VertexArray
                };
            }
        }

        private IVertexArray BuildVertexArray(VertexLocations vertexLocations, ShaderCreationArguments args)
        {
            if (!args.Type.UseIndexing)
            {
                return new VertexArray(vertexLocations, args.Type, RawVertexData.NonIndexed);
            }
            else
            {
                return new VertexArray(vertexLocations, args.Type, RawVertexData.Indexed);
            }
        }

        private void Destroy()
        {

        }

        private void Render(int width, int height)
        {
            if (!ShaderStore.Shaders.IsEmpty)
            {
                StaticLogger.Logger.Info("New Shader Detected.");

                ShaderInfo newShader;
                // don't bother checking success.
                ShaderStore.Shaders.TryPop(out newShader);

                StaticLogger.Logger.Info(newShader);

                IEnumerable<string> shaderStringBase = newShader.Source.Trim().Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
                IEnumerable<string> shaderStringPostPendNewlines = shaderStringBase.Select(x => x + "\n");

                var shaderActual = ShaderToyConverter.TranslateShader(shaderStringPostPendNewlines.ToList());

                var newShaderArgs = new ShaderCreationArguments()
                {
                    Type = ShaderBlobType.Standard,
                    DisplayName = newShader.ConvenientName(),
                    FragmentShaderSource = shaderActual,
                };

                var newBlob = BuildProgram(newShaderArgs);

                ErrorBox.Text = newBlob.ErrorMessage;
                ErrorBox.Refresh();

                ShaderBlobs.Add(newBlob);

                RefreshShaderSelector(null, null);
            }

            ShaderBlob currentShader = (ShaderBlob)ShaderSelector.SelectedItem;

            if (currentShader != Previous)
            {
                this.StartTime = DateTime.UtcNow;
                ErrorBox.Text = currentShader.ErrorMessage;
                ErrorBox.Refresh();
            }

            this.Previous = currentShader;

            this.FrameTracker.StartFrame();

            Gl.Viewport(0, 0, width, height);
            Gl.Clear(ClearBufferMask.ColorBufferBit);

            // Select the program for drawing
            Gl.UseProgram(currentShader.Program.ProgramName);

            double time = (DateTime.UtcNow - StartTime).TotalSeconds;

            SetUTime(time);

            Vertex2f resolution = new Vertex2f(configuration.Length, configuration.Height);

            Gl.UseProgram(currentShader.Program.ProgramName);

            SetUniforms(currentShader.Program.UniformLocations, time, resolution);

            if (currentShader.CreationArguments.Type.UseTexture)
            {
                Gl.BindTexture(TextureTarget.Texture2d, currentShader.TextureName);
            }

            // Use the vertex array
            Gl.BindVertexArray(currentShader.VertexArray.ArrayName);
            GlErrorLogger.Check();
            // Draw triangle
            if (currentShader.CreationArguments.Type.UseIndexing)
            {
                Gl.DrawElements(PrimitiveType.Triangles, currentShader.VertexArray.Count, DrawElementsType.UnsignedInt, IntPtr.Zero);
            }
            else
            {
                Gl.DrawArrays(PrimitiveType.Triangles, 0, currentShader.VertexArray.Count);
            }
            GlErrorLogger.Check();

            this.FrameTracker.EndFrame();
            StatusStrip_Update();
        }

        private void SetUniforms(UniformLocations uniforms, double time, Vertex2f resolution)
        {
            if (LocationValidation.Enabled(uniforms.Location_iTime))
            {
                Gl.Uniform1f<float>(uniforms.Location_iTime, 1, (float)time);
            }

            if (LocationValidation.Enabled(uniforms.Location_iResolution))
            {
                Gl.Uniform2f(uniforms.Location_iResolution, 1, resolution);
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}