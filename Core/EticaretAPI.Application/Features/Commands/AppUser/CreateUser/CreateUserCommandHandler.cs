using EticaretAPI.Application.Abstractions.Services;
using EticaretAPI.Application.DTOs.User;
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
        readonly IUserService _userService;

        public CreateUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {
            CreateUserResponse response=await _userService.CreatedAsync(new DTOs.User.CreateUser() 
            { 
                Email =request.Email,
                NameSurname=request.NameSurname,
                Password=request.Password,
                PasswordConfirm=request.PasswordConfirm,
                UserName=request.UserName
            }
            );

            return new CreateUserCommandResponse()
            {
                Message = response.Message,
                Succeeded = response.Succeeded,
            };
            // biz burada createuserresponse tipinde reesponse aliriq sonra onu commandresponse cevririeik

        }
    }
}
