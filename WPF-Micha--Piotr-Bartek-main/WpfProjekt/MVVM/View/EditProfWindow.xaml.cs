using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static System.Collections.Specialized.BitVector32;

namespace WpfProjekt
{
    /// <summary>
    /// Logika interakcji dla klasy EditProfWindow.xaml
    /// </summary>
    public partial class EditProfWindow : Window
    {
        private Session session = Session.GetInstance();
        public EditProfWindow()
        {
            InitializeComponent();
            DisplayUserInfo();
        }

        private void DisplayUserInfo()
        {
            if (session.currentUser != null)
            {
                loginValue.Text = session.currentUser.login;
                passwordValue.Text = session.currentUser.password;
                usernameValue.Text = session.currentUser.username;
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (ConfirmDataChange())
            {
                session.currentUser.username = usernameValue.Text;
                session.currentUser.password = passwordValue.Text;
                session.currentUser.login = loginValue.Text;
            }
        }

        private bool ConfirmDataChange()
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to change your data?", "Confirmation", MessageBoxButton.YesNo);
            return result == MessageBoxResult.Yes;
        }


    }
}
