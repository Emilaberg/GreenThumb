using GreenThumb.Controllers;
using GreenThumb.Managers;
using System.Windows;

namespace GreenThumb.Views
{
    /// <summary>
    /// Interaction logic for RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        public RegistrationWindow()
        {
            InitializeComponent();
        }

        private void BtnGoBack_Click(object sender, RoutedEventArgs e)
        {
            ViewManager.LoginWindow().Show();
            Close();
        }

        private async void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            if (await ValidationController.RegisterUserAsync(txtUsername.Text, txtPassword.Password, txtConfirmPassword.Password))
            {
                MyGardenWindow myGardenWindow = ViewManager.MyGardenWindow();
                Close();
                myGardenWindow.Show();
            }
        }
    }
}
