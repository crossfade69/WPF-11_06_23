using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    public class User
    {
    public static int idCount=0;
    public int id { get; set; }

    public bool isAdmin { get; set; }
    public string Login { get; set; }//login

    public string password { get; set; }//haslo

    public List<int> games { get; set; }
    public User(string n,string p,bool isA) 
    {
        this.password = p;
        isAdmin = isA;
        this.Login = n;
        id= idCount++;
        games = new List<int>();
    }

    
    }
