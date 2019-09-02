namespace FacturacionElectronica.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("seguridad.Empleado")]
    public partial class Empleado
    {
        [Key]
        public int id { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Nombres")]
        public string nombres { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Apellidos")]
        public string apellidos { get; set; }


        [DataType(DataType.Date)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "*")]
        [DisplayFormat(HtmlEncode = false, DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha de nacimiento", Prompt = "{0:yyyy-MM-dd }")]
        [Column(TypeName = "date")]
        public DateTime fechaNacimiento { get; set; }

        [Display(Name = "Foto")]
         public byte[] foto { get; set; }

        public int idUsuario { get; set; }

        public virtual Usuario Usuario { get; set; }
    }
}
