layui.config({
    base: '../../lib/' //指定 winui 路径
    , version: '1.0.0-beta'
}).extend({
    winui: 'winui/winui',
    window: 'winui/js/winui.window',
    devindex: 'devextend/devindex'
}).define(['table', 'winui', 'window', 'layer', 'devindex'], function (exports) {
    winui.renderColor();
    var table = layui.table,
        $ = layui.$,
        devindex = layui.devindex,
        msg = winui.window.msg
        ,form = layui.form
        , tableId = 'roleidtableid'
        ;
    var $devId = wooutil.getUrlVar('Id');
   
 
   
    //提交
    form.on('submit(dev-formAddrole)', function (data) {

        var postdata = data.field;
        //无赖之举目前没有更好办法，如果不这样制定一个int的id到后端API接收时对象为null
        postdata.Id = $devId > 0 ? $devId : 0;
        //表单验证
        if (winui.verifyForm(data.elem)) {
            $.ajax({
                type: 'POST',
                url: devsetter.devbaseurl + 'api/DevRole/roleSave',
                //async: false,
                processData: false,
                data: JSON.stringify(postdata),
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (json) {
                    submitsuccess(json);

                },
                error: function (xml) {
                    msg('操作失败');
                    console.log(xml.responseText);
                }
            });
        }
        return false;
    });
    //取消
    $("#quxiaobtn").click(function () {
        closeWin();
        return false;
    });
    /**关闭窗体 */
    function closeWin() {
        if ($devId > 0) {
            top.winui.window.close('win_updaterole');
        } else {
            top.winui.window.close('win_addrole');
        }
    }
    /**提交成功 */
    function submitsuccess(json) {
        if (json.Result) {
            msg('操作成功');
            closeWin();
            top.winui.window.tablelaod({id:'16'});
            //parent.table.reload(tableId, {});
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
                //async: false,
                data: { Id: $devId },
                dataType: 'json',
                success: function (res) {
                    form.val("Dev-RoleForm", res.data);
                    //下拉树（所属机构）,必须放到设置值以后，不然修改时设置不稳定

                   
                   
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
    exports('rolebuild', {});
});