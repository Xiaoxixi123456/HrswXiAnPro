using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;



namespace WpfListUI
{
    public class ViewModel : BindableBase
    {
        private object syncLock = new object();

        private int _columnCount;
        public int ColumnCount
        {
            get { return _columnCount; }
            set
            {
                _columnCount = value;
                this.RaisePropertyChanged("ColumnCount");
            }
        }

        private int _rowCount;
        public int RowCount
        {
            get { return _rowCount; }
            set
            {
                _rowCount = value;
                this.RaisePropertyChanged("RowCount;");
            }
        }

        private bool _canChange = true;
        public bool CanChange
        {
            get { return _canChange; }
            set
            {
                _canChange = value;
                this.RaisePropertyChanged("CanChange");
            }
        }


        public ObservableCollection<RackViewModel> RackViewModels { get; set; }

        public DelegateCommand ChangeCommand { get; set; }

        public ViewModel()
        {
            _columnCount = 3;
            _rowCount = 3;
            RackViewModels = new ObservableCollection<RackViewModel>();
            Random rand = new Random(DateTime.Now.Millisecond);
            for (int i = 0; i < ColumnCount * RowCount; i++)
            {
                if (rand.Next(1, 200) % 2 == 0)
                {
                    RackViewModels.Add(new RackViewModel(i, true));
                }
                else
                {
                    RackViewModels.Add(new RackViewModel(i, false));
                }
            }
            ChangeCommand = new DelegateCommand(ChangeSlot, () => CanChange);
            Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                BindingOperations.EnableCollectionSynchronization(RackViewModels, syncLock);
            }));
        }

        private void ChangeSlot()
        {
            throw new NotImplementedException();
        }
    }
}
