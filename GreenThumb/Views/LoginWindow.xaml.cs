using GreenThumb.Controllers;
using GreenThumb.Managers;
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

        }


        public async void Login()
        {
            if (await ValidationController.LoginUserAsync(txtUsername.Text, txtPassword.Password))
            {
                MyGardenWindow myGardenWindow = ViewManager.MyGardenWindow();
                Close();
                myGardenWindow.Show();
            }
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            Login();
        }

        private void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            ViewManager.RegistrationWindow().Show();
            Close();
        }
    }
}
