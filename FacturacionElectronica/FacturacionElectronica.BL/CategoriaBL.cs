using FacturacionElectronica.DA;
using FacturacionElectronica.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacturacionElectronica.BL
{
    public class CategoriaBL
    {
        public CategoriaBL()
        {

        }

        public static List<Categoria> ObtenerTodos()
        {
            try
            {
                DBContext db = new DBContext();
                return db.Categoria.ToList();
            }
            catch (Exception error)
            {
                throw error;
            }

        }
    
    }
}
