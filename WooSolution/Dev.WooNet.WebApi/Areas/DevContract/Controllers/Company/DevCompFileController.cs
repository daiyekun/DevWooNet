using AutoMapper;
using Dev.WooNet.Common.Models;
using Dev.WooNet.Common.Utility;
using Dev.WooNet.IWooService;
using Dev.WooNet.Model.DevDTO;
using Dev.WooNet.Model.Enums;
using Dev.WooNet.Model.ExtendModel;
using Dev.WooNet.Model.Models;
using Dev.WooNet.WebCore.Extend;
using Dev.WooNet.WebCore.FilterExtend;
using Dev.WooNet.WebCore.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NF.Common.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;



namespace Dev.WooNet.WebAPI.Areas.DevContract.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [TokenSessionActionFilter]
    public class DevCompFileController : ControllerBase
    {
        private IMapper _IMapper;
        private IDevCompfileService _IDevCompfileService;
        private IConfiguration _Configuration;

        public DevCompFileController(IMapper iMapper, IDevCompfileService iDevCompfileService
            , IConfiguration iConfiguration)
        {
            _IDevCompfileService = iDevCompfileService;
            _IMapper = iMapper;
            _Configuration = iConfiguration;

        }


        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="pgInfo">分页对象</param>
        /// <returns></returns>
        [Route("list")]
        [HttpPost]
        public IActionResult GetList([FromBody] PgRequestInfo pgInfo)
        {
            var userId = HttpContext.User.Claims.GetTokenUserId();
            var pageInfo = new NoPageInfo<DevCompfile>();
            var prdAnd = PredBuilder.True<DevCompfile>();
            prdAnd = prdAnd.And(a => a.IsDelete == 0 && ((pgInfo != null && a.CompId == pgInfo.otherId) || a.CompId == -userId));
            var prdOr = PredBuilder.False<DevCompfile>();

            var pagelist = _IDevCompfileService.GetList(pageInfo, prdAnd, a => a.Id, false);
            return new DevResultJson(pagelist);

        }
        /// <summary>
        ///新增修改
        /// </summary>
        /// <param name="infodto">新增修改信息</param>
        /// <returns></returns>
        [Route("save")]
        [HttpPost]
        public IActionResult Save([FromBody] DevCompfileDTO infodto)
        {
            var userId = HttpContext.User.Claims.GetTokenUserId();
            infodto.FilePath = $"Uploads/{infodto.FolderName}/{infodto.GuidFileName}";
            if (infodto.Id > 0)
            {
                var cinfo = _IDevCompfileService.Find(infodto.Id);
                var saveinfo = _IMapper.Map<DevCompfileDTO, DevCompfile>(infodto, cinfo);
                saveinfo.UpdateUserId = userId;
                saveinfo.UpdateDateTime = DateTime.Now;
                _IDevCompfileService.Update(saveinfo);

            }
            else
            {
                var saveinfo = _IMapper.Map<DevCompfile>(infodto);
                saveinfo.AddUserId = userId;
                saveinfo.AddDateTime = DateTime.Now;
                saveinfo.UpdateUserId = userId;
                saveinfo.DowNumber = 0;
                saveinfo.UpdateDateTime = DateTime.Now;
                saveinfo.CompId = (saveinfo.CompId ?? 0) == 0 ? -userId : saveinfo.CompId;
                _IDevCompfileService.Add(saveinfo);

            }
            return new DevResultJson(new AjaxResult()
            {
                msg = "success",
                code = (int)MessageEnums.success,


            });


        }
        /// <summary>
        /// 修改页面
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [Route("showView")]
        [HttpGet]
        public IActionResult ShowView(int Id)
        {
            return new DevResultJson(new AjaxResult<DevCompfileDTO>()
            {
                msg = "",
                code = 0,
                data = _IDevCompfileService.GetInfoById(Id)


            });

        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="Ids">删除IDs</param>
        /// <returns></returns>
        [Route("delete")]
        [HttpGet]
        public IActionResult Delete(string Ids)
        {
            string strsql = $"DELETE from dev_compfile where Id in({Ids})";

            _IDevCompfileService.ExecuteSqlCommand(strsql);
            return new DevResultJson(new AjaxResult()
            {
                msg = "success",
                code = (int)MessageEnums.success,


            });

        }
        /// <summary>
        /// pdf预览
        /// </summary>
        /// <param name="Id">预览ID</param>
        /// <returns></returns>
       
        [Route("getpdf")]
        [HttpGet]
        public IActionResult GetPdf(int Id,int Folder)
        {
            string guidFileName = string.Empty;
            var custfile = _IDevCompfileService.Find(Id);
            if (custfile != null)
            {
                guidFileName = custfile.GuidFileName;
            }
            if (guidFileName.StartsWith('~'))
            {
                var filearr = StringHelper.Strint2ArrayString(guidFileName, "/");

                guidFileName = filearr.LastOrDefault();
            }
            var pathf = Path.Combine(
                            Directory.GetCurrentDirectory(), "Uploads", EmunUtility.GetDesc(typeof(DevFoldersEnum), Folder),
                            guidFileName);

            var downInfo = FileStreamHelper.Download(pathf);
            return File(downInfo.NfFileStream, downInfo.Memi, downInfo.FileName);
        }
        /// <summary>
        /// Word文件预览
        /// </summary>
        /// <returns></returns>
        [Route("wordtopdfview")]
        [HttpGet]
        public IActionResult WordtoPdfView(int Id, int Folder)
        {
            string guidFileName = string.Empty;
            var contText = _IDevCompfileService.Find(Id);
           
           
           
            var wordname = guidFileName;
           
            var pathf = Path.Combine(
                            Directory.GetCurrentDirectory(), "Uploads", EmunUtility.GetDesc(typeof(DevFoldersEnum), Folder),
                            wordname);
            var pdfpath = Path.Combine(
                             Directory.GetCurrentDirectory(), "Uploads", EmunUtility.GetDesc(typeof(DevFoldersEnum), 6),
                             guidFileName.Replace(".docx", ".pdf"));
            //var markpath = Path.Combine(
            //                Directory.GetCurrentDirectory(), "Uploads", EmunUtility.GetDesc(typeof(DevFoldersEnum), 11),
            //                "ContractTextWordWaterMark.dotx");

            MsWordToPdfHelper wpfh = new MsWordToPdfHelper();
            wpfh.ConvertWordToPdf(pathf, pdfpath);
           

            var downInfo = FileStreamHelper.Download(pdfpath);
            return File(downInfo.NfFileStream, downInfo.Memi, downInfo.FileName);
        }

        /// <summary>
        /// 图片预览
        /// </summary>
        /// <returns></returns>
        [Route("pictureview")]
        [HttpGet]
        public IActionResult PictureView(int CompId)
        {
            return new DevResultJson(new AjaxResult<IList<PicViewDTO>>()
            {
                msg = "",
                code = 0,
                data = _IDevCompfileService.GetPicViews(CompId, _Configuration["DevAppSeting:filedownIp"])


            });

        }
    }
}
