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
    public class PeopleController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Authorize(Roles = "Admin,User")]
        public ActionResult Index(int? id)
        {
          

            if (id != null)
            {
                var actionname = db.ActionMes.Find(id).Title;
                var actionId = db.ActionMes.Find(id).Id;
                Session["ActionName"] = actionname;
                Session["ActionId"] = actionId;
                var people = db.People.Where(x => x.ActionId == id).Include(g => g.Bime);
                return View(people.ToList());
            }
            else
            {
                int actionid = (int)Session["ActionId"];
                var people = db.People.Where(x => x.ActionId == actionid).Include(g => g.Bime);
                return View(people.ToList());
            }

        }

        [Authorize(Roles = "Admin,User")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.People.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }
        [Authorize(Roles ="Admin")]
        public ActionResult Create()
        {
            ViewBag.BimeId = new SelectList(db.Bimes, "Id", "Title");
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Fname,Lname,Age,CodeMeli,BimeId,IsBime")] Person person)
        {
            if (ModelState.IsValid)
            {
                person.ActionId = (int)Session["ActionId"];
                db.People.Add(person);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BimeId = new SelectList(db.Bimes, "Id", "Title", person.BimeId);
            return View(person);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.People.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            ViewBag.BimeId = new SelectList(db.Bimes, "Id", "Title", person.BimeId);
            return View(person);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Fname,Lname,Age,CodeMeli,BimeId,IsBime")] Person person)
        {
            if (ModelState.IsValid)
            {
                person.ActionId = (int)Session["ActionId"];
                db.Entry(person).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BimeId = new SelectList(db.Bimes, "Id", "Title", person.BimeId);
            return View(person);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.People.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Person person = db.People.Find(id);
            db.People.Remove(person);
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
