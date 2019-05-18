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
        private GlControl RenderControl;
        private StatusStrip StatusStrip;
        private readonly UrielConfiguration configuration;
        private FrameTracker FrameTracker;

        private ToolStripStatusLabel FpsLabel;
        private ToolStripStatusLabel U_timeLabel;
        private ToolStripStatusLabel KeyState;

        private RenderLoop renderLoop;
        private ShaderBuilder builder;
        private readonly ShaderFileWatcher watcher;

        private Panel LeftPanel;
        private ListBox ShaderSelector;

        public BindingList<ShaderBlob> ShaderBlobs = new BindingList<ShaderBlob>();

        public KeyPressListener listener = new KeyPressListener();
        public TotalKeyState tks = new TotalKeyState();
        public KeyInterpreter ki = new KeyInterpreter();

        public ShaderBlob Previous;
        private TextBox ErrorBox;

        private const int LEFT_PANEL_WIDTH = 300;

        public DateTime StartTime { get; private set; }

        public UrielForm(UrielConfiguration configuration)
        {
            this.configuration = configuration;
            this.watcher = new ShaderFileWatcher(configuration.WatchDirectory);
            watcher.Run();
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

            RenderControl.KeyDown += listener.KeyDown;
            RenderControl.KeyUp += listener.KeyUp;

            // Label

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
            FpsLabel.Size = new System.Drawing.Size(109, 17);
            FpsLabel.Text = "---";

            U_timeLabel.Name = "u_timeLabel";
            U_timeLabel.Size = new System.Drawing.Size(109, 17);
            U_timeLabel.Text = "---";

            KeyState.Name = "KeyState";
            KeyState.Size = new System.Drawing.Size(109, 17);
            KeyState.Text = "---";

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
            this.Name = "Uriel";
            this.Text = "Uriel";
            this.ResumeLayout(false);

            this.renderLoop = new RenderLoop(this.configuration.Length, this.configuration.Height);
            this.builder = new ShaderBuilder(ShaderZoo.BadShaderArguments());
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
            UpdateFPSLabel(this.FrameTracker.averageFramePerSecond);
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

            watcher.LoadAll();

            foreach (var shaderBlob in ShaderZoo.BuildZoo())
            {
                ShaderBlobs.Add(builder.BuildProgram(shaderBlob));
            }

            GlErrorLogger.Check();

            StartTime = DateTime.UtcNow;
        }

        private void Destroy()
        {

        }

        private void Loop()
        {
            ShaderBlob currentShader;

            var possibleNewShader = watcher.GetNew();
            if (possibleNewShader != null)
            {
                var newShader = this.builder.BuildProgram(possibleNewShader);
                this.ShaderBlobs.Add(newShader);
                ShaderSelector.SelectedIndex = ShaderSelector.Items.Count - 1;
            }

            currentShader = (ShaderBlob)ShaderSelector.SelectedItem;


            if (currentShader != Previous)
            {
                this.StartTime = DateTime.UtcNow;
                ErrorBox.Text = currentShader.ErrorMessage;
                ErrorBox.Refresh();
            }

            this.Previous = currentShader;

            this.FrameTracker.StartFrame();

            double time = (DateTime.UtcNow - StartTime).TotalSeconds;

            UpdateTimeLabel(time);
            UpdateKeys();

            Vertex2f resolution = new Vertex2f(configuration.Length, configuration.Height);

            UniformValues uniforms = new UniformValues()
            {
                Resolution = resolution,
                Time = time
            };

            renderLoop.Render(currentShader, uniforms);

            this.FrameTracker.EndFrame();
            StatusStrip_Update();
        }

        private void UpdateKeys()
        {
            this.tks = this.ki.Update(this.tks, this.listener.CurrentKeys);
            this.UpdateKeyStateLabel(this.tks);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}