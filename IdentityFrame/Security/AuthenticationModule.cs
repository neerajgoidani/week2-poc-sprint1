using System;
using System.Collections.Generic;
using System.IdentityModel.Protocols.WSTrust;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Web;

namespace IdentityFrame.Security
{
    public class AuthenticationModule
    {
        
        private static string Secret = "XCAP05H6LoKvbRRa/QkqLNMI7cOHguaRyHzyg7n5qEkGjQmtBhz4SzYh4Fqwjyi3KJHlSXKPwVu2+bXr6CtpgQ==";

        // The Method is used to generate token for user
        public static string GenerateToken(string username,string role)
        {
            byte[] key = Convert.FromBase64String(Secret);
            SymmetricSecurityKey securityKey = new InMemorySymmetricSecurityKey(key);

            var signingCredentials = new SigningCredentials(securityKey,
               SecurityAlgorithms.HmacSha256Signature, SecurityAlgorithms.Sha256Digest);

            

            var claimsIdentity = new ClaimsIdentity(new[] 
            {
                      new Claim(ClaimTypes.Role, role)
            }
            );

            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                SigningCredentials = signingCredentials,
                Lifetime = new Lifetime(DateTime.UtcNow, DateTime.UtcNow.AddYears(1)),
                AppliesToAddress = "http://www.example.com",
                TokenIssuerName = "self",


            };
        


            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            SecurityToken token = handler.CreateToken(descriptor);

            return handler.WriteToken(token);
        }


        public static ClaimsPrincipal GetPrincipal(string token)
        {
            try
            {
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                JwtSecurityToken jwtToken = (JwtSecurityToken)tokenHandler.ReadToken(token);
                if (jwtToken == null)
                    return null;
                byte[] key = Convert.FromBase64String(Secret);
                TokenValidationParameters parameters = new TokenValidationParameters()
                {
                    RequireExpirationTime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidAudiences = new string[]
                      {
                    "http://www.example.com",
                      },

                    ValidIssuers = new string[]
                  {
                      "self",
                  },
                    IssuerSigningKey = new InMemorySymmetricSecurityKey(key)
                };
                SecurityToken securityToken;
                ClaimsPrincipal principal = tokenHandler.ValidateToken(token,
                      parameters, out securityToken);
                return principal;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static string ValidateToken(string token)
        {
            string userRole = null;
            ClaimsPrincipal principal = GetPrincipal(token);
            if (principal == null)
                return null;
            ClaimsIdentity identity = null;
            try
            {
                identity = (ClaimsIdentity)principal.Identity;
            }
            catch (NullReferenceException)
            {
                return null;
            }
            Claim userRoleClaim = identity.FindFirst(ClaimTypes.Role);
            userRole = userRoleClaim.Value;
            return userRole;
        }

            
        }
    }