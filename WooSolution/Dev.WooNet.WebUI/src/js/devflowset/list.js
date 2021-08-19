
layui.config({
    base: '../../lib/' //指定 winui 路径
    , version: '1.0.0-beta'

}).extend({
    winui: 'winui/winui',
    window: 'winui/js/winui.window',
   devindex: 'devextend/devindex',
   
}).define(['table', 'jquery', 'winui', 'window', 'layer', 'devindex','form'], function (exports) {
    winui.renderColor();
    var table = layui.table,
        $ = layui.$,
        devsdevindexetter = layui.devindex,
         msg = winui.window.msg,
         form=layui.form,
         tableId = 'flowlisttableid';
    //表格渲染
   
    var tburl=devsetter.devuserurl+"api/DevFlowTemp/list?ctype=0";
    var localdata=wooutil.devlocaldata();
    var woocustomer=table.render({
        id: tableId,
        elem: '#wooflowlist',
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
            { field: 'Name', title: '流程名称', width: 180,templet: '#titleTpl',  fixed: 'left' },
            { field: 'ObjTypeDic', title: '对象名称',  width: 150, fixed: 'left' },
            { field: 'CateName', title: '对象类别', width: 150 },
            {
                field: 'Version', title: '版本', width: 100, templet: function (d) {
                    return d.Version + ".0";
                }
            }
            , { field: 'DeptsName', title: '所属机构', width: 150 }
            , { field: 'FlowItemsDic', title: '审批事项', width: 150 }
            , { field: 'AddUserName', title: '建立人', width: 120 }
            , { field: 'AddDateTime', title: '建立日期', width: 120 }
            , { field: 'Id', title: 'Id', width: 50, hide: true }
            , { field: 'IsValid', width: 120, title: '状态', align: 'center', templet: '#IsValidTpl', unresize: true }
            , { title: '操作', width: 240, align: 'center', fixed: 'right', toolbar: '#devtablebar' }
          
          
        ]],
        done:function(res, curr, count){
            
            wooutil.devloginout(res);

            

        }
    });
    //监听工具条
    table.on('tool(wooflowlist)', function (obj) { //注：tool是工具条事件名，test是table原始容器的属性 lay-filter="对应的值"
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
            devopenwin('win_updateflowset', data.Id, '修改流程模板');
            layer.close(index);
        }else if(layEvent=='flowset'){
            if (!data.Id) return;
            var index = layer.load(1);
            devopenwin('win_credateflowset', data.Id, '画流程图',1);
            layer.close(index);
        }
        else if(layEvent === 'showview'){
            if (!data.Id) return;
            var index = layer.load(1);
            devopenwin('win_viewflowset', data.Id, '查看流程模板',2);
            layer.close(index);


        }
    });
    //表格重载
    function reloadTable() {
        table.reload(tableId, {});
    }

     //打开添加页面
     function devopenwin (winid, id, wintitle,Isview) {
        var url = "/views/devflowset/build.html";
        if (id > 0) {
            url = "/views/devflowset/build.html?Id=" + id;
        }
        if(Isview==1){
            url = "/views/devflowset/flowset.html?Id=" + id;
        }else if(Isview==2){
            url = "/views/devflowset/view.html?Id=" + id;
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
                url: devsetter.devbaseurl + 'api/DevFlowTemp/delete',
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

        devopenwin('win_addflowset', 0, '新增流程模板');
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
        var url="/views/devuser/selstate.html?Id=" + checkStatus.data[0].Id+"&ustate="+checkStatus.data[0].Ustate;
        top.winui.window.open({
            id: 'win_userstate',
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
    

    //
    function search() {//查询
        table.reload(tableId, {
            page: { curr: 1 }
            , where: {
             kword: $("input[name=keyWord]").val()

            }
        });

    }

     //监听状态操作
     form.on('switch(IsValid)', function (obj) {
        var state = obj.elem.checked ? 1 : 0;//状态
        wooutil.devajax({
                type: 'POST',
                url: devsetter.devbaseurl + 'api/DevFlowTemp/UpdateField',
                data: JSON.stringify( { Id: this.value, Field: "IsValid", FieldVal: state }),
                dataType: "json",
                contentType: "application/json; charset=utf-8",
               success: function (res) {
                layer.msg('修改成功！');

            }

        });
    });
    
 
    exports('flowsetlist', {});
});
