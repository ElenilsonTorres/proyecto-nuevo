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
    public class MoraClientesController : Controller
    {
        private SistemadeAguaEntities db = new SistemadeAguaEntities();

        // GET: MoraClientes
        public ActionResult Index()
        {
            var moraClientes = db.MoraClientes.Include(m => m.Cliente);
            return View(moraClientes.ToList());
        }

        // GET: MoraClientes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MoraCliente moraCliente = db.MoraClientes.Find(id);
            if (moraCliente == null)
            {
                return HttpNotFound();
            }
            return View(moraCliente);
        }

        // GET: MoraClientes/Create
        public ActionResult Create()
        {
            ViewBag.ClienteId = new SelectList(db.Clientes, "Id", "Nombre_Completo");
            return View();
        }

        // POST: MoraClientes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ClienteId,Meses,Total")] MoraCliente moraCliente)
        {
            if (ModelState.IsValid)
            {
                db.MoraClientes.Add(moraCliente);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClienteId = new SelectList(db.Clientes, "Id", "Nombre_Completo", moraCliente.ClienteId);
            return View(moraCliente);
        }

        // GET: MoraClientes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MoraCliente moraCliente = db.MoraClientes.Find(id);
            if (moraCliente == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClienteId = new SelectList(db.Clientes, "Id", "Nombre_Completo", moraCliente.ClienteId);
            return View(moraCliente);
        }

        // POST: MoraClientes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ClienteId,Meses,Total")] MoraCliente moraCliente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(moraCliente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClienteId = new SelectList(db.Clientes, "Id", "Nombre_Completo", moraCliente.ClienteId);
            return View(moraCliente);
        }

        // GET: MoraClientes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MoraCliente moraCliente = db.MoraClientes.Find(id);
            if (moraCliente == null)
            {
                return HttpNotFound();
            }
            return View(moraCliente);
        }

        // POST: MoraClientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MoraCliente moraCliente = db.MoraClientes.Find(id);
            db.MoraClientes.Remove(moraCliente);
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
