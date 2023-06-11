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
        public RelayCommand DisViewCommand { get; set; }
        public RelayCommand ShopViewCommand { get; set; }
        public RelayCommand ProfileViewCommand { get; set; }
        public RelayCommand GameViewCommand { get; set; }



        public HomeViewModel HomeVM { get; set; }
        public DiscoveryVModel DisVM { get; set; }
        public ShopViewModel ShopVM { get; set; }
        public ProfileViewModel ProfileVM { get; set; }
        public GameViewModel GameVM { get; set; }

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
            DisVM= new DiscoveryVModel();
            ShopVM = new ShopViewModel();
            ProfileVM = new ProfileViewModel();
            GameVM = new GameViewModel();

            CurrentView = HomeVM;

            HomeViewCommand = new RelayCommand(o =>
            {
                CurrentView = HomeVM;
            });

            DisViewCommand = new RelayCommand(o =>
            {
                CurrentView = DisVM;
            });

            ShopViewCommand = new RelayCommand(o =>
            {
                CurrentView = ShopVM;
            });

            ProfileViewCommand = new RelayCommand(o =>
            {
                CurrentView = ProfileVM;
            });

            GameViewCommand = new RelayCommand(o =>
            {
                CurrentView = GameVM;
            });
        }
    }
}
