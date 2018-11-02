using Apps.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*




*/
namespace Apps.IDAL
{
    public interface IHomeRepository
    {
        List<SysModule> GetMenuByPersonId(string personId,string moduleId);

        List<SysModule> GetMenuTree(string personId, string moduleId);
    }
}
