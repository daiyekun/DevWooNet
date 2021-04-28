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
        treeSelect = layui.treeSelect,
        msg = winui.window.msg
        ,laydate=layui.laydate
        ,form = layui.form
        ;

        form.on('submit(btn-login)', function (data) {

            var postdata = data.field;
         
            //表单验证
            if (winui.verifyForm(data.elem)) {
                $.ajax({
                    type: 'POST',
                    url: devsetter.devuserurl + 'api/DevAuth/accredit',
                    //async: false,
                    processData: false,
                    data: JSON.stringify(postdata),
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    success: function (json) {
                        //debugger;
                        if(json.tag==0){
                            layui.data("devToken", json.otherValue);//保存token
                            window.location.href="/index.html";
                        }else if(json.tag==2){
                            msg("用户名或者密码错误");

                        }else if(json.tag==1){
                            msg("此用户不存在！");
                        }
    
                    },
                    error: function (xml) {
                        msg('操作失败');
                        console.log(xml.responseText);
                    }
                });
            }
            return false;
        });

        
    

    




    exports('login', {});
});