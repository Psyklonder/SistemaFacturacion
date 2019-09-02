namespace FacturacionElectronica.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Xml.Serialization;

    [Table("facturacion.Cliente")]
    public partial class Cliente
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Cliente()
        {
            Factura = new HashSet<Factura>();
        }
        [Key]
        public int id { get; set; }

        [Display(Name = "Nombres")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "*")]
        [StringLength(50)]
        public string nombres { get; set; }

        [Display(Name = "Apellidos")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "*")]
        [StringLength(50)]
        public string apellidos { get; set; }

        [Display(Name = "No. documento")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "*")]
        [StringLength(50)]
        public string documento { get; set; }


        [DataType(DataType.Date)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "*")]
        [DisplayFormat(HtmlEncode = false, DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha de nacimiento", Prompt = "{0:yyyy-MM-dd }")]
        [Column(TypeName = "date")]
        public DateTime fechaNacimiento { get; set; }

        [Display(Name = "Dirección")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "*")]
        [StringLength(50)]
        public string direccion { get; set; }

        [Display(Name = "Teléfono")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "*")]
        [StringLength(50)]
        public string telefono { get; set; }

        [Display(Name = "Correo electónico")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "*")]
        [StringLength(100)]
        public string email { get; set; }

        public int idCiudad { get; set; }

        public virtual Ciudad Ciudad { get; set; }

        [NotMapped]
        public List<Ciudad> Ciudades { get; set; }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Factura> Factura { get; set; }
    }
}
