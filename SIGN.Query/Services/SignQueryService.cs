using SIGN.Query.Domains;
using SIGN.Query.Repository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGN.Query.Services
{
    public class SignQueryService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="func"></param>
        public static void OnTransaction(string connection, Action<SignTransaction> func)
        {
            var _transaction = Activator.CreateInstance<SignTransaction>();
            try
            {
                _transaction.OpenTransaction(connection);

                func(_transaction);
            }
            catch (Exception e)
            {
                _transaction.Rollback();
                throw e;
            }
            finally
            {
                if (_transaction.GetConnection() != null)
                    _transaction.GetConnection().Close();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="func"></param>
        public void OnTransaction_DB(string connection, Action<SignTransaction> func)
        {
            OnTransaction(connection, func);
        }
    }
}
