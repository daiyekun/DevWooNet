
layui.config({
    base: '../../lib/' //指定 winui 路径
    , version: '1.0.0-beta'

}).extend({
    winui: 'winui/winui',
    window: 'winui/js/winui.window',
   devindex: 'devextend/devindex',
   
}).define(['table', 'jquery', 'winui', 'window', 'layer', 'devindex'], function (exports) {
    winui.renderColor();
    var table = layui.table,
        $ = layui.$,
        devsdevindexetter = layui.devindex,
         msg = winui.window.msg,
         tableId = 'flowgrouptableid';
    //表格渲染
   
    var tburl=devsetter.devuserurl+"api/DevFlowGroup/list";
    var localdata=wooutil.devlocaldata();
    var woocustomer=table.render({
        id: tableId,
        elem: '#FlowTemp-selectGroup',
        url:tburl,
        method:'POST',
         contentType:'application/json',
        //height: 'full-65', //自适应高度
        //size: '',   //表格尺寸，可选值sm lg
        //skin: '',   //边框风格，可选值line row nob
        toolbar:false,
        //defaultToolbar: ["filter"],
        even:true,  //隔行变色
        page: true,
        headers: {
            "Authorization": "Bearer "+ localdata.token +""
            ,loginkey:localdata.loginkey
        },
       
        limits:devsetter.listtable.mainlistlimits,
        limit: devsetter.listtable.mainlistlimit,
        rowDrag: {
            numbers: false
          },
           filter: {
            cache: true
           , bottom: false
        },
        cols: [[
            { type: 'numbers', fixed: 'left' }
            , { type: 'radio', fixed: 'left' }
            , { field: 'Name', title: '组名称', width: 150 }
            , { field: 'UserNames', title: '用户', width: 300 }
            
            , { field: 'Remark', title: '备注', width: 150 }
          
          
        ]],
        done:function(res, curr, count){
            
            wooutil.devloginout(res);

            

        }
    });
    
    

    

  
    
    


 
  
   
   
    
    $("#btnsearch").on('click',function(){
        search();
    });
    

    //
    function search() {//查询
        table.reload(tableId, {
            page: { curr: 1 }
            , where: {
             kword: $("input[name=keyWord]").val()

            }
        });

    }
    
 
    exports('selectgroup', {});
});
