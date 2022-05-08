using DBQuery.Core.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DBQuery.Core.Model
{
    public class DBQueryLevelModel
    {
        public StepType LevelType { get; set; }
        public dynamic LevelValue { get; set; }
        public Expression LevelExpression { get; set; }
        public string Documentation { get; set; }
    }
}
