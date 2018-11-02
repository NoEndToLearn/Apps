using Apps.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apps.Models.Sys;
using Apps.Models;


/*




*/
namespace Apps.DAL
{
    public class SysRightRepository : ISysRightRepository, IDisposable
    {
        /// <summary>
        /// 取角色模块的操作权限，用于权限控制
        /// </summary>
        /// <param name="accountid">acount Id</param>
        /// <param name="controller">url</param>
        /// <returns></returns>
        public List<PermModel> GetPermission(string accountid, string controller)
        {

            using (DBContainer db = new DBContainer())
            {
                List<PermModel> rights = (from r in db.P_Sys_GetRightOperate(accountid, controller)
                                          select new PermModel
                                          {
                                              KeyCode = r.KeyCode,
                                              IsValid = r.IsValid
                                          }).ToList();
                return rights;
            }
        }


        public IQueryable<SysRight> GetList(DBContainer db)
        {
            IQueryable<SysRight> list = db.SysRight.AsQueryable();
            return list;
        }

        public int Create(SysRight entity)
        {
            using (DBContainer db = new DBContainer())
            {
                db.SysRight.Add(entity);
                return db.SaveChanges();
            }
        }

        public int Delete(string id)
        {
            using (DBContainer db = new DBContainer())
            {
                SysRight entity = db.SysRight.SingleOrDefault(a => a.Id == id);
                if (entity != null)
                {

                    db.SysRight.Remove(entity);
                }
                return db.SaveChanges();
            }
        }

        public void Delete(DBContainer db, string[] deleteCollection)
        {
            IQueryable<SysRight> collection = from f in db.SysRight
                                              where deleteCollection.Contains(f.Id)
                                              select f;
            foreach (var deleteItem in collection)
            {
                db.SysRight.Remove(deleteItem);
            }
        }

        public int Edit(SysRight entity)
        {
            using (DBContainer db = new DBContainer())
            {
                // db.SysRight.Attach(entity);
                //  db.ObjectStateManager.ChangeObjectState(entity, EntityState.Modified);
                db.Set<SysRight>().Attach(entity);
                db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                return db.SaveChanges();
            }
        }

        public SysRight GetById(string id)
        {
            using (DBContainer db = new DBContainer())
            {
                return db.SysRight.SingleOrDefault(a => a.Id == id);
            }
        }

        public bool IsExist(string id)
        {
            using (DBContainer db = new DBContainer())
            {
                SysRight entity = GetById(id);
                if (entity != null)
                    return true;
                return false;
            }
        }
        public void Dispose()
        {

        }

        public int UpdateRight(SysRightOperate model)
        {
            //转换
            SysRightOperate rightOperate = new SysRightOperate();
            rightOperate.Id = model.Id;
            rightOperate.RightId = model.RightId;
            rightOperate.KeyCode = model.KeyCode;
            rightOperate.IsValid = model.IsValid;
            //判断rightOperate是否存在，如果存在就更新rightOperate,否则就添加一条
            using (DBContainer db = new DBContainer())
            {
                SysRightOperate right = db.SysRightOperate.Where(a => a.Id == rightOperate.Id).FirstOrDefault();
                if (right != null)
                {
                    right.IsValid = rightOperate.IsValid;
                }
                else
                {
                    db.SysRightOperate.Add(rightOperate);
                }
                if (db.SaveChanges() > 0)
                {
                    //更新角色--模块的有效标志RightFlag
                    var sysRight = (from r in db.SysRight
                                    where r.Id == rightOperate.RightId
                                    select r).First();
                    db.P_Sys_UpdateSysRightRightFlag(sysRight.ModuleId, sysRight.RoleId);
                    return 1;
                }
            }
            return 0;
        }
        //按选择的角色及模块加载模块的权限项
        public List<P_Sys_GetRightByRoleAndModule_Result> GetRightByRoleAndModule(string roleId, string moduleId)
        {
            List<P_Sys_GetRightByRoleAndModule_Result> result = null;
            using (DBContainer db = new DBContainer())
            {
                result = db.P_Sys_GetRightByRoleAndModule(roleId, moduleId).ToList();
            }
            return result;
        }
    }
}
