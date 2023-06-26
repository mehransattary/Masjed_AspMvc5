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
using System.IO;
using Rotativa.MVC;
using PagedList;
using Masjed.Utilities.Convertor;

namespace Masjed.Areas.Farhangi.Controllers
{
   
    public class ActionMesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Authorize(Roles = "Admin,User")]
        public ActionResult Index(string str, int? page = 1 )
        {
            if (str != null && str!="")
            {      

                var action = db.ActionMes.SqlQuery("select * from ActionMes ").ToList();
                if (Request.IsAjaxRequest())
                {
                    return PartialView("_Index", action.Where(x => x.Title.Contains(str)).OrderBy(x => x.Id).ToPagedList((int)page, 12));
                }
                return View(action.OrderBy(x => x.Id).Where(x => x.Title.Contains(str)).ToPagedList((int)page, 12));
            }
            else
            {
                var action = db.ActionMes;
                if (Request.IsAjaxRequest())
                {
                    return PartialView("_Index", action.OrderBy(x => x.Id).ToPagedList((int)page,12));
                }
                return View(action.OrderBy(x => x.Id).ToPagedList((int)page, 12));

            }

        }
        public ActionResult GetData(string str)
        {
            var query = (from p in db.ActionMes
                         orderby p.Id ascending
                         where p.Title.Contains(str)
                         select p).Take(10);
            var result = db.ActionMes.Where(x => x.Title.Contains(str)).ToList();
            return Json(query.ToList(), JsonRequestBehavior.AllowGet);
        }
    

        public ActionResult ReportIndex(string AsDate, string ToDate)
        {
            if (AsDate != null && ToDate != null)
            {
                string asDate = ConvertDate.ConvertToEnglish(AsDate);
                string toDate = ConvertDate.ConvertToEnglish(ToDate);

                int _AsDate_Year = int.Parse(((asDate)).Substring(0, 4));
                int _AsDate_Month = int.Parse(((asDate)).Substring(5, 2));
                int _AsDate_Day = int.Parse(((asDate)).Substring(8, 2));

                int _ToDate_Year = int.Parse(((toDate)).Substring(0, 4));
                int _ToDate_Month = int.Parse(((toDate)).Substring(5, 2));
                int _ToDate_Day = int.Parse(((toDate)).Substring(8, 2));





                if (ToDate != null && AsDate == null)
                {
                    var date = db.ActionMes.Where(x => ((x.Year)) <= _ToDate_Year && x.Month <= _ToDate_Month && x.Day <= _ToDate_Day);
                    return View(date.ToList());
                }
                else if (AsDate != null && ToDate == null)
                {
                    var date = db.ActionMes.Where(x => ((x.Year)) >= _AsDate_Year && x.Month >= _AsDate_Month && x.Day >= _AsDate_Day);
                    return View(date.ToList());
                }
                else
                {
                    var date = db.ActionMes.Where(x => ((x.Year)) <= _ToDate_Year && x.Month <= _ToDate_Month && x.Day <= _ToDate_Day && ((x.Year)) >= _AsDate_Year && x.Month >= _AsDate_Month && x.Day >= _AsDate_Day);
                    return View(date.ToList());
                }
            }
            else
            {
                return View(db.ActionMes.ToList());
            }



        }
        [Authorize(Roles = "Admin")]
        public ActionResult PrintAll(string AsDate, string ToDate)
        {
            var report = new ActionAsPdf("ReportIndex", new { AsDate = AsDate, ToDate = ToDate });
            return report;
        }


        [Authorize(Roles = "Admin,User")]
        // GET: Farhangi/ActionMes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActionMe actionMe = db.ActionMes.Find(id);
            if (actionMe == null)
            {
                return HttpNotFound();
            }
            return View(actionMe);
        }
        public ActionResult DetailsReport(int? id)
        {
            var result = db.ActionMes.Where(x => x.Id == id).FirstOrDefault();
            return View(result);
        }

        public ActionResult Print(int id)
        {
            var report = new ActionAsPdf("DetailsReport", new { id = id });
            return report;
        }
   
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,DateCreate,Img,Description,date")] ActionMe actionMe, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    //ImageName
                    Guid guid = System.Guid.NewGuid();
                    string imgname = guid + "-" + file.FileName;

                    file.SaveAs(HttpContext.Server.MapPath("~/Content/Images/") + imgname);
                    actionMe.Img = imgname;
                }
                if (actionMe.DateCreate != null)
                {

                    actionMe.DateCreate = Masjed.Utilities.Convertor.ConvertDate.ConvertToEnglish(actionMe.DateCreate);
                    actionMe.Year = actionMe.DateCreate.ConvertIntYear();
                    actionMe.Month = actionMe.DateCreate.ConvertIntMonth();
                    actionMe.Day = actionMe.DateCreate.ConvertIntDay();
                }
                db.ActionMes.Add(actionMe);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(actionMe);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActionMe actionMe = db.ActionMes.Find(id);
            if (actionMe == null)
            {
                return HttpNotFound();
            }
            return View(actionMe);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,DateCreate,Img,Description,date")] ActionMe actionMe, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {

                    //ImageName 
                    Guid guid = System.Guid.NewGuid();
                    string imgname = guid + "-" + file.FileName;
                    if (System.IO.File.Exists(Server.MapPath("~/Content/Images/") + actionMe.Img))
                    {
                        System.IO.File.Delete(Server.MapPath("~/Content/Images/") + actionMe.Img);
                    }


                    file.SaveAs(HttpContext.Server.MapPath("~/Content/Images/") + imgname);
                    actionMe.Img = imgname;
                }
                db.Entry(actionMe).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(actionMe);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActionMe actionMe = db.ActionMes.Find(id);
            if (actionMe == null)
            {
                return HttpNotFound();
            }
            return View(actionMe);
        }

        // POST: Farhangi/ActionMes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ActionMe actionMe = db.ActionMes.Find(id);
            db.ActionMes.Remove(actionMe);
            if (System.IO.File.Exists(Server.MapPath("~/Content/Images/") + actionMe.Img))
            {
                System.IO.File.Delete(Server.MapPath("~/Content/Images/") + actionMe.Img);
            }
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
