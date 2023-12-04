using GreenThumb.Models;

namespace GreenThumb.Database
{
    public class InstructionRepository
    {
        private readonly GreenThumbDbContext _context;

        public InstructionRepository(GreenThumbDbContext context)
        {
            _context = context;
        }

        public List<Instruction> GetAll()
        {
            return _context.Instructions.ToList();
        }


        //show All
        public Instruction? GetInstructionById(int id)
        {

            return _context.Instructions.FirstOrDefault(i => i.InstructionId == id);
        }

        //Create

        public void CreateInstruction(Instruction instruction)
        {
            _context.Instructions.Add(instruction);
        }

        //update

        public void UpdateSelectedInstruction(int id, Instruction updatedInstruction)
        {
            Instruction? instructionToUpdate = _context.Instructions.FirstOrDefault(i => i.InstructionId == id);

            if (instructionToUpdate != null)
            {
                instructionToUpdate.Description = updatedInstruction.Description;

                ////kanske inte behövs?
                //instructionToUpdate.PlantId = updatedInstruction.PlantId;
                //instructionToUpdate.Plant = updatedInstruction.Plant;

            }
        }

        //delete

        public void RemoveSelectedUser(int id)
        {
            Instruction? instructionToRemove = GetInstructionById(id);

            if (instructionToRemove != null)
            {
                _context.Instructions.Remove(instructionToRemove);
            }

        }

        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}
