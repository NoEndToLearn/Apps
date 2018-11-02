using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Unity;
using Apps.Models;
using Apps.Common;
using System.Transactions;
using Apps.Models.MIS;
using Apps.MIS.IBLL;
using Apps.MIS.IDAL;
using Apps.BLL.Core;
using Unity.Attributes;
using Apps.BLL;
using Apps.BLL.core;

namespace Apps.MIS.BLL
{
    public class MIS_ArticleBLL : BaseBLL, IMIS_ArticleBLL
    {
        [Dependency]
        public IMIS_ArticleRepository articleRep { get; set; }

        public List<MIS_ArticleModel> GetList(ref GridPager pager, string queryStr)
        {

            IQueryable<MIS_Article> queryData = null;
            if (!string.IsNullOrWhiteSpace(queryStr))
            {
                queryData = articleRep.GetList(db).Where(a => a.Title.Contains(queryStr) || a.BodyContent.Contains(queryStr));
            }
            else
            {
                queryData = articleRep.GetList(db);
            }
            pager.total = queryData.Count();
            queryData = LinqHelper.SortingAndPaging(queryData, pager.sort, pager.order, pager.page, pager.rows);
            return CreateModelList(ref queryData);
        }
        /// <summary>
        /// 获取未审核的信息
        /// </summary>
        /// <param name="pager"></param>
        /// <param name="queryStr"></param>
        /// <returns></returns>
        public List<MIS_ArticleModel> GeAuditedList(ref GridPager pager, bool isAudited, string queryStr)
        {

            IQueryable<MIS_Article> queryData = null;
            int isAudit = isAudited ? 1 : 0;
            if (!string.IsNullOrWhiteSpace(queryStr))
            {
                queryData = articleRep.GetList(db).Where(s => s.CheckFlag == isAudit).Where(a => a.Title.Contains(queryStr) || a.BodyContent.Contains(queryStr));
            }
            else
            {
                queryData = articleRep.GetList(db).Where(s => s.CheckFlag == isAudit);
            }
            pager.total = queryData.Count();
            queryData = LinqHelper.SortingAndPaging(queryData, pager.sort, pager.order, pager.page, pager.rows);
            return CreateModelList(ref queryData);
        }
        private List<MIS_ArticleModel> CreateModelList(ref IQueryable<MIS_Article> queryData)
        {

            List<MIS_ArticleModel> modelList = (from r in queryData
                                                select new MIS_ArticleModel
                                                {
                                                    BodyContent = r.BodyContent,
                                                    CategoryId = r.CategoryId,
                                                    ChannelId = r.ChannelId,
                                                    CheckDateTime = r.CheckDateTime,
                                                    Checker = r.Checker,
                                                    CheckFlag = r.CheckFlag,
                                                    Click = r.Click,
                                                    Creater = r.Creater,
                                                    CreateTime = r.CreateTime,
                                                    Id = r.Id,
                                                    ImgUrl = r.ImgUrl,
                                                    Sort = r.Sort,
                                                    Title = r.Title
                                                }).ToList();
            return modelList;
        }

        public bool Create(ref ValidationErrors errors, MIS_ArticleModel model)
        {
            try
            {
                MIS_Article entity = articleRep.GetById(model.Id);
                if (entity != null)
                {
                    errors.Add(Suggestion.PrimaryRepeat);
                    return false;
                }
                entity = new MIS_Article();
                entity.BodyContent = model.BodyContent;
                entity.CategoryId = model.CategoryId;
                entity.ChannelId = model.ChannelId;
                entity.CheckDateTime = model.CheckDateTime;
                entity.Checker = model.Checker;
                entity.CheckFlag = model.CheckFlag;
                entity.Click = model.Click;
                entity.Creater = model.Creater;
                entity.CreateTime = model.CreateTime;
                entity.Id = model.Id;
                entity.ImgUrl = model.ImgUrl;
                entity.Sort = model.Sort;
                entity.Title = model.Title;
                if (articleRep.Create(entity) == 1)
                {
                    return true;
                }
                else
                {
                    errors.Add(Suggestion.InsertFail);
                    return false;
                }
            }
            catch (Exception ex)
            {
                errors.Add(ex.Message);
                ExceptionHander.WriteException(ex);
                return false;
            }
        }
        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="errors"></param>
        /// <param name="id"></param>
        /// <param name="currUserId"></param>
        /// <returns></returns>
        public bool Audit(ref ValidationErrors errors, string id,string currUserId)
        {
            try
            {
                MIS_Article entity = articleRep.GetById(id);
                entity.CheckFlag = 1;
                entity.CheckDateTime = DateTime.Now;
                entity.Checker = currUserId;
                if (articleRep.Edit(entity) == 1)
                {
                    return true;
                }
                else
                {
                    errors.Add(Suggestion.EditFail);
                    return false;
                }
            }
            catch (Exception ex)
            {
                errors.Add(ex.Message);
                ExceptionHander.WriteException(ex);
                return false;
            }
        }

