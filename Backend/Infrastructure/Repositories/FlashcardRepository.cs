using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class FlashcardRepository : GenericRepository<Flashcard>, IFlashcardRepository
    {
        private readonly PublicDbContext _context;
        public FlashcardRepository(PublicDbContext context) : base(context)
        {
         _context = context;   
        }
    }
}