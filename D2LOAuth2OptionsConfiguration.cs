using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace D2L.Authentication.OAuth2
{
    internal class D2LOAuth2OptionsConfiguration : IConfigureNamedOptions<D2LOAuth2Options>
    {
        private readonly IOptions<D2LOAuth2SchemeOptions> _schemeOptions;
        public D2LOAuth2OptionsConfiguration(IOptions<D2LOAuth2SchemeOptions> schemeOptions)
        {
            _schemeOptions = schemeOptions;
        }
        public void Configure(string name, D2LOAuth2Options options)
        {
            if (_schemeOptions.Value.JwtBearerMappings.TryGetValue(name, out var mapping))
            {
                options.JwtBearerSchemeName = mapping.JwtBearerScheme;
                return;
            }
        }

        public void Configure(D2LOAuth2Options options)
        {
        }
    }
}
