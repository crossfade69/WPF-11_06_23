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

public class Game : INotifyPropertyChanged
{
    //public static int idCount = 0; //kazda ma odzielne id
    private string _title;
    public string title
    {
        get { return _title; }
        set { _title = value; OnPropertyChanged("DisplayTitle"); }
    }
    public int id { get; set; }
    public float rating { get; set; } // ocena od 1 do 5
    public int votes { get; set; } = 1;//jak wiele osob glosowalo
    public string imageDir { get; set; }
    public BitmapImage image { get; set; }
    //public string image;
    public CategoryEnum category { get; set; }// typy gier są w enumie dla ułatwienia nam wpisywania

    public Game(int id, string n, CategoryEnum cat, string imageDir, float rat)
    {
        this.imageDir = imageDir;
        BitmapImage newImage = new BitmapImage();//przygotowanie obrazka do wyswieltenia // nie tykac
        try
        {
            newImage.BeginInit();
            newImage.UriSource = new Uri(imageDir);
            newImage.EndInit();
            //id = idCount++;
        }
        catch (UriFormatException ex)
        {
            throw ex;
        }


        this.id = id;
        title = n;
        category = cat;
        rating = rat;
        image = newImage;
        //image = imageDir;
    }

    public override string ToString()
    {
        return $"{this.title}";
    }

    public string DisplayTitle
    {
        get
        {
            return this.title;
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


