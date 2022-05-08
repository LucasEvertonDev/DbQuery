using Application.Domains.Entities;
using DBQuery.Core.Factory;
using DBQuery.Core.Model;
using DBQuery.Core.Services;
using DBQuery.Core.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBQuery.Core.Base
{
    public class DBQuery<TEntity> : IDBQuery where TEntity : EntityBase
    {
        protected List<DBQueryLevelModel> _steps { get; set; }
        protected DbTransaction _transaction { get; set; }
        protected DBQueryLevelModelFactory _levelFactory { get; set; }
        public DBQuery()
        { 
            _transaction = new DbTransaction();
            _steps = new List<DBQueryLevelModel>();
            _levelFactory = new DBQueryLevelModelFactory();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected TNextStep InstanceNextLevel<TNextStep>(DBQueryLevelModel levelModel) where TNextStep : DBQuery<TEntity>
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
        protected void IncludeStep(DBQueryLevelModel dBQueryLevels)
        {
            _steps.Add(dBQueryLevels);
        }

        /// <summary>
        /// /
        /// </summary>
        protected void VerifyChangeDataBase()
        {
            if (_transaction != null)
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
        /// <param name="dataBaseService"></param>
        public void BindTransaction(DbTransaction dataBaseService)
        {
            this._transaction = dataBaseService;
        }

        /// <summary>
        /// 
        /// </summary>
        public void ClearOldConfigurations()
        {
            this._transaction = null;
            this._steps.Clear();
        }
    }
}
