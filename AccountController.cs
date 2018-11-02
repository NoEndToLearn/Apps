using Apps.Web.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Apps.Web.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        [Authorization(IsCheck = false)]
        public ActionResult Index()
        {
            return View();
        }
        [Authorization(IsCheck = false)]
        public ActionResult Login()
        {
            return View();
        }
    }
}