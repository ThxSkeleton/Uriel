using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uriel
{
    public class StaticLogger
    {
        public static void Create(bool enabled)
        {
            if (enabled)
            {
                string declaringType = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString();
                Logger = LogManager.GetLogger(declaringType);
            }
            else
            {

                Logger = LogManager.GetLogger("SpecialLogger");
            }
        }

        public static ILog Logger { get; private set; }

    }
}
