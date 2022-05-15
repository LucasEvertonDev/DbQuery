using Application.Domains.Entities;
using DBQuery.Core.Enuns;
using DBQuery.Core.Model;
using DBQuery.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DBQuery.Core.Factory
{
    public class DBQueryLevelModelFactory
    {
        /// <summary>
        /// Prepara a criação de uma etapa de UseAlias. 
        /// Que posteriormente será traduzida para SQL <see cref="InterpretService{EntityBase}">aqui.</see>
        /// </summary>
        /// <returns>Retorna uma etapa corresponte a USE_ALIAS</returns>
        public DBQueryStepModel PrepareAliasStep(string alias)
        {
            return new DBQueryStepModel()
            {
                StepType = StepType.USE_ALIAS,
                StepValue = alias,
                Documentation = "Responsável por indicar se os apelidos usados nas expressions serão denominados como chaves apelidos 'as'."
            };
        }

        /// <summary>
        /// Prepara a criação de uma etapa de Insert. 
        /// Que posteriormente será traduzida para SQL <see cref="InterpretService{EntityBase}.GenerateInsertScript">aqui.</see>
        /// </summary>
        /// <returns>Retorna uma etapa corresponte a INSERT</returns>
        public DBQueryStepModel PrepareInsertStep (EntityBase domain)
        {
            return new DBQueryStepModel()
            {
                StepType = StepType.INSERT,
                StepValue = domain,
                Documentation = "Responsável por chamar a etapa de insert. A mesma recebe um objeto preenchido do tipo TEntity para a persistência."
            };
        }

        /// <summary>
        /// Prepara a criação de uma etapa de DELETE. 
        /// Que posteriormente será traduzida para SQL <see cref="InterpretService{EntityBase}.GenerateDeleteScript">aqui.</see>
        /// </summary>
        /// <returns>Retorna uma etapa corresponte a DELETE</returns>
        public DBQueryStepModel PrepareDeleteStep()
        {
            return new DBQueryStepModel
            {
                StepType = StepType.DELETE,
                Documentation = "Reponsável por chamar a etapa de delete. O controle de propriedades a serem apagadas será realizado na etapa WHERE."
            };
        }

        /// <summary>
        /// Prepara a criação de uma etapa de SELECT. 
        /// Que posteriormente será traduzida para SQL <see cref="InterpretService{EntityBase}.GenerateSelectScript">aqui.</see>
        /// </summary>
        /// <returns>Retorna uma etapa corresponte a SELECT</returns>
        public DBQueryStepModel PrepareSimpleSelectStep()
        {
            return new DBQueryStepModel
            {
                StepType = StepType.SELECT,
                Documentation = "Reponsável por chamar a etapa select de uma consulta SIMPLES. Retornando apenas os dados da entidade passada como tipo na propriedade Repository<TEntity>."
            };
        }

        /// <summary>
        /// Prepara a criação de uma etapa de UPDATE. 
        /// Que posteriormente será traduzida para SQL <see cref="InterpretService{EntityBase}.GenerateUpdateScript">aqui.</see>
        /// </summary>
        /// <returns>Retorna uma etapa corresponte a SELECT</returns>
        public DBQueryStepModel PrepareUpdateStep(EntityBase domain)
        {
            return new DBQueryStepModel
            {
                StepType = StepType.UPDATE,
                StepValue = domain,
                Documentation = "Reponsável por chamar a etapa update. A mesma recebe um objeto preenchido do tipo TEntity para a atualização."
            };
        }

        /// <summary>
        /// Prepara a criação de uma etapa de INSERT_OR_UPDATE. 
        /// Que posteriormente será traduzida para SQL <see cref="InterpretService{EntityBase}.GenerateInsertOrUpdateScript">aqui.</see>
        /// </summary>
        /// <returns>Retorna uma etapa corresponte a INSERT_OR_UPDATE</returns>
        public DBQueryStepModel PrepareInsertOrUpdateStep(EntityBase domain)
        {
            return new DBQueryStepModel
            {
                StepType = StepType.INSERT_OR_UPDATE,
                StepValue = domain,
                Documentation = "Reponsável por chamar a etapa de insert ou update. A etapa WHERE é de suma importância para essa ação." +
                            " Pois a mesma será a chave para a verificação de existência do objeto ou até mesmo na atualização caso ele já exista."
            };
        }

        /// <summary>
        /// Prepara a criação de uma etapa de DELETE_AND_INSERT. 
        /// Que posteriormente será traduzida para SQL <see cref="InterpretService{EntityBase}.GenerateDeleteAndInsertScript">aqui.</see>
        /// </summary>
        /// <returns>Retorna uma etapa corresponte a DELETE_AND_INSERT</returns>
        public DBQueryStepModel PrepareDeleteAndInsertStep(EntityBase domain)
        {
            return new DBQueryStepModel
            {
                StepType = StepType.DELETE_AND_INSERT,
                StepValue = domain,
                Documentation = "Reponsável por chamar a etapa delete e insert. Ou seja limpa os registros condizentes com a condição e insere na sequência "
            };
        }


        /// <summary>
        /// Prepara a criação de uma etapa de INSERT_NOT_EXISTS. 
        /// Que posteriormente será traduzida para SQL <see cref="InterpretService{EntityBase}.GenerateInsertIfNotExistsScript">aqui.</see>
        /// </summary>
        /// <returns>Retorna uma etapa corresponte a INSERT_NOT_EXISTS</returns>
        public DBQueryStepModel PrepareInsertIfNotExistsStep(EntityBase domain)
        {
            return new DBQueryStepModel
            {
                StepType = StepType.INSERT_NOT_EXISTS,
                StepValue = domain,
                Documentation = "Responsável por chamar a etapa de insert. A mesma recebe um objeto preenchido do tipo TEntity para a persistência, onde será válidada se a tabela não possui nenhum registro igual, com apenas a chave unica diferente. Sendo esse atributo ignorado na validação, representado com a anotação Identity"
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DBQueryStepModel PrepareExecuteStep()
        {
            return new DBQueryStepModel
            {
                StepType = StepType.EXECUTE,
                Documentation = "Comando usado para iniciar processe de leitura e execução da query"
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DBQueryStepModel PrepareSelectStep(Expression expression)
        {
            return new DBQueryStepModel
            {
                StepType = StepType.CUSTOM_SELECT,
                StepExpression = expression,
                Documentation = "Reponsável por chamar a etapa select de uma consulta personalizada"
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DBQueryStepModel PrepareWhereStep(Expression expression)
        {
            return new DBQueryStepModel
            {
                StepType = StepType.WHERE,
                StepExpression = expression,
                Documentation = "Responsável pelas condições da operação"
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DBQueryStepModel PrepareDistinctStep()
        {
            return new DBQueryStepModel
            {
                StepType = StepType.DISTINCT,
                Documentation = "Implica a Key DISTINCT a consulta acionada"
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DBQueryStepModel PrepareTopStep(int top)
        {
            return new DBQueryStepModel
            {
                StepType = StepType.TOP,
                StepValue = top,
                Documentation = "Implica a Key TOP a consulta acionada"
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DBQueryStepModel PrepareJoinStep(Expression expression)
        {
            return new DBQueryStepModel
            {
                StepType = StepType.JOIN,
                StepExpression = expression,
                Documentation = "Adiciona a instrução de INNER JOIN e suas condições('ON')"
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DBQueryStepModel PrepareLeftJoinStep(Expression expression)
        {
            return new DBQueryStepModel
            {
                StepType = StepType.LEFT_JOIN,
                StepExpression = expression,
                Documentation = "Adiciona a instrução de INNER JOIN e suas condições('ON')"
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DBQueryStepModel PrepareOrderByAscStep(Expression expression)
        {
            return new DBQueryStepModel
            {
                StepType = StepType.ORDER_BY_ASC,
                StepExpression = expression,
                Documentation = "Adiciona a instrução de ORDER BY ASC a consulta"
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DBQueryStepModel PreparePaginationStep(int pageSize, int pageNumber)
        {
            return new DBQueryStepModel
            {
                StepType = StepType.PAGINATION,
                PageNumber = pageNumber,
                PageSize = pageSize,
                Documentation = "Adiciona a paginação a consulta"
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DBQueryStepModel PrepareOrderByDescStep(Expression expression)
        {
            return new DBQueryStepModel
            {
                StepType = StepType.ORDER_BY_DESC,
                StepExpression = expression,
                Documentation = "Adiciona a instrução de ORDER BY DESC a consulta"
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DBQueryStepModel PrepareGroupByStep(Expression expression)
        {
            return new DBQueryStepModel
            {
                StepType = StepType.GROUP_BY,
                StepExpression = expression,
                Documentation = "Adiciona a instrução de GROUP BY a consulta"
            };
        }
    }
}

