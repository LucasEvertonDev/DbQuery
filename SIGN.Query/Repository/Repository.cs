﻿using SIGN.Query.Domains;
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
        public string IdInserido { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ExecuteQuery<T> Insert(T domain)
        {
            this._origin = typeof(InsertQuery<T>);
            this._domain = domain;
            return CreateDbQuery<InsertQuery<T>>().Add();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DeleteQuery<T> Delete()
        {
            this._origin = typeof(DeleteQuery<T>);
            return CreateDbQuery<DeleteQuery<T>>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public SelectQuery<T> Select()
        {
            this._origin = typeof(SelectQuery<T>);
            return CreateDbQuery<SelectQuery<T>>();
        }

       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public UpdateQuery<T> Update(T domain)
        {
            this._origin = typeof(UpdateQuery<T>);
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
            this._origin = typeof(InsertQuery<T>);
            this._domain = domain;

            var insert = CreateDbQuery<InsertNotExistsQuery<T>>();
            return insert.Add();
        }

        #region CustomSelect
        /// <summary>
        /// 
        /// </summary>
        /// <param name="top"></param>
        /// <returns></returns>
        public SelectQuery<T> Select(int? top)
        {
            this._origin = typeof(SelectQuery<T>);
            var select = CreateDbQuery<SelectQuery<T>>();
            select._top = top;
            return select;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public SelectCustomQuery<T> Select(Expression<Func<T, dynamic[]>> expression)
        {
            this._origin = typeof(SelectCustomQuery<T>);
            var select = CreateDbQuery<SelectCustomQuery<T>>();
            select._customExpression = expression;
            return select;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public SelectCustomQuery<T> Select(Expression<Func<T, dynamic>> expression)
        {
            this._origin = typeof(SelectCustomQuery<T>);
            var select = CreateDbQuery<SelectCustomQuery<T>>();
            select._customExpression = expression;
            return select;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="top"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public SelectCustomQuery<T> Select(int? top, Expression<Func<T, dynamic[]>> expression)
        {
            this._origin = typeof(SelectCustomQuery<T>);
            var select = CreateDbQuery<SelectCustomQuery<T>>();
            select._customExpression = expression;
            select._top = top;
            return select;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="top"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public SelectCustomQuery<T> Select(int? top, Expression<Func<T, dynamic>> expression)
        {
            this._origin = typeof(SelectCustomQuery<T>);
            var select = CreateDbQuery<SelectCustomQuery<T>>();
            select._customExpression = expression;
            select._top = top;
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
            this._origin = typeof(SelectCustomQuery<T>);
            var select = CreateDbQuery<SelectCustomQuery<T>>();
            select._customExpression = expression;
            return select;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="A"></typeparam>
        /// <typeparam name="B"></typeparam>
        /// <param name="top"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public SelectCustomQuery<T> Select<A, B>(int? top, Expression<Func<A, B, dynamic[]>> expression)
        {
            this._origin = typeof(SelectCustomQuery<T>);
            var select = CreateDbQuery<SelectCustomQuery<T>>();
            select._customExpression = expression;
            select._top = top;
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
            this._origin = typeof(SelectCustomQuery<T>);
            var select = CreateDbQuery<SelectCustomQuery<T>>();
            select._customExpression = expression;
            return select;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="A"></typeparam>
        /// <typeparam name="B"></typeparam>
        /// <typeparam name="C"></typeparam>
        /// <param name="top"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public SelectCustomQuery<T> Select<A, B, C>(int? top, Expression<Func<A, B, C, dynamic[]>> expression)
        {
            this._origin = typeof(SelectCustomQuery<T>);
            var select = CreateDbQuery<SelectCustomQuery<T>>();
            select._customExpression = expression;
            select._top = top;
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
            this._origin = typeof(SelectCustomQuery<T>);
            var select = CreateDbQuery<SelectCustomQuery<T>>();
            select._customExpression = expression;
            return select;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="A"></typeparam>
        /// <typeparam name="B"></typeparam>
        /// <typeparam name="C"></typeparam>
        /// <typeparam name="D"></typeparam>
        /// <param name="top"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public SelectCustomQuery<T> Select<A, B, C, D>(int? top, Expression<Func<A, B, C, D, dynamic[]>> expression)
        {
            this._origin = typeof(SelectCustomQuery<T>);
            var select = CreateDbQuery<SelectCustomQuery<T>>();
            select._customExpression = expression;
            select._top = top;
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
            this._origin = typeof(SelectCustomQuery<T>);
            var select = CreateDbQuery<SelectCustomQuery<T>>();
            select._customExpression = expression;
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
        /// <param name="top"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public SelectCustomQuery<T> Select<A, B, C, D, E>(int? top, Expression<Func<A, B, C, D, E, dynamic[]>> expression)
        {
            this._origin = typeof(SelectCustomQuery<T>);
            var select = CreateDbQuery<SelectCustomQuery<T>>();
            select._customExpression = expression;
            select._top = top;
            return select;
        }
        #endregion
    }
}
