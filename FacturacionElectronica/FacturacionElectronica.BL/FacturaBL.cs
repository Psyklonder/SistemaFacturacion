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
    public class FacturaBL : IbaseBL
    {
        DBContext db;
        public FacturaBL()
        {
            db = new DBContext();
        }
        public void Borrar(int id)
        {
            try
            {
                var _registro = db.Factura.Find(id);
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
                var _registro = (Factura)registro;
                _registro.Usuario = null;
                _registro.Cliente = null;
                _registro.fechaCompra = DateTime.Now;

                var _registroBD = db.Factura.Include("DetalleFactura").Where(x => x.id == _registro.id).FirstOrDefault();
                _registroBD.valorTotal = _registro.valorTotal;
                _registroBD.facturado = _registro.facturado;
                _registroBD.fechaCompra = DateTime.Now;
                db.Entry(_registroBD).State = EntityState.Modified;
                db.SaveChanges();
                return _registroBD;
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
                var _registro = (Factura)registro;
                _registro.fechaCompra = DateTime.Now;
                db.Entry(_registro).State = EntityState.Added;
                db.SaveChanges();
                return db.Factura.Include("Usuario").Include("Cliente").Where(x => x.id == ((Factura)registro).id).FirstOrDefault();
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
                return db.Factura.Find(id);

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
                return db.Factura.Include("Cliente").Include("Usuario").OrderByDescending(x => x.fechaCompra).ToList<object>();
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        static public List<object> ObtenerPorCliente(int idCliente)
        {
            try
            {
                DBContext db = new DBContext();
                return db.Factura.Where(x => x.idCliente == idCliente).OrderByDescending(x => x.fechaCompra).ToList<object>();
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        static public List<object> ObtenerPorVendedor(int idVendedor)
        {
            try
            {
                DBContext db = new DBContext();
                return db.Factura.Where(x => x.idVendedor == idVendedor).OrderByDescending(x => x.fechaCompra).ToList<object>();
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        static public void Facturar(int id)
        {
            try
            {
                DBContext db = new DBContext();
                var _factura = db.Factura.Find(id);
                _factura.facturado = true;
                db.Entry(_factura).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception error)
            {

                throw error;
            }
        }
    }
}
