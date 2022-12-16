using DB.Query.Core.Constants;
using DB.Query.Core.Enuns;
using DB.Query.Core.Factorys;
using DB.Query.Models.Entities;
using System;
using System.Linq;

namespace DB.Query.Core.Services
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class InterpretDeleteService<TEntity> : InterpretService<TEntity> where TEntity : EntityBase
    {
        /// <summary>
        /// 
        /// </summary>
        public InterpretDeleteService() { }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override string RunInterpret()
        {
            var step = _levelModels.First();
            _domain = step.StepValue;
            if (step.StepType == StepType.DELETE)
            {
                return GenerateDeleteScript();
            }
            else if (step.StepType == StepType.DELETE_AND_INSERT)
            {
                IsValid(_domain);

                return GenerateDeleteAndInsertScript();
            }
            return base.RunInterpret();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected string GenerateDeleteScript()
        {
            var query = string.Format(
                DBKeysConstants.DELETE,
                GetFullName(typeof(TEntity)),
                string.Empty);

            var where = _levelModels.Where(step => step.StepType == StepType.WHERE).FirstOrDefault();
            if (where != null)
            {
                query += AddWhere(where.StepExpression);
            }
            return query;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected string GenerateDeleteAndInsertScript()
        {
            var insertQuery = Activator.CreateInstance<InterpretInsertService<TEntity>>().StartToInterpret(this._levelModels);
            return string.Concat(GenerateDeleteScript(), " ", insertQuery);
        }
    }
}
