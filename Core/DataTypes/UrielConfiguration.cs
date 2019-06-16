using System.Collections.Generic;

namespace Uriel.DataTypes
{
    public class UrielConfiguration : PrettyPrintObject
    {
        public int ViewPortLength { get; set; }

        public int ViewPortHeight { get; set; }

        public bool LockSize { get; set; }

        public bool LoggingEnabled { get; set; }

        public UrielWorkflowMode WorkflowMode { get; set; }

        public string MovieModeShaderFileName { get; set; }

        public List<string> WatchDirectory { get; set; }
    }
}
