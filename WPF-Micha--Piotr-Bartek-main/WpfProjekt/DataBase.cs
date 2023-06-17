using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace WpfProjekt
{
    public class DataBase
    {
        public SQLiteConnection connection;
        private string databasePath;

        public DataBase()
        {
            databasePath = Path.Combine(Directory.GetCurrentDirectory(), "database.db");
            InitializeDatabase();
        }

        private void InitializeDatabase()
        {
            if (!File.Exists(databasePath))
            {
                SQLiteConnection.CreateFile(databasePath);
                CreateTables();
                InsertInitialData();
            }
            else
            {
                connection = new SQLiteConnection($"Data Source={databasePath};Version=3;");
                connection.Open();
            }
        }

        private void CreateTables()
        {
            connection = new SQLiteConnection($"Data Source={databasePath};Version=3;");
            connection.Open();

            string createUserTableQuery = "CREATE TABLE IF NOT EXISTS Users (Id INTEGER PRIMARY KEY AUTOINCREMENT, Login TEXT, Password TEXT);";
            string createGameTableQuery = "CREATE TABLE IF NOT EXISTS Games (Id INTEGER PRIMARY KEY AUTOINCREMENT, Title TEXT, Category INTEGER, ImagePath TEXT, Rating REAL);";
            string createGameUserTableQuery = "CREATE TABLE IF NOT EXISTS UserGames (UserId INTEGER, GameId INTEGER, FOREIGN KEY(UserId) REFERENCES Users(Id), FOREIGN KEY(GameId) REFERENCES Games(Id));";

            ExecuteQuery(createUserTableQuery);
            ExecuteQuery(createGameTableQuery);
            ExecuteQuery(createGameUserTableQuery);
        }

        private void InsertInitialData()
        {
            // Insert initial user data
            ExecuteQuery("INSERT INTO Users (Login, Password, ) VALUES ('Piotrek', 'haslo');");
            ExecuteQuery("INSERT INTO Users (Login, Password) VALUES ('Bartek', 'Bartek');");
            ExecuteQuery("INSERT INTO Users (Login, Password) VALUES ('Michał', 'Napiórkowski');");

            // Insert initial game data
            ExecuteQuery("INSERT INTO Games (Title, Category, ImagePath, Rating) VALUES ('Mario', 0, 'default_user.png', 4.8);");
            ExecuteQuery("INSERT INTO Games (Title, Category, ImagePath, Rating) VALUES ('Mario2', 0, 'default_user.png', 4.7);");
            ExecuteQuery("INSERT INTO Games (Title, Category, ImagePath, Rating) VALUES ('Smash bros', 1, 'default_user.png', 4.7);");
            ExecuteQuery("INSERT INTO Games (Title, Category, ImagePath, Rating) VALUES ('Dying Light', 0, 'default_user.png', 4.6);");
        }

        private void ExecuteQuery(string query)
        {
            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                command.ExecuteNonQuery();
            }
        }

        public User Login(string login, string pass)
        {
            string query = $"SELECT * FROM Users WHERE Login = '{login}' AND Password = '{pass}';";
            User user = null;

            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        string username = reader.GetString(1);
                        string password = reader.GetString(2);
                        user = new User(username, password, false, new List<int>(), ""); // You may need to modify the constructor parameters based on your User class
                    }
                }
            }

            return user;
        }











        /*
        public static string dir = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString()).ToString();
        public List<Game> games = new List<Game>()
        {

            new Game("Mario",CategoryEnum.adventure,dir+@"\Images\default_user.png",4.8f),
            new Game("Mario2",CategoryEnum.adventure,dir+@"\Images\default_user.png",4.7f),
            new Game("Smash bros",CategoryEnum.fighting,dir+@"\Images\default_user.png",4.7f),
            new Game("Dying Light",CategoryEnum.adventure,dir+@"\Images\default_user.png",4.6f),

        };
        //KONTA MUSZĄ BYĆ UNIKALNE
        public List<User> users { get; set; }=new List<User>() {
        new User("Piotrek","haslo",false,new List<int>(){ 0,1,2},dir+@"\Images\mario.png"),
        new User("Bartek","Bartek",false,new List<int>(){ 2}, dir + @"\Images\mario.png"),
        new User("Michał","Napiórkowski",false,new List<int>(){ 1,2}, dir + @"\Images\mario.png"),
        };
        public User Login(string login,string pass)// zwraca null w przypadku braku konta
        {
            return users.Where(u => u.login == login && u.password == pass).FirstOrDefault();
        }*/


    }
}
