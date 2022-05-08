using Application.Domains.Entities;
using DBQuery.Core.Base;
using DBQuery.Core.Steps.SimpleSelectSteps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DBQuery.Core.Steps
{
    public class SelectAfterWhereStep<TEntity> : SelectOrderByStep<TEntity>, IPersistenceStep where TEntity : EntityBase
    {
      
    }
}
