using FacturacionElectronica.BL;
using FacturacionElectronica.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FacturacionElectronica.Web.Controllers
{
    [Filters.AdminAuth]
    public class ProductosController : Controller
    {
        IbaseBL _base;
        // GET: Cliente
        public ActionResult Index()
        {
            _base = new ProductoBL();

            var _productos = _base.ObtenerTodos();
            List<Producto> _result = (_productos).Cast<Producto>().ToList();


            return View(_result);

        }

        // GET: Cliente/Details/5
        public ActionResult Details(int? id)
        {
            if (id != null)
            {
                int.TryParse(id.ToString(), out int _idProducto);
                Producto _registro = new Producto();
                _base = new ProductoBL();
                _registro = (Producto)_base.ObtenerId(_idProducto);
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
            Producto _registro = new Producto();
            _registro.Categorias = CategoriaBL.ObtenerTodos();

            return View(_registro);
        }


        // POST: Empleados/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "nombre,descripcion,precio,cantidad")] Producto registro, FormCollection formCollection)
        {
            try
            {
                string _idCategoria = formCollection.Get("categoria");

                if (ModelState.IsValid)
                {
                    _base = new ProductoBL();
                    registro.idCategoria = int.Parse(_idCategoria);
                    _base.Guardar(registro);
                    return RedirectToAction("Index");
                }
                else
                {
                  
                    registro.Categorias = CategoriaBL.ObtenerTodos();
                    return View(registro);
                }
            }
            catch
            {
                registro.Categorias = CategoriaBL.ObtenerTodos();
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
                _base = new ProductoBL();
                Producto _registro = new Producto();
                int.TryParse(id.ToString(), out int _idProducto);
                _registro = (Producto)_base.ObtenerId(_idProducto);
                _registro.Categorias = CategoriaBL.ObtenerTodos();

                return View(_registro);
            }
        }

        // POST: Cliente/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "id,idCategoria,nombre,descripcion,precio,cantidad")] Producto registro, FormCollection formCollection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string _idCategoria = formCollection.Get("categoria");
                    _base = new ProductoBL();
                    registro.idCategoria = int.Parse(_idCategoria);
                    _base.Editar(registro);
                    return RedirectToAction("Index");
                }
                else
                {
                    registro.Categorias = CategoriaBL.ObtenerTodos();
                    return View(registro);
                }
            }
            catch (Exception error)
            {
                registro.Categorias = CategoriaBL.ObtenerTodos();
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
                _base = new ProductoBL();
                Producto _registro = new Producto();
                int.TryParse(id.ToString(), out int _idProducto);
                _registro = (Producto)_base.ObtenerId(_idProducto);
                return View(_registro);
            }
        }

        // POST: Cliente/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                ProductoBL _existenProductos = new ProductoBL();
               
                if (_existenProductos.ExistenFacturas(id))
                {
                    ViewBag.Message = "no se puede borrar el producto. Hay facturas asociadas al mismo.";
                    return View(id);
                }
                else
                {
                    _base = new ProductoBL();
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
