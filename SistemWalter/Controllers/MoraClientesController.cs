using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SistemWalter.Context;
using SistemWalter.ViewModels;

namespace SistemWalter.Controllers
{
    public class MoraClientesController : Controller
    {
        private Sistemadeagua5Entities db = new Sistemadeagua5Entities();

        // GET: MoraClientes
        public ActionResult Index()
        {
            var moraClientes = (from m in db.MoraClientes
                                where m.Estado == 1
                                orderby m.Meses descending
                                select m);
            return View(moraClientes.ToList());
        }


        public ActionResult GenerarMora()
        {
            var pagos = (from p in db.Pagos
                         where p.Estado == 1
                         orderby p.Total descending
                         select p).ToList();

            var pagosMora = (from p in db.Pagos
                             join m in db.MoraClientes on p.ClienteId equals m.ClienteId
                             where m.Estado == 1
                             select m).ToList();

            var pagos_pendientes = new List<PagosView>();

            foreach (var item in pagos)
            {
                var pago = new PagosView
                {
                    Id = item.Id,
                    Lectura_Id = item.Lectura_Id,
                    ClienteId = item.ClienteId,
                    Numero_Factura = Convert.ToInt32(item.Numero_Factura),
                    Lectura_Anterior = item.Lectura_Anterior,
                    Lectura_Actual = item.Lectura_Actual,
                    Consumo = item.Consumo,
                    Cuota_Fija = item.Cuota_Fija,
                    Mes_Atrasado = item.Mes_Atrasado,
                    Mora = item.Mora,
                    Total = item.Total,
                    Fecha_Lectura = item.Fecha_Lectura,
                    Fecha_Pago = Convert.ToDateTime(item.Fecha_Pago),
                    Estado = item.Estado,
                    Fecha_Registro = item.Fecha_Registro,
                    NombreCliente = item.Cliente.Nombre_Completo
                };

                var existe = (from p in pagos_pendientes
                              where p.ClienteId == item.ClienteId
                              select p).FirstOrDefault();

                var existeMora = (from m in pagosMora
                                  where m.Idpago == item.Id && m.Estado == 1
                                  select m).FirstOrDefault();
                if (existe == null && existeMora == null)
                {
                    pagos_pendientes.Add(pago);
                }
            }

            var pagoMora = (from p in db.Configuraciones
                            select p.Mora).FirstOrDefault();

            foreach (var pago in pagos_pendientes)
            {
                var moraCliente = (from m in db.MoraClientes
                                   where m.ClienteId == pago.ClienteId && m.Estado == 1
                                   select m).FirstOrDefault();
                if(moraCliente != null)
                {
                    int meses = Convert.ToInt32(moraCliente.Meses);
                    moraCliente.Meses = meses + 1;
                    db.Entry(moraCliente).State = EntityState.Modified;
                    db.SaveChanges();
                }
                else
                {
                    MoraCliente mora = new MoraCliente();
                    mora.ClienteId = pago.ClienteId;
                    mora.Meses = 1;
                    mora.Total = pagoMora;
                    mora.Estado = 1;
                    mora.Fecha_Registro = DateTime.Now;
                    mora.Idpago = pago.Id;

                    db.MoraClientes.Add(mora);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index");
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

        public ActionResult CrearMora(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var cliente = db.Clientes.Find(id);
            ViewBag.ClienteId = cliente.Id;
            ViewBag.NombreCliente = cliente.Nombre_Completo;
            return View("Create");
        }

        [HttpPost]
        public ActionResult crearMora([Bind(Include = "Id,ClienteId,Meses,Total")] MoraCliente moraCliente)
        {
            if (ModelState.IsValid)
            {
                var existe = (from m in db.MoraClientes
                              where m.ClienteId == moraCliente.ClienteId
                              select m).FirstOrDefault();

                if(existe == null)
                {
                    var mora = (from m in db.Configuraciones
                                select m.Mora).FirstOrDefault();

                    moraCliente.Total = mora;
                    moraCliente.Fecha_Registro = DateTime.Now;
                    moraCliente.Estado = 0;
                    db.MoraClientes.Add(moraCliente);
                    db.SaveChanges();
                }
                
                return RedirectToAction("Index");
            }

            ViewBag.ClienteId = new SelectList(db.Clientes, "Id", "Nombre_Completo", moraCliente.ClienteId);
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
                var mora = (from m in db.Configuraciones
                            select m.Mora).FirstOrDefault();

                moraCliente.Total = mora;
                moraCliente.Fecha_Registro = DateTime.Now;
                moraCliente.Estado = 0;
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
                var mora = (from m in db.Configuraciones
                            select m.Mora).FirstOrDefault();

                moraCliente.Total = mora;

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
