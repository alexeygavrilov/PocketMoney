using System;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Castle.Core.Logging;
using Castle.Facilities.NHibernateIntegration;
using Castle.Services.Transaction;
using NHibernate;
using NHibernate.Linq;
using PocketMoney.Util;

namespace PocketMoney.Data.NHibernate
{
    //[Transactional]
    public class Repository<TEntity, TIdentity, TIdentityIdType> : Disposable, IRepository<TEntity, TIdentity, TIdentityIdType>
        where TEntity : Entity<TEntity, TIdentity, TIdentityIdType>
        where TIdentity : AbstractIdentity<TIdentityIdType>
        where TIdentityIdType : struct
    {
        private ISessionManager _sessionManager;

        public Repository(ISessionManager sessionManager, ILogger logger)
        {
            if (sessionManager == null)
                throw new ArgumentNullException("sessionManager");
            _sessionManager = sessionManager;
            _logger = logger;
        }

        protected override void DisposeCore()
        {
            _sessionManager = null;
            _logger = null;
            base.DisposeCore();
        }

        protected ISession Session
        {
            get
            {
                ThrowErrorIfDisposed();
                return _sessionManager.OpenSession();
            }
        }


        private ILogger _logger;

        #region IRepository<TEntity,TIdentity,TIdentityIdType> Members

        //[Transaction(TransactionMode.Requires)]
        public virtual void Add(TEntity instance)
        {
            var session = Session;
            DoLogRepositoryChanges(session, instance);
            session.Save(instance);
        }

        //[Transaction(TransactionMode.Requires)]
        public virtual void Update(TEntity instance)
        {
            var session = Session;
            DoLogRepositoryChanges(session, instance);
            CheckLocking(instance);
            session.SaveOrUpdate(instance);
        }

        //[Transaction(TransactionMode.Requires)]
        public virtual void Remove(TEntity instance)
        {
            var session = Session;
            DoLogRepositoryChanges(session, instance);
            CheckLocking(instance);
            if (instance is IActivable)
            {
                ((IActivable)instance).Active = false;
                session.SaveOrUpdate(instance);
            }
            else
                session.Delete(instance);
        }

        public virtual bool Exists(Expression<Func<TEntity, bool>> predicate)
        {
            using (ISession s = Session)
            {
                return s.Query<TEntity>().Any(predicate);
            }
        }

        public virtual int Count()
        {
            return Session.Query<TEntity>().Count();
        }

        public virtual int Count(Expression<Func<TEntity, bool>> predicate)
        {
            return Session.Query<TEntity>().Count(predicate);
        }

        public virtual TEntity One(TIdentity identity)
        {
            return Session.Get<TEntity>(identity.Id);
        }

        public virtual TEntity FindOne(Expression<Func<TEntity, bool>> predicate)
        {
            return Session.Query<TEntity>().FirstOrDefault(predicate);
        }

        public virtual IQueryable<TEntity> FindAll(Expression<Func<TEntity, bool>> predicate)
        {
            return Session.Query<TEntity>().Where(predicate);
        }

        public virtual IQueryable<TEntity> All()
        {
            return Session.Query<TEntity>();
        }

        public virtual IQueryable<T> AllOf<T>() where T : TEntity
        {
            return Session.Query<T>();
        }

        public virtual IQueryable<TEntity> Linq()
        {
            return Session.Linq<TEntity>();
        }

        public virtual IQueryable<T> LinqOf<T>() where T : TEntity
        {
            return Session.Linq<T>();
        }

        public virtual TEntity FindOrAdd(Expression<Func<TEntity, bool>> predicate, TEntity entity, out bool wasFound)
        {
            lock (typeof(TEntity))
            {
                ISession session = Session;

                TEntity e =
                    session.Query<TEntity>().FirstOrDefault(
                        predicate);
                if (e != null)
                {
                    if (!(e is IActivable))
                    {
                        wasFound = true;
                        return e;
                    }
                    if (((IActivable)e).Active)
                    {
                        wasFound = true;
                        return e;
                    }
                }
                wasFound = false;
                DoLogRepositoryChanges(session, entity);
                session.Save(entity);
                return entity;
            }
        }
        public virtual T FindOrAdd<T>(Expression<Func<TEntity, bool>> predicate, T entity, out bool wasFound)
            where T : TEntity
        {
            lock (typeof(TEntity))
            {
                ISession session = Session;

                T e =
                    session.Query<TEntity>().FirstOrDefault(
                        predicate).As<T>();
                if (e != null)
                {
                    if (!(e is IActivable))
                    {
                        wasFound = true;
                        return e;
                    }
                    if (((IActivable)e).Active)
                    {
                        wasFound = true;
                        return e;
                    }
                }
                wasFound = false;
                DoLogRepositoryChanges(session, entity);
                session.Save(entity);
                return entity;
            }
        }

        #endregion

        #region Logging
        private void DoLogRepositoryChanges(ISession session, params TEntity[] parameters)
        {

            if ((_logger != null) && (_logger.IsInfoEnabled))
            {
                var msg = new StringBuilder();
                msg.AppendLine("=======");
                if (session != null)
                {
                    if (session.Transaction != null)
                    {
                        msg.AppendLine(session.Transaction.IsActive ? "Transaction ACTIVE" : "Transaction INACTIVE");
                    }
                    else
                        msg.AppendLine("No transaction");
                }

                msg.AppendLine(String.Format("Stack trace: {0}", new StackTrace().ToString()));

                foreach (var parameter in parameters)
                    try
                    {
                        if (parameter != null)
                        {
                            msg.AppendLine(String.Format("Parameter {0}, {1}", parameter.GetType(), parameter.Id));
                            //msg.AppendLine("Fields: ");
                            //ObjectDumper.Write(parameter, 1, new StringWriter(msg));
                            //msg.AppendLine();
                        }
                    }
                    catch (Exception ex)
                    {
                        msg.Append("Failed to dump object: " + ex);
                    }
                msg.AppendLine("=======");
                _logger.Info(msg.ToString());
            }
        }

        #endregion

        #region Locking

        //[Transaction(TransactionMode.Requires)]
        public virtual void Lock(IEntityLock instance, int minutes)
        {
            var session = Session;
            instance.Locked = true;
            instance.LockedUntil = Clock.UtcNow().AddMinutes(minutes);
            session.SaveOrUpdate(instance);
        }

        //[Transaction(TransactionMode.Requires)]
        public virtual void Unlock(IEntityLock instance)
        {
            var session = Session;
            instance.Locked = false;
            instance.LockedUntil = null;
            session.SaveOrUpdate(instance);
        }

        private void CheckLocking(TEntity instance)
        {
            if (instance is IEntityLock)
            {
                var lockInst = (IEntityLock)instance;
                if (lockInst.Locked && lockInst.LockedUntil.HasValue)
                    if (lockInst.LockedUntil < Clock.UtcNow())
                        throw new ArgumentException(string.Format("Entity {0} is locked until {1}. Please wait or unlock object manually.", instance.GetType().Name, lockInst.LockedUntil.Value));
            }
        }
        #endregion

        #region Implementation of IRepositoryExtensibilityPoint

        public object GetSession()
        {
            return Session;
        }

        #endregion


    }

