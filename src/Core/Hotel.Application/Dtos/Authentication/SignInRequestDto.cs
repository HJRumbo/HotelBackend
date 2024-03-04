namespace Hotel.Core.Application.Dtos.Authentication
{
    public class SignInRequestDto
    {
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
