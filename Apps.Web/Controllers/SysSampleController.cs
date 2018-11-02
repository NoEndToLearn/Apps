using Apps.BLL.Core;
using Apps.Common;
using Apps.IBLL;
using Apps.Models;
using Apps.Models.Sys;
using Apps.Web.Core;
using Apps.Web.Models;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Unity.Attributes;

namespace Apps.Web.Controllers
{
    public class SysSampleController : BaseController
    {
        ValidationErrors errors = new ValidationErrors();
        //
        // GET: /SysSample/
        /// <summary>
        /// 业务层注入
        /// </summary>
        [Dependency]
        public ISysSampleBll _SysSampleBll { get; set; }
        [SupportFilter]
        public ActionResult Index()
        {
            ViewBag.Perm = GetPermission();
            return View();
        }
        [HttpPost]
        public ActionResult GetList(GridPager pager,string queryStr)
        {

            List<SysSampleModel> list = _SysSampleBll.GetList(ref pager, queryStr);
            var json = new
            {
                total = pager.total,
                rows = (from r in list
                        select new SysSampleModel()
                        {
                            Id = r.Id,
                            Name = r.Name,
                            Age = r.Age,
                            Bir = r.Bir,
                            Photo = r.Photo,
                            Note = r.Note,
                            CreateTime = r.CreateTime
                        }).ToArray()
            };
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        #region 创建
        [SupportFilter]
        public ActionResult Create()
        {
           
            return View();
        }

        [HttpPost]
        public JsonResult Create(SysSampleModel model)
        {


            if (_SysSampleBll.Create(ref errors,model))
            {
                LogHandler.WriteServiceLog("虚拟用户", "Id:" + model.Id + ",Name:" + model.Name, "成功", "创建", "样例程序");
                return Json(JsonHandler.CreateMessage(1, "插入成功"), JsonRequestBehavior.AllowGet);
            }
            else
            {
                string ErrorCol = errors.Error;
                LogHandler.WriteServiceLog("虚拟用户", "Id:" + model.Id + ",Name:" + model.Name, "失败", "创建", "样例程序");
                return Json(JsonHandler.CreateMessage(0, "插入失败更新条目时出错，有关详细信息，请参见内部异常。"), JsonRequestBehavior.AllowGet);
            }

        }
        #endregion

        #region 修改

        public ActionResult Edit(string id)
        {

            SysSampleModel entity = _SysSampleBll.GetById(id);
            LogHandler.WriteServiceLog("虚拟用户", "Id:" + entity.Id + ",Name:" + entity.Name, "成功", "修改", "样例程序");
            return View(entity);
        }

        [HttpPost]

        public JsonResult Edit(SysSampleModel model)
        {


            if (_SysSampleBll.Edit(model))
            {
                LogHandler.WriteServiceLog("虚拟用户", "Id:" + model.Id + ",Name:" + model.Name, "成功", "修改", "样例程序");
                return Json(JsonHandler.CreateMessage(1, "修改成功。"), JsonRequestBehavior.AllowGet);
            }
            else
            {
                LogHandler.WriteServiceLog("虚拟用户", "Id:" + model.Id + ",Name:" + model.Name, "失败", "修改", "样例程序");
                return Json(JsonHandler.CreateMessage(0, "修改失败更新条目时出错，有关详细信息，请参见内部异常。"), JsonRequestBehavior.AllowGet);
            }

        }
        #endregion

        #region 详细
        public ActionResult Details(string id)
        {
   
            SysSampleModel entity = _SysSampleBll.GetById(id);
            LogHandler.WriteServiceLog("虚拟用户", "Id:" + entity.Id + ",Name:" + entity.Name, "成功", "查看", "样例程序");
            return View(entity);
        }

        #endregion

        #region 删除
        [HttpPost]
        public JsonResult Delete(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                if (_SysSampleBll.Delete(id))
                {
                    LogHandler.WriteServiceLog("虚拟用户", "Id:" + id, "成功", "删除", this.GetType().FullName);
                    return Json(JsonHandler.CreateMessage(1, "删除成功。"), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    LogHandler.WriteServiceLog("虚拟用户", "Id:" + id, "失败", "删除", this.GetType().FullName);
                    return Json(JsonHandler.CreateMessage(0, "删除失败。"), JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                LogHandler.WriteServiceLog("虚拟用户", "Id:" + id, "失败", "删除", this.GetType().FullName);
                return Json(JsonHandler.CreateMessage(0, "删除失败。"), JsonRequestBehavior.AllowGet);
            }


        }
        #endregion

        public ActionResult Reporting(string type = "PDF", string queryStr = "", int rows = 0, int page = 1)
        {
            //选择了导出全部
            if (rows == 0 && page == 0)
            {
                rows = 9999999;
                page = 1;
            }
            GridPager pager = new GridPager()
            {
                rows = rows,
                page = page,
                sort = "Id",
                order = "desc"
            };
            List<SysSampleModel> ds = _SysSampleBll.GetList(ref pager, queryStr);
            LocalReport localReport = new LocalReport();
            localReport.ReportPath = Server.MapPath("~/Reports/SysSampleReport.rdlc");
            ReportDataSource reportDataSource = new ReportDataSource("DataSet1", ds);
            localReport.DataSources.Add(reportDataSource);
            string reportType = type;
            string mimeType;
            string encoding;
            string fileNameExtension;

            string deviceInfo =
                "<DeviceInfo>" +
                "<OutPutFormat>" + type + "</OutPutFormat>" +
                "<PageWidth>11in</PageWidth>" +
                "<PageHeight>11in</PageHeight>" +
                "<MarginTop>0.5in</MarginTop>" +
                "<MarginLeft>1in</MarginLeft>" +
                "<MarginRight>1in</MarginRight>" +
                "<MarginBottom>0.5in</MarginBottom>" +
                "</DeviceInfo>";
            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;

            renderedBytes = localReport.Render(
                reportType,
                deviceInfo,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings
                );
            return File(renderedBytes, mimeType);
        }
    }
}