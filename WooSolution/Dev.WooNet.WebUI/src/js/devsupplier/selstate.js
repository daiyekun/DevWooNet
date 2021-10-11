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
        , tableId = 'thirdtableid'
        ;
    var $devId = wooutil.getUrlVar('Id');//当前数据ID
    var $state = wooutil.getUrlVar('ustate');//当前数据状态
    var $objtype = wooutil.getUrlVar('objtype');//0:客户，1：供应商，2合同对方
    var $objcateId= wooutil.getUrlVar('objcate');//对方类别ID
    var $dname= decodeURI(decodeURI(wooutil.getUrlVar('dname')));//名称
    var $dno= decodeURI(decodeURI(wooutil.getUrlVar('dno')));//编号
   
    function initview() {
        if ($state == 0) {//未审核
            $("button[flowitem='1']").show();
            $("button[flowitem='2']").hide();
            $("button[flowitem='3']").hide();

        } else if ($state == 1) {//审核通过
            $("button[flowitem='1']").hide();
            $("button[flowitem='2']").show();
            $("button[flowitem='3']").hide();
        }else if($state == 2){//已终止
            $("button[flowitem='1']").hide();
            $("button[flowitem='2']").hide();
            $("button[flowitem='3']").show();

        }
    }
    initview();
    //点击状态切换
    $(".btn-toostate").on('click',function(e){
        var $upstate=$(this).attr('tostate');
        var $flowitem=$(this).attr('flowitem');
        //获取以后再关闭
        top.winui.window.close('win_customerstate');
        var result=flowtool.submitflow({
            flowitem:$flowitem
            ,deptId: -1 //不传递后台获取登录人的部门ID
            , objType: $objtype//
            , objCateId: $objcateId
            ,objId:$devId
            ,objName:$dname//流程名称
            ,objamt:0//金额
            ,objNo:$dno//编号

        });
        if(result==-1){//没有匹配上流程
            layer.confirm('没有流程模板符合，是否直接修改状态？', { icon: 3, title: '提示信息' }, function (cfindex) {
                //直接修改状态
                layer.close(cfindex);
            });

        }

       
      
        // wooutil.devajax({
        //     type: 'POST',
        //     url: devsetter.devuserurl + 'api/DevUser/updateState',
        //     data: JSON.stringify( { 
        //         Id: $devId,
        //         State:$upstate,
        //         CurrState:$state
        //     }),
        //     dataType: 'json',
        //     contentType: "application/json; charset=utf-8",
        //     success: function (res) {
                
        //         top.winui.window.msg('修改成功', {
        //             icon: 1,
        //             time: 2000
        //         });
               
        //         top.winui.window.close('win_customerstate');
               
               
              
                
        //     },
        //     error: function (xml) {
        //         msg('操作失败!');

        //     }
        // });

    });










    exports('selstate', {});
});