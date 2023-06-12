using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

public class Game
{
    public static int idCount = 0;
    public string Login;
    public int id;
    public DateTime relase;
    public BitmapImage image;
    public Game(string n, DateTime r, string imageDir)
    {
        BitmapImage newImage = new BitmapImage();
        newImage.BeginInit();
        newImage.UriSource = new Uri(imageDir);
        newImage.EndInit();
        id = idCount++;
        Login = n;
        relase = r;
        image = newImage;
    }
}

