using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Query.Modelos.Procedures
{
    /// <summary>
    /// TReturnType indica o tipo de valor tratado a ser retornado.
    /// Sendo lista ou datatable
    /// </summary>
    /// <typeparam name="TReturnType"></typeparam>
    public class PSStoredProcedure<TReturnType> : StoredProcedureBase where TReturnType : class
    {
    }
}
