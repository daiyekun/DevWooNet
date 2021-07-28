using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dev.WooNet.Common.Models
{

    /// <summary>
    /// 用于过滤actionfilter
    /// </summary>
    public class ActionFilterData
    {
     public static Dictionary<string, int> ActionDatas = new Dictionary<string, int>
                     {
                      {"GetPdf", 0},//pdf预览
                      {"WordtoPdfView", 1},//word to pdf 预览
                      

                };
    }
}
