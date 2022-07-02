
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBQuery.Core.Extensions
{
    public static class SqlExtensions
    {
        /// <summary>
        /// Excuta o sql e retorna a datatable 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public static DataTable ExecuteSql(this SqlCommand Sql_Comando)
        {
            DataTable dataDados = new DataTable();
            SqlDataAdapter sql_Ada = new SqlDataAdapter(Sql_Comando);
            sql_Ada.Fill(dataDados);

            return dataDados;
        }
    }
}

