using JWTLoginCommon.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWTLoginCommon.Helpers
{
    public class JwtHelpers
    {
        private readonly IConfiguration Configuration;

        public JwtHelpers(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        public string GenerateToken(string userName, ConstantsConfig constantsConfig,int expireHours = 1)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("account", userName)
            };
            
            var secretBytes = Encoding.UTF8.GetBytes(constantsConfig.Secret);
            var key = new SymmetricSecurityKey(secretBytes);
            var algorithm = SecurityAlgorithms.HmacSha256;

            var signingCredentials = new SigningCredentials(key, algorithm);

            var token = new JwtSecurityToken(
                constantsConfig.Issuer,
                constantsConfig.Audiance,
                claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddHours(expireHours),
                signingCredentials);

            var tokenJson = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenJson;
        }
    }
}
