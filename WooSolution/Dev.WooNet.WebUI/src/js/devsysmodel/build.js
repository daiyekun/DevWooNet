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
        ;
    var $devId = wooutil.getUrlVar('Id');
   
 
    //提交
    form.on('submit(dev-formAddSysModel)', function (data) {
        var postdata = data.field;
        //无赖之举目前没有更好办法，如果不这样制定一个int的id到后端API接收时对象为null
        if(postdata.Pid==""){
            postdata.Pid=0;
        }
        postdata.Id = $devId > 0 ? $devId : 0;
        postdata.id= $devId > 0 ? $devId : 0;
        postdata.pid=0;
        //表单验证
        if (winui.verifyForm(data.elem)) {
           
            $.ajax({
                type: 'POST',
                url: devsetter.devuserurl + 'api/DevSysModel/Save',
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
    /*********
    *上级菜单
     *********/
    
    function InitsysmodelTree(tvl) {
        treeSelect.render(
            {
                elem: "#Pid",
                data: devsetter.devuserurl+"api/DevSysModel/GetSelectTree?rand=" + wooutil.getRandom(),
                method: "GET",
                verify: true,
                click: function (d) {
                    
                    $("#pid").val(d.current.id);
                    $("#Pid").val(d.current.id);
                },
                success: function (d) {
                    if (tvl != null) {
                        treeSelect.checkNode("Pid", tvl);
                    }
                }
            });
    }
    /**关闭窗体 */
    function closeWin() {
        
        if ($devId > 0) {
            top.winui.window.close('win_updatesysmodel');
        } else {
            top.winui.window.close('win_addsysmodel');

        }
    }
    /**提交成功 */
    function submitsuccess(json) {
        if (json.Result) {
            top.winui.window.msg('操作成功', {
                icon: 1
            },function(){
                closeWin();
                top.winui.window.tablelaod({id:'2'});
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
            $.ajax({
                type: 'GET',
                url: devsetter.devuserurl + 'api/DevSysModel/showView',
                //async: false,
                data: { Id: $devId },
                dataType: 'json',
                success: function (res) {
                    form.val("DEV-SysMoelForm", res.data);
                    //下拉树（所属机构）,必须放到设置值以后，不然修改时设置不稳定

                   
                    InitsysmodelTree(res.data.Pid);
                    $("#Id").val(res.data.Id);
                   // $("#id").val(res.data.Id);
                },
                error: function (xml) {
                    msg('加载失败!');

                }
            });


        } else {
            InitsysmodelTree(null);
        }
    }
    //执行赋值表单
    devSetValues();
    form.render(null, 'DEV-SysMoelForm');
    exports('sysmodelbuild', {});
});