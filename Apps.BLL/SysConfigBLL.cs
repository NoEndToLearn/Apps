using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apps.Models.Sys;
using System.IO;

namespace Apps.BLL
{
    public class SysConfigBLL
    {
        public SysConfigModel loadConfig(string configPath)
        {
            SysConfigModel sysConfigModel = null;
            if (File.Exists(configPath))
            {
                System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();
                xmlDoc.Load(configPath);
                sysConfigModel = XmlUtil<SysConfigModel>.Deserialize(xmlDoc.ToString());
            }
            return sysConfigModel;
        }
    }
}
