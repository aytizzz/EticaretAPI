﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Application.Features.Commands.Product.RemoveProduct
{
    public class RemoveProductCommandRequest:IRequest<RemoveProductCommandResponse>
    {
        public int Id { get; set; }
    }
}
