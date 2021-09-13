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
            var list = listAll.Where(a => a.IsDelete == 0 ).ToList();
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

        #region 根据角色获取菜单数据
        /// <summary>
        /// 角色菜单集合
        /// </summary>
        /// <returns></returns>
        public IList<DevModelCheck> GetModelChecks(int roleId)
        {
            IList<DevModelCheck> listChecks = new List<DevModelCheck>();
            var listAll = GetListAll();
            var listrolemodel = GetRoleModel(roleId);
            var list = listAll.Where(a => a.IsDelete == 0).ToList();
            foreach (var item in list.Where(a => a.Pid == 1))//排除pid=0的菜单。从实际一级菜单开始
            {
                var chkmodel = new DevModelCheck();
                chkmodel.Id = item.Id;
                chkmodel.Name = item.Name;
                chkmodel.Chk = listrolemodel.Any(a => a.Mid == item.Id);//是否存在
                ChrenModelNode(list, chkmodel, item, listrolemodel);
                listChecks.Add(chkmodel);

            }
            return listChecks;
        }

        /// <summary>
        /// 根据校色获取菜单
        /// </summary>
        /// <returns></returns>
        public IList<DevRoleModule> GetRoleModel(int roleId)
        {
            var list = DevDb.Set<DevRoleModule>().Where(a => a.Rid == roleId).ToList();
            return list;

        }

        /// <summary>
        /// 递归
        /// </summary>
        /// <param name="listmodels">菜单集合</param>
        /// <param name="Info">循环对象</param>
        /// <param name="item">父类对象</param>
        public void ChrenModelNode(IList<DevSysmodelDTO> listmodels,
            DevModelCheck Info, DevSysmodelDTO item, IList<DevRoleModule> roleModules)
        {
            var listchren = listmodels.Where(a => a.Pid == item.Id);
            var listchrennode = new List<DevModelCheck>();
            if (listchren.Any())
            {
                foreach (var chrenItem in listchren.ToList())
                {
                    DevModelCheck chckmodel = new DevModelCheck();
                    chckmodel.Id = chrenItem.Id;
                    chckmodel.Name = chrenItem.Name;
                    chckmodel.Chk = roleModules.Any(a=>a.Mid== chrenItem.Id);
                   
                    ChrenModelNode(listmodels, chckmodel, chrenItem, roleModules);
                    listchrennode.Add(chckmodel);
                }
                Info.ChildrenItem = listchrennode;

            }



        }
        /// <summary>
        /// 保存角色模块
        /// </summary>
        /// <param name="rolemodel">角色模块对象</param>

        public IList<DevRoleModule> SaveRolemodel(RoleModel rolemodel)
        {
            string strsql = $"delete from dev_role_module where Rid={rolemodel.RoleId} and Mid in({rolemodel.ModelIds})";
            ExecuteSqlCommand(strsql);
            var mIds = StringHelper.String2ArrayInt(rolemodel.ModelIds);
            IList<DevRoleModule> roleModules = new List<DevRoleModule>();
            var listAll = GetListAll();
            foreach (var mid in mIds)
            {
                var info = new DevRoleModule();
                info.Rid = rolemodel.RoleId;
                info.Mid = mid;
                roleModules.Add(info);

                var minfo = listAll.FirstOrDefault(a => a.Id == mid);
                if (minfo != null)
                {   //防止遗漏父类-->pid=0的父类不要
                    var pinfo = listAll.FirstOrDefault(a => a.Id == minfo.Pid && a.Pid != 0);
                    if (pinfo !=null&& !roleModules.Any(a=>a.Mid== pinfo.id))
                    {
                       var  tpinfo = new DevRoleModule();
                        tpinfo.Rid= rolemodel.RoleId;
                        tpinfo.Mid = pinfo.Id;
                        roleModules.Add(tpinfo);
                    }
                }
                

            }


            this.DevDb.Set<DevRoleModule>().AddRange(roleModules);
            this.SaveChanges();//一个链接  多个sql
            return roleModules;

        }


        #endregion


        #region 桌面菜单
        /// <summary>
        /// 根据用户获取菜单权限
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>

        public IList<WinuiMenu> GetWinDeskMenus(int userId)
        {
            var listmenus = new List<WinuiMenu>();
            var roleIds = DevDb.Set<DevUserRole>().Where(a => a.Uid == userId).Select(a => a.Rid).ToList();
            var listall = GetListAll();
            //查询角色菜单
            var menuIds = DevDb.Set<DevRoleModule>().Where(a => roleIds.Contains(a.Rid)).Select(a => a.Mid).ToList();
            var deskmenus = listall.Where(a => a.IsDelete != 1 && a.IsShow == 1 && menuIds.Contains(a.Id)).ToList();
            foreach (var item in deskmenus)
            {
                var menu = new WinuiMenu();
                menu.id = item.Id;
                menu.icon = item.Ico;
                menu.title = item.Title;
                menu.name = item.Name;
                menu.openType = item.PageType;
                menu.pageURL = item.RequestUrl;
                listmenus.Add(menu);

            }
            return listmenus;



        }

       
        #endregion

        #region 开始菜单获取
        
        /// <summary>
        /// 开始菜单-查询系统菜单为是的
        /// </summary>
        /// <param name="userId">当前用户ID</param>
        /// <returns>系统菜单</returns>
        public IList<WinuiMenu> GetWinStartMenus(int userId)
        {
            var listmenus = new List<WinuiMenu>();
            var roleIds = DevDb.Set<DevUserRole>().Where(a => a.Uid == userId).Select(a => a.Rid).ToList();
            var listall = GetListAll().Where(a=>a.IsSystem==1&&a.IsDelete==0).ToList();
            //查询角色菜单
            var menuIds = DevDb.Set<DevRoleModule>().Where(a => roleIds.Contains(a.Rid)).Select(a => a.Mid).ToList();
            var deskmenus = listall.Where(a => a.IsDelete != 1  && menuIds.Contains(a.Id)).ToList();
            foreach (var item in deskmenus.Where(a => a.Pid==1))//排除pid=0的菜单。从实际一级菜单开始
            {
                var menu = new WinuiMenu();
                menu.id = item.Id;
                menu.icon = item.Ico;
                menu.title = item.Title;
                menu.name = item.Name;
                menu.openType = item.PageType;
                menu.pageURL = item.RequestUrl;
                Chrenstratmenus(deskmenus, menu, item);
                listmenus.Add(menu);

            }
            return listmenus;
        }

        /// <summary>
        /// 递归
        /// </summary>
        /// <param name="listmodels">菜单集合</param>
        /// <param name="Info">循环对象</param>
        /// <param name="item">父类对象</param>
        public void Chrenstratmenus(IList<DevSysmodelDTO> deskmenus,
            WinuiMenu Info, DevSysmodelDTO item)
        {
            var listchren = deskmenus.Where(a => a.Pid == item.Id);
            var listchrennode = new List<WinuiMenu>();
            if (listchren.Any())
            {
                foreach (var chrenItem in listchren.ToList())
                {
                    WinuiMenu menu = new WinuiMenu();
                    menu.id = chrenItem.Id;
                    menu.icon = chrenItem.Ico;
                    menu.title = chrenItem.Title;
                    menu.name = chrenItem.Name;
                    menu.openType = chrenItem.PageType;
                    menu.pageURL = chrenItem.RequestUrl;
                    Chrenstratmenus(deskmenus, menu, chrenItem);
                  
                    listchrennode.Add(menu);
                }
                Info.childs = listchrennode;

            }



        }


        #endregion

        #region layui Tree 递归
        /// <summary>
        /// 系统菜单  layuiTree
        /// </summary>
        /// <returns>返回系统菜单 Layui Tree</returns>
        public IList<LayuiTree> GetLayuiTreeData()
        {

            IList<LayuiTree> layuitrees = new List<LayuiTree>();
            var listAll = GetListAll();
           
            var list = listAll.Where(a => a.IsDelete == 0).ToList();
            foreach (var item in list.Where(a => a.Pid == 1))//排除pid=0的菜单。从实际一级菜单开始
            {
                var laytree = new LayuiTree();
                laytree.id = item.Id;
                laytree.title = item.Name;

                LayTreeDiGui(list, laytree, item);
                layuitrees.Add(laytree);

            }
            return layuitrees;

        }

        /// <summary>
        /// 递归
        /// </summary>
        /// <param name="listmodels">菜单集合</param>
        /// <param name="Info">循环对象</param>
        /// <param name="item">父类对象</param>
        public void LayTreeDiGui(IList<DevSysmodelDTO> listmodels,
            LayuiTree Info, DevSysmodelDTO item)
        {
            var listchren = listmodels.Where(a => a.Pid == item.Id);
            var listchrennode = new List<LayuiTree>();
            if (listchren.Any())
            {
                foreach (var chrenItem in listchren.ToList())
                {
                    LayuiTree chckmodel = new LayuiTree();
                    chckmodel.id = chrenItem.Id;
                    chckmodel.title = chrenItem.Name;
                  
                    LayTreeDiGui(listmodels, chckmodel, chrenItem);
                    listchrennode.Add(chckmodel);
                }
                Info.children = listchrennode;

            }



        }
        #endregion



    }
}
