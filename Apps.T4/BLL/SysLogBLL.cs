


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
    /// SysLog 操作类
    /// </summary>
    public class SysLogBLL: BaseBLL, ISysLogBLL
    {

        

        public static SysLog Select(String id)
        {
            using(var context = db.Context())
            {
                return context.Sql(" SELECT * FROM SysLog WHERE Id = @id ")
                    .Parameter("id", id)

                    .QuerySingle<SysLog>();
            }
        }

        public static List<SysLog> SelectAll()
        {
            return SelectAll(string.Empty);
        }

        public static List<SysLog> SelectAll(string sortExpression)
        {
            return SelectAll(0, 0, sortExpression);
        }

        public static List<SysLog> SelectAll(int startRowIndex, int maximumRows, string sortExpression)
        {
            using (var context = db.Context())
            {
                var select = context.Select<SysLog>(" * ")
                    .From(" SysLog ");

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
                return context.Sql(" SELECT COUNT(*) FROM SysLog ")
                    .QuerySingle<int>();
            }
        }

        


        public static bool Insert(SysLog sysLog) 
        {
            using (var context =db.Context())
            {
                return context.Insert<SysLog>("SysLog", sysLog)
                    .Execute() > 0;
            }
        }

        public static bool Update(SysLog sysLog)
        {
            using (var context = db.Context())
            {
                return context.Update<SysLog>("SysLog", sysLog)
                    .AutoMap(x => x.Id)
                    
                    .Where("Id", sysLog.Id)
                    
                    .Execute() > 0;
            }
        }

        public static bool Delete(SysLog sysLog) 
        {
            return Delete(sysLog.Id);
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
    