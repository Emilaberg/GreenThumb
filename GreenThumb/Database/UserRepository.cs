using GreenThumb.Models;
using Microsoft.EntityFrameworkCore;

namespace GreenThumb.Database
{
    public class UserRepository
    {
        //RULE!! 
        //de metoder som returnera nånting, sätter jag till async await metoder, och de som endast skapar eller raderar

        private readonly GreenThumbDbContext _context;

        public UserRepository(GreenThumbDbContext context)
        {
            _context = context;
        }


        public async Task<bool> IsTakenAsync(string username)
        {
            if (await _context.Users.FirstOrDefaultAsync(u => u.Name == username) == null)
            {
                return false;
            }
            return true;
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        //Method for login. 
        public async Task<User?> GetUserByCredentialsAsync(string username, string password)
        {
            return await _context.Users.Include(u => u.Garden).FirstOrDefaultAsync(u => u.Name == username && u.Password == password);
        }

        //show All
        public async Task<User?> GetUserByIdAsync(int id)
        {

             return await _context.Users.Include(u => u.Garden).FirstOrDefaultAsync(u => u.UserId == id);
        }

        //Create

        public async Task CreateUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
        }

        //update

        public async Task UpdateSelectedUserAsync(int id, User updatedUser)
        {
            User? userToUpdate =  await _context.Users.FirstOrDefaultAsync(u => u.UserId == id);

            if (userToUpdate != null)
            {
                userToUpdate.Name = updatedUser.Name;
                userToUpdate.Password = updatedUser.Password;

                ////kanske inte behövs?
                //userToUpdate.Garden = updatedUser.Garden;
            }
        }

        //delete


        
        public async Task RemoveSelectedUserAsync(int id)
        {
             User? userToRemove = await GetUserByIdAsync(id);

            if (userToRemove != null)
            {
               _context.Users.Remove(userToRemove);
            }

        }


        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

    }
}
