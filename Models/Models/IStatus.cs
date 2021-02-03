using Hrsw.XiAnPro.Models;
using Hrsw.XiAnPro.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrsw.XiAnPro.Models
{
    public interface IStatus
    {
        AAStatus Status { get; set; }
    }
}
