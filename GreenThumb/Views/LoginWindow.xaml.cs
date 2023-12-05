using GreenThumb.Controllers;
using GreenThumb.Managers;
using System.Security.Cryptography.X509Certificates;
using System.Windows;

namespace GreenThumb.Views
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            Login();


            //if (ValidationController.LoginUser("Admin2", "hej123"))
            //{
            //    MessageBox.Show($"{SessionManager.UserSessionId}");
            //}
        }


        public async Task Login()
        {
            if (await ValidationController.LoginUserAsync("Admin1", "hej123"))
            {
                MessageBox.Show($"{SessionManager.UserSessionId}");
            }
        }
    }
}
