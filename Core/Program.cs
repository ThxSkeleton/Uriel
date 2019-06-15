using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

using Khronos;

namespace Uriel
{
    static class Program
	{
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            UrielConfiguration editorMode = new UrielConfiguration()
            {
                Length = 900,
                Height = 900,
                LockSize = true,
                LoggingEnabled = true,
                WorkflowMode = UrielWorkflowMode.EditorMode,

                WatchDirectory = new List<string>() { @"Z:\ShaderStore\", (Directory.GetParent(Assembly.GetExecutingAssembly().Location).FullName) + "\\ExampleShaders" }
            };

            UrielConfiguration movieMode = new UrielConfiguration()
            {
                Length = 900,
                Height = 900,
                LockSize = true,
                LoggingEnabled = true,
                WorkflowMode = UrielWorkflowMode.MovieMode,
                MovieModeShaderFileName = @"Z:\Uriel\Core\bin\Debug\ExampleShaders\TexTest.glsl",
                WatchDirectory = new List<string>() 
            };

            //UrielConfiguration config = editorMode;
            UrielConfiguration config = movieMode;

            StaticLogger.Create(config.LoggingEnabled);

            StaticLogger.Logger.Info("Starting");

            KhronosApi.Log += delegate(object sender, KhronosLogEventArgs e) {
                StaticLogger.Logger.Info(e.ToString());
			};
			KhronosApi.LogEnabled = false;

            Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new UrielForm(config));
		}
	}
}
