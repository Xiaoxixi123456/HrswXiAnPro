using AutoMapper;
using Hrsw.XiAnPro.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace AutoMapTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Part part1 = new Part() { Name = "part1" };
            Part part2 = new Part() { Name = "part2" };
            Part part3 = new Part() { Name = "part3" };
            Part part4 = new Part() { Name = "part4" };
            Part part5 = new Part() { Name = "part5" };
            Tray tray = new Tray() { Name = "Tray1" };
            tray.Parts.Add(part1);
            tray.Parts.Add(part2);
            tray.Parts.Add(part3);
            tray.Parts.Add(part4);
            tray.Parts.Add(part5);

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Part, PartSer>();
                cfg.CreateMap<Tray, TraySer>();
            });

            IMapper mapper = config.CreateMapper();

            var traySer = mapper.Map<TraySer>(tray);

            Console.WriteLine(traySer.Name);
            Console.WriteLine(traySer.Parts.Count);

            foreach (var item in traySer.Parts)
            {
                Console.WriteLine(item.Name);
            }

            BinaryFormatter formatter = new BinaryFormatter();
            using (var stream = new FileStream("E:\\WorkTemp\\res.dat", FileMode.Create))
            {
                formatter.Serialize(stream, traySer);
            }

            Console.WriteLine("success...");
            Console.ReadLine();
        }
    }
}
