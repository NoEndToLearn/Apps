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
using Apps.Models.MIS;
using Apps.BLL.Core;
using Apps.MIS.IBLL;

namespace Apps.Web.Areas.MIS.Controllers
{
    public class MyArticleController : BaseController
    {
        [Dependency]
        public IMIS_ArticleBLL articleBll { get; set; }
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
            string currUserId = GetCurrentUserId();
            List<MIS_ArticleModel> list = articleBll.GetList(ref pager, queryStr).Where(s=>s.Creater == currUserId).ToList();
            var json = new
            {
                total = pager.total,
                rows = (from r in list
                        select new MIS_ArticleModel()
                        {

                            Id = r.Id,
                            ChannelId = r.ChannelId,
                            CategoryId = r.CategoryId,
                            Title = r.Title,
                            ImgUrl = r.ImgUrl,
                            BodyContent = r.BodyContent,
                            Sort = r.Sort,
                            Click = r.Click,
                            CheckFlag = r.CheckFlag,
                            Checker = r.Checker,
                            CheckDateTime = r.CheckDateTime,
                            Creater = r.Creater,
                            CreateTime = r.CreateTime

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
        public JsonResult Create(MIS_ArticleModel model)
        {
            model.Id = ResultHelper.NewId;
            model.CreateTime = ResultHelper.NowTime;
            model.Creater = GetCurrentUserId();
            if (model != null && ModelState.IsValid)
            {

                if (articleBll.Create(ref errors, model))
                {
                    LogHandler.WriteServiceLog(GetCurrentUserId(), "Id" + model.Id + ",ChannelId" + model.ChannelId, "成功", "创建", "IMIS_ArticleRepository");
                    return Json(JsonHandler.CreateMessage(1, Suggestion.InsertSucceed));
                }
                else
                {
                    string ErrorCol = errors.Error;
                    LogHandler.WriteServiceLog(GetCurrentUserId(), "Id" + model.Id + ",ChannelId" + model.ChannelId + "," + ErrorCol, "失败", "创建", "IMIS_ArticleRepository");
                    return Json(JsonHandler.CreateMessage(0, Suggestion.InsertFail + ErrorCol));
                }
            }
            else
            {
                return Json(JsonHandler.CreateMessage(0, Suggestion.InsertFail));
            }
        }
        #endregion

        #region 修改
        [SupportFilter]
        public ActionResult Edit(string id)
        {
            ViewBag.Perm = GetPermission();
            MIS_ArticleModel entity = articleBll.GetById(id);
            return View(entity);
        }

        [HttpPost]
        [SupportFilter]
        public JsonResult Edit(MIS_ArticleModel model)
        {
            if (model != null && ModelState.IsValid)
            {

                if (articleBll.Edit(ref errors, model))
                {
                    LogHandler.WriteServiceLog(GetCurrentUserId(), "Id" + model.Id + ",ChannelId" + model.ChannelId, "成功", "修改", "IMIS_ArticleRepository");
                    return Json(JsonHandler.CreateMessage(1, Suggestion.EditSucceed));
                }
                else
                {
                    string ErrorCol = errors.Error;
                    LogHandler.WriteServiceLog(GetCurrentUserId(), "Id" + model.Id + ",ChannelId" + model.ChannelId + "," + ErrorCol, "失败", "修改", "IMIS_ArticleRepository");
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
            MIS_ArticleModel entity = articleBll.GetById(id);
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
                if (articleBll.Delete(ref errors, id))
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
            List<MIS_ArticleModel> ds = articleBll.GetList(ref pager, queryStr);
            LocalReport localReport = new LocalReport();
            localReport.ReportPath = Server.MapPath("~/Reports/MIS_ArticleReport.rdlc");
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
