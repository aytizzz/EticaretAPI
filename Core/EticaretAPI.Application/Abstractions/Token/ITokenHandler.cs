﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Application.Abstractions.Token
{
    public interface ITokenHandler
    {
        DTOs.Token.Token CreateAccessToken(int minute);
    }
}
