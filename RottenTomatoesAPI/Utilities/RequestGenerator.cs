using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp;

namespace RottenTomatoes.Utilities
{
	internal partial class RequestGenerator
	{
		#region Properties

		private Method Method { get; set; }
		private string ApiKey { get; set; }

		#endregion

		public RequestGenerator(string apiKey,  Method method = Method.GET)
		{
			Method = method;
			ApiKey = apiKey;
		}

		private RequestBuilder GetBuilder(string request)
		{
			return new RequestBuilder(request, Method);
		}

		#region Movie Info

		internal RestRequest GetMovieInfo(RottenTomatoes.V1.RottenTomatoesRequest Request, object userState = null)
		{
			return GetBuilder(REQUEST_MOVIE)
				.SetUserState(userState)
				.AddUrlSegment(RequestBuilder.PARAMETER_ID, Request.MovieID)
				.AddParameter(RequestBuilder.PARAMETER_APIKEY, ApiKey )
				.GetRequest();
		}

		internal RestRequest GetMovieClips(RottenTomatoes.V1.RottenTomatoesRequest Request, object userState = null)
		{
			return GetBuilder(REQUEST_MOVIECLIPS)
				.SetUserState(userState)
				.AddUrlSegment(RequestBuilder.PARAMETER_ID, Request.MovieID)
				.AddParameter(RequestBuilder.PARAMETER_APIKEY, ApiKey)
				.GetRequest();
		}

		internal RestRequest GetMovieCast(RottenTomatoes.V1.RottenTomatoesRequest Request, object userState = null)
		{
			return GetBuilder(REQUEST_MOVIECAST)
				.SetUserState(userState)
				.AddUrlSegment(RequestBuilder.PARAMETER_ID, Request.MovieID)
				.AddParameter(RequestBuilder.PARAMETER_APIKEY, ApiKey)
				.GetRequest();
		}

		internal RestRequest GetMovieAlias(RottenTomatoes.V1.RottenTomatoesRequest Request, object userState = null)
		{
			return GetBuilder(REQUEST_MOVIECAST)
				.SetUserState(userState)
				.AddParameter(RequestBuilder.PARAMETER_ID, Request.MovieID)
				.AddParameter(RequestBuilder.PARAMETER_APIKEY, ApiKey)
				.AddParameter(RequestBuilder.PARAMETER_TYPE , "imdb")
				.GetRequest();
		}

		#endregion
	}
}
