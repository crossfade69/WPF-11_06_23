using System;
using System.Collections.Generic;
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

namespace WpfProjekt.MVVM.View
{
    /// <summary>
    /// Logika interakcji dla klasy AdminUserGames.xaml
    /// </summary>
    public partial class AdminUserGames : Window
    {
        static Session session = Session.GetInstance();
        public List<Game> userGames { get; } = new List<Game>(session.GetUserGames());
        private ListBoxItem selectedGameItem;

        public AdminUserGames()
        {
            InitializeComponent();
            DataContext = this;
            DisplayGamesInList(userGames);
        }

        private void UserGamesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedGameItem = (ListBoxItem)UserGamesListBox.SelectedItem;
        }

        

        private void DisplayGamesInList(List<Game> Games)
        {
            foreach (Game game in userGames)
            {
                ListViewItem listViewItem = new ListViewItem();
                listViewItem.Content = game;
                UserGamesListBox.Items.Add(listViewItem);
            }
        }

        private void DeleteGameButton_Click(object sender, RoutedEventArgs e)
        {
            if (UserGamesListBox.SelectedItem != null)
            {
                ListViewItem selectedItem = (ListViewItem)UserGamesListBox.SelectedItem;
                Game selectedGame = (Game)selectedItem.Content;
                string selectedGameTitle = selectedGame.title;
                session.DeleteGame(selectedGame);
                UserGamesListBox.Items.Remove(selectedItem);
                MessageBox.Show("Gra " + selectedGameTitle + " została usunieta z biblioteki");
            }
        }

        
    }
}
