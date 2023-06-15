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

namespace WpfProjekt.MVVM.View
{
    /// <summary>
    /// Logika interakcji dla klasy ProfileView.xaml
    /// </summary>
    public partial class ProfileView : UserControl
    {
        public ObservableCollection<Game> Games = new ObservableCollection<Game>(Session.GetInstance().GetUserGames());

        public ProfileView()
        {
            InitializeComponent();
            DataContext = this;
            foreach (Game game in Games)
            {
                // Tworzenie elementów XAML dla poszczególnych atrybutów Game
                Image image = new Image();
                image.Source = game.image;
                image.Width = 100;
                image.Height = 100;

                TextBlock nameTextBlock = new TextBlock();
                nameTextBlock.Text = game.title;

                TextBlock categoryTextBlock = new TextBlock();
                categoryTextBlock.Text = game.category.ToString();

                TextBlock ratingTextBlock = new TextBlock();
                ratingTextBlock.Text = game.rating.ToString();

                // Tworzenie elementu ListViewItem
                ListViewItem item = new ListViewItem();

                // Tworzenie StackPanel i ustawianie orientacji
                StackPanel stackPanel = new StackPanel();
                stackPanel.Orientation = Orientation.Horizontal;

                // Dodawanie elementów XAML do StackPanel
                stackPanel.Children.Add(image);
                stackPanel.Children.Add(nameTextBlock);
                stackPanel.Children.Add(categoryTextBlock);
                stackPanel.Children.Add(ratingTextBlock);

                // Ustawianie StackPanel jako zawartość elementu ListViewItem
                item.Content = stackPanel;

                // Dodawanie elementu ListViewItem do ListView
                GamesInStoreListView.Items.Add(item);
            }
        }

        private void ProfEditButton_Click(object sender, RoutedEventArgs e)
        {
            EditProfWindow editWindow = new EditProfWindow();
            editWindow.Show();
            
        }

        private void DeleteGameButton_Click(object sender, RoutedEventArgs e)
        {
            //if()
        }
    }
}
