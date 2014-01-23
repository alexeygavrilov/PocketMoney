using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.ServiceModel;
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

        private string GetHttpAuthUrl(NetworkType type, string apiId, string apiKey)
        {
            if (type == NetworkType.VK)
                return string.Format("https://oauth.vk.com/access_token?client_id={0}&client_secret={1}&v=5.5&grant_type=client_credentials", apiId, apiKey);
            else
                throw new NotImplementedException();
        }
        private string GetToken(NetworkType type)
        {
            var token = _currentUserProvider.GetDate(GetAuthTokenKey(type)) as string;

            if (string.IsNullOrEmpty(token))
            {
                var json = this.Request(this.GetHttpAuthUrl(type, Properties.Settings.Default.VK_ApiId, Properties.Settings.Default.VK_ApiKey), type);

                if (json["access_token"] != null)
                    token = json["access_token"].ToObject<string>();

                if (string.IsNullOrEmpty(token))
                    throw new DataNotFoundException("access_token", "Access token is not found");

                _currentUserProvider.SetData(GetAuthTokenKey(type), token);
            }

            return token;
        }

        private string GetHttpApiUrl(NetworkType type, string methodName, IDictionary<string, string> args, bool addToken = true)
        {
            if (type == NetworkType.VK)
            {
                string parameters = string.Join("&",
                    args.Select(x => string.Format("{0}={1}", x.Key, x.Value)).ToArray());
                if (addToken)
                    return string.Format("https://api.vk.com/method/{0}?{1}&v=5.5&access_token={2}", methodName, parameters, this.GetToken(type));
                else
                    return string.Format("https://api.vk.com/method/{0}?{1}&v=5.5", methodName, parameters);
            }
            else
                throw new NotImplementedException();
        }

        private JObject Request(string url, NetworkType type)
        {
            using (var stream = _httpClient.OpenRead(url))
            {
                var rawJson = new StreamReader(stream).ReadToEnd();
                var json = JObject.Parse(rawJson);
                if (json["error"] != null)
                {
                    if (json["error"].Type == JTokenType.Object)
                        throw new InternalServiceException(string.Format("Invalid request to '{0}' network return '{1}' status and '{2}' message",
                            type, json["error"]["error_code"].ToObject<string>(), json["error"]["error_msg"].ToObject<string>()));
                    else
                        throw new InternalServiceException(string.Format("Invalid request to '{0}' network return '{1}' status and '{2}' message",
                            type, json["error"].ToObject<string>(), json["error_description"].ToObject<string>()));
                }
                return json;
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        [OperationBehavior]
        public NetworkAccountResult GetAccount(StringNetworkRequest identity)
        {
            var args = new Dictionary<string, string>();
            args.Add("user_ids", identity.Data);
            args.Add("fields", "photo");
            var json = this.Request(this.GetHttpApiUrl(identity.Type, "users.get", args, false), identity.Type);
            if (json["response"] != null && json["response"].First != null)
            {
                var first = json["response"].First;
                NetworkAccount account = new NetworkAccount
                {
                    UserId = first["id"].ToObject<string>(),
                    FirstName = first["first_name"].ToObject<string>(),
                    LastName = first["last_name"].ToObject<string>(),
                    Photo = first["photo"].ToObject<string>()
                };
                return new NetworkAccountResult { Data = account };
            }
            else
            {
                throw new DataNotFoundException("user", "User not found in network");
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        [OperationBehavior]
        public NetworkAccountList SearchAccount(StringNetworkRequest query)
        {
            var args = new Dictionary<string, string>();
            args.Add("q", query.Data);
            args.Add("count", "10");
            args.Add("fields", "photo_50");
            var url = this.GetHttpApiUrl(query.Type, "users.search", args);
            var json = this.Request(url, query.Type);
            IList<NetworkAccount> list = new List<NetworkAccount>();
            int count = 0;
            if (json["response"] != null)
            {
                count = json["response"]["count"].ToObject<int>();
                foreach (var account in json["response"]["items"])
                {
                    list.Add(new NetworkAccount
                    {
                        UserId = account["id"].ToObject<string>(),
                        FirstName = account["first_name"].ToObject<string>(),
                        LastName = account["last_name"].ToObject<string>(),
                        Photo = account["photo_50"].ToObject<string>()
                    });
                }
            }
            return new NetworkAccountList { List = list.ToArray(), TotalCount = count };
        }
    }
}
