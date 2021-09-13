layui.config({
    base: '../../lib/' //指定 winui 路径
    , version: '1.0.0-beta'
}).extend({
    winui: 'winui/winui',
    window: 'winui/js/winui.window',
    devindex: 'devextend/devindex',
   
}).define(['table', 'winui', 'window', 'layer', 'devindex','tree','util'], function (exports) {
    winui.renderColor();
   
    var table = layui.table,
        $ = layui.$,
        devindex = layui.devindex,
        msg = winui.window.msg
        ,form = layui.form
        ,tree=layui.tree
        ,util=layui.util
        ,tableId='functiontableId'
         ;
         //角色ID
    var roleId = wooutil.getUrlVar('Id');
        //本地缓存
     var localdata=wooutil.devlocaldata();
    // var $devId = wooutil.getUrlVar('Id');
     /****
      * 权限列表
      ****/
      function initFuncTable(mId){
        var tburl=devsetter.devbaseurl+"api/DevPermissionSet/getfunctionlist";
      
        var woocustomer=table.render({
            id: tableId,
            elem: '#woomodelList',
            url:tburl,
            method:'POST',
            contentType:'application/json',
            toolbar:false,
            //defaultToolbar: ["filter"],
            even:true,  //隔行变色
            page: true,
            headers: {
                "Authorization": "Bearer "+ localdata.token +""
                ,loginkey:localdata.loginkey
            },
            where:{
                otherId:mId

            },
           
            limits:devsetter.listtable.mainlistlimits,
            limit: 40,//devsetter.listtable.mainlistlimit,
            rowDrag: {
                numbers: false
              },
               filter: {
                cache: true
               , bottom: false
            },
            cols: [[
                { type: 'numbers', fixed: 'left' },
                { field: 'Name', title: '权限名称',fixed: 'left', width: 260 },
                { field: 'FunType', title: '权限设置', width:500,templet:'#functionLx'}
            ]],
            done:function(res, curr, count){
              
                wooutil.devloginout(res);
                active.roleFunction(mId);
    
                
    
            }
        });
     }
    //菜单
    function inittree(treedata){
      //仅节点左侧图标控制收缩
      tree.render({
        elem: '#devsysmodeltree'
       ,data: treedata
       ,onlyIconControl: true  //是否仅允许节点左侧图标控制展开收缩
       ,click: function(obj){
         //layer.msg(JSON.stringify(obj.data));
          initFuncTable(obj.data.id);
          }
        });
     }
     

     wooutil.devajax({
        type: 'GET',
        url: devsetter.devuserurl + 'api/DevSysModel/getlayuitree',
        dataType: "json",
        success: function (res) {
            inittree(res.data);
        },
        error: function (res) {}

       });

       //选择机构
       form.on('radio(showdepttree)', function (data)
       {
           //功能ID
           var funcId = this.getAttribute("funcId");
          
           layer.open({
               type: 2
               , title: '选择当前角色操作数据部门'
               , content: '/views/devrole/seldepttree.html?funcId=' + funcId + '&roleId=' + roleId + '&rand=' + wooutil.getRandom()
               , area: ['60%', '98%']
               , maxmin: true
               , btnAlign: 'c'
               , btn: ['确定', '取消']
               , yes: function (index, layero) {
                   var iframeWindow = window['layui-layer-iframe' + index]
                       , submitID = 'Dev-submitseldepart'
                       , submit = layero.find('iframe').contents().find('#' + submitID);
                   //监听提交
                   iframeWindow.layui.form.on('submit(' + submitID + ')', function (data) {
                       var field = data.field; //获取提交的字段{deptIds: "2,29,30", funcId: "1", Id: "17", setType: "1"}
                       // wooutil.SaveForm(null, "/System/UserInfor/AllotSysModels", "", field, '操作成功！', index);
                      debugger;
                       $("input[name=modeIds_" + funcId + "]").val(field.deptIds);
                       layer.close(index); 
                       return false;

                   });
                   submit.trigger('click');
                   
               },
               success: function (layero, index) {
                  
                   

               }

           });
       });
       //事件
       var active = {
        savepermssion: function () {

            var postdata = [];
            $('input:radio:checked').each(function (_index, element) {
                var _funcId = $(this).attr("funcId");
                var funccode = $(this).attr("funccode");
                var modeId = $(this).attr("modelId");
                postdata.push({
                    "Id":0,
                    "RoleId": roleId,
                    "FuncId": _funcId,
                    "FuncCode": funccode,
                    "FuncType": $(this).val(),
                    "DeptIds":$(this).val() !== 2 && $(this).val() !=="2"?0:$("input[name=modeIds_" + _funcId + "]").val(),
                    "Mid": modeId
                    
                });
                
                //console.log($(this).val() + ">" + funcId + ">" + funccode + ">" + modeId);
              
            });
            // $.post('/System/Role/AllotFuncPermssion', { rolePermissions: postdata}, function (res)
            // {
            //     layer.msg("操作成功！");
            // });
           
            var savedata= { rolePermissions: postdata};
            wooutil.devajax({
                type: 'POST',
                url: devsetter.devuserurl + 'api/DevPermissionSet/saverolepermssion',
                data: JSON.stringify(postdata),
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (res) {
                    wooutil.devloginout(res);
                    top.winui.window.msg('操作成功', {
                        icon: 1
                    });
                    
    
                },
                error: function (xml) {
                    msg('操作失败');
                    console.log(xml.responseText);
                }
    
               });

        },
        /**
 * 角色权限树
 * @param {any} admin
 * @param {any} $
 * @param {any} item
 * @param {any} form
 */
 roleFunction:function( mid) {
    setTimeout(
        function () {
            wooutil.devajax({
                url:  devsetter.devuserurl +'api/DevRole/GetRolePermission?rand=' + wooutil.getRandom(),
                data: { roleId: roleId, modeId: mid },
                 success: function (res) {
                    $.each(res.data, function (n, v) {
                        $("input:radio[funcid=" + v.FuncId + "][value=" + v.FuncType + "]").eq(0).attr("checked", "checked");
                        if (v.FuncType == "2") { //如果是机构
                            $("input[name=modeIds_" + v.FuncId + "]").val(v.DeptIds);
                        }

                    });
                    form.render();
                }
            })
        }
    , 1000);
        }
 };

    $("#saverolepermssion").click(function(){

        active.savepermssion();
    });

    exports('setmodelfunction', {});
});