using DBQuery.Core.Base;
using Application.Domains.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBQuery.Core.Steps
{
    public class InsertStep<TEntity> : PersistenceStep<TEntity>, IPersistenceStep where TEntity : EntityBase
    { 

    }
}
