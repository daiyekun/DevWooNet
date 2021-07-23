/**
 * 客户联系人
 */
layui.config({
    base: '../../../lib/' //指定 winui 路径
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
        treeSelect = layui.treeSelect,
        msg = winui.window.msg
        ,laydate=layui.laydate
        ,form = layui.form
        , tableId = 'useridtableid'
        ;
    var $devId = wooutil.getUrlVar('Id');
    var CompId=wooutil.getUrlVar('CompId');
    $("#CompId").val(CompId);
    
    //提交
    form.on('submit(dev-formSaveContact)', function (data) {
        var postdata = data.field;
        //无赖之举目前没有更好办法，如果不这样制定一个int的id到后端API接收时对象为null
        postdata.Id = $devId > 0 ? $devId : 0;
        //表单验证
        if (winui.verifyForm(data.elem)) {
            wooutil.devajax({
                type: 'POST',
                url: devsetter.devuserurl + 'api/DevCompContact/custcontactsave',
                //async: false,
                processData: false,
                data: JSON.stringify(postdata),
                dataType: "json",
                contentType: "application/json;charset=utf-8",
                success: function (res) {
                    wooutil.devloginout(res);
                    submitsuccess(res);

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
            top.winui.window.close('win_updatecontact');
        } else {
            top.winui.window.close('win_addcontact');

        }
    }
    /**提交成功 */
    function submitsuccess(json) {
        if (json.Result) {
            top.winui.window.msg('操作成功', {
                icon: 1
            },function(){
                closeWin();
                top.winui.window.devtablelaod({id:'win_addcustomer',resbtn:'custContactrefresh'});
            });
           
            
            
        } else {
            msg(json.msg)
        }


    }
    /****
     * 修改时候赋值
     */
    function devSetValues() {

        if ($devId !== "" && $devId !== undefined) {
            wooutil.devajax({
                type: 'GET',
                url: devsetter.devuserurl + 'api/DevCompContact/showView',
                //async: false,
                data: { Id: $devId },
                dataType: 'json',
                success: function (res) {
                    wooutil.devloginout(res);
                    form.val("DEV-customercontactForm", res.data);
                   
                    $("#Id").val(res.data.Id);
                },
                error: function (xml) {
                    msg('加载失败!');

                }
            });


        } else {
           
        }
    }
    //执行赋值表单
    devSetValues();
    form.render(null, 'DEV-customercontactForm');
    exports('contactbuild', {});
});