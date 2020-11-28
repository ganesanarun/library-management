using System.Collections.Generic;
using IdentityModel;
using IdentityServer4.Models;

namespace Identity_Server
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResource("role", new[] {JwtClaimTypes.Role})
            };

        public static IEnumerable<ApiScope> ApiScopes => new[]
        {
            new ApiScope("library")
        };

        public static IEnumerable<Client> Clients => new[]
        {
            new Client
            {
                ClientName = "Postman",
                ClientId = "Postman",
                RequireClientSecret = false,
                AllowedScopes = new[]
                {
                    "library",
                    "openid",
                    "profile",
                    "role"
                },
                AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials
            }
        };
    }
}