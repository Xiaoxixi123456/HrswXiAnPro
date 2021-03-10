using AutoMapper;
using Hrsw.XiAnPro.Utilities;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Xml.Serialization;

namespace Hrsw.XiAnPro.PCDmisService
{
    public class MeasProgManager : BindableBase
    {
        [Bindable]
        public ObservableCollection<MeasProg> MeasProgs { get; set; }
        [Bindable]
        public string SavFileName { get; set; } = "MeasProgs.xml";

        private static MeasProgManager _inst;
        public static MeasProgManager Inst => _inst ?? (_inst = new MeasProgManager()); 

        private MeasProgManager()
        {
            MeasProgs = new ObservableCollection<MeasProg>();
        }

        public void LoadPrograms()
        {
            try
            {
                string fullSavFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, SavFileName);
                if (!File.Exists(fullSavFileName))
                    return;
                List<MeasProg_s> measProgs_s = null;
                using (var sw = new FileStream(fullSavFileName, FileMode.Open, FileAccess.ReadWrite))
                {
                    XmlSerializer xs = new XmlSerializer(typeof(List<MeasProg_s>));
                    measProgs_s = xs.Deserialize(sw) as List<MeasProg_s>;
                }
                if (measProgs_s == null)
                    return;

                var config = new MapperConfiguration(cfg => cfg.CreateMap<MeasProg_s, MeasProg>());
                var mapper = config.CreateMapper();
                MeasProgs = mapper.Map<List<MeasProg_s>, ObservableCollection<MeasProg>>(measProgs_s);
            }
            catch
            {

            }

        }

        public void SavePrograms()
        {
            string fullSavFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, SavFileName);
            if (File.Exists(fullSavFileName))
            {
                File.Delete(fullSavFileName);
            }

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<MeasProg, MeasProg_s>();
            });

            var mapper = config.CreateMapper();
            var measProgs_s = mapper.Map<ObservableCollection<MeasProg>, List<MeasProg_s>>(MeasProgs);

            using (var sw = new FileStream(fullSavFileName, FileMode.Create))
            {
                XmlSerializer xs = new XmlSerializer(typeof(List<MeasProg_s>));
                xs.Serialize(sw, measProgs_s);
            }
        }

        public string GetMeasProg(int id)
        {
            MeasProg prg = null;
            string resultProg = string.Empty;
            try
            {
                prg = MeasProgs.Where(p => p.Id == id).Single();
                resultProg = prg.FileName;
            }
            catch (Exception)
            {
                throw new FileNotFoundException("测量程序未找到");
            }
            return resultProg;
        }

        public void AddMeasProg(int id, string fileName)
        {
            bool has = MeasProgs.Any(p => p.Id == id);
            if (has)
                return;
            MeasProgs.Add(new MeasProg() { Id = id, FileName = fileName });
        }

        public void DeleteMeasProg(int id)
        {
            MeasProg mp = null;
            try
            {
                mp = MeasProgs.Where(p => p.Id == id).Single();
                MeasProgs.Remove(mp);
            }
            catch (Exception)
            {
            }
        }
    }
}
