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
        public List<Game> Games { get; } = new List<Game>(session.GetAllGames());
        public List<User> Users { get; } = new List<User>(session.GetAllUsers());
        private ListBoxItem selectedGameItem;
        private ListBoxItem selectedUserItem;


        public AdminWindow()
        {
            InitializeComponent();
            DataContext = this;
            int i = 0;
            foreach (CategoryEnum category in Enum.GetValues(typeof(CategoryEnum)))
            {
                catValue.Items.Insert(i, category);
                i++;
            }
            

            LoadGamesFromDatabase();
            LoadUsersFromDatabase();
        }

        private void LoadGamesFromDatabase()
        {
            List<Game> games = session.GetAllGames();
            Games.Clear();
            Games.AddRange(games);
            DisplayGamesInList(Games);
        }

        private void LoadUsersFromDatabase()
        {
            List<User> users = session.GetAllUsers();
            Users.Clear();
            Users.AddRange(users);
            DisplayUsersInList(Users);
        }

        //// GRY

        private void GamesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedGameItem = (ListBoxItem)GamesListBox.SelectedItem;
        }


        private void DisplayGamesInList(List<Game> Games)
        {
            foreach (Game game in Games)
            {
                ListBoxItem gameslistBoxItem = new ListBoxItem();
                gameslistBoxItem.Content = game;
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
            float rating = float.Parse(ratingValue.Text);

            Game newGame = new Game(id, title, category, imagePath, rating);
            session.AddGame(newGame);
            Games.Add(newGame);

            ListBoxItem newGameListBoxItem = new ListBoxItem();
            newGameListBoxItem.Content = newGame;
            GamesListBox.Items.Add(newGameListBoxItem);

            ClearInputFields();
        }

        private void DeleteGame_Click(object sender, RoutedEventArgs e)
        {
            if (selectedGameItem != null)
            {
                Game selectedGame = (Game)selectedGameItem.Content;
                Games.Remove(selectedGame);
                GamesListBox.Items.Remove(selectedGameItem);
                selectedGameItem = null;
            }
        }

        private void ClearInputFields()
        {
            titleValue.Text = string.Empty;
            catValue.SelectedItem = null;
            imagepathValue.Text = string.Empty;
            ratingValue.Text = string.Empty;
        }

        private void catValue_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }



        //// UŻYTKOWNICY
        

        private void UserGames_Click(object sender, RoutedEventArgs e)
        {
            AdminUserGames adminWindow2 = new AdminUserGames();
            adminWindow2.Show();
        }

        private void UserListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedUserItem = (ListBoxItem)UserListBox.SelectedItem;
        }


        private void DisplayUsersInList(List<User> Users)
        {
            foreach (User user in Users)
            {
                ListBoxItem userslistBoxItem = new ListBoxItem();
                userslistBoxItem.Content = user;
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

            ListBoxItem newUserListBoxItem = new ListBoxItem();
            newUserListBoxItem.Content = newUser;
            UserListBox.Items.Add(newUserListBoxItem);

            ClearInputFields();
        }

        private void DeleteUser_Click(object sender, RoutedEventArgs e)
        {
            if (selectedUserItem != null)
            {
                User selectedUser = (User)selectedUserItem.Content;
                if (session.DeleteUser(selectedUser.id))
                {
                    Users.Remove(selectedUser);
                    UserListBox.Items.Remove(selectedUserItem);
                    selectedUserItem = null;
                }
                else
                {
                    MessageBox.Show("Error during deleting user");
                }

            }
        }

        private void isAdminValue_Checked(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
