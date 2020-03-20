using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
//using WebApi.Entities;

namespace ApiConsulta.Helpers
{
    public static class Utilerias
    {
        public static string GetToken(string usuario, string secret, int segundos)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("asesor_identificador", usuario),
                    new Claim("asesor_nombre", usuario),
                    new Claim("unique_device", usuario)
                }),
                Expires = DateTime.UtcNow.AddSeconds(segundos),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public static void GetDataTocken(string tocken)
        {
            var tockenHandler = new JwtSecurityToken();

        }
    }
}