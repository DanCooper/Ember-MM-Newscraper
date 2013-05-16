using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp;
using System.Net;

namespace RottenTomatoes.V1
{
    public partial class RottenTomatoes
    {
        #region Process Execution

        private void ProcessAsyncRequest<T>(RestRequest request, Action<RottenTomatoesAsyncResult<T>> callback)
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
                var result = new RottenTomatoesAsyncResult<T>
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
		
        private static bool CheckQuery<T>(string query, object userState, Action<RottenTomatoesAsyncResult<T>> callback) 
            where T : class
        {
            if (string.IsNullOrEmpty(query))
            {
                callback(new RottenTomatoesAsyncResult<T>
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
		/// Retrieve all the basic movie information for a particular movie by RottenTomatoes reference.
		/// http://api.rottentomatoes.com/api/public/v1.0/movies/<id>.json?apikey=<apikey>
		/// </summary>
		/// <param name="id">RottenTomatoes movie id</param>
		/// <returns></returns>
		public void GetMovieInfo(RottenTomatoesRequest Request, Action<RottenTomatoesAsyncResult<RottenTomatoesMovieInfo>> callback)
		{
			ProcessAsyncRequest<RottenTomatoesMovieInfo>(Generator.GetMovieInfo(Request),callback);
		}

		/// <summary>
		/// Retrieve all the basic movie information for a particular movie by RottenTomatoes reference.
		/// http://api.rottentomatoes.com/api/public/v1.0/movies/<id>/clips.json?apikey=<apikey>
		/// </summary>
		/// <param name="id">RottenTomatoes movie id</param>
		/// <returns></returns>
		public void GetMovieInfo(RottenTomatoesRequest Request, Action<RottenTomatoesAsyncResult<RottenTomatoesMovieClips>> callback)
		{
			 ProcessAsyncRequest<RottenTomatoesMovieClips>(Generator.GetMovieInfo(Request),callback);
		}

		/// <summary>
		/// Retrieve all the basic movie information for a particular movie by RottenTomatoes reference.
		/// http://api.rottentomatoes.com/api/public/v1.0/movies/<id>/cast.json?apikey=<apikey>
		/// </summary>
		/// <param name="id">RottenTomatoes movie id</param>
		/// <returns></returns>
		public void GetMovieCast(RottenTomatoesRequest Request, Action<RottenTomatoesAsyncResult<RottenTomatoesCastInfo>> callback)
		{
			ProcessAsyncRequest<RottenTomatoesCastInfo>(Generator.GetMovieInfo(Request), callback);
		}

		/// <summary>
		/// Retrieve all the basic movie information for a particular movie by RottenTomatoes reference.
		/// http://api.rottentomatoes.com/api/public/v1.0/movies/<id>/cast.json?apikey=<apikey>
		/// </summary>
		/// <param name="id">IMDB movie id without the initial tt</param>
		/// <returns></returns>
		public void GetMoviesAlias(RottenTomatoesRequest Request, Action<RottenTomatoesAsyncResult<RottenTomatoesMovieInfo>> callback)
		{
			if (Request.MovieID.Substring(0, 2).ToLower() == "tt")
				Request.MovieID = Request.MovieID.Substring(2, Request.MovieID.Length - 2);

			ProcessAsyncRequest<RottenTomatoesMovieInfo>(Generator.GetMovieInfo(Request),callback );
		}

        #endregion
    }
}
