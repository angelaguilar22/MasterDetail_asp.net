using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace masterDetailV2._1.Models
{
    public class rentasModelFinal
    {
        // Datos del encabezado de la renta
        public int id { get; set; }
        public string folio { get; set; }
        public System.DateTime fechaCreacion { get; set; }
        public System.DateTime fechaModificacion { get; set; }
        public System.DateTime fechaRenta { get; set; }
        public System.DateTime fechaVence { get; set; }
        public decimal subtotal { get; set; }
        public decimal total { get; set; }
        public int cantidadTotalProd { get; set; }


        // Datos del Detalle de la renta
        public List<productosEstructuraModel> listadoProducto { get; set; }

    }
}