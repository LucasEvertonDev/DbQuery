using DB.Query.Models.Entities;
using DB.Query.Core.Steps.CustomSelect;
using DB.Query.Core.Steps.Delete;
using DB.Query.Core.Steps.Insert;
using DB.Query.Core.Steps.Select;
using DB.Query.Core.Steps.Update;
using DB.Query.Services;
using System;
using System.Linq.Expressions;

namespace DB.Query.Core.Repositorys
{
    public interface IRepositoryBase<TEntity> where TEntity : EntityBase
    {
        void BindTransaction(DBTransaction dataBaseService);
        DeleteStep<TEntity> Delete();
        DeleteStep<TEntity> DeleteAndInsert(TEntity domain);
        DBTransaction GetTransaction();
        InsertStep<TEntity> Insert(TEntity domain);
        InsertStep<TEntity> InsertIfNotExists(TEntity domain);
        UpdateStep<TEntity> InsertOrUpdate(TEntity domain);
        SelectStep<TEntity> Select();
        CustomSelectStep<TEntity> Select(Expression<Func<TEntity, dynamic[]>> expression);
        CustomSelectStep<TEntity> Select(Expression<Func<TEntity, dynamic>> expression);
        CustomSelectStep<TEntity> Select<Entity1, Entity2, Entity3, Entity4, Entity5>(Expression<Func<Entity1, Entity2, Entity3, Entity4, Entity5, dynamic[]>> expression);
        CustomSelectStep<TEntity> Select<Entity1, Entity2, Entity3, Entity4, Entity5>(Expression<Func<Entity1, Entity2, Entity3, Entity4, Entity5, dynamic>> expression);
        CustomSelectStep<TEntity> Select<Entity1, Entity2, Entity3, Entity4>(Expression<Func<Entity1, Entity2, Entity3, Entity4, dynamic[]>> expression);
        CustomSelectStep<TEntity> Select<Entity1, Entity2, Entity3, Entity4>(Expression<Func<Entity1, Entity2, Entity3, Entity4, dynamic>> expression);
        CustomSelectStep<TEntity> Select<Entity1, Entity2, Entity3>(Expression<Func<Entity1, Entity2, Entity3, dynamic[]>> expression);
        CustomSelectStep<TEntity> Select<Entity1, Entity2, Entity3>(Expression<Func<Entity1, Entity2, Entity3, dynamic>> expression);
        CustomSelectStep<TEntity> Select<Entity1, Entity2>(Expression<Func<Entity1, Entity2, dynamic[]>> expression);
        CustomSelectStep<TEntity> Select<Entity1, Entity2>(Expression<Func<Entity1, Entity2, dynamic>> expression);
        CustomSelectStep<TEntity> Select<Entity1>(Expression<Func<Entity1, dynamic[]>> expression);
        CustomSelectStep<TEntity> Select<Entity1>(Expression<Func<Entity1, dynamic>> expression);
        UpdateStep<TEntity> Update(TEntity domain);
    }
}