using System.Collections.Generic;

namespace Uriel
{
    public class UrielConfiguration
    {
        public int Length { get; set; }

        public int Height { get; set; }

        public bool LockSize { get; set; }

        public bool LoggingEnabled { get; set; }

        public UrielWorkflowMode WorkflowMode { get; set; }

        public string MovieModeShaderFileName { get; set; }

        public List<string> WatchDirectory { get; set; }
    }
}
