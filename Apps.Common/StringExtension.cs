using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*




*/
namespace Apps.Common
{
    public static class StringExtension
    {
        /// <summary>
        /// 判断字符串是否为空 空格 null
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsNullOrEmptyOrSpace(this string source)
        {
            return string.IsNullOrWhiteSpace(source)|| string.IsNullOrEmpty(source);
        }
    }
}
