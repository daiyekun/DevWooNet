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
                wooutil.devajax({
                    url: devsetter.devuserurl + 'api/DevAuth/accredit' //实际使用请改成服务端真实接口
                    ,data: JSON.stringify(postdata)
                    ,type: 'POST'
                    ,dataType: "json"
                    ,contentType: "application/json; charset=utf-8"
                    ,success: function(res){
                        if(res.tag==0){
                            layui.data(devsetter.devtableName,{key:devsetter.request.tokenName,value:res.data.token});//保存token
                            layui.data(devsetter.devtableName,{key:devsetter.request.loginname,value:res.data.loginUser.name});//登录名称
                            layui.data(devsetter.devtableName,{key:devsetter.request.showname,value:res.data.loginUser.showName});//显示名称
                            layui.data(devsetter.devtableName,{key:devsetter.request.loginkey,value:res.data.loginKey});//登录key
                            
                            //登入成功的提示与跳转
                            layer.msg('登入成功', {
                              offset: '15px'
                              ,icon: 1
                              ,time: 1000
                            }, function(){
                              window.location.href="/index.html";
                            });
                        }else if(res.tag==2){
                         msg("用户名或者密码错误");
            
                        }else if(res.tag==1){
                         msg("此用户不存在！");
                        }
                       
                      
                     
                    }
                    ,error: function (xml) {
                    msg('操作失败');
                    console.log(xml.responseText);
                    }
                  });

                //    $.ajax({
                //     type: 'POST',
                //     url: devsetter.devuserurl + 'api/DevAuth/accredit',
                //     //async: false,
                //     processData: false,
                //     data: JSON.stringify(postdata),
                //     dataType: "json",
                //     contentType: "application/json; charset=utf-8",
                //     success: function (json) {
                //         //debugger;
                //         if(json.tag==0){
                //             layui.data(devsetter.devtableName,{key:devsetter.request.tokenName,value:json.otherValue});//保存token
                //             window.location.href="/index.html";
                //         }else if(json.tag==2){
                //             msg("用户名或者密码错误");

                //         }else if(json.tag==1){
                //             msg("此用户不存在！");
                //         }
    
                //     },
                //     error: function (xml) {
                //         msg('操作失败');
                //         console.log(xml.responseText);
                //     }
                // });
            }
            return false;
        });

        
    

    




    exports('login2', {});
});