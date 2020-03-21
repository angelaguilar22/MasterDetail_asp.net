using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace masterDetailV2._1.Models
{
    public class RentasDetailsAndHeader
    {
        public productos producto { get; set; }
        public rentas renta { get; set; }
        public rentaDetalle detalleRenta { get; set; }

        public List<productos> listaProductos { get; set; }
    }
}