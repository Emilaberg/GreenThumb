using GreenThumb.Database;
using GreenThumb.Managers;
using System.Windows;

namespace GreenThumb.Controllers
{
    public static class ValidationController
    {
        public static bool ValidateLogin(string username, string password)
        {
            //om username och password är korrekt skrivna, kör loginUser()
            if (username == "" && password == "") //Om man inte skrivit in nånting alls
            {
                MessageBox.Show("you need to type a username and a password", "Alert");
                return false;
            }
            else if (username == "" && password != "") //om man inte skrivit in ett username
            {
                MessageBox.Show("you need to type a username", "Alert");
                return false;
            }
            else if (username != "" && password == "") // om man inte skrivit in ett password 
            {
                MessageBox.Show("you need to type a password", "Alert");
                return false;
            }
            else
            {
                //username måste vara större än 5 och  mindre än 10, och lösenordet större än 5 och mindre än 50
                if (username.Length > 5 && username.Length < 10 && password.Length > 5 && password.Length < 50)
                {
                    using (GreenThumbDbContext context = new())
                    {
                        //creating a new authmanager
                        AuthManager authManager = new(new UnitOfWorkRepository(context));
                        //try to log the user in.
                        var user = authManager.Login(username, password);
                        if (user != null)
                        {
                            SessionManager.UserSessionId = user.UserId;
                            return true;
                        }
                        MessageBox.Show("There are no user registrerd with these credentials.", "Alert");
                        return false;
                    }
                }
                MessageBox.Show($" {SessionManager.UserSessionId}  username must be 5-10 characters and password between 5-50 characters and/or number ", "Alert");
                return false;


            }
        }
    }
}
