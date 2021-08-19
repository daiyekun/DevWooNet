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
 var fTempId = wooutil.getUrlVar('Id');//Id
    var fname = decodeURI(decodeURI(wooutil.getUrlVar('ftitle')));//流程名称
    var fvs = wooutil.getUrlVar('vs');//版本
    var ftype = decodeURI(decodeURI(wooutil.getUrlVar('ftype')));//流程类型
    $("#TempId").val(fTempId);
    var property = {
        toolBtns: ["start round mix", "end round mix", "task"],
        haveHead: true,
        headLabel: true,
        headBtns: ["new", "save", "undo", "redo", "reload"],//如果haveHead=true，则定义HEAD区的按钮
        haveTool: true,
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
        $("#fv").text(fvs + '.0');
        $("#ftype").text(ftype);
        //demo.setNodeRemarks(remark);
        //lhflow.loadData(jsondata);
        lhflow.loadDataAjax({
            type: "GET",
            //url: "/WorkFlow/FlowTempNode/TestNodeData",
            url: devsetter.devbaseurl + "api/DevFlowTemp/TempFlowNodeLoad",
            data: { TempId: fTempId },
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
                type: 'GET',
                url: devsetter.devbaseurl  + 'api/DevFlowTemp/GetNodeInfoView',
              //  url: "/WorkFlow/FlowTempNode/GetNodeInfoView",
                data: {
                      nodeStr: _id
                    , tempId: fTempId
                },
                success: function (res){
                    debugger;
                   
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
        if (ftype != "合同" && ftype != "收票" && ftype != "开票" && ftype != "付款") {
            $("tr.fnode.nodecdn").hide();
        } 
        if (ftype != "合同") {//合同文本修改
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
    *顶部工具栏保存按钮
    **/
    lhflow.onBtnSaveClick = function () {
        layer.confirm('你确定需要保存吗？只有流程图修改才需要保存', { icon: 3, title: '提示信息' }, function (index) {
               var reqSaveNode={};
               reqSaveNode.FlowNodeData=JSON.stringify(lhflow.exportData());
               reqSaveNode.TempId=fTempId;
               wooutil.devajax({
                type: 'POST',
                url: devsetter.devuserurl + 'api/DevFlowTemp/SaveNode',
                data: JSON.stringify(reqSaveNode),
                dataType: "json",
                contentType: "application/json; charset=utf-8",
               
                // {
                //     FlowNodeData: JSON.stringify(lhflow.exportData())
                //     , TempId: fTempId
                // },
                success: function (res) {
                    layer.alert("保存成功");
                }
            });
        });

    }
    /**
    *新建流程
    **/
    lhflow.onBtnNewClick = function () {
        layer.confirm('你确定清除当前流程图重新绘制？请慎重考虑！', { icon: 3, title: '提示信息' }, function (index) {
            wooutil.devajax({
                url: devsetter.devbaseurl +"api/DevFlowTemp/ClearNodeData",
                data: { tempId: fTempId },
                done: function (res) {
                    lhflow.clearData();
                    layer.close(index);
                }
            });
        });
    }

    /**
    *节点操作相关事件
    **/
    var active = {
        //保存节点信息
        saveNodeInfo: function () {
           layer.confirm('你确定需要保存吗？', { icon: 3, title: '提示信息' }, function (index) {
                //lhflow.setName('1552037583910', '设置节点测试', 'node');
               var submitbtn = $("#nodeInfoFromBtn");
               form.on('submit(nodeInfoFromBtn)', function (data) {
                        wooutil.devajax({
                            type: 'POST',
                            url: devsetter.devbaseurl +"api/DevFlowTemp/SaveNodeInfo",
                            data: JSON.stringify(data.field),
                            dataType: "json",
                            contentType: "application/json; charset=utf-8"
                              , done: function (res) {
                                  layer.alert("保存成功", function (_index2) {
                                      layer.close(_index2);
                                  });
                              }
                       });
                   return false;
               });
               submitbtn.trigger('click');
            });
        },
        addUsers: function () {
            layer.open({
                type: 2
                        , title: '选择组'
                        , content: '/views/devflowset/selectgroup.html'
                        , maxmin: true
                        , area: ['60%', '80%']
                        , btn: ['确定', '取消']
                        , btnAlign: 'c'
                        , skin: "layer-ext-myskin"
                        , yes: function (index, layero) {
                            debugger;
                            var iframeWindow = window['layui-layer-iframe' + index];
                           var  checkData= iframeWindow.layui.table.checkStatus("flowgrouptableid").data;
                           
                            if (checkData.length !== 1) {
                                return layer.msg('请选择一条数据');
                            }
                              debugger;
                            $("#GroupName").val(checkData[0].Name);
                            $("#groUserNames").text(checkData[0].UserNames);
                            $("#GroupId").val(checkData[0].Id);

                            layer.close(index);
                            return false;
                        },
                success: function (layero, index) {

                }
            });
        }
    };

    $('.layui-btn.tempnode-btn').on('click', function () {
        var type = $(this).data('type');
        active[type] ? active[type].call(this) : '';
    });
/***********************基本信息-end***************************************************************************************************/

    
exports('flowset', {});
});