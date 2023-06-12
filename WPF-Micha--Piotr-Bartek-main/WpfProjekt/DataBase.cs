using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace WpfProjekt
{
    public class DataBase
    {               
        public List<Game> games = new List<Game>()
        {
            new Game("Mario",DateTime.Now,@"C:\Users\Bartek\Documents\GitHub\WPF-11_06_23\WPF-Micha--Piotr-Bartek-main\WpfProjekt\Images\mario.png")
        };
        //KONTA MUSZĄ BYĆ UNIKALNE
        public List<User> users { get; set; }=new List<User>() {
        new User("Piotrek","haslo",false),
        new User("Bartek","Bartek",false),
        new User("Michał","Napiórkowski",false),
        };
        public User Login(string login,string pass)// zwraca null w przypadku braku konta
        {
            return users.Where(u => u.login == login).FirstOrDefault();
        }
    }
}
