using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Progress : BaseEntity
    {
        public int Score { get; set; }
        public string Topic { get; set; }
        public string Feedback { get; set; }
        public int UserMemberId { get; set; }
        public UserMember? UserMember { get; set; }
    }
}