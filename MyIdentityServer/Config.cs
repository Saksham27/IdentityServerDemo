using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MyIdentityServer
{
    public class Config
    {
        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new List<ApiScope>
            {
                    new ApiScope("api1.read", "My API Read Operations"),
                    new ApiScope("api1.write", "My API Write Operations")
            };
        }
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("api1","Some API 1")
                {
                    Scopes =
                    {
                      "api1.read", "api1.write"
                    }
                },
                new ApiResource
                {
                    Name = "api2",
                    DisplayName = "Some API 2",
                    Scopes =
                    {
                       "api2.delete"
                    }
                }
            };
        }
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "client",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = {"api1.read", "api1.write", "api2"},

                },

                new Client
                {
                    ClientId = "client2",
                    ClientName = "MVCClient",

                    AllowedGrantTypes = GrantTypes.Hybrid,
                    RequirePkce = false,

                    RedirectUris = new [] {"https://localhost:5201/signin-oidc"},
                    PostLogoutRedirectUris = { "https://localhost:5201/signout-callback-oidc" },
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "api1.read", "api1.write", "api2", "custom.profile"
                    },
                    AllowOfflineAccess = true //To enable support for Refresh Token
                }
            };
        }
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            var customProfile = new IdentityResource(
                name: "custom.profile",
                displayName: "Custom profile",
                userClaims: new[] { "name", "email", "status" });
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                customProfile
            };
        }
        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                        SubjectId = "1",
                        Username = "alice",
                        Password = "alice",
                        Claims = new []
                        {
                            new Claim("name", "Alice"),
                            new Claim("website", "https://alice.com"),
                            new Claim("email", "alice@alice.com"),
                            new Claim("status", "Active")

                        }
                    },
                    new TestUser
                    {
                        SubjectId = "2",
                        Username = "bob",
                        Password = "bob",
                        Claims = new []
                        {
                            new Claim("name", "Bob"),
                            new Claim("website", "https://bob.com"),
                            new Claim("email", "bob@bob.com"),
                            new Claim("status", "Inactive")
                        }
                    }
                };

        }

    }

}