using DB.Query.Core.Constants;
using DB.Query.Core.Enuns;
using DB.Query.Models.Entities;
using System.Linq;
using System;

namespace DB.Query.Core.Services
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class InterpretSelectService<TEntity> : InterpretService<TEntity> where TEntity : EntityBase
    {
        /// <summary>
        /// 
        /// </summary>
        public InterpretSelectService() { }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override string RunInterpret()
        {
            return GenerateSelectScript();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected string GenerateSelectScript()
        {
            var query = string.Empty;
            foreach (var step in _levelModels)
            {
                if (step.StepType == StepType.SELECT || step.StepType == StepType.CUSTOM_SELECT)
                {
                    query += string.Format(
                        DBKeysConstants.SELECT,
                        DBKeysConstants.ALL_COLUMNS,
                        GetFullName(typeof(TEntity)),
                        string.IsNullOrEmpty(_alias) ? string.Empty : DBKeysConstants.AS_WITH_SPACE + _alias + " ");

                    if (step.StepType == StepType.CUSTOM_SELECT)
                    {
                        var props = GetPropertiesExpression(step.StepExpression, useAlias: true, fromSelect: true);
                        query = query.Replace(DBKeysConstants.SELECT_ALL, DBKeysConstants.SELECT_KEY + " " + string.Join(", ", props));
                    }
                }
                else if (step.StepType == StepType.DISTINCT)
                {
                    query = query.Replace(DBKeysConstants.SELECT_KEY, DBKeysConstants.SELECT_DISTINCT);
                }
                else if (step.StepType == StepType.TOP)
                {
                    if (query.Contains(DBKeysConstants.SELECT_DISTINCT))
                    {
                        query = query.Replace(DBKeysConstants.SELECT_DISTINCT, string.Format(DBKeysConstants.SELECT_DISTINCT_TOP, step.StepValue));
                    }
                    else
                    {
                        query = query.Replace(DBKeysConstants.SELECT_KEY, string.Format(DBKeysConstants.SELECT_TOP, step.StepValue));
                    }
                }
                else if (step.StepType == StepType.WHERE)
                {
                    query += AddWhere(step.StepExpression);
                }
                else if (step.StepType == StepType.JOIN)
                {
                    query += AddJoin(step.StepExpression, DBKeysConstants.INNER_JOIN);
                }
                else if (step.StepType == StepType.LEFT_JOIN)
                {
                    query += AddJoin(step.StepExpression, DBKeysConstants.LEFT_JOIN);
                }
                else if (step.StepType == StepType.ORDER_BY_ASC)
                {
                    query = AddOrderBy(DBKeysConstants.ASC, step.StepExpression, query);
                }
                else if (step.StepType == StepType.ORDER_BY_DESC)
                {
                    query = AddOrderBy(DBKeysConstants.DESC, step.StepExpression, query);
                }
                else if (step.StepType == StepType.GROUP_BY)
                {
                    query = AddGroupBy(step.StepExpression, query);
                }
                else if (step.StepType == StepType.PAGINATION)
                {
                    query += string.Format(DBKeysConstants.OFFSET, step.PageSize * (step.PageNumber - 1), step.PageSize);
                }
            }
            return query;
        }
    }
}
