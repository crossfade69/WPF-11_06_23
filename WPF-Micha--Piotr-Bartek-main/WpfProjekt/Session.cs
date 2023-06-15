﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using WpfProjekt;

public class Session // statyczny obiekt sesji w którym znajdują się wszytkie potrzebne
                     // informacje takie jak database oraz teraźniejszy uzytkownik
{
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
    public List<Game> GetSortedGamesByCategory()
    {
        return dataBase.games.OrderBy(c => c.category).ToList();
    }
    public List<Game> GetSortedGamesByTitle()
    {
        return dataBase.games.OrderBy(c => c.title).ToList();
    }
    public List<Game> GetSortedGamesByRatings()
    {
        return dataBase.games.OrderBy(c => c.rating).ToList();
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
    public List<Game> GetSortedUSerGamesByCategory()
    {
        return GetUserGames().OrderBy(c=>c.category).ToList(); 
    }
    public List<Game> GetSortedUSerGamesByRatings()
    {
        return GetUserGames().OrderBy(c => c.rating).ToList();
    }
    public List<Game> GetSortedUSerGamesByTitle()
    {
        return GetUserGames().OrderBy(c => c.title).ToList();
    }

    public void AddGame(Game newGame)
    {

        currentUser.games.Add(newGame.id);
    }

    public void DeleteGame(Game deletedGame)
    {
        currentUser.games.Remove(deletedGame.id);
    }
   

}

