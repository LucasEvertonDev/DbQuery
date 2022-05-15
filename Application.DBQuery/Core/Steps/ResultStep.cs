using DBQuery.Core.Base;
using DBQuery.Core.Extensions;
using Application.Domains.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBQuery.Core.Steps
{
    public class ResultStep<TEntity> : DBQuery<TEntity>, IPersistenceStep where TEntity : EntityBase
    {
        /// <summary>
        /// 
        /// </summary>
        private dynamic _databaseRetorno { get; set; }

        public ResultStep(dynamic databaseRetorno)
        {
            this._databaseRetorno = databaseRetorno;
        }
        

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataTable GetDtRetorno()
        {
            if (_databaseRetorno != null)
            {
                if (_databaseRetorno.GetType() == typeof(DataTable))
                    return (DataTable)_databaseRetorno;
                else
                {
                    var dataRetorno = new DataTable();
                    dataRetorno.AddColluns("Output");
                    dataRetorno.Rows.Add(_databaseRetorno);
                }
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<TEntity> GetItens()
        {
            if (_databaseRetorno != null)
            {
                if (_databaseRetorno.GetType() == typeof(DataTable))
                {
                    return ((DataTable)_databaseRetorno).OfType<TEntity>();
                }
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<P> GetItens<P>()
        {
            if (_databaseRetorno != null)
            {
                if (_databaseRetorno.GetType() == typeof(DataTable))
                {
                    return ((DataTable)_databaseRetorno).OfType<P>();
                }
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public TEntity  GetItem()
        {
            if (_databaseRetorno != null)
            {
                if (_databaseRetorno.GetType() == typeof(DataTable))
                {
                    return ((DataTable)_databaseRetorno).OfType<TEntity>().FirstOrDefault();
                }
            }
            return default(TEntity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public P GetItem<P>()
        {
            if (_databaseRetorno != null)
            {
                if (_databaseRetorno.GetType() == typeof(DataTable))
                {
                    return ((DataTable)_databaseRetorno).OfType<P>().FirstOrDefault();
                }
            }
            return default(P);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public dynamic GetOutput()
        {
            if (_databaseRetorno != null)
            {

                if (_databaseRetorno.GetType() == typeof(DataTable))
                {
                    return _databaseRetorno.Rows[0][0];
                }
                else
                {
                    return _databaseRetorno;
                }
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<dynamic> GetDynamicList()
        {
            if (_databaseRetorno != null)
            {
                if (_databaseRetorno.GetType() == typeof(DataTable))
                {
                    return ((DataTable)_databaseRetorno).AsDynamicEnumerable().ToList();
                }
            }
            return null;
        }
    }
}
