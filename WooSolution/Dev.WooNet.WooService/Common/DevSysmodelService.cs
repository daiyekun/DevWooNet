using Dev.WooNet.Common.Models;
using Dev.WooNet.Common.Utility;
using Dev.WooNet.Model.DevDTO;
using Dev.WooNet.Model.Enums;
using Dev.WooNet.Model.ExtendModel;
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
    /// 系统模块操作
    /// </summary>
    public partial class DevSysmodelService
    {
        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        public IList<DevSysmodelDTO> GetListAll()
        {
            IList<DevSysmodelDTO> list = RedisUtility.StringGetToList<DevSysmodelDTO>(RedisKeys.RedisSysModellist);
            
            if (list == null)
            {
                var tempquery = DevDb.Set<DevSysmodel>().AsNoTracking().OrderBy(a => a.Sort);
                var query = from a in tempquery
                            select new
                            {
                                Id = a.Id,
                                Pid = a.Pid,
                                Name = a.Name,
                                Title = a.Title,
                                RequestUrl = a.RequestUrl,
                                Remark = a.Remark,
                                IsShow = a.IsShow,
                                IsDelete = a.IsDelete,
                                PName = DevDb.Set<DevSysmodel>().AsNoTracking().Where(p => p.Id == a.Pid).Any() ? DevDb.Set<DevSysmodel>().AsNoTracking().Where(p => p.Id == a.Pid).FirstOrDefault().Name : "",
                                Ico = a.Ico,
                                Sort = a.Sort,
                                Mpath = a.Mpath,
                                Leaf = a.Leaf,
                                IsSystem = a.IsSystem,
                                PageType=a.PageType,



                            };
                var local = from a in query.AsEnumerable()
                            select new DevSysmodelDTO
                            {
                                Id = a.Id,
                                Pid = a.Pid,
                                Name = a.Name,
                                Title = a.Title,
                                RequestUrl = a.RequestUrl,
                                Remark = a.Remark,
                                IsShow = a.IsShow,
                                IsDelete = a.IsDelete,
                                PName= a.PName,
                                Ico = a.Ico,
                                Sort = a.Sort,
                                Mpath = a.Mpath,
                                Leaf = a.Leaf,
                                IsShowDic = EmunUtility.GetDesc(typeof(IsYesNOEnum),a.IsShow),
                                PageTypeDic= EmunUtility.GetDesc(typeof(PageTypeEnum), a.PageType),
                                PageType = a.PageType,
                                id = a.Id,
                                pid = a.Pid,
                                IsSystem = a.IsSystem,
                                IsSystemDic = EmunUtility.GetDesc(typeof(IsYesNOEnum), a.IsSystem),


                            };
                list = local.ToList();
                RedisUtility.ListObjToJsonStringSetAsync(RedisKeys.RedisSysModellist, list);
                
            }
            return list;

        }

        #region treeselect需要数据
        /// <summary>
        /// 返回LayUI Tree需要数据格式
        /// </summary>
        /// <returns></returns>
        public IList<TreeSelectInfo> GetModelTreeSelect()
        {
            IList<TreeSelectInfo> listTree = new List<TreeSelectInfo>();
            var listAll = GetListAll();
            var list = listAll.Where(a => a.IsDelete == 0 && a.IsShow == 1).ToList();
            foreach (var item in list.Where(a => a.Pid ==0))
            {
                TreeSelectInfo treeInfo = new TreeSelectInfo();
                treeInfo.id = item.Id;
                treeInfo.title = item.Name;
                treeInfo.name = item.Name;
                RecursionChrenNode(list, treeInfo, item);
                listTree.Add(treeInfo);

            }
            return listTree;
        }

        /// <summary>
        /// 递归
        /// </summary>
        /// <param name="listDepts">数据列表</param>
        /// <param name="treeInfo">Tree对象</param>
        /// <param name="item">父类对象</param>
        public void RecursionChrenNode(IList<DevSysmodelDTO> listDepts, TreeSelectInfo treeInfo, DevSysmodelDTO item)
        {
            var listchren = listDepts.Where(a => a.Pid == item.Id);
            var listchrennode = new List<TreeSelectInfo>();
            if (listchren.Any())
            {
                foreach (var chrenItem in listchren.ToList())
                {
                    TreeSelectInfo treeInfotmp = new TreeSelectInfo();
                    treeInfotmp.id = chrenItem.Id;
                    treeInfotmp.title = chrenItem.Name;
                    treeInfotmp.name = chrenItem.Name;

                    RecursionChrenNode(listDepts, treeInfotmp, chrenItem);
                    listchrennode.Add(treeInfotmp);
                }
                treeInfo.children = listchrennode;

            }



        }
        #endregion

        /// <summary>
        /// 保存信息
        /// </summary>
        /// <returns></returns>
        public DevSysmodel SaveData(DevSysmodel info)
        {
            DevSysmodel resul = null;
            RedisUtility.KeyDeleteAsync(RedisKeys.RedisSysModellist);


            if (info.Id > 0)
            {//修改
                Update(info);
                resul = info;

            }
            else
            {
                info.CreateDatetime = DateTime.Now;
                info.CreateUserId = 1;
                info.ModifyUserId = 1;

                resul = Add(info);
            }
            
            return resul;



        }
        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="Ids">需要删除的ID</param>
        /// <returns></returns>
        public int DelSysModel(string Ids)
        {
            string sqlstr = $"update dev_sysmodel set IsDelete=1 where Id in({Ids})";
            var resl = ExecuteSqlCommand(sqlstr);
            RedisUtility.KeyDeleteAsync(RedisKeys.RedisSysModellist);

            return resl;

        }

        /// <summary>
        /// 根据Id 查询信息
        /// </summary>
        /// <returns>返回基本信息</returns>
        public DevSysmodelDTO GetSysModelById(int Id)
        {
            var query = from a in this.DevDb.Set<DevSysmodel>().AsTracking()
                        where a.Id == Id
                        select new
                        {
                            Id = a.Id,
                            Pid = a.Pid,
                            Name = a.Name,
                            Title=a.Title,
                            RequestUrl = a.RequestUrl,
                            Remark = a.Remark,
                            IsShow = a.IsShow,
                            IsDelete = a.IsDelete,
                            PName = DevDb.Set<DevSysmodel>().AsNoTracking().Where(p => p.Id == a.Pid).Any() ? DevDb.Set<DevSysmodel>().AsNoTracking().Where(p => p.Id == a.Pid).FirstOrDefault().Name : "",
                            Ico = a.Ico,
                            Sort = a.Sort,
                            Mpath = a.Mpath,
                            Leaf = a.Leaf,
                            IsSystem=a.IsSystem,
                            PageType = a.PageType,
                           

                        };
            var local = from a in query.AsEnumerable()
                        select new DevSysmodelDTO
                        {
                            Id = a.Id,
                            Pid = a.Pid,
                            Name = a.Name,
                            Title = a.Title,
                            RequestUrl = a.RequestUrl,
                            Remark = a.Remark,
                            IsShow = a.IsShow,
                            IsDelete = a.IsDelete,
                            PName = a.PName,
                            Ico = a.Ico,
                            Sort = a.Sort,
                            Mpath = a.Mpath,
                            Leaf = a.Leaf,
                            IsShowDic = EmunUtility.GetDesc(typeof(IsYesNOEnum), a.IsShow),
                            PageTypeDic = EmunUtility.GetDesc(typeof(PageTypeEnum), a.PageType),
                            PageType = a.PageType,
                            id = a.Id,
                            pid = a.Pid,
                            IsSystem = a.IsSystem,
                            IsSystemDic = EmunUtility.GetDesc(typeof(IsYesNOEnum), a.IsSystem),
                            

                        };
            return local.FirstOrDefault();
        }

        /// <summary>
        /// 修改字段
        /// </summary>
        /// <param name="updateField">字段对象</param>
        /// <returns></returns>
        public int UpdateField(UpdateField updateField)
        {
            RedisUtility.KeyDeleteAsync(RedisKeys.RedisSysModellist);
            var result = 0;
            StringBuilder strsql = new StringBuilder();
            switch (updateField.FileName)
            {
                case "Sort"://修改字段
                    var sort = 0;
                    int.TryParse(updateField.UpdateVal,out  sort);
                    strsql.Append($"update dev_sysmodel set Sort={sort} where Id={updateField.Id}");
                    break;

            }
            if (!string.IsNullOrEmpty(strsql.ToString()))
            {
                ExecuteSqlCommand(strsql.ToString());
                result = 1;
            }

            return result;
        }
    }
}
