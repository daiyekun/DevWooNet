
layui.config({
    base: '../../lib/' //指定 winui 路径
    , version: '1.0.0-beta'

}).extend({
    winui: 'winui/winui',
    window: 'winui/js/winui.window',
    devsetter: 'devextend/devsetter'
}).define(['table', 'jquery', 'winui', 'window', 'layer', 'devsetter'], function (exports) {

    winui.renderColor();

    var table = layui.table,
        $ = layui.$,
        devsetter = layui.devsetter,
        tableId = 'depttableid'
    msg = winui.window.msg
        ;

    //表格渲染
    var tburl = devsetter.devuserurl + "api/DevDepart/list";
    table.render({
        id: tableId,
        elem: '#woodepart',
        url: tburl,
        method: 'POST',
        contentType: 'application/json',
        //height: 'full-65', //自适应高度
        //size: '',   //表格尺寸，可选值sm lg
        //skin: '',   //边框风格，可选值line row nob
        //even:true,  //隔行变色
        page: true,
        limits: devsetter.listtable.mainlistlimits,
        limit: devsetter.listtable.mainlistlimit,
        cols: [[
            { type: 'checkbox' },
            { field: 'Id', width: 80, title: 'ID', hide: true },
            { field: 'Name', title: '名称', width: 160 },
            { field: 'Code', title: '编号', width: 120 },
            { field: 'PName', title: '所属单位', width: 160 },
            { field: 'CateName', title: '机构类型', width: 120 },
            { field: 'Sname', title: '机构简称', width: 120 },
            { field: 'IsMainDic', title: '签约主体', width: 120, templet: '#IsMainTpl', unresize: true },
            { field: 'IsCompany', width: 100, title: '子公司', templet: '#IsCompanyTpl', unresize: true },
            { field: 'Dstatus', title: '状态', width: 100, templet: '#stateTpl' },
            { title: '操作', fixed: 'right', align: 'center', toolbar: '#bardepart', width: 120 }
        ]]
    });
    //监听工具条
    table.on('tool(departtable)', function (obj) { //注：tool是工具条事件名，test是table原始容器的属性 lay-filter="对应的值"
        var data = obj.data; //获得当前行数据
        var layEvent = obj.event; //获得 lay-event 对应的值
        var tr = obj.tr; //获得当前行 tr 的DOM对象
        var ids = [];   //选中的Id
        $(data).each(function (index, item) {
            ids.push(item.Id);
        });
        if (layEvent === 'del') { //删除
            deletedata(ids.toString(), obj);
        } else if (layEvent === 'edit') { //编辑
            if (!data.Id) return;
            var content;
            var index = layer.load(1);
            addDepart('win_updatedept', data.Id, '修改组织机构');
            layer.close(index);

        }
    });
    //表格重载
    function reloadTable() {
        table.reload(tableId, {});
    }

    //打开添加页面
    function addDepart(winid, id, wintitle) {
        var url = "/views/devdepart/build.html";
        if (id > 0) {
            url = "/views/devdepart/build.html?Id=" + id;
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
    function deletedata(ids, obj) {
        var msg = obj ? '确认删除数据【' + obj.data.Name + '】吗？' : '确认删除选中数据吗？'

        top.winui.window.confirm(msg, { icon: 3, title: '删除系统数据' }, function (index) {
            layer.close(index);
            alert(ids);
            //向服务端发送删除指令
            $.ajax({
                type: 'GET',
                url: devsetter.devuserurl + 'api/DevDepart/deldepart',
                //async: false,
                data: { Ids: ids },
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
                        // top.winui.window.msg('向服务端发送删除指令后刷新表格即可', {
                        //     time: 2000
                        // });
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
    $('#adddepart').on('click', function () {

        addDepart('win_adddept', 0, '新增组织机构');
    });
    $('#deletedepart').on('click', function () {
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
        deletedata(ids.toString());
    });
    $('#reloadTable').on('click', reloadTable);
    //菜单树
    $('#treedepart').on('click', function () {
        var url = "/views/devdepart/tree.html";
        top.winui.window.open({
            id: 'win_depttree',
            type: 2,
            title: '组织机构树',
            content: url,
            area: ['50vw', '70vh'],
            offset: ['15vh', '25vw']
        });

    });
    exports('departlist', {});
});
