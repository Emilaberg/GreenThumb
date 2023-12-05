using GreenThumb.Controllers;
using GreenThumb.Managers;
using System.Windows;

namespace GreenThumb
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();


            //using (GreenThumbDbContext context = new())
            //{
            //    UnitOfWorkRepository uow = new(context);

            //    User user = new()
            //    {
            //        Name = "user",
            //        Password = "hej123"
            //    };
            //    uow.UserRepository.CreateUser(user);
            //    uow.Complete();
            //}
            //if (ValidationController.ValidateRegister("Admin2", "hej123", "hej123"))
            //{
            //    MessageBox.Show($"{SessionManager.UserSessionId}");
            //}

            if (ValidationController.ValidateLogin("Admin2", "hej123"))
            {
                MessageBox.Show($"{SessionManager.UserSessionId}");
            }


        }
    }
}