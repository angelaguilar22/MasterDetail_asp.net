using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace masterDetailV2._1.Models
{
    public class productosEstructuraModel
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public string estadoFisico { get; set; }
        public decimal precioUnitario { get; set; }
        public string unidadMedida { get; set; }
        public string numeroSerie { get; set; }
        public string categoria { get; set; }
        public string marca { get; set; }
        public int cantidad { get; set; }
        public decimal subtotal { get; set; }
        public decimal total { get; set; }
    }
}