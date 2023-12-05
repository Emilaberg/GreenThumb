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
