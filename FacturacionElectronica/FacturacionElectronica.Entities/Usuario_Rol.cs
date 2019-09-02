using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacturacionElectronica.Entities
{
  
    [Table("seguridad.Usuario_Rol")]
    public class Usuario_Rol
    {
        [Key, Column(Order = 1)]
        public int idUsuario { get; set; }
        [Key, Column(Order = 2)]
        public int idRol { get; set; }

        public Usuario Usuario { get; set; }
        public Rol Rol { get; set; }
    }
}
