layui.config({
    base: '../../lib/winui/' //指定 winui 路径
    , version: '1.0.0-beta'
}).define(['table', 'jquery', 'winui', 'devsetter'], function (exports) {
    window.onload = function () {
        winui.init();
    }
    winui.renderColor();

    var table = layui.table,
        $ = layui.$,
        devsetter = layui.devsetter;
    /*****************************************新增可能存在的修改--begin***************************************************** */
    var departtabcols = [
        { type: 'checkbox' },
        { field: 'Id', width: 80, title: 'ID', hide: true },
        { field: 'Name', title: '名称', width: 160, edit: 'text' },
        { field: 'Remark', title: '备注', width: 200, edit: 'text' }
    ];
    //渲染表格
    tablerender('woodepartdic', 0, departtabcols);
    tableEdit('woodepartdic');//注册编辑
    /*
    *根据表格ID获取枚举值
    *如果是增加枚举一定要记得增加
     */
    function GetTypeEnum(tableId) {
        var tpId = -1;
        switch (tableId) {
            case "woodepartdic"://部门类别
                tpId = 0;
                break;

        }
        return tpId;
    }
    /*****************************************新增可能存在的修改--end***************************************************** */

    //创建表格
    function tablerender(tableId, dataint, tabcols) {
        var tburl = devsetter.devuserurl + "api/DataDic/list";
        table.render({
            id: tableId,
            elem: '#' + tableId,
            url: tburl,
            method: 'POST',
            contentType: 'application/json',
            where: { "otherId": dataint },
            page: true,
            limits: devsetter.listtable.mainlistlimits,
            limit: devsetter.listtable.mainlistlimit,
            cols: [tabcols]
        });


    }


    //编辑触发事件
    function tableEdit(tableId) {
        table.on('edit(' + tableId + ')', function (obj) {
            var value = obj.value //得到修改后的值
                , data = obj.data //得到所在行所有键值
                , field = obj.field; //得到字段
            // layer.msg('[ID: '+ data.Id +'] ' + field + ' 字段更改为：'+ value);
            var $url = devsetter.devuserurl + "api/DataDic/UpdateFiled";
            var postdata = JSON.stringify({
                Id: data.Id,
                Field: field,
                FieldVal: value

            });
            $.ajax({
                type: "POST",
                url: $url,
                data: postdata,
                //crossDomain: true,
                contentType: 'application/json',
                dataType: 'json',
                success: function (data, status) {
                    reloadTable(tableId);

                }
            });


        });
    }
    //表格重载
    function reloadTable(obj) {
        var tableId = "";
        if (obj.type == "click") {
            tableId = $(this).attr("dev-tableId");
        } else {
            tableId = obj;
        }
        table.reload(tableId, {});
    }


    //新增
    function add() {
        var tableId = $(this).attr("dev-tableId");
        var $url = devsetter.devuserurl + "api/DataDic/AddDic";
        $.ajax({
            type: "GET",
            url: $url,
            data: { "TypeInt": GetTypeEnum(tableId) },
            success: function (data, status) {
                reloadTable(tableId);

            }
        });

    }
    //删除角色
    function deletedata(ids, tableId) {
        var msg = '确认删除选中数据吗？'
        top.winui.window.confirm(msg, { icon: 3, title: '删除系统角色' }, function (index) {
            layer.close(index);
            var $url = devsetter.devuserurl + "api/DataDic/DeleteDic";
            $.ajax({
                type: "GET",
                url: $url,
                data: { "Ids": ids },
                success: function (data, status) {
                    top.winui.window.msg('删除成功', {
                        icon: 1,
                        time: 1500
                    });
                    reloadTable(tableId);

                }
            });


        });
    }
    //绑定按钮事件
    $('button.adddatadic').on('click', add);
    $('button.deletedic').on('click', function () {
        var tableId = $(this).attr("dev-tableId");
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
        deletedata(ids.toString(), tableId);
    });
    $('button.reloadTable').on('click', reloadTable);

    exports('datadiclist', {});
});
