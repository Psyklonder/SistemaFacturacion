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
    public class DetalleFacturaBL : IbaseBL
    {
        DBContext db;
        public DetalleFacturaBL()
        {
            db = new DBContext();
        }
        public void Borrar(int id)
        {
            try
            {
                var _registro = db.DetalleFactura.Find(id);
                if (_registro != null)
                {
                    //RETORNAR LAS CANTIDADES A LOS PRODUCTOS
                    var _producto = db.Producto.Find(_registro.Producto.id);
                    _producto.cantidad += _registro.cantidad;
                    db.Entry(_producto).State = EntityState.Modified;
                    db.SaveChanges();
                    //BORRANDO EL REGISTRO DE DETALLE
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
            throw new NotImplementedException();
        }

        public object Guardar(object registro)
        {
            try
            {
                var _registro = (DetalleFactura)registro;
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
                return db.DetalleFactura.Find(id);

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
                return db.DetalleFactura.ToList<object>();
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public bool Existenproductos(int idProducto)
        {
            if (db.DetalleFactura.Where(x => x.idProducto == idProducto).FirstOrDefault() != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
