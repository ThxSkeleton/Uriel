namespace Uriel
{
	partial class Form
	{
		/// <summary>
		/// Variabile di progettazione necessaria.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Pulire le risorse in uso.
		/// </summary>
		/// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			this.RenderControl = new OpenGL.GlControl();
			this.SuspendLayout();
            // 
            // RenderControl
            // 

            RenderControlConfiguration.Configure(RenderControl);

            this.RenderControl.ContextCreated += new System.EventHandler<OpenGL.GlControlEventArgs>(this.RenderControl_ContextCreated);
            this.RenderControl.ContextDestroying += new System.EventHandler<OpenGL.GlControlEventArgs>(this.RenderControl_ContextDestroying);
            this.RenderControl.Render += new System.EventHandler<OpenGL.GlControlEventArgs>(this.RenderControl_Render);
            this.RenderControl.ContextUpdate += new System.EventHandler<OpenGL.GlControlEventArgs>(this.RenderControl_ContextUpdate);
            // 
            // SampleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(1080, 768);
			this.Controls.Add(this.RenderControl);
			this.Name = "Uriel SampleForm";
			this.Text = "Uriel";
			this.ResumeLayout(false);
		}

		private OpenGL.GlControl RenderControl;
	}
}

