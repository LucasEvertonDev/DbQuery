using SIGN.Query.DbFunctions;
using SIGN.Query.Domains;
using SIGN.Query.Repository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
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
        public void OnTransaction_DB(string connection, Action<SignTransaction> func)
        {
            OnTransaction(connection, func);
        }

        /// <summary>
        /// 
        /// </summary>
        private static void getProprerties(Action<SignTransaction> func, SignTransaction transaction)
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
