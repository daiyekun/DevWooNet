/**
 * dev 自定义全局配置文件 
 */
 layui.define(['laytpl', 'layer'], function(exports){
    exports('devsetter', {
        devbaseurl:'http://localhost:8059/',//请求根目录
        devuserurl:"http://localhost:8059/",//用户相关用户根目录
        listtable:{//列表配置
         mainlistlimits:[8, 16, 24, 32, 40, 48, 56,64,100]//大列表下拉页表
         ,mainlistlimit:8//默认每页显示条数
        }
 
    }
  );
});
