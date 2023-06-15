using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace WpfProjekt
{
    public class DataBase
    {
        public static string dir = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString()).ToString();
        public List<Game> games = new List<Game>()
        {
            
            
            new Game("Mario",CategoryEnum.adventure,dir+@"\Images\default_user.png",4.8f),
            new Game("Mario2",CategoryEnum.adventure,dir+@"\Images\default_user.png",4.7f),
            new Game("Smash bros",CategoryEnum.fighting,dir+@"\Images\default_user.png",4.7f),
        };
        //KONTA MUSZĄ BYĆ UNIKALNE
        public List<User> users { get; set; }=new List<User>() {
        new User("Piotrek","haslo",false,new List<int>(){ 0,1,2},dir+@"\Images\mario.png"),
        new User("Bartek","Bartek",false,new List<int>(){ 2}, dir + @"\Images\mario.png"),
        new User("Michał","Napiórkowski",false,new List<int>(){ 1,2}, dir + @"\Images\mario.png"),
        };
        public User Login(string login,string pass)// zwraca null w przypadku braku konta
        {
            return users.Where(u => u.login == login && u.password == pass).FirstOrDefault();
        }


    }
}
