using AutoMapper;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PostSharpWpf
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public Person person { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            person = new Person() { Name = "aaaa", Age = 29 };
            DataContext = person;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            person.Name = "BBBB";
            person.Age = 19;
            person.BkColor = "Yellow";
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            person.Name = "CCCCCC";
            person.Age = 39;
            person.BkColor = "Green";
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            Task.Run(async () =>
            {
                await Task.Delay(2000);
                person.Name = "FFFFF";
                person.Age = 50;
                person.BkColor = "Blue";
            });
            //MessageBox.Show("s");
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            var cnf = new MapperConfiguration(cfg => cfg.CreateMap<Person, Person_s>());
            var mapper = cnf.CreateMapper();
            var person_dto = mapper.Map<Person_s>(person);
            BinaryFormatter bf = new BinaryFormatter();
            using (var sm = new FileStream("test.dat", FileMode.Create, FileAccess.Write))
            {
                bf.Serialize(sm, person_dto);
            }
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void button4_Click_1(object sender, RoutedEventArgs e)
        {
            Person_s person_dto = null;
            BinaryFormatter bf = new BinaryFormatter();
            using (var sm = new FileStream("test.dat", FileMode.Open, FileAccess.Read))
            {
               person_dto = bf.Deserialize(sm) as Person_s;
            }
            var cnf = new MapperConfiguration(cfg => cfg.CreateMap<Person_s, Person>());
            var mapper = cnf.CreateMapper();
            person = mapper.Map<Person>(person_dto);
            DataContext = person;
        }
    }
}
