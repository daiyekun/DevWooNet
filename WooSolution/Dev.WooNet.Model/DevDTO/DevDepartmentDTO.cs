using Dev.WooNet.Common.Models;
using Dev.WooNet.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dev.WooNet.Model.DevDTO
{

    /// <summary>
    /// 部门
    /// </summary>
    public class DevDepartmentDTO: DevDepartment, IModelDTO
    {
        /// <summary>
        /// 类别名称
        /// </summary>
        public string CateName { get; set; }
        /// <summary>
        /// 上级名称
        /// </summary>
        public string PName { get; set; }
        /// <summary>
        /// 是否是签约主体
        /// </summary>
        public string IsMainDic { get; set; }
        /// <summary>
        ///法人
        /// </summary>

        public string LawPerson { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string TaxId { get; set; }
        /// <summary>
        /// 银行
        /// </summary>
        public string BankName { get; set; }
        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 邮编
        /// </summary>
        public string ZipCode { get; set; }
        /// <summary>
        /// 传真
        /// </summary>
        public string Fax { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string TelePhone { get; set; }
        /// <summary>
        /// 发票名称
        /// </summary>
        public string InvoiceName { get; set; }
    }
    /// <summary>
    /// 部门数据，主要用于保存等操作
    /// </summary>
    public class DepartData
    {
        /// <summary>
        /// 部门基本信息
        /// </summary>
        public DevDepartment DeptInfo { get; set; }
        /// <summary>
        /// 签约主体
        /// </summary>
        public DevDeptmain DeptMain { get; set; }
    }
}
