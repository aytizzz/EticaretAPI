using Azure.Core;
using EticaretAPI.Application.Abstractions.Services;
using EticaretAPI.Application.DTOs.User;
using EticaretAPI.Application.Exceptions;
using EticaretAPI.Application.Features.Commands.AppUser.CreateUser;
using EticaretAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Persistance.Services
{
    public class UserService:IUserService
    {
        readonly UserManager<Domain.Entities.Identity.AppUser> _userManager;

        public UserService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<CreateUserResponse> CreatedAsync(CreateUser model)
        {

            IdentityResult result = await _userManager.CreateAsync(new Domain.Entities.Identity.AppUser()
            {
                UserName = model.UserName,
                Email = model.Email,
                NameSurname = model.NameSurname,

            }, model.Password);


            CreateUserResponse response = new CreateUserResponse()
            {
                Succeeded = result.Succeeded
            };

            if (result.Succeeded)
               
                return new CreateUserResponse()
                {
                    Succeeded = true,
                    Message = "Kullanici basariyla eklenmisdir"
                };
            throw new UserCreateFailedException();
        }







    }
}

//    response.Message = "Kullanici basariyla olusturulmusdur";
//else
//    foreach (var error in result.Errors)
//        response.Message += $"{error.GetHashCode}-{error.Description}";
