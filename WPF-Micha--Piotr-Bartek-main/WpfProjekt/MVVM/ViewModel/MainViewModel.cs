using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfProjekt.Core;

namespace WpfProjekt.MVVM.ViewModel
{
    class MainViewModel : ObservableObj
    {
        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand ShopViewCommand { get; set; }
        public RelayCommand ProfileViewCommand { get; set; }


        public HomeViewModel HomeVM { get; set; }
        public ShopViewModel ShopVM { get; set; }
        public ProfileViewModel ProfileVM { get; set; }

        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value;
                    OnPropertyChanged();
            }
        }

        public MainViewModel() 
        {
            HomeVM= new HomeViewModel();
            ShopVM = new ShopViewModel();
            ProfileVM = new ProfileViewModel();

            CurrentView = HomeVM;

            HomeViewCommand = new RelayCommand(o =>
            {
                CurrentView = HomeVM;
            });

            ShopViewCommand = new RelayCommand(o =>
            {
                CurrentView = ShopVM;
            });

            ProfileViewCommand = new RelayCommand(o =>
            {
                CurrentView = ProfileVM;
            });            
        }
    }
}
