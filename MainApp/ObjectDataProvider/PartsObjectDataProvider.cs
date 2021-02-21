using Hrsw.XiAnPro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainApp.ObjectDataProvider
{
    public class PartsObjectDataProvider
    {
        public IEnumerable<string> GetPartCategories(IEnumerable<Part> parts)
        {
            var cat = parts.Select(p => p.Category).Distinct().Select(r => r.ToString()).ToList();
            if(cat == null)
            {
                cat = new List<string>();
            }
            cat.Add("All");
            return cat;
        }

        public IEnumerable<Part> GetPartsByCategory(string cat, IEnumerable<Part> parts)
        {
            if (string.Compare(cat, "All", true) == 0)
                return parts;
            else
            {
                int category = int.Parse(cat);
                var ps = parts.Where(p => p.Category == category);
                return ps;
            }
        }
    }
}
