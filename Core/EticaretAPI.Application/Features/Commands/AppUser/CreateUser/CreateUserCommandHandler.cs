using EticaretAPI.Application.Exceptions;
using EticaretAPI.Domain.Entities.Identity;
using MediatR;
//using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Application.Features.Commands.AppUser.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
    {
        private readonly UserManager<EticaretAPI.Domain.Entities.Identity.AppUser> _userManager;

        public CreateUserCommandHandler(UserManager<Domain.Entities.Identity.AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {
            IdentityResult result = await _userManager.CreateAsync(new Domain.Entities.Identity.AppUser()
            {
                UserName = request.UserName,
                Email = request.Email,
                NameSurname = request.NameSurname,

            }, request.Password);


            CreateUserCommandResponse response = new CreateUserCommandResponse()
            {
                Succeeded = result.Succeeded 
            };

            if (result.Succeeded)
                //    response.Message = "Kullanici basariyla olusturulmusdur";
                //else
                //    foreach (var error in result.Errors)
                //        response.Message += $"{error.GetHashCode}-{error.Description}";

                return new CreateUserCommandResponse()
                {
                    Succeeded = true,
                    Message = "Kullanici basariyla eklenmisdir"
                };
            throw new UserCreateFailedException();


        }
    }
}
