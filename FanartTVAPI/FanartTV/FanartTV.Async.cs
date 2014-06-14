using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp;
using System.Net;

namespace FanartTV.V1
{
    public partial class FanartTV
    {
        #region Process Execution

        private void ProcessAsyncRequest<T>(RestRequest request, Action<FanartTVAsyncResult<T>> callback)
            where T : new()
        {
            var client = new RestClient(BASE_URL);
            client.AddHandler("application/json", new WatJsonDeserializer());
			client.AddHandler("application/json charset=utf-8", new WatJsonDeserializer());
			
			if (Timeout.HasValue)
                client.Timeout = Timeout.Value;

#if !WINDOWS_PHONE
            if (Proxy != null)
                client.Proxy = Proxy;
#endif

            Error = null;

            //request.AddHeader("Accept", "application/json");
            //request.AddParameter("api_key", ApiKey);

            ++AsyncCount;
            var asyncHandle = client.ExecuteAsync<T>(request, resp =>
            {
                --AsyncCount;
                var result = new FanartTVAsyncResult<T>
                {
                    Data = resp.Data != null ? resp.Data : default(T),
                    UserState = request.UserState
                };

                ResponseContent = resp.Content;
                ResponseHeaders = resp.Headers.ToDictionary(k => k.Name, v => v.Value);

				switch (resp.ResponseStatus)
				{
					case ResponseStatus.Completed:
						switch (resp.StatusCode)
						{
							case HttpStatusCode.OK:
								if (resp.Content == "Please specify a valid API key")
									result.Error = resp.Content;
								break;
							default:
								result.Error = resp.Content;
								break;
						}
						break;
					default:
						result.Error = "HTTP Error";
						break;
				}
				
				Error = result.Error;

                callback(result);
            });
        }


		#endregion
		
        private static bool CheckQuery<T>(string query, object userState, Action<FanartTVAsyncResult<T>> callback) 
            where T : class
        {
            if (string.IsNullOrEmpty(query))
            {
                callback(new FanartTVAsyncResult<T>
                {
                    Data = null,
                    Error = "Search cannot be empty",
                    UserState = userState
                });
                return false;
            }

            return true;
        }
        
		#region Movie Info

		/// <summary>
		/// Retrieve all the basic movie information for a particular movie by FanartTV reference.
		/// http://fanart.tv/api-docs/movie-api/
		/// </summary>
		/// <param name="apikey">FanartTV movie id</param>
		/// <param name="id">optional - ISO 639-1 language code</param>
		/// The following parameters are optional but if you want to change any from the default value you must specify all optional parameters before the one you are changing (so if you are changing just format you don’t have to specify type, but if you want to change the sort you MUST also specify the format and type)</param>
		/// <param name="format">Returns the results in the requested format - json (default) / php (returns a php serialized object)</param>
		/// <param name="type">Returns the requested image types - all (default) / movielogo / movieart / moviedisc</param>
		/// <param name="sort">1 – Sorted by most popular image then newest(default) / 2 – Sorted by newest uploaded image / 3 – Sorted by oldest uploaded image</param>
		/// <param name="limit">Value is either 1 (1 image) or 2 (all images – default), for example, when automatically downloading images you might only want to return the first result so the user doesn’t have to provide input, whereas with a manual download you might want the user to see all the options.</param>
		/// <returns></returns>
		public void GetMovieInfo(FanartTVMovieRequest Request, Action<FanartTVAsyncResult<FanartTVMovie>> callback)
        {
            //ProcessAsyncRequest<FanartTVMovie>(BuildGetMovieInfoRequest(MovieID, language, UserState), callback);
			ProcessAsyncRequest<FanartTVMovie>(Generator.GetMovieInfo(Request), callback);
        }

        public void GetSeriesInfo(FanartTVSeriesRequest Request, Action<FanartTVAsyncResult<FanartTVSeries>> callback)
        {
            //ProcessAsyncRequest<FanartTVMovie>(BuildGetMovieInfoRequest(MovieID, language, UserState), callback);
            ProcessAsyncRequest<FanartTVSeries>(Generator.GetSeriesInfo(Request), callback);
        }
        #endregion
    }
}
