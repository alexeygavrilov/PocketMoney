// -----------------------------------------------------------------------
// <copyright file="ExceptionExtensions.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Diagnostics;
using Castle.Core.Logging;
using Microsoft.Practices.ServiceLocation;

namespace PocketMoney.Util.ExtensionMethods
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public static class ExceptionExtensions
    {
        private static readonly ILogger Logger;

        static ExceptionExtensions()
        {
            try
            {
                Logger = (ServiceLocator.Current != null ? ServiceLocator.Current.GetInstance<ILogger>() : new NullLogger()) ??
                         new NullLogger();
            }
            catch (Exception ex)
            {
                Logger = new NullLogger();
                WriteLogElseWhere(ex.ToString());
                Logger.Error("Failed to create logger:",ex);
            }
        }

        private static void DoLogMessage(string message, Action<string, Exception> logMethod)
        {
            try
            {
                logMethod(message + Environment.NewLine, null);
            }
            catch (Exception)
            {
                WriteLogElseWhere(message);
            }            
        }

        private static Exception DoLog(Exception exception, Action<string,Exception> logMethod)
        {
            try
            {
                logMethod(exception.Message + Environment.NewLine, exception);
            }
            catch (Exception)
            {
                if (exception!=null)
                    WriteLogElseWhere(exception.ToString());
            }
            return exception;
        }

        private static void WriteLogElseWhere(string message)
        {
            try{Trace.WriteLine(message);}catch (Exception)
            {};
            try{Console.WriteLine(message); }catch (Exception)
            {};
            try {Debug.WriteLine(message); }catch (Exception)
            {};
        }

        public static Exception LogDebug(this Exception exception)
        {
            return DoLog(exception, Logger.Debug);
        }

        public static Exception LogError(this Exception exception)
        {
            return DoLog(exception, Logger.Error);
        }

        public static Exception LogFatal(this Exception exception)
        {
            return DoLog(exception, Logger.Fatal);
        }


        public static Exception LogDebug(this Exception exception,string message)
        {
            DoLogMessage(message,  Logger.Debug);
            return DoLog(exception, Logger.Debug);
        }

      

        public static Exception LogError(this Exception exception, string message)
        {
            DoLogMessage(message, Logger.Error);
            return DoLog(exception, Logger.Error);
        }

        public static Exception LogFatal(this Exception exception, string message)
        {
            DoLogMessage(message, Logger.Fatal);
            return DoLog(exception, Logger.Fatal);
        }
    }
}