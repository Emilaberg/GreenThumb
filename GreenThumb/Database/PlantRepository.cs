using GreenThumb.Models;

namespace GreenThumb.Database
{
    public class PlantRepository
    {
        private readonly GreenThumbDbContext _context;

        public PlantRepository(GreenThumbDbContext context)
        {
            _context = context;
        }

        public List<Plant> GetAll()
        {
            return _context.Plants.ToList();
        }


        //show All
        public Plant? GetPlantById(int id)
        {

            return _context.Plants.FirstOrDefault(p => p.PlantId == id);
        }

        //Create
        public void CreatePlant(Plant plant)
        {
            _context.Plants.Add(plant);
        }

        //update

        public void UpdateSelectedPlant(int id, Plant updatedPlant)
        {
            Plant? plantToUpdate = _context.Plants.FirstOrDefault(p => p.PlantId == id);

            if (plantToUpdate != null)
            {
                plantToUpdate.Name = updatedPlant.Name;

                ////kanske inte behövs?
                //plantToUpdate.Gardens = updatedPlant.Gardens;
                //plantToUpdate.Instructions = updatedPlant.Instructions;
            }
        }

        //delete

        public void RemoveSelectedPlant(int id)
        {
            Plant? plantToRemove = GetPlantById(id);

            if (plantToRemove != null)
            {
                _context.Plants.Remove(plantToRemove);
            }
        }

        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}
