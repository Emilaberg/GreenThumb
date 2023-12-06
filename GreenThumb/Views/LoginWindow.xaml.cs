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


        public async Task Login()
        {
            if (await ValidationController.LoginUserAsync("Admin1", "hej123"))
            {
                MessageBox.Show($"{AuthManager.UserSessionId} {AuthManager.CurrentUser.Name}");
            }
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            ViewManager.RegistrationWindow().Show();
            Close();
        }
    }
}
