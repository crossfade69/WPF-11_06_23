using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography;
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
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Data;

namespace WpfProjekt.MVVM.View
{
    public partial class ShopView : UserControl
    {

        static Session session = Session.GetInstance();
        public ObservableCollection<Game> Games { get; } = new ObservableCollection<Game>(session.GetAllGames());

        public ShopView()
        {
            InitializeComponent();
            DataContext = this;
            DisplayGamesInList();


        }

        private void ListViewItemMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 1)
            {
                GamesInStoreListView_SelectionChanged(sender, e);
            }
        }

        private void GamesInStoreListView_SelectionChanged(object sender, MouseButtonEventArgs e)
        {
            if (GamesInStoreListView.SelectedItem != null)
            {
                BuyAndDownloadButton.Visibility = Visibility.Visible;
            }
        }


        private void BuyAndDownloadButton_Click(object sender, RoutedEventArgs e)
        {
            if (GamesInStoreListView.SelectedItem != null)
            {
                //Game selectedGame = (Game)GamesInStoreListView.SelectedItem;
                //session.AddGame()
                MessageBox.Show("Zakup udany");
            }
        }
        private void DisplayGamesInList()
        {
            foreach (Game game in Games)
            {
                ListViewItem listViewItem = new ListViewItem();


                listViewItem.Content = game;
                listViewItem.MouseUp += ListViewItemMouseDoubleClick;

                GamesInStoreListView.Items.Add(listViewItem);
            }
        }

    }
}