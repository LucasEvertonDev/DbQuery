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
    public class InterpretUpdateService<TEntity> : InterpretService<TEntity> where TEntity : EntityBase
    {
        /// <summary>
        /// 
        /// </summary>
        public InterpretUpdateService() { }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override string RunInterpret()
        {
            var step = _levelModels.First();
            _domain = step.StepValue;

            if (step.StepType == StepType.UPDATE)
            {
                return GenerateUpdateScript();
            }
            else if (step.StepType == StepType.INSERT_OR_UPDATE)
            {
                return GenerateInsertOrUpdateScript();
            }
            return base.RunInterpret();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected string GenerateUpdateScript()
        {
            var query = string.Empty;
            var stepSet = _levelModels.Where(step => step.StepType == StepType.UPDATE_SET).FirstOrDefault();

            _entityContext = new EntityAttributesModelFactory<TEntity>().InterpretEntity(_domain, true, _entityContext);

            if (stepSet != null)
            {
                var sets = GetPropertiesExpression(stepSet.StepExpression);
                sets = sets.Select(s => s.Split('.')[1]).ToList();

                var objectClausules = _entityContext.Props.Where(a => !a.Identity && sets.Contains(a.Name))
                        .Select(a => string.Concat(a.Name, DBKeysConstants.EQUALS_WITH_SPACE, TreatValue(a.Valor, true)));

                query = string.Format(
                    DBKeysConstants.UPDATE,
                    _entityContext.FullName,
                    string.Join(", ", objectClausules),
                    string.Empty);
            }
            else
            {
                IsValid(_domain);

                var objectClausules = _entityContext.Props.Where(a => !a.Identity).Select(a => string.Concat(a.Name, DBKeysConstants.EQUALS_WITH_SPACE, TreatValue(a.Valor, true)));

                query = string.Format(
                    DBKeysConstants.UPDATE,
                    _entityContext.FullName,
                    string.Join(", ", objectClausules),
                    string.Empty);
            }

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
        protected string GenerateInsertOrUpdateScript()
        {
            _entityContext = new EntityAttributesModelFactory<TEntity>().InterpretEntity(_domain, true, _entityContext);

            var insertQuery = Activator.CreateInstance<InterpretInsertService<TEntity>>().StartToInterpret(this._levelModels);

            var query = string.Format(
                DBKeysConstants.INSERT_NOT_EXISTS_ELSE_ACTION,
                _entityContext.FullName,
                AddWhere(_levelModels.Where(step => step.StepType == StepType.WHERE).First().StepExpression),
                insertQuery,
                GenerateUpdateScript()
            );
            return query;
        }
    }
}