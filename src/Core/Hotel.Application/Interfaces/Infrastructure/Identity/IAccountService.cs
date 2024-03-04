using Hotel.Core.Application.Dtos.Authentication;
using Hotel.Core.Application.Models;

namespace Hotel.Core.Application.Interfaces.Infrastructure.Identity
{
    public interface IAccountService
    {
        Task<Response<SignInResponseDto>> SignInAsync(SignInRequestDto request);
        Task<string> SignUpAsync(SignUpRequestDto request);
        Task<SignInResponseDto?> GetEmailByNameAsync(string username);
    }
}
