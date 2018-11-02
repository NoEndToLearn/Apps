using Apps.IBLL;
using Apps.IDAL;
using Apps.Models;
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
    public class AccountBLL : BaseBLL, IAccountBLL
    {
        [Dependency]
        public IAccountRepository accountRepository { get; set; }
        public SysUser Login(string username, string pwd)
        {
            return accountRepository.Login(username, pwd);

        }
    }
}
