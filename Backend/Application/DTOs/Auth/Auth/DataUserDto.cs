namespace Application.DTOs.Auth
{
    public class DataUserDto
    {
        public string? Codeb { get; set; }          
        public string? Message { get; set; }        
        public bool IsAuthenticated { get; set; }  
        public string? UserName { get; set; }      
        public string? Name  { get; set; }          
        public string? Email { get; set; }         
        public string? Token { get; set; }    
        public string? RefreshToken { get; set; }     
        public DateTime RefreshTokenExpiration { get; set; } 
    }
}
