
layui.config({
    base: '../../lib/' //指定 winui 路径
    , version: '1.0.0-beta'

}).extend({
    winui: 'winui/winui',
    window: 'winui/js/winui.window',
   devindex: 'devextend/devindex',
   soulTable:'soultable/soulTable',
   tableChild:'soultable/tableChild',
   tableMerge:'soultable/tableMerge',
   tableFilter:'soultable/tableFilter',
   excel:'soultable/excel'
}).define(['table', 'jquery', 'winui', 'window', 'layer', 'devindex','soulTable'], function (exports) {
    winui.renderColor();
    var table = layui.table,
        $ = layui.$,
        devsdevindexetter = layui.devindex,
         msg = winui.window.msg,
         soulTable=layui.soulTable,
         tableId = 'customertableid';
    //表格渲染
   
    var tburl=devsetter.devuserurl+"api/DevCompany/list?ctype=0";
    var localdata=wooutil.devlocaldata();
    var woocustomer=table.render({
        id: tableId,
        elem: '#woocustomer',
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
            { type: 'checkbox',fixed: 'left' },
            { field: 'Name', title: '名称', width: 180,templet: '#titleTpl',  fixed: 'left' },
            { field: 'Code', title: '编号',  width: 130 },
            { field: 'CompClassName', title: '类别', width: 140 },
            { field: 'StateDic', title: '状态', width: 120,templet: '#stateTpl'  },
            { field: 'CareditName', title: '信用等级', width: 120 },
            { field: 'LevelName', title: '级别', width: 120 },
            { field: 'Reserve1', title: '备用1', width: 120,hide: true },
            { field: 'Reserve2', title: '备用2', width: 120,hide: true},
            { field: 'FaceUserName', title: '负责人', width: 120,hide: true},
            { field: 'GjName', title: '国家', width: 100,hide: true},
            { field: 'PrName', title: '省', width: 100},
            { field: 'CityName', title: '市', width: 100},
            { field: 'Trade', title: '行业', width: 120},
            { field: 'AddUserName', title: '创建人', width: 100},
            { field: 'AddDateTime', title: '创建日期', width: 100},
            { field: 'Id', width: 80, title: 'ID', hide: true },
            // { field: 'WnodeName', title: '流程节点', width: 140},
            // { field: 'Ustate', title: '流程状态', width: 120},
            // { field: 'Ustate', title: '审批事项', width: 130},
            { title: '操作', fixed: 'right', align: 'center', toolbar: '#devtablebar', width: 120 }
        ]],
        done:function(res, curr, count){
            soulTable.render(this);
            wooutil.devloginout(res);

            

        }
    });
    //监听工具条
    table.on('tool(woocustomer)', function (obj) { //注：tool是工具条事件名，test是table原始容器的属性 lay-filter="对应的值"
        var data = obj.data; //获得当前行数据
        var layEvent = obj.event; //获得 lay-event 对应的值
        var tr = obj.tr; //获得当前行 tr 的DOM对象
        var ids = [];   //选中的Id
        $(data).each(function (index, item) {
            ids.push(item.Id);
        });
        if (layEvent === 'del') { //删除
            devdelete(ids, obj);
        } else if (layEvent === 'edit') { //编辑
            if (!data.Id) return;
            var index = layer.load(1);
            devopenwin('win_updatecustomer', data.Id, '修改客户');
            layer.close(index);
        }else if(layEvent === 'showview'){
            if (!data.Id) return;
            var index = layer.load(1);
            devopenwin('win_viewcustomer', data.Id, '查看客户',true);
            layer.close(index);


        }
    });
    //表格重载
    function reloadTable() {
        table.reload(tableId, {});
    }

     //打开添加页面
     function devopenwin (winid, id, wintitle,Isview) {
        var url = "/views/devcustomer/build.html";
        if (id > 0) {
            url = "/views/devcustomer/build.html?Id=" + id;
        }
        if(Isview){
            url = "/views/devcustomer/view.html?Id=" + id;
        }
        top.winui.window.open({
            id: winid,
            type: 2,
            title: wintitle,
            content: url,
            area: ['50vw', '70vh'],
            offset: ['15vh', '25vw']
        });
    }

  
    //删除角色
    function devdelete(ids, obj) {
        var msg = obj ? '确认删除数据【' + obj.data.Name + '】吗？' : '确认删除选中数据吗？'
        top.winui.window.confirm(msg, { icon: 3, title: '删除系统数据' }, function (index) {
            if(obj){
                layer.close(index);
            }else{
                top.layer.close(index);

            }
            //向服务端发送删除指令
            wooutil.devajax({
                type: 'GET',
                url: devsetter.devuserurl + 'api/DevCompany/delete',
                data: { Ids: ids.toString() },
                dataType: 'json',
                success: function (res) {
                    if(res.code==0){
                         //刷新表格
                    if (obj) {
                        top.winui.window.msg('删除成功', {
                            icon: 1,
                            time: 2000
                        });
                        obj.del(); //删除对应行（tr）的DOM结构
                    } else {
                        reloadTable();  //直接刷新表格
                    }
                    }else{
                        top.winui.window.msg(res.msg, {
                            time: 1000
                        });
                        reloadTable();  //直接刷新表格
                    }
                   

                },
                error: function (xml) {
                    top.winui.window.msg('删除失败', {
                        // icon: 1,
                        time: 2000
                    });

                }
            });




        });
    }
    


    //绑定按钮事件
    $('#adddev').on('click', function () {

        devopenwin('win_addcustomer', 0, '新增客户');
    });
    //删除按钮
    $('#deletedev').on('click', function () {
        var checkStatus = table.checkStatus(tableId);
        var checkCount = checkStatus.data.length;
        if (checkCount < 1) {
            top.winui.window.msg('请选择一条数据', {
                time: 2000
            });
            return false;
        }
        var ids = [];
        $(checkStatus.data).each(function (index, item) {
            ids.push(item.Id);
        });
        devdelete(ids);
    });
   
    $('#reloadTable').on('click', reloadTable);
    //跳转修改状态界面
    $('#tostate').on('click', function(){
        var checkStatus = table.checkStatus(tableId);
        var checkCount = checkStatus.data.length;
        if (checkCount < 1) {
            top.winui.window.msg('请选择一条数据', {
                time: 2000
            });
            return false;
        }
        var url="/views/devcustomer/selstate.html?Id=" 
        + checkStatus.data[0].Id+"&ustate="+checkStatus.data[0].Dstatus
        +"&objtype=0&objcate="+checkStatus.data[0].CompClassId
        +"&dname="+encodeURI(encodeURI(checkStatus.data[0].Name));
        top.winui.window.open({
            id: 'win_customerstate',
            type: 2,
            title: '修改状态',
            maxmin:true,
            content: url,
            area: ['25vw', '35vh'],
            offset: ['25vh', '25vw']
        });

    });
    $("#btnsearch").on('click',function(){
        search();
    });

    //用户
    function search() {//查询
        table.reload(tableId, {
            page: { curr: 1 }
            , where: {
             kword: $("input[name=keyWord]").val()

            }
        });

    }
    //导出
    $("#excelexport").on('click',function(){
       var $url= devsetter.devuserurl+"api/DevCompany/exportexcel";

        wooutil.exportexcel(woocustomer, { url: $url});
    });
 
    
  

    exports('customerlist', {});
});
