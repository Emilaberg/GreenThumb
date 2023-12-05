using GreenThumb.Database;
using GreenThumb.Models;

namespace GreenThumb.Managers
{
    public class AuthManager
    {
        private readonly UnitOfWorkRepository _unitOfWork;

        public AuthManager(UnitOfWorkRepository unitOfWork)
        {
            _unitOfWork = unitOfWork;


        }

        //Validation

        //kolla om den inskickade usern finns i databasen, 
        //om den finns
        //returnera user
        private User? Validate(string username, string password)
        {
            var user = _unitOfWork.UserRepository.GetUserByCredentials(username, password);
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
        public User? Login(string username, string password)
        {
            var user = Validate(username, password);
            if (user != null)
            {

                return user;

            }
            return null;

        }

        //Register
        //logga in användaren
        public User? Register(string username, string password)
        {
            if (IsUsernameTaken(username))
            {
                return null;
            }

            User newUser = new()
            {
                Name = username,
                Password = password
            };
            _unitOfWork.UserRepository.CreateUser(newUser);
            _unitOfWork.Complete();
            return _unitOfWork.UserRepository.GetUserByCredentials(username, password);
        }

        public bool IsUsernameTaken(string username)
        {
            if (_unitOfWork.UserRepository.IsTaken(username) == false)
            {
                return false;
            }
            return true;
        }
    }
}
