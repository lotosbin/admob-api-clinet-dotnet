using System.Collections.Generic;
using System.Web.Script.Serialization;
using Binbin.HttpHelper;

namespace Binbin.AdMobApi
{
    public class AdMobApiClientV2
    {
        private string client_key;
        private string email;
        private string password;
        public string token;
        public AdMobApiClientV2(string clientKey, string email, string password)
        {
            this.client_key = clientKey;
            this.email = email;
            this.password = password;
        }

        public string AuthLogin()
        {
            var paras = new List<APIParameter>()
                            {
                                new APIParameter("client_key", this.client_key),
                                new APIParameter("email",(this.email)),
                                new APIParameter("password", (this.password)),
                            };

            return new SyncHttpRequest().HttpPost("https://api.admob.com/v2/auth/login", paras);
        }

        public void GetToken(string result)
        {
            if (result.Contains("token"))
            {
                var startTag = "\"token\":\"";
                int startIndex = result.IndexOf(startTag) + startTag.Length;
                int endIndex = result.IndexOf("\"", startIndex);
                this.token = result.Substring(startIndex, endIndex - startIndex);
            }
        }

        public AdMobApiReturn<AdMobApiAuthLoginData> AuthLogin2()
        {
            var result = this.AuthLogin();
            return new JavaScriptSerializer().Deserialize<AdMobApiReturn<AdMobApiAuthLoginData>>(result);
        }
        public string AuthLogout()
        {
            var paras = new List<APIParameter>()
                            {
                                new APIParameter("client_key", this.client_key),
                                new APIParameter("token",(this.token)),
                            };
            return new SyncHttpRequest().HttpPost("https://api.admob.com/v2/auth/logout", paras);
        }
        public string SiteSearch()
        {
            var paras = new List<APIParameter>()
                            {
                                new APIParameter("client_key", this.client_key),
                                new APIParameter("token",(this.token)),
                            };
            return new SyncHttpRequest().HttpGet("https://api.admob.com/v2/site/search", paras);
        }
        public AdMobApiReturns<AdMobApiSiteSearchData> SiteSearch2()
        {
            var result = this.SiteSearch();
            return new JavaScriptSerializer().Deserialize<AdMobApiReturns<AdMobApiSiteSearchData>>(result);
        }
    }

    public class AdMobApiAuthLoginData
    {
        public string token { get; set; }
    }
}