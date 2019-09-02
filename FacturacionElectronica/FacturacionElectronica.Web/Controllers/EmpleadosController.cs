using FacturacionElectronica.BL;
using FacturacionElectronica.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace FacturacionElectronica.Web.Controllers
{
    [Filters.AdminAuth]
    public class EmpleadosController : Controller
    {
        IbaseBL _base;
        // GET: Empleados
        public ActionResult Index()
        {
            EmpleadoBL empleadoBD = new EmpleadoBL();

            var _empleado = empleadoBD.ObtenerTodos();
            List<Empleado> result = (_empleado).Cast<Empleado>().ToList();


            return View(result);
        }

        // GET: Empleados/Details/5
        public ActionResult Details(int id)
        {
            _base = new EmpleadoBL();
            Empleado _empleado = (Empleado)_base.ObtenerId(id);
            return View(_empleado);
        }

        // GET: Empleados/Create
        public ActionResult Create()
        {
            Empleado _registro = new Empleado();
            _registro.Usuario = new Usuario();
            _registro.Usuario.Rol = new List<Rol>();
            _registro.Usuario.Rol = RolBL.ObtenerTodos();
            _registro.fechaNacimiento = DateTime.Now;
            return View(_registro);
        }

        // POST: Empleados/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "nombres,apellidos,fechaNacimiento,Usuario")] Empleado registro, FormCollection formCollection)
        {
            try
            {
                var foto1 = Request.InputStream;
                String roleValue = formCollection.Get("roles");

                if (Request.Files.Count > 0)
                {
                    var file1 = Request.Files[0];

                    if (file1 != null)
                    {
                        if (file1.ContentLength == 0)
                        {
                            registro.foto = null;
                        }
                        else
                        {
                            registro.foto = new byte[file1.ContentLength];
                            file1.InputStream.Read(registro.foto, 0, file1.ContentLength);
                        }
                    }

                }


                if (ModelState.IsValid)
                {
                    registro.Usuario.idRol = int.Parse(roleValue);
                    _base = new EmpleadoBL();
                    _base.Guardar(registro);
                    return RedirectToAction("Index");
                }
                else
                {
                    registro = new Empleado();
                    registro.Usuario = new Usuario();
                    registro.Usuario.Rol = new List<Rol>();
                    registro.Usuario.Rol = RolBL.ObtenerTodos();

                    return View(registro);
                }
            }
            catch
            {
                return View(registro);
            }
        }

        // GET: Empleados/Edit/5
        public ActionResult Edit(int id)
        {
            _base = new EmpleadoBL();
            Empleado _empleado = (Empleado)_base.ObtenerId(id);
            return View(_empleado);
        }

        // POST: Empleados/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "id, Usuario,idUsuario, nombres, apellidos,fechaNacimiento,Usuario")] Empleado registro)
        {
            try
            {

          
                _base = new EmpleadoBL();

                _base.Editar(registro);
                return RedirectToAction("Index");


            }
            catch (Exception error)
            {
                return View(registro);
            }
        }

        // GET: Empleados/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Empleados/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
