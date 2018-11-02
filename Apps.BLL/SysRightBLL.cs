using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Unity;
using Apps.Models;
using Apps.Common;
using System.Transactions;
using Apps.Models.Sys;
using Apps.IBLL;
using Apps.IDAL;
using Apps.BLL.Core;
using Unity.Attributes;
using Apps.BLL.core;

namespace Apps.BLL
{
    public class SysRightBLL : BaseBLL, ISysRightBLL
    {
        [Dependency]
        public ISysRightRepository sysRightRep { get; set; }

        public List<PermModel> GetPermission(string accountid, string controllor)
        {
            return sysRightRep.GetPermission(accountid, controllor);
        }

        public List<P_Sys_GetRightByRoleAndModule_Result> GetRightByRoleAndModule(string roleId, string moduleId)
        {
            return sysRightRep.GetRightByRoleAndModule(roleId, moduleId);
        }

        public int UpdateRight(SysRightOperate model)
        {
            return sysRightRep.UpdateRight(model);
        }

        public List<SysRightModel> GetList(ref GridPager pager, string queryStr)
        {

            IQueryable<SysRight> queryData = null;
            if (!string.IsNullOrWhiteSpace(queryStr))
            {
                queryData = sysRightRep.GetList(db).Where(a => a.RoleId.Contains(queryStr) || a.ModuleId.Contains(queryStr));
            }
            else
            {
                queryData = sysRightRep.GetList(db);
            }
            pager.rows = queryData.Count();
            queryData = LinqHelper.SortingAndPaging(queryData, pager.sort, pager.order, pager.page, pager.rows);
            return CreateModelList(ref queryData);
        }
        private List<SysRightModel> CreateModelList(ref IQueryable<SysRight> queryData)
        {

            List<SysRightModel> modelList = (from r in queryData
                                             select new SysRightModel
                                             {
                                                 Id = r.Id,
                                                 ModuleId = r.ModuleId,
                                                 Rightflag = r.Rightflag,
                                                 RoleId = r.RoleId
                                             }).ToList();
            return modelList;
        }

        public bool Create(ref ValidationErrors errors, SysRightModel model)
        {
            try
            {
                SysRight entity = sysRightRep.GetById(model.Id);
                if (entity != null)
                {
                    errors.Add(Suggestion.PrimaryRepeat);
                    return false;
                }
                entity = new SysRight();
                entity.Id = model.Id;
                entity.ModuleId = model.ModuleId;
                entity.Rightflag = model.Rightflag;
                entity.RoleId = model.RoleId;
                if (sysRightRep.Create(entity) == 1)
                {
                    return true;
                }
                else
                {
                    errors.Add(Suggestion.InsertFail);
                    return false;
                }
            }
            catch (Exception ex)
            {
                errors.Add(ex.Message);
                ExceptionHander.WriteException(ex);
                return false;
            }
        }

        public bool Delete(ref ValidationErrors errors, string id)
        {
            try
            {
                if (sysRightRep.Delete(id) == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                errors.Add(ex.Message);
                ExceptionHander.WriteException(ex);
                return false;
            }
        }

        public bool Delete(ref ValidationErrors errors, string[] deleteCollection)
        {
            try
            {
                if (deleteCollection != null)
                {
                    using (TransactionScope transactionScope = new TransactionScope())
                    {
                        sysRightRep.Delete(db, deleteCollection);
                        if (db.SaveChanges() == deleteCollection.Length)
                        {
                            transactionScope.Complete();
                            return true;
                        }
                        else
                        {
                            Transaction.Current.Rollback();
                            return false;
                        }
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                errors.Add(ex.Message);
                ExceptionHander.WriteException(ex);
                return false;
            }
        }
        public bool Edit(ref ValidationErrors errors, SysRightModel model)
        {
            try
            {
                SysRight entity = sysRightRep.GetById(model.Id);
                if (entity == null)
                {
                    errors.Add(Suggestion.Disable);
                    return false;
                }
                entity.Id = model.Id;
                entity.ModuleId = model.ModuleId;
                entity.Rightflag = model.Rightflag;
                entity.RoleId = model.RoleId;

                if (sysRightRep.Edit(entity) == 1)
                {
                    return true;
                }
                else
                {
                    errors.Add(Suggestion.EditFail);
                    return false;
                }
            }
            catch (Exception ex)
            {
                errors.Add(ex.Message);
                ExceptionHander.WriteException(ex);
                return false;
            }
        }

        public bool IsExists(string id)
        {
            if (db.SysRight.SingleOrDefault(a => a.Id == id) != null)
            {
                return true;
            }
            return false;
        }

        public SysRightModel GetById(string id)
        {
            if (IsExist(id))
            {
                SysRight entity = sysRightRep.GetById(id);
                SysRightModel model = new SysRightModel();
                model.Id = entity.Id;
                model.ModuleId = entity.ModuleId;
                model.Rightflag = entity.Rightflag;
                model.RoleId = entity.RoleId;
                return model;
            }
            else
            {
                return null;
            }
        }

        public bool IsExist(string id)
        {
            return sysRightRep.IsExist(id);
        }
    }
}
