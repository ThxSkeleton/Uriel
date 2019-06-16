using Khronos;
using OpenGL;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Uriel.DataTypes;
using Uriel.GLInteractions;
using Uriel.KeyPress;
using Uriel.ShaderTypes;
using Uriel.Support;

namespace Uriel
{
    public class UrielForm : Form
    {
        private readonly UrielConfiguration configuration;

        // Winforms Components
        private GlControl RenderControl;

        private Panel LeftPanel;
        private ListBox ShaderSelector;
        private TextBox ErrorBox;
        private StatusStrip StatusStrip;
        private ToolStripStatusLabel FpsLabel;
        private ToolStripStatusLabel U_timeLabel;
        private ToolStripStatusLabel KeyState;

        // Core Components.
        private readonly RenderLoop renderLoop;
        private readonly ShaderBuilder builder;
        private readonly ShaderFileWatcher watcher;
        private readonly FrameTracker FrameTracker;
        private readonly KeyPressListener listener = new KeyPressListener();
        private readonly KeyInterpreter ki = new KeyInterpreter();

        // Public list
        public readonly BindingList<ShaderBlob> ShaderBlobs;

        // Current State Data Objects
        private TotalKeyState tks = new TotalKeyState();
        private ShaderBlob CurrentShader;

        public DateTime StartTime { get; private set; }

        public UrielForm(UrielConfiguration configuration)
        {
            this.configuration = configuration;
            if (configuration.WorkflowMode != UrielWorkflowMode.MovieMode)
            {
                this.watcher = new ShaderFileWatcher(configuration.WatchDirectory);
                watcher.Run();
            }

            this.renderLoop = new RenderLoop();
            this.builder = new ShaderBuilder(ShaderZoo.BadShaderArguments());
            this.FrameTracker = new FrameTracker();
            this.ShaderBlobs = new BindingList<ShaderBlob>();

            this.listener = new KeyPressListener();
            this.tks = new TotalKeyState();
            this.ki = new KeyInterpreter();

            InitializeComponent();
        }

        private void UpdateFPSLabel(float f)
        {
            FpsLabel.Text = "FPS: " + f.ToString("0.00");
        }

        private void UpdateTimeLabel(double f)
        {
            U_timeLabel.Text = "Time: " + f.ToString("0.0000");
        }

        private void UpdateKeyStateLabel(TotalKeyState tks)
        {
            KeyState.Text = string.Format("TKS: [{0},{1},{2}] [{3}, {4}, {5}]", tks.Position.x, tks.Position.y, tks.Position.z, tks.Movement.x, tks.Movement.y, tks.Movement.z);
        }


        private const int LEFT_PANEL_WIDTH = 300;
        private const int LEFT_SUBPANEL_HEIGHT = 400;
        private const int StatusStripLabelWidth = 109;
        private const int StatusStripLabelHeight = 17;

        // What is this????
        private const int ClientSizeWidthBuffer = 200;

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // RenderControl
            // 


            this.RenderControl = BuildGLControl();
            this.Controls.Add(this.RenderControl);

            if (this.configuration.WorkflowMode == UrielWorkflowMode.EditorMode)
            {

                // Add keypress listeners to the RenderControl for Keystate Monitoring
                RenderControl.KeyDown += listener.KeyDown;
                RenderControl.KeyUp += listener.KeyUp;

                // StatusStrip

                this.StatusStrip = new StatusStrip();

                StatusStrip.BackColor = System.Drawing.Color.LightGray;
                StatusStrip.Dock = System.Windows.Forms.DockStyle.Bottom;
                StatusStrip.Name = "StatusStrip";
                StatusStrip.SizingGrip = false;
                FpsLabel = new System.Windows.Forms.ToolStripStatusLabel();
                U_timeLabel = new System.Windows.Forms.ToolStripStatusLabel();
                KeyState = new System.Windows.Forms.ToolStripStatusLabel();

                StatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                    FpsLabel,
                    new ToolStripSeparator(),
                    U_timeLabel,
                    new ToolStripSeparator(),
                    KeyState
                });

                FpsLabel.Name = "fpsLabel";
                FpsLabel.Size = new System.Drawing.Size(StatusStripLabelWidth, StatusStripLabelHeight);
                FpsLabel.Text = "---";

