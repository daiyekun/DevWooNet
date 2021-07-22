
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
         tableId = 'useridtableid';
    //表格渲染
   
    var tburl=devsetter.devuserurl+"api/DevUser/list";
    var localdata=wooutil.devlocaldata();
    var usertable=table.render({
        id: tableId,
        elem: '#woouser',
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
       
        limits:devsetter.listtable.mainlistlimits,
        limit: devsetter.listtable.mainlistlimit,
        cols: [[
            { type: 'checkbox',fixed: 'left' },
            { field: 'Id', width: 80, title: 'ID', hide: true },
            { field: 'Name', title: '用户名', width: 120,templet: '#titleTpl', fixed: 'left' },
            { field: 'DeptName', title: '所属部门', width: 180 },
            { field: 'ShowName', title: '显示名称', width: 140 },
            { field: 'SexDic', title: '性别', width: 80 },
            { field: 'Tel', title: '电话', width: 110 },
            { field: 'Mobile', title: '手机', width: 120 },
            { field: 'Email', title: '邮箱', width: 130 },
            { field: 'Ustate', title: '状态', width: 60, templet: '#stateTpl' },
            { title: '操作', fixed: 'right', align: 'center', toolbar: '#barUser', width: 120 }
        ]],
        done:function(res, curr, count){
            wooutil.devloginout(res);


        }
    });
    //监听工具条
    table.on('tool(usertable)', function (obj) { //注：tool是工具条事件名，test是table原始容器的属性 lay-filter="对应的值"
        var data = obj.data; //获得当前行数据
        var layEvent = obj.event; //获得 lay-event 对应的值
        var tr = obj.tr; //获得当前行 tr 的DOM对象
        var ids = [];   //选中的Id
        $(data).each(function (index, item) {
            ids.push(item.Id);
        });
        if (layEvent === 'del') { //删除
            deleteUser(ids, obj);
        } else if (layEvent === 'edit') { //编辑
            if (!data.Id) return;
            var index = layer.load(1);
            openUser('win_updateuser', data.Id, '修改用户');
            layer.close(index);
        }else if(layEvent === 'showview'){
            if (!data.Id) return;
            var index = layer.load(1);
            openUser('win_updateuser', data.Id, '查看用户',true);
            layer.close(index);


        }
    });
    //表格重载
    function reloadTable() {
        table.reload(tableId, {});
    }

     //打开添加页面
     function openUser (winid, id, wintitle,Isview) {
        var url = "/views/devuser/build.html";
        if (id > 0) {
            url = "/views/devuser/build.html?Id=" + id;
        }
        if(Isview){
            url = "/views/devuser/view.html?Id=" + id;
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
    function deleteUser(ids, obj) {
        var msg = obj ? '确认删除数据【' + obj.data.ShowName + '】吗？' : '确认删除选中数据吗？'
        top.winui.window.confirm(msg, { icon: 3, title: '删除系统数据' }, function (index) {
            if(obj){
                layer.close(index);
            }else{
                top.layer.close(index);

            }
            //向服务端发送删除指令
            $.ajax({
                type: 'GET',
                url: devsetter.devuserurl + 'api/DevUser/deluser',
                //async: false,
                data: { Ids: ids.toString() },
                dataType: 'json',
                success: function (res) {
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
    //重置密码
    function resetpwd(ids,obj){
        top.winui.window.confirm('您确定重置您选择用户的密码吗？', { icon: 3, title: '重置密码' }, function (index) {
            layer.close(index);
            //向服务端发送指令
            $.ajax({
                type: 'GET',
                url: devsetter.devuserurl + 'api/DevUser/restpwd',
                //async: false,
                data: { Ids: ids.toString() },
                dataType: 'json',
                success: function (res) {
                    top.winui.window.msg('操作成功！', {
                        icon: 1,
                        time: 2000
                    });

                },
                error: function (xml) {
                    top.winui.window.msg('请求失败', {
                        // icon: 1,
                        time: 2000
                    });

                }
            });




        });

    }


    //绑定按钮事件
    $('#addUser').on('click', function () {

        openUser('win_adduser', 0, '新增用户');
    });
    //删除按钮
    $('#deleteuser').on('click', function () {
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
        deleteUser(ids);
    });
    //重置密码
    $('#resetPwd').on('click',function(){
        
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
        resetpwd(ids);


    })
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
    $("#btnsearchuser").on('click',function(){
        searchUser();
    });

    //用户
    function searchUser() {//查询
        table.reload(tableId, {
            page: { curr: 1 }
            , where: {
             kword: $("input[name=keyWord]").val()

            }
        });

    }
    //导出
    $("#excelexport").on('click',function(){
       var $url= devsetter.devuserurl+"api/DevUser/exportexcel";

        wooutil.exportexcel(usertable, { url: $url});
    });
 
    
  

    exports('userlist', {});
});
