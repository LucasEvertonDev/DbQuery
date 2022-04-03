using SIGN.Query.Extensions;
using SIGN.Utilitarios.Dominios.Mapeamentos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGN.Query.SignQuery
{
    public class ResultQuery<T>
    {
        public ResultQuery(dynamic databaseRetorno)
        {
            this._databaseRetorno = databaseRetorno;
        }

        /// <summary>
        /// 
        /// </summary>
        private dynamic _databaseRetorno { get; set; }

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
        public List<T> GetItens()
        {
            if (_databaseRetorno != null)
            {
                if (_databaseRetorno.GetType() == typeof(DataTable))
                {
                    return ((DataTable)_databaseRetorno).OfType<T>();
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
        public T GetItem()
        {
            if (_databaseRetorno != null)
            {
                if (_databaseRetorno.GetType() == typeof(DataTable))
                {
                    return ((DataTable)_databaseRetorno).OfType<T>().FirstOrDefault();
                }
            }
            return default(T);
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
    }
}
