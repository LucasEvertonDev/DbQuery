using DBQuery.Core.Constants;
using Application.Domains.DataAnnotatios;
using DBQuery.Core.Transaction;
using DBQuery.Functions;
using DBQuery.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DBQuery
{
    public class DbQueryService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="func"></param>
        public static void OnTransaction(string connection, Action<DbTransaction> func)
        {
            var _transaction = Activator.CreateInstance<DbTransaction>();
            try
            {
                _transaction.OpenTransaction(connection);

                getProprerties(func, _transaction);

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
        public void OnTransaction_DB(string connection, Action<DbTransaction> func)
        {
            OnTransaction(connection, func);
        }

        /// <summary>
        /// 
        /// </summary>
        private static void getProprerties(Action<DbTransaction> func, DbTransaction transaction)
        {
            foreach (var p in func.Target.GetType().GetProperties(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance).Where(a => a.PropertyType.Name.Contains("Repository")))
            {
                var obj = p.GetValue(func.Target);
                MethodInfo m = obj.GetType().GetMethod("BindTransaction");
                m.Invoke(obj, new object[] { transaction });
            }
        }

        /// <summary>
        /// Trata a lista de colunas a ser usada
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public dynamic[] Columns(params dynamic[] array)
        {
            return array;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public int? Top(int i)
        {
            return i;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prop"></param>
        /// <returns></returns>
        public Count Count(dynamic prop)
        {
            return null;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="prop"></param>
        /// <returns></returns>
        public Count Count()
        {
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Max Max(dynamic prop)
        {
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Min Min(dynamic prop)
        {
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Sum Sum(dynamic prop)
        {
            return null;
        }

        public string Concat(params dynamic[] props)
        {
            return null;
        }

        public Alias Alias(object prop, string alias)
        {
            return null;
        }
    }
}
