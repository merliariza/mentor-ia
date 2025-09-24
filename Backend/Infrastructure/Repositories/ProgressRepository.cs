using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ProgressRepository : GenericRepository<Progress>, IProgressRepository
    {
        public ProgressRepository(PublicDbContext context) : base(context) { }

        public async Task<Progress?> GetWithSessionsAsync(int id)
        {
            return await _context.Progresses
                .Include(p => p.EvaluationSessions)
                .ThenInclude(s => s.Flashcards)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
