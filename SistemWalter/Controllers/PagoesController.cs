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
    public class PagoesController : Controller
    {
        private SistemadeAguaEntities db = new SistemadeAguaEntities();

        // GET: Pagoes
        public ActionResult Index()
        {
            var pagos = db.Pagos.Include(p => p.Cliente).Include(p => p.Lectura);
            return View(pagos.ToList());
        }

        // GET: Pagoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pago pago = db.Pagos.Find(id);
            if (pago == null)
            {
                return HttpNotFound();
            }
            return View(pago);
        }

        // GET: Pagoes/Create
        public ActionResult Create()
        {
            ViewBag.ClienteId = new SelectList(db.Clientes, "Id", "Nombre_Completo");
            ViewBag.Lectura_Id = new SelectList(db.Lecturas, "Id", "Estado_Lectura");
            return View();
        }

        // POST: Pagoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Lectura_Id,ClienteId,Numero_Factura,Lectura_Anterior,Lectura_Actual,Consumo,Cuota_Fija,Mes_Atrasado,Mora,Total,Fecha_Lectura,Fecha_Pago,Estado,Fecha_Registro")] Pago pago)
        {
            if (ModelState.IsValid)
            {
                db.Pagos.Add(pago);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClienteId = new SelectList(db.Clientes, "Id", "Nombre_Completo", pago.ClienteId);
            ViewBag.Lectura_Id = new SelectList(db.Lecturas, "Id", "Estado_Lectura", pago.Lectura_Id);
            return View(pago);
        }

        // GET: Pagoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pago pago = db.Pagos.Find(id);
            if (pago == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClienteId = new SelectList(db.Clientes, "Id", "Nombre_Completo", pago.ClienteId);
            ViewBag.Lectura_Id = new SelectList(db.Lecturas, "Id", "Estado_Lectura", pago.Lectura_Id);
            return View(pago);
        }

        // POST: Pagoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Lectura_Id,ClienteId,Numero_Factura,Lectura_Anterior,Lectura_Actual,Consumo,Cuota_Fija,Mes_Atrasado,Mora,Total,Fecha_Lectura,Fecha_Pago,Estado,Fecha_Registro")] Pago pago)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pago).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClienteId = new SelectList(db.Clientes, "Id", "Nombre_Completo", pago.ClienteId);
            ViewBag.Lectura_Id = new SelectList(db.Lecturas, "Id", "Estado_Lectura", pago.Lectura_Id);
            return View(pago);
        }

        // GET: Pagoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pago pago = db.Pagos.Find(id);
            if (pago == null)
            {
                return HttpNotFound();
            }
            return View(pago);
        }

        // POST: Pagoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pago pago = db.Pagos.Find(id);
            db.Pagos.Remove(pago);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Asignarpago(int? id)
        {
            if (id== null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadGateway);
            }

            var lectura = db.Lecturas.Find(id);

            if (lectura == null)
            {
                return new HttpNotFoundResult();
            }

            ViewBag.Lectura_Id = new SelectList(db.Lecturas, "Id", "Estado_Lectura", lectura.Id);
            ViewBag.ClienteId = new SelectList(db.Clientes, "Id", "Nombre_Completo", lectura.ClientesId);

            var lec_ant = (from l in db.Lecturas
                           where l.ClientesId == lectura.ClientesId
                           orderby l.Id descending
                           select l.Lectura1).FirstOrDefault();
            return View();
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
