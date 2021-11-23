layui.config({
    base: '../../lib/' //指定 winui 路径
    , version: '1.0.0-beta'
}).extend({
    winui: 'winui/winui',
    window: 'winui/js/winui.window',
    devindex: 'devextend/devindex',
  
}).define(['table', 'winui', 'window', 'layer', 'devindex'], function (exports) {
    winui.renderColor();
   var  $ = layui.$,
    devindex = layui.devindex,
    msg = winui.window.msg
    ,form = layui.form
   
    ;
  var $devId = wooutil.getUrlVar('Id');
  /**************************底部保存按钮---begin********************************************/
  //提交
  form.on('submit(dev-formAddrole)', function (data) {
   
    var fields = data.field;
    var postdata={};
    var modelIds=[];
    $.each(fields, function(key, val) {
        if(key=="Id"){
            postdata.RoleId=val;
        }else if(key.indexOf("limits")>=0){
            modelIds.push(val);
        }

        });
        postdata.ModelIds=modelIds.toString();//模块ID
  
       var tempdata=JSON.stringify(postdata);
      
    // //无赖之举目前没有更好办法，如果不这样制定一个int的id到后端API接收时对象为null
    // postdata.Id = $devId > 0 ? $devId : 0;
    //表单验证
 
        $.ajax({
            type: 'POST',
            url: devsetter.devbaseurl + 'api/DevSysModel/saverolemodel',
            //async: false,
            processData: false,
            data:tempdata,
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
    
    return false;
});
//取消
$("#quxiaobtn").click(function () {
    closeWin();
    return false;
});
/**关闭窗体 */
function closeWin() {
    top.winui.window.close('win_sysmodel');
}
/**提交成功 */
function submitsuccess(json) {
    if (json.Result) {
        msg('操作成功');
        closeWin();
       
    } else {
        msg(json.msg)
    }


}
  /**************************底部保存按钮-end******************************************************* */







/**
 * 赋值
 **/
function devSetValues() {

    if ($devId !== "" && $devId !== undefined) {
        $.ajax({
            type: 'GET',
            url: devsetter.devbaseurl + 'api/DevRole/showView',
            data: { Id: $devId },
            dataType: 'json',
            success: function (res) {
                form.val("Dev-RoleForm", res.data);
            
                $("Id").val(res.data.Id);
                
               
            },
            error: function (xml) {
                msg('加载失败!');

            }
        });


    } 
}
//执行赋值表单
devSetValues();
/*
*获取菜单权限
 */
function getModelChk(){
    var logdindex = layer.load(0, { shade: false });
    $.ajax({
        type: 'GET',
        url: devsetter.devbaseurl + 'api/DevSysModel/getmodelchecks?rand='+wooutil.getRandom(),
        data: { roleId: $devId },
        dataType: 'json',
        success: function (res) {
            
         var resultstr = "";
           for(var k=0;k<res.data.length;k++){
              var ditem=res.data[k];
            resultstr+='<ul class="layui-input-block" style="margin-top: 10px ;">'
            resultstr+='<li>'
            if(ditem.Chk){
                resultstr+='<input type="checkbox"  class="parent" name="limits[]" value="'+ditem.Id+'" lay-skin="primary" title="'+ditem.Name+'" checked />'

            }else{
                resultstr+='<input type="checkbox"  class="parent" name="limits[]" value="'+ditem.Id+'" lay-skin="primary" title="'+ditem.Name+'" />'
            }
            
            resultstr+='<ul>'
            if(ditem.ChildrenItem!=null&&ditem.ChildrenItem!=undefined){
            for(var i=0;i<ditem.ChildrenItem.length;i++){
                if(ditem.ChildrenItem[i].Chk){

                    resultstr+='<input type="checkbox" name="limits[]" value="'+ditem.ChildrenItem[i].Id+'" lay-skin="primary" title="'+ditem.ChildrenItem[i].Name+'" checked />'
                }else{
                    resultstr+='<input type="checkbox" name="limits[]" value="'+ditem.ChildrenItem[i].Id+'" lay-skin="primary" title="'+ditem.ChildrenItem[i].Name+'"/>'
                }
               
            }
        }
           
                resultstr+='</ul>' 
                resultstr+='</li>'
                resultstr+='</ul>'

              
        }
          $("#rolemodelchkdiv").html(resultstr);
          
          form.render();
          layer.close(logdindex);
        },
        error: function (xml) {
            msg('加载失败!');

        }
    });


}
getModelChk();

/******************选择--begin***********************************************************************/
form.on('checkbox()', function(data){
				
    var pc =  data.elem.classList //获取选中的checkbox的class属性
    
    /* checkbox处于选中状态  */
    if(data.elem.checked==true){//并且当前checkbox为选中状态
            /*如果是parent节点 */
            if(pc=="parent"){  //如果当前选中的checkbox class里面有parent 
                //获取当前checkbox的兄弟节点的孩子们是 input[type='checkbox']的元素
                var c =$(data.elem).siblings().children("input[type='checkbox']");
                 c.each(function(){//遍历他们的孩子们
                    var e = $(this); //添加layui的选中的样式   控制台看元素
                    e.next().addClass("layui-form-checked");
                    e.click();
               });
            }else{/*如果不是parent*/
                //选中子级选中父级
                $(data.elem).parent().prev().addClass("layui-form-checked");
               
            }
        
    }else{	/*checkbox处于 false状态*/
           
          //父级没有选中 取消所有的子级选中
          if(pc=="parent"){/*判断当前取消的是父级*/
            var c =$(data.elem).siblings().children("input[type='checkbox']");
             c.each(function(){
                var e = $(this); 
                e.next().removeClass("layui-form-checked")
             });
          }else{/*不是父级*/
                
                var c = $(data.elem).siblings("div"); 
                var count =0; 
                c.each(function(){//遍历他们的孩子们
                       //如果有一个==3那么久说明是处于选中状态
                        var is =  $(this).get(0).classList;
                        if(is.length==3){
                            count++;
                        }
                 });
                //如果大于0说明还有子级处于选中状态
                if(count>0){
                    
                }else{/*如果不大于那么就说明没有子级处于选中状态那么就移除父级的选中状态*/
                    $(data.elem).parent().prev().removeClass("layui-form-checked");
                }
          }
    }
});  
/*******************选择--end********************************************************************** */

  exports('setsysmodel', {});

})