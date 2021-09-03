
/* 
*模块时首先加载文件
 */
layui.config({
    base: '../../lib/' //指定 winui 路径
   , version: '1.0.0-beta'
}).extend({
    devsetter:'devextend/devsetter',//配置模块
   
  }).define(['form','devsetter','table'], function(exports){
    winui.renderColor();
    var $ = layui.$
    ,form=layui.form
    , layer = layui.layer
    ,table=layui.table
    devsetter=layui.devsetter;
    var index = parent.layer.getFrameIndex(window.name);
    var body = layer.getChildFrame('body', index);
    devindex = function(id){
        return new Class(id);
      }
    $.download = function (url, method, filedir, filename) {
        $('<form action="' + url + '" method="' + (method || 'post') + '">' +  // action请求路径及推送方法
                    '<input type="text" name="filedir" value="' + filedir + '"/>' + // 文件路径
                    '<input type="text" name="filename" value="' + filename + '"/>' + // 文件名称
                '</form>')
        .appendTo('body').submit().remove();
    };
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
                url: devsetter.devbaseurl+'api/DataDic/GetDataByType?rand=' + wooutil.getRandom(),
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
       subitexel:function(postdata){
      
        wooutil.devajax({
            type: 'POST',
            url: postdata.url, //devsetter.devuserurl+"api/DevUser/exportexcel",
            //async: false,
            // processData: false,
            data: JSON.stringify(postdata),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (json) {
               
                // debugger;
                // $.download(devsetter.devuserurl, 'post', 'Uploads/ExcelExport', '系统用户.xlsx');
              // window.open(devsetter.devuserurl+'Uploads/ExcelExport'+'/系统用户.xlsx');
              window.open(devsetter.devbaseurl+json.data.filePath+'/'+json.data.fileName);

            },
            error: function (xml) {
               
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
        
        var html = '<form id="nfPostWin" action="'+url+'" target="_blank" method="post" ';
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
            postdata.Ids = selIds.toString();
            postdata.SelTitle = selcelltitles.toString();
            postdata.SelField = selcellfields.toString();
            postdata.SelCell = selcell == 1;
            postdata.SelRow = selrow == 1;
            postdata.KeyWord = param.keyword;
            postdata.url=param.url;
            wooutil.subitexel(postdata);
            //wooutil.openPostWindow(param.url, postdata, true);
            layer.close(index);
        }, success: function () {
            $("#ExportExcelSet").removeClass("layui-hide");
        }
        });
        },
        devajax:function(options){  
        /// <summary>
        /// ajax请求封装
        /// </summary>        
        /// <param name="options" type="Object">请求参数</param>
        var that = this
        ,success=options.success
        ,error = options.error
        options.data = options.data || {};
        var localdata=wooutil.devlocaldata();
        options.headers = {
            "Authorization": "Bearer "+ localdata.token +""
            ,loginkey:localdata.loginkey
        },
       
        $.ajax(options);
       

        },
        devloginout:function(res){
        /// <summary>
        /// 登录超时返回登录界面
        /// </summary>        
        /// <param name="res" type="Object">反馈数据对象</param>
           if(res.code==1001)
            {
            layer.msg('由于长时间没有操作,登录已经失效，请重新登录', {
                offset: '15px'
                ,icon: 5
                ,time: 1000
              }, function(){
                parent.location.href="/login2.html";
              });
           
           }
        },devlocaldata:function(){
         /// <summary>
         /// 返回本地缓存数据
         /// </summary>    
         var acctoken=layui.data(devsetter.devtableName)[devsetter.request.tokenName] || '';
         var loginkey=layui.data(devsetter.devtableName)[devsetter.request.loginkey] || '';
         var obj=new Object();
             obj.token= acctoken;
             obj.loginkey=loginkey;
             return obj;
      

        }
        ,selverpen: function () {///<summary>添加下拉框小笔头</summary>
            setTimeout(function () {
                $("select").each(function (_index, el) {

                    if ($(el).attr("lay-verify") != "" && $(el).attr("lay-verify") != undefined) {
                        $(el).siblings("div").find("input").addClass("dev-input-pen");
                    }

                });

            }, 500)

        },
        download: function (param) {
            /// <summary>文件下载</summary>
            /// <param name="url" type="String">下载路径</param>
            /// <param name="Id" type="number">下载数据对象ID</param>
            /// <param name="DownType" type="number">下载类型，默认是0：1:下载模板起草最终Word</param>
            /// <param name="dtype" type="number">下载类别，默认是0：1:标识下载的是历史</param>
            /// <param name="folder" type="number">文件夹索引,参考枚举：UploadAndDownloadFoldersEnum</param>
            var _url = param.url;
            if (param.url == undefined || param.url == "") {
                _url = devsetter.devupload.uploadIp+"api/DevFileCommon/download";
            }
            // var loadurl =_url + '?Id=' + param.Id + "&Folderenum=" + param.folder + "&Dtype=" + param.dtype + "&DownType=" + param.downType + "&rand=" + wooutil.getRandom();
            // console.log(loadurl);
            // window.open(loadurl);
            wooutil.subitexel(param);
        },

        
    };
    //一些工具类结束----------------------------------------------------------------
       
    //流程相关-------------------------flow-begin-----------------------------------------------------
     flowtool={
         getFlowInfo:function(param){
             /// <summary>获取流程模板及流程情况</summary>
             var $data;
             wooutil.devajax({
                  url: devsetter.devbaseurl+'api/DevFlowInstance/getflowinfo'
                , async: false//取消异步
                ,method: 'POST'
                ,contentType: 'application/json'
                , data: JSON.stringify({
                    FlowItem: param.flowitem,
                    DeptId: param.deptId,
                    ObjType: param.objType,
                    ObjCateId: param.objCateId,
                    ObjId: param.objId
    
                })
                , success: function (res) {
                   
                    $data = res.data;
                }
             });
            
             return $data;


         },
         submitflow:function(flowdata){
             /// <summary>提交流程</summary>
             var tempdata = flowtool.getFlowInfo({
                flowitem:flowdata.flowitem
                ,deptId: flowdata.deptId
                , objType: flowdata.objType
                , objCateId: flowdata.objCateId
                ,objId:flowdata.objId
            });
            
            if(tempdata.InstId!==0){
                top.winui.window.msg('流程已经提交,不能重复提交！', {
                    icon: 2,
                    time: 2000
                });
                return false;//layer.alert("流程已经提交,不能重复提交！");

            }else if(tempdata.TempId===0){
                return -1;//没有流程模板

            }else{//开始准备提交
                var opurl = "/views/devworkflow/flowcview.html?tempId="
                + tempdata.TempId + "&ftitle=" + encodeURI(encodeURI(flowdata.objName))
                + "&ftype=" + flowdata.objType + "&famt="+ flowdata.objamt
                +"&dno="+encodeURI(encodeURI(flowdata.objNo))
                +"&mission="+flowdata.flowitem+"&objId="+flowdata.objId
                +"&cateId="+flowdata.objCateId
                +"&tempHistId="+tempdata.TempHistId
                ;
                var $title = flowdata.objName + "--提交流程"

                top.winui.window.open({
                    id: 'win_submitflow',
                    type: 2,
                    title: $title,
                    btn: ['提交流程', '取消'],
                    content: opurl,
                    area: ['80%', '90%']
                    // offset: ['15vh', '25vw']
                    ,success: function (layero, index) {
                      parent.parent.layer.full(index);
                    }
                });

            //     parent.parent.layer.open({
            //         type: 2
            //    , title: $title
            //    , content: opurl
            //    , maxmin: true
            //         // , area: ['60%', '80%']
            //    , btn: ['提交流程', '取消']
            //    , btnAlign: 'c'
            //    , skin: "layer-ext-myskin"
            //    , yes: function (index, layero) {
            //        var logdindextp = layer.load(0, { shade: false });
            //        var flowitem = flowdata.flowitem;
            //        wooutil.devajax({
            //            async: false,
            //            url: devsetter.devbaseurl+'api/DevFlowInstance/CheckSubmitFlow'
            //           , data: JSON.stringify({
            //             tempId: tempdata.TempId
            //            , amount: param.objamt
            //            , flowType: param.objType
            //            }) ,
            //            type: 'POST',
            //            success: function (res) {
            //                if (res === -1 || res === -2 || res === -3 || res === -4) {
            //                    layer.close(logdindextp);
            //                }
            //                if (res === -1) {
            //                    return layer.alert("提交异常！"); 
            //                } else if (res === -2) {
            //                    return layer.alert("没有开始，结束节点！");
            //                }
            //                else if (res === -3) {
            //                    return layer.alert("没有完整的流程，可能是金额不匹配！");
            //                }
            //                else if (res === -4) {
            //                    return layer.alert("没有节点信息或者节点图");
            //                } else {
            //                    admin.req({
            //                        url: '/WorkFlow/AppInst/SubmitWorkFlow'
            //                                         , data: {
            //                                             ObjType: param.objType//审批对象类型（客户，合同。。）
            //                                             , AppObjId: checkData[0].Id//对象ID
            //                                             , AppObjName: param.objName//名称
            //                                             , AppObjNo: param.objCode//编号
            //                                             , AppObjCateId: param.objCateId//类别ID
            //                                             , TempId: tempdata.TempId//模板ID
            //                                             , AppObjAmount: param.objamt//金额
            //                                             , Mission: flowitem
            //                                             , TempHistId: tempdata.TempHistId
            //                                             , FinceType: param.finceType//资金性质，合同使用
            //                                             , AppSecObjId: param.AppSecObjId
            //                                         }, done: function (res) {
            //                                             layer.close(logdindextp);
            //                                             layer.msg("提交成功", { icon: 6, time: 500 }, function (msgindex) {
            //                                                 table.reload(param.tableId, {
            //                                                     where: { rand: wooutil.getRandom() }

            //                                                 });
            //                                                 layer.close(index);
            //                                             });

            //                                         }
            //                    });
            //                }

            //            }

            //        });


            //    },
            //         success: function (layero, index) {
            //             parent.parent.layer.full(index);


            //         }
            //     })


            }

          

         }

    };
    //流程相关-------------------------flow-end---------------------------------------------------------
           

    
  
    //加载公共模块
    //layui.use('common');
    //对外输出
    // exports('devindex', {
      
    // });
     exports('devindex', devindex);
  });