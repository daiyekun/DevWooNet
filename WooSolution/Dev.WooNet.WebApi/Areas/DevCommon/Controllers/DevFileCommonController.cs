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
        public DevFileCommonController(IDevCompfileService iDevCompfileService, IMapper iMapper)
        {
            _IMapper = iMapper;
            _IDevCompfileService = iDevCompfileService;


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

    }
}
