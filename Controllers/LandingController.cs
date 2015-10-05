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
    public class LandingController : Controller
    {
        private uClosetSQLAdminEntities db = new uClosetSQLAdminEntities();

        [FacebookAuthorize("email", "user_photos", "publish_stream")]
        public async Task<ActionResult> Index(FacebookContext context)
        {
            if (ModelState.IsValid)
            {
                var user = await context.Client.GetCurrentUserAsync<MyAppUser>();
                long startTime = DateTime.UtcNow.Ticks;

                //Store Access Token
                HttpCookie cookieToken = new HttpCookie("UserToken");
                cookieToken.Value = context.AccessToken;
                this.ControllerContext.HttpContext.Response.Cookies.Add(cookieToken);

                string SessionID = "";

                var ckUsername = HttpContext.Request.Cookies["UserName"];
                string Username = "";

                if (ckUsername == null)
                {
                    Username = "";
                }
                else
                {
                    Username = ckUsername.Value.ToString();
                }

                if (Request.Cookies["UserSessionID"] == null)
                {
                    SessionID = System.Guid.NewGuid().ToString();
                }
                else if (Request.Cookies["UserSessionID"].ToString() == "")
                {
                    SessionID = System.Guid.NewGuid().ToString();
                }
                else
                {
                    SessionID = Request.Cookies["UserSessionID"].Value.ToString();
                }

                System.Data.Entity.Core.Objects.ObjectResult<AddSession_Result> spResult = db.AddSession(SessionID, user.Id.ToString(), user.Name, 2, "FaceBook HomePage", "fbMVCApp");
                List<AddSession_Result> myResults = spResult.ToList();

                if (myResults.Count > 0)
                {
                    //Get UserSessionID
                    if (myResults[0].SessionID == null)
                    {
                        HttpContext.Response.Cookies.Add(new HttpCookie("UserSessionID", ""));

                        HttpCookie cookie = new HttpCookie("UserSessionID");
                        cookie.Value = "";
                        this.ControllerContext.HttpContext.Response.Cookies.Add(cookie);
                    }
                    else
                    {
                        HttpCookie cookie = new HttpCookie("UserSessionID");
                        cookie.Value = myResults[0].SessionID.ToString();
                        this.ControllerContext.HttpContext.Response.Cookies.Add(cookie);
                    }
                    //Get UserID
                    if (myResults[0].UserID == null)
                    {
                        HttpCookie cookie1 = new HttpCookie("UserID");
                        cookie1.Value = "";
                        this.ControllerContext.HttpContext.Response.Cookies.Add(cookie1);
                    }
                    else
                    {
                        HttpCookie cookie1 = new HttpCookie("UserID");
                        cookie1.Value = myResults[0].UserID.ToString();
                        ViewBag.UserID = myResults[0].UserID.ToString();
                        this.ControllerContext.HttpContext.Response.Cookies.Add(cookie1);
                    }
                    //Get sessFBUserID
                    if (myResults[0].FBUserID == null)
                    {
                        HttpCookie cookie2 = new HttpCookie("FBUserID");
                        cookie2.Value = "";
                        this.ControllerContext.HttpContext.Response.Cookies.Add(cookie2);
                    }
                    else
                    {
                        HttpCookie cookie2 = new HttpCookie("FBUserID");
                        cookie2.Value = myResults[0].FBUserID.ToString();
                        this.ControllerContext.HttpContext.Response.Cookies.Add(cookie2);
                    }

                    //Get UserName
                    if (myResults[0].UserName == null)
                    {
                        HttpCookie cookie3 = new HttpCookie("UserName");
                        cookie3.Value = "";
                        this.ControllerContext.HttpContext.Response.Cookies.Add(cookie3);
                    }
                    else
                    {
                        HttpCookie cookie3 = new HttpCookie("UserName");
                        cookie3.Value = myResults[0].UserName.ToString();
                        this.ControllerContext.HttpContext.Response.Cookies.Add(cookie3);
                    }
                    //Get Firstname
                    if (myResults[0].Firstname == null)
                    {
                        HttpCookie cookie4 = new HttpCookie("Firstname");
                        cookie4.Value = "";
                        this.ControllerContext.HttpContext.Response.Cookies.Add(cookie4);
                    }
                    else
                    {
                        HttpCookie cookie4 = new HttpCookie("Firstname");
                        cookie4.Value = myResults[0].Firstname.ToString();
                        this.ControllerContext.HttpContext.Response.Cookies.Add(cookie4);
                    }

                    //Get LastName
                    if (myResults[0].LastName == null)
                    {
                        HttpCookie cookie5 = new HttpCookie("LastName");
                        cookie5.Value = "";
                        this.ControllerContext.HttpContext.Response.Cookies.Add(cookie5);
                    }
                    else
                    {
                        HttpCookie cookie5 = new HttpCookie("LastName");
                        cookie5.Value = myResults[0].LastName.ToString();
                        this.ControllerContext.HttpContext.Response.Cookies.Add(cookie5);
                    }

                    //Get EmailAddress
                    if (myResults[0].EmailAddress == null)
                    {
                        HttpCookie cookie6 = new HttpCookie("EmailAddress");
                        cookie6.Value = "";
                        this.ControllerContext.HttpContext.Response.Cookies.Add(cookie6);
                    }
                    else
                    {
                        HttpCookie cookie6 = new HttpCookie("EmailAddress");
                        cookie6.Value = myResults[0].EmailAddress.ToString();
                        this.ControllerContext.HttpContext.Response.Cookies.Add(cookie6);
                    }

                    //Get LoggedIN
                    if (myResults[0].LoggedIN == null)
                    {
                        HttpCookie cookie7 = new HttpCookie("LoggedIN");
                        cookie7.Value = "";
                        this.ControllerContext.HttpContext.Response.Cookies.Add(cookie7);
                    }
                    else
                    {
                        HttpCookie cookie7 = new HttpCookie("LoggedIN");
                        cookie7.Value = myResults[0].LoggedIN.ToString();
                        this.ControllerContext.HttpContext.Response.Cookies.Add(cookie7);
                    }

                    //Get PrivID
                    if (myResults[0].PrivID == null)
                    {
                        HttpCookie cookie8 = new HttpCookie("PrivID");
                        cookie8.Value = "";
                        this.ControllerContext.HttpContext.Response.Cookies.Add(cookie8);
                    }
                    else
                    {
                        HttpCookie cookie8 = new HttpCookie("PrivID");
                        cookie8.Value = myResults[0].PrivID.ToString();
                        this.ControllerContext.HttpContext.Response.Cookies.Add(cookie8);
                    }

                    //Get PrivilegesDesc
                    if (myResults[0].PrivilegesDesc == null)
                    {
                        HttpCookie cookie9 = new HttpCookie("PrivilegesDesc");
                        cookie9.Value = "";
                        this.ControllerContext.HttpContext.Response.Cookies.Add(cookie9);
                    }
                    else
                    {
                        HttpCookie cookie9 = new HttpCookie("PrivilegesDesc");
                        cookie9.Value = myResults[0].PrivilegesDesc.ToString();
                        this.ControllerContext.HttpContext.Response.Cookies.Add(cookie9);
                    }

                    //Get SessionStatus
                    if (myResults[0].SessionStatus == null)
                    {
                        HttpCookie cookie10 = new HttpCookie("PrivilegesDesc");
                        cookie10.Value = "";
                        this.ControllerContext.HttpContext.Response.Cookies.Add(cookie10);
                    }
                    else
                    {
                        HttpCookie cookie10 = new HttpCookie("PrivilegesDesc");
                        cookie10.Value = myResults[0].SessionStatus.ToString();
                        this.ControllerContext.HttpContext.Response.Cookies.Add(cookie10);
                    }

                    //Get SessionCreateTime
                    if (myResults[0].SessionCreateTime == null)
                    {
                        HttpCookie cookie11 = new HttpCookie("SessionCreateTime");
                        cookie11.Value = "";
                        this.ControllerContext.HttpContext.Response.Cookies.Add(cookie11);
                    }
                    else
                    {
                        HttpCookie cookie11 = new HttpCookie("SessionCreateTime");
                        cookie11.Value = myResults[0].SessionCreateTime.ToString();
                        this.ControllerContext.HttpContext.Response.Cookies.Add(cookie11);
                    }

                    //Get SessionLastActivityTime
                    if (myResults[0].SessionLastActivityTime == null)
                    {
                        HttpCookie cookie12 = new HttpCookie("SessionLastActivityTime");
                        cookie12.Value = "";
                        this.ControllerContext.HttpContext.Response.Cookies.Add(cookie12);
                    }
                    else
                    {
                        HttpCookie cookie12 = new HttpCookie("SessionLastActivityTime");
                        cookie12.Value = myResults[0].SessionLastActivityTime.ToString();
                        this.ControllerContext.HttpContext.Response.Cookies.Add(cookie12);
                    }

                    //Get SessionLastActivityTime
                    if (myResults[0].fbUserFacebookID == null)
                    {
                        HttpCookie cookie13 = new HttpCookie("fbUserFacebookID");
                        cookie13.Value = "";
                        this.ControllerContext.HttpContext.Response.Cookies.Add(cookie13);
                    }
                    else
                    {
                        HttpCookie cookie13 = new HttpCookie("fbUserFacebookID");
                        cookie13.Value = myResults[0].fbUserFacebookID.ToString();
                        this.ControllerContext.HttpContext.Response.Cookies.Add(cookie13);
                    }

                    System.Data.Entity.Core.Objects.ObjectResult<AddfbUser_Result> spFBResult = db.AddfbUser(user.Id.ToString(), user.Name.ToString(), user.Name.ToString(), user.Email.ToString(), "FBApp");
                }
                else
                {
                    HttpCookie cookie14 = new HttpCookie("UserSessionID");
                    cookie14.Value = "";
                    this.ControllerContext.HttpContext.Response.Cookies.Add(cookie14);

                    cookie14 = new HttpCookie("UserID");
                    cookie14.Value = "";
                    this.ControllerContext.HttpContext.Response.Cookies.Add(cookie14);

                    cookie14 = new HttpCookie("FBUserID");
                    cookie14.Value = "";
                    this.ControllerContext.HttpContext.Response.Cookies.Add(cookie14);

                    cookie14 = new HttpCookie("UserName");
                    cookie14.Value = "";
                    this.ControllerContext.HttpContext.Response.Cookies.Add(cookie14);

                    cookie14 = new HttpCookie("Firstname");
                    cookie14.Value = "";
                    this.ControllerContext.HttpContext.Response.Cookies.Add(cookie14);

                    cookie14 = new HttpCookie("LastName");
                    cookie14.Value = "";
                    this.ControllerContext.HttpContext.Response.Cookies.Add(cookie14);

                    cookie14 = new HttpCookie("EmailAddress");
                    cookie14.Value = "";
                    this.ControllerContext.HttpContext.Response.Cookies.Add(cookie14);

                    cookie14 = new HttpCookie("LoggedIN");
                    cookie14.Value = "";
                    this.ControllerContext.HttpContext.Response.Cookies.Add(cookie14);

                    cookie14 = new HttpCookie("PrivID");
                    cookie14.Value = "";
                    this.ControllerContext.HttpContext.Response.Cookies.Add(cookie14);

                    cookie14 = new HttpCookie("PrivilegesDesc");
                    cookie14.Value = "";
                    this.ControllerContext.HttpContext.Response.Cookies.Add(cookie14);

                    cookie14 = new HttpCookie("SessionStatus");
                    cookie14.Value = "";
                    this.ControllerContext.HttpContext.Response.Cookies.Add(cookie14);

                    cookie14 = new HttpCookie("SessionCreateTime");
                    cookie14.Value = "";
                    this.ControllerContext.HttpContext.Response.Cookies.Add(cookie14);

                    cookie14 = new HttpCookie("SessionLastActivityTime");
                    cookie14.Value = "";
                    this.ControllerContext.HttpContext.Response.Cookies.Add(cookie14);

                    cookie14 = new HttpCookie("fbUserFacebookID");
                    cookie14.Value = "";
                    this.ControllerContext.HttpContext.Response.Cookies.Add(cookie14);
                }

                System.Data.Entity.Core.Objects.ObjectResult<spView_FeatureOutfit_Result> spOutfitParts = db.spView_FeatureOutfit("A76D4B67-726B-45C6-B581-C3C3793A443F");
                List<spView_FeatureOutfit_Result> myOutfitParts = spOutfitParts.ToList();
                if (myOutfitParts.Count > 0)
                {
                    string OutfitLBL = "";

                    ViewBag.LoadOutfitParts = myOutfitParts;

                    OutfitLBL = myOutfitParts[0].uoUserOutfitDesc;
                    OutfitLBL = OutfitLBL.Replace("%20", " ");

                    ViewBag.OutfitDesc = OutfitLBL;

                }
                else
                {
                    ViewBag.LoadOutfitParts = null;
                    ViewBag.OutfitDesc = "";
                }


                long endTime = DateTime.UtcNow.Ticks; ;
                long lenTime;

                lenTime = (endTime - startTime) / 10000000;

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
