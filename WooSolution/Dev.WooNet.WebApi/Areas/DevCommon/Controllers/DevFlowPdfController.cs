using AutoMapper;
using Dev.WooNet.IWooService;
using Dev.WooNet.Model.Models;
using Dev.WooNet.Model.Enums;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dev.WooNet.Model.DevDTO.DevFlow.FlowPdfModel;
using Wkhtmltopdf.NetCore;
using System.IO;

namespace Dev.WooNet.WebAPI.Areas.DevCommon.Controllers
{

    /// <summary>
    /// pdf文件
    /// https://blog.csdn.net/zyq025/article/details/112976951?utm_medium=distribute.pc_relevant.none-task-blog-2%7Edefault%7EBlogCommendFromBaidu%7Edefault-17.no_search_link&depth_1-utm_source=distribute.pc_relevant.none-task-blog-2%7Edefault%7EBlogCommendFromBaidu%7Edefault-17.no_search_link
    /// </summary>

    [Area("Flow")]
    [Route("Flow/[controller]/[action]")]
    //[Route("api/[controller]")]
    //[ApiController]

    public class DevFlowPdfController : Controller
    {
        /// <summary>
        /// 映射
        /// </summary>
        private IMapper _IMapper;
        /// <summary>
        /// 实例服务
        /// </summary>
        private IDevAppInstService _IDevAppInstService;
        /// <summary>
        /// 生成pdf相关数据
        /// </summary>
        private IDevFlowPdfService _IDevFlowPdfService;
        private readonly IGeneratePdf _IGeneratePdf;
        public DevFlowPdfController(IMapper iMapper, 
            IDevAppInstService iDevAppInstService, 
            IDevFlowPdfService iDevFlowPdfService,
            IGeneratePdf iGeneratePdf
            )
        {
            _IMapper = iMapper;
            _IDevAppInstService = iDevAppInstService;
            _IDevFlowPdfService = iDevFlowPdfService;
            _IGeneratePdf = iGeneratePdf;
        }
        

        
        [HttpGet]
      
        public async Task<IActionResult> PdfViewTest()
        {
            var pdf = await _IGeneratePdf.GetByteArray("PdfTest");
            var pdfstram = new MemoryStream();
            pdfstram.Write(pdf,0, pdf.Length);
            pdfstram.Position = 0;
            return new FileStreamResult(pdfstram,"application/pdf");

        }

        [HttpGet]
        public async Task<IActionResult> ConverToPdf(int InceId)
        {
            var pdfstram = new MemoryStream();
            var wfinfo = _IDevAppInstService.Find(InceId);
            switch (wfinfo.ObjType)
            {
                case (int)FlowObjEnums.Customer://客户
                    {
                        CompanyInfo info = _IDevFlowPdfService.GetCompanyFlowPdfInfo(wfinfo);
                        var  pdf = await _IGeneratePdf.GetByteArray("CustomerPDF", info);
                        pdfstram.Write(pdf, 0, pdf.Length);
                        pdfstram.Position = 0;
                    }
                    break;

            }

           


           
           
            
            return new FileStreamResult(pdfstram, "application/pdf");
        }
       
    }
}
