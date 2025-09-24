using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Flashcard : BaseEntity
    {
        public string? Question { get; set; }
        public string? Answer { get; set; }

        public int EvaluationSessionId { get; set; }
        public EvaluationSession? EvaluationSession { get; set; }
    }
}