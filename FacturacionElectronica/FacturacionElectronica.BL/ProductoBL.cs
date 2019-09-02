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
    public class ProductoBL : IbaseBL
    {
        
        DBContext db;
        public ProductoBL()
        {
            db = new DBContext();
        }
        public void Borrar(int id)
        {
            try
            {
                var _registro = db.Producto.Find(id);
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
                db.Entry((Producto)registro).State = EntityState.Modified;
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
                var _registro = (Producto)registro;
                db.Entry(_registro).State = EntityState.Added;
                db.SaveChanges();
                return _registro;
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
                return db.Producto.Find(id);

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
                return db.Producto.ToList<object>();
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        static public List<object> ObtenerPorCategoria(int idCategoria)
        {
            try
            {
                DBContext db = new DBContext();
                return db.Producto.Where(x => x.idCategoria == idCategoria).ToList<object>();
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public bool ExistenFacturas(int idProducto)
        {
            DetalleFacturaBL _detalle = new DetalleFacturaBL();
            return _detalle.Existenproductos(idProducto);
        }

    }
}
