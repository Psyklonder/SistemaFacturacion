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
    public class UsuarioBL : IbaseBL
    {
        DBContext db;
        public UsuarioBL()
        {
            db = new DBContext();
        }

        public Usuario IniciarSesion(Usuario registro)
        {
            try
            {
                var _usuarios = db.Usuario.ToList();

                string _password = Utilidades.GenerarHash(registro.password.Trim());
               // _password = registro.password;
                var usuario = db.Usuario.Where(x => x.userName == registro.userName.Trim() && x.password == _password && x.habilitado).FirstOrDefault();
                if (usuario != null)
                {
                    return usuario;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public void Borrar(int id)
        {
            try
            {
                var usuario = db.Usuario.Find(id);
                if (usuario != null)
                {
                    usuario.habilitado = false;
                    db.Entry(usuario).State = EntityState.Modified;
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
                Usuario _usuario = ((Empleado)registro).Usuario;

                db.Entry(_usuario).State = EntityState.Modified;
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
            var entity = (Usuario)registro;
            db.Usuario.Add(entity);
            db.SaveChanges();
            return entity;
        }

        public object ObtenerId(int id)
        {
            throw new NotImplementedException();
        }

        public List<object> ObtenerTodos()
        {
            throw new NotImplementedException();
        }
    }
}
