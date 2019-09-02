namespace FacturacionElectronica.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Xml.Serialization;

    [Table("facturacion.DetalleFactura")]
    public partial class DetalleFactura
    {
        [Key]
        public int id { get; set; }

        public int idProducto { get; set; }

        public int idFactura { get; set; }

        public int cantidad { get; set; }

        public double precio { get; set; }

        
        public virtual Factura Factura { get; set; }

        public virtual Producto Producto { get; set; }
    }
}
