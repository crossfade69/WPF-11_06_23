using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

public class User
{
    public static int idCount = 0;
    public int id { get; set; }

    public bool isAdmin { get; set; }
    public string login { get; set; }//login

    public string password { get; set; }//haslo

    public float currency { get; set; } = 0;

    public BitmapImage avatar = null;

    public List<int> games { get; set; }
    public User(string n,string p,bool isA) 
    {
        this.password = p;
        isAdmin = isA;
        this.login = n;
        id= idCount++;
        games = new List<int>();
    }

    
    }
