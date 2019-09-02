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
    public class EmpleadoBL : IbaseBL
    {
        DBContext db;
        public EmpleadoBL()
        {
            db = new DBContext();
        }
        public void Borrar(int id)
        {
            try
            {
                var empleado = db.Empleado.Find(id);
                if (empleado != null)
                {
                    UsuarioBL usuario = new UsuarioBL();
                    usuario.Borrar(empleado.idUsuario);
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
                Usuario _usuario = new Usuario();
                _usuario = ((Empleado)registro).Usuario;
                db.Entry(_usuario).State = EntityState.Modified;
                db.SaveChanges();
                return (Empleado)registro;
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
                var _empleado = (Empleado)registro;
                IbaseBL _base = new UsuarioBL();

                _empleado.Usuario.fechaCreacion = DateTime.Now;
                _empleado.Usuario.password= Utilidades.GenerarHash(_empleado.Usuario.password);
                var _usuario = (Usuario)_base.Guardar(_empleado.Usuario);
                
                db.Usuario.Add(_usuario);
                db.Usuario.Attach(_usuario);

                var _rol = new Rol { id = _usuario.idRol };
                db.Rol.Add(_rol);
                db.Rol.Attach(_rol);
                _usuario.Rol.Add(_rol);

                db.SaveChanges();


                _empleado.idUsuario = _usuario.id;

                db.Empleado.Add(_empleado);
                db.SaveChanges();
                return registro;
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
                return db.Empleado.Find(id);

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
                var _usuarioRol = new List<Usuario_Rol>();

                //_usuarioRol = db.Usuario_Roles.ToList();
                return db.Empleado.ToList<object>();
            }
            catch (Exception error)
            {
                throw error;
            }
        }
    }
}
