using Apps.IDAL;
using Apps.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Apps.DAL
{
    public class SysSampleRepository : ISysSampleRepository, IDisposable
    {  /// <summary>
    /// 获取列表
    ///</summary>
        /// <param name="db">数据库上下文</param>
        /// <returns>数据列表</returns>
        public IQueryable<SysSample> GetList(DBContainer db)
        {
            IQueryable<SysSample>
                list = db.SysSample.AsQueryable();
            return list;
        }
        /// <summary>
            /// 创建一个实体
            ///</summary>
        /// <param name="db">数据库上下文</param>
        /// <param name="entity">实体</param>
        public int Create(SysSample entity)
        {
            using (DBContainer db = new DBContainer())
            {
                db.Set<SysSample>().Add(entity);
                return db.SaveChanges();
            }
        }
        /// <summary>
            /// 删除一个实体
            ///</summary>
        /// <param name="db">数据库上下文</param>
        /// <param name="entity">主键ID</param>
        public bool Delete(string id)
        {
            using (DBContainer db = new DBContainer())
            {
                SysSample entity = db.SysSample.SingleOrDefault(a => a.Id.ToString() == id);
                db.Set<SysSample>().Remove(entity);
                return db.SaveChanges() > 0;
            }
        }

        /// <summary>
            /// 修改一个实体
            ///</summary>
        /// <param name="db">数据库上下文</param>
        /// <param name="entity">实体</param>
        public int Edit(SysSample entity)
        {
            using (DBContainer db = new DBContainer())
            {
                db.Set<SysSample>().Attach(entity);
                db.Entry(entity).State = EntityState.Modified;
                return db.SaveChanges();
            }
        }
        /// <summary>
            /// 获得一个实体
            ///</summary>
        /// <param name="id">id</param>
        /// <returns>实体</returns>
        public SysSample GetById(string id)
        {
            using (DBContainer db = new DBContainer())
            {
                return db.SysSample.SingleOrDefault(a => a.Id.ToString() == id);
            }
        }
        /// <summary>
            /// 判断一个实体是否存在
            ///</summary>
        /// <param name="id">id</param>
        /// <returns>是否存在 true or false</returns>
        public bool IsExist(string id)
        {
            using (DBContainer db = new DBContainer())
            {
                SysSample entity = GetById(id);
                if (entity != null)
                    return true;
                return false;
            }
        }
        public void Dispose()
        {
            // Method intentionally left empty.
        }
    }
}
