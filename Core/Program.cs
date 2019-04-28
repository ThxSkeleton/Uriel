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
            UrielConfiguration config = new UrielConfiguration()
            {
                Length = 900,
                Height = 900,
                LockSize = true,
                LoggingEnabled = true,

                WatchDirectory = new List<string>() { @"Z:\ShaderStore\", (Directory.GetParent(Assembly.GetExecutingAssembly().Location).FullName) + "\\ExampleShaders" }
            };

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
