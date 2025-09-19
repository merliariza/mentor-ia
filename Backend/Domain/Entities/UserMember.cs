using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class UserMember : BaseEntity
    {
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? PasswordHash { get; set; }
        public ICollection<Progress> Progresses { get; set; } = new List<Progress>();

        
        public List<RefreshToken> RefreshTokens { get; set; } = new();
    }
}