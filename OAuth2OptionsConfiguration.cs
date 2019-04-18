using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace D2L.Authentication.OAuth2
{
    internal class OAuth2OptionsConfiguration : IConfigureNamedOptions<OAuthOptions>
    {
        private readonly IOptions<D2LOAuth2SchemeOptions> _schemeOptions;
        private readonly IOptionsMonitor<D2LOAuth2Options> _d2lOAuthOptions;
        public OAuth2OptionsConfiguration(IOptions<D2LOAuth2SchemeOptions> schemeOptions, IOptionsMonitor<D2LOAuth2Options> d2LOAuth2Options)
        {
            _schemeOptions = schemeOptions;
            _d2lOAuthOptions = d2LOAuth2Options;
        }

        public void Configure(string name, OAuthOptions options)
        {
            var d2lOAuthScheme = GetD2LOauth2Scheme(name);
            var d2lOAuthOptions = _d2lOAuthOptions.Get(d2lOAuthScheme);
            if (name != d2lOAuthOptions.JwtBearerSchemeName)
            {
                return;
            }

            options.ClientId = d2lOAuthOptions.ClientId;
            options.ClientSecret = d2lOAuthOptions.ClientSecret;
            options.CallbackPath = d2lOAuthOptions.CallbackPath ?? options.CallbackPath;
            options.SignInScheme = d2lOAuthOptions.CookieSchemeName;
        }

        private string GetD2LOauth2Scheme(string name)
        {
            foreach (var mapping in _schemeOptions.Value.OAuth2Mappings)
            {
                if (mapping.Value.OAuth2Scheme == name)
                {
                    return mapping.Key;
                }
            }

            return null;
        }

        public void Configure(OAuthOptions options)
        {
        }
    }
}
