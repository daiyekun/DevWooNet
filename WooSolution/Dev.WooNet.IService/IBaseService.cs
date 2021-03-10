﻿using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Dev.WooNet.IWooService
{
    /// <summary>
    /// 操作基础类库
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBaseService<T> where T : class, new()
    {
        /// <summary>
        /// 根据条件查询某张表
        /// </summary>
        /// <param name="whereLambda">where条件表达式</param>
        /// <returns>返回 IQueryable</returns>
        IQueryable<T> GetQueryable(Expression<Func<T, bool>> whereLambda);
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="info">实体对象</param>
        /// <returns>返回新增实体</returns>
        T Add(T info);
        /// <summary>
        /// 集合新增，即时Commit
        /// 多条sql 一个连接，事务插入
        /// </summary>
        /// <param name="tList">集合</param>
        /// <returns>返回带主键的集合</returns>
        IEnumerable<T> Add(IEnumerable<T> tList);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="info">删除的实体对象</param>
        /// <returns>true:成功，false：失败</returns>
        bool Delete(T info);
        /// <summary>
        /// 删除满足条件的数据
        /// </summary>
        /// <param name="whereLambda">where条件</param>
        /// <returns>删除成功</returns>
        bool Delete(Expression<Func<T, bool>> whereLambda);
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="info">更新实体</param>
        /// <returns>true:成功,false失败</returns>
        bool Update(T info);
        /// <summary>
        /// 更新数据，即时Commit
        /// </summary>
        /// <param name="tList"></param>
        bool Update(IEnumerable<T> tList);
        /// <summary>
        /// 执行不带参数新增，修改，删除SQL语句
        /// </summary>
        /// <param name="strSql">SQL语句</param>
        /// <returns>返回受影响的行数</returns>
        int ExecuteSqlCommand(string strSql);
        /// <summary>
        /// 执行不带参数新增，修改，删除SQL语句
        /// </summary>
        /// <param name="strSql">sql语句</param>
        /// <param name="dbParameter">参数</param>
        /// <returns>返回受影响的行数</returns>
        int ExecuteSqlCommand(string strSql, DbParameter[] dbParameter);
        /// <summary>
        /// 提交
        /// </summary>
        /// <returns></returns>
        int SaveChanges();
        /// <summary>
        /// 根据Ids批量删除
        /// </summary>
        /// <param name="Ids">Ids</param>
        /// <returns>受影响行数</returns>
        int ExecuteDelSqlCommandByIds(string Ids);
        /// <summary>
        /// 获取实体对象
        /// </summary>
        /// <param name="Id">当前ID(主键)</param>
        /// <returns>实体对象</returns>
        T Find(int Id);
    }
}
