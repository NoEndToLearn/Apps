using Apps.IBLL;
using Apps.Models;
using Apps.Models.Sys;
using Apps.Web.Core;
using Apps.Web.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Unity.Attributes;

namespace Apps.Web.Controllers
{
    public class HomeController : BaseController
    {
        //
        // GET: /Home/
        [Dependency]
        public IHomeBll homeBLL { get; set; }

        public ActionResult Index()
        {
            AccountModel account = (AccountModel)Session["Account"];
            SysModuleModelTree menus = homeBLL.GetMenu(account.Id, "0");
            return View(menus);
        }

        /// <summary>
        /// 获取导航菜单
        /// </summary>
        /// <param name="id">所属</param>
        /// <returns>树</returns>
        public JsonResult GetTree(string id)
        {
            if (Session["Account"] != null)
            {
                AccountModel account = (AccountModel)Session["Account"];
                List<SysModuleModel> menus = homeBLL.GetMenuByPersonId(account.Id, id);
                var jsonData = (
                        from m in menus
                        select new
                        {
                            id = m.Id,
                            text = m.Name,
                            value = m.Url,
                            showcheck = false,
                            complete = false,
                            isexpand = false,
                            checkstate = 0,
                            hasChildren = m.IsLast ? false : true,
                            Icon = m.Iconic
                        }
                    ).ToArray();
                return Json(jsonData, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("0", JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 获取导航菜单
        /// </summary>
        /// <param name="id">所属</param>
        /// <returns>树</returns>
        public JsonResult GetJsonTree(string id)
        {
            if (Session["Account"] != null)
            {
                AccountModel account = (AccountModel)Session["Account"];
                List<SysModuleModel> menus = homeBLL.GetMenuByPersonId(account.Id, id);
                //            private int menuId;//菜单编号
                //private int pMenuId;//父菜单编号
                //private String name;//菜单名称
                //private String controller;//菜单对应的controller
                var jsonData = (
                        from m in menus
                        select new
                        {
                            id = m.Id,
                            Pid = m.ParentId,
                            text = m.Name,
                            value = m.Url,
                            showcheck = false,
                            complete = false,
                            isexpand = false,
                            checkstate = 0,
                            hasChildren = m.IsLast ? false : true,
                            Icon = m.Iconic
                        }
                    ).ToArray();
                JsonConvert.SerializeObject(jsonData);
                return Json(jsonData, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("0", JsonRequestBehavior.AllowGet);
            }
        }
    }
}