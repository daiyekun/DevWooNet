
layui.config({
    base: '../../lib/' //指定 winui 路径
    , version: '1.0.0-beta'

}).extend({
    winui: 'winui/winui',
    window: 'winui/js/winui.window',
   // devsetter: 'devextend/devsetter'
   devindex: 'devextend/devindex',
}).define(['table', 'jquery', 'winui', 'window', 'layer', 'devindex'], function (exports) {
    winui.renderColor();
    var table = layui.table,
        $ = layui.$,
        devsdevindexetter = layui.devindex,
         msg = winui.window.msg
         tableId = 'flowoptiontable';
    //表格渲染
    var instId= wooutil.getUrlVar('instId');//审批实例
    var nodestrId= wooutil.getUrlVar('nodestrId');//节点ID
   
    var tburl=devsetter.devbaseurl+"api/DevAppInstOption/GetWorkFlowOption";
    var localdata=wooutil.devlocaldata();
    var usertable=table.render({
        id: tableId,
        elem: '#wooflowoptions',
        url:tburl,
        method:'POST',
         contentType:'application/json',
        //height: 'full-65', //自适应高度
        //size: '',   //表格尺寸，可选值sm lg
        //skin: '',   //边框风格，可选值line row nob
        toolbar:true,
        defaultToolbar: ["filter"],
        even:true,  //隔行变色
        page: true,
        headers: {
            "Authorization": "Bearer "+ localdata.token +""
            ,loginkey:localdata.loginkey
        },
        where:{
            otherId:instId,
            nodestrId:nodestrId

        },
       
        limits:devsetter.listtable.mainlistlimits,
        limit: devsetter.listtable.mainlistlimit,
        cols: [[
            // { type: 'checkbox',fixed: 'left' },
            // { field: 'Id', width: 80, title: 'ID', hide: true },
            { field: 'NodeName', title: '节点名称', merge: true, width: 188}
            , { field: 'AddUserName', title: '审批人', width: 130 }
            , { field: 'Opinion', title: '审批意见', width: 400 }
            , { field: 'ReceDateTime', title: '收到日期', width: 140 }
            , { field: 'AddDateTime', title:'审批时间', width: 140 }
        ]],
        done:function(res, curr, count){
            wooutil.devloginout(res);


        }
    });
   
    //表格重载
    function reloadTable() {
        table.reload(tableId, {});
    }

    

  
   
    
    $('#reloadTable').on('click', reloadTable);
    //导出
    $("#excelexport").on('click',function(){
       var $url= devsetter.devuserurl+"api/DevUser/exportexcel";

        wooutil.exportexcel(usertable, { url: $url});
    });
 
    
  

    exports('flowoptionview', {});
});