                U_timeLabel.Name = "u_timeLabel";
                U_timeLabel.Size = new System.Drawing.Size(StatusStripLabelWidth, StatusStripLabelHeight);
                U_timeLabel.Text = "---";

                KeyState.Name = "KeyState";
                KeyState.Size = new System.Drawing.Size(StatusStripLabelWidth, StatusStripLabelHeight);
                KeyState.Text = "---";

                this.Controls.Add(this.StatusStrip);


                // LeftPanel

                LeftPanel = new Panel();
                LeftPanel.Dock = DockStyle.Left;
                LeftPanel.Width = LEFT_PANEL_WIDTH;

                ShaderSelector = new ListBox();

                LeftPanel.Controls.Add(ShaderSelector);

                ShaderSelector.Width = LEFT_PANEL_WIDTH;
                ShaderSelector.Height = LEFT_SUBPANEL_HEIGHT;
                ShaderSelector.DataSource = this.ShaderBlobs;
                ShaderSelector.DisplayMember = "DisplayName";

                ErrorBox = new TextBox();

                ErrorBox.Multiline = true;
                ErrorBox.ReadOnly = true;

                ErrorBox.Dock = DockStyle.Bottom;
                ErrorBox.Width = LEFT_PANEL_WIDTH;
                ErrorBox.Height = LEFT_SUBPANEL_HEIGHT;

                LeftPanel.Controls.Add(ErrorBox);

