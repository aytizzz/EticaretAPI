﻿using EticaretAPI.Application.Repositories;
using EticaretAPI.Domain.Entities;
using EticaretAPI.Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Persistance.Repositories
{
    internal class OrderWriteRepository : WriteRepository<Order>, IOrderWriteRepository
    {
        public OrderWriteRepository(ETicaretAPIDbContext context) : base(context)
        {
        }
    }
}
