using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class EvaluationSession : BaseEntity
    {
        public int Score { get; set; }
        public string? Feedback { get; set; }

        public int ProgressId { get; set; }
        public Progress? Progress { get; set; }

        public ICollection<Flashcard> Flashcards { get; set; } = new List<Flashcard>();
    }
}