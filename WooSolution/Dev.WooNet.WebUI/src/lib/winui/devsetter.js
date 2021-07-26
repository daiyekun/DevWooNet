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
         ,devtableName: 'devadmin' //本地存储表名
        }//自定义请求字段
        ,request: {
          tokenName: "devaccesstoken" //自动携带 token 的字段名（如：access_token）。可设置 false 不携带。
          ,loginname:'loginname'
          ,showname:'showname'
          ,loginkey:'loginkey'
         }
    
    //自定义响应字段
      ,response: {
      statusName: 'code' //数据状态的字段名称
      ,statusCode: {
        ok: 0 //数据状态一切正常的状态码
        ,logout: 1001 //登录状态失效的状态码
      }
      ,msgName: 'msg' //状态信息的字段名称
      ,dataName: 'data' //数据详情的字段名称
      },
      devupload: {//上传组件配置
        size: '5368709120'
       , accept: 'file'
       , exts: 'txt|doc|jpg|gif|png|rar|zip|docx|pdf|xls|xlsx|jpeg|mdi|tif|dwg|psd|3ds|eps|vsd|TXT|DOC|JPG|JPEG|MDI|TIF|GIF|PNG|RAR|ZIP|DOCX|PDF|XLS|XLSX|DWG|PSD|3DS|EPS|VSD'
       , uploadIp: 'http://localhost:8059/'
       }
 
    }
  );
});
