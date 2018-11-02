using System;
using System.ComponentModel.DataAnnotations;
using Apps.Models;
namespace Apps.Models.Sys
{
    public class SysRightModel
    {
      //  [MaxWordsExpression(200)]
        [Display(Name = "Id")]
        public string Id { get; set; }

      //  [MaxWordsExpression(50)]
        [Display(Name = "ModuleId")]
        public string ModuleId { get; set; }

       // [MaxWordsExpression(50)]
        [Display(Name = "RoleId")]
        public string RoleId { get; set; }

        [Display(Name = "Rightflag")]
        public bool Rightflag { get; set; }

    }
}
