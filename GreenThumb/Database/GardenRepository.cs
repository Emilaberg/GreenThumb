using GreenThumb.Models;

namespace GreenThumb.Database
{
    public class GardenRepository
    {
        private readonly GreenThumbDbContext _context;

        public GardenRepository(GreenThumbDbContext context)
        {
            _context = context;
        }

        public List<Garden> GetAll()
        {
            return _context.Gardens.ToList();
        }


        //show All
        public Garden? GetGardenById(int id)
        {

            return _context.Gardens.FirstOrDefault(g => g.GardenId == id);
        }

        //Create

        public void CreateGarden(Garden garden)
        {
            _context.Gardens.Add(garden);
        }

        //update

        public void UpdateSelectedGarden(int id, Garden updatedGarden)
        {
            Garden? gardenToUpdate = _context.Gardens.FirstOrDefault(g => g.GardenId == id);

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

        public void RemoveSelectedGarden(int id)
        {
            Garden? gardenToRemove = GetGardenById(id);

            if (gardenToRemove != null)
            {
                _context.Gardens.Remove(gardenToRemove);
            }

        }

        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}
