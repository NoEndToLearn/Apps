using Apps.Web.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Apps.Web.Controllers
{
    public class HomeController : Controller
    {
        //protected override void OnActionExecuting(ActionExecutingContext filterContext)
        //{
        //    if (filterContext.HttpContext.Session["username"] == null)
        //        filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { action = "Login", controller = "Account" }));

        //    base.OnActionExecuting(filterContext);
        //}
        public ActionResult Index()
        {
            return View();
        }
    }
}