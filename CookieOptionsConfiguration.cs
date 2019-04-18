using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.Extensions.Options;

namespace D2L.Authentication.OAuth2
{
    internal class CookieOptionsConfiguration : IConfigureNamedOptions<CookieAuthenticationOptions>
    {
        private readonly IOptions<D2LOAuth2SchemeOptions> _schemeOptions;
        private readonly IOptionsMonitor<D2LOAuth2Options> _d2lOAuthOptions;

        public CookieOptionsConfiguration(IOptions<D2LOAuth2SchemeOptions> schemeOptions, IOptionsMonitor<D2LOAuth2Options> d2LOAuth2Options)
        {
            _schemeOptions = schemeOptions;
            _d2lOAuthOptions = d2LOAuth2Options;
        }

        public void Configure(string name, CookieAuthenticationOptions options)
        {
            throw new System.NotImplementedException();
        }

        public void Configure(CookieAuthenticationOptions options)
        {
            throw new System.NotImplementedException();
        }
    }
}