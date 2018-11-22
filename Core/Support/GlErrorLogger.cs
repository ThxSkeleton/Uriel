using OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uriel
{
    /// <summary>
    /// Use: GlErrorLogger.Check();
    /// </summary>
    public class GlErrorLogger
    {
        public static void Check()
        {
            try
            {
                Gl.CheckErrors();
            }
            catch (Exception e)
            {
                StaticLogger.Logger.Error(e);
            }
        }
    }
}
