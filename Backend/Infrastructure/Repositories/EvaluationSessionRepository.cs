using Domain.Entities;
using Application.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class EvaluationSessionRepository : GenericRepository<EvaluationSession>, IEvaluationSessionRepository
    {
        private readonly PublicDbContext _context;

        public EvaluationSessionRepository(PublicDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<EvaluationSession>> GetByProgressIdAsync(int progressId)
        {
            return await _context.EvaluationSessions
                .Include(es => es.Flashcards)  
                .Where(es => es.ProgressId == progressId)
                .ToListAsync();
        }
    }
}
