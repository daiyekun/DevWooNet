using Dev.WooNet.IWooService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using System.Linq;
using Dev.WooNet.Model.Models;
///****************************************************
///代码自动生成,需要修改builder里构造函数数据库连接字符串即可
///如果有个性业务在建立一个public partial interface 
///如有报错，添加引用NuGet  PetaPoco.NetCore (1.0.1)、T4 (2.0.1)
///****************************************************
namespace Dev.WooNet.WooService
{
 
 public partial class DevCurrencyManagerService : BaseService<DevCurrencyManager>, IDevCurrencyManagerService
    {
        public DevCurrencyManagerService(DevDbContext DevDb)
           : base(DevDb)
        {
           
        }
		
		public DevCurrencyManagerService(){}
    }
    
   
     
 public partial class DevDbContextService : BaseService<DevDbContext>, IDevDbContextService
    {
        public DevDbContextService(DevDbContext DevDb)
           : base(DevDb)
        {
           
        }
		
		public DevDbContextService(){}
    }
    
   
     
 public partial class DevDepartmentService : BaseService<DevDepartment>, IDevDepartmentService
    {
        public DevDepartmentService(DevDbContext DevDb)
           : base(DevDb)
        {
           
        }
		
		public DevDepartmentService(){}
    }
    
   
     
 public partial class DevDeptmainService : BaseService<DevDeptmain>, IDevDeptmainService
    {
        public DevDeptmainService(DevDbContext DevDb)
           : base(DevDb)
        {
           
        }
		
		public DevDeptmainService(){}
    }
    
   
     
 public partial class DevLoginLogService : BaseService<DevLoginLog>, IDevLoginLogService
    {
        public DevLoginLogService(DevDbContext DevDb)
           : base(DevDb)
        {
           
        }
		
		public DevLoginLogService(){}
    }
    
   
     
 public partial class DevOptionLogService : BaseService<DevOptionLog>, IDevOptionLogService
    {
        public DevOptionLogService(DevDbContext DevDb)
           : base(DevDb)
        {
           
        }
		
		public DevOptionLogService(){}
    }
    
   
     
 public partial class DevRemindService : BaseService<DevRemind>, IDevRemindService
    {
        public DevRemindService(DevDbContext DevDb)
           : base(DevDb)
        {
           
        }
		
		public DevRemindService(){}
    }
    
   
     
 public partial class DevRoleService : BaseService<DevRole>, IDevRoleService
    {
        public DevRoleService(DevDbContext DevDb)
           : base(DevDb)
        {
           
        }
		
		public DevRoleService(){}
    }
    
   
     
 public partial class DevRoleModuleService : BaseService<DevRoleModule>, IDevRoleModuleService
    {
        public DevRoleModuleService(DevDbContext DevDb)
           : base(DevDb)
        {
           
        }
		
		public DevRoleModuleService(){}
    }
    
   
     
 public partial class DevRolePessionService : BaseService<DevRolePession>, IDevRolePessionService
    {
        public DevRolePessionService(DevDbContext DevDb)
           : base(DevDb)
        {
           
        }
		
		public DevRolePessionService(){}
    }
    
   
     
 public partial class DevSealmanagerService : BaseService<DevSealmanager>, IDevSealmanagerService
    {
        public DevSealmanagerService(DevDbContext DevDb)
           : base(DevDb)
        {
           
        }
		
		public DevSealmanagerService(){}
    }
    
   
     
 public partial class DevSysemailService : BaseService<DevSysemail>, IDevSysemailService
    {
        public DevSysemailService(DevDbContext DevDb)
           : base(DevDb)
        {
           
        }
		
		public DevSysemailService(){}
    }
    
   
     
 public partial class DevSysmodelService : BaseService<DevSysmodel>, IDevSysmodelService
    {
        public DevSysmodelService(DevDbContext DevDb)
           : base(DevDb)
        {
           
        }
		
		public DevSysmodelService(){}
    }
    
   
     
 public partial class DevUserinfoService : BaseService<DevUserinfo>, IDevUserinfoService
    {
        public DevUserinfoService(DevDbContext DevDb)
           : base(DevDb)
        {
           
        }
		
		public DevUserinfoService(){}
    }
    
   
     
 public partial class DevUserminorService : BaseService<DevUserminor>, IDevUserminorService
    {
        public DevUserminorService(DevDbContext DevDb)
           : base(DevDb)
        {
           
        }
		
		public DevUserminorService(){}
    }
    
   
     
 public partial class DevUserModuleService : BaseService<DevUserModule>, IDevUserModuleService
    {
        public DevUserModuleService(DevDbContext DevDb)
           : base(DevDb)
        {
           
        }
		
		public DevUserModuleService(){}
    }
    
   
     
 public partial class DevUserPessionService : BaseService<DevUserPession>, IDevUserPessionService
    {
        public DevUserPessionService(DevDbContext DevDb)
           : base(DevDb)
        {
           
        }
		
		public DevUserPessionService(){}
    }
    
   
     
 public partial class DevUserRoleService : BaseService<DevUserRole>, IDevUserRoleService
    {
        public DevUserRoleService(DevDbContext DevDb)
           : base(DevDb)
        {
           
        }
		
		public DevUserRoleService(){}
    }
    
   
    
}

