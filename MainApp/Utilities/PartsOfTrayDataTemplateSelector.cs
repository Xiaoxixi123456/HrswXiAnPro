using Hrsw.XiAnPro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MainApp.Utilities
{
    public class PartsOfTrayDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate DefaultPartDataTemplate { get; set; }
        public DataTemplate EmptyPartDataTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            Part part = (Part)item;
            if (part.Status == PartStatus.PS_Empty)
            {
                return EmptyPartDataTemplate;
            }
            else
            {
                return DefaultPartDataTemplate;
            }
        }
    }
}
