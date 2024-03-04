namespace Hotel.Core.Application.Dtos.Authentication
{
    public class SignInResponseDto
    {
        public string Id { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public bool IsVerified { get; set; }
        public string Token { get; set; } = string.Empty;
    }
}
