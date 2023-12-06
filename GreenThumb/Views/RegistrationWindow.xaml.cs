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

        private void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            ViewManager.MyGardenWindow().Show();
            Close();
        }
    }
}
