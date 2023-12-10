using GreenThumb.Models;
using Microsoft.EntityFrameworkCore;
using System.Windows;

namespace GreenThumb.Database
{
    public class PlantRepository
    {
        private readonly GreenThumbDbContext _context;

        public PlantRepository(GreenThumbDbContext context)
        {
            _context = context;
        }

        public async Task<List<Plant>> GetAllAsync()
        {
            return await _context.Plants.Include(p => p.Instructions).ToListAsync();
        }

        public async Task<Plant?> GetPlantByName(string name)
        {
            //string big = name.First().ToString().ToUpper();
            //string s = name.Replace(name[0], big[0]);
            //ta bort första bokstaven
            

            var plants = await GetAllAsync();
            var res = plants.FirstOrDefault(p => p.Name == name);
            return res;
        }

        //show All
        public async Task<Plant?> GetPlantByIdAsync(int id)
        {

            return await _context.Plants.Include(p => p.Instructions).FirstOrDefaultAsync(p => p.PlantId == id);
        }

        //Create
        public async Task CreatePlantAsync(Plant plant)
        {
            await _context.Plants.AddAsync(plant);
        }

        //update

        public async Task UpdateSelectedPlantAsync(int id, Plant updatedPlant)
        {
            Plant? plantToUpdate = await _context.Plants.FirstOrDefaultAsync(p => p.PlantId == id);

            if (plantToUpdate != null)
            {
                plantToUpdate.Name = updatedPlant.Name;

                ////kanske inte behövs?
                //plantToUpdate.Gardens = updatedPlant.Gardens;
                //plantToUpdate.Instructions = updatedPlant.Instructions;
            }
        }

        //delete

        public async Task RemoveSelectedPlantAsync(int id)
        {
            Plant? plantToRemove = await GetPlantByIdAsync(id);

            if (plantToRemove != null)
            {
                _context.Plants.Remove(plantToRemove);
            }
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
