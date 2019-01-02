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

    //[Authorize(Roles = "")]      //Esta linea de codigo representa las autorizaciones y roles de usuarios 
    public class ConfiguracionesController : Controller
    {
        private SistemadeAguaEntities db = new SistemadeAguaEntities();

        // GET: Configuraciones
        public ActionResult Index()
        {
            return View(db.Configuraciones.ToList());
        }

        // GET: Configuraciones/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Configuracione configuracione = db.Configuraciones.Find(id);
            if (configuracione == null)
            {
                return HttpNotFound();
            }
            return View(configuracione);
        }

        // GET: Configuraciones/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Configuraciones/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Couta_Fija,Mora,Valor_Metro,Valor_Metro2,Valor_Metro3,Multa,Detalle,Estado,Fecha_Registro")] Configuracione configuracione)
        {
            if (ModelState.IsValid)
            {
                db.Configuraciones.Add(configuracione);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(configuracione);
        }

        // GET: Configuraciones/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Configuracione configuracione = db.Configuraciones.Find(id);
            if (configuracione == null)
            {
                return HttpNotFound();
            }
            return View(configuracione);
        }

        // POST: Configuraciones/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Couta_Fija,Mora,Valor_Metro,Valor_Metro2,Valor_Metro3,Multa,Detalle,Estado,Fecha_Registro")] Configuracione configuracione)
        {
            if (ModelState.IsValid)
            {
                db.Entry(configuracione).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(configuracione);
        }

        // GET: Configuraciones/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Configuracione configuracione = db.Configuraciones.Find(id);
            if (configuracione == null)
            {
                return HttpNotFound();
            }
            return View(configuracione);
        }

        // POST: Configuraciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Configuracione configuracione = db.Configuraciones.Find(id);
            db.Configuraciones.Remove(configuracione);
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
