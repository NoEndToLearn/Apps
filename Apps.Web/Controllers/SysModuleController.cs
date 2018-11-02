using Apps.BLL.Core;
using Apps.Common;
using Apps.IBLL;
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
    public class SysModuleController : BaseController
    {
        /// <summary>
        /// 业务层注入
        /// </summary>
        [Dependency]
        public ISysModuleBLL m_BLL { get; set; }
        [Dependency]
        public ISysModuleOperateBLL operateBLL { get; set; }
        ValidationErrors errors = new ValidationErrors();

        /// <summary>
        /// 主页
        /// </summary>
        /// <returns>视图</returns>
        [SupportFilter]
        public ActionResult Index()
        {
            ViewBag.Perm = GetPermission();
            return View();

        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pager">分页</param>
        /// <param name="queryStr">查询条件</param>
        /// <returns></returns>
        [SupportFilter(ActionName = "Index")]
        [HttpPost]
        public JsonResult GetList(string id)
        {
            if (id == null)
                id = "0";
            List<SysModuleModel> list = m_BLL.GetList(id);
            var json = from r in list
                       select new SysModuleModel()
                       {
                           Id = r.Id,
                           Name = r.Name,
                           EnglishName = r.EnglishName,
                           ParentId = r.ParentId,
                           Url = r.Url,
                           Iconic = r.Iconic,
                           Sort = r.Sort,
                           Remark = r.Remark,
                           Enable = r.Enable,
                           CreatePerson = r.CreatePerson,
                           CreateTime = r.CreateTime,
                           IsLast = r.IsLast,
                           state = (m_BLL.GetList(r.Id).Count > 0) ? "closed" : "open"
                       };


            return Json(json);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pager">分页</param>
        /// <param name="queryStr">查询条件</param>
        /// <returns></returns>
        [SupportFilter(ActionName = "Index")]
        [HttpPost]
        public JsonResult GetTreeList(string id)
        {
            if (id == null)
                id = "0";
            List<SysModuleModel> list = m_BLL.GetList(id);
            SysModuleModelTree JsonTree;
            var modelTreeList = new List<SysModuleModelTree>();
            GetTreeList(list, modelTreeList, null);

            return Json(modelTreeList);
        }

        private SysModuleModelTree GetTreeList(List<SysModuleModel> list, List<SysModuleModelTree> listTree, SysModuleModelTree jsonTree)
        {
            SysModuleModelTree ss = null;
            for (int i = 0; i < list.Count; i++)
            {
                SysModuleModel model = list[i];
                List<SysModuleModel> modelList = list.Where(s => s.ParentId == model.Id).OrderBy(s => s.Sort).ToList();
                ss = new SysModuleModelTree()
                {
                    Id = model.Id,
                    Enable = model.Enable,
                    EnglishName = model.EnglishName,
                    Iconic = model.Iconic,
                    IsLast = model.IsLast,
                    Name = model.Name,
                    ParentId = model.ParentId,
                    Sort = model.Sort,
                    state = model.state,
                    Url = model.Url,
                    Version = model.Version
                };
                if (ss.Child == null)
                {
                    ss.Child = new List<SysModuleModelTree>();
                }
                if (jsonTree != null)
                {
                    if (modelList.Count == 0)
                    {
                        jsonTree.Child.Add(GetTreeList(modelList, listTree, ss));
                    }
                    if (jsonTree.ParentId == "0")
                    {
                        listTree.Add(jsonTree);
                    }
                }
            }
            return ss;


        }

        [HttpPost]
        [SupportFilter(ActionName = "Index")]
        public JsonResult GetOptListByModule(GridPager pager, string mid)
        {
            pager.rows = 1000;
            pager.page = 1;
            List<SysModuleOperateModel> list = operateBLL.GetList(ref pager, mid);
            var json = new
            {
                total = pager.total,
                rows = (from r in list
                        select new SysModuleOperateModel()
                        {
                            Id = r.Id,
                            Name = r.Name,
                            KeyCode = r.KeyCode,
                            ModuleId = r.ModuleId,
                            IsValid = r.IsValid,
                            Sort = r.Sort

                        }).ToArray()

            };

            return Json(json);
        }


        #region 创建模块
        [SupportFilter]
        public ActionResult Create(string id)
        {
            ViewBag.Perm = GetPermission();
            SysModuleModel entity = new SysModuleModel()
            {
                ParentId = id,
                Enable = true,
                Sort = 0
            };
            return View(entity);
        }

        [HttpPost]
        [SupportFilter]
        public JsonResult Create(SysModuleModel model)
        {
            model.Id = ResultHelper.NewId;
            model.CreateTime = ResultHelper.NowTime;
            model.CreatePerson = GetCurrentUserId();
            if (model != null && ModelState.IsValid)
            {

                if (m_BLL.Create(ref errors, model))
                {
                    LogHandler.WriteServiceLog(GetCurrentUserId(), "Id" + model.Id + ",Name" + model.Name, "成功", "创建", "SysModule");
                    return Json(JsonHandler.CreateMessage(1, Suggestion.InsertSucceed));
                }
                else
                {
                    string ErrorCol = errors.Error;
                    LogHandler.WriteServiceLog(GetCurrentUserId(), "Id" + model.Id + ",Name" + model.Name + "," + ErrorCol, "失败", "创建", "SysModule");
                    return Json(JsonHandler.CreateMessage(0, Suggestion.InsertFail + ErrorCol));
                }
            }
            else
            {
                return Json(JsonHandler.CreateMessage(0, Suggestion.InsertFail));
            }
        }
        #endregion


        #region 创建
        [SupportFilter(ActionName = "Create")]
        public ActionResult CreateOpt(string moduleId)
        {
            ViewBag.Perm = GetPermission();
            SysModuleOperateModel sysModuleOptModel = new SysModuleOperateModel();
            sysModuleOptModel.ModuleId = moduleId;
            sysModuleOptModel.IsValid = true;
            return View(sysModuleOptModel);
        }


        [HttpPost]
        [SupportFilter(ActionName = "Create")]
        public JsonResult CreateOpt(SysModuleOperateModel info)
        {
            if (info != null && ModelState.IsValid)
            {
                SysModuleOperateModel entity = operateBLL.GetById(info.Id);
                if (entity != null)
                    return Json(JsonHandler.CreateMessage(0, Suggestion.PrimaryRepeat), JsonRequestBehavior.AllowGet);
                entity = new SysModuleOperateModel();
                entity.Id = info.ModuleId + info.KeyCode;
                entity.Name = info.Name;
                entity.KeyCode = info.KeyCode;
                entity.ModuleId = info.ModuleId;
                entity.IsValid = info.IsValid;
                entity.Sort = info.Sort;

                if (operateBLL.Create(ref errors, entity))
                {
                    LogHandler.WriteServiceLog(GetCurrentUserId(), "Id:" + info.Id + ",Name:" + info.Name, "成功", "创建", "模块设置");
                    return Json(JsonHandler.CreateMessage(1, Suggestion.InsertSucceed), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    string ErrorCol = errors.Error;
                    LogHandler.WriteServiceLog(GetCurrentUserId(), "Id:" + info.Id + ",Name:" + info.Name + "," + ErrorCol, "失败", "创建", "模块设置");
                    return Json(JsonHandler.CreateMessage(0, Suggestion.InsertFail + ErrorCol), JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(JsonHandler.CreateMessage(0, Suggestion.InsertFail), JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region 修改模块
        [SupportFilter]
        public ActionResult Edit(string id)
        {
            ViewBag.Perm = GetPermission();
            SysModuleModel entity = m_BLL.GetById(id);
            return View(entity);
        }

        [HttpPost]
        [SupportFilter]
        public JsonResult Edit(SysModuleModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                if (m_BLL.Edit(ref errors, model))
                {
                    LogHandler.WriteServiceLog(GetCurrentUserId(), "Id" + model.Id + ",Name" + model.Name, "成功", "修改", "系统菜单");
                    return Json(JsonHandler.CreateMessage(1, Suggestion.EditSucceed));
                }
                else
                {
                    string ErrorCol = errors.Error;
                    LogHandler.WriteServiceLog(GetCurrentUserId(), "Id" + model.Id + ",Name" + model.Name + "," + ErrorCol, "失败", "修改", "系统菜单");
                    return Json(JsonHandler.CreateMessage(0, Suggestion.EditFail + ErrorCol));
                }
            }
            else
            {
                return Json(JsonHandler.CreateMessage(0, Suggestion.EditFail));
            }
        }
        #endregion



        #region 删除
        [HttpPost]
        [SupportFilter]
        public JsonResult Delete(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                if (m_BLL.Delete(ref errors, id))
                {
                    LogHandler.WriteServiceLog(GetCurrentUserId(), "Ids:" + id, "成功", "删除", "模块设置");
                    return Json(JsonHandler.CreateMessage(1, Suggestion.DeleteSucceed), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    string ErrorCol = errors.Error;
                    LogHandler.WriteServiceLog(GetCurrentUserId(), "Id:" + id + "," + ErrorCol, "失败", "删除", "模块设置");
                    return Json(JsonHandler.CreateMessage(0, Suggestion.DeleteFail + ErrorCol), JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(JsonHandler.CreateMessage(0, Suggestion.DeleteFail), JsonRequestBehavior.AllowGet);
            }


        }


        [HttpPost]
        [SupportFilter(ActionName = "Delete")]
        public JsonResult DeleteOpt(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                if (operateBLL.Delete(ref errors, id))
                {
                    LogHandler.WriteServiceLog(GetCurrentUserId(), "Id:" + id, "成功", "删除", "模块设置KeyCode");
                    return Json(JsonHandler.CreateMessage(1, Suggestion.DeleteSucceed), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    string ErrorCol = errors.Error;
                    LogHandler.WriteServiceLog(GetCurrentUserId(), "Id:" + id + "," + ErrorCol, "失败", "删除", "模块设置KeyCode");
                    return Json(JsonHandler.CreateMessage(0, Suggestion.DeleteFail + ErrorCol), JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(JsonHandler.CreateMessage(0, Suggestion.DeleteFail), JsonRequestBehavior.AllowGet);
            }


        }

        #endregion
    }
}