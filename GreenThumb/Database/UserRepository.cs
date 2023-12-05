using GreenThumb.Models;

namespace GreenThumb.Database
{
    public class UserRepository
    {
        private readonly GreenThumbDbContext _context;

        public UserRepository(GreenThumbDbContext context)
        {
            _context = context;
        }

        public List<User> GetAll()
        {
            return _context.Users.ToList();
        }
        //Method for login. 
        public User? GetUserByCredentials(string username, string password)
        {
            return _context.Users.FirstOrDefault(u => u.Name == username && u.Password == password);
        }

        //show All
        public User? GetUserById(int id)
        {

            return _context.Users.FirstOrDefault(u => u.UserId == id);
        }

        //Create

        public void CreateUser(User user)
        {
            _context.Users.Add(user);
        }

        //update

        public void UpdateSelectedUser(int id, User updatedUser)
        {
            User? userToUpdate = _context.Users.FirstOrDefault(u => u.UserId == id);

            if (userToUpdate != null)
            {
                userToUpdate.Name = updatedUser.Name;
                userToUpdate.Password = updatedUser.Password;

                ////kanske inte behövs?
                //userToUpdate.Garden = updatedUser.Garden;
            }
        }

        //delete

        public void RemoveSelectedUser(int id)
        {
            User? userToRemove = GetUserById(id);

            if (userToRemove != null)
            {
                _context.Users.Remove(userToRemove);
            }

        }


        public void Complete()
        {
            _context.SaveChanges();
        }

    }
}
