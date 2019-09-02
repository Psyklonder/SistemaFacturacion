using FacturacionElectronica.DA;
using FacturacionElectronica.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacturacionElectronica.BL
{
    public class RolBL
    {
        public static List<Rol> ObtenerTodos()
        {
            try
            {
                DBContext db = new DBContext();

                return db.Rol.ToList();
            }
            catch (Exception error)
            {

                throw;
            }

        }
    }
}
