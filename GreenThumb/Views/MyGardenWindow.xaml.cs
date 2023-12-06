using GreenThumb.Managers;
using System.Windows;

namespace GreenThumb.Views
{
    /// <summary>
    /// Interaction logic for MyGardenWindow.xaml
    /// </summary>
    public partial class MyGardenWindow : Window
    {
        public MyGardenWindow()
        {
            InitializeComponent();
        }


        private void BtnPlantDetails_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnRemoveGarden_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnAddGarden_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnLogout_Click(object sender, RoutedEventArgs e)
        {
            ViewManager.LoginWindow().Show();
            Close();
        }

        private void BtnAccount_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
