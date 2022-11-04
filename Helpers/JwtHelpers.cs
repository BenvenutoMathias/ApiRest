using Microsoft.IdentityModel.Tokens;
using RestApi.Entities.DataEntities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace RestApi.Helpers
{
    public static class JwtHelpers
    {
        public static IEnumerable<Claim> GetClaims(this UserTokens userAccounts, Guid Id)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim("Id", userAccounts.Id.ToString()),
                new Claim(ClaimTypes.Name, userAccounts.UserName),
                new Claim(ClaimTypes.Email, userAccounts.EmailId),
                new Claim(ClaimTypes.NameIdentifier, Id.ToString()),
                new Claim(ClaimTypes.Expiration, DateTime.UtcNow.AddDays(1).ToString("MMM ddd dd yyyy HH:mm:ss tt")),
            };

            if (userAccounts.Role == Role.Administrator)
            {
                claims.Add(new Claim(ClaimTypes.Role, Role.User.ToString()));
                claims.Add(new Claim(ClaimTypes.Role, Role.Administrator.ToString()));
            }
            else if (userAccounts.Role == Role.User)
            {
                claims.Add(new Claim(ClaimTypes.Role, "User"));
            }

            return claims;

        }

        public static IEnumerable<Claim> GetClaims(this UserTokens userAccounts, out Guid Id)
        {
            Id = Guid.NewGuid();

            return GetClaims(userAccounts, Id);
        }

        public static UserTokens GenTokenKey(User model, JwtSettings jwtSettings)
        {
            try
            {
                var userToken = new UserTokens();

                userToken.UserName = model.Name;
                userToken.EmailId = model.EmailAdress;
                userToken.GuidId = new Guid();
                userToken.Role = model.Role;

                if (model == null)
                    throw new ArgumentNullException(nameof(model));

                // Obtain Secret Key
                var key = System.Text.Encoding.ASCII.GetBytes(jwtSettings.IssuerSigningKey);

                Guid Id;

                // Expires in 1 Day
                DateTime expireTime = DateTime.UtcNow.AddDays(1);

                // Validity of our token
                userToken.Validity = expireTime.TimeOfDay;

                // Generate Our JWT
                var jwToken = new JwtSecurityToken(

                    issuer: jwtSettings.ValidIssuer,
                    audience: jwtSettings.ValidAudience,
                    claims: GetClaims(userToken, out Id),
                    notBefore: new DateTimeOffset(DateTime.Now).DateTime,
                    expires: new DateTimeOffset(expireTime).DateTime,
                    signingCredentials: new SigningCredentials(
                        new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha256));

                userToken.Token = new JwtSecurityTokenHandler().WriteToken(jwToken);
                userToken.Id = model.Id;
                userToken.GuidId = Id;

                return userToken;
            }
            catch (Exception ex)
            {
                throw new Exception("Error generating the JWT", ex);
            }
        }
    }
}
