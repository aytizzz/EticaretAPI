﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Application.Features.Queries.ProductImageFile.GetProductImages
{
    public class GetProductImageQueryResponse
    {
        public string Path { get; set; }
        public string FileName { get; set; }
        public int Id { get; set; }
    }
}
