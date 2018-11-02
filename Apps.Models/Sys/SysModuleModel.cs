using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*




*/
namespace Apps.Models.Sys
{
   public class SysModuleModel
    {
        public SysModuleModel()
        {

        }
        [Display(Name="编号")]
        public string Id { get; set; }
        [Display(Name = "名称")]
        public string Name { get; set; }
        [Display(Name = "英文名称")]
        public string EnglishName { get; set; }
        [Display(Name = "父级编号")]
        public string ParentId { get; set; }
        [Display(Name = "URL")]
        public string Url { get; set; }
        [Display(Name = "图标")]
        public string Iconic { get; set; }
        [Display(Name = "排序")]
        public int? Sort { get; set; }
        [Display(Name = "备注")]
        public string Remark { get; set; }
        [Display(Name = "状态")]
        public bool Enable { get; set; }
        [Display(Name = "创建人")]
        public string CreatePerson { get; set; }
        [Display(Name = "创建时间")]
        public DateTime? CreateTime { get; set; }
        [Display(Name = "是否是最后")]
        public bool IsLast { get; set; }
        [Display(Name = "版本")]
        public byte[] Version { get; set; }

        public string state { get; set; }//treegrid
    }
}
