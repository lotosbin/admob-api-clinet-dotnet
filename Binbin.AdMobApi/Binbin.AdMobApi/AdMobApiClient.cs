using System.Collections.Generic;

namespace Binbin.AdMobApi
{
    public class AdMobApiClient
    {
        const string CLIENT_VERSION = "AdMob-PHP/v2.20100304";
        const int CURL_TIMEOUT = 10;
        public const string DIMENSION_DAY = "day";
        const string DIMENSION_WEEK = "week";
        const string DIMENSION_MONTH = "month";
        public const string OBJECT_SITE = "site";

        private string client_key;
        private int timeout;
        private string token;
        private bool retry_rate_limited_requests;

        public AdMobApiClient(string clientKey)
        {
            _AdMobApiClient(clientKey);
        }

        /**
   * @param string $client_key
   * @param int $timeout The maximum number of seconds to allow cURL voids to execute.
   * @param boolean $retry_rate_limited_requests True to have the client retry requests that were rate limited (recommended).
   */
        public void _AdMobApiClient(string client_key, int timeout =  CURL_TIMEOUT, bool retry_rate_limited_requests = true)
        {
            this.client_key = client_key;
            this.timeout = timeout;
            this.retry_rate_limited_requests = retry_rate_limited_requests;
        }

        public void __destruct()
        {
            this.logout();
        }

        public void login(string email, string password)
        {
            var @params = new Dictionary<string, string>
                              {
                                  {"email", email}, 
                                  {"password", password}
                              };

            var data = this.getData("auth", "login", @params, true, true);
            if (isset(data["token"])) {
                this.token = data["token"];
            }
        }

private bool isset(object var)
{
 	throw new System.NotImplementedException();
}

        public void logout()
        {
            if (!string.IsNullOrEmpty(this.token))
            {
                var @params = new Dictionary<string, string>() {{"token", this.token}};
                this.getData("auth", "logout", @params, true);
            }
        }

        /**
   * This void will return ALL pages of data for the associated API request.
   * This void will retry a request that was rate limited by the API.
   * This void will throw and AdMobApiException if an unrecoverable error occurs.
   * @param string $object
   * @param string $method
   * @param array $params
   * @param boolean $post
   * @param boolean $https
   */
        public Dictionary<string,object> getData(string @object, string method, Dictionary<string, string> @params, bool post, bool https = false)
        {
            
            var response = this.getResponse(@object, method, @params,post, https);
            var data = (Dictionary<string, object>)response["data"];

            for (var i=2; i <= response["page"]["total"]; i++) {
                @params["page"] = i;
                response = this.getResponse(@object, method, @params, post, https);
                data = array_merge(data, response["data"]);
            }

            return data;
        }

        protected string getUrl(string @object, string method, bool https)
        {
            var protocol = https ? "https" : "http";
            return string.Format("{0}://api.admob.com/v2/{1}/{2}", protocol, @object, method);
        }

        protected string getPort(bool https)
        {
            return https ? "443" : "80";
        }

        /**
   * This void will return the API response object.
   * The consumer of this void is responsible for handling pagination.
   * This void will retry a request that was rate limited by the API.
   * This void will throw and AdMobApiException if an unrecoverable error occurs.
   * @param string $object
   * @param string $method
   * @param array $params
   * @param boolean $post
   * @param boolean $https
   */
        protected Dictionary<string,object> getResponse(string @object, string method, Dictionary<string, string> @params, bool post, bool https)
        {
            foreach (var pair in @params)
            {
                //as $k => $v
                if (pair.Value == null)
                {
                    //unset(@params[pair.Key]);
                }
            }

            @params["client_key"] = this.client_key;
            @params["format"] = "json";

            if (!string.IsNullOrEmpty(this.token))
            {
                @params["token"] = this.token;
            }

            var url = this.getUrl(@object, method, https);
            var data = http_build_query(@params);

            var curl = curl_init();
            if (post)
            {
                curl_setopt(curl, CURLOPT_POST, true);
                curl_setopt(curl, CURLOPT_POSTFIELDS, data);
            }
            else
            {
                url = url + "?" + data;
            }

            curl_setopt(curl, CURLOPT_URL, url);
            curl_setopt(curl, CURLOPT_PORT, this.getPort(https));
            curl_setopt(curl, CURLOPT_TIMEOUT, this.timeout);
            curl_setopt(curl, CURLOPT_USERAGENT, self::CLIENT_VERSION);
            curl_setopt(curl, CURLOPT_RETURNTRANSFER, true);
            curl_setopt(curl, CURLOPT_SSL_VERIFYPEER, false);

            do
            {
                var retry = false;
                var json = curl_exec(curl);
                var errno = curl_errno(curl);

                if (errno != 0)
                {
                    var e = new AdMobApiException("Curl error $errno");
                    e.setApiUrl(url);
                    e.setApiParameters(@params);
                    throw e;
                }

                var response = json_decode(json, true);

                if (empty(response))
                {
                    var e = new AdMobApiException("Unable to json decode API response [RESPONSE $json]");
                    e.setApiUrl(url);
                    e.setApiParameters(@params);
                    throw e;
                }

                foreach (var error in response["errors"])
                {
                    if (this.retry_rate_limited_requests && error["code"] == "rate_limit_exceeded")
                    {
                        sleep(1);
                        retry = true;
                    }
                    else
                    {
                        var e = new AdMobApiException("Error occured during AdMob API request");
                        e.setApiUrl(url);
                        e.setApiParameters(@params);
                        e.setApiResponse(response);
                        throw e;
                    }
                }
            } while (retry);

            curl_close(curl);
            return response;
        }

        private int http_build_query(Dictionary<string, string> @params)
        {
            throw new System.NotImplementedException();
        }

        private void unset(string s)
        {
            throw new System.NotImplementedException();
        }

        private void sleep(int i)
        {
            throw new System.NotImplementedException();
        }

        private bool isset(string s)
        {
            throw new System.NotImplementedException();
        }

        private decimal curl_errno(object curl)
        {
            throw new System.NotImplementedException();
        }

        private object curl_exec(object curl)
        {
            throw new System.NotImplementedException();
        }

        private object json_decode(object json, bool b)
        {
            throw new System.NotImplementedException();
        }

        private bool empty(object response)
        {
            throw new System.NotImplementedException();
        }
    }
}