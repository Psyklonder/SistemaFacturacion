namespace FacturacionElectronica.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("facturacion.Factura")]
    public partial class Factura
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Factura()
        {
            DetalleFactura = new HashSet<DetalleFactura>();
        }

        [Key]
        public int id { get; set; }

        public int idCliente { get; set; }

        public int idVendedor { get; set; }

        public double valorTotal { get; set; }

        public DateTime? fechaCompra { get; set; }

        public bool facturado { get; set; }

        public virtual Cliente Cliente { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DetalleFactura> DetalleFactura { get; set; }

        public virtual Usuario Usuario { get; set; }

        [NotMapped]
        public List<Producto> Productos { get; set; }
    }
}
