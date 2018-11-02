using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Apps.Web.Core
{
    public class MaxWordsExpressionAttribute : ActionFilterAttribute
    {
        public int Length { get; set; }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //    #region 到要经过执行controller里方法后 显示出页面。  
            //    //return RedirectToAction("Index");//可跳出本controller  
            //    //return RedirectToRoute(new {controller="Home",action="Index"});//可跳出本controller  
            //    //Response.Redirect("Index");//只能使用本controller下的方法名称。返回值为void  
            //    //return Redirect("Index");//只能使用本controller下的方法名称。  
            //    #endregion


            //    #region 直接显示出对应的页面 不经过执行controller的方法。  
            //    //return View("Index");//非本方法  
            //    //return  View("~/Views/Home/Index.aspx");//这种方法是写全路径  
            //    #endregion
            // filterContext.ActionDescriptor.me

            //if (filterContext.HttpContext.Session["Account"] == null)
            //{
            //    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { action = "Login", controller = "Account" }));
            //}
            //   // base.OnActionExecuting(filterContext);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);

        }
        public MaxWordsExpressionAttribute(int length)
        {
            Length = length;
        }

        //public void OnAuthorization(AuthorizationContext filterContext)
        //{
        //    //这个方法是在Action执行之前调用
        //    var user = filterContext.HttpContext.Session["username"];
        //    if (user == null)
        //    {
        //        //filterContext.HttpContext.Response.Redirect("/Account/Logon");
        //        var Url = new UrlHelper(filterContext.RequestContext);
        //        var url = Url.Action("Login", "Account", new { area = "" });
        //        filterContext.Result = new RedirectResult(url);
        //    }
        //}
    }
}