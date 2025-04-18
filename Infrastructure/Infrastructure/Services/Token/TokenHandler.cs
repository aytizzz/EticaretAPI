using EticaretAPI.Application.Abstractions.Token;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Infrastructure.Services.Token
{
    public class TokenHandler : ITokenHandler
    {
        readonly IConfiguration _configuration; //konfiqurasiyadan (appsettings.json) Token:Issuer,
                                                //Token:Audience, Token:SecurityKey kimi dəyərləri oxuyur.

        public TokenHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Application.DTOs.Token.Token CreateAccessToken(int second)
        {
            Application.DTOs.Token.Token token = new Application.DTOs.Token.Token();

            // security keyin simmetirikini aliriq 
            SymmetricSecurityKey symmetricSecurityKey = new(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]));

            // sifrelenmis kimligi olusturoyoruz
            SigningCredentials signingCredentials = new SigningCredentials(symmetricSecurityKey,SecurityAlgorithms.HmacSha256);

            // olusturalacak token ayarlaruni veriyoruz
            token.Expiration = DateTime.UtcNow.AddSeconds(second);

            JwtSecurityToken securityToken = new JwtSecurityToken(
            

                audience: _configuration["Token:Audience"],
                issuer: _configuration["Token:Issuer"], 
                expires: token.Expiration,
                notBefore: DateTime.UtcNow,
                signingCredentials: signingCredentials
                
            );



            //Token olusturucu sinfidan bir ornek alalim
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            token.AccessToken = tokenHandler.WriteToken(securityToken);
            return token;

            
        }
    }
}
