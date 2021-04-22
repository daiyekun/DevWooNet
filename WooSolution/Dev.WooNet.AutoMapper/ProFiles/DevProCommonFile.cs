using AutoMapper;
using Dev.WooNet.Model.DevDTO;
using Dev.WooNet.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dev.WooNet.AutoMapper.ProFiles
{

    /// <summary>
    /// 公共映射文件
    /// </summary>
    public class DevProCommonFile : Profile, IProfile
    {
        public DevProCommonFile()
        {
            //机构
            CreateMap<DepartData, DevDepartment>();
            //签约主体
            CreateMap<DepartData, DevDeptmain>();
            //用户
            CreateMap<DevUserinfoDTO, DevUserinfo>()
            .ForMember(a=>a.IsDelete, opt => opt.MapFrom(src => 0))
            .ForMember(a => a.ModifyDatetime, opt => opt.MapFrom(src => DateTime.Now))
             .ForMember(a => a.Ustate, opt => opt.MapFrom(src => 0))
              .ForMember(a => a.Mstart, opt => opt.MapFrom(src => 0))
            ;
            //.ForMember(a => a.ContId, opt => opt.Ignore())
            //.ForMember(a => a.CreateUserId, opt => opt.Ignore())
            //.ForMember(a => a.CreateDateTime, opt => opt.Ignore())
            //.ForMember(a => a.ModifyUserId, opt => opt.Ignore())
            //.ForMember(a => a.ModifyDateTime, opt => opt.Ignore());
        }
       
    }
}
