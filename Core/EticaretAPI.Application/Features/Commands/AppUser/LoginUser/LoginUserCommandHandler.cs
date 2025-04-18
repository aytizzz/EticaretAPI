using EticaretAPI.Application.Abstractions.Services;
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
        IAuthenticationService _authenticationService;

        public LoginUserCommandHandler(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {
            Token token= await  _authenticationService.loginAsync(request.UserNameorEmail, request.Password,15);
            return new LoginUserSuccessCommandResponse()
            {
                Token = token //?
            };
        }
    }
}
