﻿using DB.Query.Models.Entities;
using DB.Query.Core.Steps.Base;
using System.Data;
using DB.Query.Core.Extensions;
using System.Linq;
using System.Collections.Generic;
using System;

namespace DB.Query.Core.Steps.Select
{
    public class SelectResultStep<TEntity> : ResultStep<TEntity> where TEntity : EntityBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="databaseRetorno"></param>
        public SelectResultStep(dynamic databaseRetorno) : base()
        {
            _databaseRetorno = databaseRetorno;
        }

        /// <summary>
        ///     Responsável por retornar o primeiro item tipado como <typeparamref name="TEntity"/>
        /// </summary>
        /// <returns>
        ///     Retorno do tipo <typeparamref name="TEntity"/>. Contendo o primeiro item do resultado da query executada
        /// </returns>
        public TEntity First()
        {
            if (_databaseRetorno != null)
            {
                if (_databaseRetorno.GetType() == typeof(DataTable))
                {
                    try
                    {
                        return ((DataTable)_databaseRetorno).OfType<TEntity>().First();
                    }
                    catch 
                    {
                        throw new System.Exception("Não foi encontrado nenhum item para a consulta realizada na tabela.");
                    }
                }
            }
            throw new System.Exception("Não foi encontrado nenhum item para a consulta realizada na tabela.");
        }

        /// <summary>
        ///     Responsável por retornar o primeiro item tipado como <typeparamref name="TEntity"/>
        /// </summary>
        /// <returns>
        ///     Retorno do tipo <typeparamref name="TEntity"/>. Contendo o primeiro item do resultado da query executada
        /// </returns>
        public TEntity FirstOrDefault()
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
        ///     Responsável por retornar o primeiro item tipado como <typeparamref name="TEntity"/>
        /// </summary>
        /// <returns>
        ///     Retorno do tipo <typeparamref name="TEntity"/>. Contendo o primeiro item do resultado da query executada
        /// </returns>
        public List<TEntity> ToList()
        {
            if (_databaseRetorno != null)
            {
                if (_databaseRetorno.GetType() == typeof(DataTable))
                {
                    return ((DataTable)_databaseRetorno).OfType<TEntity>();
                }
            }
            return new List<TEntity>();
        }

        /// <summary>
        ///     Responsável por retornar o primeiro item tipado como <typeparamref name="TEntity"/>
        /// </summary>
        /// <returns>
        ///     Retorno do tipo <typeparamref name="TEntity"/>. Contendo o primeiro item do resultado da query executada
        /// </returns>
        public DataTable ToDataTable()
        {
            if (_databaseRetorno != null)
            {
                if (_databaseRetorno.GetType() == typeof(DataTable))
                {
                    return ((DataTable)_databaseRetorno);
                }
                else
                {
                    var dataRetorno = new DataTable();
                    dataRetorno.AddColluns("Output");
                    dataRetorno.Rows.Add(_databaseRetorno);
                }
            }
            return new DataTable();
        }

        /// <summary>
        ///     Responsável por retornar o primeiro item tipado como <typeparamref name="T"/>
        /// </summary>
        /// <returns>
        ///     Retorno do tipo <typeparamref name="T"/>. Contendo o primeiro item do resultado da query executada
        /// </returns>
        public T First<T>()
        {
            if (_databaseRetorno != null)
            {
                if (_databaseRetorno.GetType() == typeof(DataTable))
                {
                    try
                    {
                        return ((DataTable)_databaseRetorno).OfType<T>().First();
                    }
                    catch
                    {
                        throw new System.Exception("Não foi encontrado nenhum item para a consulta realizada na tabela.");
                    }
                }
            }
            throw new System.Exception("Não foi encontrado nenhum item para a consulta realizada na tabela.");
        }

        /// <summary>
        ///     Responsável por retornar o primeiro item tipado como <typeparamref name="T"/>
        /// </summary>
        /// <returns>
        ///     Retorno do tipo <typeparamref name="T"/>. Contendo o primeiro item do resultado da query executada
        /// </returns>
        public T FirstOrDefault<T>()
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
        ///     Responsável por retornar o primeiro item tipado como <typeparamref name="T"/>
        /// </summary>
        /// <returns>
        ///     Retorno do tipo <typeparamref name="T"/>. Contendo o primeiro item do resultado da query executada
        /// </returns>
        public List<T> ToList<T>()
        {
            if (_databaseRetorno != null)
            {
                if (_databaseRetorno.GetType() == typeof(DataTable))
                {
                    return ((DataTable)_databaseRetorno).OfType<T>();
                }
            }
            return new List<T>();
        }

        /// <summary>
        ///     Responsável por retornar valor único da query
        /// </summary>
        /// <returns>
        ///     Retorno do tipo dynamic. Contendo o valor único da query resultado da query executada
        /// </returns>
        public dynamic GetField()
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
