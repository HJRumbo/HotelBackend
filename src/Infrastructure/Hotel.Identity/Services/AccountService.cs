using Hotel.Core.Application.Dtos.Authentication;
using Hotel.Core.Application.Exceptions;
using Hotel.Core.Application.Interfaces.Infrastructure.Identity;
using Hotel.Core.Application.Models;
using Hotel.Core.Domain.Enums;
using Hotel.Infrastructure.Identity.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Hotel.Infrastructure.Identity.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IJwtService _jwtHelper;

        public AccountService(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IJwtService jwtHelper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtHelper = jwtHelper;
        }

        public async Task<Response<SignInResponseDto>> SignInAsync(SignInRequestDto request)
        {
            var user = (await _userManager.FindByNameAsync(request.UserName) ?? await _userManager.FindByEmailAsync(request.UserName)) ?? throw new ApiException($"No hay una cuenta registrada con el usuario {request.UserName} ");
            var result = await _signInManager.PasswordSignInAsync(user.UserName!, request.Password, false, lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                throw new ApiException($"Las credenciales del usuario no son válidas");
            }

            var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
            var role = rolesList.FirstOrDefault() ?? "";

            var token = _jwtHelper.GenerateJwtToken(user, role);

            var response = new SignInResponseDto()
            {
                Token = token,
                Email = user.Email!,
                UserName = user.UserName!,
                IsVerified = true,
                Role = role
            };

            return new Response<SignInResponseDto>(response, $"Usuario autenticado {user.UserName}");
        }

        public async Task<string> SignUpAsync(SignUpRequestDto request)
        {
            var userNameUsed = await _userManager.FindByNameAsync(request.UserName);

            if (userNameUsed != null)
            {
                throw new ApiException($"El nombre de usuario {request.UserName} ya fue registrado. ");
            }

            var emailUsed = await _userManager.FindByEmailAsync(request.Email);

            if (emailUsed != null)
            {
                throw new ApiException($"El correo {request.Email} ya fue registrado. ");
            }

            var user = new IdentityUser
            {
                Email = request.Email,
                UserName = request.UserName,
                EmailConfirmed = true,
                PhoneNumber = request.Phone,
                PhoneNumberConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, Roles.Traveler.ToString());
                return user.Id;

            }
            else
            {
                throw new ApiException($"{result.Errors}");
            }
        }

        public async Task<SignInResponseDto?> GetEmailByNameAsync(string username)
        {
            var user = await _userManager.FindByNameAsync(username);

            return user != null ? new SignInResponseDto()
            {
                Id = user.Id.ToString(),
                Email = user.Email!,
                UserName = user.UserName!,
                IsVerified = true,
            } : null;
        }
    }
}
