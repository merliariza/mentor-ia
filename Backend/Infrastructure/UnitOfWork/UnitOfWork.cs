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

    public async Task<int> SaveAsync()
    {
        return await _context.SaveChangesAsync();
    }
    public void Dispose()
    {
        _context.Dispose();
    }
}