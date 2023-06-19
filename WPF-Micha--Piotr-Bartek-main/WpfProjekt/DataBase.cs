﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data.Entity;
using System.Runtime.Remoting.Messaging;

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

            string createUserTableQuery = "CREATE TABLE IF NOT EXISTS Users (Id INTEGER PRIMARY KEY AUTOINCREMENT, Login TEXT, Username TEXT, Password TEXT, isAdmin INTEGER, ImagePath TEXT);";
            string createGameTableQuery = "CREATE TABLE IF NOT EXISTS Games (Id INTEGER PRIMARY KEY AUTOINCREMENT, Title TEXT, Category INTEGER, ImagePath TEXT, Rating REAL);";
            string createGameUserTableQuery = "CREATE TABLE IF NOT EXISTS UserGames (UserId INTEGER, GameId INTEGER, FOREIGN KEY(UserId) REFERENCES Users(Id), FOREIGN KEY(GameId) REFERENCES Games(Id));";

            ExecuteQuery(createUserTableQuery);
            ExecuteQuery(createGameTableQuery);
            ExecuteQuery(createGameUserTableQuery);
        }

        private void InsertInitialData()
        {
            //Dodanie użytkowników

            string imagePath = dir + @"\Images\default_user.png";
            string query = "INSERT INTO Users (Login, Username, Password, isAdmin, ImagePath) VALUES (@Login, @Username, @Password, @IsAdmin, @ImagePath);";

            string[] logins = new string[] { "Piotrek", "Bartek", "Michał" };
            string[] usernames = new string[] { "Enigma", "Vector", "Doomer" };
            string[] passwords = new string[] { "haslo", "Bartek", "Napiórkowski" };
            int[] isAdmins = new int[] { 0, 0, 1 };

            for (int i = 0; i < logins.Length; i++)
            {
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Login", logins[i]);
                    command.Parameters.AddWithValue("@Username", usernames[i]);
                    command.Parameters.AddWithValue("@Password", passwords[i]);
                    command.Parameters.AddWithValue("@IsAdmin", isAdmins[i]);
                    command.Parameters.AddWithValue("@ImagePath", imagePath);
                    command.ExecuteNonQuery();
                }
            }

            //Dodanie gier
            query = "INSERT INTO Games (Title, Category, ImagePath, Rating) VALUES (@Title, @Category, @ImagePath, @Rating);";

            string[] titles = new string[] { "Mario", "Mario2", "Smash bros", "Dying Light", "Cyberpunk 2077", "Witcher 3" };
            string[] categories = new string[] { "adventure", "adventure", "fighting", "adventure", "FPS", "RPG" };
            string[] imagesPaths = new string[] { dir + @"\Images\mario.png", dir + @"\Images\mario2.png", dir + @"\Images\Smash.png",
               dir +  @"\Images\dl.png", dir + @"\Images\cyperpunk.png", dir +  @"\Images\witcher.png" };                                                                                     // tutaj zdjecia gier
            float[] ratings = new float[] { 4.8f, 4.7f, 4.7f, 4.6f, 3.5f, 5.0f };

            for (int i = 0; i < titles.Length; i++)
            {
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Title", titles[i]);
                    command.Parameters.AddWithValue("@Category", categories[i]);
                    command.Parameters.AddWithValue("@ImagePath", imagesPaths[i]);
                    command.Parameters.AddWithValue("@Rating", ratings[i]);
                    command.ExecuteNonQuery();
                }
            }

            // Dodanie użytkownikom ich gier
            /*query = "INSERT INTO UserGames (UserId, GameId) VALUES (@UserId, @GameId);";
            int[] userIds = new int[] { 0, 1, 2 };
            List<int>[] gameIds = new List<int>[] { new List<int> { 0, 1, 2 }, new List<int> { 2 }, new List<int> { 1, 2 } };


            for (int i = 0; i < userIds.Length; i++)
            {
                foreach (int gameId in gameIds[i])
                {
                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserId", userIds[i]);
                        command.Parameters.AddWithValue("@GameId", gameId);
                        command.ExecuteNonQuery();
                    }
                }

            }*/
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
            string loginstring, username, password, imagePath;
            bool isAdmin;
            List<int> gameIds;

            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        id = reader.GetInt32(0);
                        loginstring = reader.GetString(1);
                        username = reader.GetString(2);
                        password = reader.GetString(3);
                        isAdmin = reader.GetInt32(4) != 0;
                        imagePath = reader.GetString(5);
                        gameIds = GetGameIdsForUserById(id);
                        //Console.WriteLine(id.ToString() + ":" + gameIds.ToString());
                        user = new User(id,loginstring, username, password, isAdmin, gameIds, imagePath);
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
                        int gameId = reader.GetInt32(0);
                        gamesIds.Add(gameId);
                    }
                }
            }
            return gamesIds;
        }


        public List<Game> QueryGames(string query)
        {
            //string query1 = $"SELECT * FROM Games INNER JOIN UserGames ON Games.Id = UserGames.GameId WHERE UserGames.UserId = 2;";

            List<Game> games = new List<Game>();

            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        string title = reader.GetString(1);
                        CategoryEnum category = (CategoryEnum)Enum.Parse(typeof(CategoryEnum), reader.GetString(2));
                        string imagePath = reader.GetString(3);
                        float rating = (float)reader.GetDouble(4);

                        Game game = new Game(id, title, category, imagePath, rating);
                        games.Add(game);
                    }
                }
            }

            return games;
        }


        public List<User> QueryUsers(string query)
        {
            List<User> users = new List<User>();
            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        string login = reader.GetString(1);
                        string username = reader.GetString(2);
                        string password = reader.GetString(3);
                        bool isAdmin = reader.GetInt32(4) != 0;
                        string imagePath = reader.GetString(5);
                        List<int> gameIds = GetGameIdsForUserById(id);

                        User user = new User(id, login, username, password, isAdmin, gameIds, imagePath);
                        users.Add(user);
                    }
                }
            }

            return users;
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
