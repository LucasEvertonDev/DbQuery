using SIGN.Query.Domains;
using SIGN.Query.Services;
using SIGN.Query.SignQuery;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SIGN.Query.Repository
{
    public class Repository<T> : SignQuery<T> where T : SignQueryBase
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ExecuteQuery<T> Insert(T domain)
        {
            this._isScalar = true;
            this._domain = domain;
            return CreateDbQuery<InsertQuery<T>>().Add();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DeleteQuery<T> Delete()
        {
            this._isScalar = true;
            return CreateDbQuery<DeleteQuery<T>>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public SelectQuery<T> Select()
        {
            var select = CreateDbQuery<SelectQuery<T>>();
            return select;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public UpdateQuery<T> Update(T domain)
        {
            this._isScalar = true;
            this._domain = domain;
            return CreateDbQuery<UpdateQuery<T>>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ExecuteQuery<T> InsertIfNotExists(T domain)
        {
            this._isScalar = true;
            this._domain = domain;

            return CreateDbQuery<InsertNotExistsQuery<T>>().Add();
        }

        #region CustomSelect
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public SelectCustomQuery<T> Select(Expression<Func<T, dynamic[]>> expression)
        {
            var select = CreateDbQuery<SelectCustomQuery<T>>();
            select.SetExpression(expression);
            return select;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public SelectCustomQuery<T> Select(Expression<Func<T, dynamic>> expression)
        {
            var select = CreateDbQuery<SelectCustomQuery<T>>();
            select.SetExpression(expression);
            return select;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="A"></typeparam>
        /// <typeparam name="B"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public SelectCustomQuery<T> Select<A>(Expression<Func<A, dynamic[]>> expression)
        {
            var select = CreateDbQuery<SelectCustomQuery<T>>();
            select.SetExpression(expression);
            return select;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="A"></typeparam>
        /// <typeparam name="B"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public SelectCustomQuery<T> Select<A, B>(Expression<Func<A, B, dynamic[]>> expression)
        {
            var select = CreateDbQuery<SelectCustomQuery<T>>();
            select.SetExpression(expression);
            return select;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="A"></typeparam>
        /// <typeparam name="B"></typeparam>
        /// <typeparam name="C"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public SelectCustomQuery<T> Select<A, B, C>(Expression<Func<A, B, C, dynamic[]>> expression)
        {
            var select = CreateDbQuery<SelectCustomQuery<T>>();
            select.SetExpression(expression);
            return select;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="A"></typeparam>
        /// <typeparam name="B"></typeparam>
        /// <typeparam name="C"></typeparam>
        /// <typeparam name="D"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public SelectCustomQuery<T> Select<A, B, C, D>(Expression<Func<A, B, C, D, dynamic[]>> expression)
        {
            var select = CreateDbQuery<SelectCustomQuery<T>>();
            select.SetExpression(expression);
            return select;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="A"></typeparam>
        /// <typeparam name="B"></typeparam>
        /// <typeparam name="C"></typeparam>
        /// <typeparam name="D"></typeparam>
        /// <typeparam name="E"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public SelectCustomQuery<T> Select<A, B, C, D, E>(Expression<Func<A, B, C, D, E, dynamic[]>> expression)
        {
            var select = CreateDbQuery<SelectCustomQuery<T>>();
            select.SetExpression(expression);
            return select;
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Repository<T> UseAlias(string alias)
        {
            _alias = alias;
            return this;
        }
    }
}
