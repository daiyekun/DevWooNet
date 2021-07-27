layui.define(['table', 'form'], function (exports) {
    var $ = layui.$
 //, table = layui.table
 //, setter = layui.setter
 , layer = layui.layer
 , admin = layui.admin
 , form = layui.form;

 var localdata=wooutil.devlocaldata();
 var devselitem={//选择项
    selectUserItem: function (param) {
        /// <summary>
        /// 选择用户
        /// </summary>        
        /// <param name="elem" type="String">触发此动作的文本框</param>
        /// <param name="tableSelect" type="Object">TableSelect对象</param>
        /// <param name="hide_elem" type="number">隐藏文本框ID-主要为了存储后台数据库</param>
        /// <param name="suc" type="Function">回调函数，用于扩展业务需要除名称和ID以外的内容</param>
        /// <param name="seltype" type="string">回调函数，用于扩展业务需要除名称和ID以外的内容</param>
        ///<param name="noval" type="string">标识不赋值，交给回调函数处理</param>
        var _seltype = 'radio';
        if (param.seltype != undefined) {
            _seltype = param.seltype;
        }
        param.tableSelect.render({
            elem: param.elem,
            searchKey: 'keyword',
            searchPlaceholder: '关键词搜索',
            table: {
                method:'POST',
                contentType:'application/json',
                headers: {
                    "Authorization": "Bearer "+ localdata.token +""
                    ,loginkey:localdata.loginkey
                },
                url:devsetter.devuserurl+"api/DevUser/list?searchType=2&rand="+wooutil.getRandom(), //'/System/UserInfor/GetList?ISQy=' + 1 +'&rand=' + wooutil.getRandom(),
                cols: [[
                    { type: 'numbers', fixed: 'left' }
                    , { type: _seltype, fixed: 'left' },
                    { field: 'Id', width: 80, title: 'ID', hide: true },
                    { field: 'Name', title: '用户名', width: 130, fixed: 'left' },
                    { field: 'DeptName', title: '所属部门', width: 150 },
                    { field: 'ShowName', title: '显示名称', width: 140 },
                    { field: 'SexDic', title: '性别', width: 80,hide: true },
                    { field: 'Tel', title: '电话', width: 110,hide: true },
                    { field: 'Mobile', title: '手机', width: 120 },
                    { field: 'Email', title: '邮箱', width: 130,hide: true },
                ]]
            },
            done: function (elem, data) {
                if (data.isclear) {
                    $(param.elem).val('');
                    $(param.hide_elem).val('');
                } else {
                    if (param.noval == undefined) {
                        $(param.elem).val(data.data[0].ShowName);

                        $(param.hide_elem).val(data.data[0].Id);
                    }
                    if (typeof param.suc === 'function') {
                        param.suc(data.data);
                    }


                }
            }
        });
    }


 }


 exports('devselitem', devselitem);

});
