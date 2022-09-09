using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace DB.Query.Core.Transaction
{
    public class DbTransaction
    {
        protected bool hasCommit { get; set; }
        protected SqlConnection _sqlConnection { get; set; }
        protected SqlTransaction _sqlTransaction { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="procedures"></param>
        public void OpenTransaction(string conexao)
        {
            _sqlConnection = new SqlConnection(conexao);
            _sqlConnection.Open();
            _sqlTransaction = _sqlConnection.BeginTransaction(Guid.NewGuid().ToString().Substring(0, 2));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="procedures"></param>
        public async Task OpenTransactionAsync(string conexao)
        {
            _sqlConnection = new SqlConnection(conexao);
            await _sqlConnection.OpenAsync();
            _sqlTransaction = _sqlConnection.BeginTransaction(Guid.NewGuid().ToString().Substring(0, 2));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public SqlTransaction GetTransaction()
        {
            return _sqlTransaction;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public SqlConnection GetConnection()
        {
            return _sqlConnection;
        }

        /// <summary>
        /// 
        /// </summary>
        public int Commit()
        {
            hasCommit = true;
            if (_sqlTransaction != null)
                _sqlTransaction.Commit();
            if (_sqlConnection != null)
                _sqlConnection.Close();

            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        public int Rollback()
        {
            if (_sqlTransaction != null)
                _sqlTransaction.Rollback();
            if (_sqlConnection != null)
                _sqlConnection.Close();

            return 0;
        }


        public void ChangeDatabase(string database)
        {
            _sqlConnection.ChangeDatabase(database);
        }
    }
}
