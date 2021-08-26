layui.config({
    base: '../../lib/' //指定 winui 路径
    , version: '1.0.0-beta'
}).extend({
    winui: 'winui/winui',
    window: 'winui/js/winui.window',
    devindex: 'devextend/devindex',
    devselitem: 'devextend/devselitem',
    formSelects: 'devextend/formSelects/formSelects-v4'
   
}).define(['table', 'winui', 'window', 'layer', 'devindex', 'devselitem','formSelects'], function (exports) {
    winui.renderColor();
    var table = layui.table,
        $ = layui.$,
        devindex = layui.devindex,
        msg = winui.window.msg
        ,form = layui.form
        ,devselitem=layui.devselitem
        ,formSelects=layui.formSelects
        , tableId = 'flowsettableid'
        ;
    var $devId = wooutil.getUrlVar('Id');
    var localdata=wooutil.devlocaldata();

/***********************基本信息-begin***************************************************************************************************/
var fTempId = wooutil.getUrlVar('tempId');//Id
var fname = decodeURI(decodeURI(wooutil.getUrlVar('ftitle')));//流程名称
var fno = decodeURI(decodeURI(wooutil.getUrlVar('dno')));//编号
var ftype = wooutil.getUrlVar('ftype');//流程类型0：客户，1供应商。。。。
var famount = wooutil.getUrlVar('famt');//审批金额
var flowitem = wooutil.getUrlVar('mission');//审批事项 int 类型
var cateId = wooutil.getUrlVar('cateId');//类别ID int 类型
var objId = wooutil.getUrlVar('objId');//审批对象ID
var tempHistId= wooutil.getUrlVar('tempHistId');//历史模板ID
if (famount == undefined) {
    famount = -1;
}
$("#TempId").val(fTempId);
var property = {
    toolBtns: ["start round mix", "end round mix", "task"],
    haveHead: false,
    headLabel: true,
    headBtns: ["new", "save", "undo", "redo", "reload"],//如果haveHead=true，则定义HEAD区的按钮
    haveTool: false,
    haveDashed: true,
    haveGroup: true,
    useOperStack: true,
    initNum: 1

};
//取代setNodeRemarks方法，采用更灵活的注释配置
GooFlow.prototype.remarks.toolBtns = {
    cursor: "选择指针",
    direct: "结点连线",
    dashed: "关联虚线",
    start: "开始节点",
    "end": "结束结点",
    "task": "流程节点",
    //node: "自动结点",
    // chat: "决策结点",
    // state: "状态结点",
    //plug: "附加插件",
    // fork: "分支结点",
    //"join": "联合结点",
    //"complex": "复合结点",
    group: "组织划分框编辑开关"
};
GooFlow.prototype.remarks.headBtns = {
    new: "新建流程",
    open: "打开流程",
    save: "保存流程图",
    undo: "撤销",
    redo: "重做",
    reload: "刷新流程",
    print: "打印流程图"
};
var lhflow;
$(document).ready(function () {
    GooFlow.prototype.remarks.extendRight = "工作区向右扩展";
    GooFlow.prototype.remarks.extendBottom = "工作区向下扩展";
    lhflow = $.createGooFlow($("#flowdesignser"), property);
    $("tr.fnode").hide();//初始化隐藏节点信息

    lhflow.setTitle(fname);
    $("#ftype").text(ftype);
    //demo.setNodeRemarks(remark);
    //lhflow.loadData(jsondata);
    lhflow.loadDataAjax({
        type: "GET",
        
        //url: "/WorkFlow/FlowTempNode/TestNodeData",
        url:devsetter.devbaseurl+ "api/DevFlowTemp/SubmitFlowNodeLoad",
        data: { TempId: fTempId, Amount: famount },
        dataType: "json"

    });

});
/**
*单元格右键事件
**/
lhflow.onItemRightClick = function (id, type) {
    //console.log("onItemRightClick: " + id + "," + type);
    return false;//返回false可以阻止浏览器默认的右键菜单事件
}
/**
*单元格双击事件
**/
lhflow.onItemDbClick = function (id, type) {
    //console.log("onItemDbClick: " + id + "," + type);

    return true;//返回false可以阻止原组件自带的双击直接编辑事件
}
/**
*设置编辑器宽高
**/
window.onresize = function () {
    lhflow.reinitSize($("#flowpanel").width(), $("#flowpanel").height());

}
/**
*相关方法
**/
var flowThod = {
    setNodeInfo: function (_id, objdata) {
        wooutil.devajax({
            url:devsetter.devbaseurl+ "api/DevFlowInstance/GetNodeInfoView",
           
            data: {
                nodeStr: _id
                , tempId: fTempId
            },
            success: function (res) {

                $("#nodeId").text(_id);
                $("#tdnodeName").text(objdata.name);
                $("#NodeStrId").val(_id);

                form.val('nodeInfo', res.data);
                $("#GroupName").val(res.data.GroupName);
                $("#groUserNames").text(res.data.UserNames);
                  if (res.data.Nrule == 0) {
                    $("#Nrule0").attr("checked", true);
                  } else if (res.data.Nrule == 1) {//审批规则
                     $("#Nrule1").attr("checked", true);
                  }
                 
                  form.render();//这样checkbox显示才正常
                

            }
        });





    }

}
/**
*节点获取光标事件
**/
lhflow.onItemFocus = function (_id, type) {
    var objdata;
    switch (type) {
        case "node":
        case "task":
            $("tr.fnode").show();
            //审批条件
            checkNodeCdn();
            objdata = this.$nodeData[_id];
            flowThod.setNodeInfo(_id, objdata);
            break;
        default:

            break;
    }

    return true;
}
/**
*根据审批类型判断条件是否显示
***/
function checkNodeCdn() {
    //根据枚举NF.ViewModel.Models/WorkFlow/Enums/FlowObjEnums判断
    if (ftype != 3 && ftype != 4 && ftype != 5 && ftype != 6) {
        $("tr.fnode.nodecdn").hide();

    }
    if (ftype != 3) {//合同文本修改
        $("tr.fnode.htnode").hide();

    }
   
}
/**
*贯标失效
**/
lhflow.onItemBlur = function (_id, type) {
    $("tr.fnode").hide();
    return true;
}



/**
*节点操作相关事件
**/
var active = {
    //提交流程
    submitFlow:function(){
        var logdindextp = layer.load(0, { shade: false });
        wooutil.devajax({
                       async: false,
                       url: devsetter.devbaseurl+'api/DevFlowInstance/CheckSubmitFlow'
                      , data: JSON.stringify({
                        tempId: fTempId
                       , amount: famount
                       , flowType: ftype
                       }) ,
                       type: 'POST',
                       dataType: "json",
                       contentType: "application/json; charset=utf-8",
                       success: function (res) {
                           var result=res.OtherValue;
                           layer.alert(JSON.stringify(res));
                           if (result === -1 || result === -2 || result === -3 || result === -4) {
                               layer.close(logdindextp);
                           }
                           if (result === -1) {
                            top.winui.window.msg('提交异常！', {
                                icon: 2,
                                time: 2000
                            });
                               return false;
                           } else if (result === -2) {
                            top.winui.window.msg('没有开始，结束节点！！', {
                                icon: 2,
                                time: 2000
                            });
                               return false;
                               
                           }
                           else if (result === -3) {
                            top.winui.window.msg('没有完整的流程，可能是金额不匹配！', {
                                icon: 2,
                                time: 2000
                            });
                               return false;
                              
                           }
                           else if (result === -4) {
                            top.winui.window.msg('没有节点信息或者节点图！', {
                                icon: 2,
                                time: 2000
                            });
                               return false;
                              
                           } else{//正式提交流程
                            wooutil.devajax({
                                url: devsetter.devbaseurl+'api/DevFlowInstance/SubmitWorkFlow'
                                ,type: 'POST',
                                dataType: "json",
                                contentType: "application/json; charset=utf-8"
                                , data:JSON.stringify({
                                  Id:0//方便后台绑定
                                ,ObjType: ftype//审批对象类型（客户，合同。。）
                                , AppObjId: objId//对象ID
                                , AppObjName: fname//名称
                                , AppObjNo: fno//编号
                                , AppObjCateId: cateId//类别ID
                                , TempId: fTempId//模板ID
                                , AppObjAmount: famount//金额
                                , Mission: flowitem
                                ,TempHistId:tempHistId//历史模板ID
                                // , FinceType: param.finceType//资金性质，合同使用
                                //, AppSecObjId: param.AppSecObjId
                            })
                            , success: function (res) {
                                layer.close(logdindextp);
                                layer.msg("提交成功", { icon: 6, time: 500 }, function (msgindex) {
                                //     table.reload(param.tableId, {
                                //         where: { rand: wooutil.getRandom() }

                                //  });
                                //刷新表格
                                 top.winui.window.tablelaod({id:'22'});
                                 top.winui.window.close('win_submitflow');
                                });

                                         }
                                });





                           }
                        }
                    });

    }
    
};


/********
*提交流程
 **********/
$("#btn_submitflow").on('click',function(){
active.submitFlow();
});
/***********************基本信息-end***************************************************************************************************/

    
exports('flowcview', {});
});