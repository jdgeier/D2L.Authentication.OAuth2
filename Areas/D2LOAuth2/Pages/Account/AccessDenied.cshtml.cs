// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace D2L.Authentication.OAuth2.Areas.D2LOAuth2.UI.Internal
{
    /// <summary>
    /// This API supports infrastructure and is not intended to be used
    /// directly from your code.This API may change or be removed in future releases
    /// </summary>
    [AllowAnonymous]
    public class AccessDeniedModel : PageModel
    {
        /// <summary>
        /// This API supports infrastructure and is not intended to be used
        /// directly from your code.This API may change or be removed in future releases
        /// </summary>
        public void OnGet()
        {
        }
    }
}