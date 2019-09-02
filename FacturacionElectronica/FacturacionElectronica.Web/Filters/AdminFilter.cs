using FacturacionElectronica.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FacturacionElectronica.Web.Filters
{
    public class AdminAuth: System.Web.Mvc.ActionFilterAttribute, System.Web.Mvc.IActionFilter
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var usuario = (Usuario)HttpContext.Current.Session["User"];
            if (usuario != null)
            {
                if (usuario.Rol == null)
                {
                    filterContext.Result = new System.Web.Mvc.RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary { { "Controller", "LogIns" }, { "Action", "Index" } });
                }
                else if (usuario.Rol.Where(x => x.nombre == "SUPERADMIN").FirstOrDefault() == null)
                {
                    filterContext.Result = new System.Web.Mvc.RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary { { "Controller", "Home" }, { "Action", "Index" } });

                }
            }
            else
            {
                filterContext.Result = new System.Web.Mvc.RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary { { "Controller", "LogIn" }, { "Action", "Index" } });
            }
            base.OnActionExecuting(filterContext);
        }
    }
      
}