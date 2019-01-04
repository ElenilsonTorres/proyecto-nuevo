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
            var pagos = (from p in db.Pagos 
                         where p.Estado== 1
                         select p);
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
        public ActionResult Edit([Bind(Include = "Id,Lectura_Id,ClienteId,Numero_Factura,Lectura_Anterior,Lectura_Actual,Consumo,Cuota_Fija,Mes_Atrasado,Mora,Total,Fecha_Lectura,Fecha_Pago,Estado,Fecha_Registro")] Pago pago, string Estad)
        {
            if (ModelState.IsValid)
            {
                if(Estad == "1")
                {
                    pago.Estado = 1;

                }
                else
                {
                    pago.Estado = 0;
                }
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
            if (id == null)
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

            var ultimas_lecturas = (from l in db.Lecturas
                                    where l.ClientesId == lectura.ClientesId
                                    orderby l.Id descending
                                    select l.Lectura1).Take(2).ToList();

            var lec_ant = ultimas_lecturas[1];

            var cuota_fija = (from c in db.Configuraciones
                              select c.Couta_Fija).FirstOrDefault();

            var metro1 = (from c in db.Configuraciones
                          select c.Valor_Metro).FirstOrDefault();

            var metro2 = (from c in db.Configuraciones
                          select c.Valor_Metro2).FirstOrDefault();

            var metro3 = (from c in db.Configuraciones
                          select c.Valor_Metro3).FirstOrDefault();


            var consumo = lectura.Lectura1 - lec_ant;

            var moras = (from m in db.MoraClientes
                         where m.ClienteId == lectura.ClientesId && m.Estado==0
                         select m.Total).Sum();

            var pagopendiente = (from p in db.Pagos
                                 where p.ClienteId == lectura.ClientesId && p.Estado == 1
                                 select p.Total).Sum();
            decimal total = 0;

            if (consumo < 13)
            {
                if (pagopendiente != null)
                {
                    total = Convert.ToDecimal((consumo * metro1) + moras + pagopendiente + cuota_fija);
                }

                else
                {
                    total = Convert.ToDecimal((consumo * metro1) + moras + cuota_fija);
                }
                    
            }

            else if (consumo >= 13 && consumo < 20)
            {
                if (pagopendiente != null)
                {
                    total = Convert.ToDecimal((consumo * metro2) + moras + pagopendiente + cuota_fija);
                }

                else
                {
                    total = Convert.ToDecimal((consumo * metro2) + moras + cuota_fija);
                }
            }

            else if (consumo >= 20)
            {
                if (pagopendiente != null)
                {
                    total = Convert.ToDecimal((consumo * metro3) + moras + pagopendiente + cuota_fija);
                }

                else
                {
                    total = Convert.ToDecimal((consumo * metro3) + moras + cuota_fija);
                }
            }
            
            Pago pago = new Pago();

            if (pagopendiente != null)
            {
                pago.Mes_Atrasado = pagopendiente;
            }
            pago.Lectura_Anterior = lec_ant;
            pago.Lectura_Actual = lectura.Lectura1;
            pago.Consumo = lectura.Lectura1 - lec_ant;
            pago.Mora = moras;
            pago.Total = total;
            pago.Cuota_Fija = cuota_fija;
            pago.Fecha_Lectura = lectura.Fecha_Registro;
            return View("Create", pago);
        }
        [HttpPost]
        public ActionResult Asignarpago(Pago pagos)
        {
            try
            {
                pagos.Estado = 1;
                pagos.Fecha_Registro = DateTime.Now;
                db.Pagos.Add(pagos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {

                throw;
            }
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
