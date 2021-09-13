using Dev.WooNet.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dev.WooNet.Model.ExtendModel
{

    #region 菜单权限信息
    /// <summary>
    /// 设置菜单
    /// </summary>
    public class SysModelFuncSet 
    {
        /// <summary>
        /// 权限ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 菜单ID
        /// </summary>
        public int Mid { get; set; }
        /// <summary>
        /// 权限类别
        /// </summary>
        public FunTypeEnums FunType { get; set; }
        /// <summary>
        /// 权限ID
        /// </summary>
        public int FunId { get; set; }
        /// <summary>
        /// 菜单名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 权限标识字符串
        /// </summary>
        public string FunStr { get; set; }
    
    }
    #endregion


    /// <summary>
    ///设置菜单
    /// </summary>
    public class DevSysModelUtility
    {
        /// <summary>
        /// 权限分类
        /// </summary>
        /// <returns></returns>
        public static IList<SysModelFuncSet> InitFuncts()
        {
            IList<SysModelFuncSet> sysModelFuncs = new List<SysModelFuncSet>();
            SysModelFuncSet sysModelFunc = new SysModelFuncSet();
            sysModelFunc.Id = 1;
            sysModelFunc.Name = "合同对方管理";
            sysModelFunc.Mid = 21;
            sysModelFunc.FunType = FunTypeEnums.FunTypeN;
            sysModelFunc.FunId = 1;
            sysModelFunc.FunStr = "CompanyMg";
            sysModelFuncs.Add(sysModelFunc);

            #region 客户权限管理
            sysModelFunc = new SysModelFuncSet();
            sysModelFunc.Id = 2;
            sysModelFunc.Name = "客户列表权限";
            sysModelFunc.Mid = 22;
            sysModelFunc.FunType = FunTypeEnums.FunType1;
            sysModelFunc.FunStr = "CustomerList";
            sysModelFuncs.Add(sysModelFunc);

            sysModelFunc = new SysModelFuncSet();
            sysModelFunc.Id = 3;
            sysModelFunc.Name = "客户新增权限";
            sysModelFunc.Mid = 22;
            sysModelFunc.FunType = FunTypeEnums.FunType0;
            sysModelFunc.FunStr = "CustomerAdd";
            sysModelFuncs.Add(sysModelFunc);

            sysModelFunc = new SysModelFuncSet();
            sysModelFunc.Id = 4;
            sysModelFunc.Name = "客户修改权限";
            sysModelFunc.Mid = 22;
            sysModelFunc.FunType = FunTypeEnums.FunType1;
            sysModelFunc.FunStr = "CustomerUpdate";
            sysModelFuncs.Add(sysModelFunc);

            sysModelFunc = new SysModelFuncSet();
            sysModelFunc.Id = 5;
            sysModelFunc.Name = "客户删除权限";
            sysModelFunc.Mid = 22;
            sysModelFunc.FunType = FunTypeEnums.FunType1;
            sysModelFunc.FunStr = "CustomerDelete";
            sysModelFuncs.Add(sysModelFunc);

            sysModelFunc = new SysModelFuncSet();
            sysModelFunc.Id = 6;
            sysModelFunc.Name = "客户次要字段权限";
            sysModelFunc.Mid = 22;
            sysModelFunc.FunType = FunTypeEnums.FunType1;
            sysModelFunc.FunStr = "CustomerSecondaryField";
            sysModelFuncs.Add(sysModelFunc);

            #endregion

            #region 供应商权限管理
            sysModelFunc = new SysModelFuncSet();
            sysModelFunc.Id = 7;
            sysModelFunc.Name = "供应商列表权限";
            sysModelFunc.Mid = 23;
            sysModelFunc.FunType = FunTypeEnums.FunType1;
            sysModelFunc.FunStr = "SupplierList";
            sysModelFuncs.Add(sysModelFunc);

            sysModelFunc = new SysModelFuncSet();
            sysModelFunc.Id = 8;
            sysModelFunc.Name = "供应商新增权限";
            sysModelFunc.Mid = 23;
            sysModelFunc.FunType = FunTypeEnums.FunType0;
            sysModelFunc.FunStr = "SupplierAdd";
            sysModelFuncs.Add(sysModelFunc);

            sysModelFunc = new SysModelFuncSet();
            sysModelFunc.Id = 9;
            sysModelFunc.Name = "供应商修改权限";
            sysModelFunc.Mid = 23;
            sysModelFunc.FunType = FunTypeEnums.FunType1;
            sysModelFunc.FunStr = "SupplierUpdate";
            sysModelFuncs.Add(sysModelFunc);

            sysModelFunc = new SysModelFuncSet();
            sysModelFunc.Id = 10;
            sysModelFunc.Name = "供应商删除权限";
            sysModelFunc.Mid = 23;
            sysModelFunc.FunType = FunTypeEnums.FunType1;
            sysModelFunc.FunStr = "SupplierDelete";
            sysModelFuncs.Add(sysModelFunc);

            sysModelFunc = new SysModelFuncSet();
            sysModelFunc.Id = 11;
            sysModelFunc.Name = "供应商次要字段权限";
            sysModelFunc.Mid = 23;
            sysModelFunc.FunType = FunTypeEnums.FunType1;
            sysModelFunc.FunStr = "SupplierSecondaryField";
            sysModelFuncs.Add(sysModelFunc);

            #endregion

            #region 其他对方权限管理
            sysModelFunc = new SysModelFuncSet();
            sysModelFunc.Id = 12;
            sysModelFunc.Name = "其他对方列表权限";
            sysModelFunc.Mid = 24;
            sysModelFunc.FunType = FunTypeEnums.FunType1;
            sysModelFunc.FunStr = "OtherPartyList";
            sysModelFuncs.Add(sysModelFunc);

            sysModelFunc = new SysModelFuncSet();
            sysModelFunc.Id = 13;
            sysModelFunc.Name = "其他对方新增权限";
            sysModelFunc.Mid = 24;
            sysModelFunc.FunType = FunTypeEnums.FunType0;
            sysModelFunc.FunStr = "OtherPartyAdd";
            sysModelFuncs.Add(sysModelFunc);

            sysModelFunc = new SysModelFuncSet();
            sysModelFunc.Id = 14;
            sysModelFunc.Name = "其他对方修改权限";
            sysModelFunc.Mid = 24;
            sysModelFunc.FunType = FunTypeEnums.FunType1;
            sysModelFunc.FunStr = "OtherPartyUpdate";
            sysModelFuncs.Add(sysModelFunc);

            sysModelFunc = new SysModelFuncSet();
            sysModelFunc.Id = 15;
            sysModelFunc.Name = "其他对方删除权限";
            sysModelFunc.Mid = 24;
            sysModelFunc.FunType = FunTypeEnums.FunType1;
            sysModelFunc.FunStr = "OtherPartyDelete";
            sysModelFuncs.Add(sysModelFunc);

            sysModelFunc = new SysModelFuncSet();
            sysModelFunc.Id = 16;
            sysModelFunc.Name = "其他对方次要字段权限";
            sysModelFunc.Mid = 24;
            sysModelFunc.FunType = FunTypeEnums.FunType1;
            sysModelFunc.FunStr = "OtherPartySecondaryField";
            sysModelFuncs.Add(sysModelFunc);

            #endregion







            return sysModelFuncs;
        }
    }
}
