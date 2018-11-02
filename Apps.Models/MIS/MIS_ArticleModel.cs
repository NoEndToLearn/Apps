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
namespace Apps.Models.MIS
{    
    /// <summary>
    /// MIS_ArticleModel
    /// </summary>
    public class MIS_ArticleModel    
    {    
        /// <summary>
          ///
          /// </summary>
          //[NotNullExpression]
          //[Required]
          [MaxWordsExpression(50)]
          [Display(Name="ID")]
          public string Id { get; set; }
          /// <summary>
          ///
          /// </summary>
         // [NotNullExpression]
          [MaxWordsExpression(4)]
          [Display(Name="频道编码")]
          public int ChannelId { get; set; }
          /// <summary>
          ///
          /// </summary>
        //  [NotNullExpression]
          [MaxWordsExpression(50)]
          [Display(Name="分类编码")]
          public string CategoryId { get; set; }
          /// <summary>
          ///
          /// </summary>
        //  [NotNullExpression]
          [MaxWordsExpression(100)]
          [Display(Name="标题")]
          public string Title { get; set; }
          /// <summary>
          ///
          /// </summary>
          [MaxWordsExpression(255)]
          [Display(Name="图片路径")]
          public string ImgUrl { get; set; }
          /// <summary>
          ///
          /// </summary>
          [MaxWordsExpression(8000)]
          [Display(Name="内容")]
          public string BodyContent { get; set; }
          /// <summary>
          ///
          /// </summary>
          [MaxWordsExpression(4)]
          [Display(Name="排序")]
          public int? Sort { get; set; }
          /// <summary>
          ///
          /// </summary>
          [MaxWordsExpression(4)]
          [Display(Name="点击数")]
          public int? Click { get; set; }
          /// <summary>
          ///
          /// </summary>
         // [NotNullExpression]
          [MaxWordsExpression(4)]
          [Display(Name="是否审核通过")]
          public int CheckFlag { get; set; }
          /// <summary>
          ///
          /// </summary>
          [MaxWordsExpression(50)]
          [Display(Name="审核人")]
          public string Checker { get; set; }
          /// <summary>
          ///
          /// </summary>
          //[DateExpression]
          [MaxWordsExpression(8)]
          [Display(Name="审核时间")]
          public DateTime? CheckDateTime { get; set; }
          /// <summary>
          ///
          /// </summary>
          [MaxWordsExpression(50)]
          [Display(Name="创建人")]
          public string Creater { get; set; }
          /// <summary>
          ///
          /// </summary>
        //  [DateExpression]
          [MaxWordsExpression(8)]
          [Display(Name="创建时间")]
          public DateTime? CreateTime { get; set; }

    }
}
    