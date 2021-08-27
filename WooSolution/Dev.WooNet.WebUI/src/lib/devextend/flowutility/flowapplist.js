/*
*审批历史列表
 */
layui.config({
    base: '../../lib/' //指定 winui 路径
    
}).extend({
  
 
}).define(['table', 'form'], function (exports) {
    var $ = layui.$
   , table = layui.table
   , admin = layui.admin
    , form = layui.form;
    var logdindex = layer.load(0, { shade: false });
    var wftype = -1;//流程类型
    var localdata=wooutil.devlocaldata();
    var flowtableinstance=null;
    var flowapplist = {
        applistInit: function (parm) {
            wftype = parm.objType;
            var $url = devsetter.devbaseurl+'api/DevFlowInstance/GetAppHistList?rand=' + wooutil.getRandom();
            $url = $url + '&appObjId=' + parm.Id + '&objType=' + parm.objType
            flowtableinstance= table.render({
                elem: '#NF-workFlow-approveList'
               , url: $url
               ,headers: {
                "Authorization": "Bearer "+ localdata.token +""
                ,loginkey:localdata.loginkey
              }
               , toolbar: '#toolworkflowhist'
               , defaultToolbar: ['filter']
               , cellMinWidth: 80
               , cols: [[
                     { type: 'numbers', fixed: 'left' }
                   , { type: 'checkbox', fixed: 'left' }
                   , { field: 'MissionDic', title: '审批事项', width: 160 }
                   , { field: 'StartUserName', title: '发起人', width: 130 }
                   , { field: 'StartDateTime', title: '发起日期', width: 130 }
                   , { field: 'CurrentNodeName', title: '当前节点', width: 160 }
                   , { field: 'AppStateDic', title: '流程状态', width: 130 }
                   , { field: 'CompleteDateTime', title: '完成日期', width: 140 }
                   , { title: '操作', width: 170, align: 'center', fixed: 'right', toolbar: '#applisthist-bar' }
               ]]
               , page: false
               , loading: true
               , height:350
              , limit: 20
               , done: function (res, curr, count) {   //返回数据执行回调函数
                   layer.close(logdindex);    //返回数据关闭loading



               }

            });


        }


      
    }
  
    /***查看流程图**/
    function viewflow(obj) {
        var $url = '/views/devworkflow/flowhistview.html?instId=' + obj.data.Id + '&wftype=' + wftype;
        top.winui.window.open({
            id: 'win_showflowchart',
            type: 2,
            title: '查看流程图',
            maxmin:true,
            content: $url,
            area: ['90%', '80%'],
           
        });
        //parent.parent.parent.layui.index.openTabsPage($url, '查看流程')
    }
    
    
  /**
   *审批历史列表
  **/
    table.on('tool(NF-workFlow-approveList)', function (obj) {
        switch (obj.event) {
            case "viewapp"://查看
                viewflow(obj);
                break;
            case "printapp"://审批单
                if (obj.data.AppState == 2) {
                    var opurl = '/WorkFlow/FlowDocToPdf/FlowinceToPdf?WfInceId=' + obj.data.Id;
                    window.open(opurl);
                } else {
                    layer.alert("只有审批通过才允许打印");
                }
                break;
        }
    });
    //头部工具栏

    table.on('toolbar(NF-workFlow-approveList)', function (obj) {
        switch (obj.event) {
            case "reloadTable"://刷新
            if(flowtableinstance!=null){
                flowtableinstance.reload();
            }
            break;
            
        }
    });
   
    layer.close(logdindex);
    exports('flowapplist', flowapplist);
});