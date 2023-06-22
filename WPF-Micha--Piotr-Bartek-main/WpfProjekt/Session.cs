using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using WpfProjekt;
using System.Data.SQLite;
using System.Data.Entity;
using System.IO;
using System.Data.SqlClient;
using System.Windows.Media;

public class Session // statyczny obiekt sesji w którym znajdują się wszytkie potrzebne
                     // informacje takie jak database oraz teraźniejszy uzytkownik
{

    public User currentUser;
    private static Session instance;
    public DataBase dataBase;
    public static string dir = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString()).ToString();

    public static Session GetInstance()
    {
        if (instance == null)
            instance = new Session();
        return instance;
    }

    private Session()
    {
        dataBase = new DataBase();
    }

    public bool Login(string log, string pas)
    {
        currentUser = dataBase.Login(log, pas);
        if (currentUser != null)
        {
            return true;
        }
        return false;
    }
    public void Logout()
    {
        currentUser = null;
        dataBase.Logout();
    }
    private List<Game> QueryGames(string query)
    {
        return dataBase.QueryGames(query);
    }

    private List<User> QueryUsers(string query)
    {
        return dataBase.QueryUsers(query);
    }

    public List<Game> GetAllGames()
    {
        string query = "SELECT * FROM Games;";
        return QueryGames(query);
    }

    public List<User> GetAllUsers()
    {
        string query = "SELECT * FROM Users;";
        return QueryUsers(query);
    }

    public List<Game> GetUserGames()
    {
        if (currentUser == null)
        {
            MessageBox.Show("Wpierw się zaloguj.");
            return null;
        }

        string query = $"SELECT * FROM Games INNER JOIN UserGames ON Games.Id = UserGames.GameId WHERE UserGames.UserId = {currentUser.id};";
        Console.WriteLine(currentUser.id);
        List<Game> games = QueryGames(query);

        return games;
    }

    public List<Game> GetSortedGamesByCategory(bool sortCategoryAsc)
    {
        string query = sortCategoryAsc ? "SELECT * FROM Games ORDER BY Category ASC;" : "SELECT * FROM Games ORDER BY Category DESC;";
        return QueryGames(query);
    }

    public List<Game> GetSortedGamesByTitle(bool sortTitleAsc)
    {
        string query = sortTitleAsc ? "SELECT * FROM Games ORDER BY Title ASC;" : "SELECT * FROM Games ORDER BY Title DESC;";
        return QueryGames(query);
    }

    public List<Game> GetSortedGamesByRatings(bool sortRatingAsc)
    {
        string query = sortRatingAsc ? "SELECT * FROM Games ORDER BY Rating ASC;" : "SELECT * FROM Games ORDER BY Rating DESC;";
        return QueryGames(query);
    }

    public List<Game> GetSortedUserGamesByCategory(bool sortCategoryAsc)
    {
        string query = sortCategoryAsc
            ? $"SELECT Games.* FROM Games INNER JOIN UserGames ON Games.Id = UserGames.GameId WHERE UserGames.UserId = {currentUser.id} ORDER BY Games.Category ASC;"
            : $"SELECT Games.* FROM Games INNER JOIN UserGames ON Games.Id = UserGames.GameId WHERE UserGames.UserId = {currentUser.id} ORDER BY Games.Category DESC;";

        return QueryGames(query);
    }

    public List<Game> GetSortedUserGamesByRatings(bool sortRatingAsc)
    {
        string query = sortRatingAsc
            ? $"SELECT Games.* FROM Games INNER JOIN UserGames ON Games.Id = UserGames.GameId WHERE UserGames.UserId = {currentUser.id} ORDER BY Games.Rating ASC;"
            : $"SELECT Games.* FROM Games INNER JOIN UserGames ON Games.Id = UserGames.GameId WHERE UserGames.UserId = {currentUser.id} ORDER BY Games.Rating DESC;";

        return QueryGames(query);
    }

    public List<Game> GetSortedUserGamesByTitle(bool sortTitleAsc)
    {
        string query = sortTitleAsc
            ? $"SELECT Games.* FROM Games INNER JOIN UserGames ON Games.Id = UserGames.GameId WHERE UserGames.UserId = {currentUser.id} ORDER BY Games.Title ASC;"
            : $"SELECT Games.* FROM Games INNER JOIN UserGames ON Games.Id = UserGames.GameId WHERE UserGames.UserId = {currentUser.id} ORDER BY Games.Title DESC;";

        return QueryGames(query);
    }

    public bool AddGame(Game newGame)
    {
        if (currentUser == null)
        {
            MessageBox.Show("Wpierw się zaloguj.");
            return false;
        }

        // Sprawdzenie, czy użytkownik ma już posiadaną grę
        string checkQuery = $"SELECT COUNT(*) FROM UserGames WHERE UserId = {currentUser.id} AND GameId = {newGame.id};";
        int count = 0;

        using (SQLiteCommand checkCommand = new SQLiteCommand(checkQuery, dataBase.connection))
        {
            object result = checkCommand.ExecuteScalar();
            if (result != null)
            {
                count = Convert.ToInt32(result);
            }
        }

        if (count > 0)
        {
            return false;
        }

        string query = $"INSERT INTO UserGames (UserId, GameId) VALUES ({currentUser.id}, {newGame.id});";
        using (SQLiteCommand command = new SQLiteCommand(query, dataBase.connection))
        {
            command.ExecuteNonQuery();
        }

        return true;
    }

    public void DeleteGame(Game deletedGame)
    {
        if (currentUser == null)
        {
            MessageBox.Show("Wpierw się zaloguj.");
            return;
        }

        string query = $"DELETE FROM UserGames WHERE UserId = {currentUser.id} AND GameId = {deletedGame.id};";
        using (SQLiteCommand command = new SQLiteCommand(query, dataBase.connection))
        {
            command.ExecuteNonQuery();
        }
    }

    public User AddUser(string login, string username, string password, bool isAdmin)
    {
        // Check if the user already exists
        if (currentUser == null || !currentUser.isAdmin || isThereUserWithThisLogin(login))
        {
            return null;
        }

        string defaultUserImagePath = dir + @"\Images\default_user.png";
        string query = "INSERT INTO Users (Login, Username, Password, isAdmin, ImagePath) VALUES (@Login, @Username, @Password, @IsAdmin, @ImagePath);";

        using (SQLiteCommand command = new SQLiteCommand(query, dataBase.connection))
        {
            command.Parameters.AddWithValue("@Login", login);
            command.Parameters.AddWithValue("@Username", username);
            command.Parameters.AddWithValue("@Password", password);
            command.Parameters.AddWithValue("@IsAdmin", isAdmin ? 1 : 0);
            command.Parameters.AddWithValue("@ImagePath", defaultUserImagePath);
            command.ExecuteNonQuery();
        }
        int newUserId = -1;
        // Get the ID of the newly created user
        string getUserIdQuery = "SELECT last_insert_rowid();";
        using (SQLiteCommand getUserIdCommand = new SQLiteCommand(getUserIdQuery, dataBase.connection))
        {
            object result = getUserIdCommand.ExecuteScalar();
            if (result != null)
            {
                newUserId = Convert.ToInt32(result);
            }
        }
        if (newUserId == -1)
        {
            return null;
        }

        List<int> gamesList = new List<int>();
        User newUser = new User(newUserId, login, username, password, isAdmin, gamesList, defaultUserImagePath);
        return newUser;
    }

    public bool isThereUserWithThisLogin(string login)
    {
        string checkQuery = $"SELECT COUNT(*) FROM Users WHERE Login = '{login}';";
        int count = 0;

        using (SQLiteCommand checkCommand = new SQLiteCommand(checkQuery, dataBase.connection))
        {
            object result = checkCommand.ExecuteScalar();
            if (result != null)
            {
                count = Convert.ToInt32(result);
            }
        }

        if (count > 0)
        {
            return true;
        }
        return false;
    }

    public bool DeleteUser(int userId)
    {
        string deleteQuery = $"DELETE FROM Users WHERE Id = {userId};";
        try
        {
            using (SQLiteCommand command = new SQLiteCommand(deleteQuery, dataBase.connection))
            {
                command.ExecuteNonQuery();
            }
        }
        catch (SqlException ex)
        {
            return false;
        }
        return true;
    }

    public int AddGameToDatabase(string title, string category, string imagePath, float rating)
    {
        if (currentUser == null)
        {
            MessageBox.Show("Wpierw się zaloguj.");
            return -1;
        }
        string query = "INSERT INTO Games (Title, Category, ImagePath, Rating) VALUES (@Title, @Category, @ImagePath, @Rating);";
        using (SQLiteCommand command = new SQLiteCommand(query, dataBase.connection))
        {
            command.Parameters.AddWithValue("@Title", title);
            command.Parameters.AddWithValue("@Category", category);
            command.Parameters.AddWithValue("@ImagePath", imagePath);
            command.Parameters.AddWithValue("@Rating", rating);
            command.ExecuteNonQuery();
        }

        query = "SELECT last_insert_rowid();";

        using (SQLiteCommand command = new SQLiteCommand(query, dataBase.connection))
        {
            object result = command.ExecuteScalar();
            if (result != null)
            {
                return Convert.ToInt32(result);
            }
        }
        return -1;
    }

    public void UpdateGameInDatabase(Game game)
    {
        if (currentUser == null)
        {
            MessageBox.Show("Wpierw się zaloguj.");
            return;
        }
        string query = "UPDATE Games SET Title = @Title, Category = @Category, ImagePath = @ImagePath, Rating = @Rating WHERE ID = @ID;";
        using (SQLiteCommand command = new SQLiteCommand(query, dataBase.connection))
        {
            command.Parameters.AddWithValue("@Title", game.title);
            command.Parameters.AddWithValue("@Category", game.category);
            command.Parameters.AddWithValue("@ImagePath", game.imageDir);
            command.Parameters.AddWithValue("@Rating", game.rating);
            command.Parameters.AddWithValue("@ID", game.id);
        }
    }

    public void UpdateUserInDatabase(User user)
    {
        if (currentUser == null)
        {
            MessageBox.Show("Wpierw się zaloguj.");
            return;
        }
        string query = "UPDATE Users SET Login = @Login, Username = @Username, Password = @Password, isAdmin = @IsAdmin, ImagePath = @ImagePath WHERE ID = @ID;";
        using (SQLiteCommand command = new SQLiteCommand(query, dataBase.connection))
        {
            command.Parameters.AddWithValue("@Login", user.login);
            command.Parameters.AddWithValue("@Username", user.username);
            command.Parameters.AddWithValue("@Password", user.password);
            command.Parameters.AddWithValue("@IsAdmin", user.isAdmin);
            command.Parameters.AddWithValue("@ImagePath", user.imageDir);
            command.Parameters.AddWithValue("@ID", user.id);
        }
    }
}

