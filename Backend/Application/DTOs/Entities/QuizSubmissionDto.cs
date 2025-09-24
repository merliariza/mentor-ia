using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class QuizSubmissionDto
    {
        public int ProgressId { get; set; }
        public List<AnswerDto> Answers { get; set; } = new();
    }
}