using AutoMapper;
using Hrsw.XiAnPro.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace TestFormat
{
    class Program
    {
        static void Main(string[] args)
        {
            Tray tray = new Tray();
            for (int i = 0; i < 3; i++)
            {

            tray.Parts.Add(new Part());
            }
  
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Part, Part_s>();
                cfg.CreateMap<Tray, Tray_s>();
            });
            var mapper = config.CreateMapper();
            var tray_s = mapper.Map<Tray_s>(tray);
            BinaryFormatter bf = new BinaryFormatter();
            using (var sm = File.OpenWrite(@"Test.dat"))
            {
                bf.Serialize(sm, tray_s);
            }

            var config1 = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Part_s, Part>();
                cfg.CreateMap<Tray_s, Tray>();
            });
            var mapper1 = config1.CreateMapper();
            Tray tray1;
            using (var sm = File.OpenRead(@"Test.dat"))
            {
                var tray_s1 =  bf.Deserialize(sm) as Tray_s;
                tray1 = mapper1.Map<Tray>(tray_s1);
            }

            Console.WriteLine(tray1.Parts.Count);
            Console.ReadLine();
        }
    }
}
