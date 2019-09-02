using FacturacionElectronica.DA;
using FacturacionElectronica.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace FacturacionElectronica.BL
{
    public class PaisBL
    {
        public static List<Pais> ObtenerTodos()
        {
            try
            {
                DBContext db = new DBContext();

                return db.Pais.ToList();
            }
            catch (Exception error)
            {

                throw;
            }
           
        }
    }
}
