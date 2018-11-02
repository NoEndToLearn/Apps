using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Apps.Models;

/*




*/
namespace Apps.BLL
{
    public class BaseBLL : IDisposable
    {
        //用base类去做统一的实例化
        private DBContainer _entity = new DBContainer();

        public DBContainer db
        {
            get { return _entity; }
        }

        public void Dispose()
        {

        }
    }
}
