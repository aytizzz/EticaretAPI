using EticaretAPI.Domain.Entities;
using EticaretAPI.Domain.Entities.Common;
using EticaretAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using File = EticaretAPI.Domain.Entities.File;

namespace EticaretAPI.Persistance.Contexts
{
    public class ETicaretAPIDbContext : IdentityDbContext<AppUser,AppRole,string>
    {
        //public ETicaretAPIDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        //{

        //}
        public ETicaretAPIDbContext(DbContextOptions<ETicaretAPIDbContext> dbContextOptions) : base(dbContextOptions)
        {
        }


        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<ProductImageFile> ProductImageFiles { get; set; }
        public DbSet<File> files { get; set; }
        public DbSet<InvoiceFile> InvoiceFiles { get; set; }



        //ChangeTracker-Entityler uzerinden yapilan deyisikliklerin ve ya yeni eklenen verilerin yakalanmasini
        // saglayan propertydir.
        //update operasyonlarinda track edilen verilerin yakalanmasini saglar

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {

            var datas = ChangeTracker.Entries<BaseEntity>();
            foreach (var data in datas)
            {
                _ = data.State switch
                {
                    EntityState.Added => data.Entity.CreatedDate = DateTime.UtcNow,
                    EntityState.Modified => data.Entity.UpdateDate = DateTime.UtcNow,
                    _ => DateTime.UtcNow
                };

            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
