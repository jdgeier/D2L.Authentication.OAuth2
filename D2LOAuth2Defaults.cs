using System;
using System.Collections.Generic;
using System.Text;

namespace D2L.Authentication.OAuth2
{
    public class D2LOAuth2Defaults
    {
        /// <summary>
        /// The scheme name for cookies when using
        /// <see cref="D2LAuthenticationBuilderExtensions.AddD2LOAuthBearer(AuthenticationBuilder, System.Action{D2LOAuth2Options})"/>.
        /// </summary>
        public const string CookieScheme = "D2LTokenCookie";

        /// <summary>
        /// The default scheme for Azure Active Directory Bearer.
        /// </summary>
        public const string BearerAuthenticationScheme = "D2LTokenBearer";

        /// <summary>
        /// The scheme name for JWT Bearer when using
        /// <see cref="D2LAuthenticationBuilderExtensions.AddD2LOAuthBearer(AuthenticationBuilder, System.Action{D2LOAuth2Options})"/>.
        /// </summary>
        public const string JwtBearerAuthenticationScheme = "D2LTokenJwtBearer";

        /// <summary>
        /// The default scheme for Azure Active Directory.
        /// </summary>
        public const string AuthenticationScheme = "D2LToken";

        /// <summary>
        /// The display name for Azure Active Directory.
        /// </summary>
        public static readonly string DisplayName = "Desire 2 Learn OAuth2";
    }
}
