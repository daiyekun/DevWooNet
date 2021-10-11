using Dev.WooNet.Common.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dev.WooNet.Model.Enums
{
    /// <summary>
    /// 数据字典枚举
    /// </summary>
    [EnumClass(Max = 100, Min = 0, None = -1)]
    public enum DatadicEnum
    {
        /// <summary>
        /// 机构类别：0
        /// </summary>
        [EnumItem(Value = 0, Desc = "机构类别")]
        depart = 0,
        /// <summary>
        /// 合同类别：1
        /// </summary>
        [EnumItem(Value = 1, Desc = "合同类别")]
        contract = 1,
        /// <summary>
        /// 供应商类别：2
        /// </summary>
        [EnumItem(Value = 2, Desc = "供应商类别")]
        suppliers = 2,
        /// <summary>
        /// 客户类别：3
        /// </summary>
        [EnumItem(Value = 3, Desc = "客户类别")]
        customer = 3,
        /// <summary>
        /// 其他对方类别：4
        /// </summary>
        [EnumItem(Value = 4, Desc = "其他对方类别")]
        otherType = 4,
        /// <summary>
        /// 客户级别：5
        /// </summary>
        [EnumItem(Value = 5, Desc = "客户级别")]
        customerLevel = 5,
        /// <summary>
        /// 信用等级：6
        /// </summary>
        [EnumItem(Value = 6, Desc = "客户信用等级")]
        customerCaredit = 6,
        /// <summary>
        /// 公司类型：7
        /// </summary>
        [EnumItem(Value = 7, Desc = "公司类型")]
        companyType = 7,
        /// <summary>
        /// 客户附件类别:8
        /// </summary>
        [EnumItem(Value = 8, Desc = "客户附件类别")]
        customerAttachment = 8,
        /// <summary>
        /// 供应商附件类别:9
        /// </summary>
        [EnumItem(Value = 9, Desc = "供应商附件类别")]
        supplierAttachment = 9,
        /// <summary>
        /// 其他对方附件类别:9
        /// </summary>
        [EnumItem(Value = 10, Desc = "其他对方附件类别")]
        otherAttachment = 10,
        /// <summary>
        /// 供应商级别:11
        /// </summary>
        [EnumItem(Value = 11, Desc = "供应商级别")]
        supplierLevel = 11,
        /// <summary>
        /// 其他对方级别:12
        /// </summary>
        [EnumItem(Value = 12, Desc = "其他对方级别")]
        otherLevel = 12,
        /// <summary>
        /// 项目类别:13
        /// </summary>
        [EnumItem(Value = 13, Desc = "项目类别")]
        project = 13,
        /// <summary>
        /// 项目类别:projectFile:13
        /// </summary>
        [EnumItem(Value = 14, Desc = "项目附件类别")]
        projectFile = 14,
        /// <summary>
        /// 合同来源:contSource
        /// </summary>
        [EnumItem(Value = 15, Desc = "合同来源")]
        contSource = 15,
        /// <summary>
        /// 文本类别:ContTxt
        /// </summary>
        [EnumItem(Value = 16, Desc = "文本类别")]
        ContTxt = 16,
        /// <summary>
        /// 结算方式:SettlModes:17
        /// </summary>
        [EnumItem(Value = 17, Desc = "结算方式")]
        SettlModes = 17,
        /// <summary>
        /// 合同附件类别:ContAttachment:18
        /// </summary>
        [EnumItem(Value = 18, Desc = "合同附件类别")]
        ContAttachment = 18,
        /// <summary>
        /// 发票类别:Invoice:19
        /// </summary>
        [EnumItem(Value = 19, Desc = "发票类别")]
        Invoice = 19,
        /// <summary>
        /// 单品附件类别:BcAttachment
        /// </summary>
        [EnumItem(Value = 20, Desc = "单品附件类别")]
        BcAttachment = 20,
        /// <summary>
        /// 交付方式:Dev
        /// </summary>
        [EnumItem(Value = 30, Desc = "交付方式")]
        Dev = 30,
        /// <summary>
        /// 实际资金附件
        /// </summary>
        [EnumItem(Value = 31, Desc = "实际资金附件类别")]
        ActFinceFile = 31,
        /// <summary>
        /// 发票附件类别
        /// </summary>
        [EnumItem(Value = 32, Desc = "发票附件类别")]
        InvoFile = 32,
        /// <summary>
        /// 供应商信用等级
        /// </summary>
        [EnumItem(Value = 33, Desc = "供应商信用等级")]
        GYSXinYong = 33,
        /// <summary>
        /// 其他对方信用等级
        /// </summary>
        [EnumItem(Value = 34, Desc = "其他对方信用等级")]
        QTDFXinYong = 34,
    }
}
