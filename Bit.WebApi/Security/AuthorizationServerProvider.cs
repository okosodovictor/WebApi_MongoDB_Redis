using Bit.Application.Interfaces;
using Bit.Application.Repository;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace Bit.WebApi.Security
{
    public class AuthorizationServerProvider: OAuthAuthorizationServerProvider
    {
        private readonly IUserManager _manager;

        public AuthorizationServerProvider(IUserManager manager)
        {
            _manager= manager;
        }

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            await Task.Run(() => context.Validated());
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            //Add Cors Header to Request
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
            //Get User Information
            var user = await  _manager.GetUserAsync(context.UserName, context.Password);

                if (user!= null)
                {           
                    //Create Identity
                    var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                    identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user._id));
                    identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));

                    //Validate Request
                    context.Validated(identity);
                }
                else
                {
                    context.SetError("invalid_grant", "The user name or password is incorrect.");
                    return;
                }
        }

        public override Task GrantRefreshToken(OAuthGrantRefreshTokenContext context)
        {
            return base.GrantRefreshToken(context);

        }
    }
}