using Application.Domains.Entities;
using DBQuery.Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBQuery.Core.Steps.SelectSteps
{
    public class CustomSelectAfterTopStep<TEntity> : CustomSelectBaseStep<TEntity>, IPersistenceStep where TEntity : EntityBase
    {
    }
}
