using DB.Query.Core.Models;

namespace DB.Query.Services
{
    public class DBQueryService
    {
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
    }
}
