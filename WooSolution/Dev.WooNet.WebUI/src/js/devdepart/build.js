layui.config({
        base: '../../lib/' //指定 winui 路径
       , version: '1.0.0-beta'
   }).extend({
       winui: 'winui/winui',
       window: 'winui/js/winui.window',
       //devsetter:'devextend/devsetter',//由于devindex 引入就不需要了。直接使用即可，否则出现类名被占
       devindex:'devextend/devindex',
       treeSelect:'devextend/treeselect'
   }).define(['table', 'winui', 'window', 'layer','devindex','treeSelect'], function (exports) {
    winui.renderColor();
    var table = layui.table,
        $ = layui.$,
        devindex=layui.devindex,
       
        treeSelect = layui.treeSelect,
        msg = winui.window.msg
        form = layui.form
        ,tableId='depttableid'
        ;
        var $devId = wooutil.getUrlVar('Id');
       // form.render();
       //签约主体
        form.on('checkbox(IsMainCheck)', function (data) {
            console.log("点击了");
            if (data.elem.checked) {
                $("div .deptMain").show();

            } else {
                $("div .deptMain").hide();

            }

        });
        //提交
        form.on('submit(dev-formAddDepart)', function (data) {
        
          var postdata= data.field;
          //无赖之举目前没有更好办法，如果不这样制定一个int的id到后端API接收时对象为null
          postdata.Id=$devId>0?$devId:0;
         
            //表单验证
            if (winui.verifyForm(data.elem)) {
                $.ajax({
                    type: 'POST',
                    url: devsetter.devuserurl+'api/DevDepart/departSave',
                    //async: false,
                    processData: false,
                    data:JSON.stringify(postdata),
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
        $("#quxiaobtn").click(function(){
            closeWin();
            return false;
        });
        /*********
        *所属部门树形初始化
         *********/
      function InitDeptTree(tvl) {
            treeSelect.render(
                {
                    elem: "#NF-PDept",
                    data: devsetter.devuserurl+'api/DevDepart/GetTree',
                    method: "GET",
                    verify: true,
                    click: function (d) {
                        $("input[name=Pid]").val(d.current.id);
                    },
                    success: function (d) {
                        if (tvl != null) {
                            treeSelect.checkNode("NF-PDept", tvl);
                        }
                    }
                });
        }
        /**关闭窗体 */
        function closeWin(){
            if($devId>0){
                top. winui.window.close('win_updatedept');
            }else{
                top. winui.window.close('win_adddept');
            }
        }
        /**提交成功 */
        function submitsuccess(json){
            if (json.Result) {
                msg('操作成功');
                closeWin();
                //parent.table.reload(tableId, {});
            } else {
                msg(json.msg)
            }
            
           
        }
        /****
         * 修改时候赋值
         */
        function devSetValues(){
            
        if ($devId !== "" && $devId !== undefined) {
            $.ajax({
                type: 'GET',
                url:devsetter.devuserurl+'api/DevDepart/ShowValues',
                //async: false,
                data: {Id:$devId},
                dataType: 'json',
                success: function (res) {
                    form.val("DEV-DeptForm", res.data);
                    //下拉树（所属机构）,必须放到设置值以后，不然修改时设置不稳定

                    if (res.data.IsMain === 1) {
                        $("input[name=IsMain]").attr("checked", true);
                        $("div .deptMain").show();

                    }
                    if (res.data.IsSubCompany === 1) {
                        $("input[name=IsCompany]").attr("checked", true);
                    }
                  
                   InitDeptTree(res.data.Pid);
                   $("Id").val(res.data.Id);
                },
                error: function (xml) {
                    msg('加载失败!');
                   
                }
            });
            

        } else {
            InitDeptTree(null);
        }
    }
    wooutil.getdatadic({ dataenum: 0, selectEl: "#CateId" });
    //执行赋值表单
    devSetValues();
    form.render(null, 'DEV-DeptForm');
    exports('departbuild', {});
});