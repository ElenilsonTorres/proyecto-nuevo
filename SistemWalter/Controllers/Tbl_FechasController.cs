using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SistemWalter.Context;

namespace SistemWalter.Controllers
{
    public class Tbl_FechasController : Controller
    {
        private Sistemadeagua5Entities db = new Sistemadeagua5Entities();

        // GET: Tbl_Fechas
        public ActionResult Index()
        {
            return View(db.Tbl_Fechas.ToList());
        }

        // GET: Tbl_Fechas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_Fechas tbl_Fechas = db.Tbl_Fechas.Find(id);
            if (tbl_Fechas == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Fechas);
        }

        // GET: Tbl_Fechas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tbl_Fechas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdFecha,Fecha_Pago,Fecha_Vencimiento,Mes,Anio")] Tbl_Fechas tbl_Fechas)
        {
            if (ModelState.IsValid)
            {
                db.Tbl_Fechas.Add(tbl_Fechas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbl_Fechas);
        }

        // GET: Tbl_Fechas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_Fechas tbl_Fechas = db.Tbl_Fechas.Find(id);
            if (tbl_Fechas == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Fechas);
        }

        // POST: Tbl_Fechas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdFecha,Fecha_Pago,Fecha_Vencimiento,Mes,Anio")] Tbl_Fechas tbl_Fechas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_Fechas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbl_Fechas);
        }

        // GET: Tbl_Fechas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_Fechas tbl_Fechas = db.Tbl_Fechas.Find(id);
            if (tbl_Fechas == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Fechas);
        }

        // POST: Tbl_Fechas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tbl_Fechas tbl_Fechas = db.Tbl_Fechas.Find(id);
            db.Tbl_Fechas.Remove(tbl_Fechas);
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
