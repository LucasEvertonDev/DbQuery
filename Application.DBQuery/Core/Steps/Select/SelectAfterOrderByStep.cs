using Application.Domains.Entities;
using DBQuery.Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBQuery.Core.Steps.SimpleSelectSteps
{
    public class SelectAfterOrderByStep<TEntity> : SelectOrderByStep<TEntity>, IPersistenceStep where TEntity : EntityBase
    {
    }
}
