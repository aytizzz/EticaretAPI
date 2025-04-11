using EticaretAPI.Application.DTOs.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Application.Features.Commands.AppUser.LoginUser
{
    public class LoginUserCommandResponse
    {

    }
    public class LoginUserSuccessCommandResponse:LoginUserCommandResponse
    {
        public Token Token { get; set; } //egerki basariliysa bunu dondurecek
    }

    public class LoginUserErrorCommandResponse:LoginUserCommandResponse
    {
        public string Message { get; set; } // deyilse bunu

    }
}
