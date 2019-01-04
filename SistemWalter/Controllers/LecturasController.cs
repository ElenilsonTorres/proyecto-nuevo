﻿using System;
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
    public class LecturasController : Controller
    {
        private SistemadeAguaEntities db = new SistemadeAguaEntities();

        // GET: Lecturas
        public ActionResult Index()
        {
            var lecturas = db.Lecturas.Include(l => l.Cliente);
            return View(lecturas.ToList());
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