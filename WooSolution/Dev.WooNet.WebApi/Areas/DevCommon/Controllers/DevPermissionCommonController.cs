using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


/// <summary>
/// 模块一些权限操作
/// </summary>
namespace Dev.WooNet.WebAPI.Areas.DevCommon.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevPermissionCommonController : ControllerBase
    {
        // GET: api/<DevPermissionCommonController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<DevPermissionCommonController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<DevPermissionCommonController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<DevPermissionCommonController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<DevPermissionCommonController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
