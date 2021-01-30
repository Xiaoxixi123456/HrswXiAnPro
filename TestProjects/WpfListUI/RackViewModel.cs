using Prism.Mvvm;
using System.Windows.Controls;

namespace WpfListUI
{
    public class RackViewModel : BindableBase
    {
        private int _id;
        public int Id
        {
            get { return _id; }
            set
            {
                _id = value;
                this.RaisePropertyChanged("Id");
            }
        }


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

        private UserControl _userControl;
        public UserControl UserControlUI
        {
            get { return _userControl; }
            set
            {
                _userControl = value;
                this.RaisePropertyChanged("UserControlUI");
            }
        }

        public RackViewModel(int id, bool hasData)
        {
            _id = id;
            if (hasData)
            {
                _person = new Person() { Name = "abc", Age = 30 };
                _userControl = new UserControl1();
                UserControlUI.DataContext = Person; 
            }
            else
            {
                _userControl = new EmptyUserControl();
            }
        }
    }
}