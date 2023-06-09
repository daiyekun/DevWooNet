// layui.config({
//     base: '../../lib/winui/' //指定 winui 路径
//     , version: '1.0.0-beta'
// })

layui.config({
    base: '../../lib/' //指定 winui 路径
    , version: '1.0.0-beta'
}).extend({
    winui: 'winui/winui',
    window: 'winui/js/winui.window',
    devindex: 'devextend/devindex'
  
}).define(['table', 'jquery', 'winui', 'devindex'], function (exports) {
    window.onload = function () {
        winui.init();
    }
    winui.renderColor();

    var table = layui.table,
        $ = layui.$,
        devindex = layui.devindex;
    /*****************************************新增可能存在的修改--begin***************************************************** */
    //常规列
    var commcols=[
        { type: 'checkbox' },
        { field: 'Id', width: 80, title: 'ID', hide: true },
        { field: 'Name', title: '名称', width: 160, edit: 'text' },
        { field: 'Remark', title: '备注', width: 200, edit: 'text' }
    ];
    //部门类别
    var departtabcols = commcols;
     tablerender('woodepartdic', departtabcols);
     tableEdit('woodepartdic');
    //合同类别
    var conttabcols = commcols;
   
    tablerender('woocontdic',  conttabcols);
    tableEdit('woocontdic');
    //客户类别
    var customertabcols = commcols;
   
    tablerender('woodcustomerdic',  customertabcols);
    tableEdit('woodcustomerdic');
    //客户附件类别
    var custfiletabcols =commcols;
   
    tablerender('woodcustfiledic', custfiletabcols);
    tableEdit('woodcustfiledic');
    //客户级别
    var custLeveltabcols =commcols;
    
    tablerender('woodcustLeveldic', custLeveltabcols);
    tableEdit('woodcustLeveldic');
    //信用等级
    var custCaredittabcols = commcols;
    tablerender('woodcustCareditdic', custCaredittabcols);
    tableEdit('woodcustCareditdic');
    //公司类型
    var custdcustTypetabcols =commcols; 
    tablerender('woodcustTypetdic', custdcustTypetabcols);
    tableEdit('woodcustTypetdic');
    //供应商类别
    var supplierTypetabcols =commcols; 
    tablerender('woodsupplierdic', supplierTypetabcols);
    tableEdit('woodsupplierdic');
    //供应商附件类别
    var supplierfileTypetabcols =commcols; 
    tablerender('woodsupplierfiledic', supplierfileTypetabcols);
    tableEdit('woodsupplierfiledic');
    //供应商级别
    var supplierLeveltabcols =commcols; 
    tablerender('woodsupplierLeveldic', supplierLeveltabcols);
    tableEdit('woodsupplierLeveldic');
    //供应商信用等级
    var supplierCaredittabcols =commcols; 
    tablerender('woodsupplierCareditdic', supplierCaredittabcols);
    tableEdit('woodsupplierCareditdic');
    //其他对方类别
    var thirddictabcols =commcols; 
    tablerender('woodthirddic', thirddictabcols);
    tableEdit('woodthirddic');
    //其他对方附件类别
    var thirdfiledictabcols =commcols; 
    tablerender('woodthirdfiledic', thirdfiledictabcols);
    tableEdit('woodthirdfiledic');
    //其他对方级别
    var thirdLeveldictabcols =commcols; 
    tablerender('woodthirdLeveldic', thirdLeveldictabcols);
    tableEdit('woodthirdLeveldic');
    //其他对方信用等级
    var thirdCareditdictabcols =commcols; 
    tablerender('woodthirdCareditdic', thirdCareditdictabcols);
    tableEdit('woodthirdCareditdic');

    
   
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
                case "woocontdic"://合同类别
                tpId = 1;
                case "woodsupplierdic"://供应商类别
                tpId = 2;
                break;
                case "woodcustomerdic"://客户类别
                tpId = 3;
                break;
                case "woodthirddic"://其他对方类别
                tpId = 4;
                break;
                case "woodcustfiledic"://客户附件类别
                tpId = 8;
                break;
                case "woodcustLeveldic"://客户级别
                tpId = 5;
                break;
                case "woodcustCareditdic"://信用等级
                tpId = 6;
                break;
                case "woodcustTypetdic"://公司类型
                tpId = 7;
                break;
                case "woodsupplierfiledic"://供应商附件
                tpId = 9;
                break;
                case "woodthirdfiledic"://其他对方附件
                tpId = 10;
                break;
                case "woodsupplierLeveldic"://供应商级别
                tpId = 11;
                break;
                case "woodthirdLeveldic"://其他对方级别
                tpId = 12;
                break;
                case "woodsupplierCareditdic"://供应商信用等级
                tpId = 33;
                break;
                case "woodthirdCareditdic"://其他对方信用等级
                tpId = 34;
                break;

        }
        return tpId;
    }
    /*****************************************新增可能存在的修改--end***************************************************** */

    //创建表格
    function tablerender(tableId,  tabcols) {
        var tburl = devsetter.devuserurl + "api/DataDic/list";
        table.render({
            id: tableId,
            elem: '#' + tableId,
            url: tburl,
            method: 'POST',
            contentType: 'application/json',
            where: { "otherId": GetTypeEnum(tableId) },
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
