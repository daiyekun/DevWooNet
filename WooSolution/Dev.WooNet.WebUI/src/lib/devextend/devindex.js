
/* 
*模块时首先加载文件
 */
layui.config({
    base: '../../lib/' //指定 winui 路径
   , version: '1.0.0-beta'
}).extend({
    devsetter:'devextend/devsetter',//配置模块
  }).define(['form','devsetter'], function(exports){
    var $ = layui.$
    ,form=layui.form
    , layer = layui.layer
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
        

    }
    }
    
  
    
    
    
   
    
    
    
    //加载公共模块
    //layui.use('common');
  
    //对外输出
    exports('devindex', {
      
    });
  });