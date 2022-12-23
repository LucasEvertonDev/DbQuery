# Sumário

- [Introdução](https://github.com/LucasEvertonDev/DbQuery#introdu%C3%A7%C3%A3o)
- [Trabalhando com Transações](https://github.com/LucasEvertonDev/DbQuery#trabalhando-com-transa%C3%A7%C3%B5es)
- [Insert](https://github.com/LucasEvertonDev/DbQuery#insert)
- [InsertIfNotExists](https://github.com/LucasEvertonDev/DbQuery#insertifnotexists)
- [InsertOrUpdate](https://github.com/LucasEvertonDev/DbQuery#insertorupdate)
- [DeleteAndInsert](https://github.com/LucasEvertonDev/DbQuery#deleteandinsert)
- [Delete](https://github.com/LucasEvertonDev/DbQuery#delete)
- [Select](https://github.com/LucasEvertonDev/DbQuery#select)
- [Update](https://github.com/LucasEvertonDev/DbQuery#update)
- [Custom Select](https://github.com/LucasEvertonDev/DbQuery#custom-select)
- [Funções Prédefinidas Select](https://github.com/LucasEvertonDev/DbQuery#fun%C3%A7%C3%B5es-pr%C3%A9definidas-select)
- [Where](https://github.com/LucasEvertonDev/DbQuery#where)
- [Join](https://github.com/LucasEvertonDev/DbQuery#join)
- [LeftJoin](https://github.com/LucasEvertonDev/DbQuery#left-join)
- [OrderBY](https://github.com/LucasEvertonDev/DbQuery#orderby)
- [GroupBy](https://github.com/LucasEvertonDev/DbQuery#groupby)

# Introdução

Para utilizar o DB.Query devem ser baixadas as bibliotecas DB.Query e DB.Query.Models.

Crie uma classe herdando a classe DBTransaction, conforme o exemplo abaixo.

```C#
using DB.Query.Models.PersistenceContext.Constants;
using DB.Query.Models.PersistenceContext.DataAnnotations;
using DB.Query.Models.PersistenceContext.Procedures;
using DB.Query.Services;
using System.Linq;
using System.Reflection;

namespace SICPontos.Utilitarios.Services
{
    public class DBTransaction : DBTransaction
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="conexao"></param>
        public override void OpenTransaction(string conexao)
        {
            base.OpenTransaction(conexao);
            #if DEBUG
                _onDebug = true;
            #endif
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inf"></param>
        /// <returns></returns>
        protected override object GetInputValue(PropertyInfo inf, StoredProcedureBase storedProcedure)
        {
            var attr = inf.GetCustomAttributes(typeof(ConfigurationAttribute), false).FirstOrDefault() as ConfigurationAttribute;
            if (attr != null)
            {
               // set configuration attributes
            }
            return inf.GetValue(storedProcedure);
        }
    }
}
```

# Trabalhando com transações

É recomendado criar uma classe "DatabaseService". Para controlar todas as suas transações a mesma deverá ser herdada por toda classe que desempenha a função de buscar dados.
Conforme o exemplo abaixo:

```C#
using SICPontos.Utilitarios.Extensions;
using SICPontos.Utilitarios.Utils;
using DB.Query.Core.Extensions;
using DB.Query.Models.PersistenceContext.Dominios;
using DB.Query.Models.PersistenceContext.Procedures;
using DB.Query.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace SICPontos.Utilitarios.Services
{
    public class DatabaseService : DBQueryService
    {
        /// <summary>
        /// Não usar na declaração do on transaction 
        /// </summary>
        private DBTransaction _transaction { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataBase_Persistence"></param>
        /// <param name="connection"></param>
        /// <param name="func"></param>
        protected void OnTransaction(Action<DBTransaction> func)
        {
            bool alreadyOpen = false;
            var _dbTransaction = InstanceDbTransaction(out alreadyOpen);
            try
            {
                _dbTransaction.OpenTransaction(Conexao.strConexaoMaster);

                BindTransactionOnRepositorys(this, _dbTransaction);

                func(_dbTransaction);

                if (!_dbTransaction.HasCommited() && !alreadyOpen)
                {
                    _dbTransaction.Commit();
                }
            }
            catch (Exception e)
            {
                if (e.GetType() == typeof(DBException) && ((DBException)e).GetTypeException() == DTypeException.Validation)
                {
                    LogService.LogInfo<DatabaseService>($"Validação no OnTransaction. Chamada -> {this.GetType().FullName} -> {func.Method.Name}");
                }
                else
                {
                    LogService.LogError<DatabaseService>(e, $"Erro em OnTransaction. Chamada -> {this.GetType().FullName} -> {func.Method.Name}");
                }

                if (!_dbTransaction.HasReversed())
                {
                    _dbTransaction.Rollback();
                }
                throw;
            }
            finally
            {
                if (!alreadyOpen)
                {
                    _transaction = null;
                    if (_dbTransaction.GetConnection() != null)
                        _dbTransaction.GetConnection().Close();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataBase_Persistence"></param>
        /// <param name="connection"></param>
        /// <param name="func"></param>
        protected bool OnTransactionSafe(Action<DBTransaction> func)
        {
            bool hasError = true;
            bool alreadyOpen = false;
            var _dbTransaction = InstanceDbTransaction(out alreadyOpen);
            try
            {
                _dbTransaction.OpenTransaction(Conexao.strConexaoMaster);

                BindTransactionOnRepositorys(this, _dbTransaction);

                func(_dbTransaction);

                if (!_dbTransaction.HasCommited() && !alreadyOpen)
                {
                    _dbTransaction.Commit();
                }
            }
            catch (Exception e)
            {
                if (e.GetType() == typeof(DBException) && ((DBException)e).GetTypeException() == DTypeException.Validation)
                {
                    LogService.LogInfo<DatabaseService>($"Validação no OnTransaction. Chamada -> {this.GetType().FullName} -> {func.Method.Name}");
                }
                else
                {
                    LogService.LogError<DatabaseService>(e, $"Erro em OnTransaction. Chamada -> {this.GetType().FullName} -> {func.Method.Name}");
                }

                if (!_dbTransaction.HasReversed())
                {
                    _dbTransaction.Rollback();
                }
                hasError = false;
            }
            finally
            {
                if (!alreadyOpen)
                {
                    _transaction = null;
                    if (_dbTransaction.GetConnection() != null)
                        _dbTransaction.GetConnection().Close();
                }
            }
            return hasError;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="func"></param>
        /// <returns></returns>
        protected T OnTransaction<T>(Func<DBTransaction, T> func)
        {
            T retorno;
            if (typeof(T) == typeof(string))
            {
                retorno = (dynamic)string.Empty;
            }
            else
            {
                retorno = Activator.CreateInstance<T>();
            }
            bool alreadyOpen = false;
            var _dbTransaction = InstanceDbTransaction(out alreadyOpen);
            try
            {
                _dbTransaction.OpenTransaction(Conexao.strConexaoMaster);

                BindTransactionOnRepositorys(this, _dbTransaction);

                retorno = func(_dbTransaction);


                if (!_dbTransaction.HasCommited() && !alreadyOpen)
                {
                    _dbTransaction.Commit();
                }
            }
            catch (Exception e)
            {
                if (e.GetType() == typeof(DBException) && ((DBException)e).GetTypeException() == DTypeException.Validation)
                {
                    LogService.LogInfo<DatabaseService>($"Validação no OnTransaction. Chamada -> {this.GetType().FullName} -> {func.Method.Name}");
                }
                else
                {
                    LogService.LogError<DatabaseService>(e, $"Erro em OnTransaction. Chamada -> {this.GetType().FullName} -> {func.Method.Name}");
                }

                if (!_dbTransaction.HasReversed())
                {
                    _dbTransaction.Rollback();
                }
                throw;
            }
            finally
            {
                if (!alreadyOpen)
                {
                    _transaction = null;
                    if (_dbTransaction.GetConnection() != null)
                        _dbTransaction.GetConnection().Close();
                }
            }
            return retorno;
        }

        /// <summary>
        /// Realizo o set da transação corrente na classe base
        /// </summary>
        protected void BindTransaction(DBTransaction transaction)
        {
            _transaction = transaction;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="transaction"></param>
        /// <returns></returns>
        protected T InstanceService<T>(DBTransaction transaction) where T : DatabaseService
        {
            var inst = Activator.CreateInstance<T>();
            inst.BindTransaction(transaction);
            return inst;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private DBTransaction InstanceDbTransaction(out bool alreadyOpen)
        {
            if (_transaction != null && _transaction.GetConnection() != null && _transaction.GetTransaction() != null
                && _transaction.GetConnection().State != ConnectionState.Closed)
            {
                alreadyOpen = true;
                return _transaction;
            }
            else
            {
                alreadyOpen = false;
                return Activator.CreateInstance<DBTransaction>();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TParameter"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="parameter"></param>
        /// <param name="conexao"></param>
        /// <returns></returns>
        public List<T> ConsultaPadrao<T>(StoredProcedureBase parameter)
        {
            List<T> retorno = new List<T>();
            try
            {
                OnTransaction((transaction) =>
                {
                    retorno = transaction.ExecuteSql(parameter).ConvertToList<T>();
                });
            }
            catch (Exception e)
            {
                var query = Activator.CreateInstance<DBTransaction>().CreateStoredProcedureCommand(parameter).PrintSql();
                LogService.LogError<DatabaseService>(e, $"Chamada automática da procedure -> {query}");
                throw;
            }
            return retorno;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TParameter"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="parameter"></param>
        /// <param name="conexao"></param>
        /// <returns></returns>
        public DataTable ConsultaPadrao(StoredProcedureBase parameter)
        {
            DataTable retorno = new DataTable();
            try
            {
                OnTransaction((transaction) =>
                {
                    retorno = transaction.ExecuteSql(parameter);
                });
            }
            catch (Exception e)
            {
                var query = Activator.CreateInstance<DBTransaction>().CreateStoredProcedureCommand(parameter).PrintSql();
                LogService.LogError<DatabaseService>(e, $"Chamada automática da procedure -> {query}");
                throw;
            }
            return retorno;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="func"></param>
        /// <param name="transaction"></param>
        private static void BindTransactionOnRepositorys(DatabaseService dataBase_Persistence, DBTransaction transaction)
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
    }
}

```

# Insert

Indica que a ação a ser realizada será um INSERT simples, sem verificações.

```C#
OnTransaction((transaction) =>
{
    var yourInstance = new CiDominio();
    transaction.GetRepository<CiDominio>().Insert(yourInstance).Execute();
});

```

# InsertIfNotExists

Indica que a ação a ser realizada será um INSERT, com uma validação de existência para efetuação do mesmo.

ATENÇÃO!! A verificação da existência é feita atravez de todas as propriedades mapeadas, QUE NÃO ESTÂO MAPEADAS COMO IDENTITY(AUTO-INCREMENT)

```C#
OnTransaction((transaction) =>
{
    var yourInstance = new CiDominio();
    transaction.GetRepository<CiDominio>().InsertIfNotExists(yourInstance).Execute();
});

```

# InsertOrUpdate

Indica que a ação a ser realizada será um INSERT, com uma validação de existência para efetuação do mesmo. Caso já exista é realizado o UPDATE.

ATENÇÃO!! A etapa [WHERE](https://github.com/LucasEvertonDev/DbQuery#where) é de suma importância para essa ação.  Pois a mesma irá verificar a existência e também controlar a atualização se necessário.

```C#
OnTransaction((transaction) =>
{
    var yourInstance = new CiDominio();
    transaction.GetRepository<CiDominio>().InsertOrUpdate(yourInstance).Where(CiDominio => CiDominio.Codigo == 1).Execute();
});

```

# DeleteAndInsert

Indica que a ação a ser realizada será um INSERT, após uma etapa de DELETE

ATENÇÃO!! O controle de quais entidades serão apagadas deve ser realizado na condição [WHERE](https://github.com/LucasEvertonDev/DbQuery#where)!

```C#
OnTransaction((transaction) =>
{
    var yourInstance = new CiDominio();
    transaction.GetRepository<CiDominio>().DeleteAndInsert(yourInstance).Where(CiDominio => CiDominio.Codigo == 1).Execute();
});

```

# Delete
Indica que a ação a ser realizada será um DELETE simples.

ATENÇÃO!! O controle de quais entidades serão apagadas deve ser realizado na condição [WHERE](https://github.com/LucasEvertonDev/DbQuery#where)!

```C#
OnTransaction((transaction) =>
{
    transaction.GetRepository<CiDominio>().Delete().Where(CiDominio => CiDominio.Codigo == 1).Execute();
});

```

# Select

Indica que a ação a ser realizada será um SELECT simples!

Um SELECT SIMPLES, nada mais é que um espelho da tabela representada pela entidade (SELECT * FROM TEntity)

ATENÇÃO!! O controle de quais entidades serão selecionadas deve ser realizado na condição [WHERE](https://github.com/LucasEvertonDev/DbQuery#where)!

```C#
OnTransaction((transaction) =>
{
    // Seleciona todas as entidades que se adequarem a condição bem como todas as colunas
    transaction.GetRepository<CiDominio>().Select().Where(CiDominio => CiDominio.Codigo == 1).ToList();

    // Realiza o select top das entidades que se adequarem a condição
    transaction.GetRepository<CiDominio>().Select().Top(10).Where(CiDominio => CiDominio.Codigo == 1).ToList();

    // Realiza o distinct das entidades que se adequarem a condição
    transaction.GetRepository<CiDominio>().Select().Distinct().Where(CiDominio => CiDominio.Codigo == 1).ToList();

    // Realiza o select ordenando de forma asc
    transaction.GetRepository<CiDominio>().Select().Where(CiDominio => CiDominio.Codigo == 1).OrderBy(CiDominio => CiDominio.Codigo).ToList();

    // Realiza o select ordenando de forma asc e desc com mais de uma coluna como criterio
    transaction.GetRepository<CiDominio>()
        .Select()
        .Where(CiDominio => CiDominio.Codigo == 1)
        .OrderBy(CiDominio => Columns(
            CiDominio.Codigo,
            CiDominio.Nome)
        )
        .OrderByDesc(CiDominio => CiDominio.Descricao)
        .ToList();

    // Realiza o select ordenando de forma asc e desc com mais de uma coluna como criterio
    transaction.GetRepository<CiDominio>()
        .Select()
        .Where(CiDominio => CiDominio.Codigo == 1)
        .OrderBy(CiDominio => Columns(
            CiDominio.Codigo,
            CiDominio.Nome)
        )
        .OrderByDesc(CiDominio => CiDominio.Descricao)
        .ToList();

    // Realiza o select paginando a consulta
    transaction.GetRepository<CiDominio>()
        .Select()
        .Where(CiDominio => CiDominio.Codigo == 1)
        .OrderBy(CiDominio => Columns(
            CiDominio.Codigo,
            CiDominio.Nome)
        )
        .Pagination(pageNumber: 1, pageSize: 10)
        .Execute();
});

```

# UPDATE

Indica que a ação a ser realizada será um UPDATE simples, sem verificações.

ATENÇÃO!! O controle de quais entidades serão atualizadas deve ser realizado na condição [WHERE](https://github.com/LucasEvertonDev/DbQuery#where)!

```C#
OnTransaction((transaction) =>
{
    //Consultando os items e atualizando todas as colunas
    var toUpdate = transaction.GetRepository<CiDominio>()
        .Select()
        .Where(CiDominio => CiDominio.Codigo == 1)
        .OrderBy(CiDominio => Columns(
                CiDominio.Codigo,
                CiDominio.Nome)
            )
        .Pagination(pageNumber: 1, pageSize: 10)
        .FirstOrDefault();

    toUpdate.Nome = "Novo Nome";

    transaction.GetRepository<CiDominio>()
        .Update(toUpdate)
        .Where(CiDominio => CiDominio.Codigo == 1)
        .Execute();

    // Atualizando direto apenas uma coluna usando new {}
    var toUpdate2 = new CiDominio
    {
        Nome = "Novo Nome"
    };

    transaction.GetRepository<CiDominio>()
        .Update(toUpdate2)
        .SetCollumns(Ci => new { Ci.Nome })
        .Where(CiDominio => CiDominio.Codigo == 1)
        .Execute();

    // Atualizando direto apenas uma coluna usando Columns()
    transaction.GetRepository<CiDominio>()
        .Update(toUpdate2)
        .SetCollumns(Ci => Columns( Ci.Nome ))
        .Where(CiDominio => CiDominio.Codigo == 1)
        .Execute();
});

```

# Custom Select

Indica que a ação a ser realizada será um SELECT tipado. Onde os tipos passados no metodo serão usados para a listagem das colunas!

Funções pré definidas e tratadas para a instrução select: [Funções Prédefinidas Select](https://github.com/LucasEvertonDev/DbQuery#fun%C3%A7%C3%B5es-pr%C3%A9definidas-select)!

```C#
OnTransaction((transaction) =>
{
    transaction.GetRepository<CiDominio>()
        .Select<CiDominio, CiItemDominio>((CiDominio, CiItemDominio) =>
            new Retorno
            {
                Dominio = CiDominio.Nome,
                Nome = CiItemDominio.Nome,
                Descricao = CiItemDominio.Descricao,
                Display = Concat(CiItemDominio.Codigo, " ", CiItemDominio.Descricao),
                Codigo = IsNull(CiItemDominio.Codigo, 0)
            }
        )
        .Distinct()
        .Join<CiDominio, CiItemDominio>((CiDominio, CiItemDominio) => 
            CiDominio.Codigo == CiItemDominio.Codigo
        )
        .Where(CiDominio =>
                CiDominio.Codigo == 1
        )
        .OrderBy(
            CiDominio => CiDominio.Codigo
        )
        .ToList<Retorno>();
  
 transaction.GetRepository<FinSolicitacaoNumerario>()
  .Select<FinSolicitacaoNumerario, EstItens, ComCadFor, EstItensPC, CiItemDominio, CICadUsuario>(
   (FinSolicitacaoNumerario, EstItens, ComCadFor, EstItensPC, CiItemDominio, CICadUsuario) => new
   {
    Id = FinSolicitacaoNumerario.Id,
    PedidoCompra = FinSolicitacaoNumerario.NumPed,
    RCM = FinSolicitacaoNumerario.RCM,
    Fornecedor = ComCadFor.CNPJ_CPF + " - " + ComCadFor.Razao,
    Item = EstItens.Descricao1 + " " + EstItens.Descricao3,
    Valor = FinSolicitacaoNumerario.Valor,
    DataNumerario = FinSolicitacaoNumerario.DataNumerario,
    Situacao = CiItemDominio.Descricao,
    Usuario = CICadUsuario.Descricao
   }
  )
  .Join<FinSolicitacaoNumerario, EstItensRCM>((FinSolicitacaoNumerario, EstItensRCM) =>
    FinSolicitacaoNumerario.RCM == EstItensRCM.RCM
  )
  .Join<EstItensRCM, EstItens>((EstItensRCM, EstItens) =>
    EstItensRCM.Cod_Full == EstItens.Cod_Full
  )
  .Join<EstItensRCM, EstItensPC>((EstItensRCM, EstItensPC) =>
    EstItensPC.RCM == EstItensRCM.RCM
  )
  .Join<FinSolicitacaoNumerario, EstPedCompra>((FinSolicitacaoNumerario, EstPedCompra) =>
    FinSolicitacaoNumerario.NumPed == EstPedCompra.NumPed
  )
  .Join<EstPedCompra, ComCadFor>((EstPedCompra, ComCadFor) =>
    EstPedCompra.CodFor == ComCadFor.CNPJ_CPF
  )
  .Join<FinSolicitacaoNumerario, CICadUsuario>((FinSolicitacaoNumerario, CICadUsuario) =>
    FinSolicitacaoNumerario.UsuarioSolicitante == CICadUsuario.Codigo
  )
  .Join<FinSolicitacaoNumerario, CiItemDominio>((FinSolicitacaoNumerario, CiItemDominio) =>
    CiItemDominio.Codigo == FinSolicitacaoNumerario.Situacao
  )
  .Join<CiItemDominio, CiDominio>((CiItemDominio, CiDominio) =>
    CiDominio.Codigo == CiItemDominio.Codigo_Dominio
    && CiDominio.Nome == DSituacaoSolicitacaoNumerario.CHAVE_DOMINIO
  )
  .Where<FinSolicitacaoNumerario, EstPedCompra>((FinSolicitacaoNumerario, EstPedCompra) =>
    (RCM == null || FinSolicitacaoNumerario.RCM == RCM.GetValueOrDefault())
    && (pedidoCompra == null || FinSolicitacaoNumerario.NumPed == pedidoCompra)
    && (fornecedor == null || EstPedCompra.CodFor == fornecedor)
  )
  .ToList();
});

```

# Funções Prédefinidas Select

Exemplo de funções e converções válidas:

```C#
// Function Count 
transaction.GetRepository<CiDominio>()
    .Select<CiDominio, CiItemDominio>(
        (CiDominio, CiItemDominio) => Columns(
            Count(CiItemDominio.Codigo),
            CiItemDominio.Codigo_Dominio,
            CiDominio.Descricao
        )
    )
    .Join<CiDominio, CiItemDominio>(
            (CiDominio, CiItemDominio) => CiDominio.Codigo == CiItemDominio.Codigo
    )
    .Where<CiDominio, CiItemDominio>(
        (CiDominio, CiItemDominio) => CiDominio.Codigo == 1
    )
    .GroupBy<CiDominio, CiItemDominio>(
        (CiDominio, CiItemDominio) => Columns(
            CiItemDominio.Codigo_Dominio,
            CiDominio.Descricao
        )
    )
    .ToList();

// Function Max
transaction.GetRepository<CiDominio>().Select(CiDominio => Max(CiDominio.Codigo)).First();

// Function Min
transaction.GetRepository<CiDominio>().Select(CiDominio => Min(CiDominio.Codigo)).First();

// Function Concat
transaction.GetRepository<CiDominio>().Select(CiDominio => Concat(CiDominio.Codigo, '-', CiDominio.Descricao)).ToList();

// Function Sum 
transaction.GetRepository<CiDominio>().Select(CiDominio => Sum(CiDominio.Codigo)).First();

// Function Alias 
transaction.GetRepository<CiDominio>().Select(CiDominio => Alias(CiDominio.Codigo, "Cod")).ToList();

// Function Upper
transaction.GetRepository<CiDominio>().Select(CiDominio => Upper(CiDominio.Codigo)).ToList();

/// Exemplos de casts básicos permitidos 
transaction.GetRepository<EstItensPC>()
    .Select(est =>
        new
        {
            DisplayMember = decimal.Parse(est.RCM.ToString()),
            ValueMember = int.Parse(est.RCM.ToString()),
            CastInt = (int)est.RCM,
            CastDecimal = (decimal)est.PerDesc,
        }
    )
    .Where(est => est.NumPed == 12345)
    .ToList();
```

# Where

A expressão deve ter um resultado booleano, porém é de suma importância na comparação de propriedade evitar: associações, parses e funções que não foram tratadas.
Tendo como exceção os paramêtros passados para a consulta.

Metodos ou extenções como Parse, Convert e tratações extras. Ainda não possuem suporte em propriedades instanciadas pela consulta. O motivo disso é que as expressões são traduzidadas, não executadas.

As condições devem ter implementação simples e diretas. De nível quase semelhante ao sql.

Exemplo de comparações válidas:

```C#
OnTransaction((transaction) =>
{
     transaction.GetRepository<EstPedCompra>()
        .Select<EstPedCompra, ComCadFor>(
            (EstPedCompra, ComCadFor) => ComCadFor.AllColumns()
        )
        .Join<EstPedCompra, ComCadFor>((EstPedCompra, ComCadFor) =>
            EstPedCompra.CodFor == ComCadFor.CNPJ_CPF
        )
        .Where<EstPedCompra, ComCadFor>((EstPedCompra, ComCadFor) =>
            EstPedCompra.CodFor == ComCadFor.CNPJ_CPF
            && EstPedCompra.NumPed.ToString() == ComCadFor.CNPJ_CPF
            && EstPedCompra.NumPed.GetValueOrDefault() == ComCadFor.Codigo_Empresa
            && EstPedCompra.NumPed.Value == ComCadFor.Codigo_Empresa
            && EstPedCompra.CodFor.LIKE(ComCadFor.CNPJ_CPF)
            && EstPedCompra.CodFor.Equals(ComCadFor.CNPJ_CPF)
            && EstPedCompra.CodFor.Contains("Teste")
            && EstPedCompra.CodFor.IN("Teste", "Teste2")
            && EstPedCompra.CodFor.NOT_IN("Teste", "Teste2")
        )
        .ToList();

```

# Join

A expressão deve ter um resultado booleano, porém é de suma importância na comparação de propriedade evitar: associações, parses e funções que não foram tratadas.
Tendo como exceção os paramêtros passados para a consulta.

Metodos ou extenções como Parse, Convert e tratações extras. Ainda não possuem suporte em propriedades instanciadas pela consulta. O motivo disso é que as expressões são traduzidadas, não executadas.

As condições devem ter implementação simples e diretas. De nível quase semelhante ao sql.

O primeiro TypeParam deve ser um classe já presente na consulta. Ou seja => Join<ClassePresente, ClasseNova>();

Exemplo de comparações válidas:

```C#
OnTransaction((transaction) =>
{
    transaction.GetRepository<EstPedCompra>()
        .Select<EstPedCompra, ComCadFor>(
            (EstPedCompra, ComCadFor) => ComCadFor.AllColumns()
        )
        .Join<EstPedCompra, ComCadFor>((EstPedCompra, ComCadFor) =>
            EstPedCompra.CodFor == ComCadFor.CNPJ_CPF
            && EstPedCompra.NumPed.ToString() == ComCadFor.CNPJ_CPF
            && EstPedCompra.NumPed.GetValueOrDefault() == ComCadFor.Codigo_Empresa
            && EstPedCompra.NumPed.Value == ComCadFor.Codigo_Empresa
            && EstPedCompra.CodFor.LIKE(ComCadFor.CNPJ_CPF)
            && EstPedCompra.CodFor.Equals(ComCadFor.CNPJ_CPF)
        )
        .ToList();

```

# Left Join

A expressão deve ter um resultado booleano, porém é de suma importância na comparação de propriedade evitar: associações, parses e funções que não foram tratadas.
Tendo como exceção os paramêtros passados para a consulta.

Metodos ou extenções como Parse, Convert e tratações extras. Ainda não possuem suporte em propriedades instanciadas pela consulta. O motivo disso é que as expressões são traduzidadas, não executadas.

As condições devem ter implementação simples e diretas. De nível quase semelhante ao sql.

O primeiro TypeParam deve ser um classe já presente na consulta. Ou seja => Join<ClassePresente, ClasseNova>();

Exemplo de comparações válidas:

```C#
OnTransaction((transaction) =>
{
    transaction.GetRepository<EstPedCompra>()
        .Select<EstPedCompra, ComCadFor>(
            (EstPedCompra, ComCadFor) => ComCadFor.AllColumns()
        )
        .LeftJoin<EstPedCompra, ComCadFor>((EstPedCompra, ComCadFor) =>
            EstPedCompra.CodFor == ComCadFor.CNPJ_CPF
            && EstPedCompra.NumPed.ToString() == ComCadFor.CNPJ_CPF
            && EstPedCompra.NumPed.GetValueOrDefault() == ComCadFor.Codigo_Empresa
            && EstPedCompra.NumPed.Value == ComCadFor.Codigo_Empresa
            && EstPedCompra.CodFor.LIKE(ComCadFor.CNPJ_CPF)
            && EstPedCompra.CodFor.Equals(ComCadFor.CNPJ_CPF)
        )
        .ToList();

```

# OrderBY

Lista as colunas que ordenarão a query

```C#
OnTransaction((transaction) =>
{
    transaction.GetRepository<CiDominio>().Select().OrderBy(ci => new { ci.Descricao, ci.Codigo }).ToList();

    transaction.GetRepository<CiDominio>().Select().OrderByDesc(ci => new { ci.Descricao, ci.Codigo }).ToList();
});
```

# GroupBy

Lista as colunas que agruparão a query

```C#
OnTransaction((transaction) =>
{
    transaction.GetRepository<CiDominio>()
        .Select<CiDominio, CiItemDominio>(
            (CiDominio, CiItemDominio) => Columns(
                Count(CiItemDominio.Codigo),
                CiItemDominio.Codigo_Dominio,
                CiDominio.Descricao
            )
        )
        .Join<CiDominio, CiItemDominio>(
                (CiDominio, CiItemDominio) => CiDominio.Codigo == CiItemDominio.Codigo
        )
        .Where<CiDominio, CiItemDominio>(
            (CiDominio, CiItemDominio) => CiDominio.Codigo == 1
        )
        .GroupBy<CiDominio, CiItemDominio>(
            (CiDominio, CiItemDominio) => new 
            {
                CiItemDominio.Codigo_Dominio,
                CiDominio.Descricao
            }
        )
        .Execute();
});
```
