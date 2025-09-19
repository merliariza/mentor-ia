using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class UserMemberRepository : GenericRepository<UserMember>, IUserMemberRepository
    {
        private readonly PublicDbContext _context;
        public UserMemberRepository(PublicDbContext context) : base(context)
        {
         _context = context;   
        }
    }
}