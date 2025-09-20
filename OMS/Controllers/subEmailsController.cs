using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OMS.DAL;
using OMS.Models;

namespace OMS.Controllers
{
    
    public class subEmailsController : Controller
    {
        private MedicalContext db = new MedicalContext();

        // GET: subEmails
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View(db.sub.ToList());
        }
        [Authorize(Roles = "Admin")]
        // GET: subEmails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            subEmails subEmails = db.sub.Find(id);
            if (subEmails == null)
            {
                return HttpNotFound();
            }
            return View(subEmails);
        }

        // GET: subEmails/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: subEmails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,email")] subEmails subEmails)
        {
            if (ModelState.IsValid)
            {
                db.sub.Add(subEmails);
                db.SaveChanges();
                return RedirectToAction("bycity", "MadicalStores");
            }

            return RedirectToAction("bycity", "MadicalStores");
        }
        [Authorize(Roles = "Admin")]
        // GET: subEmails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            subEmails subEmails = db.sub.Find(id);
            if (subEmails == null)
            {
                return HttpNotFound();
            }
            return View(subEmails);
        }
        [Authorize(Roles = "Admin")]
        // POST: subEmails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,email")] subEmails subEmails)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subEmails).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(subEmails);
        }
        [Authorize(Roles = "Admin")]
        // GET: subEmails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            subEmails subEmails = db.sub.Find(id);
            if (subEmails == null)
            {
                return HttpNotFound();
            }
            return View(subEmails);
        }
        [Authorize(Roles = "Admin")]
        // POST: subEmails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            subEmails subEmails = db.sub.Find(id);
            db.sub.Remove(subEmails);
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
