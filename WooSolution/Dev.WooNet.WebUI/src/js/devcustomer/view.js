layui.config({
    base: '../../lib/' //指定 winui 路径
    , version: '1.0.0-beta'
}).extend({
    winui: 'winui/winui',
    window: 'winui/js/winui.window',
    devindex: 'devextend/devindex',
    address: 'devextend/address',
    tableSelect: 'devextend/tableSelect',
    devselitem: 'devextend/devselitem',
    devviewedit: 'devextend/devviewedit/devviewedit',
    flowapplist:'devextend/flowutility/flowapplist'
  
   
}).define(['table', 'winui', 'window', 'layer', 'devindex', 'laydate','address','tableSelect','devselitem','flowapplist','devviewedit'], function (exports) {
    winui.renderColor();
    var table = layui.table,
        $ = layui.$,
        devindex = layui.devindex,
        msg = winui.window.msg
        ,laydate=layui.laydate
        ,form = layui.form
        ,address=layui.address()
        ,tableSelect=layui.tableSelect
        ,devselitem=layui.devselitem
        ,flowapplist=layui.flowapplist
        ,devviewedit=layui.devviewedit
        , tableId = 'useridtableid'
        ,appflowdata=null;
        ;
    var $devId = wooutil.getUrlVar('Id');
    var localdata=wooutil.devlocaldata();

/***********************基本信息-begin***************************************************************************************************/
 /**
  * 清除-userId问题
  * */ 
function cleardata(){
    wooutil.devajax({
        type: 'GET',
        url: devsetter.devbaseurl + 'api/DevCompany/cleardata',
        // data: JSON.stringify(postdata),
        dataType: "json",
        success: function (res) {
            

        },
        error: function (res) {
           
            
        }

       });

     }
     cleardata();
   
    //提交
    form.on('submit(Dev-SubmitSave)', function (data) {
        var postdata = data.field;
        //无赖之举目前没有更好办法，如果不这样制定一个int的id到后端API接收时对象为null
        postdata.Id = $devId > 0 ? $devId : 0;

        //表单验证
        if (winui.verifyForm(data.elem)) {
           wooutil.devajax({
            type: 'POST',
            url: devsetter.devbaseurl + 'api/DevCompany/companySave',
            data: JSON.stringify(postdata),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (res) {
                wooutil.devloginout(res);
                submitsuccess(res);

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
            top.winui.window.close('win_viewcustomer');
        } else {
            top.winui.window.close('win_addcustomer');

        }
    }
    /**提交成功 */
    function submitsuccess(json) {
        if (json.code==0) {
            top.winui.window.msg('操作成功', {
                icon: 1
            },function(){
                closeWin();
                top.winui.window.tablelaod({id:'22'});
            });
 
        }
        else {
            msg(json.msg)
        }


    }
    /****
     * 修改时候赋值
     */
    function devSetValues() {

        if ($devId !== "" && $devId !== undefined) {
            wooutil.devajax({
                type: 'GET',
                url: devsetter.devbaseurl + 'api/DevCompany/showView',
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
    
    // //客户类别
    //  wooutil.getdatadic({ dataenum: 3, selectEl: "#CompClassId" });
    //  //单位级别
    //  wooutil.getdatadic({ dataenum: 5, selectEl: "#LevelId" });
    //  //信用等级
    //  wooutil.getdatadic({ dataenum: 6, selectEl: "#CareditId" });
    // //成立日期
    //  laydate.render({ elem: '#EsDateTime', trigger: 'click' });
    // //证件有效期
    //  laydate.render({ elem: '#ExpDateTime', trigger: 'click' });
     wooutil.selverpen();//下拉小笔头
     //负责人
    //  devselitem.selectUserItem(
    //     {
    //         tableSelect: tableSelect,
    //         elem: '#FaceUserName',
    //         hide_elem: '#FaceUserId'

    //     });
    //执行赋值表单
    devSetValues();
    form.render(null, 'DEV-CustomerForm');
/***********************基本信息-end***************************************************************************************************/

    
/***********************附件-begin***************************************************************************************************/
table.render({
    elem: '#Dev-CustomerFiles'
    ,id:'Dev-CustomerFiles'
   ,url:devsetter.devbaseurl + 'api/DevCompFile/list?otherId='+$devId+'&rand=' + wooutil.getRandom()
   , toolbar: '#toolCustomerFiles'
    , defaultToolbar: ['filter']
     ,method: 'POST'
     ,contentType: 'application/json'
     ,headers: {
     "Authorization": "Bearer "+ localdata.token +""
     ,loginkey:localdata.loginkey
     },
     where:{
        otherId:$devId
     }
    , cols: [[
        { type: 'numbers', fixed: 'left' }
        ,{ type: 'checkbox', fixed: 'left' }
        , { field: 'Name', title: '附件名称', width: 200, fixed: 'left' }
        , { field: 'FileClassName', title: '文件类别', width: 140}
        , { field: 'FileName', title: '文件名称', width: 200}
        , { field: 'Remark', title: '说明', width: 400 }
        , { field: 'AddDateTime', title: '建立日期', width: 120, hide: true }
        , { field: 'AddUserName', title: '建立人', width: 120, hide: true }
        , { field: 'Id', title: 'Id', width: 50, hide: true }
        , { title: '操作', width: 200, align: 'center', fixed: 'right', toolbar: '#tableCustomerFilesbar' }
    ]]
    , page: false
    , loading: true
    , height:350
    , limit: 20
    ,done:function(res, curr, count){
     wooutil.devloginout(res);
    }
    

});
var fileEvent={
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
 table.reload('Dev-CustomerFiles', {});
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
             url: devsetter.devbaseurl + 'api/DevCompFile/delete',
             data: { Ids: ids.toString() },
             dataType: 'json',
             success: function (res) {
                 wooutil.devloginout(res);
                 //刷新表格
                 if (obj) {
                     top.winui.window.msg('删除成功', {
                         icon: 1,
                         time: 1000
                     });
                     obj.del(); //删除对应行（tr）的DOM结构
                 } else {
                    fileEvent.reloadtable();
                 }

             },
             error: function (xml) {
                 top.winui.window.msg('删除失败', {
                     // icon: 1,
                     time: 1000
                 });

             }
         });




     });
 },
 tooldownload: function (obj) {

    wooutil.download({
        url: devsetter.devbaseurl + 'api/DevFileCommon/download',
        Id: obj.data.Id,
        folder: 0//标识客户附件


    });
},
devbrowse:function(){//预览
    var checkStatus = table.checkStatus("Dev-CustomerFiles")
                , checkData = checkStatus.data; //得到选中的数据
            if (checkData.length === 0) {
                return layer.msg('请选择数据');
            } else {
                if (checkData[0].Extension.toLowerCase().indexOf("pdf")>=0) {//pdf
                    var fileurl = layui.cache.base + 'pdfjs/web/viewer.html?file=' +
                    encodeURIComponent(devsetter.devupload.uploadIp+'api/DevCompFile/getpdf?Id=' + checkData[0].Id+'&Folder=0');
                    top.winui.window.open({
                        id: 'win_breaw'+checkData[0].Id,
                        type: 2,
                        title: '客户文件预览',
                        content: fileurl,
                        area: ['50vw', '70vh'],
                        offset: ['15vh', '25vw']
                       
                   
                    });


            }else if (checkData[0].Extension.toLowerCase().indexOf("png") >= 0
            || checkData[0].Extension.toLowerCase().indexOf("jpg") >= 0
            || checkData[0].Extension.toLowerCase().indexOf("jpeg") >= 0
            || checkData[0].Extension.toLowerCase().indexOf("bpm") >= 0
            || checkData[0].Extension.toLowerCase().indexOf("tif") >= 0
            || checkData[0].Extension.toLowerCase().indexOf("gif") >= 0
            || checkData[0].Extension.toLowerCase().indexOf("svg") >= 0)
              {//图片
                var pcurl = "/views/devcustomer/pictureview.html?CompId=" + checkData[0].CompId;
                top.winui.window.open({
                    id: 'win_breaw'+checkData[0].CompId,
                    type: 2,
                    title: '客户图片预览',
                    content: pcurl,
                    area: ['50vw', '70vh'],
                    offset: ['15vh', '25vw']
                   
               
                });

              }
              else if (checkData[0].Extension.toLowerCase().indexOf("docx") >= 0) 
              {
                  //word
                  var fileurl = layui.cache.base + 'pdfjs/web/viewer.html?file=' +
                  encodeURIComponent(devsetter.devupload.uploadIp+'api/DevCompFile/wordtopdfview?Id=' + checkData[0].Id+'&Folder=0');
                  top.winui.window.open({
                      id: 'win_breaw'+checkData[0].Id,
                      type: 2,
                      title: '客户文件预览',
                      content: fileurl,
                      area: ['50vw', '70vh'],
                      offset: ['15vh', '25vw']
                     
                 
                  });


              }else{
                return layer.msg('只支持PDF、dodx、png、jpg、jpeg、bpm、tif、gif、svg预览', { icon: 5 });
              }
        }

}

};
//头部工具栏
table.on('toolbar(Dev-CustomerFiles)', function (obj) {
 switch (obj.event) {
     case 'add':
        fileEvent.winopen('win_addcustfile','新增附件','/views/devcustomer/compfilebuild.html?CompId='+$devId+'&Ctype=0');
         break;
     case 'batchdel':
         {
             var checkStatus = table.checkStatus('Dev-CustomerFiles');
             var checkCount = checkStatus.data.length;
             if (checkCount < 1) {
                 top.winui.window.msg('请选择一条数据', {
                     time: 1000
                 });
                 return false;
             }
             var ids = [];
             $(checkStatus.data).each(function (index, item) {
                 ids.push(item.Id);
             });
             fileEvent.deletedata(ids);
         }
         break
     case 'LAYTABLE_COLS'://选择列-系统默认不管
         break;
     case 'reloadTable'://刷新
     fileEvent.reloadtable();
        break;
    case  'devbrowse'://浏览
    fileEvent.devbrowse();
         break;
     default:
         layer.alert("暂不支持（" + obj.event + "）");
         break;

 };
});
//列表操作栏
table.on('tool(Dev-CustomerFiles)', function (obj) {
 var _data = obj.data;
 switch (obj.event) {
     case 'del'://删除
     var ids = [];   
     $(_data).each(function (index, item) {
         ids.push(item.Id);
     });
     fileEvent.deletedata(ids,obj);
         break;
     case 'edit':
        fileEvent.winopen('win_updatecustfile','修改附件','/views/devcustomer/compfilebuild.html?CompId='+$devId+'&Id='+_data.Id);
         break;
     case 'download'://下载
     fileEvent.tooldownload(obj);
         break;
     default:
         layer.alert("暂不支持（" + obj.event + "）");
         break;
 }
});

/***********************附件-end***************************************************************************************************/


/***********************联系人-begin***************************************************************************************************/
table.render({
       elem: '#Dev-CustomerContact'
       ,id:'Dev-CustomerContact'
      ,url:devsetter.devbaseurl + 'api/DevCompContact/list?rand=' + wooutil.getRandom()
      , toolbar: '#tooCustomerContact'
       , defaultToolbar: ['filter']
        ,method: 'POST'
        ,contentType: 'application/json'
        ,headers: {
        "Authorization": "Bearer "+ localdata.token +""
        ,loginkey:localdata.loginkey
        },
        where:{
            otherId:$devId
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
                url: devsetter.devbaseurl + 'api/DevCompContact/deletecontact',
                data: { Ids: ids.toString() },
                dataType: 'json',
                success: function (res) {
                    wooutil.devloginout(res);
                    //刷新表格
                    if (obj) {
                        top.winui.window.msg('删除成功', {
                            icon: 1,
                            time: 1000
                        });
                        obj.del(); //删除对应行（tr）的DOM结构
                    } else {
                        contactEvent.reloadtable();
                    }

                },
                error: function (xml) {
                    top.winui.window.msg('删除失败', {
                        // icon: 1,
                        time: 1000
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
                        time: 1000
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
 

/***********************备忘录-begin***************************************************************************************************/
table.render({
    elem: '#Dev-CustomerDesc'
    ,id:'Dev-CustomerDesc'
   ,url:devsetter.devbaseurl + 'api/DevCompDesc/list?rand=' + wooutil.getRandom()
   , toolbar: '#toolCustomerDesc'
    , defaultToolbar: ['filter']
     ,method: 'POST'
     ,contentType: 'application/json'
     ,headers: {
     "Authorization": "Bearer "+ localdata.token +""
     ,loginkey:localdata.loginkey
     },
     where:{
        otherId:$devId
     }
    , cols: [[
        { type: 'numbers', fixed: 'left' }
        ,{ type: 'checkbox', fixed: 'left' }
        , { field: 'Id', title: 'Id', width: 50, hide: true }
        , { field: 'Item', title: '事项', width: 150, fixed: 'left' }
        , { field: 'Remark', title: '说明', width: 500 }
        , { field: 'AddDateTime', title: '建立日期', width: 120, hide: true }
        , { field: 'AddUserName', title: '建立人', width: 120, hide: true }
        , { title: '操作', width: 150, align: 'center', fixed: 'right', toolbar: '#tablecustomerDescbar' }
    ]]
    , page: false
    , loading: true
    , height:350
    , limit: 20
    ,done:function(res, curr, count){
     wooutil.devloginout(res);
    }
    

});
var descEvent={
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
 table.reload('Dev-CustomerDesc', {});
},
   //删除
   deletedata:function (ids, obj) {
     var msg = obj ? '确认删除数据【' + obj.data.Item + '】吗？' : '确认删除选中数据吗？'
     top.winui.window.confirm(msg, { icon: 3, title: '删除系统数据' }, function (index) {
         if(obj){
             layer.close(index);
         }else{
             top.layer.close(index);

         }
         //向服务端发送删除指令
         wooutil.devajax({
             type: 'GET',
             url: devsetter.devbaseurl + 'api/DevCompDesc/delete',
             data: { Ids: ids.toString() },
             dataType: 'json',
             success: function (res) {
                 wooutil.devloginout(res);
                 //刷新表格
                 if (obj) {
                     top.winui.window.msg('删除成功', {
                         icon: 1,
                         time: 1000
                     });
                     obj.del(); //删除对应行（tr）的DOM结构
                 } else {
                    descEvent.reloadtable();
                 }

             },
             error: function (xml) {
                 top.winui.window.msg('删除失败', {
                     // icon: 1,
                     time: 1000
                 });

             }
         });




     });
 }

};
//头部工具栏
table.on('toolbar(Dev-CustomerDesc)', function (obj) {
 switch (obj.event) {
     case 'add':
        descEvent.winopen('win_adddesc','新增备忘录','/views/devcustomer/descbuild.html?CompId='+$devId);
         break;
     case 'batchdel':
         {
             var checkStatus = table.checkStatus('Dev-CustomerDesc');
             var checkCount = checkStatus.data.length;
             if (checkCount < 1) {
                 top.winui.window.msg('请选择一条数据', {
                     time: 1000
                 });
                 return false;
             }
             var ids = [];
             $(checkStatus.data).each(function (index, item) {
                 ids.push(item.Id);
             });
             descEvent.deletedata(ids);
         }
         break
     case 'LAYTABLE_COLS'://选择列-系统默认不管
         break;
     case 'reloadTable'://刷新
     descEvent.reloadtable();
        break;
     default:
         layer.alert("暂不支持（" + obj.event + "）");
         break;

 };
});
//列表操作栏
table.on('tool(Dev-CustomerDesc)', function (obj) {
 var _data = obj.data;
 switch (obj.event) {
     case 'del'://删除
     var ids = [];   
     $(_data).each(function (index, item) {
         ids.push(item.Id);
     });
       descEvent.deletedata(ids,obj);
         break;
     case 'edit':
        descEvent.winopen('win_updatedesc','修改备忘录','/views/devcustomer/descbuild.html?CompId='+$devId+'&Id='+_data.Id);
         break;
     default:
         layer.alert("暂不支持（" + obj.event + "）");
         break;
 }
});

/***********************备忘录-end***************************************************************************************************/
 //次要字段编辑按钮
 function secfield(){
    wooutil.devajax({
        type: 'GET',
        url: devsetter.devbaseurl + 'api/DevPermission/SecFieldPermission',
        data: { funcode: "CustomerSecondaryField",Id:$devId },
        dataType: 'json',
        success: function (res) {
            var secfilds=["InvTitle","InvAddress","BankName","Account"];
            if(res.Tag==0){
           
            $.each(secfilds, function (index, fieldId) {

                switch (fieldId) {
                    case "InvTitle":
                    case 'InvAddress':
                    case 'BankName':
                    case 'Account':
                  
                        {//都是文本编辑框
                            devviewedit.render({
                                elem: '#' + fieldId,
                                edittype: 'text',
                                objid: $devId,
                                fieldname: fieldId,
                                verify: 'required',
                                ckEl: '#Name',
                                url: devsetter.devbaseurl +'api/DevCompany/UpdateField'

                            });
                        }

                        break;
                    // case "PrincipalUserDisplayName"://负责人
                    //     {//都是文本编辑框
                    //         viewPageEdit.render({
                    //             elem: '#' + fieldId,
                    //             edittype: 'selTable',
                    //             objid: companyId,
                    //             fieldname: fieldId,
                    //             verify: 'required',
                    //             selobjId: "#PrincipalUserId",
                    //             ckEl: '#Name',
                    //             url: '/Company/Customer/UpdateField'

                    //         });
                    //     }

                    //     break;

                }
            });
            }else {
                devviewedit.noUpShow(secfilds);
            }

      }
   });

 }
 secfield();
//审批历史
 flowapplist.applistInit({ Id: $devId, objType: 0 });
 /******************************************************审批按钮begin*******************************************/

 appflowdata=flowapplist.getappflowinfo({ AppObjId: $devId, ObjType: 0 ,CurrUserId:0});
 if(appflowdata.AppAuth===1){//有审批权限
  $("#Dev-AppSavebtn").removeClass("layui-hide");
 }
 //弹出审批框
 $("#Dev-AppSavebtn").on("click",function(){//审批按钮
    var optiondata={};
    optiondata.prefix="customer";//前缀
    optiondata.objtype=0;//审批类型
    optiondata.objId=$devId;
    optiondata.instId=appflowdata.InstId;//审批实例ID
   
    flowapplist.showflowoption(optiondata);

 });
 /******************************************************审批按钮-end********************************************* */

exports('customerview', {});
});