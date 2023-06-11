using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    public class Session
    {
    public User currentUser;
    public static Session instance=new Session();
    public DataBase dataBase;
    private Session() 
    {
        dataBase = new DataBase();
    }

    public bool Login(string log,string pas)
    {
        currentUser=dataBase.Login(log, pas);
        if(currentUser!=null)
        {
            
            return true;
        }
        return false;
    }


    }

