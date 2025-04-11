using EticaretAPI.Application.Repositories;
using EticaretAPI.Domain.Entities.Common;
using EticaretAPI.Persistance.Contexts;
using EticaretAPI.Persistance.Migrations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Persistance.Repositories
{
    public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity
    {
        private readonly ETicaretAPIDbContext _context;

        public ReadRepository(ETicaretAPIDbContext context)
        {
            _context = context;
        }

        public DbSet<T> Table => _context.Set<T>();//sorus



        public IQueryable<T> GetAll(bool tracking =true)        /*=> Table;*/
        {
            var query = Table.AsQueryable();
            if (!tracking)
            
                query = query.AsNoTracking();
            return query;
        }

        public IQueryable<T> GetWhere(Expression<Func<T, bool>> method,bool tracking = true)
        {
              var query= Table.Where(method);
            if (!tracking)
             query = query.AsNoTracking();
                return query;
            
        }



        public async Task<T> GetsingleAsync(Expression<Func<T, bool>> method,bool tracking = true)
        {
            var query = Table.AsQueryable();
            if (!tracking)
                //=>  await Table.FirstOrDefaultAsync(method);
                query = Table.AsNoTracking();
            return await query.FirstOrDefaultAsync(method);
        }
     



        public async Task<T> GetByIdAsync(int id, bool tracking = true)
        {
            var query = Table.AsQueryable(); // Iqueryable de findasync yoxdu
            if (!tracking)
                query = Table.AsNoTracking();
            return  await query.FirstOrDefaultAsync(data=>data.Id==id);
        }      /*=> await Table.FindAsync(id);*/

        /* await Table.FirstOrDefaultAsync(data => data.Id ==id);*/



    }
}
