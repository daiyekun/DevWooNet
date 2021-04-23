layui.config({
    base: '../../lib/' //指定 winui 路径
    , version: '1.0.0-beta'
}).extend({
    winui: 'winui/winui',
    window: 'winui/js/winui.window',
    devindex: 'devextend/devindex'

}).define(['table', 'winui', 'window', 'layer', 'devindex', 'laydate'], function (exports) {
    winui.renderColor();
    var table = layui.table,
        $ = layui.$,
        devindex = layui.devindex,
        msg = winui.window.msg
        , laydate = layui.laydate
        , form = layui.form
        , tableId = 'useridtableid'
        ;
    var $devId = wooutil.getUrlVar('Id');//当前数据ID
    var $state = wooutil.getUrlVar('ustate');//当前数据状态
    function initview() {
        if ($state == 0) {
            $("button[tostate='1']").show();
            $("button[tostate='2']").hide();
            $("button[tostate='0']").hide();

        } else if ($state == 1) {
            $("button[tostate='1']").hide();
            $("button[tostate='2']").show();
            $("button[tostate='0']").show();
        }
    }
    initview();
    //点击状态切换
    $(".btn-toostate").on('click',function(e){
        var $upstate=$(this).attr('tostate');
      
        $.ajax({
            type: 'POST',
            url: devsetter.devuserurl + 'api/DevUser/updateState',
            data: JSON.stringify( { 
                Id: $devId,
                State:$upstate,
                CurrState:$state
            }),
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            success: function (res) {
                top.winui.window.close('win_userstate');
                top.winui.window.msg('修改成功', {
                    icon: 1,
                    time: 2000
                });
                parent.location.reload();

               // top.table.reload(tableId, {});
                
            },
            error: function (xml) {
                msg('操作失败!');

            }
        });

    });










    exports('selstate', {});
});