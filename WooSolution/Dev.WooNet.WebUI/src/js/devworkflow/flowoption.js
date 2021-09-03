layui.config({
    base: '../../lib/' //指定 winui 路径
    , version: '1.0.0-beta'
}).extend({
    winui: 'winui/winui',
    window: 'winui/js/winui.window',
    devindex: 'devextend/devindex',
    treeSelect: 'devextend/treeselect'
}).define(['table', 'winui', 'window', 'layer', 'devindex'], function (exports) {
    winui.renderColor();
    var table = layui.table,
        $ = layui.$,
        devindex = layui.devindex,
        msg = winui.window.msg
        ,form = layui.form
        , tableId = 'useridtableid'
        ;
    var objtype = wooutil.getUrlVar('objtype');//审批对象类型
    var objId = wooutil.getUrlVar('objId');//审批对象ID
    var instId = wooutil.getUrlVar('instId');//审批实例ID
    var prefix=wooutil.getUrlVar('prefix');//前缀
    var ObjMoney=wooutil.getUrlVar('objmoney');//金额
      
    /**
     * 关闭窗口
     */
    function closeflowwin(){
        switch(objtype){
            case 0://合同
                top.winui.window.close('win_viewcustomer');
                break;

        }
        top.winui.window.close(prefix+'-win-option');
        
    }
    /*
    *提交意见
     */
    function submitoption(url,postdata){
        wooutil.devajax({
            type: 'POST',
            url: url,
            data: JSON.stringify(postdata),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (res) {
                wooutil.devloginout(res);
                top.winui.window.msg('操作成功', {
                    icon: 1
                },function(){
                   closeflowwin();
                    
                });

            },
            error: function (xml) {
                msg('操作失败');
                console.log(xml.responseText);
            }

           });
    }

    $("#dev-Agree-option").click(function(e){//同意
        var msg ='您的审批意见【同意】';
        var option=$('#flowoption').val();
        if(option==""){
            top.winui.window.msg('请填写意见', {
                icon: 5
            },function(){
                
            });

        }else{
        top.winui.window.confirm(msg, { icon: 3, title: '审批提示' }, function (index) {
            //向服务端发送删除指令
            debugger;
            var postdata={};
            postdata.InstId=instId;
            postdata.ObjType=objtype;
            postdata.ObjId=objId;
            postdata.ObjMoney=ObjMoney;
            postdata.Option=option;
            postdata.OptRes=1;//1：同意
            postdata.SubmitUserId=0;
            var url=devsetter.devuserurl + 'api/DevAppInstOption/SubmitAgree';
            submitoption(url,postdata);
        });
    }


    });
    $("#dev-NoAgree-option").click(function(e){//不同意
        var msg ='您的审批意见【不同意】'
        top.winui.window.confirm(msg, { icon: 3, title: '审批提示' }, function (index) {
             //向服务端发送删除指令
             var postdata={};
             postdata.InstId=instId;
             postdata.ObjType=objtype;
             postdata.ObjId=objId;
             postdata.ObjMoney=ObjMoney;
             postdata.Option=option;
             postdata.OptRes=2;//1：同意
             postdata.SubmitUserId=0;
             var url=devsetter.devuserurl + 'api/DevAppInstOption/SubmitDisagree';
             submitoption(url,postdata);
        });


    });

    //取消
    $("#quxiaobtn").click(function () {
        top.winui.window.close(prefix+'-win-option');
        return false;
    });
   
   
    /**提交成功 */
    function submitsuccess(json) {
        if (json.Result) {
            top.winui.window.msg('操作成功', {
                icon: 1
            },function(){
                closeWin();
                top.winui.window.tablelaod({id:'15'});
            });
           
            
            
        } else {
            msg(json.msg)
        }


    }
    
 
    form.render(null, 'DEV-UserForm');
    exports('flowoption', {});
});