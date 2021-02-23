using Hrsw.XiAnPro.Models;
using Hrsw.XiAnPro.RepositoryContracts;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainApp.ViewModels
{
    public partial class MainViewModel : BindableBase
    {
        private IPartsRepository _partsRepository;
        private async void ReadParts()
        {
            var parts = await _partsRepository.GetItemsAsync();
            foreach (var item in parts)
            {
                item.SlotNb = -1;
                item.Status = PartStatus.PS_Idle;
                item.TrayNb = -1;
                Parts.Add(item);
            }
        }
    }
}
