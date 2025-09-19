using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class ProgressDto
    {
        public int Id { get; set; }
        public int Score { get; set; }
        public string? Topic { get; set; }
        public string? Feedback { get; set; }
        public int UserMemberId { get; set; }
    }
}