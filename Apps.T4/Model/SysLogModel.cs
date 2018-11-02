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
    /// SysLogModel
    /// </summary>
    public class SysLogModel    
    {    
        /// <summary>
          ///编号
          /// </summary>
          [NotNullExpression]
          [Required]
          [MaxWordsExpression(50)]
          [Display(Name="编号")]
          public string Id { get; set; }
          /// <summary>
          ///操作人
          /// </summary>
          [MaxWordsExpression(50)]
          [Display(Name="操作人")]
          public string Operator { get; set; }
          /// <summary>
          ///信息
          /// </summary>
          [MaxWordsExpression(500)]
          [Display(Name="信息")]
          public string Message { get; set; }
          /// <summary>
          ///结果
          /// </summary>
          [MaxWordsExpression(20)]
          [Display(Name="结果")]
          public string Result { get; set; }
          /// <summary>
          ///类型
          /// </summary>
          [MaxWordsExpression(20)]
          [Display(Name="类型")]
          public string Type { get; set; }
          /// <summary>
          ///模块
          /// </summary>
          [MaxWordsExpression(256)]
          [Display(Name="模块")]
          public string Module { get; set; }
          /// <summary>
          ///创建时间
          /// </summary>
          [DateExpression]
          [MaxWordsExpression(8)]
          [Display(Name="创建时间")]
          public DateTime? CreateTime { get; set; }

    }
}
    
