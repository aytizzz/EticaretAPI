//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace EticaretAPI.Persistance
//{
//    public class DesignTimeDbContextFactory
//    {
//    }
//}
using EticaretAPI.Persistance.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace EticaretAPI.Persistance
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ETicaretAPIDbContext>
    {
        public ETicaretAPIDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../EticaretAPI.API")) // API layihən hardadırsa ora
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<ETicaretAPIDbContext>();
            var connectionString = configuration.GetConnectionString("FerqiOlmayanConnectionString");

            builder.UseSqlServer(connectionString);

            return new ETicaretAPIDbContext(builder.Options);
        }
    }
}
