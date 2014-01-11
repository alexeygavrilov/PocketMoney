using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using PocketMoney.Data;
using PocketMoney.Model;
using PocketMoney.Model.External.Results;
using PocketMoney.Model.Network;
using PocketMoney.Service.Interfaces;

namespace PocketMoney.Service
{
    public class Connector : IConnector
    {
        private ICurrentUserProvider _currentUserProvider;
        private WebClient _httpClient;

        public Connector(ICurrentUserProvider currentUserProvider)
        {
            _currentUserProvider = currentUserProvider;
            _httpClient = new WebClient();
        }

        private string GetAuthTokenKey(NetworkType type)
        {
            return type.ToString() + "_auth_token";
        }

        private string GetHttpAuthUrl(NetworkType type)
        {
            if (type == NetworkType.VK)
                return "https://oauth.vk.com/access_token?client_id={0}&client_secret={1}&v=5.5&grant_type=client_credentials";
            else
                throw new NotImplementedException();
        }


        private string GetToken(NetworkType type)
        {
            var token = _currentUserProvider.GetDate(GetAuthTokenKey(type)) as string;

            if (string.IsNullOrEmpty(token))
                throw new DataNotFoundException("access_token", "Access token is not found");

            return token;
        }

        public StringResult GetAuthToken(AuthRequest auth)
        {
            string token = null;
            using (var stream = _httpClient.OpenRead(string.Format(this.GetHttpAuthUrl(auth.Type),
                Properties.Settings.Default.VK_ApiId,
                Properties.Settings.Default.VK_ApiKey)))
            {
                var rawJson = new StreamReader(stream).ReadToEnd();
                var json = JObject.Parse(rawJson);
                if (json["error"].HasValues)
                {
                    throw new InternalServiceException(string.Format("Invalid request to '{0}' network return '{1}' status and '{2}' message",
                        auth.Type, json["error"].ToObject<string>(), json["error_description"].ToObject<string>()));
                }
                if (json["access_token"].HasValues)
                    token = json["access_token"].ToObject<string>();
            }

            if (string.IsNullOrEmpty(token))
                throw new DataNotFoundException("access_token", "Access token is not found");

            _currentUserProvider.SetData(GetAuthTokenKey(auth.Type), token);

            return new StringResult { Data = token };
        }

        public NetworkAccountResult GetAccount(StringNetworkRequest identity)
        {
            throw new NotImplementedException();
        }

        public NetworkAccountList SearchAccount(StringNetworkRequest query)
        {
            throw new NotImplementedException();
        }
    }
}
