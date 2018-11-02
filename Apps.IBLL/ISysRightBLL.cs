using System.Collections.Generic;
using Apps.Common;
using Apps.Models.Sys;
using Apps.Models;

namespace Apps.IBLL
{
    public interface ISysRightBLL
    {
        List<PermModel> GetPermission(string accountid, string controllor);

        List<P_Sys_GetRightByRoleAndModule_Result> GetRightByRoleAndModule(string roleId, string moduleId);

        int UpdateRight(SysRightOperate model);

        List<SysRightModel> GetList(ref GridPager pager, string queryStr);
        bool Create(ref ValidationErrors errors, SysRightModel model);
        bool Delete(ref ValidationErrors errors, string id);
        bool Delete(ref ValidationErrors errors, string[] deleteCollection);
        bool Edit(ref ValidationErrors errors, SysRightModel model);
        SysRightModel GetById(string id);
        bool IsExist(string id);
    }
}
