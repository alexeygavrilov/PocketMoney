using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using PocketMoney.Util.ExtensionMethods;

namespace PocketMoney.Data
{
    [DataContract]
    public abstract class ResultList<TData> : Result
    {
        [DataMember, Details]
        public new TData[] Data { get; set; }

        [DataMember, Details]
        public int TotalCount { get; set; }
    }

    [DataContract]
    public abstract class ResultStruct<TStruct> : Result where TStruct : struct
    {
        [DataMember, Details]
        public new TStruct Data { get; private set; }
    }

    [DataContract]
    public abstract class ResultClass<TClass> : Result where TClass : class
    {
        [DataMember, Details]
        public new TClass Data { get; set; }
    }

    [DataContract]
    public class Result : ObjectBase
    {
        public enum ErrorMessage
        {
            [Description(Result.ParameterName + " value is empty.")]
            EmptyValue,

            [Description(Result.ParameterName + " value has incorrect format.")]
            IncorrectFormat
        }

        public const String ParameterName = "{ParameterName}";

        private String _message = String.Empty;
        private Boolean _success = true;

        /// <summary>
        /// Create successfully result
        /// </summary>
        public Result()
        {
        }

        /// <summary>
        /// Create unsuccessfully result
        /// </summary>
        /// <param name="extraData">Some extra data</param>
        public Result(String extraData)
        {
            _success = false;
            _message = extraData;
        }

        [DataMember]
        public virtual object Data { get; private set; }

        [DataMember]
        public Boolean Success
        {
            get { return _success; }
            set { _success = value; }
        }

        [DataMember]
        public String Message
        {
            get { return _message; }
            set { _message = value; }
        }

        /// <summary>
        /// Sets the formatted error message.
        /// </summary>
        /// <param name="errorMessage">The error message string format template.</param>
        /// <param name="args">The args.</param>
        /// <returns></returns>
        public Result SetErrorMessage(String errorMessage, params string[] args)
        {
            var formattedErrorMessage = String.Format(errorMessage, args);

            return SetErrorMessage(formattedErrorMessage);
        }

        private void SetError(String errorMessage)
        {
            _success = false;
            _message += (_message.Length == 0)
                              ? errorMessage
                              : String.Format("\r\n{0}", errorMessage);
            this.Data = null;
        }
        /// <summary>
        /// Sets the error message.
        /// </summary>
        /// <param name="errorMessage">The error message.</param>
        /// <returns></returns>
        public Result SetErrorMessage(String errorMessage)
        {
            this.SetError(errorMessage);
            return this;
        }

        public T SetErrorMessage<T>(String errorMessage) where T : Result
        {
            this.SetError(errorMessage);
            return (T)this;
        }

        public Result SetParameter(String parameterName)
        {
            this._message = this._message.Replace("{ParameterName}", parameterName);

            return this;
        }
    }
}