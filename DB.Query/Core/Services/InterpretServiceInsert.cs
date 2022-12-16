using DB.Query.Core.Constants;
using DB.Query.Core.Enuns;
using DB.Query.Core.Factorys;
using DB.Query.Models.Entities;
using System.Linq;

namespace DB.Query.Core.Services
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class InterpretInsertService<TEntity> : InterpretService<TEntity> where TEntity : EntityBase
    {
        /// <summary>
        /// 
        /// </summary>
        public InterpretInsertService() { }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override string RunInterpret()
        {
            var step = _levelModels.First();
            _domain = step.StepValue;

            IsValid(_domain);

            if (step.StepType == StepType.INSERT
                || step.StepType == StepType.INSERT_OR_UPDATE
                || step.StepType == StepType.DELETE_AND_INSERT)
            {
                return GenerateInsertScript();
            }
            else if (step.StepType == StepType.INSERT_NOT_EXISTS)
            {
                return GenerateInsertIfNotExistsScript();
            }
            return base.RunInterpret();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected string GenerateInsertScript()
        {
            _entityContext = new EntityAttributesModelFactory<TEntity>().InterpretEntity(_domain, true, _entityContext);

            var props = _entityContext.Props.Where(a => !a.Identity);

            var columns = props.Select(a => a.GetFullName(_entityContext.Name));
            var identityAttribute = _entityContext.Props.FirstOrDefault(a => a.Identity && a.PrimaryKey);

            var identity = identityAttribute == null ? string.Empty : string.Concat(DBKeysConstants.OUTPUT_INSERTED, identityAttribute.Name);
            var valores = props.Select(a => TreatValue(a.Valor, true));

            return string.Format(
                DBKeysConstants.INSERT,
                _entityContext.FullName,
                string.Join(", ", columns),
                identity,
                string.Join(", ", valores));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected string GenerateInsertIfNotExistsScript()
        {
            _entityContext = new EntityAttributesModelFactory<TEntity>().InterpretEntity(_domain, true, _entityContext);

            var insert = GenerateInsertScript();
            var objectClausules = _entityContext.Props.Where(a => !a.Identity).Select(a => string.Concat(a.Name, DBKeysConstants.EQUALS_WITH_SPACE, TreatValue(a.Valor, true)));

            return string.Format(
                DBKeysConstants.INSERT_NOT_EXISTS,
                _entityContext.FullName,
                DBKeysConstants.WHERE_WITH_SPACE + string.Join(DBKeysConstants.AND_WITH_SPACE, objectClausules),
                insert);
        }
    }
}
