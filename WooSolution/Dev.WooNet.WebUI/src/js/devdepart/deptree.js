layui.config({
    base: '../../lib/' //指定 winui 路径
    , version: '1.0.0-beta'
}).extend({
    winui: 'winui/winui',
    window: 'winui/js/winui.window',
    devindex: 'devextend/devindex'

}).define(['table', 'winui', 'window', 'layer', 'devindex', 'tree'], function (exports) {
    winui.renderColor();
    var table = layui.table,
        $ = layui.$,
        devindex = layui.devindex
    tree = layui.tree;

    $.ajax({
        type: 'GET',
        url: devsetter.devuserurl + 'api/DevDepart/deptTree',
        dataType: 'json',
        success: function (res) {
            data1 = res.data;
            tree.render({
                elem: '#depttree' //默认是点击节点可进行收缩
                , data: data1
            });


        },
        error: function (xml) {


        }
    });




    exports('departtree', {});
});