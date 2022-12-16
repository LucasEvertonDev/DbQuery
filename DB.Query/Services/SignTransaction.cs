using DB.Query.Models.DataAnnotations;
using DB.Query.Models.Entities;
using DB.Query.Models.Procedures;
using DB.Query.Core.Extensions;
using DB.Query.Repositorys;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using DB.Query.Core.Services;
using DB.Query.Models.Constants;
using DB.Query.Modelos.Procedures;
using DB.Query.Core.Models;

namespace DB.Query.Services
{
    public class DBTransaction
    {
        protected bool hasCommit { get; set; }
        protected bool hasRoolback { get; set; }
        protected SqlConnection _sqlConnection { get; set; }
        protected SqlTransaction _sqlTransaction { get; set; }
        protected bool _onDebug { get; set; }
        protected ConfigurationInputsModel _configuration { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="procedures"></param>
        public virtual void OpenTransaction(string conexao)
        {
            /// Apenas se não tiver transação corrente
            if ((_sqlConnection == null && _sqlTransaction == null) || _sqlConnection.State == ConnectionState.Closed)
            {
                _sqlConnection = new SqlConnection(conexao);
                _sqlConnection.Open();
                _sqlTransaction = _sqlConnection.BeginTransaction(Guid.NewGuid().ToString().Substring(0, 2));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="procedures"></param>
        public virtual async Task OpenTransactionAsync(string conexao)
        {
            /// Apenas se não tiver transação corrente
            if ((_sqlConnection == null && _sqlTransaction == null) || _sqlConnection.State == ConnectionState.Closed)
            {
                _sqlConnection = new SqlConnection(conexao);
                await _sqlConnection.OpenAsync();
                _sqlTransaction = _sqlConnection.BeginTransaction(Guid.NewGuid().ToString().Substring(0, 2));
            }
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
        /// <param name="transaction"></param>
        public void SetDbTransaction(SqlConnection connection, SqlTransaction transaction)
        {
            _sqlConnection = connection;
            _sqlTransaction = transaction;
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
            hasRoolback = true;
            if (_sqlTransaction != null)
                _sqlTransaction.Rollback();
            if (_sqlConnection != null)
                _sqlConnection.Close();

            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="database"></param>
        public void ChangeDatabase(string database)
        {
            _sqlConnection.ChangeDatabase(database);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool HasCommited()
        {
            return hasCommit;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool HasReversed()
        {
            return hasRoolback;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public virtual Repository<T> GetRepository<T>() where T : EntityBase
        {
            var repository = Activator.CreateInstance<Repository<T>>();
            repository.BindTransaction(this);
            return repository;
        }

        /// <summary>
        /// Utilizado para repositorios Especificos(funções unicas por tabela)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public virtual T GetSpecificRepository<T>()
        {
            var repository = Activator.CreateInstance<T>();
            MethodInfo m = repository.GetType().GetMethod("BindTransaction");
            m.Invoke(repository, new object[] { this });
            return (T)repository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="storedProcedureBase"></param>
        private void VerifyDatabaseStoredProcedure(StoredProcedureBase storedProcedureBase)
        {
            var type = storedProcedureBase.GetType();
            var database = type.GetCustomAttributes(typeof(DatabaseAttribute), true).FirstOrDefault() as DatabaseAttribute;
            ChangeDatabase(database.DatabaseName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="storedProcedureBase"></param>
        /// <returns></returns>
        public virtual int ExecuteNonQuery(StoredProcedureBase storedProcedureBase)
        {
            var command = CreateStoredProcedureCommand(storedProcedureBase);
            try
            {
                VerifyDatabaseStoredProcedure(storedProcedureBase);
                return command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new Exception($"Erro ao executat a query -> {command.PrintSql()}", e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="storedProcedureBase"></param>
        /// <returns></returns>
        public virtual int ExecuteScalar(StoredProcedureBase storedProcedureBase)
        {
            var command = CreateStoredProcedureCommand(storedProcedureBase);
            try
            {
                VerifyDatabaseStoredProcedure(storedProcedureBase);
                return Int32.Parse(command.ExecuteScalar().ToString());
            }
            catch (Exception e)
            {
                throw new Exception($"Erro ao executat a query -> {command.PrintSql()}", e);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="storedProcedureBase"></param>
        /// <returns></returns>
        public virtual DataTable ExecuteSql(StoredProcedureBase storedProcedureBase)
        {
            var command = CreateStoredProcedureCommand(storedProcedureBase);
            try
            {
                VerifyDatabaseStoredProcedure(storedProcedureBase);
                return command.ExecuteSql();
            }
            catch (Exception e)
            {
                throw new Exception($"Erro ao executat a query -> {command.PrintSql()}", e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="storedProcedureBase"></param>
        /// <returns></returns>
        public virtual List<T> ExecuteSql<T>(StoredProcedureBase storedProcedureBase)
        {
            var command = CreateStoredProcedureCommand(storedProcedureBase);
            try
            {
                VerifyDatabaseStoredProcedure(storedProcedureBase);
                return command.ExecuteSql().OfTypeProcedure<T>();
            }
            catch (Exception e)
            {
                throw new Exception($"Erro ao executat a query -> {command.PrintSql()}", e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="storedProcedureDTO"></param>
        /// <param name="dataBaseService"></param>
        /// <returns></returns>
        public virtual SqlCommand CreateStoredProcedureCommand(StoredProcedureBase storedProcedure)
        {
            var type = storedProcedure.GetType();
            var procedure = type.GetCustomAttributes(typeof(ProcedureAttribute), true).FirstOrDefault() as ProcedureAttribute;
            var timeout = type.GetCustomAttributes(typeof(TimeoutAttribute), true).FirstOrDefault() as TimeoutAttribute;

            var sqlCommad = new SqlCommand(procedure.ProcedureName, _sqlConnection, _sqlTransaction) { CommandType = CommandType.StoredProcedure };

            foreach (var inf in type.GetProperties().ToList())
            {
                if (inf.GetCustomAttributes(typeof(IgnoreAttribute), false).Count() == 0)
                {
                    var attr = inf.GetCustomAttributes(typeof(ParemeterAttribute), false).FirstOrDefault() as ParemeterAttribute;
                    if (attr != null)
                    {
                        sqlCommad.AddParameter(string.IsNullOrEmpty(attr.ParameterName) ? inf.Name : attr.ParameterName, attr.ParameterType, GetInputValue(inf, storedProcedure), attr.ParameterSize, attr.ParameterDirection);
                    }
                }
            }

            if (timeout != null)
            {
                sqlCommad.CommandTimeout = timeout.TimeOut;
            }

            if (_onDebug)
            {
                LogService.PrintQuery(sqlCommad.PrintSql());
            }

            return sqlCommad;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inf"></param>
        /// <returns></returns>
        protected virtual object GetInputValue(PropertyInfo inf, StoredProcedureBase storedProcedure)
        {
            var attr = inf.GetCustomAttributes(typeof(ConfigurationAttribute), false).FirstOrDefault() as ConfigurationAttribute;
            if (attr != null && _configuration != null )
            {
                //if (attr.Configuration == ConfigurationInputs.CodigoEmpresa)
                //{
                //    return _configuration.CodigoEmpresa;
                //}
                //if (attr.Configuration == ConfigurationInputs.CodigoFilial)
                //{
                //    return _configuration.CodigoFilial;
                //}
            }
            return inf.GetValue(storedProcedure);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        protected virtual void ApplyConfigurations(ConfigurationInputsModel configuration)
        {
            this._configuration = configuration;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool ExecutedInDebug()
        {
            return _onDebug;
        }
    }
}
