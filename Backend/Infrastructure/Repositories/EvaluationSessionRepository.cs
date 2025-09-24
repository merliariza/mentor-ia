using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class EvaluationSessionRepository : GenericRepository<EvaluationSession>, IEvaluationSessionRepository
    {
        private readonly PublicDbContext _context;
        public EvaluationSessionRepository(PublicDbContext context) : base(context)
        {
         _context = context;   
        }
    }
}