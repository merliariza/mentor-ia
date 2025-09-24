using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class FlashcardRepository : GenericRepository<Flashcard>, IFlashcardRepository
    {
        public FlashcardRepository(PublicDbContext context) : base(context) { }

        public async Task<List<Flashcard>> GetByProgressIdAsync(int progressId)
        {
            return await _context.Flashcards
                .Include(f => f.EvaluationSession)
                .Where(f => f.EvaluationSession.ProgressId == progressId)
                .ToListAsync();
        }
    }
}
