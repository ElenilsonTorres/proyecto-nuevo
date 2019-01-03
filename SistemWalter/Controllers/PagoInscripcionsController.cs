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
    public class PagoInscripcionsController : Controller
    {
        private SistemadeAguaEntities db = new SistemadeAguaEntities();

        // GET: PagoInscripcions
        public ActionResult Index()
        {
            var pagoInscripcions = db.PagoInscripcions.Include(p => p.Cliente).Include(p => p.Inscripcione);
            return View(pagoInscripcions.ToList());
        }

        // GET: PagoInscripcions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PagoInscripcion pagoInscripcion = db.PagoInscripcions.Find(id);
            if (pagoInscripcion == null)
            {
                return HttpNotFound();
            }
            return View(pagoInscripcion);
        }

        // GET: PagoInscripcions/Create
        public ActionResult Create()
        {
            ViewBag.Cliente_Id = new SelectList(db.Clientes, "Id", "Nombre_Completo");
            ViewBag.InscripcionId = new SelectList(db.Inscripciones, "Id", "Estado");
            return View();
        }

        // POST: PagoInscripcions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,InscripcionId,Monto,Fecha,Estado,Cliente_Id")] PagoInscripcion pagoInscripcion)
        {
            if (ModelState.IsValid)
            {
                db.PagoInscripcions.Add(pagoInscripcion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Cliente_Id = new SelectList(db.Clientes, "Id", "Nombre_Completo", pagoInscripcion.Cliente_Id);
            ViewBag.InscripcionId = new SelectList(db.Inscripciones, "Id", "Estado", pagoInscripcion.InscripcionId);
            return View(pagoInscripcion);
        }

        // GET: PagoInscripcions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PagoInscripcion pagoInscripcion = db.PagoInscripcions.Find(id);
            if (pagoInscripcion == null)
            {
                return HttpNotFound();
            }
            ViewBag.Cliente_Id = new SelectList(db.Clientes, "Id", "Nombre_Completo", pagoInscripcion.Cliente_Id);
            ViewBag.InscripcionId = new SelectList(db.Inscripciones, "Id", "Estado", pagoInscripcion.InscripcionId);
            return View(pagoInscripcion);
        }

        // POST: PagoInscripcions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,InscripcionId,Monto,Fecha,Estado,Cliente_Id")] PagoInscripcion pagoInscripcion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pagoInscripcion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Cliente_Id = new SelectList(db.Clientes, "Id", "Nombre_Completo", pagoInscripcion.Cliente_Id);
            ViewBag.InscripcionId = new SelectList(db.Inscripciones, "Id", "Estado", pagoInscripcion.InscripcionId);
            return View(pagoInscripcion);
        }

        // GET: PagoInscripcions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PagoInscripcion pagoInscripcion = db.PagoInscripcions.Find(id);
            if (pagoInscripcion == null)
            {
                return HttpNotFound();
            }
            return View(pagoInscripcion);
        }

        // POST: PagoInscripcions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PagoInscripcion pagoInscripcion = db.PagoInscripcions.Find(id);
            db.PagoInscripcions.Remove(pagoInscripcion);
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
