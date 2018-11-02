using System.Collections.Generic;
using Apps.Common;
using Apps.Models.MIS;
namespace Apps.MIS.IBLL
{
    public interface IMIS_ArticleBLL
    {
        List<MIS_ArticleModel> GetList(ref GridPager pager, string queryStr);

        List<MIS_ArticleModel> GeAuditedList(ref GridPager pager, bool isAudited, string queryStr);

        bool Audit(ref ValidationErrors errors, string id, string currUserId);

        bool Create(ref ValidationErrors errors, MIS_ArticleModel model);
        bool Delete(ref ValidationErrors errors, string id);
        bool Delete(ref ValidationErrors errors, string[] deleteCollection);
        bool Edit(ref ValidationErrors errors, MIS_ArticleModel model);
        MIS_ArticleModel GetById(string id);
        bool IsExist(string id);
    }
}
