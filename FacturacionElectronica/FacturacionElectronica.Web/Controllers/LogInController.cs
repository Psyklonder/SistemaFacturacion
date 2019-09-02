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
    public class LogInController : Controller
    {
        public LogInController()
        {

        }

        // GET: LogIn
        public async Task<ActionResult> Index()
        {
            return await Task.Run(() => View());
        }

        [HttpPost]
        public async Task<ActionResult> Index([Bind(Include = "userName,password")] Usuario usuario)
        {
            UsuarioBL usuarioDB = new UsuarioBL();
            var sesion = usuarioDB.IniciarSesion(usuario);
            if (sesion == null)
            {
                if (usuario.userName == null || usuario.password == null)
                {
                    return await Task.Run(() => View(usuario));
                }
                else
                {
                    ViewBag.Message = "Nombre de usuario o password incorrectos";
                    return await Task.Run(() => View(usuario));
                }

            }
            else
            {
                Session["User"] = sesion;
                return RedirectToAction("Index", "Home", null);
            }

        }

        public ActionResult CerrarSesion()
        {
            Session["User"] = null;
            return RedirectToAction("Index", "Login", null);
        }
        // GET: LogIn/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: LogIn/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LogIn/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: LogIn/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: LogIn/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: LogIn/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LogIn/Delete/5
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
