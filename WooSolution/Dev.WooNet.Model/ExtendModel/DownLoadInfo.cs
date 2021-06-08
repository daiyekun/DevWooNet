using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dev.WooNet.Model.ExtendModel
{
    public class DownLoadInfo
    {
        /// <summary>
        /// 文件流
        /// </summary>
        public FileStream NfFileStream { get; set; }
        public string Memi { get; set; }
        /// <summary>
        /// 文件名称
        /// </summary>
        public string FileName { get; set; }
    }
    /// <summary>
    /// 上传文件相关信息
    /// </summary>
    public class UploadFileInfo
    {
        /// <summary>
        /// 原始文件名称
        /// </summary>
        public string SourceFileName { get; set; }
        /// <summary>
        /// Guid后文件名称
        /// </summary>
        public string GuidFileName { get; set; }
        /// <summary>
        /// 存储文件名称
        /// </summary>
        public string FolderName { get; set; }
        /// <summary>
        /// 没有扩展的文件名
        /// </summary>
        public string NotExtenFileName { get; set; }
        /// <summary>
        /// 文件扩展
        /// </summary>
        public string Extension { get; set; }
        /// <summary>
        /// 是否使用Guid文件名称
        /// </summary>
        public bool RemGuidName { get; set; } = true;


    }
}
