﻿using EticaretAPI.Application.RequestParametres;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Application.Features.Queries.Product.GetAllProducts
{
    public class GetAllProductQueryRequest : IRequest<GetAllProductQueryResponse>
    {

        //public Pagination Pagination { get; set; }
        public int Page { get; set; } = 0;
        public int Size { get; set; } = 5;
    }
}
