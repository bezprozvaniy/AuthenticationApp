using System.Security.Principal;
using AuthenticationApp.Core;
using AuthenticationApp.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UAParser;

namespace AuthenticationApp.Controllers
{
    [Route("api/Auth")]
    public class AuthController : Controller
    {
        // GET api/auth
        [HttpGet]
        [Authorize]
        public ActionResult Auth()
        {
            if (User != null)
            {
                var uaParser = Parser.GetDefault();
                var userAgent = uaParser.Parse(Request.Headers["User-Agent"]);
                if (!UserManager.HasUser(User.Identity.Name))
                {
                    UserManager.AddUser(new User(User.Identity.Name, new[] {userAgent.OS.ToString(), userAgent.UA.ToString()}));
                }

                var windowsIdentity = WindowsIdentity.GetCurrent();
                return Json(new {windowsIdentity.Name, windowsIdentity.Token});
            }

            return Unauthorized();
        }
    }
}
