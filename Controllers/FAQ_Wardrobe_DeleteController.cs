using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace fbuCloset.Controllers
{
    public class FAQ_Wardrobe_DeleteController : Controller
    {
        //
        // GET: /FAQ_Wardrobe_Delete/
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /FAQ_Wardrobe_Delete/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /FAQ_Wardrobe_Delete/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /FAQ_Wardrobe_Delete/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /FAQ_Wardrobe_Delete/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /FAQ_Wardrobe_Delete/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /FAQ_Wardrobe_Delete/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /FAQ_Wardrobe_Delete/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
