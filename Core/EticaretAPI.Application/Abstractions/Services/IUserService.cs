using EticaretAPI.Application.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Application.Abstractions.Services
{
    public interface IUserService   
    {
        Task<CreateUserResponse> CreatedAsync(CreateUser model);
    } 
}