                this.Controls.Add(this.LeftPanel);
            }

            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            // Set the MaximizeBox to false to remove the maximize box.
            this.MaximizeBox = true;
            this.Resize += new EventHandler(UrielForm_Resize);

            // Set the MinimizeBox to false to remove the minimize box.
            this.MinimizeBox = true;

            // Set the start position of the form to the center of the screen.
            this.StartPosition = FormStartPosition.CenterScreen;

            // 
            // The Uriel Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.None;

            if (this.configuration.WorkflowMode == UrielWorkflowMode.EditorMode)
            {
                this.ClientSize = new System.Drawing.Size(this.configuration.ViewPortLength + LEFT_PANEL_WIDTH, this.configuration.ViewPortHeight);
            }
            else
            {
                this.ClientSize = new System.Drawing.Size(this.configuration.ViewPortLength, this.configuration.ViewPortHeight);
            }

            this.Name = "Uriel";
            this.Text = String.Format("Uriel - {0}", this.configuration.WorkflowMode);

            this.ResumeLayout(false); // ?
        }

        private GlControl BuildGLControl()
        {
            StaticLogger.Logger.Debug("Building RenderControl GlControl");

            GlControl newRenderControl = new OpenGL.GlControl();

            newRenderControl.Animation = true;
            newRenderControl.AnimationTimer = false;
            newRenderControl.BackColor = System.Drawing.Color.DimGray;
            newRenderControl.ColorBits = ((uint)(24u));
            newRenderControl.DepthBits = ((uint)(0u));
            newRenderControl.Dock = System.Windows.Forms.DockStyle.Fill;
            newRenderControl.Location = new System.Drawing.Point(0, 0);
            newRenderControl.MultisampleBits = ((uint)(0u));
            newRenderControl.Name = "RenderControl";
            newRenderControl.Size = this.Size;
            newRenderControl.StencilBits = ((uint)(0u));
            newRenderControl.TabIndex = 0;

            newRenderControl.ContextCreated += new EventHandler<GlControlEventArgs>(this.RenderControl_ContextCreated);
            newRenderControl.ContextDestroying += new EventHandler<GlControlEventArgs>(this.RenderControl_ContextDestroying);
            newRenderControl.Render += new EventHandler<GlControlEventArgs>(this.RenderControl_Render);
            newRenderControl.ContextUpdate += new EventHandler<GlControlEventArgs>(this.RenderControl_ContextUpdate);

            StaticLogger.Logger.Debug("Done building RenderControl GlControl");

            return newRenderControl;
        }

        private void RegenerateGlControl()
        {
            this.SuspendLayout();
            this.RenderControl.Animation = false;
            this.Controls.Remove(this.RenderControl);
            this.RenderControl = BuildGLControl();
            this.Controls.Add(this.RenderControl);
            this.ResumeLayout();
        }

        private void RefreshShaderSelector(object sender, EventArgs e)
        {
            ShaderSelector.DataSource = ShaderBlobs;
            ShaderSelector.Refresh();
            ShaderSelector.SetSelected(ShaderBlobs.Count() - 1, true);
            ShaderSelector.Refresh();
        }

        private void StatusStrip_Update(double time)
        {
            StatusStrip.Invalidate();
            UpdateFPSLabel(this.FrameTracker.averageFramePerSecond);
            UpdateKeyStateLabel(this.tks);
            UpdateTimeLabel(time);
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
            Loop();
        }

        private void RenderControl_ContextUpdate(object sender, GlControlEventArgs e)
        {
        }

        private void RenderControl_ContextDestroying(object sender, GlControlEventArgs e)
        {
            Destroy();
        }

        private void UrielForm_Resize(object sender, EventArgs e)
        {
        }

        #endregion

        private void ContextCreated(uint multisampleBits)
        {
            GlErrorLogger.Check();
            // Uses multisampling, if available
            if (Gl.CurrentVersion != null && Gl.CurrentVersion.Api == KhronosVersion.ApiGl && multisampleBits > 0)
            {
                Gl.Enable(EnableCap.Multisample);
            }

            if (this.configuration.WorkflowMode == UrielWorkflowMode.EditorMode)
            {
                LoadInitialShaders();
            }
            else
            {
                this.CurrentShader = builder.BuildProgram(ShaderLoader.LoadFromFile(this.configuration.MovieModeShaderFileName));
            }

            StartTime = DateTime.UtcNow;
        }

        private void LoadInitialShaders()
        {
            StaticLogger.Logger.Debug("Loading Intitial Shaders.");

            watcher.LoadAll();

            foreach (var shaderBlob in ShaderZoo.BuildZoo())
            {
                ShaderBlobs.Add(builder.BuildProgram(shaderBlob));
            }

            StaticLogger.Logger.Debug("Done Loading Initial Shaders.");

            this.CurrentShader = ShaderBlobs.Last();
        }


        private void Destroy()
        {

        }

        private void CreateNewShaderAndSelect()
        {
            var possibleNewShader = watcher.GetNew();
            if (possibleNewShader != null)
            {
                var newShader = this.builder.BuildProgram(possibleNewShader);
                this.ShaderBlobs.Add(newShader);
                ShaderSelector.SelectedIndex = ShaderSelector.Items.Count - 1;
            }
        }

        private ShaderBlob LoadNewShaderFromSelection(ShaderBlob currentShader)
        {
            ShaderBlob selectedShader = (ShaderBlob)ShaderSelector.SelectedItem;

            if (currentShader != selectedShader)
            {
                this.StartTime = DateTime.UtcNow;
                ErrorBox.Text = currentShader.ErrorMessage;
                ErrorBox.Refresh();
            }

            return selectedShader;
        }


        private void Loop()
        {
            //if (FrameTracker.frameCount % 1000 == 0 && FrameTracker.frameCount != 0)
            //{
            //    StaticLogger.Logger.Debug("Regenerating.");
            //    RegenerateGlControl();
            //}

            if (this.configuration.WorkflowMode == UrielWorkflowMode.EditorMode)
            {
                CreateNewShaderAndSelect();
                this.CurrentShader = LoadNewShaderFromSelection(this.CurrentShader);
            }

            this.FrameTracker.StartFrame();

            double time = (DateTime.UtcNow - StartTime).TotalSeconds;

            if (this.CurrentShader.CreationArguments.Type.Uniforms.Contains(KnownFragmentShaderUniform.CursorPosition) ||
                this.CurrentShader.CreationArguments.Type.Uniforms.Contains(KnownFragmentShaderUniform.CursorMovement))
            {
                UpdateKeys();
            }

            Vertex2f resolution = new Vertex2f(this.Size.Width, this.Size.Height);

            UniformValues uniforms = new UniformValues()
            {
                Resolution = resolution,
                Time = time,
                CursorMovement = this.tks.Movement,
                CursorPosition = this.tks.Position
            };

            renderLoop.Render(this.CurrentShader, uniforms, this.RenderControl.Size);

            this.FrameTracker.EndFrame();
            if (this.configuration.WorkflowMode == UrielWorkflowMode.EditorMode)
            {
                StatusStrip_Update(time);
            }
        }

        private void UpdateKeys()
        {
            this.tks = this.ki.Update(this.tks, this.listener.CurrentKeys);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}