using GreenThumb.Database;
using GreenThumb.Managers;
using GreenThumb.Models;
using System.Windows;

namespace GreenThumb.Controllers
{
    public static class ValidationController
    {
        public async static Task<bool> LoginUserAsync(string username, string password)
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
                using (GreenThumbDbContext context = new())
                {
                    //creating a new authmanager
                    AuthManager authManager = new(new UnitOfWorkRepository(context));

                    //TODOOOOOOO await??
                    var user = await authManager.LoginAsync(username, password);
                    if (user != null)
                    {
                        AuthManager.CurrentUser = user;
                        return true;
                    }
                    MessageBox.Show("Password or Username was incorrect, try again", "Alert");
                    return false;
                }
            }
        }

        public async static Task<bool> RegisterUserAsync(string username, string password, string confirmPassword)
        {

            if (username == "" && password == "" && confirmPassword == "") //Om man inte skrivit in nånting alls
            {
                MessageBox.Show("you need to type a username, password and confirm password", "Alert");
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
            else if (username != "" && password != "" && confirmPassword == "") // confirm password är tom
            {
                MessageBox.Show("you need to confirm password", "Alert");
                return false;
            }
            else
            {
                if (password != confirmPassword)
                {
                    MessageBox.Show("password was not the same, try again", "Alert");
                    return false;
                }
                else if (username.Length > 4 && username.Length < 11 && password.Length > 4 && password.Length < 51)
                {
                    using (GreenThumbDbContext context = new())
                    {
                        AuthManager authManager = new(new UnitOfWorkRepository(context));
                        var user = await authManager.RegisterAsync(username, password);
                        if (user != null)
                        {
                            AuthManager.CurrentUser = user;
                            return true;
                        }
                        MessageBox.Show($"username {username} is already taken, please select a different username.");
                        return false;
                    }
                }
                MessageBox.Show($"username must be 5-10 characters and password between 5-50 characters and/or number ", "Alert");
                return false;
            }

        }

        public async static Task<bool> ContainsPlant(string PlantName)
        {
            using (GreenThumbDbContext context = new())
            {
                UnitOfWorkRepository uow = new(context);

                Plant? res = await uow.PlantRepository.GetPlantByName(PlantName);
                if(res != null)
                {
                    return true;
                }
                return false;
            }
            //hämta den plantan med skapat, 
            //om den är null, returnera false 
            //annars return true
        }
    }
}
