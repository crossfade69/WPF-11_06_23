using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

public class User
{
    //public static int idCount = 0;
    public int id { get; set; }

    public bool isAdmin { get; set; }
    public string login { get; set; }//login

    public string password { get; set; }//haslo

    public float currency { get; set; } = 0;

    public BitmapImage avatar = null;

    public List<int> games { get; set; }// user trzyma id do gier a nie cale obiekty
    public User(string n,string p,bool isA,List<int> gamesList, string imageDir) 
    {
        BitmapImage newImage = new BitmapImage();//przygotowanie obrazka do wyswieltenia // nie tykac
        newImage.BeginInit();
        newImage.UriSource = new Uri(imageDir);
        newImage.EndInit();
        avatar = newImage;

        this.password = p;
        isAdmin = isA;
        this.login = n;
        //id= idCount++;
        games = gamesList;
    }

    public User(int id,string n, string p, bool isA, List<int> gamesList, string imageDir)
    {
        BitmapImage newImage = new BitmapImage();//przygotowanie obrazka do wyswieltenia // nie tykac
        newImage.BeginInit();
        newImage.UriSource = new Uri(imageDir);
        newImage.EndInit();
        avatar = newImage;

        this.password = p;
        isAdmin = isA;
        this.login = n;
        //id= idCount++;
        this.id = id;
        games = gamesList;
    }

}
