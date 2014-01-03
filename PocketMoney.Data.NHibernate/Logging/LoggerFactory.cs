// -----------------------------------------------------------------------
// <copyright file="LoggerFactory.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace PocketMoney.Data.NHibernate.Logging
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Security.Principal;
    using System.Web;

    using PocketMoney.Util;
    using PocketMoney.Util.Logging;

    using Castle.Core.Logging;

    using global::NHibernate;

    using Microsoft.IdentityModel.Claims;
    using Microsoft.Practices.ServiceLocation;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [Serializable]
    public class LoggerFactory : AbstractLoggerFactory
    {
        #region Overrides of AbstractLoggerFactory

        public override ILogger Create(string name)
        {
            return new Logger(name);
        }

        public override ILogger Create(string name, LoggerLevel level)
        {
            return new Logger(name,level);
        }

        #endregion
    }


    [Serializable]
    public class Logger : LoggerBase
    {
        public Logger(string name)
            : base(name)
        {
            this.Level = LoggerLevel.Error;
        }

        public Logger(string name, LoggerLevel logLevel)
            : base(name, logLevel)
        {
            this.Level = LoggerLevel.Error;
        }


        #region Overrides of LevelFilteredLogger

        public override ILogger CreateChildLogger(string loggerName)
        {
            if (loggerName == null)
            {
                throw new ArgumentNullException(
                    "loggerName", @"To create a child logger you must supply a non null name");
            }
            return
                new Logger(
                    string.Format(CultureInfo.CurrentCulture, "{0}.{1}", new object[] { this.Name, loggerName }),
                    this.Level);
        }


        protected override void Log(LoggerLevel loggerLevel, string loggerName, string message, Exception exception)
        {
           
            if (loggerLevel <= LoggerLevel.Error)
            {
                Log2Db(loggerLevel, message, exception);
            }
        }

        private static void Log2Db(LoggerLevel loggerLevel, string message, Exception exception)
        {
            ISessionFactory sessionFactory = null;
            try
            {
                sessionFactory = ServiceLocator.Current.GetInstance<ISessionFactory>();
            }
            catch
            {
                return;
            }
            if (sessionFactory != null)
            try
            {
                ErrorLog error = Create(loggerLevel, message, exception);
                using (var session = sessionFactory.OpenStatelessSession())
                {
                    using (var transaction = session.BeginTransaction())
                        try
                        {

                            session.Insert(error);
                            transaction.Commit();
                        }
                        catch (Exception)
                        {
                            transaction.Rollback();
                        }
                }
            }
            finally
            {
                sessionFactory.Dispose();
            }
        }

        private static ErrorLog Create(LoggerLevel loggerLevel, string message, Exception exception)
        {
            return new ErrorLog
                { Date = Clock.UtcNow(), Thread = GetThread(),Level = loggerLevel.ToString(), Message = message,Exception = GetExeption(exception),User = GetUser(),Request = GetRequest(),Response = GetResponse(), StackTrace = GetStackTrace()
                };

        }

        #endregion

        #region Overrides of LoggerBase
        static Logger()
        {
            ClaimsFetcher = GetClaims;
        }

        protected static string GetClaims(IIdentity identity)
        {
            if (identity is IClaimsIdentity)
                return String.Join("\r\n",((IClaimsIdentity)identity).Claims.Select(c => c.ToString()).ToArray());
            return  identity.Name + " is not a claim idenity";
        }

        #endregion
    }

    public class ErrorLog
    {
        public virtual Guid Id { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual string Thread { get; set; }
        public virtual string Level { get; set; }       
        public virtual string Message { get; set; }
        public virtual string Exception { get; set; }
        public virtual string User { get; set; }
        public virtual string Request { get; set; }
        public virtual string Response { get; set; }
        public virtual string StackTrace { get; set; }
    }

}
