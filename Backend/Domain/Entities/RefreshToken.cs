namespace Domain.Entities
{
    public class RefreshToken : BaseEntity
    {
        public int Id { get; set; } 
        public int MemberId { get; set; }
        public UserMember User { get; set; } = null!;
        
        public string Token { get; set; } = string.Empty;
        public DateTime Expires { get; set; }
        public bool IsExpired => DateTime.UtcNow >= Expires;

        public DateTime Created { get; set; }
        public DateTime? Revoked { get; set; }

        public bool IsActive => Revoked == null && !IsExpired;
    }
}
