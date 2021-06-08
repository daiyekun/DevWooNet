using Dev.WooNet.Common.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dev.WooNet.Model.Enums
{
    /// <summary>
    /// 上传下载文件夹枚举
    /// </summary>
    [EnumClass(Max = 50, Min = 0, None = -1)]
    public  enum DevFoldersEnum
    {
        /// <summary>
        /// 客户附件（CustomerFile）：0
        /// </summary>
        [EnumItem(Value = 0, Desc = "CustomerFile")]
        CustomerFile=0,
        /// <summary>
        /// 供应商附件（SupplierFile）：1
        /// </summary>
        [EnumItem(Value = 1, Desc = "SupplierFile")]
        SupplierFile = 1,
        /// <summary>
        /// 其他对方附件（OtherFile）：2
        /// </summary>
        [EnumItem(Value = 2, Desc = "OtherFile")]
        OtherFile = 2,
        /// <summary>
        /// ExcelExport:Excel导出临时存储
        /// </summary>
        [EnumItem(Value = 3, Desc = "ExcelExport")]
        ExcelExport = 3,
        /// <summary>
        /// 项目附件：ProjectFile
        /// </summary>
        [EnumItem(Value = 4, Desc = "ProjectFile")]
        ProjectFile=4,
        /// <summary>
        /// 合同附件：ContractFile
        /// </summary>
        [EnumItem(Value = 5, Desc = "ContractFile")]
        ContractFile = 5,
        /// <summary>
        /// 合同文本：ContractTextFile
        /// </summary>
        [EnumItem(Value = 6, Desc = "ContractTextFile")]
        ContractTextFile = 6,
        /// <summary>
        /// 合同文本归档电子版：ContractTextEleFile
        /// </summary>
        [EnumItem(Value = 7, Desc = "ContractTextEleFile")]
        ContractTextEleFile = 7,
        /// <summary>
        /// 单品附件：DanPingFile
        /// </summary>
        [EnumItem(Value = 8, Desc = "DanPingFile")]
        DanPingFile = 8,
        /// <summary>
        /// 交付附件：SubDevFile
        /// </summary>
        [EnumItem(Value = 9, Desc = "SubDevFile")]
        SubDevFile = 9,
        /// <summary>
        /// 合同模板：ContractTemplates
        /// </summary>
        [EnumItem(Value = 10, Desc = "ContractTemplates")]
        ContractTemplates = 10,
        /// <summary>
        /// 水印：WaterMarkTemplate
        /// </summary>
        [EnumItem(Value = 11, Desc = "WaterMarkTemplate")]
        WaterMarkTemplate = 11,
       

    }
}
