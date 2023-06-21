using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
using WpfProjekt.Core;
//using System.Printing;

namespace WpfProjekt.MVVM.View
{
    /// <summary>
    /// Logika interakcji dla klasy ProfileView.xaml
    /// </summary>
    public partial class ProfileView : UserControl
    {
        static Session session = Session.GetInstance();
        public User CurrentUser => session.currentUser;
        public List<Game> userGames { get; } = new List<Game>(session.GetUserGames());
        public ICommand SortByTitleCommand { get; }
        public ICommand SortByCategoryCommand { get; }
        public ICommand SortByRatingCommand { get; }
        public bool sortTitleAsc;
        public bool sortCategoryAsc;
        public bool sortRatingAsc;

        public ProfileView()
        {
            InitializeComponent();
            DataContext = this;

            DisplayUsername();

            DisplayGamesInList(userGames);
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
            List<Game> sortedGames = session.GetSortedUserGamesByTitle(sortTitleAsc);
            DisplayGamesInList(sortedGames);
            sortTitleAsc = !sortTitleAsc;
        }

        private void SortByCategory(object obj)
        {
            GamesInStoreListView.Items.Clear();
            List<Game> sortedGames = session.GetSortedUserGamesByCategory(sortCategoryAsc);
            DisplayGamesInList(sortedGames);
            sortCategoryAsc = !sortCategoryAsc;
        }

        private void SortByRating(object obj)
        {
            GamesInStoreListView.Items.Clear();
            List<Game> sortedGames = session.GetSortedUserGamesByRatings(sortRatingAsc);
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
                DeleteGameButton.Visibility = Visibility.Visible;
                PlayGameButton.Visibility = Visibility.Visible;
            }
        }

        private void DeleteGameButton_Click(object sender, RoutedEventArgs e)
        {
            if (GamesInStoreListView.SelectedItem != null)
            {
                ListViewItem selectedItem = (ListViewItem)GamesInStoreListView.SelectedItem;
                Game selectedGame = (Game)selectedItem.Content;
                string selectedGameTitle = selectedGame.title;
                session.DeleteGame(selectedGame);
                GamesInStoreListView.Items.Remove(selectedItem);
                MessageBox.Show("Gra " + selectedGameTitle + " została usunieta z biblioteki");
            }
        }

        private void PlayGameButton_Click(object sender, RoutedEventArgs e)
        {
            if (GamesInStoreListView.SelectedItem != null)
            {
                ListViewItem selectedItem = (ListViewItem)GamesInStoreListView.SelectedItem;
                Game selectedGame = (Game)selectedItem.Content;
                string selectedGameTitle = selectedGame.title;
                MessageBox.Show("Uruchomienie gry: " + selectedGameTitle);
            }
        }

        private void ProfEditButton_Click(object sender, RoutedEventArgs e)
        {
            EditProfWindow editWindow = new EditProfWindow();
            editWindow.Show();
            
        }
        private void PrintButton_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printDlg = new PrintDialog();

            FlowDocument doc = new FlowDocument();
            Paragraph paragraph = new Paragraph();

            // Print username
            paragraph.Inlines.Add(new Run("Username: " + CurrentUser.username));

            // Print game titles
            paragraph.Inlines.Add(new Run("\nGames in Library:\n"));
            foreach (Game game in userGames)
            {
                paragraph.Inlines.Add(new Run("- " + game.title + "\n"));
            }

            doc.Blocks.Add(paragraph);
            doc.Name = "Playerprofile";

            IDocumentPaginatorSource idpSource = doc;

            if (printDlg.ShowDialog() == true)
            {
                printDlg.PrintDocument(idpSource.DocumentPaginator, "Player Profile Printing.");
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

        private void DisplayUsername()
        {
            if (CurrentUser != null)
            {
                UsernameLabel.Content = CurrentUser.username;
            }
        }
    }
}
