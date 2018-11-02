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


/*




*/
namespace Apps.BLL
{
    public class HomeBll : IHomeBll
    {
        [Dependency]
        public IHomeRepository HomeRepository { get; set; }
        public List<SysModuleModel> GetMenuByPersonId(string personId, string moduleId)
        {

            List<SysModule> queryData = HomeRepository.GetMenuByPersonId(personId, moduleId);
            List<SysModuleModel> modelList = null;
            try
            {


                modelList = (from r in queryData
                             select new SysModuleModel
                             {
                                 Id = r.Id,
                                 Name = r.Name,
                                 EnglishName = r.EnglishName,
                                 ParentId = r.ParentId,
                                 Url = r.Url,
                                 Iconic = r.Iconic,
                                 Sort = r.Sort,
                                 Remark = r.Remark,
                                 Enable = r.Enable,
                                 CreatePerson = r.CreatePerson,
                                 CreateTime = r.CreateTime,
                                 IsLast = r.IsLast,
                                 Version = r.Version,
                             }).ToList();
            }
            catch (Exception ex)
            {

                throw;
            }
            return modelList;
        }

        public SysModuleModelTree GetMenu(string personId, string moduleId)
        {

            List<SysModule> queryData = HomeRepository.GetMenuTree(personId, moduleId);
            SysModuleModelTree mainModel = new SysModuleModelTree()
            {
                Id = "0",
                Name = "顶级菜单",
                Child = new List<SysModuleModelTree>()
                
            };
            try
            {
                GetMenu(mainModel, mainModel, queryData);
            }
            catch (Exception ex)
            {
                throw;
            }
            return mainModel;
        }
        /// <summary>
        /// 基类窗体递归加载树形右键菜单
        /// </summary>
        /// <param name="aToolStripMenuItem">菜单项</param>
        /// <param name="aMenuTable">菜单表</param>
        /// <returns>属性菜单项</returns>
        private void GetMenu(SysModuleModelTree ParentModel, SysModuleModelTree mainModelTree, List<SysModule> queryData)
        {
            if (queryData.Count > 0)
            {
                List<SysModule> currList = queryData.Where(s => s.ParentId == ParentModel.Id).ToList();
                if (currList == null || currList.Count == 0) return;
                foreach (var dr1 in currList)
                {
                    SysModuleModelTree model = new SysModuleModelTree()
                    {
                        Enable = dr1.Enable,
                        EnglishName = dr1.EnglishName,
                        Iconic = dr1.Iconic,
                        Id = dr1.Id,
                        IsLast = dr1.IsLast,
                        Name = dr1.Name,
                        ParentId = dr1.ParentId,
                        Sort = dr1.Sort,
                        Url = dr1.Url,
                        Version = dr1.Version
                    };
                    if (model.ParentId == "0")
                    {
                        mainModelTree.Child.Add(model);
                    }
                    else
                    {
                        if (ParentModel.Child == null)
                        {
                            ParentModel.Child = new List<SysModuleModelTree>();
                        }
                        ParentModel.Child.Add(model);
                    }
                    GetMenu(model, mainModelTree, queryData);
                }
            }
        }
    }
}
