layui.config({
    base: '../../lib/' //指定 winui 路径
    , version: '1.0.0-beta'
}).extend({
    winui: 'winui/winui',
    window: 'winui/js/winui.window',
    devindex: 'devextend/devindex',
    treeSelect: 'devextend/treeselect'
}).define(['table', 'winui', 'window', 'layer', 'devindex', 'treeSelect','laydate'], function (exports) {
    winui.renderColor();
   
    var table = layui.table,
        $ = layui.$,
        devindex = layui.devindex,
        treeSelect = layui.treeSelect,
        msg = winui.window.msg
        ,laydate=layui.laydate
        ,form = layui.form
        , tableId = 'useridtableid'
        , roleusertableId='roleusertableId'
        ;
    var $devId = wooutil.getUrlVar('Id');

    //角色用户列表---------------------begin-----------------------------------------------------
    var tburl=devsetter.devbaseurl+"api/DevUser/list";
    table.render({
        id: roleusertableId,
        elem: '#wooRoleuser',
        url:tburl,
        method:'POST',
         contentType:'application/json',
         where:{
            searchType:1,
            searchWhre:$devId

         },
        
        even:true,  //隔行变色
        page: true,
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
            { field: 'Email', title: '邮箱', width: 130 }
           
           
        ]]
    });
    $('#rolesuerreloadTable').on('click', roleuserreloadTable);
    //表格重载
    function roleuserreloadTable() {
        table.reload(roleusertableId, {
            where:{
                searchType:1,
                searchWhre:$devId
    
             }
        });
    }
    //删除角色用户
    $("#deleteroleuser").on('click',function(){
        var checkStatus = table.checkStatus(roleusertableId);
        var checkCount = checkStatus.data.length;
        if (checkCount < 1) {
            top.winui.window.msg('请选择数据', {
                time: 2000
            });
            return false;
        }
        var ids = [];
        $(checkStatus.data).each(function (index, item) {
            ids.push(item.Id);
        });
        $.ajax({
            type: 'GET',
            url: devsetter.devbaseurl + 'api/DevRole/deleteroleUser',
            data: { Ids: ids.toString(),RoleId:$devId },
            dataType: 'json',
            success: function (res) {
                top.winui.window.msg('操作成功', {
                    icon: 1,
                    time: 2000
                });
                roleuserreloadTable(); 

            },
            error: function (xml) {
                top.winui.window.msg('操作失败', {
                    time: 2000
                });

            }
        });




    });
   // 角色用户列表---------------end-------------------------------------------------------------
   //用户列表--------------------begin-----------------------------------------------------------
    var tburl=devsetter.devbaseurl+"api/DevUser/list";
    table.render({
        id: tableId,
        elem: '#woouser',
        url:tburl,
        method:'POST',
         contentType:'application/json',
        even:true,  //隔行变色
        page: true,
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
            { field: 'Email', title: '邮箱', width: 130 }
           
           
        ]]
    });
    $('#reloadTable').on('click', reloadTable);
    //表格重载
    function reloadTable() {
        table.reload(tableId, {});
    }
    //保存用户到角色
    $("#roleSaveUser").on('click',function(){
        var checkStatus = table.checkStatus(tableId);
        var checkCount = checkStatus.data.length;
        if (checkCount < 1) {
            top.winui.window.msg('请选择数据', {
                time: 2000
            });
            return false;
        }
        var ids = [];
        $(checkStatus.data).each(function (index, item) {
            ids.push(item.Id);
        });
        $.ajax({
            type: 'GET',
            url: devsetter.devbaseurl + 'api/DevRole/saveroleUser',
            data: { Ids: ids.toString(),RoleId:$devId },
            dataType: 'json',
            success: function (res) {
                top.winui.window.msg('操作成功', {
                    icon: 1,
                    time: 2000
                });
                roleuserreloadTable(); 

            },
            error: function (xml) {
                top.winui.window.msg('操作失败', {
                    time: 2000
                });

            }
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

//------------------------用户列表-end-----------------------------------------------------------------------------

   
   
    /**提交成功 */
    function submitsuccess(json) {
        if (json.Result) {
            msg('操作成功');
            closeWin();
           
        } else {
            msg(json.msg)
        }


    }
    /****
     * 修改时候赋值
     */
    function devSetValues() {

        if ($devId !== "" && $devId !== undefined) {
            $.ajax({
                type: 'GET',
                url: devsetter.devbaseurl + 'api/DevRole/showView',
                data: { Id: $devId },
                dataType: 'json',
                success: function (res) {
                    form.val("Dev-RoleForm", res.data);
                
                    $("Id").val(res.data.Id);
                },
                error: function (xml) {
                    msg('加载失败!');

                }
            });


        } 
    }
    //执行赋值表单
    devSetValues();
    form.render(null, 'Dev-RoleForm');




    exports('roleview', {});
});