        public bool Delete(ref ValidationErrors errors, string id)
        {
            try
            {
                if (articleRep.Delete(id) == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                errors.Add(ex.Message);
                ExceptionHander.WriteException(ex);
                return false;
            }
        }

        public bool Delete(ref ValidationErrors errors, string[] deleteCollection)
        {
            try
            {
                if (deleteCollection != null)
                {
                    using (TransactionScope transactionScope = new TransactionScope())
                    {
                        articleRep.Delete(db, deleteCollection);
                        if (db.SaveChanges() == deleteCollection.Length)
                        {
                            transactionScope.Complete();
                            return true;
                        }
                        else
                        {
                            Transaction.Current.Rollback();
                            return false;
                        }
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                errors.Add(ex.Message);
                ExceptionHander.WriteException(ex);
                return false;
            }
        }
        public bool Edit(ref ValidationErrors errors, MIS_ArticleModel model)
        {
            try
            {
                MIS_Article entity = articleRep.GetById(model.Id);
                if (entity == null)
                {
                    errors.Add(Suggestion.Disable);
                    return false;
                }
                entity.BodyContent = model.BodyContent;
                entity.CategoryId = model.CategoryId;
                entity.ChannelId = model.ChannelId;
                entity.CheckDateTime = model.CheckDateTime;
                entity.Checker = model.Checker;
                entity.CheckFlag = model.CheckFlag;
                entity.Click = model.Click;
                entity.Creater = model.Creater;
                entity.CreateTime = model.CreateTime;
                entity.Id = model.Id;
                entity.ImgUrl = model.ImgUrl;
                entity.Sort = model.Sort;
                entity.Title = model.Title;

                if (articleRep.Edit(entity) == 1)
                {
                    return true;
                }
                else
                {
                    errors.Add(Suggestion.EditFail);
                    return false;
                }
            }
            catch (Exception ex)
            {
                errors.Add(ex.Message);
                ExceptionHander.WriteException(ex);
                return false;
            }
        }

        public bool IsExists(string id)
        {
            if (db.MIS_Article.SingleOrDefault(a => a.Id == id) != null)
            {
                return true;
            }
            return false;
        }

        public MIS_ArticleModel GetById(string id)
        {
            if (IsExist(id))
            {
                MIS_Article entity = articleRep.GetById(id);
                MIS_ArticleModel model = new MIS_ArticleModel();
                model.BodyContent = entity.BodyContent;
                model.CategoryId = entity.CategoryId;
                model.ChannelId = entity.ChannelId;
                model.CheckDateTime = entity.CheckDateTime;
                model.Checker = entity.Checker;
                model.CheckFlag = entity.CheckFlag;
                model.Click = entity.Click;
                model.Creater = entity.Creater;
                model.CreateTime = entity.CreateTime;
                model.Id = entity.Id;
                model.ImgUrl = entity.ImgUrl;
                model.Sort = entity.Sort;
                model.Title = entity.Title;
                return model;
            }
            else
            {
                return null;
            }
        }

        public bool IsExist(string id)
        {
            return articleRep.IsExist(id);
        }
    }
}
