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

namespace WpfProjekt.MVVM.View
{
    /// <summary>
    /// Logika interakcji dla klasy ShopView.xaml
    /// </summary>
    public partial class ShopView : UserControl
    {

        public ObservableCollection<Game> Games = new ObservableCollection<Game>(Session.GetInstance().GetAllGames());

        public ShopView()
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
    }
}

