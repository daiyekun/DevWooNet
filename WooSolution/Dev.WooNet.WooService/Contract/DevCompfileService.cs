using Dev.WooNet.Common.Models;
using Dev.WooNet.Common.Utility;
using Dev.WooNet.Model.DevDTO;
using Dev.WooNet.Model.ExtendModel;
using Dev.WooNet.Model.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Dev.WooNet.WooService
{

    /// <summary>
    /// 附件
    /// </summary>
    public partial class DevCompfileService
    {
        /// <summary>
        /// 列表
        /// </summary>
        /// <typeparam name="s"></typeparam>
        /// <param name="pageInfo"></param>
        /// <param name="whereLambda"></param>
        /// <param name="orderbyLambda"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        public AjaxListResult<DevCompfileDTO> GetList<s>(PageInfo<DevCompfile> pageInfo, Expression<Func<DevCompfile, bool>> whereLambda,
             Expression<Func<DevCompfile, s>> orderbyLambda, bool isAsc)
        {
            var tempquery = this.DevDb.Set<DevCompfile>().AsTracking().Where<DevCompfile>(whereLambda);
            pageInfo.TotalCount = tempquery.Count();
            if (isAsc)
            {
                tempquery = tempquery.OrderBy(orderbyLambda);
            }
            else
            {
                tempquery = tempquery.OrderByDescending(orderbyLambda);
            }
            if (!(pageInfo is NoPageInfo<DevCompfile>))
            { //分页
                tempquery = tempquery.Skip<DevCompfile>((pageInfo.PageIndex - 1) * pageInfo.PageSize).Take<DevCompfile>(pageInfo.PageSize);
            }


            var query = from a in tempquery
                        select new
                        {
                            Id = a.Id,
                            FilePath = a.FilePath,
                            FileName = a.FileName,
                            FileClassId = a.FileClassId,
                            Name = a.Name,
                            CompId = a.CompId,
                            Remark = a.Remark,
                            DowNumber = a.DowNumber,
                            FolderName = a.FolderName,
                            GuidFileName = a.GuidFileName,
                            Extension = a.Extension,
                            AddUserId = a.AddUserId,
                            AddDateTime = a.AddDateTime,

                        };
            var local = from a in query.AsEnumerable()
                        select new DevCompfileDTO
                        {
                            Id = a.Id,
                            FilePath = a.FilePath,
                            FileName = a.FileName,
                            FileClassId = a.FileClassId,
                            Name = a.Name,
                            CompId = a.CompId,
                            Remark = a.Remark,
                            DowNumber = a.DowNumber,
                            FolderName = a.FolderName,
                            GuidFileName = a.GuidFileName,
                            Extension = a.Extension,
                            AddUserId = a.AddUserId,
                            AddDateTime = a.AddDateTime,
                            AddUserName = RedisDevCommUtility.GetUserName(a.AddUserId ?? 0),
                            FileClassName = RedisDevCommUtility.GetHashDataDic(a.FileClassId??0)

                        };
            return new AjaxListResult<DevCompfileDTO>()
            {
                data = local.ToList(),
                count = pageInfo.TotalCount,
                code = 0


            };
        }

        /// <summary>
        /// 根据ID获取信息
        /// </summary>
        /// <returns>返回基本信息</returns>
        public DevCompfileDTO GetInfoById(int Id)
        {
            var query = from a in this.DevDb.Set<DevCompfile>().AsTracking()
                        where a.Id == Id
                        select new
                        {
                            Id = a.Id,
                            FilePath = a.FilePath,
                            FileName = a.FileName,
                            FileClassId = a.FileClassId,
                            Name = a.Name,
                            CompId = a.CompId,
                            Remark = a.Remark,
                            DowNumber = a.DowNumber,
                            FolderName = a.FolderName,
                            GuidFileName = a.GuidFileName,
                            Extension = a.Extension,
                            AddUserId = a.AddUserId,
                            AddDateTime = a.AddDateTime,



                        };
            var local = from a in query.AsEnumerable()
                        select new DevCompfileDTO
                        {
                            Id = a.Id,
                            FilePath = a.FilePath,
                            FileName = a.FileName,
                            FileClassId = a.FileClassId,
                            Name = a.Name,
                            CompId = a.CompId,
                            Remark = a.Remark,
                            DowNumber = a.DowNumber,
                            FolderName = a.FolderName,
                            GuidFileName = a.GuidFileName,
                            Extension = a.Extension,
                            AddUserId = a.AddUserId,
                            AddDateTime = a.AddDateTime,
                            AddUserName = RedisDevCommUtility.GetUserName(a.AddUserId ?? 0),
                            FileClassName= RedisDevCommUtility.GetHashDataDic(a.FileClassId??0)

                        };
            return local.FirstOrDefault();
        }

        /// <summary>
        /// 获取图片集合
        /// </summary>
        /// <param name="contId">图片ID</param>
        /// <returns></returns>
        public IList<PicViewDTO> GetPicViews(int contId,string basurl)
        {
            var extens = new string[] {
                ".png",".jpg",".jpeg",".bmp",".svg",".gif",".tif",".psd",".pcx",".svg",".cdr",".raw",
                ".avif",".raw",".ai",".tga",".exif",".fpx",".eps",".webp"
            };
            var list = DevDb.Set<DevCompfile>().Where(a => a.CompId == contId && extens.Contains(a.Extension.ToLower()))
                .Select(a => new PicViewDTO
                {
                    Id = a.Id,
                    Name = a.Name,
                    Url = $"{basurl}/{a.FilePath}",
                }).ToList();

            return list;

        }
    }
}
