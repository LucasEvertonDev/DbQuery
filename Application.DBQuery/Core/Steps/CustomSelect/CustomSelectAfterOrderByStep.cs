using DBQuery.Core.Base;
using Application.Domains.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DBQuery.Core.Steps.CustomSelects
{
    public class CustomSelectAfterOrderByStep<TEntity> : CustomSelectOrderByStep<TEntity>, IPersistenceStep where TEntity : EntityBase
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
