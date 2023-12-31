﻿namespace GreenThumb.Database
{
    public class UnitOfWorkRepository
    {
        private readonly GreenThumbDbContext _context;
        public GardenRepository GardenRepository { get; }
        public InstructionRepository InstructionRepository { get; }
        public PlantRepository PlantRepository { get; }
        public UserRepository UserRepository { get; }
        public UnitOfWorkRepository(GreenThumbDbContext context)
        {
            _context = context;

            GardenRepository = new(context);
            InstructionRepository = new(context);
            PlantRepository = new(context);
            UserRepository = new(context);
        }

        

        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}
