using Dev.WooNet.Model.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Dev.WooNet.WooService
{
    /// <summary>
    /// 操作基础类
    /// </summary>
  public  abstract class BaseService<T>
        where T : class, new()
    {
        protected DevDbContext DevDb { get; private set; }
        public BaseService(DevDbContext context)
        {
            this.DevDb = context;
        }


        ///// <summary>
        ///// 无参数构造
        ///// </summary>
        public BaseService()
        {

        }
        /// <summary>
        /// 提交
        /// </summary>
        /// <returns></returns>
        public int SaveChanges()
        {

            return this.DevDb.SaveChanges();
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="Info"></param>
        /// <returns></returns>
        public T Add(T Info)
        {

            this.DevDb.Set<T>().Add(Info);
            this.SaveChanges();
            return Info;
        }
        /// <summary>
        /// 添加集合
        /// </summary>
        /// <param name="tList"></param>
        /// <returns></returns>
        public IEnumerable<T> Add(IEnumerable<T> tList)
        {
            this.DevDb.Set<T>().AddRange(tList);
            this.SaveChanges();//一个链接  多个sql
            return tList;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="Info">删除对象</param>
        /// <returns>true:成功/false:失败</returns>
        public bool Delete(T Info)
        {
            this.DevDb.Entry<T>(Info).State = EntityState.Deleted;

            return this.SaveChanges() > 0;

        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="predicate">表达式</param>
        /// <returns>true:成功,false:失败</returns>
        public bool Delete(Expression<Func<T, bool>> predicate)
        {
            var entitys = this.DevDb.Set<T>().Where(predicate).ToList();
            entitys.ForEach(m => DevDb.Entry<T>(m).State = EntityState.Deleted);
            return this.SaveChanges() > 0;


        }
        /// <summary>
        /// 修改单个对象
        /// </summary>
        /// <param name="Info">修改对象</param>
        /// <returns>true:成功/false:失败</returns>
        public bool Update(T Info)
        {
            this.DevDb.Entry<T>(Info).State = EntityState.Modified;
            return this.SaveChanges() > 0;

        }

        /// <summary>
        /// 修改集合
        /// </summary>
        /// <param name="tList"></param>
        /// <returns></returns>
        public bool Update(IEnumerable<T> tList)
        {
            foreach (var t in tList)
            {
                
                this.DevDb.Entry<T>(t).State = EntityState.Modified;
            }
            return this.SaveChanges() > 0;

        }
        /// <summary>
        /// 普通查询
        /// </summary>
        /// <param name="whereLambda">Where条件表达式</param>
        /// <returns></returns>
        public IQueryable<T> GetQueryable(Expression<Func<T, bool>> whereLambda)
        {
            return DevDb.Set<T>().Where(whereLambda).AsQueryable();
        }


        public int ExecuteSqlCommand(string strSql)
        {
            //return Db.Database.ExecuteSqlCommand(strSql);
            return DevDb.Database.ExecuteSqlRaw(strSql);
        }


        public int ExecuteSqlCommand(string strSql, DbParameter[] dbParameter)
        {
            return DevDb.Database.ExecuteSqlRaw(strSql, dbParameter);
        }
        /// <summary>
        /// 根据Ids批量删除
        /// </summary>
        /// <returns></returns>
        public int ExecuteDelSqlCommandByIds(string Ids)
        {
            string tablename = typeof(T).Name;
            string sqlstr = $"delete {tablename} where Id in({Ids})";
            return DevDb.Database.ExecuteSqlRaw(sqlstr);
        }
        /// <summary>
        /// 根据ID返回实体对象
        /// </summary>
        /// <param name="Id">当前ID(主键)</param>
        /// <returns>实体对象</returns>
        public T Find(int Id)
        {
            return DevDb.Set<T>().Find(Id);
        }

    }
}
