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
        public string IdInserido { get; set; }

        /// <summary>
        /// A partir das expressions são montadas strings que serão covertidas em sql, é obrigatório o uso da propriedade antes da comparação 
        /// EX: Table.Collumn = "teste"
        /// E proibido o contrário:
        /// EX: "teste" = Table.Collumn
        /// Caso necessite do sql gerado pode navegar para a classe ExecuteQuery no método ExecuteSql
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ExecuteQuery<T> Insert(T domain)
        {
            this.Origin = typeof(InsertQuery<T>);
            this.Domain = domain;
            return CreateDbQuery<InsertQuery<T>>().Add();
        }

        /// <summary>
        /// A partir das expressions são montadas strings que serão covertidas em sql, é obrigatório o uso da propriedade antes da comparação 
        /// EX: Table.Collumn = "teste"
        /// E proibido o contrário:
        /// EX: "teste" = Table.Collumn
        /// Caso necessite do sql gerado pode navegar para a classe ExecuteQuery no método ExecuteSql
        /// </summary>
        /// <returns></returns>
        public DeleteQuery<T> Delete()
        {
            this.Origin = typeof(DeleteQuery<T>);
            return CreateDbQuery<DeleteQuery<T>>();
        }

        /// <summary>
        /// A partir das expressions são montadas strings que serão covertidas em sql, é obrigatório o uso da propriedade antes da comparação 
        /// EX: Table.Collumn = "teste"
        /// E proibido o contrário:
        /// EX: "teste" = Table.Collumn
        /// Caso necessite do sql gerado pode navegar para a classe ExecuteQuery no método ExecuteSql
        /// </summary>
        /// <returns></returns>
        public SelectQuery<T> Select()
        {
            this.Origin = typeof(SelectQuery<T>);
            return CreateDbQuery<SelectQuery<T>>();
        }

        /// <summary>
        /// A partir das expressions são montadas strings que serão covertidas em sql, é obrigatório o uso da propriedade antes da comparação 
        /// EX: Table.Collumn = "teste"
        /// E proibido o contrário:
        /// EX: "teste" = Table.Collumn
        /// Caso necessite do sql gerado pode navegar para a classe ExecuteQuery no método ExecuteSql
        /// </summary>
        /// <returns></returns>
        public SelectQuery<T> Select(int? top)
        {
            this.Origin = typeof(SelectQuery<T>);
            var select = CreateDbQuery<SelectQuery<T>>();
            select._top = top;
            return select;
        }

        // <summary>
        /// A partir das expressions são montadas strings que serão covertidas em sql, é obrigatório o uso da propriedade antes da comparação 
        /// EX: Table.Collumn = "teste"
        /// E proibido o contrário:
        /// EX: "teste" = Table.Collumn
        /// Caso necessite do sql gerado pode navegar para a classe ExecuteQuery no método ExecuteSql
        /// </summary>
        /// <returns></returns>
        public SelectCustomQuery<T> Select(Expression<Func<T, dynamic[]>> expression)
        {
            this.Origin = typeof(SelectCustomQuery<T>);
            var select = CreateDbQuery<SelectCustomQuery<T>>();
            select.CustomExpression = expression;
            return select;
        }

        // <summary>
        /// A partir das expressions são montadas strings que serão covertidas em sql, é obrigatório o uso da propriedade antes da comparação 
        /// EX: Table.Collumn = "teste"
        /// E proibido o contrário:
        /// EX: "teste" = Table.Collumn
        /// Caso necessite do sql gerado pode navegar para a classe ExecuteQuery no método ExecuteSql
        /// </summary>
        /// <returns></returns>
        public SelectCustomQuery<T> Select(Func<dynamic> func)
        {
            Expression<Func<dynamic>> DangerousExpression = () => func;
            this.Origin = typeof(SelectCustomQuery<T>);
            var select = CreateDbQuery<SelectCustomQuery<T>>();
            select.CustomExpression = DangerousExpression;
            return select;
        }

        // <summary>
        /// A partir das expressions são montadas strings que serão covertidas em sql, é obrigatório o uso da propriedade antes da comparação 
        /// EX: Table.Collumn = "teste"
        /// E proibido o contrário:
        /// EX: "teste" = Table.Collumn
        /// Caso necessite do sql gerado pode navegar para a classe ExecuteQuery no método ExecuteSql
        /// </summary>
        /// <returns></returns>
        public SelectCustomQuery<T> Select(int? top, Expression<Func<T, dynamic[]>> expression)
        {
            this.Origin = typeof(SelectCustomQuery<T>);
            var select = CreateDbQuery<SelectCustomQuery<T>>();
            select.CustomExpression = expression;
            select._top = top;
            return select;
        }



        /// <summary>
        /// A partir das expressions são montadas strings que serão covertidas em sql, é obrigatório o uso da propriedade antes da comparação 
        /// EX: Table.Collumn = "teste"
        /// E proibido o contrário:
        /// EX: "teste" = Table.Collumn
        /// Caso necessite do sql gerado pode navegar para a classe ExecuteQuery no método ExecuteSql
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public UpdateQuery<T> Update(T domain)
        {
            this.Origin = typeof(UpdateQuery<T>);
            this.Domain = domain;
            return CreateDbQuery<UpdateQuery<T>>();
        }

        /// <summary>
        /// A partir das expressions são montadas strings que serão covertidas em sql, é obrigatório o uso da propriedade antes da comparação 
        /// EX: Table.Collumn = "teste"
        /// E proibido o contrário:
        /// EX: "teste" = Table.Collumn
        /// Caso necessite do sql gerado pode navegar para a classe ExecuteQuery no método ExecuteSql
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ExecuteQuery<T> InsertIfNotExists(T domain)
        {
            this.Origin = typeof(InsertQuery<T>);
            this.Domain = domain;

            var insert = CreateDbQuery<InsertQuery<T>>();
            insert.InsertIfNotExists();
            return insert.Add();
        }


        // <summary>
        /// A partir das expressions são montadas strings que serão covertidas em sql, é obrigatório o uso da propriedade antes da comparação 
        /// EX: Table.Collumn = "teste"
        /// E proibido o contrário:
        /// EX: "teste" = Table.Collumn
        /// Caso necessite do sql gerado pode navegar para a classe ExecuteQuery no método ExecuteSql
        /// </summary>
        /// <returns></returns>
        public SelectCustomQuery<T> Select<A, B>(Expression<Func<A, B, dynamic[]>> expression)
        {
            this.Origin = typeof(SelectCustomQuery<T>);
            var select = CreateDbQuery<SelectCustomQuery<T>>();
            select.CustomExpression = expression;
            return select;
        }

        // <summary>
        /// A partir das expressions são montadas strings que serão covertidas em sql, é obrigatório o uso da propriedade antes da comparação 
        /// EX: Table.Collumn = "teste"
        /// E proibido o contrário:
        /// EX: "teste" = Table.Collumn
        /// Caso necessite do sql gerado pode navegar para a classe ExecuteQuery no método ExecuteSql
        /// </summary>
        /// <returns></returns>
        public SelectCustomQuery<T> Select<A, B>(int? top, Expression<Func<A, B, dynamic[]>> expression)
        {
            this.Origin = typeof(SelectCustomQuery<T>);
            var select = CreateDbQuery<SelectCustomQuery<T>>();
            select.CustomExpression = expression;
            select._top = top;
            return select;
        }



        // <summary>
        /// A partir das expressions são montadas strings que serão covertidas em sql, é obrigatório o uso da propriedade antes da comparação 
        /// EX: Table.Collumn = "teste"
        /// E proibido o contrário:
        /// EX: "teste" = Table.Collumn
        /// Caso necessite do sql gerado pode navegar para a classe ExecuteQuery no método ExecuteSql
        /// </summary>
        /// <returns></returns>
        public SelectCustomQuery<T> Select<A, B, C>(Expression<Func<A, B, C, dynamic[]>> expression)
        {
            this.Origin = typeof(SelectCustomQuery<T>);
            var select = CreateDbQuery<SelectCustomQuery<T>>();
            select.CustomExpression = expression;
            return select;
        }

        // <summary>
        /// A partir das expressions são montadas strings que serão covertidas em sql, é obrigatório o uso da propriedade antes da comparação 
        /// EX: Table.Collumn = "teste"
        /// E proibido o contrário:
        /// EX: "teste" = Table.Collumn
        /// Caso necessite do sql gerado pode navegar para a classe ExecuteQuery no método ExecuteSql
        /// </summary>
        /// <returns></returns>
        public SelectCustomQuery<T> Select<A, B, C>(int? top, Expression<Func<A, B, C, dynamic[]>> expression)
        {
            this.Origin = typeof(SelectCustomQuery<T>);
            var select = CreateDbQuery<SelectCustomQuery<T>>();
            select.CustomExpression = expression;
            select._top = top;
            return select;
        }


        // <summary>
        /// A partir das expressions são montadas strings que serão covertidas em sql, é obrigatório o uso da propriedade antes da comparação 
        /// EX: Table.Collumn = "teste"
        /// E proibido o contrário:
        /// EX: "teste" = Table.Collumn
        /// Caso necessite do sql gerado pode navegar para a classe ExecuteQuery no método ExecuteSql
        /// </summary>
        /// <returns></returns>
        public SelectCustomQuery<T> Select<A, B, C, D>(Expression<Func<A, B, C, D, dynamic[]>> expression)
        {
            this.Origin = typeof(SelectCustomQuery<T>);
            var select = CreateDbQuery<SelectCustomQuery<T>>();
            select.CustomExpression = expression;
            return select;
        }

        // <summary>
        /// A partir das expressions são montadas strings que serão covertidas em sql, é obrigatório o uso da propriedade antes da comparação 
        /// EX: Table.Collumn = "teste"
        /// E proibido o contrário:
        /// EX: "teste" = Table.Collumn
        /// Caso necessite do sql gerado pode navegar para a classe ExecuteQuery no método ExecuteSql
        /// </summary>
        /// <returns></returns>
        public SelectCustomQuery<T> Select<A, B, C, D>(int? top, Expression<Func<A, B, C, D, dynamic[]>> expression)
        {
            this.Origin = typeof(SelectCustomQuery<T>);
            var select = CreateDbQuery<SelectCustomQuery<T>>();
            select.CustomExpression = expression;
            select._top = top;
            return select;
        }

        // <summary>
        /// A partir das expressions são montadas strings que serão covertidas em sql, é obrigatório o uso da propriedade antes da comparação 
        /// EX: Table.Collumn = "teste"
        /// E proibido o contrário:
        /// EX: "teste" = Table.Collumn
        /// Caso necessite do sql gerado pode navegar para a classe ExecuteQuery no método ExecuteSql
        /// </summary>
        /// <returns></returns>
        public SelectCustomQuery<T> Select<A, B, C, D, E>(Expression<Func<A, B, C, D, E, dynamic[]>> expression)
        {
            this.Origin = typeof(SelectCustomQuery<T>);
            var select = CreateDbQuery<SelectCustomQuery<T>>();
            select.CustomExpression = expression;
            return select;
        }

        // <summary>
        /// A partir das expressions são montadas strings que serão covertidas em sql, é obrigatório o uso da propriedade antes da comparação 
        /// EX: Table.Collumn = "teste"
        /// E proibido o contrário:
        /// EX: "teste" = Table.Collumn
        /// Caso necessite do sql gerado pode navegar para a classe ExecuteQuery no método ExecuteSql
        /// </summary>
        /// <returns></returns>
        public SelectCustomQuery<T> Select<A, B, C, D, E>(int? top, Expression<Func<A, B, C, D, E, dynamic[]>> expression)
        {
            this.Origin = typeof(SelectCustomQuery<T>);
            var select = CreateDbQuery<SelectCustomQuery<T>>();
            select.CustomExpression = expression;
            select._top = top;
            return select;
        }

    }
}
