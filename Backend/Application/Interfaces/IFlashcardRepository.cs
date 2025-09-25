using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IFlashcardRepository : IGenericRepository <Flashcard>
    {
        Task<IEnumerable<Flashcard>> GetByEvaluationSessionIdAsync(int sessionId);
        Task<List<Flashcard>> GetByProgressIdAsync(int progressId);
    }
}