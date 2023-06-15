using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using WpfProjekt;
/// przykład użycia BitmapImage
/// 
///  imageTest.Source = session.dataBase.games.FirstOrDefault().image;

public class Game
{
    public static int idCount = 0; //kazda ma odzielne id
    public string title { get; set; }
    public int id { get; }
    public float rating { get; set; } // ocena od 1 do 5
    public int votes { get; set; } = 1;//jak wiele osob glosowalo
    public BitmapImage image { get; set; }
    //public string image;
    public CategoryEnum category { get; set; }// typy gier są w enumie dla ułatwienia nam wpisywania

    public Game(string n, CategoryEnum cat, string imageDir, float rat) 
    {
        BitmapImage newImage = new BitmapImage();//przygotowanie obrazka do wyswieltenia // nie tykac
        newImage.BeginInit();
        newImage.UriSource = new Uri(imageDir);
        newImage.EndInit();
        id = idCount++;

        title = n;
        category = cat;
        rating = rat;
        image = newImage;
        //image = imageDir;
    }
}


