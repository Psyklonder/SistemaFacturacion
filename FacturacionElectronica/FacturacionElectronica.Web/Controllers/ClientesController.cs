using FacturacionElectronica.BL;
using FacturacionElectronica.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace FacturacionElectronica.Web.Controllers
{
    [Filters.AdminAuth]
    public class ClientesController : Controller
    {
        IbaseBL _base;
        // GET: Cliente
        public ActionResult Index()
        {
            ClienteBL clienteBD = new ClienteBL();

            var _clientes = clienteBD.ObtenerTodos();
            List<Cliente> result = (_clientes).Cast<Cliente>().ToList();


            return View(result);

        }

        // GET: Cliente/Details/5
        public ActionResult Details(int? id)
        {
            if (id != null)
            {
                int.TryParse(id.ToString(), out int idCliente);
                Cliente _registro = new Cliente();
                _base = new ClienteBL();
                _registro = (Cliente)_base.ObtenerId(idCliente);
                return View(_registro);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        // GET: Cliente/Create
        public ActionResult Create()
        {
            Cliente registro = new Cliente();
            registro.Ciudades = new List<Ciudad>();
            registro.fechaNacimiento = DateTime.Now;
            registro.Ciudades = CiudadBL.ObtenerTodos();
            return View(registro);
        }


        // POST: Empleados/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "nombres,apellidos,documento,fechaNacimiento,direccion,telefono,email")] Cliente registro, FormCollection formCollection)
        {
            try
            {
                string idCiudad = formCollection.Get("ciudad");

                if (ModelState.IsValid)
                {
                    _base = new ClienteBL();
                    registro.idCiudad = int.Parse(idCiudad);
                    _base.Guardar(registro);
                    return RedirectToAction("Index");
                }
                else
                {
                    registro.Ciudades = new List<Ciudad>();
                    registro.Ciudades = CiudadBL.ObtenerTodos();
                    return View(registro);
                }
            }
            catch
            {
                registro.Ciudades = new List<Ciudad>();
                registro.Ciudades = CiudadBL.ObtenerTodos();
                return View(registro);
            }
        }


        // GET: Cliente/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                _base = new ClienteBL();
                Cliente _registro = new Cliente();
                int.TryParse(id.ToString(), out int _idCliente);
                _registro = (Cliente)_base.ObtenerId(_idCliente);

                _registro.Ciudades = new List<Ciudad>();

                _registro.Ciudades = CiudadBL.ObtenerTodos();
                return View(_registro);
            }
        }

        // POST: Cliente/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "id,nombres,apellidos,documento,fechaNacimiento,direccion,telefono,email")] Cliente registro, FormCollection formCollection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string idCiudad = formCollection.Get("ciudad");
                    _base = new ClienteBL();
                    registro.idCiudad = int.Parse(idCiudad);
                    _base.Editar(registro);
                    return RedirectToAction("Index");
                }
                else
                {
                    registro.Ciudades = new List<Ciudad>();
                    registro.Ciudades = CiudadBL.ObtenerTodos();
                    return View(registro);
                }
            }
            catch (Exception error)
            {
                return View(registro);
            }
        }

        // GET: Cliente/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                _base = new ClienteBL();
                Cliente _registro = new Cliente();
                int.TryParse(id.ToString(), out int _idCliente);
                _registro = (Cliente)_base.ObtenerId(_idCliente);
                return View(_registro);
            }
        }

        // POST: Cliente/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                var _facturas = FacturaBL.ObtenerPorCliente(id);
                var _listaFacturas = (_facturas).Cast<Factura>().ToList();
                if (_listaFacturas.Count > 0)
                {
                    ViewBag.Message = "no se puede borrar el cliente. Hay facturas asociadas al mismo.";
                    return View(id);
                }
                else
                {
                    _base = new ClienteBL();
                    _base.Borrar(id);
                    return RedirectToAction("Index");
                }

            }
            catch
            {
                return View(id);
            }
        }
    }
}
