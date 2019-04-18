using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace D2L.Authentication.OAuth2.Areas.D2LOAuth2.Controllers
{
    [NonController]
    [AllowAnonymous]
    [Area("D2LOAuth2")]
    [Route("[area]/[controller]/[action]")]
    internal class AccountController : Controller
    {
        public AccountController(IOptionsMonitor<D2LOAuth2Options> options)
        {
            Options = options;
        }

        public IOptionsMonitor<D2LOAuth2Options> Options { get; }

        [HttpGet("{scheme?}")]
        public IActionResult SignIn([FromRoute] string scheme)
        {
            scheme = scheme ?? D2LOAuth2Defaults.AuthenticationScheme;
            var redirectUrl = Url.Content("~/");
            return Challenge(
                new AuthenticationProperties { RedirectUri = redirectUrl },
                scheme);
        }

        [HttpGet("{scheme?}")]
        public IActionResult SignOut([FromRoute] string scheme)
        {
            scheme = scheme ?? D2LOAuth2Defaults.AuthenticationScheme;
            var options = Options.Get(scheme);
            var callbackUrl = Url.Page("/Account/SignedOut", pageHandler: null, values: null, protocol: Request.Scheme);
            return SignOut(
                new AuthenticationProperties { RedirectUri = callbackUrl },
                options.CookieSchemeName,
                options.JwtBearerSchemeName);
        }
    }
}
