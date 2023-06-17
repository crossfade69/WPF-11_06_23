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


public class Session // statyczny obiekt sesji w którym znajdują się wszytkie potrzebne
                     // informacje takie jak database oraz teraźniejszy uzytkownik
{

    public User currentUser;
    private static Session instance;
    public DataBase dataBase;

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
    private List<Game> QueryGames(string query)
    {
        return dataBase.QueryGames(query);
    }

    public List<Game> GetAllGames()
    {
        string query = "SELECT * FROM Games;";
        return QueryGames(query);
    }

    public List<Game> GetUserGames()
    {
        if (currentUser == null)
        {
            MessageBox.Show("Wpierw się zaloguj.");
            return null;
        }

        string query = $"SELECT Games.* FROM Games INNER JOIN UserGames ON Games.Id = UserGames.GameId WHERE UserGames.UserId = {currentUser.id};";
        return QueryGames(query);
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
























    /*
    public User currentUser;
    private static Session instance;
    public DataBase dataBase;
    public static Session GetInstance()// tą funkcją pobieramy naszą sesje
    {
        if (instance == null)
            instance = new Session();
        return instance;
    }
    private Session()
    {
        dataBase = new DataBase();
    }

    public bool Login(string log, string pas)//funkcja zwraca true przy udanym logowaniu, oraz przy udanym logowaniu ustawia current usera
    {
        currentUser = dataBase.Login(log, pas);
        if (currentUser != null)
        {

            return true;
        }
        return false;
    }



    //METODY DO SKLEPU
    public List<Game> GetAllGames()
    {
        return dataBase.games;
    }


    public List<Game> GetSortedGamesByCategory(bool sortCategoryAsc)
    {
        return sortCategoryAsc ? dataBase.games.OrderBy(c => c.category).ToList() : dataBase.games.OrderByDescending(c => c.category).ToList();
    }

    public List<Game> GetSortedGamesByTitle(bool sortTitleAsc)
    {
        return sortTitleAsc ? dataBase.games.OrderBy(c => c.title).ToList() : dataBase.games.OrderByDescending(c => c.title).ToList();
    }

    public List<Game> GetSortedGamesByRatings(bool sortRatingAsc)
    {
        return sortRatingAsc ? dataBase.games.OrderBy(c => c.rating).ToList() : dataBase.games.OrderByDescending(c => c.rating).ToList();
    }


    //METODY DO KOLECKII

    public List<Game> GetUserGames()
    {
        if (currentUser == null)
        {
            MessageBox.Show("wpier sie zaloguj");
            return null;
        }
        List<Game> userGames=new List<Game>();
        foreach (Game game in dataBase.games) 
        {
            foreach(int id in currentUser.games)
            {
                if (id == game.id)
                    userGames.Add(game);
            }
        }

        return userGames;
            
    }
    public List<Game> GetSortedUSerGamesByCategory(bool sortCategoryAsc)
    {
        return sortCategoryAsc ? GetUserGames().OrderBy(c=>c.category).ToList() : GetUserGames().OrderByDescending(c => c.category).ToList(); 
    }
    public List<Game> GetSortedUSerGamesByRatings(bool sortRatingAsc)
    {
        return sortRatingAsc ? GetUserGames().OrderBy(c => c.rating).ToList() : GetUserGames().OrderByDescending(c => c.rating).ToList();
    }
    public List<Game> GetSortedUSerGamesByTitle(bool sortTitleAsc)
    {
        return sortTitleAsc ? GetUserGames().OrderBy(c => c.title).ToList() : GetUserGames().OrderByDescending(c => c.title).ToList();
    }

    public bool AddGame(Game newGame)
    {
        int newGameId = newGame.id;

        foreach (int id in currentUser.games)
        {
            if(id == newGameId)
            {
                return false;
            }
        }
        currentUser.games.Add(newGameId);
        return true;
    }

    public void DeleteGame(Game deletedGame)
    {
        currentUser.games.Remove(deletedGame.id);
    }
    */

}

