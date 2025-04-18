using EticaretAPI.Application.Validators.Products;
using EticaretAPI.Infrastructure;
using EticaretAPI.Infrastructure.Filters;
using EticaretAPI.Infrastructure.Services.Azure;
using EticaretAPI.Infrastructure;
using EticaretAPI.Infrastructure.Services.Storage.Local;
using EticaretAPI.Persistance.Contexts;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using EticaretAPI.Application;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPersistenceServices(builder.Configuration); //baxx
//builder.Services.AddStorage<LocalStorage>();
builder.Services.AddStorage<AzureStorage>();
builder.Services.AddInfrastructureServices();
builder.Services.AddApplicationServices();

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ValidationFilter>();

})
    .AddFluentValidation(configuration => configuration.RegisterValidatorsFromAssemblyContaining<CreateProductValidator>())
    .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);



builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer("Admin",options =>
    {
        options.TokenValidationParameters = new()
        {
            ValidateAudience = true,//Olusturulacak token degerini kimlerin/hangi originlerin/sitelerin kullanacagi
                                    //belirlediyimiz degerdir

            ValidateLifetime = true,    //Olusturulacak token degerini kimin dagitdigini ifade edececimiz sahe 

            ValidateIssuer = true,          ////Olusturulan token deyerinin siresini kontrol edecek olan dogrulamadir.

            ValidateIssuerSigningKey = true, ////uretilecek token degerinin uygulamamiza ait bir deger oldugunu
                                             ////ifade eden security key verisinin dogrulanmasidr.
            ValidAudience = builder.Configuration["Token:Audience"],
            ValidIssuer = builder.Configuration["Token:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
           LifetimeValidator = (notBefore, expires, securityToken, validationParameters) => expires!=null ?expires>DateTime.UtcNow:false //?


        };
    });


builder.Services.AddDbContext<ETicaretAPIDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("FerqiOlmayanConnectionString")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseCors();//middleware nedir ioc container nedir
app.UseStaticFiles();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();



//builder.Services.AddCors(options => options
//.AddDefaultPolicy(policy => policy.WithOrigins().AllowAnyHeader().AllowAnyMethod()
//));
