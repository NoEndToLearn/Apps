using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Apps.Common;
using Apps.IBLL;
using Apps.Models.Sys;
using Microsoft.Practices.Unity;
using Microsoft.Reporting.WebForms;
using Apps.Web.Core;
using Unity.Attributes;
using Apps.MIS.IBLL;
using Apps.Models.MIS;
using Apps.BLL.Core;

namespace Apps.Web.Areas.MIS.Controllers
{
    public class CategoryController : BaseController
    {
        [Dependency]
        public IMIS_Article_CategoryBLL articleCategoryBLL { get; set; }
        ValidationErrors errors = new ValidationErrors();
        [SupportFilter]
        public ActionResult Index()
        {
            ViewBag.Perm = GetPermission();
            return View();
        }
        [HttpPost]
        public JsonResult GetList(GridPager pager, string queryStr)
        {
            List<MIS_Article_CategoryModel> list = articleCategoryBLL.GetList(ref pager, queryStr);
            var json = new
            {
                total = pager.total,
                rows = (from r in list
                        select new MIS_Article_CategoryModel()
                        {

                            Id = r.Id,
                            ChannelId = r.ChannelId,
                            Name = r.Name,
                            ParentId = r.ParentId,
                            Sort = r.Sort,
                            ImgUrl = r.ImgUrl,
                            BodyContent = r.BodyContent,
                            CreateTime = r.CreateTime,
                            Enable = r.Enable

                        }).ToArray()

            };

            return Json(json);
        }

        #region 创建
        [SupportFilter]
        public ActionResult Create()
        {
            ViewBag.Perm = GetPermission();
            return View();
        }

        [HttpPost]
        [SupportFilter]
        public JsonResult Create(MIS_Article_CategoryModel model)
        {
            model.Id = ResultHelper.NewId;
           // model.ChannelId = ResultHelper.NewId;
            model.CreateTime = ResultHelper.NowTime;

            if (model != null && ModelState.IsValid)
            {
                if (articleCategoryBLL.Create(ref errors, model))
                {
                    LogHandler.WriteServiceLog(GetCurrentUserId(), "Id" + model.Id + ",ChannelId" + model.Name, "成功", "创建", "IMIS_ArticleRepository");
                    return Json(JsonHandler.CreateMessage(1, Suggestion.InsertSucceed));
                }
                else
                {
                    string ErrorCol = errors.Error;
                    LogHandler.WriteServiceLog(GetCurrentUserId(), "Id" + model.Id + ",ChannelId" + model.Name + "," + ErrorCol, "失败", "创建", "IMIS_ArticleRepository");
                    return Json(JsonHandler.CreateMessage(0, Suggestion.InsertFail + ErrorCol));
                }
            }
            else
            {
                return Json(JsonHandler.CreateMessage(0, Suggestion.InsertFail + "\n" + ErrorInfo));
            }
        }
        #endregion

        #region 修改
        [SupportFilter]
        public ActionResult Edit(string id)
        {
            ViewBag.Perm = GetPermission();
            MIS_Article_CategoryModel entity = articleCategoryBLL.GetById(id);
            return View(entity);
        }

        [HttpPost]
        [SupportFilter]
        public JsonResult Edit(MIS_Article_CategoryModel model)
        {
            if (model != null && ModelState.IsValid)
            {

                if (articleCategoryBLL.Edit(ref errors, model))
                {
                    LogHandler.WriteServiceLog(GetCurrentUserId(), "Id" + model.Id + ",ChannelId" + model.Name, "成功", "修改", "IMIS_ArticleRepository");
                    return Json(JsonHandler.CreateMessage(1, Suggestion.EditSucceed));
                }
                else
                {
                    string ErrorCol = errors.Error;
                    LogHandler.WriteServiceLog(GetCurrentUserId(), "Id" + model.Id + ",ChannelId" + model.Name + "," + ErrorCol, "失败", "修改", "IMIS_ArticleRepository");
                    return Json(JsonHandler.CreateMessage(0, Suggestion.EditFail + ErrorCol));
                }
            }
            else
            {
                return Json(JsonHandler.CreateMessage(0, Suggestion.EditFail));
            }
        }
        #endregion

        #region 详细
        [SupportFilter]
        public ActionResult Details(string id)
        {
            ViewBag.Perm = GetPermission();
            MIS_Article_CategoryModel entity = articleCategoryBLL.GetById(id);
            return View(entity);
        }

        #endregion

        #region 删除
        [HttpPost]
        [SupportFilter]
        public JsonResult Delete(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                if (articleCategoryBLL.Delete(ref errors, id))
                {
                    LogHandler.WriteServiceLog(GetCurrentUserId(), "Id:" + id, "成功", "删除", "IMIS_ArticleRepository");
                    return Json(JsonHandler.CreateMessage(1, Suggestion.DeleteSucceed));
                }
                else
                {
                    string ErrorCol = errors.Error;
                    LogHandler.WriteServiceLog(GetCurrentUserId(), "Id" + id + "," + ErrorCol, "失败", "删除", "IMIS_ArticleRepository");
                    return Json(JsonHandler.CreateMessage(0, Suggestion.DeleteFail + ErrorCol));
                }
            }
            else
            {
                return Json(JsonHandler.CreateMessage(0, Suggestion.DeleteFail));
            }


        }
        #endregion
        #region 导出到PDF EXCEL WORD
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
            List<MIS_Article_CategoryModel> ds = articleCategoryBLL.GetList(ref pager, queryStr);
            LocalReport localReport = new LocalReport();
            localReport.ReportPath = Server.MapPath("~/Reports/MIS_Article_CategoryReport.rdlc");
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
        #endregion
    }
}
