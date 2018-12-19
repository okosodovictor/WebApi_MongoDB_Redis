using Bit.Application.Interfaces;
using Bit.WebApi;
using Bit.WebApi.App_Start.Ninject.Http;
using Bit.WebApi.Security;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using Swashbuckle.Application;
using System;
using System.Web;
using System.Web.Http;

[assembly: OwinStartup(typeof(Bit.WebApi.Startup))]

namespace Bit.WebApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureNinject
            ConfigureNinject(app);

            // Configure OAUTH
            ConfigureOAuth(app);

            //Configure Web Api
            ConfigureWebApi(app);

            //Add swagger
           // ConfigureSwagger(app);

        }


        private void ConfigureNinject(IAppBuilder app)
        {
            //Configure Ninject Middleware
            NinjectHttpContainer.RegisterAssembly();
        }

        private void ConfigureWebApi(IAppBuilder app)
        {
            // Configure Web API for self-host. 
            HttpConfiguration config = new HttpConfiguration();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(config);
        }

        private void ConfigureOAuth(IAppBuilder app)
        {
            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/authtoken"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new AuthorizationServerProvider(NinjectHttpContainer.Resolve<IUserManager>()),
                RefreshTokenProvider= new RefreshTokenProvider()
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);

            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}