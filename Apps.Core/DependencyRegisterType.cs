﻿using Apps.BLL;
using Apps.DAL;
using Apps.IBLL;
using Apps.IDAL;
using Apps.MIS.BLL;
using Apps.MIS.DAL;
using Apps.MIS.IBLL;
using Apps.MIS.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace Apps.Core
{
    public class DependencyRegisterType
    {
        //系统注入
        public static void Container_Sys(ref UnityContainer container)
        {
            container.RegisterType<ISysSampleBll, SysSampleBll>();//样例
            container.RegisterType<ISysSampleRepository, SysSampleRepository>();
            //
            container.RegisterType<IHomeBll, HomeBll>();
            container.RegisterType<IHomeRepository, HomeRepository>();

            container.RegisterType<ISysLogBLL, SysLogBLL>();
            container.RegisterType<ISysLogRepository, SysLogRepository>();


            container.RegisterType<ISysExceptionBLL, SysExceptionBLL>();
            container.RegisterType<ISysExceptionRepository, SysExceptionRepository>();

            container.RegisterType<IAccountBLL, AccountBLL>();
            container.RegisterType<IAccountRepository, AccountRepository>();

            container.RegisterType<ISysUserBLL, SysUserBLL>();
            container.RegisterType<ISysUserRepository, SysUserRepository>();

            container.RegisterType<ISysModuleBLL, SysModuleBLL>();
            container.RegisterType<ISysModuleRepository, SysModuleRepository>();

            container.RegisterType<ISysModuleOperateBLL, SysModuleOperateBLL>();
            container.RegisterType<ISysModuleOperateRepository, SysModuleOperateRepository>();

            container.RegisterType<ISysRoleBLL, SysRoleBLL>();
            container.RegisterType<ISysRoleRepository, SysRoleRepository>();

            container.RegisterType<ISysRightBLL, SysRightBLL>();
            container.RegisterType<ISysRightRepository, SysRightRepository>();

            #region MIS

            container.RegisterType<IMIS_Article_CategoryBLL, MIS_Article_CategoryBLL>();
            container.RegisterType<IMIS_Article_CategoryRepository, MIS_Article_CategoryRepository>();

            container.RegisterType<IMIS_ArticleBLL, MIS_ArticleBLL>();
            container.RegisterType<IMIS_ArticleRepository, MIS_ArticleRepository>();
            #endregion
        }
    }
}
