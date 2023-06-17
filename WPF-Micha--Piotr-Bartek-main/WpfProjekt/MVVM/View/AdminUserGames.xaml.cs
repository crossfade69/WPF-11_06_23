using System;
using System.Collections.Generic;
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

namespace WpfProjekt.MVVM.View
{
    /// <summary>
    /// Logika interakcji dla klasy AdminUserGames.xaml
    /// </summary>
    public partial class AdminUserGames : Window
    {
        static Session session = Session.GetInstance();
        public List<Game> userGames { get; } = new List<Game>(session.GetUserGames());
        public AdminUserGames()
        {
            InitializeComponent();
        }

        private void UserGamesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            /*if (GamesInStoreListView.SelectedItem != null)
            {
                DeleteGameButton.Visibility = Visibility.Visible;
            }*/
        }

        private void ListViewItemMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 1)
            {
                //GamesInStoreListView_SelectionChanged(sender, e);
            }
        }

        private void DisplayGamesInList(List<Game> Games)
        {
            foreach (Game game in Games)
            {
                ListViewItem listViewItem = new ListViewItem();
                listViewItem.Content = game;
                listViewItem.MouseUp += ListViewItemMouseDoubleClick;
                //GamesInStoreListView.Items.Add(listViewItem);
            }
        }

        private void DeleteGameButton_Click(object sender, RoutedEventArgs e)
        {
            /*if (GamesInStoreListView.SelectedItem != null)
            {
                ListViewItem selectedItem = (ListViewItem)GamesInStoreListView.SelectedItem;
                Game selectedGame = (Game)selectedItem.Content;
                string selectedGameTitle = selectedGame.title;
                session.DeleteGame(selectedGame);
                GamesInStoreListView.Items.Remove(selectedItem);
                MessageBox.Show("Gra " + selectedGameTitle + " została usunieta z biblioteki");
            }*/
        }
    }
}
