using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Application.Abstractions.Services
{
    public interface IAuthenticationService
    {
        Task <DTOs.Token.Token> loginAsync(string usernameorEmail,string password,int AccessTokenLifeTime);
    }
}
