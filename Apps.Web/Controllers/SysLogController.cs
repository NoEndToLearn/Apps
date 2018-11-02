using Apps.Common;
using Apps.IBLL;
using Apps.Models;
using Apps.Models.Sys;
using Apps.Web.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Unity.Attributes;

namespace Apps.Web.Controllers
{
    public class SysLogController : BaseController
    {
        [Dependency]
        public ISysLogBLL logBLL { get; set; }
        // GET: SysLog
        [SupportFilter]
        public ActionResult Index()
        {
            ViewBag.Perm = GetPermission();
            return View();
        }

        public JsonResult GetList(GridPager pager, string queryStr)
        {
            List<SysLog> list = logBLL.GetList(ref pager, queryStr);
            var json = new
            {
                total = pager.total,
                rows = (from r in list
                        select new SysLogModel()
                        {

                            Id = r.Id,
                            Operator = r.Operator,
                            Message = r.Message,
                            Result = r.Result,
                            Type = r.Type,
                            Module = r.Module,
                            CreateTime = r.CreateTime

                        }).ToArray()

            };

            return Json(json);
        }


        #region 详细

        public ActionResult Details(string id)
        {
            SysLog entity = logBLL.GetById(id);
            SysLogModel info = new SysLogModel()
            {
                Id = entity.Id,
                Operator = entity.Operator,
                Message = entity.Message,
                Result = entity.Result,
                Type = entity.Type,
                Module = entity.Module,
                CreateTime = entity.CreateTime,
            };
            return View(info);
        }

        #endregion

        #region 删除

        [HttpPost]
        public JsonResult Delete(string id)
        {
            if (logBLL.DeleteById(id) && !id.IsNullOrEmptyOrSpace())
            {
                return Json(JsonHandler.CreateMessage(1, "删除成功。"), JsonRequestBehavior.AllowGet);
            }
            return Json(JsonHandler.CreateMessage(0, "执行删除操作失败。"), JsonRequestBehavior.AllowGet);
        }
        #endregion

    }
}