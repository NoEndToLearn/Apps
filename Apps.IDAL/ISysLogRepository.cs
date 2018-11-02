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
    public interface ISysLogRepository
    {
        int Create(SysLog entity);
        bool Delete(DBContainer db, string[] deleteCollection);
        IQueryable<SysLog> GetList(DBContainer db);
        SysLog GetById(string id);
    }
}
