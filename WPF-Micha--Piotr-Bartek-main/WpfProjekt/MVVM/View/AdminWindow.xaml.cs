using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
using System.Windows.Shapes;
using WpfProjekt.Core;

namespace WpfProjekt.MVVM.View
{
    /// <summary>
    /// Logika interakcji dla klasy AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        static Session session = Session.GetInstance();

        public ObservableCollection<User> Users { get; } = new ObservableCollection<User>();
        public ObservableCollection<Game> Games { get; } = new ObservableCollection<Game>();

        private Game previousSelectedGame;
        private User previousSelectedUser;

        public AdminWindow()
        {
            InitializeComponent();
            DataContext = this;
            previousSelectedGame = null;
            previousSelectedUser = null;
            int i = 0;
            foreach (CategoryEnum category in Enum.GetValues(typeof(CategoryEnum)))
            {
                catValue.Items.Insert(i, category);
                i++;
            }

            LoadGamesFromDatabase();
            LoadUsersFromDatabase();

            Binding szczegolyUserBinding = new Binding();
            szczegolyUserBinding.Source = UserListBox;
            szczegolyUserBinding.Path = new PropertyPath("SelectedItem");
            szczegolyUser.SetBinding(Grid.DataContextProperty, szczegolyUserBinding);

            Binding szczegolyGameBinding = new Binding();
            szczegolyGameBinding.Source = GamesListBox;
            szczegolyGameBinding.Path = new PropertyPath("SelectedItem");
            szczegolyGame.SetBinding(Grid.DataContextProperty, szczegolyGameBinding);
        }

        private void LoadGamesFromDatabase()
        {
            List<Game> games = session.GetAllGames();
            Games.Clear();
            foreach (Game game in games)
            {
                Games.Add(game);
            }
            DisplayGamesInList(Games);
        }

        private void LoadUsersFromDatabase()
        {
            List<User> users = session.GetAllUsers();
            Users.Clear();
            foreach (User user in users)
            {
                Users.Add(user);
            }
            DisplayUsersInList(Users);
        }

        private void DisplayGamesInList(ObservableCollection<Game> Games)
        {
            GamesListBox.Items.Clear();
            foreach (Game game in Games)
            {
                ListBoxItem gameslistBoxItem = new ListBoxItem();
                gameslistBoxItem.DataContext = game;
                GamesListBox.Items.Add(gameslistBoxItem);
            }
        }

        private void AddGame_Click(object sender, RoutedEventArgs e)
        {

            Random random = new Random();
            int id = random.Next();
            string title = titleValue.Text;

            string kategory = catValue.SelectedValue.ToString();
            CategoryEnum category = (CategoryEnum)Enum.Parse(typeof(CategoryEnum), kategory);

            string imagePath = imagepathValue.Text;
            float rating = float.Parse(ratingValue.Text.Replace(",", "."));
            Game newGame;
            try
            {
                newGame = new Game(id, title, category, imagePath, rating);
            }
            catch (UriFormatException ex)
            {
                MessageBox.Show("Nie własciwa ścieżka do pliku zdjęcia. Ustawiono grafikę domyślną");
                imagePath = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString()) + @"\Images\default_user.png";
                newGame = new Game(id, title, category, imagePath, rating);
            }

            int actualGameId = session.AddGameToDatabase(title, category.ToString(), imagePath, rating);

            if (actualGameId != -1)
            {
                newGame.id = actualGameId;
                Games.Add(newGame);

                ClearInputFields();
            }
            else
            {
                MessageBox.Show("Invalid Id error");
            }
        }

        private void DeleteGame_Click(object sender, RoutedEventArgs e)
        {
            if (GamesListBox.SelectedItem != null)
            {
                Game selectedGame = (Game)GamesListBox.SelectedItem;
                Games.Remove(selectedGame);
                previousSelectedGame = null;
            }
        }

        private void ClearInputFields()
        {
            titleValue.Text = string.Empty;
            catValue.SelectedItem = null;
            imagepathValue.Text = string.Empty;
            ratingValue.Text = string.Empty;
        }

        private void GamesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (GamesListBox.SelectedItem != null)
            {
                if (previousSelectedGame != null)
                {
                    session.UpdateGameInDatabase(previousSelectedGame);
                }
            }
            previousSelectedGame = (Game)GamesListBox.SelectedItem;
        }

        private void catValue_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }



        //// UŻYTKOWNICY

        private void UserListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (UserListBox.SelectedItem != null)
            {
                if (previousSelectedUser != null)
                {
                    session.UpdateUserInDatabase(previousSelectedUser);
                }
            }
            previousSelectedUser = (User)UserListBox.SelectedItem;
        }


        private void DisplayUsersInList(ObservableCollection<User> Users)
        {
            foreach (User user in Users)
            {
                ListBoxItem userslistBoxItem = new ListBoxItem();
                userslistBoxItem.DataContext = user;
                UserListBox.Items.Add(userslistBoxItem);
            }
        }

        private void AddUser_Click(object sender, RoutedEventArgs e)
        {
            string login = loginValue.Text;
            string username = usernameValue.Text;
            string password = passwordValue.Text;
            bool isAdmin = isAdminValue.IsChecked ?? false; ;


            User newUser = session.AddUser(login, username, password, isAdmin);
            if (newUser == null)
            {
                if (!session.currentUser.isAdmin)
                {
                    MessageBox.Show("You need to be logged in as an admin to add a user.");
                }
                else if (session.isThereUserWithThisLogin(login))
                {
                    MessageBox.Show("User already exists.");
                }
                else
                {
                    MessageBox.Show("Unknown reason for not creating new user.");
                }
                return;
            }
            Users.Add(newUser);
            ClearInputFields();
        }

        private void DeleteUser_Click(object sender, RoutedEventArgs e)
        {
            if (UserListBox.SelectedItem != null)
            {
                    User selectedUser = (User)UserListBox.SelectedItem;
                if (session.DeleteUser(selectedUser.id))
                {
                    Users.Remove(selectedUser);
                    previousSelectedUser = null;
                }
                else
                {
                    MessageBox.Show("Error during deleting user");
                }

            }
        }

    }
}

