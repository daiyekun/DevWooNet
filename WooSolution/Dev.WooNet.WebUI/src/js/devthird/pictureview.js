/**
 * 图片预览
 */
 layui.config({
    base: '../../../lib/' //指定 winui 路径
    , version: '1.0.0-beta'
}).extend({
    winui: 'winui/winui',
    window: 'winui/js/winui.window',
    devindex: 'devextend/devindex'
    
}).define(['table', 'winui', 'window', 'layer', 'devindex'], function (exports) {
    winui.renderColor();
   
       var  $ = layui.$,
        devindex = layui.devindex,
        msg = winui.window.msg
        
        ,form = layui.form
         ;
    
    var CompId=wooutil.getUrlVar('CompId');
    

    function selectpic() {
        if (CompId !== "" && CompId !== undefined) {
            wooutil.devajax({
                type: 'GET',
                url: devsetter.devuserurl + 'api/DevCompFile/pictureview',
                //async: false,
                data: { CompId:CompId },
                dataType: 'json',
                success: function (res) {
                    wooutil.devloginout(res);
                    if(res.data.length>0){
                        var reshtml="";
                       for(var i=0;i<res.data.length;i++){
                        reshtml+='<li><img src="'+res.data[i].Url+'" alt="'+res.data[i].Name+'"></li>';
                       }
                       $("#devpiclist").html(reshtml);
                       form.render(null, 'DEV-CustomerPicForm');
                       $('#devpiclist').viewer();//必须放在此处
                    }
                   
                   
                },
                error: function (xml) {
                    msg('加载失败!');

                }
            });


        } 
    }
    //执行赋值表单
    selectpic();
    
    exports('pictureview', {});
});