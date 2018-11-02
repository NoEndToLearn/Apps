using System;
using System.Configuration;
using System.IO;
using System.Web;

namespace Apps.Common
{
    public class Utils
    {
        #region 获得配置文件节点XML文件的绝对路径

        public static string GetXmlMapPath(string xmlName)
        {
            return GetMapPath(ConfigurationManager.AppSettings[xmlName].ToString());
        }
        #endregion

        #region 获得当前绝对路径

        /// <summary>
        /// 获得当前绝对路径
        /// </summary>
        /// <param name="strPath">指定的路径</param>
        /// <returns>绝对路径</returns>
        public static string GetMapPath(string strPath)
        {
            if (strPath.ToLower().StartsWith("http://"))
            {
                return strPath;
            }

            if (HttpContext.Current != null)
            {
                return HttpContext.Current.Server.MapPath(strPath);
            }
            else // 非web程序引用
            {
                strPath = strPath.Replace("/", "\\");
                if (strPath.StartsWith("\\"))
                {
                    strPath = strPath.Substring(strPath.IndexOf('\\', 1)).TrimStart('\\');
                }

                return System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, strPath);
            }
        }

        public static void DeleteUpFile(string delfile)
        {
            string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, delfile);
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
        #endregion

    }
}
