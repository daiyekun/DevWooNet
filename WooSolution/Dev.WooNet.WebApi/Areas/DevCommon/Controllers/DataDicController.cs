using Dev.WooNet.Common.Models;
using Dev.WooNet.Common.Utility;
using Dev.WooNet.IWooService;
using Dev.WooNet.Model.DevDTO;
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

    /// <summary>
    /// 数据字典
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DataDicController : ControllerBase
    {
        private IDevDatadicService _IDevDatadicService;
        public DataDicController(IDevDatadicService iDevDatadicService)
        {
            _IDevDatadicService = iDevDatadicService;
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
            //枚举类型
            prdAnd = prdAnd.And(a => a.TypeInt == pgInfo.otherId);
            if (!string.IsNullOrWhiteSpace(pgInfo.kword))
            {
                prdOr = prdOr.Or(a => a.Name.Contains(pgInfo.kword));
                prdAnd = prdAnd.And(prdOr);
            }
            
            var pagelist = _IDevDatadicService.GetList(pageInfo, prdAnd, a => a.Id, false);
            return new DevResultJson(pagelist);

        }
        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="pgInfo">分页对象</param>
        /// <returns></returns>
        [Route("GetDataByType")]
        [HttpGet]
        public IActionResult GetDataByType(int typeint)
        {
            var listall = _IDevDatadicService.GetAll();
            var currdata = listall.Where(a => a.TypeInt == typeint).ToList();

            return new DevResultJson(new AjaxListResult<DevDatadicDTO>()
            {
                data = currdata
            }); ;

        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="datadicDTO">新增对象</param>
        [Route("AddDic")]
        [HttpGet]
        public IActionResult AddDataDic(int TypeInt)
        {
            var info = new DevDatadic();
            info.Name = "新增类别";
            info.TypeInt = TypeInt;
            _IDevDatadicService.Add(info);
            _IDevDatadicService.SetRedisHash();
            RedisUtility.KeyDeleteAsync($"{RedisKeyData.RedisBaseRoot}:{RedisKeyData.DataDicList}");
             var result = new AjaxResult
            {
                code = 0,
                msg = "",
            };
            return new DevResultJson(result);
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="datadicDTO">新增对象</param>
        [Route("UpdateFiled")]
        [HttpPost]
        public IActionResult UpdateFiled(UpdateFieldInfo updateField)
        {
            string sqlstr ="";
            switch (updateField.Field)
            {
               
                default://字符串字段修改,特殊类型加case
                    sqlstr = $"update  dev_datadic set {updateField.Field}='{updateField.FieldVal}',ModifyDatetime='{DateTime.Now}' where Id={updateField.Id}";
                    break;

            }
            if (!string.IsNullOrEmpty(sqlstr))
            {
                _IDevDatadicService.ExecuteSqlCommand(sqlstr);
                _IDevDatadicService.SetRedisHash();
                RedisUtility.KeyDeleteAsync($"{RedisKeyData.RedisBaseRoot}:{RedisKeyData.DataDicList}");

            }
           
            var result = new AjaxResult
            {
                code = 0,
                msg = "",
            };
            return new DevResultJson(result);
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="Ids">选中ID</param>
        [Route("DeleteDic")]
        [HttpGet]
        public IActionResult DeleteDic(string Ids)
        {
            string sqlstr = $"delete from dev_datadic where Id in({Ids})";
            _IDevDatadicService.ExecuteSqlCommand(sqlstr);
            _IDevDatadicService.SetRedisHash();
            RedisUtility.KeyDeleteAsync($"{RedisKeyData.RedisBaseRoot}:{RedisKeyData.DataDicList}");
            var result = new AjaxResult
            {
                code = 0,
                msg = "",
            };
            return new DevResultJson(result);
        }


    }
}
