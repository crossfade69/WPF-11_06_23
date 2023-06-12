using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using WpfProjekt;

public class Session
    {
    public User currentUser;
    public static Session instance=new Session();
    public DataBase dataBase;
    private Session() 
    {
        dataBase = new DataBase();
    }

    public bool Login(string log,string pas)//funkcja zwraca true przy udanym logowaniu, oraz przy udanym logowaniu ustawia current usera
    {
        currentUser=dataBase.Login(log, pas);
        if(currentUser!=null)
        {
            
            return true;
        }
        return false;
    }
    public BitmapImage game()
    {
        return dataBase.games.FirstOrDefault().image;
    }


    }

