using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;

namespace D2L.Authentication.OAuth2
{
    public static class D2LAuthenticationBuilderExtensions
    {
        public static AuthenticationBuilder AddD2LOAuthBearer(this AuthenticationBuilder builder, Action<D2LOAuth2Options> configureOptions) =>
            builder.AddD2LOAuthBearer(
                D2LOAuth2Defaults.AuthenticationScheme,
                D2LOAuth2Defaults.BearerAuthenticationScheme,
                D2LOAuth2Defaults.CookieScheme,
                D2LOAuth2Defaults.DisplayName,
                configureOptions);
        public static AuthenticationBuilder AddD2LOAuthBearer(
            this AuthenticationBuilder builder,
            string scheme,
            string OAuth2ConnectScheme,
            string cookieScheme,
            string displayName,
            Action<D2LOAuth2Options> configureOptions)
        {
            AddAdditionalMvcApplicationParts(builder.Services);
            builder.AddPolicyScheme(scheme, displayName, o =>
            {
                o.ForwardDefault = cookieScheme;
                o.ForwardChallenge = OAuth2ConnectScheme;
            });

            builder.Services.Configure(TryAddOAuth2CookieSchemeMappings(scheme, OAuth2ConnectScheme, cookieScheme));

            builder.Services.TryAddSingleton<IConfigureOptions<D2LOAuth2Options>, D2LOAuth2OptionsConfiguration>();

            builder.Services.TryAddSingleton<IConfigureOptions<OAuthOptions>, OAuth2OptionsConfiguration>();

            builder.Services.TryAddSingleton<IConfigureOptions<CookieAuthenticationOptions>, CookieOptionsConfiguration>();

            builder.Services.Configure(scheme, configureOptions);

            builder.AddOAuth(OAuth2ConnectScheme, null, o => { });
            builder.AddCookie(cookieScheme, null, o => { });

            return builder;
        }

        private static Action<D2LOAuth2SchemeOptions> TryAddOAuth2CookieSchemeMappings(string scheme, string oAuth2ConnectScheme, string cookieScheme)
        {
            return TryAddMapping;

            void TryAddMapping(D2LOAuth2SchemeOptions o)
            {
                if (o.OAuth2Mappings.ContainsKey(scheme))
                {
                    throw new InvalidOperationException($"A scheme with the name '{scheme}' was already added.");
                }
                foreach (var mapping in o.OAuth2Mappings)
                {
                    if (mapping.Value.CookieScheme == cookieScheme)
                    {
                        throw new InvalidOperationException(
                            $"The cookie scheme '{cookieScheme}' can't be associated with the Azure Active Directory scheme '{scheme}'. " +
                            $"The cookie scheme '{cookieScheme}' is already mapped to the Azure Active Directory scheme '{mapping.Key}'");
                    }

                    if (mapping.Value.OAuth2Scheme == oAuth2ConnectScheme)
                    {
                        throw new InvalidOperationException(
                            $"The Open ID Connect scheme '{oAuth2ConnectScheme}' can't be associated with the Azure Active Directory scheme '{scheme}'. " +
                            $"The Open ID Connect scheme '{oAuth2ConnectScheme}' is already mapped to the Azure Active Directory scheme '{mapping.Key}'");
                    }
                }
                o.OAuth2Mappings.Add(scheme, new D2LOAuth2SchemeOptions.D2LOAuth2SchemeMapping
                {
                    OAuth2Scheme = oAuth2ConnectScheme,
                    CookieScheme = cookieScheme
                });
            };
        }
        private static void AddAdditionalMvcApplicationParts(IServiceCollection services)
        {
            var additionalParts = GetAdditionalParts();
            var mvcBuilder = services
                .AddMvc()
                .ConfigureApplicationPartManager(apm =>
                {
                    foreach (var part in additionalParts)
                    {
                        if (!apm.ApplicationParts.Any(ap => HasSameName(ap.Name, part.Name)))
                        {
                            apm.ApplicationParts.Add(part);
                        }
                    }

                    apm.FeatureProviders.Add(new D2LOAuth2AccountControllerFeatureProvider());
                });

            bool HasSameName(string left, string right) => string.Equals(left, right, StringComparison.Ordinal);
        }

        private static IEnumerable<ApplicationPart> GetAdditionalParts()
        {
            var thisAssembly = typeof(D2LOAuth2AccountControllerFeatureProvider).Assembly;
            var relatedAssemblies = RelatedAssemblyAttribute.GetRelatedAssemblies(thisAssembly, throwOnError: true);

            foreach (var reference in relatedAssemblies)
            {
                yield return new CompiledRazorAssemblyPart(reference);
            }
        }
    }
}
