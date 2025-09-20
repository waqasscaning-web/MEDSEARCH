using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using OMS.DAL;
using OMS.Models;
using OMS.ViewModel;
namespace OMS.Controllers
{
    public class HomeController : Controller
    {
        private MedicalContext db = new MedicalContext();
        [Authorize]
        public ActionResult Index()
        {
            var products= db.Products.Include(p => p.MadicalStore);
            if (User.IsInRole("Admin"))
            {
                //products = db.Products.Include(p => p.MadicalStore);
                return RedirectToAction("Index", "UsersAdmin");
            }
            //else
            //{
            //    string s = User.Identity.GetUserName();
            //    products = db.Products.Where(x => x.MadicalStore.userid == s).Include(p => p.MadicalStore);
            //}
            return RedirectToAction("Index", "Products");

        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}