using System;
using System.Collections.ObjectModel;
using Hrsw.XiAnPro.Models;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using AutoMapper;

namespace MainApp.ViewModels
{
    internal class TraysRepository
    {
        internal static ObservableCollection<Tray> LoadTrays()
        {
            string fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "trays.bin");

            // 文件不存在返回空料盘集合
            if (!File.Exists(fileName))
                return new ObservableCollection<Tray>();

            ObservableCollection<Tray_s> trays_s = null;

            BinaryFormatter bf = new BinaryFormatter();
            using (var sm = File.OpenRead(fileName))
            {
                trays_s = bf.Deserialize(sm) as ObservableCollection<Tray_s>;
            }

            if (trays_s == null)
                return new ObservableCollection<Tray>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Tray_s, Tray>();
            });
            var mapper = config.CreateMapper();
            var trays = mapper.Map<ObservableCollection<Tray>>(trays_s);
            if (trays != null)
            {
                foreach (var tray in trays)
                {
                    tray.MakePartPlaceholder();
                }
                return trays;
            }
            else
                return new ObservableCollection<Tray>();
        }

        internal static void UpdateTrays(ObservableCollection<Tray> trays)
        {
            string fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "trays.bin");

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Tray, Tray_s>();
            });
            var mapper = config.CreateMapper();
            var trays_s = mapper.Map<ObservableCollection<Tray_s>>(trays);

            BinaryFormatter bf = new BinaryFormatter();
            using (var sm = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                bf.Serialize(sm, trays_s);
            }
        }
    }
}