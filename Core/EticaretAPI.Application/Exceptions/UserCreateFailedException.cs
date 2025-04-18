using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Application.Exceptions
{
    public class UserCreateFailedException : Exception
    {
        public UserCreateFailedException():base("Kullanici olusturulurken beklenmeyen hatayla karsilasildi")
        {
        }

        public UserCreateFailedException(string? message) : base(message)
        {
        }
    }
}
