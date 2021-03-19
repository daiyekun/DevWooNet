using Dev.WooNet.Common.Models;
using Dev.WooNet.IWooService;
using Dev.WooNet.Model.Models;
using Dev.WooNet.WebCore.Utility;
using Microsoft.AspNetCore.Mvc;
using NF.Common.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace Dev.WooNet.WebAPI.Areas.DevCommon.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataDicController : ControllerBase
    {
        private IDevDatadicService _IDevDatadicService;
        public DataDicController(IDevDatadicService iDevDatadicService)
        {
            _IDevDatadicService = iDevDatadicService;
        }
        // GET: api/<DataDicController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<DataDicController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<DataDicController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<DataDicController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<DataDicController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="pgInfo">分页对象</param>
        /// <returns></returns>
        [Route("list")]
        [HttpPost]
        public IActionResult GetListDicData([FromBody] PgRequestInfo pgInfo)
        {
            var pageInfo = new PageInfo<DevDatadic>(pageIndex: pgInfo.page, pageSize: pgInfo.limit);
            var prdAnd = PredBuilder.True<DevDatadic>();
            var prdOr = PredBuilder.False<DevDatadic>();
            if (!string.IsNullOrWhiteSpace(pgInfo.kword))
            {
                prdOr = prdOr.Or(a => a.Name.Contains(pgInfo.kword));
                prdAnd = prdAnd.And(prdOr);
            }
            var pagelist = _IDevDatadicService.GetList(pageInfo, prdAnd, a => a.Id, false);
            return new DevResultJson(pagelist);

        }

    }
}
