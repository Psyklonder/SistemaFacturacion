using FacturacionElectronica.DA;
using FacturacionElectronica.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacturacionElectronica.BL
{
    public class CiudadBL
    {


        public CiudadBL()
        {

        }

        public static List<Ciudad> ObtenerTodos()
        {
            try
            {
                DBContext db = new DBContext();
                return db.Ciudad.ToList();
            }
            catch (Exception error)
            {

                throw;
            }

        }
        public static Ciudad ObtenerId(int id)
        {
            try
            {
                DBContext db = new DBContext();
                return db.Ciudad.Find(id);
            }
            catch (Exception error)
            {

                throw error;
            }

        }
    }
}
