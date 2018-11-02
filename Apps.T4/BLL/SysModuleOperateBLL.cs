


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
    /// SysModuleOperate 操作类
    /// </summary>
    public class SysModuleOperateBLL: BaseBLL, ISysModuleOperateBLL
    {

        

        public static SysModuleOperate Select(String id)
        {
            using(var context = db.Context())
            {
                return context.Sql(" SELECT * FROM SysModuleOperate WHERE Id = @id ")
                    .Parameter("id", id)

                    .QuerySingle<SysModuleOperate>();
            }
        }

        public static List<SysModuleOperate> SelectAll()
        {
            return SelectAll(string.Empty);
        }

        public static List<SysModuleOperate> SelectAll(string sortExpression)
        {
            return SelectAll(0, 0, sortExpression);
        }

        public static List<SysModuleOperate> SelectAll(int startRowIndex, int maximumRows, string sortExpression)
        {
            using (var context = db.Context())
            {
                var select = context.Select<SysModuleOperate>(" * ")
                    .From(" SysModuleOperate ");

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
                return context.Sql(" SELECT COUNT(*) FROM SysModuleOperate ")
                    .QuerySingle<int>();
            }
        }

        

        public static List<SysModuleOperate> SelectBySysModule(String moduleId)
        {
            return SelectBySysModule(moduleId, string.Empty);
        }

        public static List<SysModuleOperate> SelectBySysModule(String moduleId, string sortExpression)
        {
            return SelectBySysModule(moduleId, 0, 0, sortExpression);
        }

        public static List<SysModuleOperate> SelectBySysModule(String moduleId, int startRowIndex, int maximumRows, string sortExpression)
        {
            using (var context = db.Context())
            {
                var select = context.Select<SysModuleOperate>(" * ")
                    .From(" SysModuleOperate ")
                    .Where(" ModuleId = @moduleid ")
                    .Parameter("moduleid", moduleId);

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

        public static int CountBySysModule(String moduleId)
        {
            using (var context = db.Context())
            {
                return context.Sql(" SELECT COUNT(*) FROM SysModuleOperate WHERE ModuleId = @moduleid")
                    .Parameter("moduleid", moduleId)
                    .QuerySingle<int>();
            }
        }
        


        public static bool Insert(SysModuleOperate sysModuleOperate) 
        {
            using (var context =db.Context())
            {
                return context.Insert<SysModuleOperate>("SysModuleOperate", sysModuleOperate)
                    .Execute() > 0;
            }
        }

        public static bool Update(SysModuleOperate sysModuleOperate)
        {
            using (var context = db.Context())
            {
                return context.Update<SysModuleOperate>("SysModuleOperate", sysModuleOperate)
                    .AutoMap(x => x.Id)
                    
                    .Where("Id", sysModuleOperate.Id)
                    
                    .Execute() > 0;
            }
        }

        public static bool Delete(SysModuleOperate sysModuleOperate) 
        {
            return Delete(sysModuleOperate.Id);
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
    