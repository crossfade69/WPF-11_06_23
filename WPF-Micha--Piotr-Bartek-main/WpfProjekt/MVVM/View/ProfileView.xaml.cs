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
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Data;

namespace WpfProjekt.MVVM.View
{
    /// <summary>
    /// Logika interakcji dla klasy ProfileView.xaml
    /// </summary>
    public partial class ProfileView : UserControl
    {
        static Session session = Session.GetInstance();
        public ObservableCollection<Game> Games { get; } = new ObservableCollection<Game>(session.GetUserGames());
        private bool isItemSelected = false;
        public ProfileView()
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
                isItemSelected = true;
                DeleteGameButton.Visibility = Visibility.Visible;
                PlayGameButton.Visibility = Visibility.Visible;
            }
            /*else
            {
                isItemSelected = false;
                DeleteGameButton.Visibility = Visibility.Collapsed;
                PlayGameButton.Visibility = Visibility.Collapsed;
            }*/
        }

        private void DeleteGameButton_Click(object sender, RoutedEventArgs e)
        {
            if (GamesInStoreListView.SelectedItem != null)
            {
                 //Game selectedGame = (Game)GamesInStoreListView.SelectedItem;
                 //session.DeleteGame(selectedGame);
                 GamesInStoreListView.Items.Remove(GamesInStoreListView.SelectedItem);
                MessageBox.Show("Gra została usunieta z biblioteki");
            }
        }

        private void PlayGameButton_Click(object sender, RoutedEventArgs e)
        {
            if (GamesInStoreListView.SelectedItem != null)
            {
                //Game selectedGame = (Game)GamesInStoreListView.SelectedItem;
                MessageBox.Show("Uruchomienie gry");
            }
        }

        private void ProfEditButton_Click(object sender, RoutedEventArgs e)
        {
            EditProfWindow editWindow = new EditProfWindow();
            editWindow.Show();
            
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
