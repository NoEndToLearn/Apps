using Apps.Common;
using Apps.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*




*/
namespace Apps.IBLL
{
    public interface ISysLogBLL
    {
        List<SysLog> GetList(ref GridPager pager, string queryStr);
        SysLog GetById(string id);

        bool DeleteById(string id);
    }
}
