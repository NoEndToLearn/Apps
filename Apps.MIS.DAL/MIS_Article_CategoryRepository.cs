using System;
using System.Linq;
using Apps.MIS.IDAL;
using Apps.Models;
using System.Data;

namespace Apps.MIS.DAL
{
    public class MIS_Article_CategoryRepository : IMIS_Article_CategoryRepository, IDisposable
    {

        public IQueryable<MIS_Article_Category> GetList(DBContainer db)
        {
            IQueryable<MIS_Article_Category> list = db.MIS_Article_Category.AsQueryable();
            return list;
        }

        public int Create(MIS_Article_Category entity)
        {
            using (DBContainer db = new DBContainer())
            {
               // db.MIS_Article_Category.Remove(entity);
                db.Set<MIS_Article_Category>().Attach(entity);
                db.Entry(entity).State = System.Data.Entity.EntityState.Added;
                return db.SaveChanges();
            }
        }

        public int Delete(string id)
        {
            using (DBContainer db = new DBContainer())
            {
                MIS_Article_Category entity = db.MIS_Article_Category.SingleOrDefault(a => a.Id == id);
                if (entity != null)
                {
                    db.Set<MIS_Article_Category>().Attach(entity);
                    db.Entry(entity).State = System.Data.Entity.EntityState.Deleted;
                    db.MIS_Article_Category.Remove(entity);
                }
                return db.SaveChanges();
            }
        }

        public void Delete(DBContainer db, string[] deleteCollection)
        {
            IQueryable<MIS_Article_Category> collection = from f in db.MIS_Article_Category
                                                          where deleteCollection.Contains(f.Id)
                                                          select f;
            foreach (var deleteItem in collection)
            {
                db.MIS_Article_Category.Remove(deleteItem);
            }
        }

        public int Edit(MIS_Article_Category entity)
        {
            using (DBContainer db = new DBContainer())
            {
                //db.MIS_Article_Category.Attach(entity);
                //db.ObjectStateManager.ChangeObjectState(entity, EntityState.Modified);
                db.Set<MIS_Article_Category>().Attach(entity);
                db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                return db.SaveChanges();
            }
        }

        public MIS_Article_Category GetById(string id)
        {
            using (DBContainer db = new DBContainer())
            {
                return db.MIS_Article_Category.SingleOrDefault(a => a.Id == id);
            }
        }

        public bool IsExist(string id)
        {
            using (DBContainer db = new DBContainer())
            {
                MIS_Article_Category entity = GetById(id);
                if (entity != null)
                    return true;
                return false;
            }
        }
        public void Dispose()
        {

        }
    }
}
