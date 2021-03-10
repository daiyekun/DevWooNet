作用：数据库表与对象之间映射
执行命令生成实体：
//把项目启动项设置为Dev.WooNet.Model 否则会报 Microsoft.EntityFrameworkCore.Design..Tools没有引用
//3个Microsoft.EntityFrameworkCore 类库目前只支持5.0.3 否则生成实体报错“Missing required argument '<PROVIDER>'.”
Scaffold-DbContext "Server=localhost;port=3306;Database=devdb;Persist Security Info=True;User ID=sa;password=Sasa123;" Pomelo.EntityFrameworkCore.MySql -OutputDir Models -Context "DevDbContext"  -Force
