
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Binbin.AdMobApi
{
    public class AdMobApiException : Exception
    {
        #region Constructors

        public AdMobApiException()
        {
        }

        public AdMobApiException(string message)
            : base(message)
        {
        }

        protected AdMobApiException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public AdMobApiException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        #endregion

        private string api_url;
        private Dictionary<string, string> api_parameters;
        private Dictionary<string, string> api_response;
        private string message;

        public void setApiUrl(string api_url
)
        {

            this.api_url = api_url;
            this.message += " [URL {$this.api_url}]";
        }

        public void setApiParameters(Dictionary<string, string> api_parameters)
        {
            if (isset(api_parameters["password"]))
            {
                api_parameters["password"] = "";
            }

            this.api_parameters = api_parameters;

            var parameter_string = http_build_query(api_parameters);

            this.message += " [PARAMETERS "+parameter_string+"]";
        }

        private string http_build_query(Dictionary<string, string> apiParameters)
        {
            throw new NotImplementedException();
        }

        private bool isset(string apiParameter)
        {
            throw new NotImplementedException();
        }

        public void setApiResponse(Dictionary<string,string> api_response)
        {
            this.api_response = api_response;

            foreach (var error in api_response["errors"])
            {
                this.message += " [ERROR {$error['code']} {$error['msg']}]";
            }

            foreach (var warning in api_response["warnings"])
            {
                this.message += " [WARNING {" + warning["code"] + "} {" + warning["msg"] + "}]";
            }
        }

        public string getApiUrl()
        {
            return this.api_url;
        }

        public Dictionary<string, string> getApiParameters()
        {
            return this.api_parameters;
        }

        public Dictionary<string, string> getApiResponse()
        {
            return this.api_response;
        }
    }
}