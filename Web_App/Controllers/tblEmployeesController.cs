using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Web_App;

namespace Web_App.Controllers
{
    
    public class tblEmployeesController : Controller
    {
        private WebAplicationRegisterEmployeeEntities1 db = new WebAplicationRegisterEmployeeEntities1();

        // GET: tblEmployees
       
        public ActionResult Index()
        {
            return View(db.tblEmployees.ToList());
        }

        // GET: tblEmployees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblEmployees tblEmployees = db.tblEmployees.Find(id);
            if (tblEmployees == null)
            {
                return HttpNotFound();
            }
            return View(tblEmployees);
        }

        // GET: tblEmployees/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: tblEmployees/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Last_Name,First_Name,Years,NroDocument,Post")] tblEmployees tblEmployees)
        {
            if (ModelState.IsValid)
            {
                db.tblEmployees.Add(tblEmployees);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tblEmployees);
        }

        // GET: tblEmployees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblEmployees tblEmployees = db.tblEmployees.Find(id);
            if (tblEmployees == null)
            {
                return HttpNotFound();
            }
            return View(tblEmployees);
        }

        // POST: tblEmployees/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Last_Name,First_Name,Years,NroDocument,Post")] tblEmployees tblEmployees)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblEmployees).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblEmployees);
        }

        // GET: tblEmployees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblEmployees tblEmployees = db.tblEmployees.Find(id);
            if (tblEmployees == null)
            {
                return HttpNotFound();
            }
            return View(tblEmployees);
        }

        // POST: tblEmployees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblEmployees tblEmployees = db.tblEmployees.Find(id);
            db.tblEmployees.Remove(tblEmployees);
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
