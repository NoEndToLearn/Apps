using Apps.BLL.core;
using Apps.Common;
using Apps.IBLL;
using Apps.IDAL;
using Apps.Models;
using Apps.Models.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Attributes;


/*




*/
namespace Apps.BLL
{
    public class SysRoleBLL : BaseBLL, ISysRoleBLL
    {
        [Dependency]
        public ISysRoleRepository SysRoleRep { get; set; }
        public List<SysRoleModel> GetList(ref GridPager pager, string queryStr)
        {

            IQueryable<SysRole> queryData = null;
            if (!string.IsNullOrWhiteSpace(queryStr))
            {
                queryData = SysRoleRep.GetList(db).Where(a => a.Name.Contains(queryStr));
            }
            else
            {
                queryData = SysRoleRep.GetList(db);
            }
            pager.rows = queryData.Count();
            queryData = LinqHelper.SortingAndPaging(queryData, pager.sort, pager.order, pager.page, pager.rows);
            return CreateModelList(ref queryData);
        }
        private List<SysRoleModel> CreateModelList(ref IQueryable<SysRole> queryData)
        {
            List<SysRole> sysRoleList = queryData.ToList();
            List<SysRoleModel> modelList = sysRoleList.Select(r => new SysRoleModel()
            {
                Id = r.Id,
                Name = r.Name,
                Description = r.Description,
                CreateTime = r.CreateTime,
                CreatePerson = r.CreatePerson,
                UserName = r.SysUser.Aggregate("", (a, b) => b == null ? a : a + "[" + b.UserName + "] ")
            }).ToList();

            return modelList;
        }

        public bool Create(ref ValidationErrors errors, SysRoleModel model)
        {
            try
            {
                SysRole entity = SysRoleRep.GetById(model.Id);
                if (entity != null)
                {
                    errors.Add(Suggestion.PrimaryRepeat);
                    return false;
                }
                entity = new SysRole();
                entity.Id = model.Id;
                entity.Name = model.Name;
                entity.Description = model.Description;
                entity.CreateTime = model.CreateTime;
                entity.CreatePerson = model.CreatePerson;
                if (SysRoleRep.Create(entity) == 1)
                {
                    //分配给角色
                    db.P_Sys_InsertSysRight();
                    //清理无用的项
                    db.P_Sys_ClearUnusedRightOperate();
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
                if (SysRoleRep.Delete(id) == 1)
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


        public bool Edit(ref ValidationErrors errors, SysRoleModel model)
        {
            try
            {
                SysRole entity = SysRoleRep.GetById(model.Id);
                if (entity == null)
                {
                    errors.Add(Suggestion.Disable);
                    return false;
                }
                entity.Id = model.Id;
                entity.Name = model.Name;
                entity.Description = model.Description;
                entity.CreateTime = model.CreateTime;
                entity.CreatePerson = model.CreatePerson;

                if (SysRoleRep.Edit(entity) == 1)
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
            if (db.SysRole.SingleOrDefault(a => a.Id == id) != null)
            {
                return true;
            }
            return false;
        }

        public SysRoleModel GetById(string id)
        {
            if (IsExist(id))
            {
                SysRole entity = SysRoleRep.GetById(id);
                SysRoleModel model = new SysRoleModel();
                model.Id = entity.Id;
                model.Name = entity.Name;
                model.Description = entity.Description;
                model.CreateTime = entity.CreateTime;
                model.CreatePerson = entity.CreatePerson;
                return model;
            }
            else
            {
                return null;
            }
        }

        public bool IsExist(string id)
        {
            return SysRoleRep.IsExist(id);
        }

        /// <summary>
        /// 获取角色对应的所有用户
        /// </summary>
        /// <param name="roleId">角色id</param>
        /// <returns></returns>
        public string GetRefSysUser(string roleId)
        {
            string UserName = "";
            var userList = SysRoleRep.GetRefSysUser(db, roleId);
            if (userList != null)
            {
                foreach (var user in userList)
                {
                    UserName += "[" + user.UserName + "] ";
                }
            }
            return UserName;
        }

        public IQueryable<P_Sys_GetUserByRoleId_Result> GetUserByRoleId(ref GridPager pager, string roleId)
        {
            IQueryable<P_Sys_GetUserByRoleId_Result> queryData = SysRoleRep.GetUserByRoleId(db, roleId);
            pager.rows = queryData.Count();
            queryData = SysRoleRep.GetUserByRoleId(db, roleId);
            return queryData.Skip((pager.page - 1) * pager.rows).Take(pager.rows);
        }
        public bool UpdateSysRoleSysUser(string roleId, string[] userIds)
        {
            try
            {
                SysRoleRep.UpdateSysRoleSysUser(roleId, userIds);
                return true;
            }
            catch (Exception ex)
            {
                ExceptionHander.WriteException(ex);
                return false;
            }
        }

    }
}
