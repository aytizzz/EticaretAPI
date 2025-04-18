using Azure.Core;
using EticaretAPI.Application.Abstractions.Services;
using EticaretAPI.Application.Abstractions.Token;
using EticaretAPI.Application.DTOs.Token;
using EticaretAPI.Application.Exceptions;
using EticaretAPI.Application.Features.Commands.AppUser.LoginUser;
using EticaretAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Persistance.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        readonly UserManager<Domain.Entities.Identity.AppUser> _userManager;

        readonly SignInManager<Domain.Entities.Identity.AppUser> _signInManager;//Parol yoxlama, istifadəçini login etmək kimi proseslər üçün istifadə olunur.
        readonly ITokenHandler _tokenHandler; //	Token yaratmaq üçün interfeysdir. Access Token-lər burada yaradılır.

        public AuthenticationService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenHandler tokenHandler)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenHandler = tokenHandler;
        }

        public async Task<Token> loginAsync(string usernameorEmail, string password, int AccessTokenLifeTime)
        {
            Domain.Entities.Identity.AppUser user = await _userManager.FindByNameAsync(usernameorEmail);//Əgər istifadəçi adı (username) ilə
                                                                                                                //tapa bilmirsə, email ilə axtarır.
            if (user == null)
                user = await _userManager.FindByEmailAsync(usernameorEmail);
            if (user == null)
                throw new NotFoundUserException();
            Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.CheckPasswordSignInAsync(user,password, false); //Əgər istifadəçi varsa,
                                                                                                                                              //parolu düzgün daxil edilibmi – bunu yoxlayır.  
            if (result.Succeeded)
            {
                Token CreatedToken = _tokenHandler.CreateAccessToken(AccessTokenLifeTime);//sorus
                return CreatedToken;
            }
            else
            {
               throw new  AuthenticationErrorException();
               
            }
        }
    }
}

//return new LoginUserErrorCommandResponse()
//{
//    Message = "Kullanici adi ve ya sifre hatali"
//};