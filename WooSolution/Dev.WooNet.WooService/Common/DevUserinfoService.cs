using Dev.WooNet.Common.Models;
using Dev.WooNet.Common.Utility;
using Dev.WooNet.IWooService;
using Dev.WooNet.Model.DevDTO;
using Dev.WooNet.Model.Enums;
using Dev.WooNet.Model.ExtendModel;
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
    /// 用户实现
    /// </summary>
    public partial class DevUserinfoService: BaseService<DevUserinfo>, IDevUserinfoService
    {
       
        /// <summary>
        /// 用户列表
        /// </summary>
        /// <typeparam name="s"></typeparam>
        /// <param name="pageInfo"></param>
        /// <param name="whereLambda"></param>
        /// <param name="orderbyLambda"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        public AjaxListResult<DevUserinfoDTO> GetList<s>(PageInfo<DevUserinfo> pageInfo, Expression<Func<DevUserinfo, bool>> whereLambda,
             Expression<Func<DevUserinfo, s>> orderbyLambda, bool isAsc)
         {
            var tempquery = this.DevDb.Set<DevUserinfo>().AsTracking().Where<DevUserinfo>(whereLambda);
            pageInfo.TotalCount = tempquery.Count();
            if (isAsc)
            {
                tempquery = tempquery.OrderBy(orderbyLambda);
            }
            else
            {
                tempquery = tempquery.OrderByDescending(orderbyLambda);
            }
            if (!(pageInfo is NoPageInfo<DevUserinfo>))
            { //分页
                tempquery = tempquery.Skip<DevUserinfo>((pageInfo.PageIndex - 1) * pageInfo.PageSize).Take<DevUserinfo>(pageInfo.PageSize);
            }
          
        
        var query = from a in tempquery
                        select new
                        {
                            Id = a.Id,
                            Name = a.Name,//登录名称
                            ShowName = a.ShowName,//显示名称
                            Sex=a.Sex,//性别
                            Age=a.Age,//年龄
                            Tel=a.Tel,//电话
                            Mobile=a.Mobile,//移动电话
                            Email=a.Email,//邮件
                            EntryDatetime=a.EntryDatetime,//出生日期
                            IdNo=a.IdNo,//身份证号
                            DepId=a.DepId,//部门ID
                            Ustate=a.Ustate,//状态
                            Mstart=a.Mstart,
                            WxCode=a.WxCode,//微信账号
                            CreateDatetime = a.CreateDatetime,
                            CreateUserId = a.CreateUserId,
                       

                        };
            var local = from a in query.AsEnumerable()
                        select new DevUserinfoDTO
                        {
                            Id = a.Id,
                            Name = a.Name,//登录名称
                            ShowName = a.ShowName,//显示名称
                            Sex = a.Sex,//性别
                            SexDic= EmunUtility.GetDesc(typeof(UserSexEnum), a.Sex??2),
                            Age = a.Age,//年龄
                            Tel = a.Tel,//电话
                            Mobile = a.Mobile,//移动电话
                            Email = a.Email,//邮件
                            EntryDatetime = a.EntryDatetime,//出生日期
                            IdNo = a.IdNo,//身份证号
                            DepId = a.DepId,//部门ID
                            Ustate = a.Ustate,//状态
                            StateDic = EmunUtility.GetDesc(typeof(UserStateEnum), a.Ustate),
                            Mstart = a.Mstart,
                            WxCode = a.WxCode,//微信账号
                            CreateDatetime = a.CreateDatetime,
                            CreateUserId = a.CreateUserId,
                            DeptName = DevRedisUtility.GetDeptName(a.Id),

                        };
            return new AjaxListResult<DevUserinfoDTO>()
            {
                data = local.ToList(),
                count = pageInfo.TotalCount,
                code = 0


            };
        }
        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        public IList<DevUserinfoDTO> GetAll()
        {
            var query = from a in this.DevDb.Set<DevUserinfo>().AsTracking()
                        select new
                        {
                            Id = a.Id,
                            Name = a.Name,//登录名称
                            ShowName = a.ShowName,//显示名称
                            Sex = a.Sex,//性别
                            Age = a.Age,//年龄
                            Tel = a.Tel,//电话
                            Mobile = a.Mobile,//移动电话
                            Email = a.Email,//邮件
                            EntryDatetime = a.EntryDatetime,//出生日期
                            IdNo = a.IdNo,//身份证号
                            DepId = a.DepId,//部门ID
                            Ustate = a.Ustate,//状态
                            Mstart = a.Mstart,
                            WxCode = a.WxCode,//微信账号
                            CreateDatetime = a.CreateDatetime,
                            CreateUserId = a.CreateUserId,


                        };
            var local = from a in query.AsEnumerable()
                        select new DevUserinfoDTO
                        {
                            Id = a.Id,
                            Name = a.Name,//登录名称
                            ShowName = a.ShowName,//显示名称
                            Sex = a.Sex,//性别
                            SexDic = EmunUtility.GetDesc(typeof(UserSexEnum), a.Sex ?? 2),
                            Age = a.Age,//年龄
                            Tel = a.Tel,//电话
                            Mobile = a.Mobile,//移动电话
                            Email = a.Email,//邮件
                            EntryDatetime = a.EntryDatetime,//出生日期
                            IdNo = a.IdNo,//身份证号
                            DepId = a.DepId,//部门ID
                            Ustate = a.Ustate,//状态
                            StateDic = EmunUtility.GetDesc(typeof(UserStateEnum), a.Ustate),
                            Mstart = a.Mstart,
                            WxCode = a.WxCode,//微信账号
                            CreateDatetime = a.CreateDatetime,
                            CreateUserId = a.CreateUserId,
                            DeptName= DevRedisUtility.GetDeptName(a.Id),

                        };
            return local.ToList();
        }
        /// <summary>
        /// 设置Redis
        /// </summary>
        /// <param name="datadic">字典枚举</param>
        /// <returns></returns>
        public void SetRedisHash()
        {
            try
            {
                var curdickey = $"{RedisKeys.RedisUserKey}";
                var list = GetAll();
                foreach (var item in list)
                {
                    item.SetRedisHash<DevUserinfoDTO>($"{curdickey}", (a, c) =>
                    {
                        return $"{a}:{c}";
                    });
                }
            }
            catch (Exception ex)
            {
                Log4netHelper.Error(ex.Message);
                
            }


        }
        /// <summary>
        /// 保存用户信息
        /// </summary>
        /// <returns></returns>
        public DevUserinfo SaveUser(DevUserinfo userinfo)
        {
            DevUserinfo resul = null;
            userinfo.Pwd = EncryptUtility.PwdToMD5(userinfo.Pwd,userinfo.Name);
            if (userinfo.Id > 0)
            {//修改
                Update(userinfo);
                resul = userinfo;
            
            }
            else
            {
                userinfo.CreateDatetime = DateTime.Now;
                userinfo.CreateUserId = 1;
                userinfo.ModifyUserId = 1;

                resul = Add(userinfo);
            }
            SetRedisHash();
            return resul;



        }
        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="Ids">选择用户</param>
        /// <returns></returns>
        public int RestPwd(string Ids)
        {
           var listids= StringHelper.String2ArrayInt(Ids);
            var list = DevDb.Set<DevUserinfo>().Where(a => listids.Contains(a.Id)).ToList();
            IList<DevUserinfo> users = new List<DevUserinfo>();
            foreach (var item in list)
            {
               
                item.Pwd = EncryptUtility.PwdToMD5("1", item.Name);
               
                users.Add(item);
            }

            Update(users);
            return users.Count();




        }
            
        /// <summary>
        /// 删除用户信息
        /// </summary>
        /// <param name="Ids">需要删除的用户ID</param>
        /// <returns></returns>
        public int DelUser(string Ids)
        {
            string sqlstr = $"update dev_userinfo set IsDelete=1 where Id in({Ids})";
            var resl= ExecuteSqlCommand(sqlstr);

            SetRedisHash();
            return resl;

        }
        /// <summary>
        /// 根据Id 查询用户信息
        /// </summary>
        /// <returns>返回基本信息</returns>
        public DevUserinfoDTO GetUserById(int Id)
        {
            var query = from a in this.DevDb.Set<DevUserinfo>().AsTracking()
                        where a.Id==Id
                        select new
                        {
                            Id = a.Id,
                            Name = a.Name,//登录名称
                            ShowName = a.ShowName,//显示名称
                            Sex = a.Sex,//性别
                            Age = a.Age,//年龄
                            Tel = a.Tel,//电话
                            Mobile = a.Mobile,//移动电话
                            Email = a.Email,//邮件
                            EntryDatetime = a.EntryDatetime,//出生日期
                            IdNo = a.IdNo,//身份证号
                            DepId = a.DepId,//部门ID
                            Ustate = a.Ustate,//状态
                            Mstart = a.Mstart,
                            WxCode = a.WxCode,//微信账号
                            CreateDatetime = a.CreateDatetime,
                            CreateUserId = a.CreateUserId,


                        };
            var local = from a in query.AsEnumerable()
                        select new DevUserinfoDTO
                        {
                            Id = a.Id,
                            Name = a.Name,//登录名称
                            ShowName = a.ShowName,//显示名称
                            Sex = a.Sex,//性别
                            SexDic = EmunUtility.GetDesc(typeof(UserSexEnum), a.Sex ?? 2),
                            Age = a.Age,//年龄
                            Tel = a.Tel,//电话
                            Mobile = a.Mobile,//移动电话
                            Email = a.Email,//邮件
                            EntryDatetime = a.EntryDatetime,//出生日期
                            IdNo = a.IdNo,//身份证号
                            DepId = a.DepId,//部门ID
                            Ustate = a.Ustate,//状态
                            StateDic = EmunUtility.GetDesc(typeof(UserStateEnum), a.Ustate),
                            Mstart = a.Mstart,
                            WxCode = a.WxCode,//微信账号
                            CreateDatetime = a.CreateDatetime,
                            CreateUserId = a.CreateUserId,
                            DeptName = DevRedisUtility.GetDeptName(a.Id),

                        };
            return local.FirstOrDefault();
        }
        /// <summary>
        /// 修改状态
        /// </summary>
        /// <returns></returns>
        public int UpdateState(UpdateField updateField)
        {
            string sqlstr = "";
            switch (updateField.State)
            {
                case (int)UserStateEnum.LiZhi:
                case (int)UserStateEnum.QuYong:
                case (int)UserStateEnum.JinYong:
                    sqlstr = $"update dev_userinfo set Ustate={updateField.State},ModifyDatetime='{DateTime.Now}'  where Id={updateField.Id}";
                    break;
               
                  

            }

            if (!string.IsNullOrEmpty(sqlstr))
            {
              return  ExecuteSqlCommand(sqlstr);
            }
            return 0;

        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="LoginName"></param>
        /// <param name="Pwd"></param>
        /// <returns></returns>
        public LoginResult Login(string LoginName,string Pwd)
        {
            LoginResult loginResult = new LoginResult();
            var userinfo = DevDb.Set<DevUserinfo>().Where(a => a.Name == LoginName).FirstOrDefault();
            if (userinfo != null)
            {
                var encryptpwd = EncryptUtility.PwdToMD5(Pwd,LoginName);
                if(userinfo.Pwd== encryptpwd)
                {
                    loginResult.Reult= 0; //表示验证通过
                    var loginuser = new LoginUser();
                    loginuser.Id = userinfo.Id;
                    loginuser.Name = userinfo.Name;
                    loginuser.ShowName = userinfo.ShowName;
                    loginuser.DeptId = userinfo.DepId??0;
                    loginuser.DeptName = RedisUtility.HashGet($"{RedisKeys.RedisdeptKey}", "Name");
                    loginResult.LoginUser = loginuser;
                }
                else
                {
                    loginResult.Reult = 2;//密码验证错误
                }
            }
            else
            {
                loginResult.Reult = 1;//当前用户名称不存在
            }

            return loginResult;

        }



    }
}
