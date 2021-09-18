using Dev.WooNet.Common.Models;
using Dev.WooNet.Common.Utility;
using Dev.WooNet.Model;
using Dev.WooNet.Model.DevDTO;
using Dev.WooNet.Model.Enums;
using Dev.WooNet.Model.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Dev.WooNet.WooService
{

    /// <summary>
    /// 合同对方
    /// </summary>
    public partial  class DevCompanyService
    {
        #region 查询


        /// <summary>
        /// 列表
        /// </summary>
        /// <typeparam name="s"></typeparam>
        /// <param name="pageInfo"></param>
        /// <param name="whereLambda"></param>
        /// <param name="orderbyLambda"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        public AjaxListResult<DevCompanyDTO> GetList<s>(PageInfo<DevCompany> pageInfo, Expression<Func<DevCompany, bool>> whereLambda,
             Expression<Func<DevCompany, s>> orderbyLambda, bool isAsc)
        {
            var tempquery = this.DevDb.Set<DevCompany>().AsTracking().Where<DevCompany>(whereLambda);
            pageInfo.TotalCount = tempquery.Count();
            if (isAsc)
            {
                tempquery = tempquery.OrderBy(orderbyLambda);
            }
            else
            {
                tempquery = tempquery.OrderByDescending(orderbyLambda);
            }
            if (!(pageInfo is NoPageInfo<DevCompany>))
            { //分页
                tempquery = tempquery.Skip<DevCompany>((pageInfo.PageIndex - 1) * pageInfo.PageSize).Take<DevCompany>(pageInfo.PageSize);
            }


            var query = from a in tempquery
                        select new
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Code = a.Code,
                            Dtype=a.Dtype,
                            LevelId=a.LevelId,//级别
                            CareditId=a.CareditId,//信用等级
                            CompClassId=a.CompClassId,//公司类别
                            CountryId=a.CountryId,//国家
                            ProvinceId=a.ProvinceId,//省
                            CityId=a.CityId,//省份
                            Trade=a.Trade,//行业
                            Telephone = a.Telephone,//电话
                            PostCode=a.PostCode,//邮编
                            Cfax = a.Cfax,//传值
                            RegCapital=a.RegCapital,//注册资金
                            RegAddress=a.RegAddress,//注册地址
                            EsDateTime=a.EsDateTime,//成立日期
                            BusTerm=a.BusTerm,//营业期限
                            ExpDateTime=a.ExpDateTime,//营业执照日期
                            InvTitle = a.InvTitle,//发票标题
                            DutyNo=a.DutyNo,//纳税人识别号
                            InvAddress=a.InvAddress,//发票地址
                            InvTel=a.InvTel,//发票电话
                            BankName=a.BankName,//银行
                            Account=a.Account,//账号
                            PaidCapital=a.PaidCapital,//实收
                            LegalPerson=a.LegalPerson,//法人
                            WebUrl = a.WebUrl,//网站地址
                            Dstatus = a.Dstatus,//状态
                            Wstatus=a.Wstatus,//流程状态
                            FlowTo=a.FlowTo,//审批事项
                            WnodeName=a.WnodeName,//流程节点名称
                            AddUserId=a.AddUserId,//创建人
                            AddDateTime=a.AddDateTime,//创建时间
                            FaceUserId=a.FaceUserId,//负责人
                            BusScope=a.BusScope,//经营范围
                            Reserve1 = a.Reserve1,
                            Reserve2=a.Reserve2,
                            Caddress=a.Caddress,




                        };
            var local = from a in query.AsEnumerable()
                        select new DevCompanyDTO
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Code = a.Code,
                            Dtype = a.Dtype,
                            LevelId = a.LevelId,//级别
                            CareditId = a.CareditId,//信用等级
                            CompClassId = a.CompClassId,//公司类别
                            CountryId = a.CountryId,//国家
                            ProvinceId = a.ProvinceId,//省
                            CityId = a.CityId,//省份
                            Trade = a.Trade,//行业
                            Telephone = a.Telephone,//电话
                            PostCode = a.PostCode,//邮编
                            Cfax = a.Cfax,//传值
                            RegCapital = a.RegCapital,//注册资金
                            RegAddress = a.RegAddress,//注册地址
                            EsDateTime = a.EsDateTime,//成立日期
                            BusTerm = a.BusTerm,//营业期限
                            ExpDateTime = a.ExpDateTime,//营业执照日期
                            InvTitle = a.InvTitle,//发票标题
                            DutyNo = a.DutyNo,//纳税人识别号
                            InvAddress = a.InvAddress,//发票地址
                            InvTel = a.InvTel,//发票电话
                            BankName = a.BankName,//银行
                            Account = a.Account,//账号
                            PaidCapital = a.PaidCapital,//实收
                            LegalPerson = a.LegalPerson,//法人
                            WebUrl = a.WebUrl,//网站地址
                            Dstatus = a.Dstatus,//状态
                            Wstatus = a.Wstatus,//流程状态
                            FlowTo = a.FlowTo,//审批事项
                           
                            AddUserId = a.AddUserId,//创建人
                            AddDateTime = a.AddDateTime,//创建时间
                            FaceUserId = a.FaceUserId,//负责人
                            BusScope = a.BusScope,//经营范围
                            Reserve1 = a.Reserve1,
                            Reserve2 = a.Reserve2,
                            Caddress = a.Caddress,
                            AddUserName = RedisDevCommUtility.GetUserName(a.AddUserId ?? 0),
                            StateDic= EmunUtility.GetDesc(typeof(CompanyStateEnum),a.Dstatus??-1),
                            GjName= RedisDevCommUtility.GetCountryName(a.CountryId??0),
                            PrName = RedisDevCommUtility.GetProvinceName(a.ProvinceId ?? 0),
                            CityName = RedisDevCommUtility.GetCityName(a.CityId ?? 0),
                            CompClassName = RedisDevCommUtility.GetHashDataDic(a.CompClassId ?? 0),
                            LevelName = RedisDevCommUtility.GetHashDataDic(a.LevelId ?? 0),
                            CareditName = RedisDevCommUtility.GetHashDataDic(a.CareditId ?? 0),
                            FaceUserName= RedisDevCommUtility.GetUserName(a.FaceUserId ?? 0),
                            WnodeName = a.WnodeName,//流程节点名称
                            FlowItemDic = FlowUtility.GetMessionDic(a.FlowTo ?? -1, 0),
                            WstatusDesc = EmunUtility.GetDesc(typeof(WfStateEnum), a.Wstatus ?? -1),



                        };
            return new AjaxListResult<DevCompanyDTO>()
            {
                data = local.ToList(),
                count = pageInfo.TotalCount,
                code = 0


            };
        }
        /// <summary>
        /// 根据Id 信息
        /// </summary>
        /// <returns>返回基本信息</returns>
        public DevCompanyDTO GetInfoById(int Id)
        {
            var query = from a in this.DevDb.Set<DevCompany>().AsTracking()
                        where a.Id == Id
                        select new
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Code = a.Code,
                            Dtype = a.Dtype,
                            LevelId = a.LevelId,//级别
                            CareditId = a.CareditId,//信用等级
                            CompClassId = a.CompClassId,//公司类别
                            CountryId = a.CountryId,//国家
                            ProvinceId = a.ProvinceId,//省
                            CityId = a.CityId,//省份
                            Trade = a.Trade,//行业
                            Telephone = a.Telephone,//电话
                            PostCode = a.PostCode,//邮编
                            Cfax = a.Cfax,//传值
                            RegCapital = a.RegCapital,//注册资金
                            RegAddress = a.RegAddress,//注册地址
                            EsDateTime = a.EsDateTime,//成立日期
                            BusTerm = a.BusTerm,//营业期限
                            ExpDateTime = a.ExpDateTime,//营业执照日期
                            InvTitle = a.InvTitle,//发票标题
                            DutyNo = a.DutyNo,//纳税人识别号
                            InvAddress = a.InvAddress,//发票地址
                            InvTel = a.InvTel,//发票电话
                            BankName = a.BankName,//银行
                            Account = a.Account,//账号
                            PaidCapital = a.PaidCapital,//实收
                            LegalPerson = a.LegalPerson,//法人
                            WebUrl = a.WebUrl,//网站地址
                            Dstatus = a.Dstatus,//状态
                            Wstatus = a.Wstatus,//流程状态
                            FlowTo = a.FlowTo,//审批事项
                            WnodeName = a.WnodeName,//流程节点名称
                            AddUserId = a.AddUserId,//创建人
                            AddDateTime = a.AddDateTime,//创建时间
                            FaceUserId = a.FaceUserId,//负责人
                            BusScope = a.BusScope,//经营范围
                            Reserve1 = a.Reserve1,
                            Reserve2 = a.Reserve2,
                            Caddress = a.Caddress,
                           
                          
                          


                        };
            var local = from a in query.AsEnumerable()
                        select new DevCompanyDTO
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Code = a.Code,
                            Dtype = a.Dtype,
                            LevelId = a.LevelId,//级别
                            CareditId = a.CareditId,//信用等级
                            CompClassId = a.CompClassId,//公司类别
                            CountryId = a.CountryId,//国家
                            ProvinceId = a.ProvinceId,//省
                            CityId = a.CityId,//省份
                            Trade = a.Trade,//行业
                            Telephone = a.Telephone,//电话
                            PostCode = a.PostCode,//邮编
                            Cfax = a.Cfax,//传值
                            RegCapital = a.RegCapital,//注册资金
                            RegAddress = a.RegAddress,//注册地址
                            EsDateTime = a.EsDateTime,//成立日期
                            BusTerm = a.BusTerm,//营业期限
                            ExpDateTime = a.ExpDateTime,//营业执照日期
                            InvTitle = a.InvTitle,//发票标题
                            DutyNo = a.DutyNo,//纳税人识别号
                            InvAddress = a.InvAddress,//发票地址
                            InvTel = a.InvTel,//发票电话
                            BankName = a.BankName,//银行
                            Account = a.Account,//账号
                            PaidCapital = a.PaidCapital,//实收
                            LegalPerson = a.LegalPerson,//法人
                            WebUrl = a.WebUrl,//网站地址
                            Dstatus = a.Dstatus,//状态
                            Wstatus = a.Wstatus,//流程状态
                            FlowTo = a.FlowTo,//审批事项
                            WnodeName = a.WnodeName,//流程节点名称
                            AddUserId = a.AddUserId,//创建人
                            AddDateTime = a.AddDateTime,//创建时间
                            FaceUserId = a.FaceUserId,//负责人
                            BusScope = a.BusScope,//经营范围
                            Reserve1 = a.Reserve1,
                            Reserve2 = a.Reserve2,
                            Caddress = a.Caddress,
                            AddUserName = RedisDevCommUtility.GetUserName(a.AddUserId ?? 0),
                            StateDic = EmunUtility.GetDesc(typeof(CompanyStateEnum), a.Dstatus ?? -1),
                            GjName = RedisDevCommUtility.GetCountryName(a.CountryId ?? 0),
                            PrName = RedisDevCommUtility.GetProvinceName(a.ProvinceId ?? 0),
                            CityName = RedisDevCommUtility.GetCityName(a.CityId ?? 0),
                            CompClassName= RedisDevCommUtility.GetHashDataDic(a.CompClassId??0),
                            LevelName = RedisDevCommUtility.GetHashDataDic(a.LevelId ?? 0),
                            CareditName = RedisDevCommUtility.GetHashDataDic(a.CareditId ?? 0),
                            FaceUserName = RedisDevCommUtility.GetUserName(a.FaceUserId ?? 0),

                        };
            return local.FirstOrDefault();
        }


        #endregion

        #region 修改数据库
        /// <summary>
        /// 保存客户信息
        /// </summary>
        /// <returns></returns>
        public DevCompany SaveCompany(DevCompany info)
        {
            DevCompany resul = null;
            if (info.Id > 0)
            {//修改
                Update(info);
                resul = info;
            }
            else
            {
                resul = Add(info);
            }
            return resul;
        }

        /// <summary>
        /// 清除标签垃圾数据
        /// </summary>
        /// <param name="currUserId">当前用户ID</param>
        /// <returns></returns>
        public int ClearItemData(int currUserId)
        {
            StringBuilder strsql = new StringBuilder();
            strsql.Append($"delete from  dev_compcontact  where CompId={-currUserId};");
            strsql.Append($"delete from  dev_compdesc  where CompId={-currUserId};");
            strsql.Append($"delete from  dev_compfile  where CompId={-currUserId};");
            //添加其他标签表
            return ExecuteSqlCommand(strsql.ToString());
        }
        /// <summary>
        /// 修改当前对应标签下的-UserId数据
        /// </summary>
        /// <param name="Id">当前ID</param>
        /// <param name="currUserId">当前用户ID</param>
        public int UpdateItems(int Id, int currUserId)
        {
            StringBuilder strsql = new StringBuilder();
            strsql.Append($"update dev_compcontact set CompId={Id} where CompId={-currUserId};");
            strsql.Append($"update dev_compdesc set CompId={Id} where CompId={-currUserId};");
            strsql.Append($"update dev_compfile set CompId={Id} where CompId={-currUserId};");
            //添加其他标签表
            return ExecuteSqlCommand(strsql.ToString());

        }

        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="Ids">删除Ids</param>
        /// <returns></returns>
        public AjaxResult DelCompany(string Ids)
        {
            var result = new AjaxResult()
            {
                msg = "success",
                code = (int)MessageEnums.success,


            };
            var listIds = StringHelper.String2ArrayInt(Ids);
            var listcomps = DevDb.Set<DevCompany>().Where(a => listIds.Contains(a.Id)).ToList();
            IList<int> delIds = new List<int>();
            var nodel = false;
            foreach (var item in listcomps)
            {
                if (item.Dstatus==0)
                {
                    delIds.Add(item.Id);
                }
                else
                {
                    nodel = true;
                }
            }
            StringBuilder sqlstr = new StringBuilder();
            if (delIds.Count() > 0)
            {
                var tempids = StringHelper.ArrayInt2String(delIds);
                sqlstr.Append($"update dev_compcontact set IsDelete=1  where CompId in({tempids});");
                sqlstr.Append($"update dev_compdesc set IsDelete=1  where CompId in({tempids});");
                sqlstr.Append($"update dev_compfile set IsDelete=1  where CompId in({tempids});");
                sqlstr.Append($"update dev_company set IsDelete=1 where Id in({tempids});");
                ExecuteSqlCommand(sqlstr.ToString()); ;

            }
            if (nodel)
            {//存在状态不能删除的
                result.code = (int)MessageEnums.deletestate;
                result.msg = EmunUtility.GetDesc(typeof(MessageEnums), result.code);
            }

            return result;

        }

        /// <summary>
        /// 修改字段
        /// </summary>
        /// <param name="info">修改的字段对象</param>
        /// <returns>返回受影响行数</returns>
        public int UpdateField(UpdateFieldInfo info)
        {
            string sqlstr = "";
            switch (info.Field)
            {

                case "InvoiceTitle"://发票抬头
                case "InvAddress":
                case "BankName":
                case "Account":
               
                    sqlstr = $"update  dev_company set {info.Field}='{info.FieldVal}' where Id={info.Id}";
                    break;
                //case "PrincipalUserDisplayName"://负责人
                //    sqlstr = $"update  Company set PrincipalUserId={info.FieldValue} where Id={info.Id}";
                //    break;
                //case "Cstate"://状态
                //    var state = Convert.ToByte(info.FieldValue);
                //    sqlstr = $"update  Company set Cstate={state} where Id={info.Id}";
                //    break;

                default:
                    break;
            }
            if (!string.IsNullOrEmpty(sqlstr))
                return ExecuteSqlCommand(sqlstr);
            return 0;

        }


        #endregion

    }
}
