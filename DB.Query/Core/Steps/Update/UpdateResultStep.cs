﻿using DB.Query.Models.Entities;
using DB.Query.Core.Steps.Base;

namespace DB.Query.Core.Steps.Update
{
    public class UpdateResultStep<TEntity> : ResultStep<TEntity> where TEntity : EntityBase
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="databaseRetorno"></param>
        public UpdateResultStep(dynamic databaseRetorno) : base()
        {
            _databaseRetorno = databaseRetorno;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int GetNumeroRegistrosAfetados()
        {
            if (_databaseRetorno != null)
            {
                if (_databaseRetorno.GetType() == typeof(int))
                {
                    return (int)_databaseRetorno;
                }
            }
            return 0;
        }
    }
}
