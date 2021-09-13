using Dev.WooNet.Common.Models;
using Dev.WooNet.Common.Utility;
using Dev.WooNet.Model.DevDTO;
using Dev.WooNet.Model.Enums;
using Dev.WooNet.Model.ExtendModel;
using Dev.WooNet.Model.Models;
using Microsoft.EntityFrameworkCore;
using NF.Common.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Dev.WooNet.WooService
{

    /// <summary>
    /// 角色权限
    /// </summary>
    public partial class DevRolePessionService
    {

        /// <summary>
        /// 保存角色权限
        /// </summary>
        /// <param name="rolePermissions">角色权限集合</param>
        /// <returns></returns>
        public IEnumerable<DevRolePession> SavePermission(IEnumerable<DevRolePession> rolePermissions)
        {
            var firstinfo = rolePermissions.FirstOrDefault();

            string sqlstr = "delete from  dev_role_pession where MId=" + firstinfo.Mid + " and  RoleId=" + firstinfo.RoleId;
            ExecuteSqlCommand(sqlstr);
            return Add(rolePermissions);
        }

        #region 公共的
        /// <summary>
        /// 用户和功能标识获取对应权限列表
        /// </summary>
        /// <param name="funcCode">功能代码</param>
        /// <param name="userId">用户ID</param>
        /// <returns>功能权限列表</returns>
        protected List<DevRolePession> listRoleListPermsByFunCode(string funcCode, int userId)
        {
            var roleIds= DevDb.Set<DevUserRole>().Where(a => a.Uid == userId).Select(a => a.Rid).ToList();
            var rolePermission = DevDb.Set<DevRolePession>().Where(a => roleIds.Contains(a.RoleId)).ToList();  //GetRolePermissionByUserId(userId);
            if (rolePermission != null)
                return rolePermission.Where(a => a.FuncCode == funcCode).ToList();
            return null;


        }
        
        /// <summary>
        /// 根据用户和功能标识获取相关权限
        /// </summary>
        /// <param name="funcCode">功能标识</param>
        /// <param name="userId">用户ID</param>
        /// <returns>功能权限列表</returns>
        protected PermissionInfo GetPermission(string funcCode, int userId)
        {
            PermissionInfo permission = new PermissionInfo();
            var rolepssion = listRoleListPermsByFunCode(funcCode, userId);
            if (rolepssion != null)
            {
                permission.RolePermissions = rolepssion;
            }
            //var userpssion = listUserListPermsByFunCode(funcCode, userId);
            //if (userpssion != null)
            //{
            //    permission.UserPermissions = userpssion;
            //}
            permission.listFuntypes = rolepssion.Select(a => a.FuncType).ToList();
            return permission;
        }

        /// <summary>
        /// 根据组织机构ID获取它的所有下级组织机构ID
        /// </summary>
        /// <param name="deptId">当前组织机构ID</param>
        /// <returns></returns>
        private IList<int> GetDeptAndChirdDetpId(int deptId)
        {
           
            IList<DevDepartmentDTO> list = RedisUtility.StringGetToList<DevDepartmentDTO>(RedisKeys.Reisdeptredilist);

            if (list == null)
            {
                var tempquery = DevDb.Set<DevDepartment>().AsNoTracking();
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
                                Pid = a.Pid,

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
            var infopath = list.Where(a => a.Id == deptId).Any() ? list.Where(a => a.Id == deptId).FirstOrDefault().Dpath : "notdata";
            var listchds = list.Where(a => a.Dpath.StartsWith(infopath)).Select(a => a.Id).ToList();
            listchds.Add(deptId);
            return listchds;

        }

        /// <summary>
        /// 获取权限是机构的选择机构ID
        /// </summary>
        /// <returns></returns>
        private IList<int> GetDeptIds(IList<DevRolePession> rolePermissions)
        {
            List<int> listdepts = new List<int>();
            if (rolePermissions != null && rolePermissions.Count() > 0)
            {
                var list = rolePermissions.Where(a => a.FuncType == 2).Select(a => a.DeptIds).ToList();
                foreach (var item in list)
                {
                    listdepts.AddRange(StringHelper.String2ArrayInt(item));
                }
            }
           

            return listdepts;
        }
        #endregion

        #region 合同对方权限
        /// <summary>
        /// 获取合同对方列表权限表达式
        /// </summary>
        /// <param name="userId">当前用户ID</param>
        /// <param name="deptId">当前用户所属部门ID</param>
        /// <param name="funcCode">功能点标识</param>
        /// 权限类型：
        /// 1类：4是/5否
        /// 2类：1个人、2机构、3全部、6本机构、7本机构及子机构,
        /// 如果一个人拥有权限基本多种，取权限范围最大值：
        /// 1<6<2<7<3;4，5只有新建之类的才有
        /// <returns>合同对方权限表达式树</returns>
        public Expression<Func<DevCompany, bool>> GetCompanyListPermissionExpression(string funcCode, int userId, int deptId = 0)
        {
            var predicate = PredBuilder.True<DevCompany>();
            if (userId == -10000)
            {//超级管理员
                predicate = predicate.And(a => true);
            }
            else
            {
                //查询对应角色
                var pession = GetPermission(funcCode, userId);
                if (pession.listFuntypes.Contains(3))
                {//全部
                    predicate = predicate.And(p => true);
                }
                else if (pession.listFuntypes.Contains(2) || pession.listFuntypes.Contains(6) || pession.listFuntypes.Contains(7))
                {
                    var predicatedept = PredBuilder.False<DevCompany>();
                    if (pession.listFuntypes.Contains(2))
                    {//机构
                        var listdeptIds = GetDeptIds(pession.RolePermissions);
                        predicatedept = predicatedept.Or(p => listdeptIds.Contains(p.AddUser == null ? 0 : p.AddUser.DepId ?? -100));
                    }
                    if (pession.listFuntypes.Contains(6))
                    {//本机构
                        predicatedept = predicatedept.Or(p => (p.AddUserId == null ? 0 : p.AddUser.DepId ?? -100) == deptId);
                    }
                    if (pession.listFuntypes.Contains(7))
                    {//本机构及子机构
                        var listchiddeptIds = GetDeptAndChirdDetpId(deptId);
                        predicatedept = predicatedept.Or(p => listchiddeptIds.Contains(p.AddUser == null ? 0 : p.AddUser.DepId ?? -100));
                    }
                    predicatedept = predicatedept.Or(p => p.AddUserId == userId);
                    predicatedept = predicatedept.Or(p => p.FaceUserId == userId);//负责人
                    predicate = predicate.And(predicatedept);
                }
                else
                {
                    var predicate2 = PredBuilder.False<DevCompany>();
                    predicate2 = predicate2.Or(p => p.AddUserId == userId);
                    predicate2 = predicate2.Or(p => p.FaceUserId == userId);//负责人
                    predicate = predicate.And(predicate2);
                }
            }
            return predicate;
        }

        /// <summary>
        /// 判断当前用户是否有新建合同对方的权限
        /// </summary>
        /// <param name="userId">当前用户</param>
        /// <param name="funcCode">功能点标识</param>
        /// <returns>True：有权限新建，False：没权限</returns>
        public bool GetCompanyAddPermission(string funcCode, int userId)
        {
            var permission = GetPermission(funcCode, userId);
            return permission!=null&& permission.listFuntypes.Contains(4);
        }
        /// <summary>
        /// 判断当前用户是否有修改合同对方的权限
        /// </summary>
        /// <param name="userId">当前用户</param>
        /// <param name="funcCode">功能点标识</param>
        /// <param name="updateObjId">修改数据的ID</param>
        /// <returns>PermissionDicEnum</returns>
        public PermissionDicEnum GetCompanyUpdatePermission(string funcCode, int userId, int deptId, int updateObjId)
        {
            var predicate = PredBuilder.True<DevCompany>();
            if (userId == -10000)
            {//超级管理员
                predicate = predicate.And(a => true);
                return PermissionDicEnum.OK;
            }
            else
            {

                var datainfo = DevDb.Set<DevCompany>().Include(a => a.AddUser).AsNoTracking().FirstOrDefault(a => a.Id == updateObjId); //Db.Set<Company>().Find(updateObjId);
                if (datainfo == null
                    || (datainfo.Dstatus == (int)CompanyStateEnum.WeiShenHe && (datainfo.Wstatus ?? 0) != (int)WfStateEnum.WeiTiJiao && datainfo.Wstatus != (int)WfStateEnum.BeiDaHui)
                    || (datainfo.Dstatus == (int)CompanyStateEnum.ShenHeTongGuo)
                    || (datainfo.Dstatus == (int)CompanyStateEnum.ShenHeTongGuo && (datainfo.Wstatus ?? 0) != (int)WfStateEnum.WeiTiJiao && datainfo.Wstatus != (int)WfStateEnum.BeiDaHui)
                    || (datainfo.Dstatus == (int)CompanyStateEnum.YiZhongZhi)
                    )
                {
                    return PermissionDicEnum.NotState;
                }
                var pession = GetPermission(funcCode, userId);
                if (pession.listFuntypes.Contains(3))
                {//全部
                    return PermissionDicEnum.OK;
                }
                else if (pession.listFuntypes.Contains(2) || pession.listFuntypes.Contains(6) || pession.listFuntypes.Contains(7))
                {

                    if (pession.listFuntypes.Contains(2))
                    {//机构
                        var listdeptIds = GetDeptIds(pession.RolePermissions);
                        if (listdeptIds.Contains(datainfo.AddUser.DepId ?? -100))
                        {
                            return PermissionDicEnum.OK;
                        }
                    }
                    if (pession.listFuntypes.Contains(6))
                    {//本机构
                        if (datainfo.AddUser.DepId == deptId)
                        {
                            return PermissionDicEnum.OK;
                        }
                    }
                    if (pession.listFuntypes.Contains(7))
                    { //本机构及子机构
                        var listchiddeptIds = GetDeptAndChirdDetpId(deptId);
                        if (listchiddeptIds.Contains(datainfo.AddUser.DepId ?? -100))
                        {
                            return PermissionDicEnum.OK;
                        }
                    }

                }
                else if (pession.listFuntypes.Contains(1))
                {

                    if (datainfo.AddUserId == userId || datainfo.FaceUserId == userId)
                        return PermissionDicEnum.OK;

                }
                return PermissionDicEnum.None;
            }

        }

        /// <summary>
        /// 判断当前用户是否有修改合同对方次要字段的权限
        /// </summary>
        /// <param name="userId">当前用户</param>
        /// <param name="funcCode">功能点标识</param>
        /// <param name="updateObjId">修改数据的ID</param>
        /// <returns>PermissionDicEnum</returns>
        public PermissionDicEnum GetCompanySecFieldUpdatePermission(string funcCode, int userId, int deptId, int updateObjId)
        {
            var predicate = PredBuilder.True<DevCompany>();
            if (userId == -10000)
            {//超级管理员
                predicate = predicate.And(a => true);
                return PermissionDicEnum.OK;
            }
            else
            {
                var datainfo = DevDb.Set<DevCompany>().Include(a => a.AddUser).AsNoTracking().FirstOrDefault(a => a.Id == updateObjId);
                if (datainfo.Wstatus == (int)WfStateEnum.ShenPiZhong)
                {
                    return PermissionDicEnum.NotState;//审批中
                }
                var perssion = GetPermission(funcCode, userId);
                if (perssion.listFuntypes.Contains(3))
                {//全部
                    return PermissionDicEnum.OK;
                }
                else if (perssion.listFuntypes.Contains(2) || perssion.listFuntypes.Contains(6) || perssion.listFuntypes.Contains(7))
                {

                    if (perssion.listFuntypes.Contains(2))
                    {//机构
                        var listdeptIds = GetDeptIds(perssion.RolePermissions);
                        if (listdeptIds.Contains(datainfo.AddUser.DepId ?? -100))
                        {
                            return PermissionDicEnum.OK;
                        }
                    }
                    if (perssion.listFuntypes.Contains(6))
                    {//本机构
                        if (datainfo.AddUser.DepId == deptId)
                        {
                            return PermissionDicEnum.OK;
                        }
                    }
                    if (perssion.listFuntypes.Contains(7))
                    { //本机构及子机构
                        var listchiddeptIds = GetDeptAndChirdDetpId(deptId);
                        if (listchiddeptIds.Contains(datainfo.AddUser.DepId ?? -100))
                        {
                            return PermissionDicEnum.OK;
                        }
                    }

                }
                else if (perssion.listFuntypes.Contains(1))
                {

                    if (datainfo.AddUserId == userId || datainfo.FaceUserId == userId)
                        return PermissionDicEnum.OK;

                }
                return PermissionDicEnum.None;
            }

        }

        /// <summary>
        /// 判断当前用户是否有查看合同对方详情的权限
        /// </summary>
        /// <param name="userId">当前用户</param>
        /// <param name="funcCode">功能点标识</param>
        /// <param name="detailObjId">修改数据的ID</param>
        /// <returns>True：有权限，False：没权限</returns>
        public bool GetCompanyDetailPermission(string funcCode, int userId, int deptId, int detailObjId)
        {
            var predicate = PredBuilder.True<DevCompany>();
            if (userId == -10000)
            {//超级管理员
                predicate = predicate.And(a => true);
                return true;
            }
            else
            {
                var perssion = GetPermission(funcCode, userId);
                if (perssion.listFuntypes.Contains(3))
                {//全部
                    return true;
                }
                else if (perssion.listFuntypes.Contains(2) || perssion.listFuntypes.Contains(6) || perssion.listFuntypes.Contains(7))
                {
                    var datainfo = DevDb.Set<DevCompany>().Include(a => a.AddUser).Where(a => a.Id == detailObjId).FirstOrDefault();
                    if (perssion.listFuntypes.Contains(2))
                    {//机构
                        var listdeptIds = GetDeptIds(perssion.RolePermissions);
                        if (listdeptIds.Contains(datainfo.AddUser.DepId ?? -100))
                        {
                            return true;
                        }
                    }
                    if (perssion.listFuntypes.Contains(6))
                    {//本机构
                        if (datainfo.AddUser.DepId == deptId)
                        {
                            return true;
                        }
                    }
                    if (perssion.listFuntypes.Contains(7))
                    { //本机构及子机构
                        var listchiddeptIds = GetDeptAndChirdDetpId(deptId);
                        if (listchiddeptIds.Contains(datainfo.AddUser.DepId ?? -100))
                        {
                            return true;
                        }
                    }

                }
                else if (perssion.listFuntypes.Contains(1))
                {
                    var datainfo = DevDb.Set<DevCompany>().Find(detailObjId);
                    return datainfo.AddUserId == userId || datainfo.FaceUserId == userId;
                }
                return false;
            }

        }



        /// <summary>
        //获取删除合同对方权限
        /// </summary>
        /// <param name="userId">当前用户</param>
        /// <param name="funcCode">功能点标识</param>
        /// <param name="listdelIds">删除数据ID集合</param>
        /// <returns>PermissionDicEnum</returns>
        public PermissionDataInfo GetCompanyDeletePermission(string funcCode, int userId, int deptId, IList<int> listdelIds)
        {

            var predicate = PredBuilder.True<DevCompany>();
            if (userId == -10000)
            {//超级管理员
                var permiss = new PermissionDataInfo();
                predicate = predicate.And(a => true);
                permiss.Code = 0;
                return permiss;
            }
            else
            {

                var permiss = new PermissionDataInfo();
                var querylist = DevDb.Set<DevCompany>().AsEnumerable().Where(a => listdelIds.Contains(a.Id)).ToList();
                var listnot = querylist.Where(a => a.Dstatus != (byte)SysDataSateEnum.Unreviewed
                || (a.Dstatus == (byte)SysDataSateEnum.Unreviewed && (a.Wstatus ?? 0) != 0)).Select(a => a.Name).ToList();
                if (listnot.Count() > 0)
                {
                    permiss.Code = 4;
                    permiss.noteAllow = listnot;
                    return permiss;
                }
                var perssion = GetPermission(funcCode, userId);
                if (perssion.listFuntypes.Contains(3))
                {//全部
                    permiss.Code = 0;
                    return permiss;
                }
                else if (perssion.listFuntypes.Contains(2) || perssion.listFuntypes.Contains(6) || perssion.listFuntypes.Contains(7))
                {
                    List<int> tempIds = new List<int>();

                    if (perssion.listFuntypes.Contains(2))
                    {//机构
                        var listdeptIds = GetDeptIds(perssion.RolePermissions);
                        var temIds1 = querylist.Where(a => listdeptIds.Contains(a.AddUser.DepId ?? -100)).Select(a => a.Id).ToList();
                        tempIds.AddRange(temIds1);


                    }
                    if (perssion.listFuntypes.Contains(6))
                    {//本机构
                        var temIds2 = querylist.Where(a => a.AddUser != null && a.AddUser.DepId == deptId).Select(a => a.Id).ToList();
                        tempIds.AddRange(temIds2);
                    }
                    if (perssion.listFuntypes.Contains(7))
                    { //本机构及子机构
                        var listchiddeptIds = GetDeptAndChirdDetpId(deptId);
                        var temIds3 = querylist.Where(a => listchiddeptIds.Contains(a.AddUser.DepId ?? -100)).Select(a => a.Id).ToList();
                        tempIds.AddRange(temIds3);
                    }
                    var temIds4 = querylist.Where(a => a.AddUserId == userId || a.FaceUserId == userId).Select(a => a.Id).ToList();
                    tempIds.AddRange(temIds4);

                    var listnotdeptdata = querylist.Where(a => !tempIds.Contains(a.Id)).Select(a => a.Name).ToList();
                    if (listnotdeptdata.Count() > 0)
                    {
                        permiss.Code = 3;//部分没权限
                        permiss.noteAllow = listnotdeptdata;//没权限的数据集合
                    }
                    return permiss;


                }
                else if (perssion.listFuntypes.Contains(1))
                {
                    var usertempIds = querylist.Where(a => a.AddUserId == userId || a.FaceUserId == userId).Select(a => a.Id).ToList();
                    var listnotdeptdata = querylist.Where(a => !usertempIds.Contains(a.Id)).Select(a => a.Name).ToList();
                    if (listnotdeptdata.Count() > 0)
                    {
                        permiss.Code = 3;//部分没权限
                        permiss.noteAllow = listnotdeptdata;//没权限的数据集合
                    }
                    return permiss;
                }
                permiss.Code = 1;//无权限
                return permiss;


            }

        }
        #endregion
    }
}
