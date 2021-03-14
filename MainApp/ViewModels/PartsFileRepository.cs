using AutoMapper;
using Hrsw.XiAnPro.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace MainApp.ViewModels
{
    public class PartsFileRepository
    {
        public static ObservableCollection<Part> LoadParts()
        {
            string fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Parts.bin");

            // 文件不存在返回空料盘集合
            if (!File.Exists(fileName))
                return new ObservableCollection<Part>();

            ObservableCollection<Part_s> parts_s = null;

            BinaryFormatter bf = new BinaryFormatter();
            using (var sm = File.OpenRead(fileName))
            {
                parts_s = bf.Deserialize(sm) as ObservableCollection<Part_s>;
            }

            if (parts_s == null)
                return new ObservableCollection<Part>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Part_s, Part>();
            });
            var mapper = config.CreateMapper();
            var parts = mapper.Map<ObservableCollection<Part>>(parts_s);
            if (parts != null)
            {
                foreach (var part in parts)
                {
                    part.Flag = 0;
                    part.Placed = false;
                    part.SlotNb = -1;
                    part.UseCmmNo = -1;
                    part.Status = PartStatus.PS_Idle;
                    part.TrayNb = -1;
                }
                return parts;
            }
            else
                return new ObservableCollection<Part>();
        }

        public static void UpdateParts(ObservableCollection<Part> parts)
        {
            string fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Parts.bin");

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Part, Part_s>();
            });
            var mapper = config.CreateMapper();
            var parts_s = mapper.Map<ObservableCollection<Part_s>>(parts);

            BinaryFormatter bf = new BinaryFormatter();
            using (var sm = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                bf.Serialize(sm, parts_s);
            }
        }
    }
}
