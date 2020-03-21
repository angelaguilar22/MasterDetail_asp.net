using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using masterDetailV2._1.Models;

namespace masterDetailV2._1.Controllers
{
    public class rentasController : Controller
    {
        private RentaCamionesEntities db = new RentaCamionesEntities();

        // GET: rentas
        public ActionResult Index()
        {
            var rentas = db.rentas;
            return View(rentas.ToList());
        }

        // GET: rentas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            rentas rentas = db.rentas.Find(id);
            if (rentas == null)
            {
                return HttpNotFound();
            }
            return View(rentas);
        }

        // GET: rentas/Create
        public ActionResult Create()
        {
            ViewBag.idProducto = new SelectList(db.productos, "id", "nombre");
            
            return View();
        }

        // POST: rentas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RentasDetailsAndHeader rentas)
        {
            if (ModelState.IsValid)
            {
                db.rentas.Add(rentas.renta);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

       
            return View(rentas);
        }

        // GET: rentas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            rentas rentas = db.rentas.Find(id);
            if (rentas == null)
            {
                return HttpNotFound();
            }
            
            return View(rentas);
        }

        // POST: rentas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,folio,fechaCreacion,fechaModificacion,fechaRenta,fechaVence,subtotal,total,cantidadTotalProd,idEstatus,idUSuario,idCliente,idDescuento")] rentas rentas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rentas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(rentas);
        }

        // GET: rentas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            rentas rentas = db.rentas.Find(id);
            if (rentas == null)
            {
                return HttpNotFound();
            }
            return View(rentas);
        }

        // POST: rentas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            rentas rentas = db.rentas.Find(id);
            db.rentas.Remove(rentas);
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
