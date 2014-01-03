using System;

namespace PocketMoney.Util.Http
{
    public interface IHttpService
    {
        HttpServiceResult Get(string url);

        void Get(string url, Action<HttpServiceResult> onSuccess, Action<Exception> onError);
    }
}