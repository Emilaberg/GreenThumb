using GreenThumb.Models;
using Microsoft.EntityFrameworkCore;

namespace GreenThumb.Database
{
    public class GardenRepository
    {
        private readonly GreenThumbDbContext _context;

        public GardenRepository(GreenThumbDbContext context)
        {
            _context = context;
        }

        public async Task<List<Garden>> GetAllAsync()
        {
            return await _context.Gardens.ToListAsync();
        }
        
        //Get gardens
        public async Task<Garden?> GetGardenByIdAsync(int id)
        {

            return await _context.Gardens.Include(g => g.Plants).FirstOrDefaultAsync(g => g.UserId == id);
        }

        //Create

        public async Task CreateGardenAsync(Garden garden)
        {
            await _context.Gardens.AddAsync(garden);
        }

        //update

        public async Task UpdateSelectedGardenAsync(int id, Garden updatedGarden)
        {
            Garden? gardenToUpdate = await _context.Gardens.FirstOrDefaultAsync(g => g.GardenId == id);

            if (gardenToUpdate != null)
            {
                gardenToUpdate.Name = updatedGarden.Name;
                gardenToUpdate.Size = updatedGarden.Size;
                gardenToUpdate.Level = updatedGarden.Level;

                ////kanske inte behövs?
                //gardenToUpdate.UserId = updatedGarden.UserId;
                //gardenToUpdate.User = updatedGarden.User;

            }
        }

        //delete

        public async Task RemoveSelectedGardenTask(int id)
        {
            Garden? gardenToRemove = await GetGardenByIdAsync(id);

            if (gardenToRemove != null)
            {
                _context.Gardens.Remove(gardenToRemove);
            }

        }

        public async Task Complete()
        {
            await _context.SaveChangesAsync();
        }
    }
}
