using SIGN.Query.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGN.Query.SignQuery
{
    public class SelectCountQuery<T> : SelectQuery<T> where T : SignQueryBase
    {
    }
}
