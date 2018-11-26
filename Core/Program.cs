using System;
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

                WatchDirectory = @"Z:\ShaderStore\"
            };

            StaticLogger.Create(config.LoggingEnabled);

            StaticLogger.Logger.Info("Starting");

            KhronosApi.Log += delegate(object sender, KhronosLogEventArgs e) {
                StaticLogger.Logger.Info(e.ToString());
			};
			KhronosApi.LogEnabled = false;

            ShaderFileWatcher watcher = new ShaderFileWatcher(config.WatchDirectory);
            watcher.Run();

            Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new UrielForm(config));
		}
	}
}
