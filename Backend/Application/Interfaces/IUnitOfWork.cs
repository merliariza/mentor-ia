using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUnitOfWork
    {
        IUserMemberRepository UserMember { get; }
        IProgressRepository Progress { get; }
        IFlashcardRepository Flashcard { get; }
        IEvaluationSessionRepository EvaluationSession { get; }
        Task<int> SaveAsync(); 
    }
}