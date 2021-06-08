
/* 
*模块时首先加载文件
 */
layui.config({
    base: '../../lib/' //指定 winui 路径
   , version: '1.0.0-beta'
}).extend({
    devsetter:'devextend/devsetter',//配置模块
  }).define(['form','devsetter','table'], function(exports){
    var $ = layui.$
    ,form=layui.form
    , layer = layui.layer
    ,table=layui.table
    devsetter=layui.devsetter;
    var index = parent.layer.getFrameIndex(window.name);
    var body = layer.getChildFrame('body', index);
    //一些工具类
    wooutil = {
      getUrlVars: function () {
          var vars = [], hash;
          var hashes = body.context.URL.slice(body.context.URL.indexOf('?') + 1).split('&');
          for (var i = 0; i < hashes.length; i++) {
              hash = hashes[i].split('=');
              vars.push(hash[0]);
              vars[hash[0]] = hash[1];
          }
          return vars;
      },
      getUrlVar: function (name) {
          /// <summary>获取URL参数</summary>
          ///<param name='name'>参数名称</param>
          var tempvl =wooutil.getUrlVars()[name];
          return tempvl === undefined ? "" : wooutil.getUrlVars()[name];
      },
      getRandom(){//生成随机数
        return Math.round(Math.random() * (10000 - 1)).toString();
      },
      getUrlParam(name){//获取url参数
        var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)"); //构造一个含有目标参数的正则表达式对象
            var r = window.location.search.substr(1).match(reg);  //匹配目标参数
            if (r != null) return unescape(r[2]); return null; //返回参数值
      },
      getdatadic: function (param) {
        /// <summary>数据字典下拉框赋值</summary>  
        /// <param name="param" type="Object">selectEl:select的ID带#，dataenum数据字典类别的enum值。</param>
            $.ajax({
                url: devsetter.devuserurl+'api/DataDic/GetDataByType?rand=' + wooutil.getRandom(),
                data: { typeint: param.dataenum }
                , async: false//取消异步
                , success: function (res) {
                   
                    if (param.script === "script") {
                        var slhtml = '<select name="' + param.selectEl + '" id="' + param.selectEl + '" lay-filter="' + param.selectEl + '">'
                        $(res.data).each(function (i, n) {
                            slhtml = slhtml + "<option value='" + n.Id + "'>" + n.Name + "</option>";
                            // $($("#selectLb").html()).append("<option value='" + n.Id + "'>" + n.Name + "</option>");
                        });
                        slhtml = slhtml + ' </select>';
                        $("#" + param.scriptEl).html(slhtml)
                        //form.render("select");//必须

                    }
                    else {
                        $(res.data).each(function (i, n) {
                            $(param.selectEl).append("<option value='" + n.Id + "'>" + n.Name + "</option>");
                        });
                    }
                    form.render("select");//必须
                    if (param.wooverify != undefined && param.wooverify) {
                        $(param.selectEl).next("div.layui-form-select").children("div").children("input").addClass("pen");
                        //$(param.selectEl).next("div.layui-form-select").children("div").children("input").addClass("pen");
                    }
                }
            });
        

       },
       openPostWindow: function (url, postData, isBlank) {
        /// <summary>
        /// 打开post的新窗口
        /// </summary>        
        /// <param name="url" type="String">路径</param>
        /// <param name="postData" type="Object">Post的数据</param>
        /// <param name="isBlank" type="Boolean">打开新窗口</param>
        var form = $('#nfPostWin');
        form.remove();

        var html = '<form id="nfPostWin" action="' + url + '" target="_blank" method="post"';
        if (isBlank) {
            html += ' target="_blank"';
        }
        html += '>';
        for (var key in postData) {
            var val = postData[key];
            html += '<input type="hidden" name="' + key + '" value="' + val + '" />';
        }

        html += '</form>';

        $('body').append(html);
        form = $('#nfPostWin');
        form.submit();
        form.remove();
    },
    exportexcel: function (obj, param) {
        /// <summary>
        /// 弹出导出Excel设置界面
        /// </summary>        
        /// <param name="url" type="String">请求路径</param>
        /// <param name="postData" type="Object">Post参数</param>
        var url='/views/common/excelexport.html';
        layer.open({
        type: 2
        , title: '导出数据'
        , content: url
        ,closeBtn: 0
        ,skin: 'layui-layer-lan'
        , area: ['370px', '360px']
        , btn: ['导出', '取消']
        , btnAlign: 'c'
        , yes: function (index, layero) {
            var iframeWindow = window['layui-layer-iframe' + index]
            var selcell = $("input[type=radio][name=selcell]:checked").val();//所选列
            var selrow = $("input[type=radio][name=selrow]:checked").val();//所选行
            var selcellfields = [];//选择列字段
            var selcelltitles = [];//选择列标题
            var selIds = [];//选择的ID
            var tbcols = obj.config.cols;
            if (selcell == 1) {//选择列
                $.each(tbcols[0], function (n, v) {
                    if (v.field !== "" && v.field != undefined && !v.hide) {
                        selcellfields.push(v.field);
                        selcelltitles.push(v.title);

                    }
                });
            } else {//所有数据列
                $.each(tbcols[0], function (n, v) {
                    if (v.field !== "" && v.field != undefined) {
                        selcellfields.push(v.field);
                        selcelltitles.push(v.title);

                    }

                });
            }
            if (selrow == 1) {//所选行
                var checkStatus = table.checkStatus(obj.config.id);
                var checkdata = checkStatus.data;
                if (checkdata.length <= 0) {
                    return layer.msg('请选择导出数据！');
                }
                $.each(checkdata, function (n, v) {

                    selIds.push(v.Id);
                });
            }
            var postdata = {};
            postdata.Ids = selIds;
            postdata.SelTitle = selcelltitles;
            postdata.SelField = selcellfields;
            postdata.SelCell = selcell == 1;
            postdata.SelRow = selrow == 1;
            postdata.KeyWord = param.keyword;
            wooutil.openPostWindow(param.url, postdata, true);
            layer.close(index);
        }, success: function () {
            $("#ExportExcelSet").removeClass("layui-hide");
        }
        });
    }
}
    
  
    
    
    
   
    
    
    
    //加载公共模块
    //layui.use('common');
  
    //对外输出
    exports('devindex', {
      
    });
  });