using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data.Entity;

namespace WpfProjekt
{
    public class DataBase
    {
        public static string dir = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString()).ToString();
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

            string createUserTableQuery = "CREATE TABLE IF NOT EXISTS Users (Id INTEGER PRIMARY KEY AUTOINCREMENT, Login TEXT, Password TEXT, isAdmin INTEGER, ImagePath TEXT);";
            string createGameTableQuery = "CREATE TABLE IF NOT EXISTS Games (Id INTEGER PRIMARY KEY AUTOINCREMENT, Title TEXT, Category INTEGER, ImagePath TEXT, Rating REAL);";
            string createGameUserTableQuery = "CREATE TABLE IF NOT EXISTS UserGames (UserId INTEGER, GameId INTEGER, FOREIGN KEY(UserId) REFERENCES Users(Id), FOREIGN KEY(GameId) REFERENCES Games(Id));";

            ExecuteQuery(createUserTableQuery);
            ExecuteQuery(createGameTableQuery);
            ExecuteQuery(createGameUserTableQuery);
        }

        private void InsertInitialData()
        {
            //Dodanie użytkowników
            ExecuteQuery("INSERT INTO Users (Login, Password, isAdmin, ImagePath, GameIds) VALUES ('Piotrek', 'haslo',0,'" + dir + @"\Images\default_user.png" + "');");
            ExecuteQuery("INSERT INTO Users (Login, Password, isAdmin, ImagePath, GameIds) VALUES ('Bartek', 'Bartek',0,'" + dir + @"\Images\default_user.png" + "');");
            ExecuteQuery("INSERT INTO Users (Login, Password, isAdmin, ImagePath, GameIds) VALUES ('Michał', 'Napiórkowski',1,'" + dir + @"\Images\default_user.png" + ");");

            //Dodanie gier
            ExecuteQuery("INSERT INTO Games (Title, Category, ImagePath, Rating) VALUES ('Mario', 0,'" + dir + @"\Images\default_user.png" + "', 4.8);");
            ExecuteQuery("INSERT INTO Games (Title, Category, ImagePath, Rating) VALUES ('Mario2', 0,'" + dir + @"\Images\default_user.png" + "', 4.7);");
            ExecuteQuery("INSERT INTO Games (Title, Category, ImagePath, Rating) VALUES ('Smash bros', 1,'" + dir + @"\Images\default_user.png" + "', 4.7);");
            ExecuteQuery("INSERT INTO Games (Title, Category, ImagePath, Rating) VALUES ('Dying Light', 0, '" + dir + @"\Images\default_user.png" + "', 4.6);");

            // Dodanie użytkownikom ich gier
            ExecuteQuery("INSERT INTO UserGames (UserId, GameId) VALUES (0, 0);");
            ExecuteQuery("INSERT INTO UserGames (UserId, GameId) VALUES (0, 1);");
            ExecuteQuery("INSERT INTO UserGames (UserId, GameId) VALUES (0, 2);");
            ExecuteQuery("INSERT INTO UserGames (UserId, GameId) VALUES (1, 2);");
            ExecuteQuery("INSERT INTO UserGames (UserId, GameId) VALUES (2, 1);");
            ExecuteQuery("INSERT INTO UserGames (UserId, GameId) VALUES (2, 2);");

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
            int id;
            string username, password, imagePath;
            bool isAdmin;
            List<int> gameIds;

            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        id = reader.GetInt32(0);
                        username = reader.GetString(1);
                        password = reader.GetString(2);
                        isAdmin = reader.GetInt32(3) != 0;
                        imagePath = reader.GetString(4);
                        gameIds = GetGameIdsForUserById(id);
                        user = new User(id,username, password, isAdmin, gameIds, imagePath);
                    }
                }
            }

            return user;
        }


        private List<int> GetGameIdsForUserById(int id)
        {
            List<int> gamesIds = new List<int>();
            string query = $"SELECT GameId FROM UserGames WHERE UserId = {id};";
            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int gameId = reader.GetInt32(2);
                        gamesIds.Add(gameId);
                    }
                }
            }

            return gamesIds;
        }


        public List<Game> QueryGames(string query)
        {
            List<Game> games = new List<Game>();

            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string title = reader.GetString(1);
                        CategoryEnum category = (CategoryEnum)reader.GetInt32(2);
                        string imagePath = reader.GetString(3);
                        float rating = (float)reader.GetDouble(4);

                        Game game = new Game(title, category, imagePath, rating);
                        games.Add(game);
                    }
                }
            }

            return games;
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
