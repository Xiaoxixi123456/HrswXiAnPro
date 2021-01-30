using Prism.Mvvm;

namespace WpfListUI
{
    public class Person : BindableBase
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                this.RaisePropertyChanged("Name");
            }
        }

        private int _age;
        public int Age
        {
            get { return _age; }
            set
            {
                _age = value;
                this.RaisePropertyChanged("Age");
            }
        }

    }
}