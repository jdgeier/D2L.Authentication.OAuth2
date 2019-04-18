using D2L.Authentication.OAuth2.Areas.D2LOAuth2.Controllers;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace D2L.Authentication.OAuth2
{
    internal class D2LOAuth2AccountControllerFeatureProvider : IApplicationFeatureProvider<ControllerFeature>, IApplicationFeatureProvider
    {
        public void PopulateFeature(IEnumerable<ApplicationPart> parts, ControllerFeature feature)
        {
            if (!feature.Controllers.Contains(typeof(AccountController).GetTypeInfo()))
            {
                feature.Controllers.Add(typeof(AccountController).GetTypeInfo());
            }
        }
    }
}
