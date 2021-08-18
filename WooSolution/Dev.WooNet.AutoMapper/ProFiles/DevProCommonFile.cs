using AutoMapper;
using Dev.WooNet.Model;
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


            #region 公共区域
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
            CreateMap<DevRoleDTO, DevRole>();
            //系统模块
            CreateMap<DevSysmodelDTO, DevSysmodel>();

            #endregion 公共区域

            #region 流程相关
            //审批组
            CreateMap<DevFlowGroupDTO, DevFlowGroup>();
            //流程模板
            CreateMap<DevFlowTempDTO, DevFlowTemp>()
           .ForMember(a => a.IsDelete, opt => opt.MapFrom(src => 0))
           .ForMember(a => a.AddDateTime, opt => opt.MapFrom(src => DateTime.Now))
           .ForMember(a => a.AddUserId, opt => opt.Ignore())
           .ForMember(a => a.AddDateTime, opt => opt.Ignore());
            //流程模板
            CreateMap<DevFlowTempDTO, DevFlowTempHist>()
           .ForMember(a => a.IsDelete, opt => opt.MapFrom(src => 0))
           .ForMember(a => a.AddDateTime, opt => opt.MapFrom(src => DateTime.Now))
           .ForMember(a => a.AddUserId, opt => opt.Ignore())
           .ForMember(a => a.AddDateTime, opt => opt.Ignore())
            .ForMember(a => a.TempId, opt => opt.Ignore())
             .ForMember(a => a.Id, opt => opt.Ignore())
           ;
            //流程模板->模板历史
            CreateMap<DevFlowTemp, DevFlowTempHist>()
           .ForMember(a => a.Id, opt => opt.Ignore())
           .ForMember(a => a.TempId, opt => opt.Ignore());

            //模板显示节点-模板数据节点
            CreateMap<FlowTempNodeViewDTO, DevFlowTempNode>()
            .ForMember(d => d.Name, opt =>
            {
                opt.MapFrom(s => s.name);
            }).ForMember(d => d.StrId, opt =>
            {
                opt.MapFrom(s => s.strid);
            }).ForMember(d => d.Height, opt =>
            {
                opt.MapFrom(s => s.height);
            }).ForMember(d => d.Width, opt =>
            {
                opt.MapFrom(s => s.width);
            })
           .ForMember(d => d.Left, opt =>
           {
               opt.MapFrom(s => s.left);
           }).ForMember(d => d.Top, opt =>
           {
               opt.MapFrom(s => s.top);
           })
            .ForMember(a => a.Type, opt => opt.Ignore())
             .ForMember(a => a.Alt, opt => opt.Ignore())
             .ForMember(a => a.Marked, opt => opt.Ignore())
             .ForMember(a => a.TempId, opt => opt.Ignore())
             .ForMember(a => a.Id, opt => opt.Ignore());
            //节点显示线-节点线实体
            CreateMap<TempNodeLineViewDTO, DevTempNodeLine>()
            .ForMember(d => d.Name, opt =>
            {
                opt.MapFrom(s => s.name);
            }).ForMember(d => d.StrId, opt =>
            {
                opt.MapFrom(s => s.strid);
            }).ForMember(d => d.From, opt =>
            {
                opt.MapFrom(s => s.from);
            }).ForMember(d => d.To, opt =>
            {
                opt.MapFrom(s => s.to);
            })
            .ForMember(a => a.Type, opt => opt.Ignore())
             .ForMember(a => a.Alt, opt => opt.Ignore())
             .ForMember(a => a.Marked, opt => opt.Ignore())
             .ForMember(a => a.TempId, opt => opt.Ignore())
             .ForMember(a => a.Id, opt => opt.Ignore())
             .ForMember(a => a.Dash, opt => opt.Ignore());

            //模板显示区域-区域数据节点
            CreateMap<TempNodeAreaViewDTO, DevTempNodeArea>()
            .ForMember(d => d.Name, opt =>
            {
                opt.MapFrom(s => s.name);
            }).ForMember(d => d.StrId, opt =>
            {
                opt.MapFrom(s => s.strid);
            }).ForMember(d => d.Height, opt =>
            {
                opt.MapFrom(s => s.height);
            }).ForMember(d => d.Width, opt =>
            {
                opt.MapFrom(s => s.width);
            })
            .ForMember(d => d.Left, opt =>
            {
                opt.MapFrom(s => s.left);
            }).ForMember(d => d.Top, opt =>
            {
                opt.MapFrom(s => s.top);
            })

             .ForMember(a => a.Alt, opt => opt.Ignore())
            .ForMember(a => a.Color, opt => opt.Ignore())
             .ForMember(a => a.TempId, opt => opt.Ignore())
             .ForMember(a => a.Id, opt => opt.Ignore());

            //流程模板节点->模板节点历史
            CreateMap<DevFlowTempNode, DevFlowTempNodeHist>()
            .ForMember(a => a.Id, opt => opt.Ignore())
             .ForMember(a => a.TempHistId, opt => opt.Ignore());
            //流程模板节点连线->模板节点连线历史
            CreateMap<DevTempNodeLine, DevTempNodeLineHist>()
            .ForMember(a => a.Id, opt => opt.Ignore())
             .ForMember(a => a.TempHistId, opt => opt.Ignore());
            //流程模板节点区域->模板节点区域历史
            CreateMap<DevTempNodeArea, DevTempNodeAreaHist>()
            .ForMember(a => a.Id, opt => opt.Ignore())
             .ForMember(a => a.TempHistId, opt => opt.Ignore());

            //模板显示节点-模板数据节点
            CreateMap<DevFlowTempNode, FlowTempNodeViewDTO>()
            .ForMember(d => d.name, opt =>
            {
                opt.MapFrom(s => s.Name);
            }).ForMember(d => d.strid, opt =>
            {
                opt.MapFrom(s => s.StrId);
            }).ForMember(d => d.height, opt =>
            {
                opt.MapFrom(s => s.Height);
            }).ForMember(d => d.width, opt =>
            {
                opt.MapFrom(s => s.Width);
            })
           .ForMember(d => d.left, opt =>
           {
               opt.MapFrom(s => s.Left);
           }).ForMember(d => d.top, opt =>
           {
               opt.MapFrom(s => s.Top);
           })
            .ForMember(a => a.type, opt => opt.Ignore())
             .ForMember(a => a.alt, opt => opt.Ignore())
             .ForMember(a => a.marked, opt => opt.Ignore());

            //区域数据节点->模板显示区域
            CreateMap<DevTempNodeArea, TempNodeAreaViewDTO>()
            .ForMember(d => d.name, opt =>
            {
                opt.MapFrom(s => s.Name);
            }).ForMember(d => d.strid, opt =>
            {
                opt.MapFrom(s => s.StrId);
            }).ForMember(d => d.height, opt =>
            {
                opt.MapFrom(s => s.Height);
            }).ForMember(d => d.width, opt =>
            {
                opt.MapFrom(s => s.Width);
            })
            .ForMember(d => d.left, opt =>
            {
                opt.MapFrom(s => s.Left);
            }).ForMember(d => d.top, opt =>
            {
                opt.MapFrom(s => s.Top);
            })

             .ForMember(a => a.alt, opt => opt.Ignore())
            .ForMember(a => a.color, opt => opt.Ignore());

            //节点线实体->节点显示线
            CreateMap<DevTempNodeLine, TempNodeLineViewDTO>()
            .ForMember(d => d.name, opt =>
            {
                opt.MapFrom(s => s.Name);
            }).ForMember(d => d.strid, opt =>
            {
                opt.MapFrom(s => s.StrId);
            }).ForMember(d => d.from, opt =>
            {
                opt.MapFrom(s => s.From);
            }).ForMember(d => d.to, opt =>
            {
                opt.MapFrom(s => s.To);
            })
            .ForMember(a => a.type, opt => opt.Ignore())
             .ForMember(a => a.alt, opt => opt.Ignore())
             .ForMember(a => a.marked, opt => opt.Ignore())
             .ForMember(a => a.dash, opt => opt.Ignore());

            //流程模板节点信息-模板节点信息历史
            CreateMap<DevFlowTempNodeInfo, DevFlowTempNodeInfoHist>()
                .ForMember(a => a.Id, opt => opt.Ignore())
                .ForMember(a => a.TempHistId, opt => opt.Ignore());

            //节点信息DTO->节点信息
            CreateMap<FlowTempNodeInfoDTO, DevFlowTempNodeInfo>()
               .ForMember(a => a.Id, opt => opt.Ignore());

            #endregion 流程相关



        }

    }
}
