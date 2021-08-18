layui.config({
    base: '../../lib/' //指定 winui 路径
    , version: '1.0.0-beta'
}).extend({
    winui: 'winui/winui',
    window: 'winui/js/winui.window',
    devindex: 'devextend/devindex',
   
}).define(['table', 'winui', 'window', 'layer', 'devindex'], function (exports) {
    winui.renderColor();
   
    var table = layui.table,
        $ = layui.$,
        devindex = layui.devindex,
        treeSelect = layui.treeSelect,
        msg = winui.window.msg
        ,laydate=layui.laydate
        ,form = layui.form
       
        ;
    var $devId = wooutil.getUrlVar('Id');

   
   
   
    /****
     * 修改时候赋值
     */
    function devSetValues() {

        if ($devId !== "" && $devId !== undefined) {
            wooutil.devajax({
                type: 'GET',
                url: devsetter.devbaseurl + 'api/DevFlowGroup/showView',
                data: { Id: $devId },
                dataType: 'json',
                success: function (res) {
                    form.val("Dev-FlowGroupForm", res.data);
                
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
    form.render(null, 'Dev-FlowGroupForm');




    exports('flowgroupview', {});
});