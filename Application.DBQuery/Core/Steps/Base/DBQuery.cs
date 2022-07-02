using DBQuery.Core.Factory;
using DBQuery.Core.Model;
using DBQuery.Core.Services;
using DBQuery.Core.Transaction;
using Application.Domains.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBQuery.Core.Base
{
    public class DBQuery<TEntity> : IDBQuery where TEntity : EntityBase
    {
        protected List<DBQueryStepModel> _steps { get; set; }
        protected DbTransaction _transaction { get; set; }
        protected DBQueryLevelModelFactory _levelFactory { get; set; }
        public DBQuery()
        { 
            _transaction = new DbTransaction();
            _steps = new List<DBQueryStepModel>();
            _levelFactory = new DBQueryLevelModelFactory();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected TNextStep InstanceNextLevel<TNextStep>(DBQueryStepModel levelModel) where TNextStep : DBQuery<TEntity>
        {
            var dbQuery = Activator.CreateInstance<TNextStep>();
            dbQuery._steps = this._steps;
            dbQuery.IncludeStep(levelModel);
            dbQuery._transaction = this._transaction;
            dbQuery.StartLevel();
            return dbQuery;
        }

        /// <summary>
        /// 
        /// </summary>
        protected virtual void StartLevel()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dBQueryLevels"></param>
        protected void IncludeStep(DBQueryStepModel dBQueryLevels)
        {
            _steps.Add(dBQueryLevels);
        }

        /// <summary>
        /// /
        /// </summary>
        protected void VerifyChangeDataBase()
        {
            if (_transaction != null && _transaction.GetConnection() != null)
            {
                var databaseName = new InterpretService<TEntity>().GetDatabaseName(typeof(TEntity));
                if (!databaseName.Equals(_transaction.GetConnection().Database))
                {
                    _transaction.ChangeDatabase(databaseName);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        protected void ClearOldConfigurations()
        {
            this._transaction = null;
            this._steps.Clear();
        }
    }
}
