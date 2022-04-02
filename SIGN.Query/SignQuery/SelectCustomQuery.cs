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
            AddCollumns();
            return base.Where(expression);
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual void AddCollumns()
        {
            var props = GetPropertiesExpression(this._customExpression);
            _query = _query.Replace("*", string.Join(", ", props));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override ResultQuery<T> Execute()
        {
            AddCollumns();
            return base.Execute();
        }
    }
}
