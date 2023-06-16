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
        public ObservableCollection<Game> FilteredGames { get; } = new ObservableCollection<Game>();

        public ShopView()
        {
            InitializeComponent();
            DataContext = this;
            DisplayGamesInList(Games);


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
                ListViewItem selectedItem = (ListViewItem)GamesInStoreListView.SelectedItem;
                Game selectedGame = (Game)selectedItem.Content;
                string selectedGameTitle = selectedGame.title;
                session.AddGame(selectedGame);
                MessageBox.Show("Zakup udany gry: " + selectedGameTitle);
            }
        }
        private void DisplayGamesInList(ObservableCollection<Game> Games)
        {
            foreach (Game game in Games)
            {
                ListViewItem listViewItem = new ListViewItem();
                listViewItem.Content = game;
                listViewItem.MouseUp += ListViewItemMouseDoubleClick;
                GamesInStoreListView.Items.Add(listViewItem);
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string searchText = SearchTextBox.Text.ToLower();

            if (string.IsNullOrWhiteSpace(searchText))
            {
                GamesInStoreListView.Items.Clear();
                DisplayGamesInList(Games);
                return;
            }

            FilteredGames.Clear();

            foreach (Game game in Games)
            {
                if (game.title.ToLower().Contains(searchText))
                {
                    FilteredGames.Add(game);
                }
            }

            GamesInStoreListView.Items.Clear();
            DisplayGamesInList(FilteredGames);
        }

    }
}