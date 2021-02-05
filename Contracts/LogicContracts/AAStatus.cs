using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrsw.XiAnPro.LogicContracts
{
    public class AActivityFlags
    {
        /// <summary>
        /// true是为重测，false为跳过
        /// </summary>
        public bool Next { get; set; }
        public static bool NextPart { get; set; }
        public static bool NextTray { get; set; }
        public static bool IsExit { get; set; }
    }
}
