using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Masjed.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Masjed.DataLayer.Models;

 namespace Masjed.Areas.Farhangi.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class ApplicationUsersController : Controller
    {
      
        private ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }


        [Authorize(Roles = "Admin,User")]
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }


        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            var roleStore = new RoleStore<IdentityRole>(new ApplicationDbContext());
            var roleManager = new RoleManager<IdentityRole>(roleStore);
            ViewBag.error = TempData["Error"];
            ViewBag.Roles = new SelectList(roleManager.Roles.ToList(), "Name", "Name");
            return View();
        }

        //[Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserAdmin applicationUser, string Roles)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = applicationUser.Email, Email = applicationUser.Email, EmailConfirmed = applicationUser.ConfirmEmail };

                var result = UserManager.Create(user, applicationUser.Password);
                var roleStore = new RoleStore<IdentityRole>(new ApplicationDbContext());
                var roleManager = new RoleManager<IdentityRole>(roleStore);

                var userStore = new UserStore<ApplicationUser>(new ApplicationDbContext());
                var userManager = new UserManager<ApplicationUser>(userStore);
                userManager.AddToRole(user.Id, Roles);
                //db.Users.Add(applicationUser);
                //db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(applicationUser);
        }
        [Authorize(Roles = "Admin,User")]
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(string id)
        {
            var roleStore = new RoleStore<IdentityRole>(new ApplicationDbContext());
            var roleManager = new RoleManager<IdentityRole>(roleStore);
            ViewBag.Roles = new SelectList(roleManager.Roles.ToList(), "Name", "Name");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = UserManager.FindById(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Email,EmailConfirmed,PasswordHash,PhoneNumber,UserName")] ApplicationUser applicationUser)
        {
            if (ModelState.IsValid)
            {


                db.Entry(applicationUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(applicationUser);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Delete(string id)
        {
            ApplicationUser user = UserManager.FindById(id);
            if (user != null)
            {
                IdentityResult result = UserManager.Delete(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View("Error", result.Errors);
                }
            }
            else
            {
                return View("Error", new string[] { "کاربر مورد نظر یافت نشد" });
            }
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
