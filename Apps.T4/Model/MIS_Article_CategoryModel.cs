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
    /// MIS_Article_CategoryModel
    /// </summary>
    public class MIS_Article_CategoryModel    
    {    
        /// <summary>
          ///
          /// </summary>
          [NotNullExpression]
          [Required]
          [MaxWordsExpression(50)]
          [Display(Name="Id?")]
          public string Id { get; set; }
          /// <summary>
          ///
          /// </summary>
          [MaxWordsExpression(4)]
          [Display(Name="ChannelId?")]
          public int? ChannelId { get; set; }
          /// <summary>
          ///
          /// </summary>
          [NotNullExpression]
          [MaxWordsExpression(100)]
          [Display(Name="Name?")]
          public string Name { get; set; }
          /// <summary>
          ///
          /// </summary>
          [MaxWordsExpression(50)]
          [Display(Name="ParentId?")]
          public string ParentId { get; set; }
          /// <summary>
          ///
          /// </summary>
          [MaxWordsExpression(4)]
          [Display(Name="Sort?")]
          public int? Sort { get; set; }
          /// <summary>
          ///
          /// </summary>
          [MaxWordsExpression(255)]
          [Display(Name="ImgUrl?")]
          public string ImgUrl { get; set; }
          /// <summary>
          ///
          /// </summary>
          [MaxWordsExpression(8000)]
          [Display(Name="BodyContent?")]
          public string BodyContent { get; set; }
          /// <summary>
          ///
          /// </summary>
          [DateExpression]
          [MaxWordsExpression(8)]
          [Display(Name="CreateTime?")]
          public DateTime? CreateTime { get; set; }
          /// <summary>
          ///
          /// </summary>
          [NotNullExpression]
          [MaxWordsExpression(1)]
          [Display(Name="Enable?")]
          public bool Enable { get; set; }

    }
}
    