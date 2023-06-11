using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfProjekt.Core;

namespace WpfProjekt.MVVM.ViewModel
{
    internal class LoginWindowModel : ObservableObj
    {
        public RelayCommand MainViewCommand { get; set; }
        public RelayCommand LoginWindowCommand { get; set; }

        public MainViewModel MainVM { get; set; }
        public LoginWindowModel LoginWM { get; set; }

        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public LoginWindowModel()
        {
            MainVM = new MainViewModel();
            LoginWM = new LoginWindowModel();

            CurrentView = LoginWM;

            MainViewCommand = new RelayCommand(o =>
            {
                CurrentView = MainVM;
            });
        }

    }
}
