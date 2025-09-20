using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using OMS.DAL;
using OMS.Models;
using OMS.ViewModel;

namespace OMS.Controllers
{
    public class ProductsController : Controller
    {
        private MedicalContext db = new MedicalContext();
        [Authorize(Roles = "confirm")]
        // GET: Products
        public ActionResult Index()
        {
            //ProductStoreViewModel ps = new ProductStoreViewModel();
            string s = User.Identity.GetUserName();
            var products = db.Products.Where(x=>x.MadicalStore.userid==s).Include(p => p.MadicalStore);
            return View(products.ToList());
        }


        //Products list for visitors search




        public ActionResult PublicProductList(string MadicalStore, string Category, string search, string id)
        {
            var products = db.Products.Include(p => p.MadicalStore);
            if (!String.IsNullOrEmpty(MadicalStore))
            {
                products = products.Where(p => p.MadicalStore.Name == MadicalStore);
            }
            if (!String.IsNullOrEmpty(Category))
            {
                products = products.Where(p => p.Category == Category);
            }
            if (!String.IsNullOrEmpty(search))
            {
                products = products.Where(p => p.ProducName.Contains(search) ||
                p.Producformula.Contains(search) ||
                p.Productweight.Contains(search) ||
                p.Description.Contains(search) ||
                p.Category.Contains(search) ||
                p.MadicalStore.Name.Contains(search) ||
                p.MadicalStore.City.Contains(search) ||
                p.MadicalStore.StoreAddress.Contains(search) ||
                p.MadicalStore.StoreName.Contains(search) ||
                p.Producformula.Contains(search));
            }
            if (!String.IsNullOrEmpty(id))
            {
                products = products.Where(p => p.MadicalStore.StoreLicense == id);
            }
            return View(products.ToList());
        }













        public ActionResult PublicPageStores()
        {
            
            var stores = db.MadicalStores;
            return View(stores.ToList());
        }
        public ActionResult PublicPageCategories()
        {

            var stores = db.Products;
            return View(stores.ToList());
        }



        public ActionResult PublicPage(String id)
        {
            var products = db.Products.Include(p => p.MadicalStore);
            if (id!=null)
            {
                products = products.Where(p => p.MadicalStore.StoreLicense == id);
            }
           
            return View(products.ToList());
        }















        //public ActionResult PublicPage(string MadicalStore, string Category, string search)
        //{
        //    var products = db.Products.Include(p => p.MadicalStore);
        //    if (!String.IsNullOrEmpty(MadicalStore))
        //    {
        //        products = products.Where(p => p.MadicalStore.Name == MadicalStore);
        //    }
        //    if (!String.IsNullOrEmpty(Category))
        //    {
        //        products = products.Where(p => p.Category == Category);
        //    }
        //    if (!String.IsNullOrEmpty(search))
        //    {
        //        products = products.Where(p => p.ProducName.Contains(search) ||
        //        p.Producformula.Contains(search) ||
        //        p.Productweight.Contains(search) ||
        //        p.Description.Contains(search) ||
        //        p.Category.Contains(search) ||
        //        p.MadicalStore.Name.Contains(search) ||
        //        p.MadicalStore.City.Contains(search) ||
        //        p.MadicalStore.StoreAddress.Contains(search) ||
        //        p.MadicalStore.StoreName.Contains(search) ||
        //        p.Producformula.Contains(search));
        //    }
        //    return View(products.ToList());
        //}

        [Authorize(Roles = "confirm")]
        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = db.Products.Find(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            return View(products);
        }
        [Authorize(Roles = "confirm")]
        // GET: Products/Create
        public ActionResult Create()
        {
            string s = User.Identity.GetUserName();

            var madicalStoress = db.MadicalStores.Where(x => x.userid == s).ToList();
            ViewBag.MadicalStoreID = new SelectList(madicalStoress, "MadicalStoreID", "StoreName");
            
            return View();
        }
        [Authorize(Roles = "confirm")]
        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ProducName,Producformula,Productweight,Price,Description,Category,MadicalStoreID")] Products products,HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                products.productImage = file.FileName;
                SaveFileToDisk(file);
                db.Products.Add(products);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            string s = User.Identity.GetUserName();
            ViewBag.MadicalStoreID = new SelectList(db.MadicalStores, "MadicalStoreID", "StoreName", products.MadicalStoreID);
            return View(products);
        }
        [Authorize(Roles = "confirm")]
        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = db.Products.Find(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            string s = User.Identity.GetUserName();
            ViewBag.MadicalStoreID = new SelectList(db.MadicalStores.Where(x=>x.userid==s), "MadicalStoreID", "StoreName", products.MadicalStoreID);
            return View(products);
        }
        [Authorize(Roles = "confirm")]
        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ProducName,Producformula,Productweight,Price,Description,Category,MadicalStoreID")] Products products,HttpPostedFileBase file,string img)
        {
            if (ModelState.IsValid)
            {
                db.Entry(products).State = EntityState.Modified;
                if (file != null)
                {
                    products.productImage = file.FileName;
                    SaveFileToDisk(file);
                    db.SaveChanges();
                }
                else
                {
                    products.productImage = img;
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            string s = User.Identity.GetUserName();
            ViewBag.MadicalStoreID = new SelectList(db.MadicalStores, "MadicalStoreID", "StoreName", products.MadicalStoreID);
            return View(products);
        }
        [Authorize(Roles = "confirm")]
        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = db.Products.Find(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            return View(products);
        }
        [Authorize(Roles = "confirm")]
        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Products products = db.Products.Find(id);
            db.Products.Remove(products);
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
