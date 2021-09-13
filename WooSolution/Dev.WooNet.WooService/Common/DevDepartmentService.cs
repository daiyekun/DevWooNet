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
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Dev.WooNet.WooService
{

    /// <summary>
    /// 部门
    /// </summary>
   public partial class DevDepartmentService
    {
       
        /// <summary>
        /// 部门列表
        /// </summary>
        /// <typeparam name="s"></typeparam>
        /// <param name="pageInfo"></param>
        /// <param name="whereLambda"></param>
        /// <param name="orderbyLambda"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        public AjaxListResult<DevDepartmentDTO> GetList<s>(PageInfo<DevDepartment> pageInfo, Expression<Func<DevDepartment, bool>> whereLambda,
             Expression<Func<DevDepartment, s>> orderbyLambda, bool isAsc)
        {
            var tempquery = this.DevDb.Set<DevDepartment>().AsTracking().Where<DevDepartment>(whereLambda.Compile()).AsQueryable();
            pageInfo.TotalCount = tempquery.Count();
            if (isAsc)
            {
                tempquery = tempquery.OrderBy(orderbyLambda);
            }
            else
            {
                tempquery = tempquery.OrderByDescending(orderbyLambda);
            }
            if (!(pageInfo is NoPageInfo<DevDepartment>))
            { //分页
                tempquery = tempquery.Skip<DevDepartment>((pageInfo.PageIndex - 1) * pageInfo.PageSize).Take<DevDepartment>(pageInfo.PageSize);
            }


            var query = from a in tempquery
                        select new
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Sname = a.Sname,
                            Code = a.Code,
                            CateId = a.CateId,
                            Dsort = a.Dsort,
                            Remark = a.Remark,
                            IsMain = a.IsMain,
                            IsCompany = a.IsCompany,
                            Dstatus = a.Dstatus,
                            Dpath = a.Dpath,
                            Leaf = a.Leaf,
                            CateName = "",
                           
                            PName = DevDb.Set<DevDepartment>().AsNoTracking().Where(d => a.Pid == d.Id).Any() ? DevDb.Set<DevDepartment>().AsNoTracking().Where(d => a.Pid == d.Id).FirstOrDefault().Name : "",
                           


                        };
            var local = from a in query.AsEnumerable()
                        select new DevDepartmentDTO
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Sname = a.Sname,
                            Code = a.Code,
                            CateId = a.CateId,
                            Dsort = a.Dsort,
                            Remark = a.Remark,
                            IsMain = a.IsMain,
                            IsCompany = a.IsCompany,
                            Dstatus = a.Dstatus,
                            Dpath = a.Dpath,
                            Leaf = a.Leaf,
                            CateName = "",
                            PName = a.PName,
                            IsMainDic = EmunUtility.GetDesc(typeof(IsYesNOEnum), a.IsMain ?? 0),

                        };
            return new AjaxListResult<DevDepartmentDTO>()
            {
                data = local.ToList(),
                count = pageInfo.TotalCount,
                code = 0


            };
        }
        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        public IList<DevDepartmentDTO> GetAll()
        {
            IList<DevDepartmentDTO> list = RedisUtility.StringGetToList<DevDepartmentDTO>(RedisKeys.Reisdeptredilist);
            if (list==null) { 
            var query = from a in this.DevDb.Set<DevDepartment>().AsTracking()
                        select new
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Sname = a.Sname,
                            Code = a.Code,
                            CateId = a.CateId,
                            Dsort = a.Dsort,
                            Remark = a.Remark,
                            IsMain = a.IsMain,
                            IsCompany = a.IsCompany,
                            Dstatus = a.Dstatus,
                            Dpath = a.Dpath,
                            Leaf = a.Leaf,
                            CateName = "",
                            Pid=a.Pid,

                            PName = DevDb.Set<DevDepartment>().AsNoTracking().Where(d => a.Pid == d.Id).Any() ? DevDb.Set<DevDepartment>().AsNoTracking().Where(d => a.Pid == d.Id).FirstOrDefault().Name : "",



                        };
            var local = from a in query.AsEnumerable()
                        select new DevDepartmentDTO
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Sname = a.Sname,
                            Code = a.Code,
                            CateId = a.CateId,
                            Dsort = a.Dsort,
                            Remark = a.Remark,
                            IsMain = a.IsMain,
                            IsCompany = a.IsCompany,
                            Dstatus = a.Dstatus,
                            Dpath = a.Dpath,
                            Leaf = a.Leaf,
                            CateName = "",
                            PName = a.PName,
                            Pid = a.Pid,
                            IsMainDic = EmunUtility.GetDesc(typeof(IsYesNOEnum), a.IsMain ?? 0),

                        };
                list = local.ToList();
                RedisUtility.ListObjToJsonStringSetAsync(RedisKeys.Reisdeptredilist, list);
               
                
            }
            return list;


        }
        /// <summary>
        /// 设置Redis
        /// </summary>
        /// <param name="datadic">字典枚举</param>
        /// <returns></returns>
        public void SetRedisHash()
        {
            try
            {
                var curdickey = $"{RedisKeys.RedisdeptKey}";
                RedisUtility.KeyDeleteAsync(RedisKeys.Reisdeptredilist);
                var list = GetAll();
                foreach (var item in list)
                {
                    item.SetRedisHash<DevDepartmentDTO>($"{curdickey}", (a, c) =>
                    {
                        return $"{a}:{c}";
                    });
                }
            }
            catch (Exception ex)
            {

                Log4netHelper.Error(ex.Message);
            }


        }
        /// <summary>
        /// 修改部门
        /// </summary>
        /// <param name="deptInfo"></param>
        /// <param name="deptMain"></param>
        /// <returns></returns>
        private DevDepartment UpdateSave(DevDepartment deptInfo, DevDeptmain deptMain)
        {
            DevDepartment resul;
            var tempinfo = DevDb.Set<DevDepartment>().FirstOrDefault(a => a.Id == deptInfo.Id);
            tempinfo.Name = deptInfo.Name;
            tempinfo.Pid = deptInfo.Pid;
            tempinfo.Code = deptInfo.Code;
            tempinfo.CateId = deptInfo.CateId;
            tempinfo.Dsort = deptInfo.Dsort;
            tempinfo.Remark = deptInfo.Remark;
            tempinfo.IsMain = deptInfo.IsMain;
            tempinfo.Sname = deptInfo.Sname;
            tempinfo.IsCompany = deptInfo.IsCompany;
            tempinfo.Dpath = deptInfo.Dpath;
            tempinfo.Leaf = deptInfo.Leaf;
            
            resul = tempinfo;
            var tdeptMain = DevDb.Set<DevDeptmain>().FirstOrDefault(a => a.DeptId == deptInfo.Id);
            if (tdeptMain != null)
            {
                tdeptMain.LawPerson = deptMain.LawPerson;
                tdeptMain.TaxId = deptMain.TaxId;
                tdeptMain.BankName = deptMain.BankName;
                tdeptMain.Account = deptMain.Account;
                tdeptMain.Address = deptMain.Address;
                tdeptMain.ZipCode = deptMain.ZipCode;
                tdeptMain.Fax = deptMain.Fax;
                tdeptMain.TelePhone = deptMain.TelePhone;
                tdeptMain.InvoiceName = deptMain.InvoiceName;

            }
            else
            {
                deptMain.Id = 0;
                deptMain.IsDelete = 0;
                deptMain.DeptId = tempinfo.Id;
                DevDb.Set<DevDeptmain>().Add(deptMain);
            }

            DevDb.SaveChanges();
            return resul;
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="deptInfo">部门</param>
        /// <param name="deptMain">签约主体部分信息</param>
        /// <returns></returns>
        private DevDepartment AddSave(DevDepartment deptInfo, DevDeptmain deptMain)
        {
           
            deptInfo.IsDelete = 0;
            deptInfo.Dstatus = 1;
            var tmpInfo = Add(deptInfo);
            if (deptMain != null && !string.IsNullOrEmpty(deptMain.LawPerson))
            {
                deptMain.IsDelete = 0;
                deptMain.DeptId = tmpInfo.Id;
                DevDb.Set<DevDeptmain>().Add(deptMain);
                DevDb.SaveChanges();

            }

            return deptInfo;
        }
        /// <summary>
        /// 保存部门信息
        /// </summary>
        /// <returns></returns>
        public DevDepartment SaveDeptInfo(DevDepartment deptInfo, DevDeptmain deptMain)
        {
            DevDepartment resul = null;
            if (deptInfo.Id > 0)
            {//修改
                resul = UpdateSave(deptInfo, deptMain);

            }
            else
            {
                resul = AddSave(deptInfo, deptMain);
            }
            RedisUtility.KeyDeleteAsync(RedisKeys.Reisdeptredilist);
            return resul;



        }
        /// <summary>
        /// 显示查看基本信息
        /// </summary>
        /// <param name="Id">当前ID</param>
        /// <returns></returns>
        public DevDepartmentDTO ShowValues(int Id)
        {
            var listAll = GetAll();
            var firstdept = listAll.FirstOrDefault(a => a.Id == Id);
            if (firstdept==null)
            {
                if (DevDb.Set<DevDepartment>().Any(a => a.Id == Id)) {
                    RedisUtility.KeyDeleteAsync(RedisKeys.Reisdeptredilist);
                     listAll = GetAll();
                    firstdept = listAll.FirstOrDefault(a => a.Id == Id);
                }

            }
            var deptMain = DevDb.Set<DevDeptmain>().AsNoTracking().Where(a => firstdept != null && a.DeptId == firstdept.Id).FirstOrDefault();
            var info = new DevDepartmentDTO
            {
                Id = firstdept.Id,
                Pid = firstdept.Pid,
                Name = firstdept.Name,
                Sname = firstdept.Sname,
                Code = firstdept.Code,
                CateId = firstdept.CateId,
                Dsort = firstdept.Dsort,
                Remark = firstdept.Remark,
                IsMain = firstdept.IsMain,
                IsCompany = firstdept.IsCompany,
                Dstatus = firstdept.Dstatus,
                Dpath = firstdept.Dpath,
                Leaf = firstdept.Leaf,
                CateName = firstdept.CateName,
                PName = firstdept.PName,
                IsDelete = firstdept.IsDelete,
                IsMainDic = firstdept.IsMainDic,
                //IsSubCompanyDic = firstdept.is,
              


            };
            if (deptMain != null)
            {
                info.LawPerson = deptMain.LawPerson;
                info.TaxId = deptMain.TaxId;
                info.BankName = deptMain.BankName;
                info.Account = deptMain.Account;
                info.Address = deptMain.Address;
                info.ZipCode = deptMain.ZipCode;
                info.Fax = deptMain.Fax;
                info.TelePhone = deptMain.TelePhone;
                info.InvoiceName = deptMain.InvoiceName;
            }

            return info;


        }
        #region treeselect需要数据
        /// <summary>
        /// 返回LayUI Tree需要数据格式
        /// </summary>
        /// <returns></returns>
        public IList<TreeSelectInfo> GetTreeSelect()
        {
            IList<TreeSelectInfo> listTree = new List<TreeSelectInfo>();
            var listAll = GetAll();
            var list = listAll.Where(a => a.IsDelete == 0 && a.Dstatus == 1).ToList();
            foreach (var item in list.Where(a => a.Pid == 0))
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
        public void RecursionChrenNode(IList<DevDepartmentDTO> listDepts, TreeSelectInfo treeInfo, DevDepartmentDTO item)
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
        /// 软删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteDept(string ids)
        {
            RedisUtility.KeyDeleteAsync(RedisKeys.Reisdeptredilist);
            StringBuilder builder = new StringBuilder();
            builder.Append($"update dev_deptmain set IsDelete=1 where DeptId in({ids});");
            builder.Append($"update dev_department set IsDelete=1 where Id in({ids});");
            var strsql = builder.ToString();
            if (!string.IsNullOrWhiteSpace(strsql) )
            return ExecuteSqlCommand(strsql);
            else
            return -1;

        }

        #region 部门Layui递归树

        /// <summary>
        /// 返回LayUI Tree需要数据格式
        /// </summary>
        /// <returns></returns>
        public IList<LayTree> GetLayUITree()
        {
            IList<LayTree> listTree = new List<LayTree>();
            var listAll = GetAll();
            var list = listAll.Where(a => a.IsDelete == 0 && a.Dstatus == 1).ToList();
            foreach (var item in list.Where(a => a.Pid == 0))
            {
                LayTree treeInfo = new LayTree();
                treeInfo.id = item.Id;
                treeInfo.title = item.Name;
               
                GetChrenNode(list, treeInfo, item);
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
        public void GetChrenNode(IList<DevDepartmentDTO> listDepts, LayTree treeInfo, DevDepartmentDTO item)
        {
            var listchren = listDepts.Where(a => a.Pid == item.Id);
            var listchrennode = new List<TreeInfo>();
            if (listchren.Any())
            {
                foreach (var chrenItem in listchren.ToList())
                {
                    LayTree treeInfotmp = new LayTree();
                    treeInfotmp.id = chrenItem.Id;
                    treeInfotmp.title = chrenItem.Name;


                    GetChrenNode(listDepts, treeInfotmp, chrenItem);
                    listchrennode.Add(treeInfotmp);
                }

                treeInfo.children = listchrennode;

            }


        }

        #endregion

        #region 递归XTree-主要用于功能权限分配

        /// <summary>
        /// 返回LayUI Tree需要数据格式
        /// </summary>
        /// <returns></returns>
        public IList<XTree> GetXtTree(IList<int> Ids)
        {

            IList<XTree> listTree = new List<XTree>();
            var listAll = GetAll();
            var list = listAll.Where(a => a.IsDelete == 0 && a.Dstatus == 1).ToList();
            foreach (var item in list.Where(a => a.Pid == 0))
            {
                XTree treeInfo = new XTree();
                treeInfo.value = item.Id.ToString();
                treeInfo.title = item.Name;
                treeInfo.Checked = (Ids != null && Ids.Contains(item.Id)) ? true : false;

                RecursionChrenNodeXt(list, treeInfo, item, Ids);
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
        public void RecursionChrenNodeXt(IList<DevDepartmentDTO> listDepts, XTree treeInfo, DevDepartmentDTO item, IList<int> Ids)
        {
            var listchren = listDepts.Where(a => a.Pid == item.Id);
            var listchrennode = new List<XTree>();
            if (listchren.Any())
            {
                foreach (var chrenItem in listchren.ToList())
                {
                    XTree treeInfotmp = new XTree();
                    treeInfotmp.value = chrenItem.Id.ToString();
                    treeInfotmp.title = chrenItem.Name;
                    treeInfotmp.Checked = (Ids != null && Ids.Contains(chrenItem.Id)) ? true : false;

                    RecursionChrenNodeXt(listDepts, treeInfotmp, chrenItem, Ids);
                    listchrennode.Add(treeInfotmp);
                }

                treeInfo.data = listchrennode;

            }


        }

       

        #endregion

    }
}
