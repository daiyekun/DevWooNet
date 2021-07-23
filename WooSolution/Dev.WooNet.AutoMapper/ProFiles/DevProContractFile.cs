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
    /// 合同主业务映射文件
    /// </summary>
    public class DevProContractFile : Profile, IProfile
    {
        public DevProContractFile()
        {
            #region 合同对方相关
            //合同对方
            CreateMap<DevCompanyDTO, DevCompany>()
           .ForMember(a => a.IsDelete, opt => opt.MapFrom(src => 0))
           .ForMember(a => a.UpdateDateTime, opt => opt.MapFrom(src => DateTime.Now))
           .ForMember(a => a.Dstatus, opt => opt.Ignore())
           .ForMember(a => a.AddUserId, opt => opt.Ignore())
           .ForMember(a => a.AddDateTime, opt => opt.Ignore())
           .ForMember(a => a.UpdateUserId, opt => opt.MapFrom(src => 0));
            //合同对方连接
           CreateMap<DevCompcontactDTO, DevCompcontact>()
          .ForMember(a => a.IsDelete, opt => opt.MapFrom(src => 0))
          .ForMember(a => a.UpdateDateTime, opt => opt.MapFrom(src => DateTime.Now))
          .ForMember(a => a.AddUserId,  opt =>opt.Ignore())
          .ForMember(a => a.AddDateTime, opt => opt.Ignore())
          .ForMember(a => a.UpdateUserId, opt => opt.Ignore());

            
            #endregion


        }
    }
}
