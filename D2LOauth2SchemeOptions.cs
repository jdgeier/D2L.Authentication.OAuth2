using System;
using System.Collections.Generic;
using System.Text;

namespace D2L.Authentication.OAuth2
{
    internal class D2LOAuth2SchemeOptions
    {
        public IDictionary<string, D2LOAuth2SchemeMapping> OAuth2Mappings { get; set; } = new Dictionary<string, D2LOAuth2SchemeMapping>();
        public IDictionary<string, JwtBearerSchemeMapping> JwtBearerMappings { get; set; } = new Dictionary<string, JwtBearerSchemeMapping>();
        public class D2LOAuth2SchemeMapping
        {
            public string OAuth2Scheme { get; set; }
            public string CookieScheme { get; set; }
        }


        public class JwtBearerSchemeMapping
        {
            public string JwtBearerScheme { get; set; }
        }
    }
}
