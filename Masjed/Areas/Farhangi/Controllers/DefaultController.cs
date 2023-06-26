using Masjed.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Masjed.Areas.Farhangi.Controllers
{
    public class DefaultController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        [Authorize(Roles = "Admin,User")]
        public ActionResult Index()
        {
            var result = db.ActionMes.OrderBy(x => x.DateCreate).Take(12);
            return View(result);
        }
     
       
     
    }
}