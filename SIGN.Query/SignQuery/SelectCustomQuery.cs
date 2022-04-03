using SIGN.Query.Constants;
using SIGN.Query.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SIGN.Query.SignQuery
{
    public class SelectCustomQuery<T> : SelectQuery<T> where T : SignQueryBase
    {
        public Expression _customExpression { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public override SelectExecuteQuery<T> Where(Expression<Func<T, bool>> expression = null)
        {
            PreRoutine();
            return base.Where(expression);
        }

        #region Group BY
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual OrderByQuery<T> GroupBy(Expression<Func<T, dynamic[]>> expression)
        {
            PreRoutine();
            return AddGroupBy(expression);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual OrderByQuery<T> GroupBy(Expression<Func<T, dynamic>> expression)
        {
            PreRoutine();
            return AddGroupBy(expression);
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        public virtual void AddCollumns()
        {
            var props = GetPropertiesExpression(this._customExpression, useAlias: true);
            _query = _query.Replace(SQLKeys.DISTINCT_ALL, SQLKeys.DISTINCT_WITH_SPACE + string.Join(", ", props));
        }

        /// <summary>
        /// 
        /// </summary>
        public override void PreRoutine()
        {
            base.PreRoutine();
            AddCollumns();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override ResultQuery<T> Execute()
        {
            PreRoutine();
            return base.Execute();
        }
    }
}
