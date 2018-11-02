using Apps.Models;
using System.Linq;
namespace Apps.MIS.IDAL
{
    public interface IMIS_ArticleRepository
    {
        IQueryable<MIS_Article> GetList(DBContainer db);
        int Create(MIS_Article entity);
        int Delete(string id);
        void Delete(DBContainer db, string[] deleteCollection);
        int Edit(MIS_Article entity);
        MIS_Article GetById(string id);
        bool IsExist(string id);
    }
}