    public static class RepositoryExtensions
    {
        public static IQueryOver<TEntity, TEntity> QueryOver<TEntity, TIdentity, TIdentityIdType>(this IRepository<TEntity, TIdentity, TIdentityIdType> repository)
            where TEntity : Entity<TEntity, TIdentity, TIdentityIdType>
            where TIdentity : AbstractIdentity<TIdentityIdType>
            where TIdentityIdType : struct
        {
            if (repository == null)
                throw new ArgumentNullException("repository");
            var strongRepo = (IRepositoryExtensibilityPoint)repository;
            return ((ISession)(strongRepo.GetSession())).QueryOver<TEntity>();
        }

        public static IQueryOver<T, T> QueryOverOf<T, TEntity, TIdentity, TIdentityIdType>(this IRepository<TEntity, TIdentity, TIdentityIdType> repository)
            where T : TEntity
            where TEntity : Entity<TEntity, TIdentity, TIdentityIdType>
            where TIdentity : AbstractIdentity<TIdentityIdType>
            where TIdentityIdType : struct
        {
            if (repository == null)
                throw new ArgumentNullException("repository");
            var strongRepo = (IRepositoryExtensibilityPoint)repository;
            return ((ISession)(strongRepo.GetSession())).QueryOver<T>();
        }

    }
}