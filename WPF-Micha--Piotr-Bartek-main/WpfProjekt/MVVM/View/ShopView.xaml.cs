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
            foreach (Game game in Games)
            {
                ListViewItem listViewItem = new ListViewItem();
                GridView gridView = new GridView();

                GridViewColumn imageColumn = new GridViewColumn();
                imageColumn.Header = "Image";
                imageColumn.Width = 150;

                DataTemplate imageCellTemplate = new DataTemplate();
                FrameworkElementFactory imageFactory = new FrameworkElementFactory(typeof(Image));
                imageFactory.SetValue(Image.SourceProperty, new Binding("image"));
                imageFactory.SetValue(Image.WidthProperty, 100.0);
                imageFactory.SetValue(Image.HeightProperty, 100.0);
                imageCellTemplate.VisualTree = imageFactory;
                imageColumn.CellTemplate = imageCellTemplate;

                GridViewColumn titleColumn = new GridViewColumn();
                titleColumn.DisplayMemberBinding = new Binding("title");
                titleColumn.Header = "Title";
                titleColumn.Width = 100;

                GridViewColumn categoryColumn = new GridViewColumn();
                categoryColumn.DisplayMemberBinding = new Binding("category");
                categoryColumn.Header = "Category";
                categoryColumn.Width = 100;

                GridViewColumn ratingColumn = new GridViewColumn();
                ratingColumn.DisplayMemberBinding = new Binding("rating");
                ratingColumn.Header = "Rating";
                ratingColumn.Width = 200;

                gridView.Columns.Add(imageColumn);
                gridView.Columns.Add(titleColumn);
                gridView.Columns.Add(categoryColumn);
                gridView.Columns.Add(ratingColumn);

                GamesInStoreListView.View = gridView;

                listViewItem.Content = game;
                listViewItem.MouseUp += ListViewItemMouseDoubleClick;

                GamesInStoreListView.Items.Add(listViewItem);
            }
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
    }
}