using DBQuery.Core.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DBQuery.Core.Model
{
    public class DBQueryStepModel
    {
        public StepType StepType { get; set; }
        public dynamic StepValue { get; set; }
        public Expression StepExpression { get; set; }
        public string Documentation { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
