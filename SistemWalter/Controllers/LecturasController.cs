using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SistemWalter.Context;
using SistemWalter.ViewModels;

namespace SistemWalter.Controllers
{
    public class LecturasController : Controller
    {
        private SistemadeAguaEntities db = new SistemadeAguaEntities();

        // GET: Lecturas
        public ActionResult Index()
        {
            var Todas_lecturas = db.Lecturas.ToList();

            var lecturas_confactura = (from l in db.Lecturas
                                       join p in db.Pagos on l.Id equals p.Lectura_Id
                                       select l).ToList();

            var facturas_pendientes = Todas_lecturas.Except(lecturas_confactura);

            List<LecturasView> lecturasView = new List<LecturasView>();

            foreach (var item in facturas_pendientes)
            {
                var factura = new LecturasView
                {
                    Id = item.Id,
                    Lectura1 = item.Lectura1,
                    Estado_Lectura = item.Estado_Lectura,
                    Estado = item.Estado,
                    Fecha_Registro = item.Fecha_Registro,
                    Mes = nombreMes(Convert.ToInt32(item.Mes)),
                    ClientesId = item.ClientesId,
                    NombreCliente = item.Cliente.Nombre_Completo
                };

                lecturasView.Add(factura);
            }


                                       
            return View(lecturasView.ToList());
        }

        [HttpPost]
        public ActionResult Index(string Meses, string parametro)
        {
            int mes = int.Parse(Meses);
            var lect = (from c in db.Lecturas
                            where c.Cliente.Nombre_Completo.Contains(parametro) && c.Fecha_Registro.Value.Month == mes
                            select c).ToList();

            List<LecturasView> lecturasView = new List<LecturasView>();

            foreach (var item in lect)
            {
                var factura = new LecturasView
                {
                    Id = item.Id,
                    Lectura1 = item.Lectura1,
                    Estado_Lectura = item.Estado_Lectura,
                    Estado = item.Estado,
                    Fecha_Registro = item.Fecha_Registro,
                    Mes = nombreMes(Convert.ToInt32(item.Mes)),
                    ClientesId = item.ClientesId,
                    NombreCliente = item.Cliente.Nombre_Completo
                };

                lecturasView.Add(factura);
            }
            return View(lecturasView);
        }

        // GET: Lecturas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lectura lectura = db.Lecturas.Find(id);
            if (lectura == null)
            {
                return HttpNotFound();
            }
            return View(lectura);
        }

        // GET: Lecturas/Create
        public ActionResult Create()
        {
            ViewBag.ClientesId = new SelectList(db.Clientes, "Id", "Nombre_Completo");
            return View();
        }

        // POST: Lecturas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Lectura1,Estado_Lectura,Estado,Fecha_Registro,Mes,ClientesId")] Lectura lectura)
        {
            if (ModelState.IsValid)
            {
                lectura.Estado_Lectura = "Actual";
                lectura.Estado = 1;
                lectura.Mes = DateTime.Now.Month;
                db.Lecturas.Add(lectura);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClientesId = new SelectList(db.Clientes, "Id", "Nombre_Completo", lectura.ClientesId);
            return View(lectura);
        }

        // GET: Lecturas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lectura lectura = db.Lecturas.Find(id);
            if (lectura == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClientesId = new SelectList(db.Clientes, "Id", "Nombre_Completo", lectura.ClientesId);
            return View(lectura);
        }

        // POST: Lecturas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Lectura1,Estado_Lectura,Estado,Fecha_Registro,Mes,ClientesId")] Lectura lectura)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lectura).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClientesId = new SelectList(db.Clientes, "Id", "Nombre_Completo", lectura.ClientesId);
            return View(lectura);
        }

        // GET: Lecturas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lectura lectura = db.Lecturas.Find(id);
            if (lectura == null)
            {
                return HttpNotFound();
            }
            return View(lectura);
        }

        // POST: Lecturas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Lectura lectura = db.Lecturas.Find(id);
            db.Lecturas.Remove(lectura);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        private string nombreMes(int numeroMes)
        {
            try
            {
                DateTimeFormatInfo formatoFecha = new DateTimeFormatInfo();
                string nombreMes = formatoFecha.GetMonthName(numeroMes);
                return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(nombreMes);
            }
            catch (Exception)
            {

                return "Desconocido";
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
