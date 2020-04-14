namespace Okta.FederatedAuth.Configuration
{
    public class AppSettings
    {
        public static readonly string SectionName = "Sitecore:ExternalIdentityProviders:IdentityProviders:Okta";

        public OktaIdentityProvider OktaIdentityProvider { get; set; } = new OktaIdentityProvider();
    }
}
