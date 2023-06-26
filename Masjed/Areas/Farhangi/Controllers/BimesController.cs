using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Masjed.DomainClass;
using Masjed.Models;

namespace Masjed.Areas.Farhangi.Controllers
{
    public class BimesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Authorize(Roles = "Admin,User")]
        public ActionResult Index()
        {
            return View(db.Bimes.ToList());
        }

        [Authorize(Roles = "Admin,User")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bime bime = db.Bimes.Find(id);
            if (bime == null)
            {
                return HttpNotFound();
            }
            return View(bime);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,PriceBime,AsDate,TaDate,date")] Bime bime)
        {
            if (ModelState.IsValid)
            {
                db.Bimes.Add(bime);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bime);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bime bime = db.Bimes.Find(id);
            if (bime == null)
            {
                return HttpNotFound();
            }
            return View(bime);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,PriceBime,AsDate,TaDate,date")] Bime bime)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bime).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bime);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bime bime = db.Bimes.Find(id);
            if (bime == null)
            {
                return HttpNotFound();
            }
            return View(bime);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Bime bime = db.Bimes.Find(id);
            db.Bimes.Remove(bime);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
