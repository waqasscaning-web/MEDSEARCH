using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using OMS.DAL;
using OMS.Models;

namespace OMS.Controllers
{
    public class MadicalStoresController : Controller
    {
        private MedicalContext db = new MedicalContext();
        [Authorize(Roles = "confirm")]
        // GET: MadicalStores
        public ActionResult Index()
        {
            string s = User.Identity.GetUserName();
            return View(db.MadicalStores.Where(x => x.userid == s).ToList());
        }



        //Serch and Result 

        public ActionResult bycity(string area)
        {
            var c = db.MadicalStores.DistinctBy(x=>x.City).ToList();
            
            if (!string.IsNullOrEmpty(area))
            {
                var y = db.MadicalStores.Where(a => a.City == area).ToList();


                var x = new List<MadicalStore>();
                foreach (var ob in y)
                {
                    x.Add(ob);
                }
                TempData["re"] = x;
                return RedirectToAction("result", ViewData["re"]);
            }
            else
           
                return
                   View(c);
           
               


        }

        public ActionResult result(IList<MadicalStore> x)
        {

            return View(x);
        }























        [Authorize(Roles = "confirm")]
        // GET: MadicalStores/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MadicalStore madicalStore = db.MadicalStores.Find(id);
            if (madicalStore == null)
            {
                return HttpNotFound();
            }
            return View(madicalStore);
        }
        [Authorize(Roles = "confirm")]
        // GET: MadicalStores/Create
        public ActionResult Create()
        {
            if (User.IsInRole("confirm"))
            {
                MadicalStore m = new MadicalStore();
                m.userid = User.Identity.GetUserName();
                return View(m);
            }
            return RedirectToAction("Blocked");
        }
        [Authorize]
        public ActionResult Blocked()
        {
            return View();
        }
        [Authorize(Roles = "confirm")]
        // POST: MadicalStores/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MadicalStoreID,Name,MobileNo,StoreName,StoreLicense,City,LandLine,StoreAddress,userid")] MadicalStore madicalStore,HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                madicalStore.StoreImage = file.FileName;
                SaveFileToDisk(file);
                db.MadicalStores.Add(madicalStore);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(madicalStore);
        }
        [Authorize(Roles = "confirm")]
        // GET: MadicalStores/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MadicalStore madicalStore = db.MadicalStores.Find(id);
            if (madicalStore == null)
            {
                return HttpNotFound();
            }
            return View(madicalStore);
        }
        [Authorize(Roles = "confirm")]
        // POST: MadicalStores/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MadicalStoreID,Name,MobileNo,StoreName,StoreLicense,City,LandLine,StoreAddress,userid")] MadicalStore madicalStore, HttpPostedFileBase file, string img)
        {
            if (ModelState.IsValid)
            {
                db.Entry(madicalStore).State = EntityState.Modified;
                if (file != null)
                {
                    madicalStore.StoreImage = file.FileName;
                    SaveFileToDisk(file);
                    db.SaveChanges();
                }
                else
                {
                    madicalStore.StoreImage = img;
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            return View(madicalStore);
        }
        [Authorize(Roles = "confirm")]
        // GET: MadicalStores/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MadicalStore madicalStore = db.MadicalStores.Find(id);
            if (madicalStore == null)
            {
                return HttpNotFound();
            }
            return View(madicalStore);
        }
        [Authorize(Roles = "confirm")]
        // POST: MadicalStores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MadicalStore madicalStore = db.MadicalStores.Find(id);
            db.MadicalStores.Remove(madicalStore);
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

        private void SaveFileToDisk(HttpPostedFileBase file)
        {
            WebImage img = new WebImage(file.InputStream);
            img.Resize(img.Width, img.Height);

            img.Save(Constaants.ProductImagePath + file.FileName);

            img.Resize(100, img.Height);

            img.Save(Constaants.ProductThumbnailPath + file.FileName);



        }




    }

}
