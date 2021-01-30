using Prism.Mvvm;

namespace WpfListUI
{
    public class TrayViewModel : BindableBase
    {
        private Person _person;
        public Person Person
        {
            get { return _person; }
            set
            {
                _person = value;
                this.RaisePropertyChanged("Person");
            }
        }

        public TrayViewModel()
        {
            _person = new Person() { Name = "aaaa", Age = 20 };
        }
    }
}