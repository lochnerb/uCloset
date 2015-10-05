using fbuCloset.Models;
using Microsoft.AspNet.Mvc.Facebook;
using Microsoft.AspNet.Mvc.Facebook.Client;
using Microsoft.AspNet.Mvc.Facebook.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;

using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace fbuCloset.Controllers
{
    public class HomeController : Controller
    {
        private uClosetSQLAdminEntities db = new uClosetSQLAdminEntities();

        [FacebookAuthorize("email", "user_photos", "publish_stream")]
        public async Task<ActionResult> Index(FacebookContext context)
        {
            if (ModelState.IsValid)
            {
                var user = await context.Client.GetCurrentUserAsync<MyAppUser>();

                return View(user);
            }

            return View("Error");
        }

        // This action will handle the redirects from FacebookAuthorizeFilter when
        // the app doesn't have all the required permissions specified in the FacebookAuthorizeAttribute.
        // The path to this action is defined under appSettings (in Web.config) with the key 'Facebook:AuthorizationRedirectPath'.
        public ActionResult Permissions(FacebookRedirectContext context)
        {
            if (ModelState.IsValid)
            {
                return View(context);
            }

            return View("Error");
        }

        public ActionResult Wardrobe()
        {

            return View();
        }
    }
}
