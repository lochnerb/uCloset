using fbuCloset.Models;
using Microsoft.AspNet.Mvc.Facebook;
using Microsoft.AspNet.Mvc.Facebook.Client;
using Microsoft.AspNet.Mvc.Facebook.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace fbuCloset.Controllers
{
    public class WardrobeController : Controller
    {
        private uClosetSQLAdminEntities db = new uClosetSQLAdminEntities();

        // GET: /Wardrobe/
        [FacebookAuthorize("email", "user_photos")]
        public async Task<ActionResult> Index(FacebookContext context)
        {
            if (ModelState.IsValid)
            {
                var user = await context.Client.GetCurrentUserAsync<MyAppUser>();

                //ViewBag.AppUser = Request.Cookies["UserName"].ToString();
                ViewBag.AppUser = HttpContext.Request.Cookies["UserName"];

                var ckuserID = System.Web.HttpContext.Current.Request.Cookies["UserID"];
                var ckFBuserID = HttpContext.Request.Cookies["FBUserID"];

                string userID = "";
                string FBUserID = "";

                var ckuClosetUserID  = HttpContext.Request.Cookies["uClosetUserID"];

                if (ckuserID == null)
                {
                    userID = "";
                }
                else
                {
                    userID = ckuserID.Value.ToString();
                }

                if (ckFBuserID == null)
                {
                    FBUserID = "";
                }
                else
                {
                    FBUserID = ckFBuserID.ToString();
                }


                System.Data.Entity.Core.Objects.ObjectResult<spUserType_Result> spUsertype = db.spUserType(userID);
                List<spUserType_Result> myUsertype = spUsertype.ToList();

                if (myUsertype.Count > 0)
                {
                    if (myUsertype[0].userTypeID == 2)
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

                System.Data.Entity.Core.Objects.ObjectResult<spGetFBUserList_Result> spShareUsers = db.spGetFBUserList(userID);
                List<spGetFBUserList_Result> myShareUsers = spShareUsers.ToList();
                ViewBag.ShareUsers = myShareUsers;


                string WardrobeClosetUserID = "";
                var WardrobeClosetUserIDck = HttpContext.Request.Cookies["ckWardrobeLVL0"];

                if (WardrobeClosetUserIDck == null)
                {
                    WardrobeClosetUserID = userID;
                    ViewBag.WardrobeUserID = WardrobeClosetUserID;
                    ViewBag.VendorFLG = "0";
                }
                else
                {
                    WardrobeClosetUserID = WardrobeClosetUserIDck.Value.ToString();

                    if (WardrobeClosetUserID == "null")
                    {
                        WardrobeClosetUserID = userID;
                        ViewBag.WardrobeUserID = WardrobeClosetUserID;
                        ViewBag.VendorFLG = "0";
                    }
                    else
                    {
                        ViewBag.WardrobeUserID = WardrobeClosetUserID;
                    }
                }

                spUsertype = db.spUserType(WardrobeClosetUserID);
                myUsertype = spUsertype.ToList();
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

                var ckwWardrobeLine1 = HttpContext.Request.Cookies["ckWardrobeLVL1"];
                var ckwWardrobeLine2 = HttpContext.Request.Cookies["ckWardrobeLVL2"];
                var ckwWardrobeLine3 = HttpContext.Request.Cookies["ckWardrobeLVL3"];
                var ckwWardrobeLine4 = HttpContext.Request.Cookies["ckWardrobeLVL4"];
                var ckwWardrobeLine5 = HttpContext.Request.Cookies["ckWardrobeLVL5"];
                var ckwWardrobeLine6 = HttpContext.Request.Cookies["ckWardrobeLVL6"];
                var intWardrobeLine1 = -1;
                var intWardrobeLine2 = -1;
                var intWardrobeLine3 = -1;
                var intWardrobeLine4 = -1;
                var intWardrobeLine5 = -1;
                var intWardrobeLine6 = -1;

                if (ckwWardrobeLine1 != null)
                {
                    int.TryParse(ckwWardrobeLine1.Value, out intWardrobeLine1);
                }
                if (ckwWardrobeLine2 != null)
                {
                    int.TryParse(ckwWardrobeLine2.Value, out intWardrobeLine2);
                }
                if (ckwWardrobeLine3 != null)
                {
                    int.TryParse(ckwWardrobeLine3.Value, out intWardrobeLine3);
                }
                if (ckwWardrobeLine4 != null)
                {
                    int.TryParse(ckwWardrobeLine4.Value, out intWardrobeLine4);
                }
                if (ckwWardrobeLine5 != null)
                {
                    int.TryParse(ckwWardrobeLine5.Value, out intWardrobeLine5);
                }
                if (ckwWardrobeLine6 != null)
                {
                    int.TryParse(ckwWardrobeLine6.Value, out intWardrobeLine6);
                }

                //System.Data.Entity.Core.Objects.ObjectResult<spView_ClothingParentCategories_Result> spWardrobeLine1 = db.spView_ClothingParentCategories(true, 0, WardrobeClosetUserID, WardrobeClosetUserID, "0");
                System.Data.Entity.Core.Objects.ObjectResult<spGetUsersUsedMenuItems_Result> spWardrobeLine1 = db.spGetUsersUsedMenuItems(true, 0, userID, WardrobeClosetUserID, WardrobeClosetUserID, "0");
                List<spGetUsersUsedMenuItems_Result> myWardrobeLine1 = spWardrobeLine1.ToList();
                //myWardrobeLine1.OrderBy(a => a.ClothingRank);
                ViewBag.WardrobeLine1 = myWardrobeLine1;
                ViewBag.WardrobeLine1ck = intWardrobeLine1;

                ViewBag.WardrobeLine2ck = intWardrobeLine2;
                if (intWardrobeLine1 != 0)
                {
                    List<spGetUsersUsedMenuItems_Result> myWardrobeLine2 = GarmentList(true, intWardrobeLine1, SelectUserType, userID, WardrobeClosetUserID);
                    ViewBag.WardrobeLine2 = myWardrobeLine2;
                }

                ViewBag.WardrobeLine3ck = intWardrobeLine3;
                if (intWardrobeLine2 != 0)
                {
                    List<spGetUsersUsedMenuItems_Result> myWardrobeLine3 = GarmentList(true, intWardrobeLine2, SelectUserType, userID, WardrobeClosetUserID);
                    ViewBag.WardrobeLine3 = myWardrobeLine3;
                }

                ViewBag.WardrobeLine4ck = intWardrobeLine4;
                if (intWardrobeLine3 != 0)
                {
                    List<spGetUsersUsedMenuItems_Result> myWardrobeLine4 = GarmentList(true, intWardrobeLine3, SelectUserType, userID, WardrobeClosetUserID);
                    ViewBag.WardrobeLine4 = myWardrobeLine4;
                }

                ViewBag.WardrobeLine5ck = intWardrobeLine5;
                if (intWardrobeLine4 != 0)
                {
                    List<spGetUsersUsedMenuItems_Result> myWardrobeLine5 = GarmentList(true, intWardrobeLine4, SelectUserType, userID, WardrobeClosetUserID);
                    ViewBag.WardrobeLine5 = myWardrobeLine5;
                }

                ViewBag.WardrobeLine6ck = intWardrobeLine6;
                if (intWardrobeLine5 != 0)
                {
                    List<spGetUsersUsedMenuItems_Result> myWardrobeLine6 = GarmentList(true, intWardrobeLine5, SelectUserType, userID, WardrobeClosetUserID);
                    ViewBag.WardrobeLine6 = myWardrobeLine6;
                }

                ViewBag.WardrobeLine1ck = intWardrobeLine1;

                var ckClothingtypeID = HttpContext.Request.Cookies["bxClothingtypeID"];
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

                    ViewBag.ClothingList = myWardrobeList;
                }


                var ckSelectImageID = HttpContext.Request.Cookies["ckSelectImageID"];
                var ckSelectImageTitle = HttpContext.Request.Cookies["ckSelectImageTitle"];
                var ckSelectImageALT = HttpContext.Request.Cookies["ckSelectImageALT"];

                string SelectImageID = "./Images/DoubleClickLoadGarment.gif";
                var ckSelectImageGuid = HttpContext.Request.Cookies["ckSelectImageGUID"];
                string SelectImageGUID = "";

                if (ckSelectImageGuid != null)
                {
                    if ((ckSelectImageGuid.Value != "null") && (ckSelectImageGuid.Value != ""))
                    {
                        SelectImageGUID = ckSelectImageGuid.Value;
                        ViewBag.SelectImageGUID = SelectImageGUID;
                    }
                    else
                    {
                        ViewBag.SelectImageGUID = "";
                    }
                }
                else
                {
                    ViewBag.SelectImageGUID = "";
                }

                if (ckSelectImageID != null)
                {
                    if ((ckSelectImageID.Value != "null") && (ckSelectImageID.Value != ""))
                    {
                        SelectImageID = HttpUtility.UrlDecode(ckSelectImageID.Value);
                        ViewBag.SelectImageID = SelectImageID;

                        System.Data.Entity.Core.Objects.ObjectResult<spGetAttachment_Result> spGetAttachment = db.spGetAttachment(SelectImageGUID);
                        List<spGetAttachment_Result> myGetAttachment = spGetAttachment.ToList();

                        if (myGetAttachment.Count() >= 1)
                        {
                            if (myGetAttachment[0].Web != null)
                            {
                                ViewBag.SelectImageURL = (HttpUtility.HtmlDecode(myGetAttachment[0].Web)).Trim();
                            }
                            else
                            {
                                ViewBag.SelectImageURL = "";
                            }

                            if (myGetAttachment[0].Name != null)
                            {
                                ViewBag.SelectedTitle = (HttpUtility.HtmlDecode(myGetAttachment[0].Name)).Trim();
                            }
                            else
                            {
                                ViewBag.SelectedTitle = "";
                            }
                        }
                        else
                        {
                            ViewBag.SelectImageURL = "";
                            ViewBag.SelectedTitle = "";
                        }
                    }
                    else
                    {
                        ViewBag.SelectImageID = SelectImageID;
                        ViewBag.SelectImageURL = "";
                        ViewBag.SelectedTitle = "";
                    }
                }
                else
                {
                    ViewBag.SelectImageID = SelectImageID;
                    ViewBag.SelectImageURL = "";
                    ViewBag.SelectedTitle = "";
                }

                if (ckSelectImageTitle != null)
                {
                    if (ckSelectImageTitle.Value != "")
                    {
                        ViewBag.SelectImageTitle = HttpUtility.UrlDecode(ckSelectImageTitle.Value);
                    }
                    else
                    {
                        ViewBag.SelectImageTitle = "No Garment";
                    }
                }
                else
                {
                    ViewBag.SelectImageTitle = "No Garment";
                }

                if (ckSelectImageALT != null)
                {
                    if (ckSelectImageALT.Value != "")
                    {
                        ViewBag.SelectImageALT = HttpUtility.UrlDecode(ckSelectImageALT.Value);

                    }
                    else
                    {
                        ViewBag.SelectImageALT = "No Garment";
                    }
                }
                else
                {
                    ViewBag.SelectImageALT = "No Garment";
                }

                System.Data.Entity.Core.Objects.ObjectResult<spGetShareFriends_Result> spShareFriends = db.spGetShareFriends(SelectImageGUID);

                if (SelectImageID != "./Images/No_Garment.jpg")
                {
                    List<spGetShareFriends_Result> myShareFriends = spShareFriends.ToList();

                    ViewBag.ShareFriends = myShareFriends;
                    ViewBag.ShareFG = true;
                }
                else 
                {
                    ViewBag.ShareFriends = null;
                    ViewBag.ShareFG = false;
                }

                return View(user);
            }
            else
            {
                return View("Error");
            }
            
        }

        private List<spGetUsersUsedMenuItems_Result> GarmentList(Boolean glShowFG, int glParentID, string glVendorFLG, string glUserID, string glClosetUserID)
        {
            List<spGetUsersUsedMenuItems_Result> myWardrobe;

            System.Data.Entity.Core.Objects.ObjectResult<spGetUsersUsedMenuItems_Result> spWardrobe = db.spGetUsersUsedMenuItems(glShowFG, glParentID, glUserID, glClosetUserID, glUserID, glVendorFLG);
            myWardrobe = spWardrobe.ToList();
            myWardrobe.OrderBy(a => a.ClothingRank);

            return myWardrobe;
        }

        //public ActionResult ShowImage(string siFileID)
        //{
            //System.Data.Entity.Core.Objects.ObjectResult<spGet_File_Result> spResult = db.spGet_File(siFileID);
            //List<spGet_File_Result> myResults = spResult.ToList();
            //return File(myResults[0].attachment, myResults[0].attachDocFiletype);
        //}

        [HttpPost]
        public ActionResult AddCategory(string inpCustomerCategory)
        {
            string userID = "";
            string FBuserID = "";
            int clothingTypeID = 0;
            string CustomerCategory = "";

            CustomerCategory = inpCustomerCategory;

            var ckClothingtypeID = HttpContext.Request.Cookies["bxClothingtypeID"];
            if (ckClothingtypeID != null)
            {
                int.TryParse(ckClothingtypeID.Value, out clothingTypeID);
            }

            userID = HttpContext.Request.Cookies["UserID"].Value.ToString();
            FBuserID = HttpContext.Request.Cookies["FBUserID"].ToString();

            System.Data.Entity.Core.Objects.ObjectResult<spAdd_CustomCategories_Result> spCustomCategory = db.spAdd_CustomCategories(true, CustomerCategory, clothingTypeID, userID);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Upload(string inpItemName, string inpItemWeb)
        {
            string userID = "";
            string FBuserID = "";
            int clothingTypeID = 0;
            string name = "";
            string web = "";

            name = HttpUtility.HtmlEncode(inpItemName);
            web = HttpUtility.HtmlEncode(inpItemWeb);

            var ckClothingtypeID = HttpContext.Request.Cookies["bxClothingtypeID"];
            if (ckClothingtypeID != null)
            {
                int.TryParse(ckClothingtypeID.Value, out clothingTypeID);
            }

            userID = HttpContext.Request.Cookies["UserID"].Value.ToString();
            FBuserID = HttpContext.Request.Cookies["FBUserID"].Value.ToString();
            string FileID;
            Guid myGUID;
            myGUID = Guid.NewGuid();

            FileID = myGUID.ToString();

            foreach (string upload in Request.Files)
            {
                //if (Request.Files[upload].ContentLength > 0) continue;

                string mimeType = Request.Files[upload].ContentType;
                Stream fileStream = Request.Files[upload].InputStream;
                //string fileName = Path.GetFileName(Request.Files[upload].FileName);
                int fileLength = Request.Files[upload].ContentLength;
                byte[] fileData = new byte[fileLength];
                fileStream.Read(fileData, 0, fileLength);

                if ((mimeType == "image/gif") || (mimeType == "image/tiff") || (mimeType == "image/png") || (mimeType == "image/jpeg"))
                {
                    string path = AppDomain.CurrentDomain.BaseDirectory + "./_UserGarments/";
                    string DocName = Path.GetFileName(Request.Files[upload].FileName);
                    string filename = FileID + "_" + Path.GetFileName(Request.Files[upload].FileName);
                    Request.Files[upload].SaveAs(Path.Combine(path, filename));

                    string Designer = "";
                    string Productname = "";

                    db.spAdd_File(userID, clothingTypeID, DocName, filename, mimeType, fileLength, name, web, Designer, Productname);

                    HttpCookie cookie = new HttpCookie("DisplayError");
                    cookie.Value = "0";
                    this.ControllerContext.HttpContext.Response.Cookies.Add(cookie);
                }
                else
                {
                    HttpCookie cookie = new HttpCookie("DisplayError");
                    cookie.Value = "1";
                    this.ControllerContext.HttpContext.Response.Cookies.Add(cookie);
                }
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult UpdateFriends(IEnumerable<string> ShareFriends, string AttachedID)
        {
            System.Data.Entity.Core.Objects.ObjectResult<spClearShareFriends_Result> spResult = db.spClearShareFriends(AttachedID);
            
            foreach (var FriendID in ShareFriends)
            {
                if (FriendID != "false")
                {
                    System.Data.Entity.Core.Objects.ObjectResult<spAddShareFriends_Result> spResultFriends = db.spAddShareFriends(AttachedID, FriendID);
                }
            }

            return RedirectToAction("Index");
        }


        [HttpPost]
        public ActionResult DeleteSelectedImage(string SelectedImageID, string SelectedImageName)
        {
            string userID = HttpContext.Request.Cookies["UserID"].Value.ToString();

            System.Data.Entity.Core.Objects.ObjectResult<spDeleteHideWardrobeImage_Result> spResultFriends = db.spDeleteHideWardrobeImage(userID, SelectedImageID);
                
            ViewBag.SelectImageGUID = "";
            ViewBag.SelectImageID = "";
            ViewBag.SelectImageURL = "";
            ViewBag.SelectedTitle = "";
            ViewBag.SelectImageALT = "";

            HttpCookie cookie = new HttpCookie("ckSelectImageID");
            cookie.Value = "";
            this.ControllerContext.HttpContext.Response.Cookies.Add(cookie);

            cookie = new HttpCookie("ckSelectImageTitle");
            cookie.Value = "";
            this.ControllerContext.HttpContext.Response.Cookies.Add(cookie);

            cookie = new HttpCookie("ckSelectImageALT");
            cookie.Value = "";
            this.ControllerContext.HttpContext.Response.Cookies.Add(cookie);

            cookie = new HttpCookie("ckSelectImageGUID");
            cookie.Value = "";
            this.ControllerContext.HttpContext.Response.Cookies.Add(cookie);

            return RedirectToAction("Index");
        }
	}
}