using System.Collections.Generic;
using System.Security.Claims;
using IdentityModel;
using IdentityServer4.Test;

namespace Identity_Server
{
    public static class TestUsers
    {
        public static List<TestUser> Users =>
            new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "818727",
                    Username = "John@gmail.com",
                    Password = "Password@1234",
                    Claims =
                    {
                        new Claim(JwtClaimTypes.Name, "John Smith"),
                        new Claim(JwtClaimTypes.GivenName, "John"),
                        new Claim(JwtClaimTypes.FamilyName, "Smith"),
                        new Claim(JwtClaimTypes.Email, "John@gmail.com"),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        new Claim(JwtClaimTypes.Role, "member")
                    }
                },
                new TestUser
                {
                    SubjectId = "818728",
                    Username = "admin@gmail.com",
                    Password = "Password@1234",
                    Claims =
                    {
                        new Claim(JwtClaimTypes.Name, "Admin Smith"),
                        new Claim(JwtClaimTypes.GivenName, "Admin"),
                        new Claim(JwtClaimTypes.FamilyName, "Smith"),
                        new Claim(JwtClaimTypes.Email, "John@gmail.com"),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        new Claim(JwtClaimTypes.Role, "admin")
                    }
                }
            };
    }
}