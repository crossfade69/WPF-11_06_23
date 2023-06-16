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
using WpfProjekt.Core;


namespace WpfProjekt.MVVM.View
{
    public partial class ShopView : UserControl
    {

        static Session session = Session.GetInstance();
        public List<Game> Games { get; } = new List<Game>(session.GetAllGames());
        
        public ICommand SortByTitleCommand { get; }
        public ICommand SortByCategoryCommand { get; }
        public ICommand SortByRatingCommand { get; }
        public bool sortTitleAsc;
        public bool sortCategoryAsc;
        public bool sortRatingAsc;

        public ShopView()
        {
            InitializeComponent();
            DataContext = this;
            DisplayGamesInList(Games);
            SortByTitleCommand = new RelayCommand(SortByTitle);
            SortByCategoryCommand = new RelayCommand(SortByCategory);
            SortByRatingCommand = new RelayCommand(SortByRating);
            sortTitleAsc = true;
            sortCategoryAsc = true;
            sortRatingAsc = true;
        }

        private void SortByTitle(object obj)
        {
            GamesInStoreListView.Items.Clear();
            List<Game> sortedGames = session.GetSortedGamesByTitle(sortTitleAsc);
            DisplayGamesInList(sortedGames);
            sortTitleAsc = !sortTitleAsc;
        }

        private void SortByCategory(object obj)
        {
            GamesInStoreListView.Items.Clear();
            List<Game> sortedGames = session.GetSortedGamesByCategory(sortCategoryAsc);
            DisplayGamesInList(sortedGames);
            sortCategoryAsc = !sortCategoryAsc;
        }

        private void SortByRating(object obj)
        {
            GamesInStoreListView.Items.Clear();
            List<Game> sortedGames = session.GetSortedGamesByRatings(sortRatingAsc);
            DisplayGamesInList(sortedGames);
            sortRatingAsc = !sortRatingAsc;
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
                if (session.AddGame(selectedGame))
                {
                    MessageBox.Show("Zakup udany gry: " + selectedGameTitle);
                } else
                {
                    MessageBox.Show("Gry: " + selectedGameTitle + " jest już w twojej bibliotece");
                }
            }
        }
        private void DisplayGamesInList(List<Game> Games)
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

            List<Game> FilteredGames = new List<Game>();
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