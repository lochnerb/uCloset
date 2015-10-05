using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace fbuCloset.Controllers
{
    public class FAQ_Outfit_PostController : Controller
    {
        //
        // GET: /FAQ_Outfit_Post/
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /FAQ_Outfit_Post/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /FAQ_Outfit_Post/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /FAQ_Outfit_Post/Create
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
        // GET: /FAQ_Outfit_Post/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /FAQ_Outfit_Post/Edit/5
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
        // GET: /FAQ_Outfit_Post/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /FAQ_Outfit_Post/Delete/5
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
