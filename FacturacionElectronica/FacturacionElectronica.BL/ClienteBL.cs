using FacturacionElectronica.DA;
using FacturacionElectronica.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacturacionElectronica.BL
{
    public class ClienteBL : IbaseBL
    {
        DBContext db;
        public ClienteBL()
        {
            db = new DBContext();
        }
        public void Borrar(int id)
        {
            try
            {
                var _registro = db.Cliente.Find(id);
                if (_registro != null)
                {
                    db.Entry(_registro).State = EntityState.Deleted;
                    db.SaveChanges();
                }
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public object Editar(object registro)
        {
            try
            {
                db.Entry((Cliente)registro).State = EntityState.Modified;
                db.SaveChanges();
                return registro;
            }
            catch (Exception error)
            {
                throw error;
            }

        }

        public object Guardar(object registro)
        {
            try
            {
                var _cliente = (Cliente)registro;
                db.Cliente.Add(_cliente);
                db.SaveChanges();
                return _cliente;
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public object ObtenerId(int id)
        {
            try
            {
                return db.Cliente.Find(id);

            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public List<object> ObtenerTodos()
        {
            try
            {
                return db.Cliente.ToList<object>();
            }
            catch (Exception error)
            {
                throw error;
            }
        }
    }
}
