using Hrsw.XiAnPro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrsw.XiAnPro.RepositoryContracts
{
    public interface IPartsRepository : IQueryRepository<Part, int>, IUpdateRepository<Part, int>
    {
    }
}
