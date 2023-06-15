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
            foreach (Game game in Games)
            {
                // Tworzenie nowego wiersza
                ListViewItem listViewItem = new ListViewItem();

                // Tworzenie GridView
                GridView gridView = new GridView();

                // Tworzenie kolumny dla obrazka
                GridViewColumn imageColumn = new GridViewColumn();
                imageColumn.Header = "Image";
                imageColumn.Width = 150;

                // Definicja szablonu dla komórki obrazka
                DataTemplate imageCellTemplate = new DataTemplate();
                FrameworkElementFactory imageFactory = new FrameworkElementFactory(typeof(Image));
                imageFactory.SetValue(Image.SourceProperty, new Binding("image"));
                imageFactory.SetValue(Image.WidthProperty, 100.0);
                imageFactory.SetValue(Image.HeightProperty, 100.0);
                imageCellTemplate.VisualTree = imageFactory;

                // Ustawienie szablonu dla komórki obrazka
                imageColumn.CellTemplate = imageCellTemplate;

                // Tworzenie kolumny dla tytułu
                GridViewColumn titleColumn = new GridViewColumn();
                titleColumn.DisplayMemberBinding = new Binding("title");
                titleColumn.Header = "Title";
                titleColumn.Width = 100;

                // Tworzenie kolumny dla kategorii
                GridViewColumn categoryColumn = new GridViewColumn();
                categoryColumn.DisplayMemberBinding = new Binding("category");
                categoryColumn.Header = "Category";
                categoryColumn.Width = 100;

                // Tworzenie kolumny dla oceny
                GridViewColumn ratingColumn = new GridViewColumn();
                ratingColumn.DisplayMemberBinding = new Binding("rating");
                ratingColumn.Header = "Rating";
                ratingColumn.Width = 200;

                // Dodawanie kolumn do GridView
                gridView.Columns.Add(imageColumn);
                gridView.Columns.Add(titleColumn);
                gridView.Columns.Add(categoryColumn);
                gridView.Columns.Add(ratingColumn);

                // Ustawianie GridView jako widok dla ListView
                GamesInStoreListView.View = gridView;

                // Ustawianie danych dla wiersza
                listViewItem.Content = game;

                // Dodawanie wiersza do GamesInStoreListView
                GamesInStoreListView.Items.Add(listViewItem);
            }
        }

        private void GamesInStoreListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Check if an item is selected
            if (GamesInStoreListView.SelectedItem != null)
            {
                isItemSelected = true;
                DeleteGameButton.Visibility = Visibility.Visible;
                PlayGameButton.Visibility = Visibility.Visible;
            }
            else
            {
                isItemSelected = false;
                DeleteGameButton.Visibility = Visibility.Collapsed;
                PlayGameButton.Visibility = Visibility.Collapsed;
            }
        }

        private void DeleteGameButton_Click(object sender, RoutedEventArgs e)
        {
            // Handle delete button click
            if (isItemSelected)
             {
                 // Delete the selected game
                 Game selectedGame = (Game)GamesInStoreListView.SelectedItem;
                 Games.Remove(selectedGame);
                 // Additional logic if needed
             }
        }

        private void PlayGameButton_Click(object sender, RoutedEventArgs e)
        {
            // Handle play button click
            if (isItemSelected)
            {
                // Play the selected game
                Game selectedGame = (Game)GamesInStoreListView.SelectedItem;
                // Additional logic if needed
            }
        }

        private void ProfEditButton_Click(object sender, RoutedEventArgs e)
        {
            EditProfWindow editWindow = new EditProfWindow();
            editWindow.Show();
            
        }

        
    }
}
