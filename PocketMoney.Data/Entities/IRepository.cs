using System;
using System.Linq;
using System.Linq.Expressions;
using PocketMoney.Data;

namespace PocketMoney.Data
{
    public interface IRepository<TEntity, in TIdentity, TIdentityIdType> : IRepositoryExtensibilityPoint
        where TEntity : Entity<TEntity, TIdentity, TIdentityIdType>
        where TIdentity : AbstractIdentity<TIdentityIdType>
        where TIdentityIdType : struct
    {

        void Add(TEntity instance);

        void Update(TEntity instance);

        void Remove(TEntity instance);

        bool Exists(Expression<Func<TEntity, bool>> predicate);

        int Count();

        int Count(Expression<Func<TEntity, bool>> predicate);

        TEntity One(TIdentity identity);

        TEntity FindOne(Expression<Func<TEntity, bool>> predicate);

        TEntity FindOrAdd(Expression<Func<TEntity, bool>> predicate, TEntity entity, out bool wasFound);

        T FindOrAdd<T>(Expression<Func<TEntity, bool>> predicate, T entity, out bool wasFound) where T : TEntity;

        IQueryable<TEntity> FindAll(Expression<Func<TEntity, bool>> predicate);

        IQueryable<TEntity> All();

        IQueryable<T> AllOf<T>() where T : TEntity;

        IQueryable<TEntity> Linq();

        IQueryable<T> LinqOf<T>() where T : TEntity;

        void Lock(IEntityLock instance, int minutes);

        void Unlock(IEntityLock instance);
    }
    public interface IRepositoryExtensibilityPoint
    {
        object GetSession();
    }
}