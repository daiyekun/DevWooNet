using AutoMapper;
using Dev.WooNet.Common.Models;
using Dev.WooNet.Common.Utility;
using Dev.WooNet.IWooService;
using Dev.WooNet.Model.DevDTO;
using Dev.WooNet.Model.Enums;
using Dev.WooNet.Model.ExtendModel;
using Dev.WooNet.WebAPI.Extend;
using Dev.WooNet.WebCore.Extend;
using Dev.WooNet.WebCore.FilterExtend;
using Dev.WooNet.WebCore.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace Dev.WooNet.WebAPI.Areas.DevCommon.Controllers
{
    /// <summary>
    /// 系统附件相关操作，
    /// 比如：上传、下载
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [TokenSessionActionFilter]
    public class DevFileCommonController : ControllerBase
    {
        private IMapper _IMapper;
        private IDevCompfileService _IDevCompfileService;
        private IConfiguration _Configuration;
        public DevFileCommonController(IDevCompfileService iDevCompfileService, IMapper iMapper
            , IConfiguration iConfiguration)
        {
            _IMapper = iMapper;
            _IDevCompfileService = iDevCompfileService;
            _Configuration = iConfiguration;


        }
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="downOrUpload">上传文件时请求对象</param>
        /// <returns>返回上传后信息</returns>
        [Route("upload")]
        [HttpPost]
        [DevDisableFormValueModelBinding]
        public async Task<IActionResult> UploadAsync([FromForm] DownAndUploadInfo downOrUpload)
        {


            var userId = HttpContext.User.Claims.GetTokenUserId();
            var folderName = EmunUtility.GetDesc(typeof(DevFoldersEnum), downOrUpload.Folderenum);
            var path = Path.Combine(
                         Directory.GetCurrentDirectory(), "Uploads", folderName
                         );
            FormValueProvider formModel;
            UploadFileInfo uploadFile = new UploadFileInfo();
            if (downOrUpload.Folderenum == 11)
            {//水印
                uploadFile.IsGuidName = false;
                uploadFile.FileName = "devTextWordWaterMark.dotx";
            }
            formModel = await Request.DevStreamFiles(path, uploadFile, downOrUpload.Folderenum, userId);
            uploadFile.FolderName = folderName;//文件夹名称
            var viewModel = new DevViewModel();

            var bindingSuccessful = await TryUpdateModelAsync(viewModel, prefix: "",
                valueProvider: formModel);

            if (!bindingSuccessful)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
            }
            return new DevResultJson(new AjaxResult<UploadFileInfo>()
            {
                msg = "上传成功",
                data = uploadFile

               
              
            }) ;
        }

        /// <summary>
        /// 下载
        /// 
        /// </summary>
        /// <param name="downOrUpload">下载请求对象</param>
        /// <returns>返回文件对象</returns>
        [Route("download")]
        [HttpPost]
        public IActionResult Download([FromBody]DownAndUploadInfo downOrUpload)
        {
            string guidFileName = string.Empty;
            switch (downOrUpload.Folderenum)
            {
                case 0://客户附件
                case 1://供应商
                case 2://其他对方
                    guidFileName = _IDevCompfileService.Find(downOrUpload.Id).GuidFileName;
                    break;
            //    case 4://项目附件
            //        guidFileName = _IProjAttachmentService.Find(downLoadAndUploadRequestInfo.Id).GuidFileName;
            //        break;
            //    case 5://合同附件
            //        guidFileName = _IContAttachmentService.Find(downLoadAndUploadRequestInfo.Id).GuidFileName;
            //        break;
            //    case 6://合同文本
            //        if (downLoadAndUploadRequestInfo.Dtype == 1)
            //        {
            //            var contText = _IContTextHistoryService.Find(downLoadAndUploadRequestInfo.Id);
            //            if (contText != null)
            //            {
            //                if (!contText.GuidFileName.EndsWith(".docx") && contText.IsFromTemp == (int)SourceTxtEnum.TempDraft)
            //                {


            //                    guidFileName = contText.FileName;

            //                }
            //                else
            //                {
            //                    guidFileName = contText.GuidFileName;
            //                }
            //            }
            //        }
            //        else if (downLoadAndUploadRequestInfo.Dtype == 2)
            //        {//下载PDF
            //            var contText = _IContTextService.Find(downLoadAndUploadRequestInfo.Id);
            //            if (contText != null)
            //            {
            //                guidFileName = contText.FileName;
            //            }
            //        }
            //        else
            //        {
            //            var contText = _IContTextService.Find(downLoadAndUploadRequestInfo.Id);
            //            if (contText != null)
            //            {
            //                if (!contText.GuidFileName.EndsWith(".docx") && contText.IsFromTemp == (int)SourceTxtEnum.TempDraft)
            //                {
            //                    if (downLoadAndUploadRequestInfo.DownType == 1)
            //                    {
            //                        guidFileName = contText.WordPath;
            //                    }
            //                    else
            //                    {
            //                        guidFileName = contText.FileName;
            //                    }
            //                }
            //                else
            //                {
            //                    guidFileName = contText.GuidFileName;
            //                }
            //            }

            //        }


            //        break;
            //    case 7://合同文本归档电子版下载
            //        guidFileName = _IContTextArchiveItemService.Find(downLoadAndUploadRequestInfo.Id).GuidFileName;
            //        break;
            //    case 8://单品附件
            //        guidFileName = _IBcAttachmentService.Find(downLoadAndUploadRequestInfo.Id).GuidFileName;
            //        break;
            //    case 9://标的交付附件
            //        guidFileName = _IContSubDeService.Find(downLoadAndUploadRequestInfo.Id).GuidFileName;
            //        break;
            //    case 12://招标附件
            //        guidFileName = _ITenderAttachmentService.Find(downLoadAndUploadRequestInfo.Id).GuidFileName;
            //        break;
            //    case 13://询价附件
            //        guidFileName = _IInquiryAttachmentService.Find(downLoadAndUploadRequestInfo.Id).GuidFileName;
            //        break;
            //    case 14://约谈附件
            //        guidFileName = _IQuestioningAttachmentService.Find(downLoadAndUploadRequestInfo.Id).GuidFileName;
            //        break;

            //    case 15://电子签章
            //        guidFileName = _IQuestioningAttachmentService.Find(downLoadAndUploadRequestInfo.Id).GuidFileName;
            //        break;
            //    case 16://资金附件
            //        guidFileName = _IActFinceFileService.Find(downLoadAndUploadRequestInfo.Id).GuidFileName;
            //        break;
            //    case 17://资金附件
            //        guidFileName = _IInvoFileService.Find(downLoadAndUploadRequestInfo.Id).GuidFileName;
            //        break;

            }
            ////string filename = fileinfo.Path;

            //if (guidFileName.StartsWith('~'))
            //{
            //    var filearr = StringHelper.Strint2ArrayString(guidFileName, "/");

            //    guidFileName = filearr.LastOrDefault();
            //}
            
            var pathf = Path.Combine(
                             Directory.GetCurrentDirectory(), "Uploads", EmunUtility.GetDesc(typeof(DevFoldersEnum), downOrUpload.Folderenum),
                             guidFileName);

            var downInfo = FileStreamHelper.Download(pathf);
            //var s = ToBase64String(downInfo.NfFileStream);
            var excelfile = new ExportFileInfo
            {
                FileName = guidFileName,
                Memi = downInfo.Memi,
                FilePath = $"Uploads/{EmunUtility.GetDesc(typeof(DevFoldersEnum), downOrUpload.Folderenum)}",
                DowIp = _Configuration["DevAppSeting:filedownIp"]


            };
            var ajaxResult = new AjaxResult<ExportFileInfo>()
            {
                Result = true,
                data = excelfile
            };
            return new JsonResult(ajaxResult);

            // return File(downInfo.NfFileStream, downInfo.Memi, downInfo.FileName);

        }

    }
}
