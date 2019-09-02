using FacturacionElectronica.BL;
using FacturacionElectronica.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FacturacionElectronica.Web.Controllers
{
    public class HomeController : Controller
    {
        Usuario _usuarioLogeado;
        public HomeController()
        {

          
        }

        public ActionResult Index()
        {
            if (HttpContext.Session["User"] == null)
            {
                return RedirectToAction("Index", "LogIn", null);
            }
            else
            {
                _usuarioLogeado = (Usuario)Session["User"];
                return View();
            }
            //var roles = listaPaises.Rol;
        
        }

       
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


    }
}