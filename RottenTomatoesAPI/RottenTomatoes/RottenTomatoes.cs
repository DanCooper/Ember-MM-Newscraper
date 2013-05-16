using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp.Deserializers;
using System.Net;
using RestSharp;
using RottenTomatoes.Utilities;

namespace RottenTomatoes.V1
{
    public partial class RottenTomatoes
    {
		private const string BASE_URL = "http://api.rottentomatoes.com/api/public/v1.0/";

        public RottenTomatoes(string apiKey)
        {
            ApiKey = apiKey;
			Error = null;
            Timeout = null;

            Deserializer = new JsonDeserializer();
            Generator = new RequestGenerator(apiKey);
        }

        #region Properties

        private RequestGenerator Generator { get; set; }

        private RequestGenerator ETagGenerator { get; set; }

        private string ApiKey { get; set; }

        private string Language { get; set; }

        private JsonDeserializer Deserializer;

        #endregion

        /// <summary>
        /// Current count of outstanding Async calls awaiting callback response
        /// </summary>
        public int AsyncCount = 0;

		/// <summary>
		/// Error message
		/// </summary>
		public string Error { get; set; }

        /// <summary>
        /// String representation of response content
        /// </summary>
        public string ResponseContent { get; set; }

        /// <summary>
        /// Dictionary of Header values in response
		/// http://fanart.tv/api-docs/movie-api/
        /// </summary>
        public Dictionary<string, object> ResponseHeaders { get; set; }


#if !WINDOWS_PHONE
        /// <summary>
        /// Proxy to use for requests made.  Passed on to underying WebRequest if set.
        /// </summary>
        public IWebProxy Proxy { get; set; }
#endif
        /// <summary>
        /// Timeout in milliseconds to use for requests made.
        /// </summary>
        public int? Timeout { get; set; }




    }

}
