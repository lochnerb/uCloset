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

using Facebook;
using System.Dynamic;

namespace fbuCloset.Controllers
{
    public class OutfitController : Controller
    {
        private uClosetSQLAdminEntities db = new uClosetSQLAdminEntities();

        //
        // GET: /Outfit/
        [FacebookAuthorize("email", "user_photos")]
        public async Task<ActionResult> Index(FacebookContext context)
        {
            if (ModelState.IsValid)
            {
                var user = await context.Client.GetCurrentUserAsync<MyAppUser>();
                
                var ckUserName = HttpContext.Request.Cookies["UserName"];
                string UserName = "";

                if (ckUserName == null)
                {
                    UserName = "";
                }
                else
                {
                    UserName = ckUserName.Value.ToString();
                }

                ViewBag.AppUser = UserName;

                var ckUserID = HttpContext.Request.Cookies["UserID"];
                string userID = "";

                if (ckUserID == null)
                {
                    userID = "";
                }
                else
                {
                    userID = ckUserID.Value.ToString();
                }

                ViewBag.userID = userID;

                System.Data.Entity.Core.Objects.ObjectResult<spUserType_Result> spUsertypeV = db.spUserType(userID);
                List<spUserType_Result> myUsertypeV = spUsertypeV.ToList();

                if (myUsertypeV.Count > 0)
                {
                    if (myUsertypeV[0].userTypeID == 2)
                    {
                        ViewBag.VenderID = userID;
                    }
                    else
                    {
                        ViewBag.VenderID = "";
                    }
                }
                else
                {
                    ViewBag.VenderID = "";
                }

                var ckFBuserID = HttpContext.Request.Cookies["FBUserID"];
                string FBuserID = "";

                if (ckFBuserID == null)
                {
                    FBuserID = "";
                }
                else
                {
                    FBuserID = ckFBuserID.Value.ToString();
                }

                System.Data.Entity.Core.Objects.ObjectResult<spGetFBUserList_Result> spShareUsers = db.spGetFBUserList(userID);
                List<spGetFBUserList_Result> myShareUsers = spShareUsers.ToList();
                ViewBag.ShareUsers = myShareUsers;

                string uClosetUserID = "";
                var uClosetUserIDck = HttpContext.Request.Cookies["uClosetUserID"];

                if (uClosetUserIDck == null)
                {
                    uClosetUserID = userID;
                    ViewBag.uClosetUserID = uClosetUserID;
                }
                else
                {
                    uClosetUserID = uClosetUserIDck.Value.ToString();
                    ViewBag.uClosetUserID = uClosetUserID;
                }

                string WardrobeClosetUserID = "";
                var WardrobeClosetUserIDck = HttpContext.Request.Cookies["WardrobeLVL0"];

                if (WardrobeClosetUserIDck == null)
                {
                    WardrobeClosetUserID = userID;
                    ViewBag.WardrobeUserID = WardrobeClosetUserID;
                }
                else
                {
                    WardrobeClosetUserID = WardrobeClosetUserIDck.Value.ToString();
                    ViewBag.WardrobeUserID = WardrobeClosetUserID;
                }

                string OutfitClosetUserID = "";
                var OutfitClosetUserIDck = HttpContext.Request.Cookies["OutfitLVL0"];

                if (OutfitClosetUserIDck == null)
                {
                    OutfitClosetUserID = userID;
                    ViewBag.OutfitClosetUserID = OutfitClosetUserID;
                }
                else
                {
                    OutfitClosetUserID = OutfitClosetUserIDck.Value.ToString();
                    ViewBag.OutfitClosetUserID = OutfitClosetUserID;
                }

                var ckSaveOutfit = HttpContext.Request.Cookies["ckSaveOutfit"];

                var ckMannequinTop = HttpContext.Request.Cookies["ckMannequinTop"];
                var ckMannequinLegs = HttpContext.Request.Cookies["ckMannequinLegs"];
                var ckMannequinFeet = HttpContext.Request.Cookies["ckMannequinFeet"];
                var ckMannequinHand = HttpContext.Request.Cookies["ckMannequinHand"];
                var ckMannequinFoot = HttpContext.Request.Cookies["ckMannequinFoot"];

                var ckShareOutFit = HttpContext.Request.Cookies["ckShareOutFit"];

                if (ckShareOutFit != null)
                {
                    if (ckShareOutFit.Value == "Y")
                    {
                        Facebook.FacebookClient myClient = new Facebook.FacebookClient(context.AccessToken);
                    }
                }

                var ckOutfitLBL = HttpContext.Request.Cookies["ckOutfitLBL"];
                var ckOutfitID = HttpContext.Request.Cookies["ckOutfitID"];
                var ckOutfitLoadID = HttpContext.Request.Cookies["ckOutfitLoadID"];
                var ckGenderID = HttpContext.Request.Cookies["GenderID"];
                string OutfitID = "";
                string OutfitLoadID = "";
                var MannequinTop = "";
                var MannequinLegs = "";
                var MannequinFeet = "";
                var MannequinHand = "";
                var MannequinFoot = "";
                string OutfitLBL = "";

                //Save OutfitID
                if (ckOutfitLoadID == null)
                {
                    if (ckOutfitID == null)
                    {
                        ViewBag.OutfitID = "";

                    }
                    else
                    {
                        ViewBag.OutfitID = ckOutfitID.Value;
                    }
                }
                else
                {
                    if (ckOutfitLoadID.Value == "")
                    {
                        if (ckOutfitID == null)
                        {
                            ViewBag.OutfitID = "";

                        }
                        else
                        {
                            ViewBag.OutfitID = ckOutfitID.Value;
                        }
                    }
                    else
                    {
                        ViewBag.OutfitID = ckOutfitLoadID.Value;
                    }
                }

                //System.Data.Entity.Core.Objects.ObjectResult<spGetFBUserOutfitList_Result> spShareOutfitUsers = db.spGetFBUserOutfitList(uClosetUserID);
                //List<spGetFBUserOutfitList_Result> myShareOutfitUsers = spShareOutfitUsers.ToList();
                //ViewBag.ShareOutfitUsers = myShareOutfitUsers;

                if (ckOutfitLoadID != null) 
                {
                    OutfitLoadID = ckOutfitLoadID.Value;

                    if (OutfitLoadID != "")
                    {
                        System.Data.Entity.Core.Objects.ObjectResult<spView_UserOutfit_Result> spOutfitParts = db.spView_UserOutfit(OutfitClosetUserID, OutfitLoadID, OutfitClosetUserID);
                        List<spView_UserOutfit_Result> myOutfitParts = spOutfitParts.ToList();
                        if (myOutfitParts.Count > 0)
                        {
                            if (myOutfitParts[0].uoUserOutfitDesc != null)
                            {
                                if (myOutfitParts[0].uoUserOutfitDesc != "tmpOutfit")
                                {
                                    OutfitLBL = myOutfitParts[0].uoUserOutfitDesc;
                                    OutfitLBL = OutfitLBL.Replace("%20", " ");
                                    ViewBag.OutfitLBL = OutfitLBL;
                                }
                                else
                                {
                                    OutfitLBL = "";
                                    ViewBag.OutfitLBL = OutfitLBL;
                                }
                            }

                            if (myOutfitParts.Count > 0)
                            {
                                if ((myOutfitParts[0].uopAttachmentID == null) && (myOutfitParts.Count == 1))
                                {
                                    ViewBag.EmptyParts = 0;
                                }
                                else
                                {
                                    ViewBag.EmptyParts = 1;
                                }
                            }
                            else
                            {
                                ViewBag.EmptyParts = 0;
                            }


                            if ((myOutfitParts[0].uopAttachmentID != null) || (myOutfitParts.Count > 1))
                            {
                                ViewBag.LoadOutfitParts = myOutfitParts;
                            }
                            else
                            {
                                ViewBag.LoadOutfitParts = null;
                            }

                            if (OutfitLBL == null)
                            {
                                ViewBag.OutfitDesc = "";
                            }
                            else
                            {
                                ViewBag.OutfitDesc = OutfitLBL;
                            }
                        }
                        else
                        {
                            ViewBag.LoadOutfitParts = null;
                            ViewBag.OutfitDesc = "";
                        }
                    }
                }
                else
                {
                    List<spView_UserOutfit_Result> myOutfitParts = null;
                    ViewBag.LoadOutfitParts = myOutfitParts;
                }

                if (ckMannequinTop != null)
                {
                    MannequinTop = ckMannequinTop.Value;
                }
                if (ckMannequinLegs != null)
                {
                    MannequinLegs = ckMannequinLegs.Value;
                }
                if (ckMannequinFeet != null)
                {
                    MannequinFeet = ckMannequinFeet.Value;
                }
                if (ckMannequinHand != null)
                {
                    MannequinHand = ckMannequinHand.Value;
                }
                if (ckMannequinFoot != null)
                {
                    MannequinFoot = ckMannequinFoot.Value;
                }
                if (ckOutfitLBL != null)
                {
                    ckOutfitLBL.Value = ckOutfitLBL.Value.Replace("%20", "");

                    if (ckOutfitLBL.Value.Trim() != "")
                    {
                        OutfitLBL = ckOutfitLBL.Value;
                        OutfitLBL = OutfitLBL.Replace("%20", " ");
                    }
                }

                var intOutFitCategoryID = 0;
                var ckOutFitCategoryID = HttpContext.Request.Cookies["bxOutFitCategoryID"];
                if (ckOutFitCategoryID != null)
                {
                    int.TryParse(ckOutFitCategoryID.Value, out intOutFitCategoryID);
                }

                int GenderID = 0;

                if (ckGenderID != null)
                {
                    if (ckGenderID.Value == "")
                    {
                        GenderID = 0;
                    }
                    else
                    {
                        int.TryParse(ckGenderID.Value, out GenderID);
                    }
                }
                else
                {
                    GenderID = 0;
                }

                if (ckSaveOutfit != null)
                {
                    if (ckSaveOutfit.Value == "1")
                    {
                        if (ckOutfitID != null)
                        { 
                            OutfitID = ckOutfitID.Value;
                        }

                        OutfitID = SaveImage(uClosetUserID, intOutFitCategoryID, OutfitLBL, OutfitID, GenderID, MannequinTop, MannequinLegs, MannequinFeet, MannequinHand, MannequinFoot, false);

                        OutfitID = "";
                        OutfitLoadID = "";
                        ViewBag.OutfitID = "";
                        ViewBag.LoadOutfitParts = null;

                        HttpCookie hc = new HttpCookie("ckMannequinTop", "");
                        Response.Cookies.Add(hc);
                        hc = new HttpCookie("ckMannequinLegs", "");
                        Response.Cookies.Add(hc);
                        hc = new HttpCookie("ckMannequinFeet", "");
                        Response.Cookies.Add(hc);
                        hc = new HttpCookie("ckMannequinHand", "");
                        Response.Cookies.Add(hc);
                        hc = new HttpCookie("ckMannequinFoot", "");
                        Response.Cookies.Add(hc);

                        hc = new HttpCookie("ckOutfitLBL", "");
                        Response.Cookies.Add(hc);
                        hc = new HttpCookie("ckOutfitID", "");
                        Response.Cookies.Add(hc);
                        hc = new HttpCookie("ckOutfitLoadID", "");
                        Response.Cookies.Add(hc);

                        ViewBag.OutfitDesc = "";

                    }
                    else
                    {
                        if (OutfitLoadID == "")
                        {
                            if (ckOutfitID != null)
                            {
                                if (ckOutfitID.Value == "")
                                {
                                    OutfitLBL = "tmpOutfit";
                                    OutfitID = "";
                                }
                                else
                                {
                                    OutfitID = ckOutfitID.Value;
                                }
                            }
                            else
                            {
                                OutfitLBL = "tmpOutfit";
                                OutfitID = "";
                            }

                            Boolean OutfitSessionFG = false;

                            if ((OutfitLBL == "tmpOutfit") || (OutfitLBL.Trim() == "")) 
                            {
                                OutfitSessionFG = true;
                                OutfitLBL = "tmpOutfit";
                            }
                            else
                            {
                                OutfitSessionFG = false;
                            }

                            OutfitID = SaveImage(uClosetUserID, intOutFitCategoryID, OutfitLBL, OutfitID, GenderID, MannequinTop, MannequinLegs, MannequinFeet, MannequinHand, MannequinFoot, OutfitSessionFG);
                        }
                        else
                        {
                            Boolean OutfitSessionFG = false;

                            if (((OutfitLBL == "tmpOutfit") || (OutfitLBL.Trim() == "")) && (OutfitLoadID.Trim() == ""))
                            {
                                OutfitSessionFG = true;
                                OutfitLBL = "tmpOutfit";
                            }
                            else
                            {
                                OutfitSessionFG = false;
                            }
                            
                            OutfitID = SaveImage(uClosetUserID, intOutFitCategoryID, OutfitLBL, OutfitLoadID, GenderID, MannequinTop, MannequinLegs, MannequinFeet, MannequinHand, MannequinFoot, OutfitSessionFG);

                            System.Data.Entity.Core.Objects.ObjectResult<spView_UserOutfit_Result> spOutfitParts = db.spView_UserOutfit(OutfitClosetUserID, OutfitID, OutfitClosetUserID);
                            List<spView_UserOutfit_Result> myOutfitParts = spOutfitParts.ToList();

                            ViewBag.LoadOutfitParts = myOutfitParts;

                            if (myOutfitParts.Count > 0)
                            {
                                if ((myOutfitParts[0].uopAttachmentID == null) && (myOutfitParts.Count == 1))
                                {
                                    ViewBag.EmptyParts = 0;
                                }
                                else
                                {
                                    ViewBag.EmptyParts = 1;
                                }
                            }
                            else
                            {
                                ViewBag.EmptyParts = 0;
                            }
                        }
                    }
                }
                else
                {
                    if (ckOutfitID != null)
                    {
                        if (ckOutfitID.Value == "")
                        {
                            OutfitLBL = "tmpOutfit";
                        }
                        else
                        {
                            OutfitID = ckOutfitID.Value;

                        }
                    }
                    else
                    {
                        OutfitLBL = "tmpOutfit";
                    }

                    if (intOutFitCategoryID != 0)
                    {
                        OutfitID = SaveImage(uClosetUserID, intOutFitCategoryID, OutfitLBL, OutfitID, GenderID, MannequinTop, MannequinLegs, MannequinFeet, MannequinHand, MannequinFoot, true);

                        ViewBag.OutfitID = OutfitID;
                    }
                    else
                    {
                        ViewBag.OutfitID = null;
                    }
                }

                System.Data.Entity.Core.Objects.ObjectResult<spView_UserOutfitsDesc_Result> spOutfitList = db.spView_UserOutfitsDesc(userID, intOutFitCategoryID, OutfitClosetUserID);
                List<spView_UserOutfitsDesc_Result> myOutfitList = spOutfitList.ToList();
                ViewBag.OutfitList = myOutfitList;

                var ckoWardrobeLine1 = HttpContext.Request.Cookies["WardrobeLVL1"];
                var ckoWardrobeLine2 = HttpContext.Request.Cookies["WardrobeLVL2"];
                var ckoWardrobeLine3 = HttpContext.Request.Cookies["WardrobeLVL3"];
                var ckoWardrobeLine4 = HttpContext.Request.Cookies["WardrobeLVL4"];
                var ckoWardrobeLine5 = HttpContext.Request.Cookies["WardrobeLVL5"];
                var ckoWardrobeLine6 = HttpContext.Request.Cookies["WardrobeLVL6"];
                var intWardrobeLine1 = -1;
                var intWardrobeLine2 = -1;
                var intWardrobeLine3 = -1;
                var intWardrobeLine4 = -1;
                var intWardrobeLine5 = -1;
                var intWardrobeLine6 = -1;

                var ckoOutfitLVL1 = HttpContext.Request.Cookies["OutfitLVL1"];
                var ckoOutfitLVL2 = HttpContext.Request.Cookies["OutfitLVL2"];
                var ckoOutfitLVL3 = HttpContext.Request.Cookies["OutfitLVL3"];
                var ckoOutfitLVL4 = HttpContext.Request.Cookies["OutfitLVL4"];
                var ckoOutfitLVL5 = HttpContext.Request.Cookies["OutfitLVL5"];
                var ckoOutfitLVL6 = HttpContext.Request.Cookies["OutfitLVL6"];
                var intOutfitLVL1 = -1;
                var intOutfitLVL2 = -1;
                var intOutfitLVL3 = -1;
                var intOutfitLVL4 = -1;
                var intOutfitLVL5 = -1;
                var intOutfitLVL6 = -1;

                if (ckoWardrobeLine1 != null)
                {
                    int.TryParse(ckoWardrobeLine1.Value, out intWardrobeLine1);
                }
                if (ckoWardrobeLine2 != null)
                {
                    int.TryParse(ckoWardrobeLine2.Value, out intWardrobeLine2);
                }
                if (ckoWardrobeLine3 != null)
                {
                    int.TryParse(ckoWardrobeLine3.Value, out intWardrobeLine3);
                }
                if (ckoWardrobeLine4 != null)
                {
                    int.TryParse(ckoWardrobeLine4.Value, out intWardrobeLine4);
                }
                if (ckoWardrobeLine5 != null)
                {
                    int.TryParse(ckoWardrobeLine5.Value, out intWardrobeLine5);
                }
                if (ckoWardrobeLine6 != null)
                {
                    int.TryParse(ckoWardrobeLine6.Value, out intWardrobeLine6);
                }

                if (ckoOutfitLVL1 != null)
                {
                    int.TryParse(ckoOutfitLVL1.Value, out intOutfitLVL1);
                }
                if (ckoOutfitLVL2 != null)
                {
                    int.TryParse(ckoOutfitLVL2.Value, out intOutfitLVL2);
                }
                if (ckoOutfitLVL3 != null)
                {
                    int.TryParse(ckoOutfitLVL3.Value, out intOutfitLVL3);
                }
                if (ckoOutfitLVL4 != null)
                {
                    int.TryParse(ckoOutfitLVL4.Value, out intOutfitLVL4);
                }
                if (ckoOutfitLVL5 != null)
                {
                    int.TryParse(ckoOutfitLVL5.Value, out intOutfitLVL5);
                }
                if (ckoOutfitLVL6 != null)
                {
                    int.TryParse(ckoOutfitLVL6.Value, out intOutfitLVL6);
                }

                ViewBag.AppUser = "";

                System.Data.Entity.Core.Objects.ObjectResult<spUserType_Result> spUsertype = db.spUserType(OutfitClosetUserID);
                List<spUserType_Result> myUsertype = spUsertype.ToList();
                var SelectUserType = "";

                if (myUsertype.Count > 0)
                {
                    ViewBag.SelectUserTypeID = myUsertype[0].userTypeID;
                    SelectUserType = myUsertype[0].userTypeID.ToString();
                }
                else
                {
                    ViewBag.SelectUserTypeID = "";
                    SelectUserType = "0";
                }

                System.Data.Entity.Core.Objects.ObjectResult<spUserType_Result> spWardrobeUsertype = db.spUserType(WardrobeClosetUserID);
                List<spUserType_Result> myWardrobeUsertype = spWardrobeUsertype.ToList();
                var SelectWardrobeUserType = "";

                if (myWardrobeUsertype.Count > 0)
                {
                    ViewBag.SelectUserTypeID = myWardrobeUsertype[0].userTypeID;
                    SelectWardrobeUserType = myWardrobeUsertype[0].userTypeID.ToString();
                }
                else
                {
                    ViewBag.SelectWardrobeUserType = "";
                    SelectWardrobeUserType = "0";
                }

                //Wardrobe
                List<spGetUsersUsedMenuItems_Result> myWardrobeLine1 = GarmentList(true, 0, "2", userID, WardrobeClosetUserID);
                ViewBag.WardrobeLine1 = myWardrobeLine1;

                ViewBag.WardrobeLine1ck = intWardrobeLine1;
                if ((intWardrobeLine1 != 0) && (intWardrobeLine1 != -1))
                {
                    List<spGetUsersUsedMenuItems_Result> myWardrobeLine2 = GarmentList(true, intWardrobeLine1, SelectWardrobeUserType, userID, WardrobeClosetUserID);
                    ViewBag.WardrobeLine2 = myWardrobeLine2;
                }

                ViewBag.WardrobeLine2ck = intWardrobeLine2;
                if ((intWardrobeLine2 != 0) && (intWardrobeLine2 != -1))
                {
                    List<spGetUsersUsedMenuItems_Result> myWardrobeLine3 = GarmentList(true, intWardrobeLine2, SelectWardrobeUserType, userID, WardrobeClosetUserID);
                    ViewBag.WardrobeLine3 = myWardrobeLine3;
                }

                ViewBag.WardrobeLine3ck = intWardrobeLine3;
                if ((intWardrobeLine3 != 0) && (intWardrobeLine3 != -1))
                {
                    List<spGetUsersUsedMenuItems_Result> myWardrobeLine4 = GarmentList(true, intWardrobeLine3, SelectWardrobeUserType, userID, WardrobeClosetUserID);
                    ViewBag.WardrobeLine4 = myWardrobeLine4;
                }

                ViewBag.WardrobeLine4ck = intWardrobeLine4;
                if ((intWardrobeLine4 != 0) && (intWardrobeLine4 != -1))
                {
                    List<spGetUsersUsedMenuItems_Result> myWardrobeLine5 = GarmentList(true, intWardrobeLine4, SelectWardrobeUserType, userID, WardrobeClosetUserID);
                    ViewBag.WardrobeLine5 = myWardrobeLine5;
                }

                ViewBag.WardrobeLine5ck = intWardrobeLine5;
                if ((intWardrobeLine5 != 0) && (intWardrobeLine5 != -1))
                {
                    List<spGetUsersUsedMenuItems_Result> myWardrobeLine6 = GarmentList(true, intWardrobeLine5, SelectWardrobeUserType, userID, WardrobeClosetUserID);
                    ViewBag.WardrobeLine6 = myWardrobeLine6;
                }

                ViewBag.WardrobeLine6ck = intWardrobeLine6;

                var ckClothingtypeID = HttpContext.Request.Cookies["bxOutfitWardrobeClothingtypeID"];
                var intClothingtypeID = -1;

                if (ckClothingtypeID != null)
                {
                    int.TryParse(ckClothingtypeID.Value, out intClothingtypeID);
                }

                ViewBag.ClothingtypeIDck = intClothingtypeID;
                if (intClothingtypeID != 0)
                {
                    if (WardrobeClosetUserID == "null")
                    {
                        WardrobeClosetUserID = userID;
                    }
                    int flUserTypeID = 0;
                    System.Data.Entity.Core.Objects.ObjectResult<spUserType_Result> spWardrobeUsertypeV = db.spUserType(WardrobeClosetUserID);
                    List<spUserType_Result> myWardrobeUsertypeV = spWardrobeUsertypeV.ToList();

                    if (myWardrobeUsertypeV.Count > 0)
                    {
                        if (myWardrobeUsertypeV[0].userTypeID == 2)
                        {
                            flUserTypeID = myWardrobeUsertypeV[0].userTypeID;
                        }
                        else
                        {
                            flUserTypeID = myWardrobeUsertypeV[0].userTypeID;
                        }
                    }
                    else
                    {
                        flUserTypeID = 0;
                    }

                    System.Data.Entity.Core.Objects.ObjectResult<spGet_FileList_Result> spWardrobeList = db.spGet_FileList(WardrobeClosetUserID, userID, intClothingtypeID, WardrobeClosetUserID, flUserTypeID, true);
                    List<spGet_FileList_Result> myWardrobeList = spWardrobeList.ToList();
                    //myWardrobeList.OrderBy(a => a.CreateDTE);

                    ViewBag.OutfitWardrobeClothingList = myWardrobeList;
                }
                else
                {
                    ViewBag.OutfitWardrobeClothingList = null;
                }

                spUsertype = db.spUserType(OutfitClosetUserID);
                myUsertype = spUsertype.ToList();
                //var SelectUserType = "";

                if (myUsertype.Count > 0)
                {
                    ViewBag.SelectUserTypeID = myUsertype[0].userTypeID;
                    SelectUserType = myUsertype[0].userTypeID.ToString();
                }
                else
                {
                    ViewBag.SelectUserTypeID = "";
                    SelectUserType = "0";
                }

                //Outfit
                List<spGetOutfitUsedMenuItems_Result> myOutfitLine1 = OutfitList(true, 0, userID, OutfitClosetUserID, "2");
                ViewBag.OutfitLine1 = myOutfitLine1;

                ViewBag.OutfitLVL1ck = intOutfitLVL1;
                if ((intOutfitLVL1 != 0) && (intOutfitLVL1 != -1))
                {
                    List<spGetOutfitUsedMenuItems_Result> myOutfitLine2 = OutfitList(true, intOutfitLVL1, userID, OutfitClosetUserID, SelectUserType);
                    ViewBag.OutfitLine2 = myOutfitLine2;
                }
                else
                {
                    ViewBag.OutfitLine2 = null;
                }

                ViewBag.OutfitLVL2ck = intOutfitLVL2;
                if ((intOutfitLVL2 != 0) && (intOutfitLVL2 != -1))
                {
                    List<spGetOutfitUsedMenuItems_Result> myOutfitLine3 = OutfitList(true, intOutfitLVL2, userID, OutfitClosetUserID, SelectUserType);
                    ViewBag.OutfitLine3 = myOutfitLine3;
                }
                else
                {
                    ViewBag.OutfitLine3 = null;
                }

                ViewBag.OutfitLVL3ck = intOutfitLVL3;
                if ((intOutfitLVL3 != 0) && (intOutfitLVL3 != -1))
                {
                    if (SelectUserType != "1")
                    {
                        List<spGetOutfitUsedMenuItems_Result> myOutfitLine4 = OutfitList(true, intOutfitLVL3, userID, OutfitClosetUserID, SelectUserType);
                        ViewBag.OutfitLine4 = myOutfitLine4;
                    }
                    else
                    {
                        ViewBag.OutfitLine4 = null;
                    }
                }
                else
                {
                    ViewBag.OutfitLine4 = null;
                }

                ViewBag.OutfitLVL4ck = intOutfitLVL4;
                if ((intOutfitLVL4 != 0) && (intOutfitLVL4 != -1))
                {
                    if (SelectUserType != "1")
                    {
                        List<spGetOutfitUsedMenuItems_Result> myOutfitLine5 = OutfitList(true, intOutfitLVL4, userID, OutfitClosetUserID, SelectUserType);
                        ViewBag.OutfitLine5 = myOutfitLine5;
                    }
                    else
                    {
                        ViewBag.OutfitLine5 = null;
                    }
                }
                else
                {
                    ViewBag.OutfitLine5 = null;
                }

                ViewBag.OutfitLVL5ck = intOutfitLVL5;
                if ((intOutfitLVL5 != 0) &&  (intOutfitLVL5 != -1)) 
                {
                    if (SelectUserType != "1")
                    {
                        List<spGetOutfitUsedMenuItems_Result> myOutfitLine6 = OutfitList(true, intOutfitLVL5, userID, OutfitClosetUserID, SelectUserType);
                        ViewBag.OutfitLine6 = myOutfitLine6;
                    }
                    else
                    {
                        ViewBag.OutfitLine6 = null;
                    }
                }
                else
                {
                    ViewBag.OutfitLine6 = null;
                }

                ViewBag.OutfitLVL6ck = intOutfitLVL6;

                //if (userID == uClosetUserID)
                //{
                    //ViewBag.ShareOutfitID = "";
                //}
                //else
                //{
                    string ShareOutfitID = ViewBag.OutfitID;
                    ViewBag.ShareOutfitID = ShareOutfitID;
                //}

                ViewBag.OutfitLBL = OutfitLBL;

                if (OutfitID != "")
                {
                    OutfitID = OutfitLoadID;
                }

                //////////////////////////////////
                // Outfit Share - Begin
                //////////////////////////////////
                if (OutfitID != "") 
                {
                    System.Data.Entity.Core.Objects.ObjectResult<spGetShareOutfitFriends_Result> spShareOutfitFriends = db.spGetShareOutfitFriends(OutfitID);
                    List<spGetShareOutfitFriends_Result> myShareOutfitFriends = spShareOutfitFriends.ToList();
                    ViewBag.ShareOutfitFriends = myShareOutfitFriends;
                }
                else
                {
                    ViewBag.ShareOutfitFriends = null;
                }

                System.Data.Entity.Core.Objects.ObjectResult<spGetFBUserOutfitList_Result> spGetFBUserOutfitList = db.spGetFBUserOutfitList(userID);
                List<spGetFBUserOutfitList_Result> myGetFBUserOutfitList = spGetFBUserOutfitList.ToList();

                ViewBag.ShareOutfitUsers = myGetFBUserOutfitList;
                ViewBag.ShareOutfitFG = true;
                
                //////////////////////////////////
                // Outfit Share - End
                //////////////////////////////////


                return View(user);
            }
            else
            {
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult AddOutfitCategory(string inpOutfitCustomCategoryID, string inpOutfitCustomDesc)
        {
            string userID = "";
            string FBuserID = "";
            int clothingTypeID = 0;
            string CustomCategory = "";

            CustomCategory = inpOutfitCustomDesc;

            var ckClothingtypeID = HttpContext.Request.Cookies["bxOutFitCategoryID"];
            if (ckClothingtypeID != null)
            {
                int.TryParse(ckClothingtypeID.Value, out clothingTypeID);
            }

            userID = HttpContext.Request.Cookies["UserID"].Value.ToString();
            FBuserID = HttpContext.Request.Cookies["FBUserID"].Value.ToString();

            System.Data.Entity.Core.Objects.ObjectResult<spAdd_CustomOutfitCategories_Result> spCustomOutfitCategory = db.spAdd_CustomOutfitCategories(true, CustomCategory, clothingTypeID, userID);

            return RedirectToAction("Index");
        }

        private List<spGetUsersUsedMenuItems_Result> GarmentList(Boolean glShowFG, int glParentID, string glVendorFLG, string glUserID, string glClosetUserID)
        {
            List<spGetUsersUsedMenuItems_Result> myWardrobe;

            System.Data.Entity.Core.Objects.ObjectResult<spGetUsersUsedMenuItems_Result> spWardrobe = db.spGetUsersUsedMenuItems(glShowFG, glParentID, glUserID, glClosetUserID, glUserID, glVendorFLG);
            myWardrobe = spWardrobe.ToList();
            myWardrobe.OrderBy(a => a.ClothingRank);

            return myWardrobe;
        }

        private List<spGetOutfitUsedMenuItems_Result> OutfitList(Boolean cpcShowFG, int cpcParentID, string cpcUserID, string cpcOutfitUserID, string cpcVendorFLG)
        {
            System.Data.Entity.Core.Objects.ObjectResult<spGetOutfitUsedMenuItems_Result> spOutfit = db.spGetOutfitUsedMenuItems(cpcShowFG, cpcParentID, cpcUserID, cpcOutfitUserID, cpcUserID, cpcVendorFLG);
            List<spGetOutfitUsedMenuItems_Result> myOutfit = spOutfit.ToList();
            myOutfit.OrderBy(a => a.OutfitGroupRank);

            return myOutfit;
        }

        private string SaveImage(string siSiteUserID, int siCategoryID, string siOutfitDesc, string siOutfitID, int siGenderID, string siMannequinTop, string siMannequinLegs, string siMannequinFeet, string siMannequinHand, string siMannequinFoot, Boolean SessionOutfit)
        {
            System.Data.Entity.Core.Objects.ObjectResult<spAdd_UserOutfit_OBJ_Result> spOutfit = db.spAdd_UserOutfit_OBJ(siSiteUserID, siCategoryID, "", 0, siOutfitDesc, SessionOutfit, "UclosetWWW", siOutfitID);
            List<spAdd_UserOutfit_OBJ_Result> myOutfit = spOutfit.ToList();

            string OutfitID = "";

            if (myOutfit.Count > 0)
            {

                OutfitID = myOutfit[0].uoUserOutfitID.ToString();

                ViewBag.OutfitID = OutfitID;
            }
            else
            {
                ViewBag.OutfitID = "";

            }

            int FrameID = 0;

            if (siMannequinTop != "")
            {
                if (siGenderID == 1)
                {
                    FrameID = 1;
                }
                else
                {
                    FrameID = 6;
                }
                spOutfit = db.spAdd_UserOutfit_OBJ(siSiteUserID, siCategoryID, siMannequinTop, FrameID, siOutfitDesc, SessionOutfit, "UclosetWWW", OutfitID);
            }
            if (siMannequinLegs != "")
            {
                if (siGenderID == 1)
                {
                    FrameID = 2;
                }
                else
                {
                    FrameID = 7;
                }
                spOutfit = db.spAdd_UserOutfit_OBJ(siSiteUserID, siCategoryID, siMannequinLegs, FrameID, siOutfitDesc, SessionOutfit, "UclosetWWW", OutfitID);
            }
            if (siMannequinFeet != "")
            {
                if (siGenderID == 1)
                {
                    FrameID = 3;
                }
                else
                {
                    FrameID = 8;
                }
                spOutfit = db.spAdd_UserOutfit_OBJ(siSiteUserID, siCategoryID, siMannequinFeet, FrameID, siOutfitDesc, SessionOutfit, "UclosetWWW", OutfitID);
            }
            if (siMannequinHand != "")
            {
                if (siGenderID == 1)
                {
                    FrameID = 4;
                }
                else
                {
                    FrameID = 9;
                }
                spOutfit = db.spAdd_UserOutfit_OBJ(siSiteUserID, siCategoryID, siMannequinHand, FrameID, siOutfitDesc, SessionOutfit, "UclosetWWW", OutfitID);
            }
            if (siMannequinFoot != "")
            {
                if (siGenderID == 1)
                {
                    FrameID = 5;
                }
                else
                {
                    FrameID = 10;
                }
                spOutfit = db.spAdd_UserOutfit_OBJ(siSiteUserID, siCategoryID, siMannequinFoot, FrameID, siOutfitDesc, SessionOutfit, "UclosetWWW", OutfitID);
            }

            return OutfitID;
        }

        //public ActionResult ShowImage(string siFileID)
        //{
            //System.Data.Entity.Core.Objects.ObjectResult<spGet_File_Result> spResult = db.spGet_File(siFileID);
            //List<spGet_File_Result> myResults = spResult.ToList();
            //return File(myResults[0].attachment, myResults[0].attachDocFiletype);
        //}

        [HttpPost]
        public ActionResult UpdateFriends(IEnumerable<string> ShareFriends, string ShareOutfitID)
        {
            System.Data.Entity.Core.Objects.ObjectResult<spClearShareOutfitFriends_Result> spResult = db.spClearShareOutfitFriends(ShareOutfitID);

            foreach (var FriendID in ShareFriends)
            {
                if (FriendID != "false")
                {
                    System.Data.Entity.Core.Objects.ObjectResult<spAddShareOutfitFriends_Result> spResultFriends = db.spAddShareOutfitFriends(ShareOutfitID, FriendID);
                }
            }

            return RedirectToAction("Index");
        }

        //

        [HttpPost]
        public ActionResult FacebookPOST(string ShareOutfitIDTools, string ShareOutfitGender)
        {
            string Frame1 = "";
            string Frame2 = "";
            string Frame3 = "";
            string Frame4 = "";
            string Frame5 = "";
            string Frame6 = "";
            string Frame7 = "";
            string Frame8 = "";
            string Frame9 = "";
            string Frame10 = "";
            string ImageName = Guid.NewGuid() + ShareOutfitIDTools + ".png";
            string NewImageName  = "~/_UserFacebook/" + ImageName;
            string ImageBanner = "";
            int newX = 0;
            int newY = 0;

            NewImageName = NewImageName.Replace(" ", "");

            //var user = await context.Client.GetCurrentUserAsync<MyAppUser>();

            if (ShareOutfitGender == "1")
            {
                Frame1 = "";
                Frame2 = "";
                Frame3 = "";
                Frame4 = "";
                Frame5 = "";
                Frame6 = "X";
                Frame7 = "X";
                Frame8 = "X";
                Frame9 = "X";
                Frame10 = "X";
            }
            else
            {
                Frame1 = "X";
                Frame2 = "X";
                Frame3 = "X";
                Frame4 = "X";
                Frame5 = "X";
                Frame6 = "";
                Frame7 = "";
                Frame8 = "";
                Frame9 = "";
                Frame10 = "";
            }


            if (ShareOutfitIDTools.Trim() != "")
            {
                var ckUserID = HttpContext.Request.Cookies["UserID"];
                string userID = "";

                if (ckUserID == null)
                {
                    userID = "";
                }
                else
                {
                    userID = ckUserID.Value.ToString();
                }

                System.Data.Entity.Core.Objects.ObjectResult<spView_UserOutfit_Result> spOutfitParts = db.spView_UserOutfit(userID, ShareOutfitIDTools, userID);
                List<spView_UserOutfit_Result> myOutfitParts = spOutfitParts.ToList();

                String path = Server.MapPath("~/Images/imgCanvasLarge.png");
                System.Drawing.Image imgFinal = System.Drawing.Image.FromFile(path);
                Graphics myFinalImage = Graphics.FromImage(imgFinal);

                string Path = "~/_UserGarments/";
                string ImagePath = "";

                if (myOutfitParts.Count > 0)
                {
                    for (int curOutfitPart = 0; curOutfitPart < myOutfitParts.Count; curOutfitPart++)
                    {
                        ImagePath = Path + myOutfitParts[curOutfitPart].Filename;
                        ImagePath = Server.MapPath(ImagePath);

                        var imageFile = ImagePath;

                        try
                        {
                            ImageBanner = "~/Images/Header_uCloset.gif";
                            ImageBanner = Server.MapPath(ImageBanner);

                            //Add Banner
                            using (var srcImage = Image.FromFile(ImageBanner))
                            using (var newImage = new Bitmap(230, 175))
                            using (var graphics = Graphics.FromImage(newImage))
                            using (var stream = new MemoryStream())
                            {
                                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                                graphics.DrawImage(srcImage, new Rectangle(0, 0, 225, 24));

                                myFinalImage.DrawImage(newImage, 75, 15);
                                imgFinal.Save(Server.MapPath(NewImageName));
                            }

                            if ((myOutfitParts[curOutfitPart].uopFrameID == 1) || (myOutfitParts[curOutfitPart].uopFrameID == 2) || (myOutfitParts[curOutfitPart].uopFrameID == 3) || (myOutfitParts[curOutfitPart].uopFrameID == 6) || (myOutfitParts[curOutfitPart].uopFrameID == 7) || (myOutfitParts[curOutfitPart].uopFrameID == 8))
                            {
                                newX = 230;
                                newY = 175;
                            }
                            else
                            {
                                newX = 100;
                                newY = 150;
                            }

                            using (var srcImage = Image.FromFile(imageFile))
                            using (var newImage = new Bitmap(newX, newY))
                            using (var graphics = Graphics.FromImage(newImage))
                            using (var stream = new MemoryStream())
                            {
                                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                                graphics.DrawImage(srcImage, new Rectangle(0, 0, newX, newY));

                                if (myOutfitParts[curOutfitPart].uopFrameID == 1)
                                {
                                    myFinalImage.DrawImage(newImage, 25, 50);
                                    imgFinal.Save(Server.MapPath(NewImageName));
                                    Frame1 = ImagePath;
                                    Frame6 = ImagePath;
                                    Pen blackPen = new Pen(Color.Black, 2);
                                    myFinalImage.DrawRectangle(blackPen, 25, 50, 173, 133);
                                    imgFinal.Save(Server.MapPath(NewImageName));
                                }
                                else if (myOutfitParts[curOutfitPart].uopFrameID == 2)
                                {
                                    myFinalImage.DrawImage(newImage, 25, 190);
                                    Frame2 = ImagePath;
                                    Frame7 = ImagePath;
                                    imgFinal.Save(Server.MapPath(NewImageName));

                                    Pen blackPen = new Pen(Color.Black, 2);
                                    myFinalImage.DrawRectangle(blackPen, 25, 190, 173, 133);
                                    imgFinal.Save(Server.MapPath(NewImageName));
                                }
                                else if (myOutfitParts[curOutfitPart].uopFrameID == 3)
                                {
                                    myFinalImage.DrawImage(newImage, 25, 330);
                                    Frame3 = ImagePath;
                                    Frame8 = ImagePath;
                                    imgFinal.Save(Server.MapPath(NewImageName));

                                    Pen blackPen = new Pen(Color.Black, 2);
                                    myFinalImage.DrawRectangle(blackPen, 25, 330, 173, 132);
                                    imgFinal.Save(Server.MapPath(NewImageName));
                                }
                                else if (myOutfitParts[curOutfitPart].uopFrameID == 4)
                                {
                                    myFinalImage.DrawImage(newImage, 225, 50);
                                    Frame4 = ImagePath;
                                    Frame9 = ImagePath;
                                    imgFinal.Save(Server.MapPath(NewImageName));

                                    Pen blackPen = new Pen(Color.Black, 2);
                                    myFinalImage.DrawRectangle(blackPen, 225, 50, 75, 112);
                                    imgFinal.Save(Server.MapPath(NewImageName));
                                }
                                else if (myOutfitParts[curOutfitPart].uopFrameID == 5)
                                {
                                    myFinalImage.DrawImage(newImage, 225, 175);
                                    Frame5 = ImagePath;
                                    Frame10 = ImagePath;
                                    imgFinal.Save(Server.MapPath(NewImageName));

                                    Pen blackPen = new Pen(Color.Black, 2);
                                    myFinalImage.DrawRectangle(blackPen, 225, 175, 75, 112);
                                    imgFinal.Save(Server.MapPath(NewImageName));
                                }
                                else if (myOutfitParts[curOutfitPart].uopFrameID == 6)
                                {
                                    myFinalImage.DrawImage(newImage, 25, 50);
                                    Frame6 = ImagePath;
                                    Frame1 = ImagePath;
                                    imgFinal.Save(Server.MapPath(NewImageName));

                                    Pen blackPen = new Pen(Color.Black, 2);
                                    myFinalImage.DrawRectangle(blackPen, 25, 50, 173, 133);
                                    imgFinal.Save(Server.MapPath(NewImageName));
                                }
                                else if (myOutfitParts[curOutfitPart].uopFrameID == 7)
                                {
                                    myFinalImage.DrawImage(newImage, 25, 190);
                                    Frame7 = ImagePath;
                                    Frame2 = ImagePath;
                                    imgFinal.Save(Server.MapPath(NewImageName));

                                    Pen blackPen = new Pen(Color.Black, 2);
                                    myFinalImage.DrawRectangle(blackPen, 25, 190, 173, 133);
                                    imgFinal.Save(Server.MapPath(NewImageName));
                                }
                                else if (myOutfitParts[curOutfitPart].uopFrameID == 8)
                                {
                                    myFinalImage.DrawImage(newImage, 25, 330);
                                    Frame8 = ImagePath;
                                    Frame3 = ImagePath;
                                    imgFinal.Save(Server.MapPath(NewImageName));

                                    Pen blackPen = new Pen(Color.Black, 2);
                                    myFinalImage.DrawRectangle(blackPen, 25, 330, 173, 132);
                                    imgFinal.Save(Server.MapPath(NewImageName));
                                }
                                else if (myOutfitParts[curOutfitPart].uopFrameID == 9)
                                {
                                    myFinalImage.DrawImage(newImage, 225, 50);
                                    Frame9 = ImagePath;
                                    Frame4 = ImagePath;
                                    imgFinal.Save(Server.MapPath(NewImageName));

                                    Pen blackPen = new Pen(Color.Black, 2);
                                    myFinalImage.DrawRectangle(blackPen, 225, 50, 75, 112);
                                    imgFinal.Save(Server.MapPath(NewImageName));
                                }
                                else if (myOutfitParts[curOutfitPart].uopFrameID == 10)
                                {
                                    myFinalImage.DrawImage(newImage, 225, 175);
                                    Frame10 = ImagePath;
                                    Frame5 = ImagePath;
                                    imgFinal.Save(Server.MapPath(NewImageName));

                                    Pen blackPen = new Pen(Color.Black, 2);
                                    myFinalImage.DrawRectangle(blackPen, 225, 175, 75, 112);
                                    imgFinal.Save(Server.MapPath(NewImageName));
                                }
                            }
                        }
                        catch (Exception e)
                        {

                        }
                    }

                    imgFinal.Save(Server.MapPath(NewImageName));

                    if (Frame1 == "")
                    {
                        string imageFile = "~/Images/Mannequin_Woman_Top.jpg";
                        imageFile = Server.MapPath(imageFile);

                        using (var srcImage = Image.FromFile(imageFile))
                        using (var newImage = new Bitmap(230, 175))
                        using (var graphics = Graphics.FromImage(newImage))
                        using (var stream = new MemoryStream())
                        {
                            graphics.SmoothingMode = SmoothingMode.AntiAlias;
                            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                            graphics.DrawImage(srcImage, new Rectangle(0, 0, 230, 175));

                            myFinalImage.DrawImage(newImage, 25, 50);
                            imgFinal.Save(Server.MapPath(NewImageName));

                            Pen blackPen = new Pen(Color.Black, 2);
                            myFinalImage.DrawRectangle(blackPen, 25, 50, 173, 133);
                            imgFinal.Save(Server.MapPath(NewImageName));
                        }

                        Frame6 = "x";
                        Frame7 = "x";
                        Frame8 = "x";
                        Frame9 = "x";
                        Frame10 = "x";
                    }

                    if (Frame2 == "")
                    {
                        string imageFile = "~/Images/Mannequin_Woman_Lower.jpg";
                        imageFile = Server.MapPath(imageFile);

                        using (var srcImage = Image.FromFile(imageFile))
                        using (var newImage = new Bitmap(230, 125))
                        using (var graphics = Graphics.FromImage(newImage))
                        using (var stream = new MemoryStream())
                        {
                            graphics.SmoothingMode = SmoothingMode.AntiAlias;
                            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                            graphics.DrawImage(srcImage, new Rectangle(0, 0, 230, 125));

                            myFinalImage.DrawImage(newImage, 25, 190);
                            imgFinal.Save(Server.MapPath(NewImageName));

                            Pen blackPen = new Pen(Color.Black, 2);
                            myFinalImage.DrawRectangle(blackPen, 25, 190, 173, 95);
                            imgFinal.Save(Server.MapPath(NewImageName));
                        }

                        Frame6 = "x";
                        Frame7 = "x";
                        Frame8 = "x";
                        Frame9 = "x";
                        Frame10 = "x";
                    }

                    if (Frame3 == "")
                    {
                        string imageFile = "~/Images/Mannequin_Woman_Feet.jpg";
                        imageFile = Server.MapPath(imageFile);

                        using (var srcImage = Image.FromFile(imageFile))
                        using (var newImage = new Bitmap(230, 50))
                        using (var graphics = Graphics.FromImage(newImage))
                        using (var stream = new MemoryStream())
                        {
                            graphics.SmoothingMode = SmoothingMode.AntiAlias;
                            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                            graphics.DrawImage(srcImage, new Rectangle(0, 0, 230, 50));

                            myFinalImage.DrawImage(newImage, 25, 330);
                            imgFinal.Save(Server.MapPath(NewImageName));

                            Pen blackPen = new Pen(Color.Black, 2);
                            myFinalImage.DrawRectangle(blackPen, 25, 330, 173, 50);
                            imgFinal.Save(Server.MapPath(NewImageName));
                        }

                        Frame6 = "x";
                        Frame7 = "x";
                        Frame8 = "x";
                        Frame9 = "x";
                        Frame10 = "x";
                    }

                    if (Frame4 == "")
                    {
                        string imageFile = "~/Images/Mannequin_Woman_Right_Hand.jpg";
                        imageFile = Server.MapPath(imageFile);

                        using (var srcImage = Image.FromFile(imageFile))
                        using (var newImage = new Bitmap(100, 150))
                        using (var graphics = Graphics.FromImage(newImage))
                        using (var stream = new MemoryStream())
                        {
                            graphics.SmoothingMode = SmoothingMode.AntiAlias;
                            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                            graphics.DrawImage(srcImage, new Rectangle(0, 0, 100, 150));

                            myFinalImage.DrawImage(newImage, 225, 50);
                            imgFinal.Save(Server.MapPath(NewImageName));

                            Pen blackPen = new Pen(Color.Black, 2);
                            myFinalImage.DrawRectangle(blackPen, 225, 50, 75, 112);
                            imgFinal.Save(Server.MapPath(NewImageName));
                        }

                        Frame6 = "x";
                        Frame7 = "x";
                        Frame8 = "x";
                        Frame9 = "x";
                        Frame10 = "x";
                    }

                    if (Frame5 == "")
                    {
                        string imageFile = "~/Images/Mannequin_Woman_Feet_trim.jpg";
                        imageFile = Server.MapPath(imageFile);

                        using (var srcImage = Image.FromFile(imageFile))
                        using (var newImage = new Bitmap(100, 150))
                        using (var graphics = Graphics.FromImage(newImage))
                        using (var stream = new MemoryStream())
                        {
                            graphics.SmoothingMode = SmoothingMode.AntiAlias;
                            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                            graphics.DrawImage(srcImage, new Rectangle(0, 0, 100, 150));

                            myFinalImage.DrawImage(newImage, 225, 175);
                            imgFinal.Save(Server.MapPath(NewImageName));

                            Pen blackPen = new Pen(Color.Black, 2);
                            myFinalImage.DrawRectangle(blackPen, 225, 175, 75, 112);
                            imgFinal.Save(Server.MapPath(NewImageName));
                        }

                        Frame6 = "x";
                        Frame7 = "x";
                        Frame8 = "x";
                        Frame9 = "x";
                        Frame10 = "x";
                    }

                    if (Frame6 == "")
                    {
                        string imageFile = "~/Images/Mannequin_Man_Top.jpg";
                        imageFile = Server.MapPath(imageFile);

                        using (var srcImage = Image.FromFile(imageFile))
                        using (var newImage = new Bitmap(230, 175))
                        using (var graphics = Graphics.FromImage(newImage))
                        using (var stream = new MemoryStream())
                        {
                            graphics.SmoothingMode = SmoothingMode.AntiAlias;
                            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                            graphics.DrawImage(srcImage, new Rectangle(0, 0, 230, 175));

                            myFinalImage.DrawImage(newImage, 25, 50);
                            imgFinal.Save(Server.MapPath(NewImageName));

                            Pen blackPen = new Pen(Color.Black, 2);
                            myFinalImage.DrawRectangle(blackPen, 25, 50, 173, 133);
                            imgFinal.Save(Server.MapPath(NewImageName));
                        }

                        Frame1 = "x";
                        Frame2 = "x";
                        Frame3 = "x";
                        Frame4 = "x";
                        Frame5 = "x";
                    }

                    if (Frame7 == "")
                    {
                        string imageFile = "~/Images/Mannequin_Man_Bottom_Legs.jpg";
                        imageFile = Server.MapPath(imageFile);

                        using (var srcImage = Image.FromFile(imageFile))
                        using (var newImage = new Bitmap(230, 175))
                        using (var graphics = Graphics.FromImage(newImage))
                        using (var stream = new MemoryStream())
                        {
                            graphics.SmoothingMode = SmoothingMode.AntiAlias;
                            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                            graphics.DrawImage(srcImage, new Rectangle(0, 0, 230, 175));

                            myFinalImage.DrawImage(newImage, 25, 190);
                            imgFinal.Save(Server.MapPath(NewImageName));

                            Pen blackPen = new Pen(Color.Black, 2);
                            myFinalImage.DrawRectangle(blackPen, 25, 190, 173, 133);
                            imgFinal.Save(Server.MapPath(NewImageName));
                        }

                        Frame1 = "x";
                        Frame2 = "x";
                        Frame3 = "x";
                        Frame4 = "x";
                        Frame5 = "x";
                    }

                    if (Frame8 == "")
                    {
                        string imageFile = "~/Images/Mannequin_Man_Full_Bottom_Feet.jpg";
                        imageFile = Server.MapPath(imageFile);

                        using (var srcImage = Image.FromFile(imageFile))
                        using (var newImage = new Bitmap(230, 50))
                        using (var graphics = Graphics.FromImage(newImage))
                        using (var stream = new MemoryStream())
                        {
                            graphics.SmoothingMode = SmoothingMode.AntiAlias;
                            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                            graphics.DrawImage(srcImage, new Rectangle(0, 0, 230, 50));

                            myFinalImage.DrawImage(newImage, 25, 330);
                            imgFinal.Save(Server.MapPath(NewImageName));

                            Pen blackPen = new Pen(Color.Black, 2);
                            myFinalImage.DrawRectangle(blackPen, 25, 330, 173, 50);
                            imgFinal.Save(Server.MapPath(NewImageName));
                        }

                        Frame1 = "x";
                        Frame2 = "x";
                        Frame3 = "x";
                        Frame4 = "x";
                        Frame5 = "x";
                    }

                    if (Frame9 == "")
                    {
                        string imageFile = "~/Images/Mannequin_Man_Arm_Hand_Trim.jpg";
                        imageFile = Server.MapPath(imageFile);

                        using (var srcImage = Image.FromFile(imageFile))
                        using (var newImage = new Bitmap(100, 150))
                        using (var graphics = Graphics.FromImage(newImage))
                        using (var stream = new MemoryStream())
                        {
                            graphics.SmoothingMode = SmoothingMode.AntiAlias;
                            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                            graphics.DrawImage(srcImage, new Rectangle(0, 0, 100, 150));

                            myFinalImage.DrawImage(newImage, 225, 50);
                            imgFinal.Save(Server.MapPath(NewImageName));

                            Pen blackPen = new Pen(Color.Black, 2);
                            myFinalImage.DrawRectangle(blackPen, 225, 50, 75, 112);
                            imgFinal.Save(Server.MapPath(NewImageName));
                        }

                        Frame1 = "x";
                        Frame2 = "x";
                        Frame3 = "x";
                        Frame4 = "x";
                        Frame5 = "x";
                    }

                    if (Frame10 == "")
                    {
                        string imageFile = "~/Images/Mannequin_Man_Full_Bottom_Foot.jpg";
                        imageFile = Server.MapPath(imageFile);

                        using (var srcImage = Image.FromFile(imageFile))
                        using (var newImage = new Bitmap(100, 150))
                        using (var graphics = Graphics.FromImage(newImage))
                        using (var stream = new MemoryStream())
                        {
                            graphics.SmoothingMode = SmoothingMode.AntiAlias;
                            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                            graphics.DrawImage(srcImage, new Rectangle(0, 0, 100, 150));

                            myFinalImage.DrawImage(newImage, 225, 175);
                            imgFinal.Save(Server.MapPath(NewImageName));

                            Pen blackPen = new Pen(Color.Black, 2);
                            myFinalImage.DrawRectangle(blackPen, 225, 175, 75, 112);
                            imgFinal.Save(Server.MapPath(NewImageName));
                        }

                        Frame1 = "x";
                        Frame2 = "x";
                        Frame3 = "x";
                        Frame4 = "x";
                        Frame5 = "x";
                    }

                    path = Server.MapPath("~/Images/imgCanvas.png");
                    System.Drawing.Image imgFinalSmall = System.Drawing.Image.FromFile(path);
                    Graphics myFinalSmallImage = Graphics.FromImage(imgFinalSmall);

                    string imageSmallFile = "~/Images/imgCanvas.png";
                    imageSmallFile = Server.MapPath(imageSmallFile);

                    string myFileNew = "";
                    myFileNew = "~/_UserFacebook/TEstPic2.png";

                    using (var srcImage = Image.FromFile(Server.MapPath(NewImageName)))
                    using (var newImage = new Bitmap(749, 395))
                    using (var graphics = Graphics.FromImage(newImage))
                    using (var stream = new MemoryStream())
                    {
                        graphics.SmoothingMode = SmoothingMode.AntiAlias;
                        graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                        graphics.DrawImage(srcImage, new Rectangle(0, 0, 749, 395));

                        myFinalSmallImage.DrawImage(newImage, 0, 0);
                        
                    }

                    imgFinalSmall.Save(Server.MapPath(NewImageName));

                    System.Data.Entity.Core.Objects.ObjectResult<spAdd_OutfitImage_Result> spOutfitImage = db.spAdd_OutfitImage(userID, ShareOutfitIDTools, NewImageName);

                    //Post To Facebook
                    //FBUserID
                    var ckFBUserID = HttpContext.Request.Cookies["FBUserID"];
                    string FBUserID = "";
                    if (ckFBUserID == null)
                    {
                        FBUserID = "";
                    }
                    else
                    {
                        FBUserID = ckFBUserID.Value.ToString();
                    }

                    //UserToken
                    var ckUserToken = HttpContext.Request.Cookies["UserToken"];
                    string acccessToken = "";
                    if (ckUserToken == null)
                    {
                        acccessToken = "";
                    }
                    else
                    {
                        acccessToken = ckUserToken.Value.ToString();
                    }

                    string bodymessage = "Outfit:" + System.Guid.NewGuid();
                    var client = new FacebookClient(acccessToken);
                    client.Post("/me/feed", new { message = bodymessage });

                    dynamic messagePost = new ExpandoObject();
                    messagePost.picture = "http://www.ucloset.net/_UserFacebook/" + ImageName;
                    messagePost.link = "http://www.ucloset.net/";
                    //messagePost.name = "[name] Facebook name...";
                    messagePost.name = "";

                    // "{*actor*} " + "posted news..."; //<---{*actor*} is the user (i.e.: Alex)
                    //messagePost.caption = " Facebook caption";
                    //messagePost.description = "[description] Facebook description...";
                    //messagePost.message = "[message] Facebook message...";
                    messagePost.caption = "";
                    messagePost.description = "";
                    messagePost.message = "";

                    //string acccessToken = context.AccessToken;
                    FacebookClient appp = new FacebookClient(acccessToken);
                    try
                    {
                        var postId = appp.Post(FBUserID + "/feed", messagePost);
                    }
                    catch (FacebookOAuthException ex)
                    {
                        //handle oauth exception
                    }
                    catch (FacebookApiException ex)
                    {
                        //handle facebook exception
                    }
                }
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult DeleteOutfit(string OutfitID)
        {
            var UserID = HttpContext.Request.Cookies["UserID"].Value.ToString();

            if (OutfitID.Trim() == "")
            {
                OutfitID = HttpContext.Request.Cookies["ckOutfitLoadID"].Value.ToString();
            }

            System.Data.Entity.Core.Objects.ObjectResult<spDeleteHideOufit_Result> spResultFriends = db.spDeleteHideOufit(UserID, OutfitID);

            ViewBag.OutfitID = "";
            ViewBag.OutfitDesc = "";
            ViewBag.LoadOutfitParts = null;

            HttpCookie hc = new HttpCookie("ckMannequinTop", "");
            Response.Cookies.Add(hc);
            hc = new HttpCookie("ckMannequinLegs", "");
            Response.Cookies.Add(hc);
            hc = new HttpCookie("ckMannequinFeet", "");
            Response.Cookies.Add(hc);
            hc = new HttpCookie("ckMannequinHand", "");
            Response.Cookies.Add(hc);
            hc = new HttpCookie("ckMannequinFoot", "");
            Response.Cookies.Add(hc);

            hc = new HttpCookie("ckOutfitLBL", "");
            Response.Cookies.Add(hc);
            hc = new HttpCookie("ckOutfitID", "");
            Response.Cookies.Add(hc);
            hc = new HttpCookie("ckOutfitLoadID", "");
            Response.Cookies.Add(hc);

            return RedirectToAction("Index");
        }
	}
}