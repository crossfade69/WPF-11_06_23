using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

public class Game
    {
    public static int idCount=0;
    public string Login;
    public int id;
    public DateTime relase;
    public BitmapImage image;
    public Game(string n,DateTime r,BitmapImage i) 
    {
        id = idCount++;
        Login = n;
        relase = r;
        image = i;
    }
    }

