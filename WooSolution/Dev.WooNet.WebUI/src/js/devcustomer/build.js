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
        ,laydate=layui.laydate
        ,form = layui.form
        , tableId = 'useridtableid'
        ;
    var $devId = wooutil.getUrlVar('Id');
    var localdata=wooutil.devlocaldata();
  //日期
  laydate.render({
    elem: '#EntryDatetime'
  });
   
    //提交
    form.on('submit(Dev-SubmitSave)', function (data) {
        var postdata = data.field;
        //无赖之举目前没有更好办法，如果不这样制定一个int的id到后端API接收时对象为null
        postdata.Id = $devId > 0 ? $devId : 0;

        //表单验证
        if (winui.verifyForm(data.elem)) {
           wooutil.devajax({
            type: 'POST',
            url: devsetter.devuserurl + 'api/DevCompany/companySave',
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
   
    /**关闭窗体 */
    function closeWin() {
        if ($devId > 0) {
            top.winui.window.close('win_updatecustomer');
        } else {
            top.winui.window.close('win_addcustomer');

        }
    }
    /**提交成功 */
    function submitsuccess(json) {
        if (json.Result) {
            top.winui.window.msg('操作成功', {
                icon: 1
            },function(){
                closeWin();
                top.winui.window.tablelaod({id:'22'});
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
                url: devsetter.devuserurl + 'api/DevCompany/showView',
                //async: false,
                data: { Id: $devId },
                dataType: 'json',
                success: function (res) {
                    form.val("DEV-CustomerForm", res.data);
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
    form.render(null, 'DEV-CustomerForm');

/***********************联系人-begin***************************************************************************************************/
table.render({
       elem: '#Dev-CustomerContact'
       ,id:'Dev-CustomerContact'
      ,url:devsetter.devuserurl + 'api/DevCompContact/list?otherId='+$devId + '&rand=' + wooutil.getRandom()
      , toolbar: '#tooCustomerContact'
       , defaultToolbar: ['filter']
        ,method: 'POST'
        ,contentType: 'application/json'
        ,headers: {
        "Authorization": "Bearer "+ localdata.token +""
        ,loginkey:localdata.loginkey
        }
       , cols: [[
           { type: 'numbers', fixed: 'left' }
           ,{ type: 'checkbox', fixed: 'left' }
           , { field: 'Id', title: 'Id', width: 50, hide: true }
           , { field: 'Name', title: '姓名', width: 130, fixed: 'left' }
           , { field: 'Dname', title: '部门名称', width: 150 }
           , { field: 'RoleName', title: '职位', width: 120 }
           , { field: 'PhoneTel', title: '办公电话', width: 145 }
           , { field: 'PhoneNo', title: '移动电话', width: 130 }
           , { field: 'Email', title: 'E-mail', width: 130 }
           , { field: 'Fax', title: '传真', width: 130 }
           , { field: 'Qq', title: '其他联系方式', width: 140 }
           , { field: 'AddDateTime', title: '建立日期', width: 120, hide: true }
           , { field: 'AddUserName', title: '建立人', width: 120, hide: true }
           , { field: 'Remark', title: '备注', width: 140, hide: true }
           , { title: '操作', width: 150, align: 'center', fixed: 'right', toolbar: '#tableCustomerContacttbar' }
       ]]
       , page: false
       , loading: true
       , height:350
       , limit: 20
       ,done:function(res, curr, count){
        wooutil.devloginout(res);
       }
       

});
var contactEvent={
    winopen:function(winid,wintitle,url){
        top.winui.window.open({
            id: winid,
            type: 2,
            title: wintitle,
            content: url,
            area: ['50vw', '70vh'],
            offset: ['15vh', '25vw']
           
       
        });
    },
   reloadtable:function(){
    table.reload('Dev-CustomerContact', {});
   },
      //删除
      deletedata:function (ids, obj) {
        var msg = obj ? '确认删除数据【' + obj.data.Name + '】吗？' : '确认删除选中数据吗？'
        top.winui.window.confirm(msg, { icon: 3, title: '删除系统数据' }, function (index) {
            if(obj){
                layer.close(index);
            }else{
                top.layer.close(index);

            }
            //向服务端发送删除指令
            wooutil.devajax({
                type: 'GET',
                url: devsetter.devuserurl + 'api/DevCompContact/deletecontact',
                data: { Ids: ids.toString() },
                dataType: 'json',
                success: function (res) {
                    wooutil.devloginout(res);
                    //刷新表格
                    if (obj) {
                        top.winui.window.msg('删除成功', {
                            icon: 1,
                            time: 2000
                        });
                        obj.del(); //删除对应行（tr）的DOM结构
                    } else {
                        contactEvent.reloadtable();
                    }

                },
                error: function (xml) {
                    top.winui.window.msg('删除失败', {
                        // icon: 1,
                        time: 2000
                    });

                }
            });




        });
    }

};
//其他联系人头部工具栏
table.on('toolbar(Dev-CustomerContact)', function (obj) {
    switch (obj.event) {
        case 'add':
            contactEvent.winopen('win_addcontact','新增联系人','/views/devcustomer/contactbuild.html?CompId='+$devId);
            break;
        case 'batchdel':
            {
                var checkStatus = table.checkStatus('Dev-CustomerContact');
                var checkCount = checkStatus.data.length;
                if (checkCount < 1) {
                    top.winui.window.msg('请选择一条数据', {
                        time: 2000
                    });
                    return false;
                }
                var ids = [];
                $(checkStatus.data).each(function (index, item) {
                    ids.push(item.Id);
                });
                contactEvent. deletedata(ids);
            }
            break
        case 'LAYTABLE_COLS'://选择列-系统默认不管
            break;
        case 'reloadTable'://刷新
        contactEvent.reloadtable();

            break;
        default:
            layer.alert("暂不支持（" + obj.event + "）");
            break;

    };
});
//列表操作栏
table.on('tool(Dev-CustomerContact)', function (obj) {
    var _data = obj.data;
    switch (obj.event) {
        case 'del'://删除
        var ids = [];   
        $(_data).each(function (index, item) {
            ids.push(item.Id);
        });
            contactEvent.deletedata(ids,obj);
            break;
        case 'edit':
            contactEvent.winopen('win_updatecontact','修改联系人','/views/devcustomer/contactbuild.html?CompId='+$devId+'&Id='+_data.Id);
            break;
        default:
            layer.alert("暂不支持（" + obj.event + "）");
            break;
    }
});

/***********************联系人-end***************************************************************************************************/
 exports('customerbuild', {});
});