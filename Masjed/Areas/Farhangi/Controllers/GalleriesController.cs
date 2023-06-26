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
using System.Web.Helpers;

namespace Masjed.Areas.Farhangi.Controllers
{
    public class GalleriesController : Controller
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
                var galleries = db.Galleries.Where(x => x.ActionId == id).Include(g => g.ActionMe);
                return View(galleries.ToList());
            }
            else
            {
                int actionid = (int)Session["ActionId"];
                var galleries = db.Galleries.Where(x => x.ActionId == actionid).Include(g => g.ActionMe);
                return View(galleries.ToList());
            }

        }

        [Authorize(Roles = "Admin,User")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gallery gallery = db.Galleries.Find(id);
            if (gallery == null)
            {
                return HttpNotFound();
            }
            return View(gallery);
        }


        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            int _ActionId = (int)Session["ActionId"];
            ViewBag.ActionId = new SelectList(db.ActionMes.Where(x => x.Id == _ActionId), "Id", "Title");
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ImgMain,ImgLetter,ActionId")] Gallery gallery, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    //ImageName
                    Guid guid = System.Guid.NewGuid();
                    string imgname = guid + "-" + file.FileName;

                    file.SaveAs(HttpContext.Server.MapPath("~/Content/Images/") + imgname);
                    gallery.ImgMain = imgname;


                    WebImage Img = new WebImage(file.InputStream);
                    Guid guid1 = System.Guid.NewGuid();
                    string imgname1 = "Little" + " -" + guid + "-" + file.FileName;
                    if (Img.Width > 300)
                    {
                        Img.Resize(250, 250);
                        Img.Save("~/Content/Images/" + imgname1);
                        gallery.ImgLetter = imgname1;
                    }


                }
                db.Galleries.Add(gallery);
                db.SaveChanges();
                return RedirectToAction("Index",new { id=gallery.ActionId});
            }

            ViewBag.ActionId = new SelectList(db.ActionMes, "Id", "Title");
            return View(gallery);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gallery gallery = db.Galleries.Find(id);
            if (gallery == null)
            {
                return HttpNotFound();
            }
            int _ActionId = (int)Session["ActionId"];
            ViewBag.ActionId = new SelectList(db.ActionMes.Where(x => x.Id == _ActionId), "Id", "Title");
            return View(gallery);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ImgMain,ImgLetter,ActionId")] Gallery gallery, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {


                    if (System.IO.File.Exists(Server.MapPath("~/Content/Images/") + gallery.ImgMain))
                    {
                        System.IO.File.Delete(Server.MapPath("~/Content/Images/") + gallery.ImgMain);

                    }
                    if (System.IO.File.Exists(Server.MapPath("~/Content/Images/") + gallery.ImgLetter))
                    {
                        System.IO.File.Delete(Server.MapPath("~/Content/Images/") + gallery.ImgLetter);
                    }

                    //ImageName
                    Guid guid = System.Guid.NewGuid();
                    string imgname = guid + "-" + file.FileName;
                    file.SaveAs(HttpContext.Server.MapPath("~/Content/Images/") + imgname);
                    gallery.ImgMain = imgname;




                    WebImage Img = new WebImage(file.InputStream);
                    Guid guid1 = System.Guid.NewGuid();
                    string imgname1 = "Little" + guid + "-" + file.FileName;

                    if (Img.Width > 300)
                     Img.Resize(250, 250);
                    Img.Save("~/Content/Images/" + imgname1);
                    gallery.ImgLetter = imgname1;
                   
              




                }
                //db.Galleries.Where(x => x.ActionId == gallery.ActionId).FirstOrDefault().ImgMain = gallery.ImgMain;
                //db.Galleries.Where(x => x.ActionId == gallery.ActionId).FirstOrDefault().ImgLetter = gallery.ImgLetter;
                db.Entry(gallery).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { id = gallery.ActionId });
            }
            ViewBag.ActionId = new SelectList(db.ActionMes, "Id", "Title");
            return View(gallery);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gallery gallery = db.Galleries.Find(id);
            if (gallery == null)
            {
                return HttpNotFound();
            }
            return View(gallery);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Gallery gallery = db.Galleries.Find(id);
            db.Galleries.Remove(gallery);
            if (System.IO.File.Exists(Server.MapPath("~/Content/Images/") + gallery.ImgLetter))
            {
                System.IO.File.Delete(Server.MapPath("~/Content/Images/") + gallery.ImgLetter);
            }
            if (System.IO.File.Exists(Server.MapPath("~/Content/Images/") + gallery.ImgMain))
            {
                System.IO.File.Delete(Server.MapPath("~/Content/Images/") + gallery.ImgMain);
            }
            db.SaveChanges();
            return RedirectToAction("Index", new { id = gallery.ActionId });
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
