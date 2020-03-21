using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace masterDetailV2._1.Models
{
    public class rentasModel
    {
        public int id { get; set; }
        public string folio { get; set; }
        public System.DateTime fechaCreacion { get; set; }
        public System.DateTime fechaModificacion { get; set; }
        public System.DateTime fechaRenta { get; set; }
        public System.DateTime fechaVence { get; set; }
        public decimal subtotal { get; set; }
        public decimal total { get; set; }
        public int cantidadTotalProd { get; set; }
        public int idEstatus { get; set; }
        public int idUSuario { get; set; }
        public int idCliente { get; set; }
        public Nullable<int> idDescuento { get; set; }

        public virtual clientes clientes { get; set; }
        public virtual clientes clientes1 { get; set; }
        public virtual descuentos descuentos { get; set; }
        public virtual descuentos descuentos1 { get; set; }
        public virtual estatusRenta estatusRenta { get; set; }
        public virtual estatusRenta estatusRenta1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<rentaDetalle> rentaDetalle { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<rentaDetalle> rentaDetalle1 { get; set; }
        public virtual usuarios usuarios { get; set; }
        public virtual usuarios usuarios1 { get; set; }
    }
}