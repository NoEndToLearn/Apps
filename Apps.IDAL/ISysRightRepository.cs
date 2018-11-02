using Apps.Models;
using Apps.Models.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*




*/
namespace Apps.IDAL
{
   public interface ISysRightRepository
    {
        List<PermModel> GetPermission(string accountid, string controller);

        IQueryable<SysRight> GetList(DBContainer db);
        int Create(SysRight entity);
        int Delete(string id);
        void Delete(DBContainer db, string[] deleteCollection);
        int Edit(SysRight entity);
        SysRight GetById(string id);
        bool IsExist(string id);
        //更新
        int UpdateRight(SysRightOperate model);
        //按选择的角色及模块加载模块的权限项
        List<P_Sys_GetRightByRoleAndModule_Result> GetRightByRoleAndModule(string roleId, string moduleId);
    }
}
