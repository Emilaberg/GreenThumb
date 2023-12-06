using GreenThumb.Models;
using Microsoft.EntityFrameworkCore;

namespace GreenThumb.Database
{
    public class InstructionRepository
    {
        private readonly GreenThumbDbContext _context;

        public InstructionRepository(GreenThumbDbContext context)
        {
            _context = context;
        }

        public async Task<List<Instruction>> GetAllAsync()
        {
            return await _context.Instructions.ToListAsync();
        }


        //show All
        public async Task<Instruction?> GetInstructionByIdAsync(int id)
        {

            return await _context.Instructions.FirstOrDefaultAsync(i => i.InstructionId == id);
        }

        //Create

        public async Task CreateInstructionAsync(Instruction instruction)
        {
            await _context.Instructions.AddAsync(instruction);
        }

        //update

        public async Task UpdateSelectedInstructionAsync(int id, Instruction updatedInstruction)
        {
            Instruction? instructionToUpdate = await _context.Instructions.FirstOrDefaultAsync(i => i.InstructionId == id);

            if (instructionToUpdate != null)
            {
                instructionToUpdate.Description = updatedInstruction.Description;

                ////kanske inte behövs?
                //instructionToUpdate.PlantId = updatedInstruction.PlantId;
                //instructionToUpdate.Plant = updatedInstruction.Plant;

            }
        }

        //delete

        public async Task RemoveSelectedUserAsync(int id)
        {
            Instruction? instructionToRemove = await GetInstructionByIdAsync(id);

            if (instructionToRemove != null)
            {
                _context.Instructions.Remove(instructionToRemove);
            }

        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
