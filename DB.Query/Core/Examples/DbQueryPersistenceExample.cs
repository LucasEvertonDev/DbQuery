using DB.Query.Core.Models;
using DB.Query.Services;
using DB.Query.Core.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace DB.Query.Core.Examples
{
    public class DBQueryPersistenceExample
    {
        public DBTransaction _transaction { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="func"></param>
        protected static void OnTransaction(string connection, Action<DBTransaction> func)
        {
            var _dataBaseService = Activator.CreateInstance<DBTransaction>();
            try
            {
                _dataBaseService.OpenTransaction(connection);

                getProprerties(func, _dataBaseService);

                func(_dataBaseService);

                if (!_dataBaseService.HasCommited())
                {
                    _dataBaseService.Commit();
                }
            }
            catch (Exception e)
            {
                _dataBaseService.Rollback();
                throw e;
            }
            finally
            {
                if (_dataBaseService.GetConnection() != null)
                    _dataBaseService.GetConnection().Close();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="func"></param>
        protected async static Task OnTransactionAsync(string connection, Action<DBTransaction> func)
        {
            var _dataBaseService = Activator.CreateInstance<DBTransaction>();
            try
            {
                await _dataBaseService.OpenTransactionAsync(connection);

                func(_dataBaseService);
            }
            catch (Exception e)
            {
                _dataBaseService.Rollback();
                throw e;
            }
            finally
            {
                if (_dataBaseService.GetConnection() != null)
                    _dataBaseService.GetConnection().Close();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="func"></param>
        protected async static Task OnTransactionAsync(DBQueryPersistenceExample dataBase_Persistence, string connection, Action<DBTransaction> func)
        {
            var _dataBaseService = Activator.CreateInstance<DBTransaction>();
            try
            {
                await _dataBaseService.OpenTransactionAsync(connection);

                getProprerties(dataBase_Persistence, _dataBaseService);

                func(_dataBaseService);
            }
            catch (Exception e)
            {
                _dataBaseService.Rollback();
                throw e;
            }
            finally
            {
                if (_dataBaseService.GetConnection() != null)
                    _dataBaseService.GetConnection().Close();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataBase_Persistence"></param>
        /// <param name="connection"></param>
        /// <param name="func"></param>
        protected static void OnTransaction(DBQueryPersistenceExample dataBase_Persistence, string connection, Action<DBTransaction> func)
        {
            var _dataBaseService = Activator.CreateInstance<DBTransaction>();
            try
            {
                _dataBaseService.OpenTransaction(connection);

                getProprerties(dataBase_Persistence, _dataBaseService);

                func(_dataBaseService);

                if (!_dataBaseService.HasCommited())
                {
                    _dataBaseService.Commit();
                }
            }
            catch (Exception e)
            {
                _dataBaseService.Rollback();
                throw e;
            }
            finally
            {
                if (_dataBaseService.GetConnection() != null)
                    _dataBaseService.GetConnection().Close();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="func"></param>
        public void OnTransaction_DB(string connection, Action<DBTransaction> func)
        {
            OnTransaction(connection, func);
        }

        #region DB.Query
        /// <summary>
        /// 
        /// </summary>
        private static void getProprerties(Action<DBTransaction> func, DBTransaction transaction)
        {
            foreach (var p in func.Target.GetType().GetProperties(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance).Where(a => a.PropertyType.Name.Contains("Repository")))
            {
                var obj = p.GetValue(func.Target);
                MethodInfo m = obj.GetType().GetMethod("BindTransaction");
                m.Invoke(obj, new object[] { transaction });
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="func"></param>
        /// <param name="transaction"></param>
        private static void getProprerties(DBQueryPersistenceExample dataBase_Persistence, DBTransaction transaction)
        {
            var properties = ((Type)(dataBase_Persistence.GetType())).GetProperties(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance).ToList();
            foreach (var p in properties.Where(prop => prop.PropertyType.Name.Contains("Repository")))
            {
                var obj = p.GetValue(dataBase_Persistence);
                List<PropertyInfo> properties2 = ((Type)(obj.GetType())).GetProperties(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance).ToList();
                foreach (var o in properties2.Where(o => "_transaction".Equals(o.Name)))
                {
                    DBTransaction val = (DBTransaction)o.GetValue(obj, null);
                    if (val == null || val.GetConnection() == null || val.GetConnection().State == ConnectionState.Closed)
                    {
                        MethodInfo m = obj.GetType().GetMethod("BindTransaction");
                        m.Invoke(obj, new object[] { transaction });
                    }
                }
            }
        }

        /// <summary>
        /// Trata a lista de colunas a ser usada
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        protected dynamic[] Columns(params dynamic[] array)
        {
            return array;
        }

        /// <summary>
        /// Função para obter o Top da consulta
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        protected int? Top(int i)
        {
            return i;
        }

        /// <summary>
        ///  Função para obter o Count
        /// </summary>
        /// <param name="prop"></param>
        /// <returns></returns>
        protected Count Count(dynamic prop)
        {
            return null;
        }


        /// <summary>
        ///  Função para obter o Count
        /// </summary>
        /// <param name="prop"></param>
        /// <returns></returns>
        protected Count Count()
        {
            return null;
        }

        /// <summary>
        ///  Função para obter o Max
        /// </summary>
        /// <returns></returns>
        protected Max Max(dynamic prop)
        {
            return null;
        }

        /// <summary>
        ///  Upper
        /// </summary>
        /// <returns></returns>
        protected Upper Upper(dynamic prop)
        {
            return null;
        }

        ///// <summary>
        /////  função que trata a correspondência de valor caso o mesmo seja nulo 
        ///// </summary>
        ///// <param name="prop"></param>
        ///// <returns></returns>
        //protected IsNull IsNull(dynamic prop, object val)
        //{
        //    return null;
        //}

        /// <summary>
        ///  Função para obter o Min
        /// </summary>
        /// <returns></returns>
        protected Min Min(dynamic prop)
        {
            return null;
        }

        /// <summary>
        ///  Função para obter o Sum
        /// </summary>
        /// <returns></returns>
        protected Sum Sum(dynamic prop)
        {
            return null;
        }

        /// <summary>
        /// Função para aplicar o alias "AS"
        /// </summary>
        /// <returns></returns>
        protected Alias Alias(object prop, string Name)
        {
            return null;
        }

        /// <summary>
        /// Função para aplicar o alias "IsNull"
        /// </summary>
        /// <returns></returns>
        protected IsNull IsNull(object prop, dynamic Name)
        {
            return null;
        }

        /// <summary>
        /// Função para o Concat
        /// </summary>
        /// <param name="props"></param>
        /// <returns></returns>
        protected string Concat(params dynamic[] props)
        {
            return null;
        }
        #endregion
    }
}
