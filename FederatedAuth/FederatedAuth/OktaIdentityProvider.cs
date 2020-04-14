namespace Okta.FederatedAuth
{
    public class OktaIdentityProvider : Sitecore.Plugin.IdentityProviders.IdentityProvider
    {
        public string ClientId { get; set; }

        public string ClientSecret { get; set; }

        public string Authority { get; set; }
    }
}
