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

		private T ProcessRequest<T>(RestRequest request)
			where T : new()
		{
			var client = new RestClient(BASE_URL);
			client.AddHandler("application/json", new WatJsonDeserializer());

			if (Timeout.HasValue)
				client.Timeout = Timeout.Value;

#if !WINDOWS_PHONE
			if (Proxy != null)
				client.Proxy = Proxy;
#endif

			var resp = client.Execute<T>(request);

			ResponseContent = resp.Content;
			ResponseHeaders = resp.Headers.ToDictionary(k => k.Name, v => v.Value);

			switch (resp.ResponseStatus)
			{
				case ResponseStatus.Completed:
					switch (resp.StatusCode)
					{
						case HttpStatusCode.OK:
							if (resp.Content == "Please specify a valid API key")
								Error = resp.Content;
							return resp.Data;
							break;
						default:
							Error = resp.Content;
							return default(T);
							break;
					}
					break;
				default:
					Error = "HTTP Error";
					return default(T);
					break;
			}
			return default(T);
		}

		#endregion


		#region Movie Info
		/// <summary>
		/// Retrieve all the basic movie information for a particular movie by RottenTomatoes reference.
		/// http://api.rottentomatoes.com/api/public/v1.0/movies/<id>.json?apikey=<apikey>
		/// </summary>
		/// <param name="id">RottenTomatoes movie id</param>
		/// <returns></returns>
		public RottenTomatoesMovieInfo GetMovieInfo(RottenTomatoesRequest Request)
		{
			return ProcessRequest<RottenTomatoesMovieInfo>(Generator.GetMovieInfo(Request));
		}

		/// <summary>
		/// Retrieve all the basic movie information for a particular movie by RottenTomatoes reference.
		/// http://api.rottentomatoes.com/api/public/v1.0/movies/<id>/clips.json?apikey=<apikey>
		/// </summary>
		/// <param name="id">RottenTomatoes movie id</param>
		/// <returns></returns>
		public RottenTomatoesMovieClips GetMovieClips(RottenTomatoesRequest Request)
		{
			return ProcessRequest<RottenTomatoesMovieClips>(Generator.GetMovieInfo(Request));
		}

		/// <summary>
		/// Retrieve all the basic movie information for a particular movie by RottenTomatoes reference.
		/// http://api.rottentomatoes.com/api/public/v1.0/movies/<id>/cast.json?apikey=<apikey>
		/// </summary>
		/// <param name="id">RottenTomatoes movie id</param>
		/// <returns></returns>
		public RottenTomatoesCastInfo GetMovieCast(RottenTomatoesRequest Request)
		{
			return ProcessRequest<RottenTomatoesCastInfo>(Generator.GetMovieInfo(Request));
		}

		/// <summary>
		/// Retrieve all the basic movie information for a particular movie by RottenTomatoes reference.
		/// http://api.rottentomatoes.com/api/public/v1.0/movies/<id>/cast.json?apikey=<apikey>
		/// </summary>
		/// <param name="id">IMDB movie id without the initial tt</param>
		/// <returns></returns>
		public RottenTomatoesMovieInfo GetMoviesAlias(RottenTomatoesRequest Request)
		{
			if (Request.MovieID.Substring(0, 2).ToLower() == "tt")
				Request.MovieID = Request.MovieID.Substring(2, Request.MovieID.Length - 2);

			return ProcessRequest<RottenTomatoesMovieInfo>(Generator.GetMovieInfo(Request));
		}

	#endregion
	}
}
