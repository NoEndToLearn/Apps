using Apps.BLL.core;
using Apps.Common;
using Apps.DAL;
using Apps.IBLL;
using Apps.IDAL;
using Apps.Models;
using Apps.Models.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Attributes;

namespace Apps.BLL
{
    public class SysSampleBll : ISysSampleBll
    {
        readonly DBContainer db = new DBContainer();


        [Dependency]
        public ISysSampleRepository Rep { get; set; }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pager">JQgrid分页</param>
        /// <param name="queryStr">搜索条件</param>
        /// <returns>列表</returns>
        public List<SysSampleModel> GetList(ref GridPager pager, string queryString)
        {

            IQueryable<SysSample> queryData = null;


            queryData = queryString.IsNullOrEmptyOrSpace() ? Rep.GetList(db) : Rep.GetList(db).Where(s => true);
            queryData = LinqHelper.DataSorting(queryData, pager.sort, pager.order);
            //if (pager.order == "desc")
            //{
            //    switch (pager.sort)
            //    {
            //        case "Id":
            //            queryData = queryData.OrderByDescending(c => c.Id);
            //            break;
            //        case "Name":
            //            queryData = queryData.OrderByDescending(c => c.Id);
            //            break;
            //        default:
            //            queryData = queryData.OrderByDescending(c => c.CreateTime);
            //            break;
            //    }
            //}
            //else
            //{
            //    switch (pager.sort)
            //    {
            //        case "Id":
            //            queryData = queryData.OrderBy(c => c.Id);
            //            break;
            //        case "Name":
            //            queryData = queryData.OrderBy(c => c.Id);
            //            break;
            //        default:
            //            queryData = queryData.OrderBy(c => c.CreateTime);
            //            break;
            //    }
            //}
            return CreateModelList(ref queryData, ref pager);
        }
        private List<SysSampleModel> CreateModelList(ref IQueryable<SysSample> queryData, ref GridPager pager)
        {

            pager.total = queryData.Count();
            if (pager.total > 0)
            {
                if (pager.page <= 1)
                {
                    queryData = queryData.Take(pager.rows);
                }
                else
                {
                    queryData = queryData.Skip((pager.page - 1) * pager.rows).Take(pager.rows);
                }
            }
            List<SysSampleModel> modelList = (from r in queryData
                                              select new SysSampleModel
                                              {
                                                  Id = r.Id,
                                                  Name = r.Name,
                                                  Age = r.Age,
                                                  Bir = r.Bir,
                                                  Photo = r.Photo,
                                                  Note = r.Note,
                                                  CreateTime = r.CreateTime,

                                              }).ToList();

            return modelList;
        }

        /// <summary>
        /// 创建一个实体
        /// </summary>
        /// <param name="errors">持久的错误信息</param>
        /// <param name="model">模型</param>
        /// <returns>是否成功</returns>
        public bool Create(ref ValidationErrors errors, SysSampleModel model)
        {
            try
            {
                SysSample entity = Rep.GetById(model.Id.ToString());
                if (entity != null)
                {
                    errors.Add("主键重复");
                    return false;
                }
                entity = new SysSample();
                entity.Id = model.Id;
                entity.Name = model.Name;
                entity.Age = model.Age;
                entity.Bir = model.Bir;
                entity.Photo = model.Photo;
                entity.Note = model.Note;
                entity.CreateTime = model.CreateTime;

                if (Rep.Create(entity) == 1)
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
        /// <summary>
        /// 删除一个实体
        /// </summary>
        /// <param name="errors">持久的错误信息</param>
        /// <param name="id">id</param>
        /// <returns>是否成功</returns>
        public bool Delete(string id)
        {
            try
            {
                if (Rep.Delete(id))
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
                return false;
            }
        }

        /// <summary>
        /// 修改一个实体
        /// </summary>
        /// <param name="errors">持久的错误信息</param>
        /// <param name="model">模型</param>
        /// <returns>是否成功</returns>
        public bool Edit(SysSampleModel model)
        {
            try
            {
                SysSample entity = Rep.GetById(model.Id.ToString());
                if (entity == null)
                {
                    return false;
                }
                entity.Name = model.Name;
                entity.Age = model.Age;
                entity.Bir = model.Bir;
                entity.Photo = model.Photo;
                entity.Note = model.Note;


                if (Rep.Edit(entity) == 1)
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

                //ExceptionHander.WriteException(ex);
                return false;
            }
        }
        /// <summary>
        /// 判断是否存在实体
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <returns>是否存在</returns>
        public bool IsExists(string id)
        {
            if (db.SysSample.SingleOrDefault(a => a.Id.ToString() == id) != null)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 根据ID获得一个实体
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>实体</returns>
        public SysSampleModel GetById(string id)
        {
            if (IsExist(id))
            {
                SysSample entity = Rep.GetById(id);
                SysSampleModel model = new SysSampleModel();
                model.Id = entity.Id;
                model.Name = entity.Name;
                model.Age = entity.Age;
                model.Bir = entity.Bir;
                model.Photo = entity.Photo;
                model.Note = entity.Note;
                model.CreateTime = entity.CreateTime;

                return model;
            }
            else
            {
                return new SysSampleModel();
            }
        }

        /// <summary>
        /// 判断一个实体是否存在
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>是否存在 true or false</returns>
        public bool IsExist(string id)
        {
            return Rep.IsExist(id);
        }
    }
}
