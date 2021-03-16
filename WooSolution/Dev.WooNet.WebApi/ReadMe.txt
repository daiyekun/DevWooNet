﻿作用：对外数据接口WEBAPI
dotnet Dev.WooNet.WebAPI.dll --urls="http://*:8059" --ip="127.0.0.1" --port=8059

提交代码命令：
方法1：
1. 将远程仓库克隆到本地
git clone https://gitee.com/abc/aaa.git

2. 添加或修改本地文件

3. 将本地代码push到远程仓库
git add .                    # 将当前目录所有文件添加到git暂存区
git commit -m '注释'         # 提交并备注提交信息
git push origin master       # 将本地提交，推送到远程仓库

方法2：目前是采用这种，其实第一种也不错

1. 初始化仓库、连接远程仓库、将远程仓库代码拉取到本地
git init   
git remote add origin https://gitee.com/abc/aaa.git    
git pull origin master

2. 添加或修改本地文件
（第一步搞定以后，每次修改代码都是操作步骤3）

3. 将本地代码push到远程仓库
git add .  
git commit -m '注释'
git push origin master