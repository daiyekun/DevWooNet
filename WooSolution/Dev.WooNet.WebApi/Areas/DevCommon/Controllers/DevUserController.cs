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

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Dev.WooNet.WebAPI.Areas.DevCommon.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevUserController : ControllerBase
    {
        private IDevUserinfoService _IDevUserinfoService;
        public DevUserController(IDevUserinfoService DevUserinfoService)
        {
            _IDevUserinfoService = DevUserinfoService;
        }
        // GET: api/<DevUserController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<DevUserController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<DevUserController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<DevUserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<DevUserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        [Route("list")]
        [HttpPost]
        public IActionResult  GetListUser([FromBody] PgRequestInfo pgInfo)
        {
            var pageInfo = new PageInfo<DevUserinfo>(pageIndex: pgInfo.page, pageSize: pgInfo.limit);
            var prdAnd = PredBuilder.True<DevUserinfo>();
            var prdOr = PredBuilder.False<DevUserinfo>();
            if (!string.IsNullOrWhiteSpace(pgInfo.kword))
            {
                prdOr = prdOr.Or(a => a.Name.Contains(pgInfo.kword));
                prdOr = prdOr.Or(a => a.ShowName.Contains(pgInfo.kword));
                prdOr = prdOr.Or(a => a.IdNo.Contains(pgInfo.kword));
                prdAnd = prdAnd.And(prdOr);
            }
            var pagelist = _IDevUserinfoService.GetList(pageInfo, prdAnd, a => a.Id, false);
            return new DevResultJson(pagelist);
           
        }
    }
}
