using fbuCloset.Models;
using Microsoft.AspNet.Mvc.Facebook;
using Microsoft.AspNet.Mvc.Facebook.Client;
using Microsoft.AspNet.Mvc.Facebook.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace fbuCloset.Controllers
{
    public class MerchantController : Controller
    {
        private uClosetSQLAdminEntities db = new uClosetSQLAdminEntities();

        //
        // GET: /Outfit/
        [FacebookAuthorize("email", "user_photos")]
        public async Task<ActionResult> Index(FacebookContext context)
        {
            string MerchantID = "";
            var ckMerchantID = HttpContext.Request.Cookies["MerchantID"];

            if (ckMerchantID == null)
            {
                MerchantID = "0";
            }
            else
            {
                MerchantID = ckMerchantID.Value.ToString();
            }

            System.Data.Entity.Core.Objects.ObjectResult<spGet_MerchantList_Result> spMerchantList = db.spGet_MerchantList(MerchantID);
            List<spGet_MerchantList_Result> myMerchantList = spMerchantList.ToList();

            ViewBag.myMerchantList = myMerchantList;

            if (ModelState.IsValid)
            {
                var user = await context.Client.GetCurrentUserAsync<MyAppUser>();
                
                return View(user);
            }
            else
            {
                return View("Error");
            }
        }
    }
}
