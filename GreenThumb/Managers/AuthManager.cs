using GreenThumb.Database;
using GreenThumb.Models;

namespace GreenThumb.Managers
{
    public class AuthManager
    {
        private readonly UnitOfWorkRepository _unitOfWork;
        public static int? UserSessionId { get; set; }
        public static User? CurrentUser { get; set; }

        public AuthManager(UnitOfWorkRepository unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        //Validation

        //kolla om den inskickade usern finns i databasen, 
        //om den finns
        //returnera user
        private async Task<User?> ValidateAsync(string username, string password)
        {
            var user = await _unitOfWork.UserRepository.GetUserByCredentialsAsync(username, password);
            if (user != null)
            {
                return user;
            }
            else
            {
                return null;
            }

        }

        //Login 
        //kör validation
        //om validation är true logga in användaren
        public async Task<User?> LoginAsync(string username, string password)
        {
            var user = await ValidateAsync(username, password);
            if (user != null)
            {

                return user;

            }
            return null;

        }

        //Register
        //logga in användaren
        public async Task<User?> RegisterAsync(string username, string password)
        {
            if (await IsUsernameTakenAsync(username))
            {
                return null;
            }

            User newUser = new()
            {
                Name = username,
                Password = password
            };
            await _unitOfWork.UserRepository.CreateUserAsync(newUser);
            _unitOfWork.Complete();
            return await _unitOfWork.UserRepository.GetUserByCredentialsAsync(username, password);
        }

        public async Task<bool> IsUsernameTakenAsync(string username)
        {
            if (await _unitOfWork.UserRepository.IsTakenAsync(username) == false)
            {
                return false;
            }
            return true;
        }

        public static void CloseSession()
        {
            UserSessionId = null;
            CurrentUser = null;
        }
    }
}
