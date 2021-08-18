
layui.config({
    base: '../../lib/' //指定 winui 路径
    , version: '1.0.0-beta'

}).extend({
    winui: 'winui/winui',
    window: 'winui/js/winui.window',
    devindex: 'devextend/devindex'
}).define(['table', 'jquery', 'winui', 'window', 'layer', 'devindex'], function (exports) {
    winui.renderColor();
    var table = layui.table,
        $ = layui.$,
        devindex = layui.devindex,
         msg = winui.window.msg
         tableId = 'flowgrouptableid';
     
    //表格渲染
   
    var tburl=devsetter.devbaseurl+"api/DevFlowGroup/list";
    var loginkey=layui.data(devsetter.devtableName)[devsetter.request.loginkey] || '';
    var acctoken=layui.data(devsetter.devtableName)[devsetter.request.tokenName] || '';
    table.render({
        id: tableId,
        elem: '#table-woorflowgroup',
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
        limits:devsetter.listtable.mainlistlimits,
        limit: devsetter.listtable.mainlistlimit,
        headers:{acToken:acctoken,loginkey:loginkey},
        cols: [[
            { type: 'checkbox',fixed: 'left' },
            { field: 'Id', width: 80, title: 'ID', hide: true },
            { field: 'Name', title: '名称', width: 180,templet: '#titleTpl', fixed: 'left' },
            { field: 'Remark', title: '说明', width: 300 },
            { title: '操作', fixed: 'right', align: 'center', toolbar: '#barrole', width: 350 }
        ]],done:function(res, curr, count){
            if(res.code==1001)
            {
                parent.location.href="/login2.html";
            }
            


        }
    });
    //监听工具条
    table.on('tool(table-woorflowgroup)', function (obj) { //注：tool是工具条事件名，test是table原始容器的属性 lay-filter="对应的值"
        var data = obj.data; //获得当前行数据
        var layEvent = obj.event; //获得 lay-event 对应的值
        var tr = obj.tr; //获得当前行 tr 的DOM对象
        var ids = [];   //选中的Id
        $(data).each(function (index, item) {
            ids.push(item.Id);
        });
        if (layEvent === 'del') { //删除
            deleteflowgroup(ids, obj);
        } else if (layEvent === 'edit') { //编辑
            if (!data.Id) return;
            var index = layer.load(1);
            openflowgroup('win_updateflowgroup', data.Id, '修改组信息');
            layer.close(index);
        }else if(layEvent === 'adduser'){
            if (!data.Id) return;
            var index = layer.load(1);
            openflowgroup('win_updategroup', data.Id, '查看组信息',true);
            layer.close(index);

        }
        else if(layEvent === 'showview'){
            if (!data.Id) return;
            var index = layer.load(1);
            openflowgroup('win_updaterole', data.Id, '查看组',true);
            layer.close(index);


        }
    });
    //表格重载
    function reloadTable() {
        table.reload(tableId, {});
    }

     //打开添加页面
     function openflowgroup (winid, id, wintitle,Isview) {
        var url = "/views/devflowset/flowgroupbuild.html";
        if (id > 0) {
            url = "/views/devflowset/flowgroupbuild.html?Id=" + id;
        }
        if(Isview){
            url = "/views/devflowset/flowgroupview.html?Id=" + id;
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
    function deleteflowgroup(ids, obj) {
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
                url: devsetter.devbaseurl + 'api/DevFlowGroup/delete',
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
    //绑定按钮事件
    $('#add').on('click', function () {

        openflowgroup('win_addflowgroup', 0, '新增角色');
    });
    //删除按钮
    $('#delete').on('click', function () {
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
        deleteflowgroup(ids);
    });
    $('#reloadTable').on('click', reloadTable);
    
  

    exports('flowgroup', {});
});
