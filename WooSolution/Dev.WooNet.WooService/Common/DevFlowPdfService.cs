using Dev.WooNet.Common.Utility;
using Dev.WooNet.IWooService;
using Dev.WooNet.Model.DevDTO.DevFlow;
using Dev.WooNet.Model.DevDTO.DevFlow.FlowPdfModel;
using Dev.WooNet.Model.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dev.WooNet.WooService
{

    /// <summary>
    /// 生成pdf操作类
    /// </summary>
    public partial  class DevFlowPdfService : BaseService<DevAppInst>, IDevFlowPdfService
    {
        #region 获取基础数据库连接

        private DbSet<DevAppInst> _AppInstSet = null;
        public DevFlowPdfService(DevDbContext dbContext)
           : base(dbContext)
        {
            _AppInstSet = base.DevDb.Set<DevAppInst>();
        }
        public DevFlowPdfService() { }

        #endregion


        #region 审批意见
        /// <summary>
        /// 审批意见
        /// </summary>
        /// <param name="appInst">审批实例</param>
        /// <returns>意见信息字典</returns>
        public Dictionary<string, List<WfOption>> GetWfOptions(DevAppInst appInst)
        {
            var query = from a in DevDb.Set<DevAppInstOpin>().AsNoTracking().Where(a => a.InstId == appInst.Id)
                        select new
                        {
                            Id = a.Id,
                            NodeId = a.NodeId,
                            NodeStrId = a.NodeStrId,
                            NodeName = a.Node.Name,
                            AddDateTime = a.AddDateTime,
                            AddUserId = a.AddUserId,
                            Opinion = a.Opinion

                        };
            var local = from a in query.AsEnumerable()
                        select new WfOption
                        {
                            NodeStrId = a.NodeStrId,
                            NodeId = a.NodeId,
                            NodeName = a.NodeName,
                            AppUserName = RedisDevCommUtility.GetUserName(a.AddUserId),
                            AddUserId = a.AddUserId,
                            Option = a.Opinion,
                            AppDate = a.AddDateTime,
                            //RedisHelper.HashGet($"{StaticData.RedisUserKey}:{a.CreateUserId}", "DisplyName"),
                            // UserEs = RedisHelper.HashGet($"{StaticData.RedisUserKey}:{a.CreateUserId}", "UserEs"),
                            // UserEsTy = RedisHelper.HashGet($"{StaticData.RedisUserKey}:{a.CreateUserId}", "UserEsTy"),

                            ImgSrc ="", //Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads", "UserEs", a.CreateUserId + ".PNG")
                        };

            return local.ToList().GroupBy(a => a.NodeName).ToDictionary(g => g.Key, g => g.ToList());

        }
        #endregion

        #region 合同对方
        /// <summary>
        /// 合同对方
        /// </summary>
        /// <param name="appInst">审批实例对象</param>
        /// <returns>合同对方审批单对象</returns>
        public CompanyInfo GetCompanyFlowPdfInfo(DevAppInst appInst)
        {
            CompanyInfo companyInfo = GetCompanyInfo(appInst.AppObjId);
            companyInfo.DicWfData = GetWfOptions(appInst);
            return companyInfo;

        }
        /// <summary>
        /// 获取合同对方信息
        /// </summary>
        /// <param name="Id">当前ID</param>
        /// <returns>合同对方相关信息</returns>
        private CompanyInfo GetCompanyInfo(int compId)
        {
            var query = from a in DevDb.Set<DevCompany>().AsNoTracking()
                        where a.Id == compId
                        select new
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Code = a.Code,
                            FaceUserId = a.FaceUserId,
                            AddDateTime = a.AddDateTime,
                            AddUserId = a.AddUserId,
                            Trade = a.Trade,//行业
                            CompClassId = a.CompClassId,//类别
                            Ctype = a.CompType,

                        };
            var local = from a in query.AsEnumerable()
                        select new CompanyInfo
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Code = a.Code,
                            CateName = RedisDevCommUtility.GetHashDataDic(a.CompClassId ?? 0),
                            FzrlUser = RedisDevCommUtility.GetUserName(a.FaceUserId ?? 0),
                            Trade = a.Trade,
                            AddDate = a.AddDateTime.Value.ToString("yyyy-MM-dd"),
                            AddUserName = RedisDevCommUtility.GetUserName(a.AddUserId ?? 0),
                           

                        };
            return local.FirstOrDefault();

        }





        #endregion
    }
}
