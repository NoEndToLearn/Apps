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
    public interface ISysExceptionRepository
    {
        int Create(SysException entity);
        IQueryable<SysException> GetList(DBContainer db);
        SysException GetById(string id);
    }
}
