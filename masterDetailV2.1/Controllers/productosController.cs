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
    public class productosController : Controller
    {
        private RentaCamionesEntities db = new RentaCamionesEntities();

        // GET: productos
        public ActionResult Index()
        {
            var productos = db.productos.Include(p => p.marcasProd).Include(p => p.marcasProd1).Include(p => p.tiposProductos).Include(p => p.tiposProductos1).Include(p => p.unidadesMedida).Include(p => p.unidadesMedida1);
            return View(productos.ToList());
        }

        // GET: productos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            productos productos = db.productos.Find(id);
            if (productos == null)
            {
                return HttpNotFound();
            }
            return View(productos);
        }

        // GET: productos/Create
        public ActionResult Create()
        {
            ViewBag.idMarca = new SelectList(db.marcasProd, "id", "nombre");
            ViewBag.idMarca = new SelectList(db.marcasProd, "id", "nombre");
            ViewBag.idTipoProd = new SelectList(db.tiposProductos, "id", "nombre");
            ViewBag.idTipoProd = new SelectList(db.tiposProductos, "id", "nombre");
            ViewBag.idUM = new SelectList(db.unidadesMedida, "id", "nombre");
            ViewBag.idUM = new SelectList(db.unidadesMedida, "id", "nombre");
            return View();
        }

        // POST: productos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nombre,descripcion,numeroSerie,estadoFisico,numeroExistencias,estatus,precioUnitario,idUM,idMarca,idTipoProd")] productos productos)
        {
            if (ModelState.IsValid)
            {
                db.productos.Add(productos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idMarca = new SelectList(db.marcasProd, "id", "nombre", productos.idMarca);
            ViewBag.idMarca = new SelectList(db.marcasProd, "id", "nombre", productos.idMarca);
            ViewBag.idTipoProd = new SelectList(db.tiposProductos, "id", "nombre", productos.idTipoProd);
            ViewBag.idTipoProd = new SelectList(db.tiposProductos, "id", "nombre", productos.idTipoProd);
            ViewBag.idUM = new SelectList(db.unidadesMedida, "id", "nombre", productos.idUM);
            ViewBag.idUM = new SelectList(db.unidadesMedida, "id", "nombre", productos.idUM);
            return View(productos);
        }

        // GET: productos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            productos productos = db.productos.Find(id);
            if (productos == null)
            {
                return HttpNotFound();
            }
            ViewBag.idMarca = new SelectList(db.marcasProd, "id", "nombre", productos.idMarca);
            ViewBag.idMarca = new SelectList(db.marcasProd, "id", "nombre", productos.idMarca);
            ViewBag.idTipoProd = new SelectList(db.tiposProductos, "id", "nombre", productos.idTipoProd);
            ViewBag.idTipoProd = new SelectList(db.tiposProductos, "id", "nombre", productos.idTipoProd);
            ViewBag.idUM = new SelectList(db.unidadesMedida, "id", "nombre", productos.idUM);
            ViewBag.idUM = new SelectList(db.unidadesMedida, "id", "nombre", productos.idUM);
            return View(productos);
        }

        // POST: productos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nombre,descripcion,numeroSerie,estadoFisico,numeroExistencias,estatus,precioUnitario,idUM,idMarca,idTipoProd")] productos productos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idMarca = new SelectList(db.marcasProd, "id", "nombre", productos.idMarca);
            ViewBag.idMarca = new SelectList(db.marcasProd, "id", "nombre", productos.idMarca);
            ViewBag.idTipoProd = new SelectList(db.tiposProductos, "id", "nombre", productos.idTipoProd);
            ViewBag.idTipoProd = new SelectList(db.tiposProductos, "id", "nombre", productos.idTipoProd);
            ViewBag.idUM = new SelectList(db.unidadesMedida, "id", "nombre", productos.idUM);
            ViewBag.idUM = new SelectList(db.unidadesMedida, "id", "nombre", productos.idUM);
            return View(productos);
        }

        // GET: productos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            productos productos = db.productos.Find(id);
            if (productos == null)
            {
                return HttpNotFound();
            }
            return View(productos);
        }

        // POST: productos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            productos productos = db.productos.Find(id);
            db.productos.Remove(productos);
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


        #region METODOS PERSONALIZADOS
        [HttpGet]
        public JsonResult GET_PRODUCT_BY_ID(int id)
        {

            var results = db.productos.Select(e => new
            {
                id = e.id,
                nombre = e.nombre,
                descripcion = e.descripcion,
                estadoFisico = e.estadoFisico,
                precioUnitario = e.precioUnitario,
                unidadMedida = e.unidadesMedida.nombre,
                numeroSerie = e.numeroSerie,
                categoria = e.tiposProductos.nombre,
                marca = e.marcasProd.nombre
           
            }).Where(x => x.id == id).First();

            return Json(results, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
