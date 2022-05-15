using DBQuery.Core.Base;
using DBQuery.Core.Services;
using Application.Domains.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBQuery.Core.Steps.SimpleSelectSteps
{
    public class SelectAfterOrderByStep<TEntity> : SelectOrderByStep<TEntity>, IPersistenceStep where TEntity : EntityBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public PersistenceStep<TEntity> Pagination(int pageSize, int pageNumber)
        {
            return InstanceNextLevel<PersistenceStep<TEntity>>(_levelFactory.PreparePaginationStep(pageSize, pageNumber));
        }
    }
}
