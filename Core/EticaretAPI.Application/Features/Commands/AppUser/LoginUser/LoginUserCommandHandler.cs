using EticaretAPI.Application.Abstractions.Token;
using EticaretAPI.Application.DTOs.Token;
using EticaretAPI.Application.Exceptions;
using EticaretAPI.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

//using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Application.Features.Commands.AppUser.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
    {
        readonly UserManager<Domain.Entities.Identity.AppUser> _userManager; //İstifadəçilərlə (AppUser) işləmək üçün Identity-dən gəlir.
                                                                             //Məsələn: istifadəçi tapmaq.

        readonly SignInManager<Domain.Entities.Identity.AppUser> _signInManager;//Parol yoxlama, istifadəçini login etmək kimi proseslər üçün istifadə olunur.
        readonly ITokenHandler _tokenHandler; //	Token yaratmaq üçün interfeysdir. Access Token-lər burada yaradılır.

        public LoginUserCommandHandler(UserManager<Domain.Entities.Identity.AppUser> 
            userManager, 
            SignInManager<Domain.Entities.Identity.AppUser> signInManager,ITokenHandler tokenHandler)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenHandler = tokenHandler;
        }
        public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {
            Domain.Entities.Identity.AppUser user = await _userManager.FindByNameAsync(request.UserNameorEmail);//Əgər istifadəçi adı (username) ilə
                                                                                                                //tapa bilmirsə, email ilə axtarır.
            if (user == null)
                user = await _userManager.FindByEmailAsync(request.UserNameorEmail);

            if (user == null)
                throw new NotFoundUserException();


            Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false); //Əgər istifadəçi varsa,
                                                                                                                                              //parolu düzgün daxil edilibmi – bunu yoxlayır.  
            if (result.Succeeded)
            {
                Token CreatedToken = _tokenHandler.CreateAccessToken(5);//sorus

                return new LoginUserSuccessCommandResponse()
                {
                    Token = CreatedToken  //	Yaradılan token dəyərini LoginUserSuccessCommandResponse obyektinə ötürmək üçün.	
                                          //	Client token ala bilməz → login olunmuş sayılmaz.
                };
            }
            else
            {
                return new LoginUserErrorCommandResponse()
                {
                    Message = "Kullanici adi ve ya sifre hatali"
                };
            }
           
        }
    }
}
