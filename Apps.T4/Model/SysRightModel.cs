using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由T4模板自动生成
//     生成时间2018-10-23 11:55:30
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------
namespace Apps.Models.Sys
{    
    /// <summary>
    /// SysRightModel
    /// </summary>
    public class SysRightModel    
    {    
        /// <summary>
          ///ID
          /// </summary>
          [NotNullExpression]
          [Required]
          [MaxWordsExpression(200)]
          [Display(Name="ID")]
          public string Id { get; set; }
          /// <summary>
          ///模块编码
          /// </summary>
          [NotNullExpression]
          [MaxWordsExpression(50)]
          [Display(Name="模块编码")]
          public string ModuleId { get; set; }
          /// <summary>
          ///角色编码
          /// </summary>
          [NotNullExpression]
          [MaxWordsExpression(50)]
          [Display(Name="角色编码")]
          public string RoleId { get; set; }
          /// <summary>
          ///是否授权
          /// </summary>
          [NotNullExpression]
          [MaxWordsExpression(1)]
          [Display(Name="是否授权")]
          public bool Rightflag { get; set; }

    }
}
    
