using Apps.Models;
using Apps.Models.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*




*/
namespace Apps.IBLL
{
    public interface IHomeBll
    {
        /// <summary>
        /// 根据用户获取系统权限
        /// </summary>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        List<SysModuleModel> GetMenuByPersonId(string personId, string moduleId);

        SysModuleModelTree GetMenu(string personId, string moduleId);
    }
}
