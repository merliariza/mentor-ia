using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class UserChatRequestDto
    {
        public UserMemberDto User { get; set; } = null!;
        public List<ProgressDto> Progress { get; set; } = new();
        public string Question { get; set; } = string.Empty;
    }
}