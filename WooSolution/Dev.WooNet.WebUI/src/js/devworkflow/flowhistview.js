layui.config({
    base: '../../lib/' //指定 winui 路径
    , version: '1.0.0-beta'
}).extend({
    winui: 'winui/winui',
    window: 'winui/js/winui.window',
    devindex: 'devextend/devindex',
    devselitem: 'devextend/devselitem'
   
   
}).define(['table', 'winui', 'window', 'layer', 'devindex', 'devselitem'], function (exports) {
    winui.renderColor();
    var table = layui.table,
        $ = layui.$,
        devindex = layui.devindex,
        msg = winui.window.msg
        ,form = layui.form
        ,devselitem=layui.devselitem
       
        , tableId = 'flowsettableid'
        ;
        var $instId = wooutil.getUrlVar('instId');//审批实例ID
        var ftype = wooutil.getUrlVar('wftype');
        //layer.alert($wftype);
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
            lhflow.setTitle("查看流程");
            lhflow.loadDataAjax({
                type: "GET",
                url: devsetter.devbaseurl+"api/DevFlowCommon/ViewFlowChart",
                data: { instId: $instId },
                dataType: "json"
    
            });
    
        });
    
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
                    url: devsetter.devbaseurl+"api/DevFlowInstance/ShowNodeInfoView",
                    data: {
                        nodeStr: _id
                        ,instId: $instId
                    },
                    success: function (res) {
                        $("#nodeId").text(_id);
                        $("#tdnodeName").text(objdata.name);
                        $("#NodeStrId").val(_id);
                      
                        form.val('nodeInfo', res.data);
                        $("#StateDic").text(res.data.StateDic);
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
            if (ftype != 3 || ftype != 4 || ftype != 5 || ftype != 6) {
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
            //查看所有意见
            showAllOpinion: function () {
                
                var url='/views/devworkflow/flowoptionview.html?instId=' + $instId;
                top.winui.window.open({
                    id: 'win-flowoption',
                    type: 2,
                    title: '查看审批意见',
                    content: url,
                    area: ['90%', '90%']
                    //area: ['50vw', '70vh']
                    //offset: ['15vh', '25vw']
                });
    
               
            },
            showNodeOpinion: function () {//节点意见
                var nodestrId = $("#NodeStrId").val();
                var url='/views/devworkflow/flowoptionview.html?instId=' + $instId + '&nodestrId=' + nodestrId;
                top.winui.window.open({
                    id: 'win-flownodeoption',
                    type: 2,
                    title: '查看节点意见',
                    content: url,
                    area: ['90%', '90%']
                    //area: ['50vw', '70vh']
                    //offset: ['15vh', '25vw']
                });
               
            }
        };
    
        $('.layui-btn.tempnode-btn').on('click', function () {
            var type = $(this).data('type');
            active[type] ? active[type].call(this) : '';
        });
    
        
/***********************基本信息-end***************************************************************************************************/

    
exports('flowhistview', {});
});