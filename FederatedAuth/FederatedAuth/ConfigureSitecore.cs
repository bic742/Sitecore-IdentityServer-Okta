using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Okta.FederatedAuth.Configuration;
using Sitecore.Framework.Runtime.Configuration;

namespace Okta.FederatedAuth
{
    public class ConfigureSitecore
    {
        private readonly ILogger<ConfigureSitecore> _logger;
        private readonly AppSettings _appSettings;

        public ConfigureSitecore(ISitecoreConfiguration scConfig, ILogger<ConfigureSitecore> logger)
        {
            this._logger = logger;
            this._appSettings = new AppSettings();
            scConfig.GetSection(AppSettings.SectionName);
            scConfig.GetSection(AppSettings.SectionName).Bind((object)this._appSettings.OktaIdentityProvider);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var oktaProvider = this._appSettings.OktaIdentityProvider;
            if (!oktaProvider.Enabled)
                return;
            this._logger.LogDebug("Configure '" + oktaProvider.DisplayName + "'. AuthenticationScheme = " + oktaProvider.AuthenticationScheme + ", ClientId = " + oktaProvider.ClientId, Array.Empty<object>());
            new AuthenticationBuilder(services).AddOpenIdConnect(oktaProvider.AuthenticationScheme, oktaProvider.DisplayName, (Action<OpenIdConnectOptions>)(options =>
            {
                options.SignInScheme = "idsrv.external";
                options.ClientId = oktaProvider.ClientId;
                options.ClientSecret = oktaProvider.ClientSecret;
                options.Authority = oktaProvider.Authority;
                options.CallbackPath = "/signin-idsrv";

                options.Scope.Add("groups");

                options.Events.OnAuthenticationFailed += (Func<AuthenticationFailedContext, Task>)(context =>
                {
                    return Task.CompletedTask;
                });

                options.Events.OnRedirectToIdentityProvider += (Func<RedirectContext, Task>)(context =>
                {
                    var first = context.HttpContext.User.FindFirst("idp");
                    if (string.Equals(first?.Value, oktaProvider.AuthenticationScheme, StringComparison.Ordinal))
                        context.ProtocolMessage.Prompt = "select_account";
                    return Task.CompletedTask;
                });
            }));
        }
    }
}
