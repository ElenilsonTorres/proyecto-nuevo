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
    public class InscripcionesController : Controller
    {
        private Sistemadeagua5Entities db = new Sistemadeagua5Entities();

        // GET: Inscripciones
        public ActionResult Index()
        {
            var inscripciones = db.Inscripciones.Include(i => i.Cliente);
            return View(inscripciones.ToList());
        }

        // GET: Inscripciones/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inscripcione inscripcione = db.Inscripciones.Find(id);
            if (inscripcione == null)
            {
                return HttpNotFound();
            }
            return View(inscripcione);
        }

        // GET: Inscripciones/Create
        public ActionResult Create()
        {
            ViewBag.ClienteId = new SelectList(db.Clientes, "Id", "Nombre_Completo");
            return View();
        }

        // POST: Inscripciones/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Fecha_Inicio,Monto_Pagar,Plazo,Cuota,Estado,Estado_Config,Fecha_Registro,Estado2,ClienteId")] Inscripcione inscripcione)
        {
            if (ModelState.IsValid)
            {
                db.Inscripciones.Add(inscripcione);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClienteId = new SelectList(db.Clientes, "Id", "Nombre_Completo", inscripcione.ClienteId);
            return View(inscripcione);
        }

        // GET: Inscripciones/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inscripcione inscripcione = db.Inscripciones.Find(id);
            if (inscripcione == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClienteId = new SelectList(db.Clientes, "Id", "Nombre_Completo", inscripcione.ClienteId);
            return View(inscripcione);
        }

        // POST: Inscripciones/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Fecha_Inicio,Monto_Pagar,Plazo,Cuota,Estado,Estado_Config,Fecha_Registro,Estado2,ClienteId")] Inscripcione inscripcione)
        {
            if (ModelState.IsValid)
            {
                db.Entry(inscripcione).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClienteId = new SelectList(db.Clientes, "Id", "Nombre_Completo", inscripcione.ClienteId);
            return View(inscripcione);
        }

        // GET: Inscripciones/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inscripcione inscripcione = db.Inscripciones.Find(id);
            if (inscripcione == null)
            {
                return HttpNotFound();
            }
            return View(inscripcione);
        }

        // POST: Inscripciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Inscripcione inscripcione = db.Inscripciones.Find(id);
            db.Inscripciones.Remove(inscripcione);
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
