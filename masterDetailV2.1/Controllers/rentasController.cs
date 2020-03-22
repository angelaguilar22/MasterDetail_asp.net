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

            // Datos del detalle de la renta
            ViewBag.idRenta = id;


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
                // Add datos de auditoria para agrear 
                rentas.renta.fechaCreacion = DateTime.Now;
                rentas.renta.fechaModificacion = DateTime.Now;

                var id = db.rentas.Add(rentas.renta).id;
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

        #region METODOS PERSONALIZADOS
        [HttpPost]
        public ActionResult AgregarRenta(rentasModelFinal rentaObject)
        {
            var response = new { ok = true, error = "null" };

            try
            {
                // Creacion de objeto de la renta a agregar
                rentas rentaAdd = new rentas();

                rentaAdd.folio = rentaObject.folio;
                rentaAdd.fechaCreacion = DateTime.Now;
                rentaAdd.fechaModificacion = DateTime.Now;
                rentaAdd.fechaRenta = rentaObject.fechaRenta;
                rentaAdd.fechaVence = rentaObject.fechaVence;
                rentaAdd.total = rentaObject.total;
                rentaAdd.subtotal = rentaObject.subtotal;
                rentaAdd.cantidadTotalProd = rentaObject.listadoProducto.Count;
           
                // Insercion del encabezado de la RENTA
                db.rentas.Add(rentaAdd);
                db.SaveChanges();

                // Obtenemos el ultimo ID de la RENTA para agregar su DETALLE
                var idRenta = db.rentas.ToList().Max(x => x.id);

                // Iteracion para obetener los productos a insertar el DETALLE DE LA RENTA
                foreach (var producto in rentaObject.listadoProducto)
                {
                    // Creacion de objeto de Detalle de la renta
                    rentaDetalle detalleRenta = new rentaDetalle();

                    detalleRenta.idProducto = producto.id;
                    detalleRenta.cantidad = producto.cantidad;
                    detalleRenta.precioUnitario = producto.precioUnitario;
                    detalleRenta.entregado = 0;
                    detalleRenta.idRenta = idRenta;

                    // Seccion de insercion a la tabla rentaDetalle
                    db.rentaDetalle.Add(detalleRenta);
                    db.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                var controlarExcepcion = ex.Message;
            }

            return Json(response, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult getDetallesRecepcion(int idRenta)
        {
            var results = db.rentaDetalle.Select(e => new
            {
                id = e.id,
                idRenta = e.idRenta,
                nombre = e.productos.nombre,
                descripcion = e.productos.descripcion,
                estadoFisico = e.productos.estadoFisico,
                precioUnitario = e.productos.precioUnitario,
                unidadMedida = e.productos.unidadesMedida.nombre,
                numeroSerie = e.productos.numeroSerie,
                categoria = e.productos.tiposProductos.nombre,
                marca = e.productos.marcasProd.nombre,
                entregado = e.entregado,
                cantidad = e.cantidad,
                ds_entregado = e.entregado == 1 ? "Si" : "No"

            }).Where(x => x.idRenta == idRenta).ToList();

            return Json(results, JsonRequestBehavior.AllowGet);
        }

        // Metodo para actulizar el detalle de la renta
        [HttpPost]
        public ActionResult actualizarDetalle(detalleEstructura detalleInput)
        {
            // Obtenemos el detalle de la RENTA
            rentaDetalle detalle = db.rentaDetalle.Find(detalleInput.idDetalleRenta);

            // Modificacmos el detalle 
            detalle.entregado = detalleInput.entregado;

            // Actualizamos el detalle de la renta para saber que ya se regreso
            db.Entry(detalle).State = EntityState.Modified;
            db.SaveChanges();

            return Json(new { ok = true}, JsonRequestBehavior.AllowGet);

        }
        #endregion
    }


}

public struct detalleEstructura
{
    public int idDetalleRenta { get; set; }
    public int entregado { get; set; }
}
