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
    public class TraysOfRackDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate DefaultDataTemplate { get; set; }
        public DataTemplate EmptyDataTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            Tray tray = (Tray)item;
            if (tray.Status == TrayStatus.TS_Empty)
            {
                return EmptyDataTemplate;
            }
            else
            {
                return DefaultDataTemplate;
            }
        }
    }
}
