using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

public class User : INotifyPropertyChanged
{
    public int id { get; set; }

    public bool isAdmin { get; set; }

    private string _login;
    public string login
    {
        get { return _login; }
        set { _login = value; OnPropertyChanged("DisplayUsernameAndLogin"); }
    }
    private string _username;
    public string username
    {
        get { return _username; }
        set { _username = value; OnPropertyChanged("DisplayUsernameAndLogin"); }
    }
    public string password { get; set; }//haslo

    public float currency { get; set; } = 0;

    public string imageDir;
    public BitmapImage avatar = null;

    public List<int> games { get; set; }// user trzyma id do gier a nie cale obiekty
    public User(string n, string u, string p, bool isA, List<int> gamesList, string imageDir)
    {
        BitmapImage newImage = new BitmapImage();//przygotowanie obrazka do wyswieltenia // nie tykac
        newImage.BeginInit();
        newImage.UriSource = new Uri(imageDir);
        newImage.EndInit();
        avatar = newImage;

        this.password = p;
        isAdmin = isA;
        this.login = n;
        this.username = u;
        games = gamesList;
    }

    public User(int id, string n, string u, string p, bool isA, List<int> gamesList, string imageDir)
    {
        this.imageDir = imageDir;
        BitmapImage newImage = new BitmapImage();//przygotowanie obrazka do wyswieltenia // nie tykac
        newImage.BeginInit();
        newImage.UriSource = new Uri(imageDir);
        newImage.EndInit();
        avatar = newImage;

        this.password = p;
        isAdmin = isA;
        this.login = n;
        this.id = id;
        this.username = u;
        games = gamesList;
    }

    public string DisplayUsernameAndLogin
    {
        get
        {
            return $"{this.username} ({this.login})";
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;
    public void OnPropertyChanged(string property)
    {
        if (PropertyChanged != null)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
    }
}
