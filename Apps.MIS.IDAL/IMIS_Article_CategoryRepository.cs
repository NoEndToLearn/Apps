using Apps.Models;
using System.Linq;
namespace Apps.MIS.IDAL
{
    public interface IMIS_Article_CategoryRepository
    {
        IQueryable<MIS_Article_Category> GetList(DBContainer db);
        int Create(MIS_Article_Category entity);
        int Delete(string id);
        void Delete(DBContainer db, string[] deleteCollection);
        int Edit(MIS_Article_Category entity);
        MIS_Article_Category GetById(string id);
        bool IsExist(string id);
    }
}