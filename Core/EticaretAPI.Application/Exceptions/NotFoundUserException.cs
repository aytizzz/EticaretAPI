﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Application.Exceptions
{
    public class NotFoundUserException : Exception
    {
        public NotFoundUserException() : base("Kullanci adi ve ya sifre hatali")
        {
        }

        public NotFoundUserException(string? message) : base(message)
        {
        }
    }
}
