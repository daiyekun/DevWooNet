
layui.config({
    base: '../../lib/' //指定 winui 路径
    , version: '1.0.0-beta'

}).extend({
    winui: 'winui/winui',
    window: 'winui/js/winui.window',
   // devsetter: 'devextend/devsetter'
   devindex: 'devextend/devindex',
   treetable: 'devextend/treetable/treetable'
}).define(['table','jquery', 'winui', 'window', 'layer', 'devindex','treetable'], function (exports) {
    winui.renderColor();
    var table = layui.table
   
        $ = layui.$,
        devindex = layui.devindex,
         msg = winui.window.msg,
         treetable=layui.treetable,
         tableId = 'sysmodelidtableid';
    //表格渲染
   
    var tburl=devsetter.devuserurl+"api/DevSysModel/gettreetable";
    var renderTable = function () {
        // layer.load(2);
        treetable.render({
            treeColIndex: 1,          // treetable新增参数
            treeSpid: 0,             // 与第一级的父菜单有关系
            treeIdName: 'Id',       // 节点ID字段
            treePidName: 'Pid',     // 父节点字段
            treeDefaultClose: false,   // treetable新增参数
            treeLinkage: true,        // treetable新增参数
            elem: '#woosysmodel',
            url: tburl,
            id: tableId,
            // ,height: setter.table.height_2,
            cols: [[
                // { type: 'numbers',fixed: 'left' },
                { type: 'checkbox',fixed: 'left' },
                { field: 'Name', title: '名称', width: 180, minWidth: 180,fixed: 'left'  },
                { field: 'Title', title: '标题', width: 160 },
                { field: 'Ico', title: '图标', width: 100, templet: '#IcoTpl' }
                , { field: 'RequestUrl', title: '页面地址', width: 140 }
                , { field: 'PageType', title: '页面类型', width: 140,templet:'#pagetypeTpl'}
                , { field: 'IsShow', title: '桌面显示', width: 130, templet: '#IsshowTpl' }
                , { field: 'Sort', title: '排序', width: 130,edit: 'text'}
                , { title: '操作', width: 150, align: 'center', templet: '#devsysmodeltbar',fixed: 'right' }
            ]],
            done: function () {
                // layer.closeAll('loading');
            }
        });
    };
    renderTable();
   
    //监听工具条
    table.on('tool(woosysmodel)', function (obj) { //注：tool是工具条事件名，test是table原始容器的属性 lay-filter="对应的值"
        var data = obj.data; //获得当前行数据
        var layEvent = obj.event; //获得 lay-event 对应的值
        var tr = obj.tr; //获得当前行 tr 的DOM对象
        var ids = [];   //选中的Id
        $(data).each(function (index, item) {
            ids.push(item.Id);
        });
        if (layEvent === 'del') { //删除
            deleteSysModel(ids, obj);
        } else if (layEvent === 'edit') { //编辑
            if (!data.Id) return;
            var index = layer.load(1);
            opensysmodel('win_updatesysmodel', data.Id, '修改系统菜单');
            layer.close(index);
        }else if(layEvent === 'showview'){
            if (!data.Id) return;
            var index = layer.load(1);
            opensysmodel('win_updatesysmodel', data.Id, '查看系统菜单',true);
            layer.close(index);


        }
    });
    //表格重载
    function reloadTable() {
        renderTable();
    }

     //打开添加页面
     function opensysmodel (winid, id, wintitle,Isview) {
        var url = "/views/devsysmodel/build.html";
        if (id > 0) {
            url = "/views/devsysmodel/build.html?Id=" + id;
        }
        if(Isview){
            url = "/views/devsysmodel/view.html?Id=" + id;
        }
        top.winui.window.open({
            id: winid,
            type: 2,
            title: wintitle,
            content: url,
            area: ['90%', '90%']
            // area: ['50vw', '70vh'],
            // offset: ['15vh', '25vw']
        });
    }

  
    //删除
    function deleteSysModel(ids, obj) {
        var msg = obj ? '确认删除数据【' + obj.data.Name + '】吗？' : '确认删除选中数据吗？'
        top.winui.window.confirm(msg, { icon: 3, title: '删除系统数据' }, function (index) {
            if(obj){
                layer.close(index);
            }else{
                top.layer.close(index);

            }
            //向服务端发送删除指令
            $.ajax({
                type: 'GET',
                url: devsetter.devuserurl + 'api/DevSysModel/delsysmodel',
                //async: false,
                data: { Ids: ids.toString() },
                dataType: 'json',
                success: function (res) {
                    reloadTable();  //直接刷新表格

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
    $('#addSysmodel').on('click', function () {

        opensysmodel('win_addsysmodel', 0, '新增系统菜单');
    });
    //删除按钮
    $('#deletesysmodel').on('click', function () {
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
        deleteSysModel(ids);
    });
   
    $('#reloadTable').on('click', reloadTable);

    // $("#btnsearchsysmodel").on('click',function(){
    //     searchUser();
    // });

    // //用户
    // function searchUser() {//查询
    //     table.reload(tableId, {
    //         page: { curr: 1 }
    //         , where: {
    //          kword: $("input[name=keyWord]").val()

    //         }
    //     });

    // }
    
    table.on('edit(woosysmodel)', function(obj){
        var value = obj.value //得到修改后的值
        ,data = obj.data //得到所在行所有键值
        ,field = obj.field; //得到字段
        $.ajax({
            type: 'POST',
            url: devsetter.devuserurl + 'api/DevSysModel/updatefield',
            //async: false,
            processData: false,
            data: JSON.stringify({ Id: data.Id,FileName:'Sort',UpdateVal: value}),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (json) {
                
                            top.winui.window.msg('操作成功', {
                                icon: 1,
                                time: 2000
                            });
                            reloadTable();  

            },
            error: function (xml) {
                msg('操作失败');
                console.log(xml.responseText);
            }
        });
       
        
      });
  

    exports('sysmodellist', {});
});
