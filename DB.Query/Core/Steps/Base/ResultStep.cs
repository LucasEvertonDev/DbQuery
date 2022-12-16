using DB.Query.Models.Entities;

namespace DB.Query.Core.Steps.Base
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class ResultStep<TEntity> : DBQuery<TEntity> where TEntity : EntityBase
    {
        /// <summary>
        /// 
        /// </summary>
        protected dynamic _databaseRetorno { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="databaseRetorno"></param>
        public ResultStep(dynamic databaseRetorno)
        {
            _databaseRetorno = databaseRetorno;
        }

        /// <summary>
        /// 
        /// </summary>
        public ResultStep()
        {
        }
    }
}
