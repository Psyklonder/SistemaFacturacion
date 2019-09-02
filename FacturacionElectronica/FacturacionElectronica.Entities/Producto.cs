namespace FacturacionElectronica.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("facturacion.Producto")]
    public partial class Producto
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Producto()
        {
            DetalleFactura = new HashSet<DetalleFactura>();
        }
        [Key]
        public int id { get; set; }

        [Display(Name = "Nombre")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "*")]
        [StringLength(500)]
        public string nombre { get; set; }

        [StringLength(5000)]
        public string descripcion { get; set; }

        [Display(Name = "Precio/Unidad")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "*")]
        [DisplayFormat(DataFormatString = "{0:#.####}")]
        public double precio { get; set; }

        public bool habilitado { get; set; }

        [Display(Name = "En stock")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "*")]
        public int cantidad { get; set; }

        public int idCategoria { get; set; }

        public string foto { get; set; }

        public virtual Categoria Categoria { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DetalleFactura> DetalleFactura { get; set; }

        [NotMapped]
        public List<Categoria> Categorias { get; set; }

        [NotMapped]
        public bool Seleccionado { get; set; }
        [NotMapped]
        public int cantidadSeleccionada { get; set; }
    }
}
