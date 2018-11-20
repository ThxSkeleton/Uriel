﻿using OpenGL;

namespace Uriel
{
    public class RenderControlConfiguration
    {
        public static void Configure(GlControl renderControl)
        {
            renderControl.Animation = true;
            renderControl.AnimationTimer = false;
            renderControl.BackColor = System.Drawing.Color.DimGray;
            renderControl.ColorBits = ((uint)(24u));
            renderControl.DepthBits = ((uint)(0u));
            renderControl.Dock = System.Windows.Forms.DockStyle.Fill;
            renderControl.Location = new System.Drawing.Point(0, 0);
            renderControl.MultisampleBits = ((uint)(0u));
            renderControl.Name = "RenderControl";
            renderControl.Size = new System.Drawing.Size(1080, 768);
            renderControl.StencilBits = ((uint)(0u));
            renderControl.TabIndex = 0;
        }

    }
}
