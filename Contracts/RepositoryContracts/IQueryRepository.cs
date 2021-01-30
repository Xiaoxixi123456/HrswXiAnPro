using Hrsw.XiAnPro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrsw.XiAnPro.RepositoryContracts
{
    public interface IQueryRepository<T, in TKey> 
        where T : class
    {
        Task<IEnumerable<T>> GetItemsAsync();
    }
}
