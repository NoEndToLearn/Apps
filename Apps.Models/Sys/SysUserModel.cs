using System;
using System.ComponentModel.DataAnnotations;
using Apps.Models;
using System.Collections;

namespace Apps.Models.Sys
{
    public class SysUserModel
    {
        //   [MaxWordsExpression(50)]
        [Display(Name = "用户编码")]
        public string Id { get; set; }

        //   [MaxWordsExpression(200)]
        [Display(Name = "用户名")]
        public string UserName { get; set; }

        //   [MaxWordsExpression(200)]
        [Display(Name = "密码")]
        public string Password { get; set; }

        //   [MaxWordsExpression(200)]
        [Display(Name = "真实姓名")]
        public string TrueName { get; set; }

        //   [MaxWordsExpression(50)]
        [Display(Name = "Card")]
        public string Card { get; set; }

        //   [MaxWordsExpression(200)]
        [Display(Name = "身份证")]
        public string MobileNumber { get; set; }

        //   [MaxWordsExpression(200)]
        [Display(Name = "联系电话")]
        public string PhoneNumber { get; set; }

        //  [MaxWordsExpression(50)]
        [Display(Name = "QQ")]
        public string QQ { get; set; }
        //
        //   [MaxWordsExpression(200)]
        [Display(Name = "邮件地址")]
        public string EmailAddress { get; set; }

        //  [MaxWordsExpression(200)]
        [Display(Name = "其他联系方式")]
        public string OtherContact { get; set; }

        //  [MaxWordsExpression(200)]
        [Display(Name = "省份")]
        public string Province { get; set; }

        //  [MaxWordsExpression(200)]
        [Display(Name = "城市")]
        public string City { get; set; }

        //  [MaxWordsExpression(200)]
        [Display(Name = "Village")]
        public string Village { get; set; }

        //  [MaxWordsExpression(200)]
        [Display(Name = "地址")]
        public string Address { get; set; }

        [Display(Name = "状态")]
        public bool State { get; set; }

        [Display(Name = "创建时间")]
        public DateTime? CreateTime { get; set; }

        // [MaxWordsExpression(200)]
        [Display(Name = "创建人")]
        public string CreatePerson { get; set; }

        //  [MaxWordsExpression(10)]
        [Display(Name = "性别")]
        public string Sex { get; set; }

        [Display(Name = "生日")]
        public DateTime? Birthday { get; set; }

        [Display(Name = "加入日期")]
        public DateTime? JoinDate { get; set; }

        //   [MaxWordsExpression(10)]
        [Display(Name = "婚姻")]
        public string Marital { get; set; }

        //   [MaxWordsExpression(50)]
        [Display(Name = "党派")]
        public string Political { get; set; }

        //   [MaxWordsExpression(20)]
        [Display(Name = "民族")]
        public string Nationality { get; set; }

        //   [MaxWordsExpression(20)]
        [Display(Name = "籍贯")]
        public string Native { get; set; }

        //   [MaxWordsExpression(50)]
        [Display(Name = "毕业学校")]
        public string School { get; set; }

        //   [MaxWordsExpression(100)]
        [Display(Name = "就读专业")]
        public string Professional { get; set; }

        //   [MaxWordsExpression(20)]
        [Display(Name = "学历")]
        public string Degree { get; set; }

        //   [MaxWordsExpression(50)]
        [Display(Name = "部门")]
        public string DepId { get; set; }

        //   [MaxWordsExpression(50)]
        [Display(Name = "职位")]
        public string PosId { get; set; }

        //   [MaxWordsExpression(3000)]
        [Display(Name = "个人简介")]
        public string Expertise { get; set; }

        //   [MaxWordsExpression(20)]
        [Display(Name = "在职状况")]
        public string JobState { get; set; }

        //    [MaxWordsExpression(200)]
        [Display(Name = "照片")]
        public string Photo { get; set; }

        //     [MaxWordsExpression(200)]
        [Display(Name = "附件")]
        public string Attach { get; set; }

        [Display(Name = "拥有角色")]
        public string HasRoles { get; set; }
        [Display(Name ="是否分配")]
        public string Flag { get; set; }
    }
}
