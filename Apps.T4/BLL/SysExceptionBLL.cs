


using System;
using System.Collections.Generic;
using Apps.IBLL;
using Apps.IDAL;
using Apps.Models;
using Apps.Models.Sys;
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由T4模板自动生成
//     生成时间2018-10-23 11:20:12
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------
namespace Apps.BLL
{    
    
    /// <summary>
    /// SysException 操作类
    /// </summary>
    public class SysExceptionBLL: BaseBLL, ISysExceptionBLL
    {

        

        public static SysException Select(String id)
        {
            using(var context = db.Context())
            {
                return context.Sql(" SELECT * FROM SysException WHERE Id = @id ")
                    .Parameter("id", id)

                    .QuerySingle<SysException>();
            }
        }

        public static List<SysException> SelectAll()
        {
            return SelectAll(string.Empty);
        }

        public static List<SysException> SelectAll(string sortExpression)
        {
            return SelectAll(0, 0, sortExpression);
        }

        public static List<SysException> SelectAll(int startRowIndex, int maximumRows, string sortExpression)
        {
            using (var context = db.Context())
            {
                var select = context.Select<SysException>(" * ")
                    .From(" SysException ");

                if (maximumRows > 0)
                {
                    if (startRowIndex == 0) 
                        startRowIndex = 1;

                    select.Paging(startRowIndex, maximumRows);
                }

                if (!string.IsNullOrEmpty(sortExpression))
                    select.OrderBy(sortExpression);

                return select.QueryMany();
            }
        }

        public static int CountAll()
        {
            using (var context = db.Context())
            {
                return context.Sql(" SELECT COUNT(*) FROM SysException ")
                    .QuerySingle<int>();
            }
        }

        


        public static bool Insert(SysException sysException) 
        {
            using (var context =db.Context())
            {
                return context.Insert<SysException>("SysException", sysException)
                    .Execute() > 0;
            }
        }

        public static bool Update(SysException sysException)
        {
            using (var context = db.Context())
            {
                return context.Update<SysException>("SysException", sysException)
                    .AutoMap(x => x.Id)
                    
                    .Where("Id", sysException.Id)
                    
                    .Execute() > 0;
            }
        }

        public static bool Delete(SysException sysException) 
        {
            return Delete(sysException.Id);
        }

        public static bool Delete(String id)
        {
            using (var context = db.Context())
            {
                return context.Sql(" DELETE FROM Product WHERE Id = @id ")
                    .Parameter("id", id)

                    .Execute() > 0;
            }
        }
    }
    
}
    
