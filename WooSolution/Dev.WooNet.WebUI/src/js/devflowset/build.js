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
var wfcomm = {
    /// <summary>获取审批对象列表</summary>  
    /// <param name="param" type="Object">selectEl:select的ID带#</param>
    getObjTypes: function (param) {
        wooutil.devajax({
            type: 'GET',
            url: devsetter.devuserurl + 'api/DevFlowTemp/getobjtypes',
            
            data: { rand:  Math.round(Math.random() * (10000 - 1)).toString() },
            dataType: 'json',
            success: function (res) {
                $(res.data).each(function (i, n) {
                    $(param.selectEl).append("<option value='" + n.Value + "'>" + n.Desc + "</option>");
                });
                form.render("select");//必须
            },
            error: function (xml) {
                msg('加载失败!');

            }
        });
       
    },
    getObjTypeClass(param) {
        var tagIns2 = selectM({
            //元素容器【必填】
            elem: param.selectEl
            //候选数据【必填】
        , data: param.arraydata
            //默认值
       , max: 50
            //input的name 不设置与选择器相同(去#.)
       , name: param.inputName
            //值的分隔符
       , delimiter: ','
            //候选项数据的键名
       , field: { idName: param.idName, titleName: param.titleName }
        });
    }
}

 //对象列表
 wfcomm.getObjTypes({ selectEl: '#ObjType' });
  /**
    *审批事项
    ***/
    function initFlowItems(dataEnum,dataval) {
        formSelects.config('flowitems', {
            direction: 'down'
             , success: function (id, url, searchVal, result) {
                 formSelects.value('flowitems', dataval);
             }
        }).data('flowitems', 'server', {
            url: devsetter.devuserurl +'api/DevBaseData/GetFlowItems?objEnum=' + dataEnum
        });
    }

     /**
  *类别
  ***/
  function initObjClassTree(dataval, dataEnum) {
    formSelects.config('CategoryIds', {
        direction: 'down'
        , success: function (id, url, searchVal, result) {
            formSelects.value('CategoryIds', dataval);
        }
    }).data('CategoryIds', 'server', {
        url: devsetter.devuserurl +'api/DevBaseData/GetFlowClassTree?objEnum=' + dataEnum
       
        , tree: {
            nextClick: function (id, item, callback) {
            }
        }
    });

}
/**
*所属机构
***/
function initDepts(dataval) {
    formSelects.config('DeptIds', {
        direction: 'down'
        , success: function (id, url, searchVal, result) {
            formSelects.value('DeptIds', dataval);
        }
    }).data('DeptIds', 'server', {
        url: devsetter.devuserurl +'api/DevBaseData/GetFlowDeptTree'
        , tree: {
            nextClick: function (id, item, callback) {
            }
        }
    });
    
}

 /**
    *根据选择审批对象获取字典类别
    *获取值对应 NF.ViewModel/Extend/Enums/DataDictionaryEnum
    **/
    function GetDicEnum(selval) {
        var dataenum = 0;
        switch (selval) {
            case "0"://客户
            case 0:
                dataenum = 3;
                break;
            case "1"://供应商
            case 1:
                dataenum = 2;
                break;
            case "2"://其他对方
            case 2:
                dataenum = 4;
                break;
            case "3"://合同
            case "6"://付款
            case 3:
            case 6:
                dataenum = 1;
                break;
            case "4"://收票
            case "5"://开票
            case 4:
            case 5:
                dataenum = 19;
                break;
            case 7://项目
            case "7"://项目
                dataenum = 13;
                break;
          
            default:
                dataenum = -1;
                break;
        }

        return dataenum;
    }

    //选择对象时设置对象类别
    form.on('select(ObjType)', function (data) {
        initFlowItems(data.value,[]);
        //initObjClass(GetDicEnum(data.value), []);
        initObjClassTree([],GetDicEnum(data.value));

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
            url: devsetter.devuserurl + 'api/DevFlowTemp/Save',
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
            top.winui.window.close('win_updateflowset');
        } else {
            top.winui.window.close('win_addflowset');

        }
    }
    /**提交成功 */
    function submitsuccess(json) {
        if (json.code==0) {
            top.winui.window.msg('操作成功', {
                icon: 1
            },function(){
                closeWin();
                top.winui.window.tablelaod({id:'25'});
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
                url: devsetter.devuserurl + 'api/DevFlowTemp/showView',
                //async: false,
                data: { Id: $devId },
                dataType: 'json',
                success: function (res) {
                    form.val("DEV-FlowSetForm", res.data);
                    $("#Id").val(res.data.Id);
                    initObjClassTree(res.data.CateIdsArray, GetDicEnum(res.data.ObjType));
                    initFlowItems(res.data.ObjType, res.data.FlowItemsArray);
                    initDepts(res.data.DeptIdsArray);
                },
                error: function (xml) {
                    msg('加载失败!');

                }
            });


        } else {
            initDepts([]);
          
        }
    }
    

     wooutil.selverpen();//下拉小笔头
    
    //执行赋值表单
    devSetValues();
    form.render(null, 'DEV-FlowSetForm');
/***********************基本信息-end***************************************************************************************************/

    
exports('flowsetbuild', {});
});