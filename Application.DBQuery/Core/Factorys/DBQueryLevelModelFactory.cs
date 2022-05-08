using Application.Domains.Entities;
using DBQuery.Core.Enuns;
using DBQuery.Core.Model;
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
        /// 
        /// </summary>
        /// <returns></returns>
        public DBQueryLevelModel PrepareAliasStep(string alias)
        {
            return new DBQueryLevelModel()
            {
                LevelType = StepType.USE_ALIAS,
                LevelValue = alias,
                Documentation = "Responsável por indicar se os apelidos usados nas expressions serão denominados como chaves apelidos 'as'"
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DBQueryLevelModel PrepareInsertStep (EntityBase domain)
        {
            return new DBQueryLevelModel()
            {
                LevelType = StepType.INSERT,
                LevelValue = domain,
                Documentation = "Responsável por chamar a etapa de insert. A mesma recebe um objeto preenchido do tipo TEntity para a persistência"
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DBQueryLevelModel PrepareDeleteStep()
        {
            return new DBQueryLevelModel
            {
                LevelType = StepType.DELETE,
                Documentation = "Reponsável por chamar a etapa de delete"
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DBQueryLevelModel PrepareSimpleSelectStep()
        {
            return new DBQueryLevelModel
            {
                LevelType = StepType.SELECT,
                Documentation = "Reponsável por chamar a etapa select de uma consulta simples(sem joins)"
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DBQueryLevelModel PrepareUpdateStep(EntityBase domain)
        {
            return new DBQueryLevelModel
            {
                LevelType = StepType.UPDATE,
                LevelValue = domain,
                Documentation = "Reponsável por chamar a etapa update. Recebendo parametro do tipo TEntity para atualização"
            };
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DBQueryLevelModel PrepareInsertIfNotExistsStep(EntityBase domain)
        {
            return new DBQueryLevelModel
            {
                LevelType = StepType.INSERT_NOT_EXISTS,
                LevelValue = domain,
                Documentation = "Responsável por chamar a etapa de insert. A mesma recebe um objeto preenchido do tipo TEntity para a persistência, onde será válidada se a tabela não possui nenhum registro igual, com apenas a chave unica diferente. Sendo esse atributo ignorado na validação, representado com a anotação Identity"
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DBQueryLevelModel PrepareExecuteStep()
        {
            return new DBQueryLevelModel
            {
                LevelType = StepType.EXECUTE,
                Documentation = "Comando usado para iniciar processe de leitura e execução da query"
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DBQueryLevelModel PrepareSelectStep(Expression expression)
        {
            return new DBQueryLevelModel
            {
                LevelType = StepType.CUSTOM_SELECT,
                LevelExpression = expression,
                Documentation = "Reponsável por chamar a etapa select de uma consulta personalizada"
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DBQueryLevelModel PrepareWhereStep(Expression expression)
        {
            return new DBQueryLevelModel
            {
                LevelType = StepType.WHERE,
                LevelExpression = expression,
                Documentation = "Responsável pelas condições da operação"
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DBQueryLevelModel PrepareDistinctStep()
        {
            return new DBQueryLevelModel
            {
                LevelType = StepType.DISTINCT,
                Documentation = "Implica a Key DISTINCT a consulta acionada"
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DBQueryLevelModel PrepareTopStep(int top)
        {
            return new DBQueryLevelModel
            {
                LevelType = StepType.TOP,
                LevelValue = top,
                Documentation = "Implica a Key TOP a consulta acionada"
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DBQueryLevelModel PrepareJoinStep(Expression expression)
        {
            return new DBQueryLevelModel
            {
                LevelType = StepType.JOIN,
                LevelExpression = expression,
                Documentation = "Adiciona a instrução de INNER JOIN e suas condições('ON')"
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DBQueryLevelModel PrepareLeftJoinStep(Expression expression)
        {
            return new DBQueryLevelModel
            {
                LevelType = StepType.LEFT_JOIN,
                LevelExpression = expression,
                Documentation = "Adiciona a instrução de INNER JOIN e suas condições('ON')"
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DBQueryLevelModel PrepareOrderByAscStep(Expression expression)
        {
            return new DBQueryLevelModel
            {
                LevelType = StepType.ORDER_BY_ASC,
                LevelExpression = expression,
                Documentation = "Adiciona a instrução de ORDER BY ASC a consulta"
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DBQueryLevelModel PrepareOrderByDescStep(Expression expression)
        {
            return new DBQueryLevelModel
            {
                LevelType = StepType.ORDER_BY_DESC,
                LevelExpression = expression,
                Documentation = "Adiciona a instrução de ORDER BY DESC a consulta"
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DBQueryLevelModel PrepareGroupByStep(Expression expression)
        {
            return new DBQueryLevelModel
            {
                LevelType = StepType.GROUP_BY,
                LevelExpression = expression,
                Documentation = "Adiciona a instrução de GROUP BY a consulta"
            };
        }
    }
}

