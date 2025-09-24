using Application.Interfaces;
using Infrastructure;
using Infrastructure.Data;
using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork,IDisposable
{
    private readonly PublicDbContext _context;
    private IProgressRepository? _progress;

    public IUserMemberRepository? _userMember;
    public IEvaluationSessionRepository? _evaluationSession;
    public IFlashcardRepository? _flashcard;

    public UnitOfWork(PublicDbContext context)
    {
        _context = context;
    }
   

    public IProgressRepository Progress{
        get
        {
            if (_progress == null)
            {
                _progress = new ProgressRepository(_context);
            }
            return _progress;
        }
    }

    public IUserMemberRepository UserMember{
        get
        {
            if (_userMember == null)
            {
                _userMember = new UserMemberRepository(_context);
            }
            return _userMember;
        }
    }

    public IEvaluationSessionRepository EvaluationSession{
        get
        {
            if (_evaluationSession == null)
            {
                _evaluationSession = new EvaluationSessionRepository(_context);
            }
            return _evaluationSession;
        }
    }

    public IFlashcardRepository Flashcard{
        get
        {
            if (_flashcard == null)
            {
                _flashcard = new FlashcardRepository(_context);
            }
            return _flashcard;
        }
    }

    public async Task<int> SaveAsync()
    {
        return await _context.SaveChangesAsync();
    }
    public void Dispose()
    {
        _context.Dispose();
    }
}