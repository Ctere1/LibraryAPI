using LibraryAPI.Models;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace LibraryAPI.Providers
{
    public class AuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            //Get admin's and user's credentials 
            List<admin> adminList = new List<admin>();
            List<user> userList = new List<user>();
            libraryManagementEntities db = new libraryManagementEntities();
            adminList = db.admins.ToList();
            userList = db.users.ToList();

            if (adminList.Exists(x => x.name == context.UserName) && adminList.Exists(x => x.password == context.Password))
            {
                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
                identity.AddClaim(new Claim(ClaimTypes.Role, "admin"));
                context.Validated(identity);
            }
            else if (userList.Exists(x => x.email == context.UserName) && userList.Exists(x => x.password == context.Password))
            {
                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                identity.AddClaim(new Claim(ClaimTypes.Email, context.UserName));
                identity.AddClaim(new Claim(ClaimTypes.Role, "user"));
                context.Validated(identity);
            }
            else
            {
                context.SetError("invalid_grant", "User credentials incorrect");
            }
        }
    }
}