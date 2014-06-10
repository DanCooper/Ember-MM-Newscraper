using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp;

namespace FanartTV.Utilities
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
			return new RequestBuilder(request, Method)
						.AddUrlSegment(RequestBuilder.PARAMETER_APIKEY, ApiKey);
		}

		#region Movie Info

		internal RestRequest GetMovieInfo(FanartTV.V1.FanartTVMovieRequest Request, object userState = null)
		{
			return GetBuilder(REQUEST_MOVIE)
				.SetUserState(userState)
				.AddUrlSegment(RequestBuilder.PARAMETER_ID, Request.MovieID)
				.AddUrlSegment(RequestBuilder.PARAMETER_FORMAT, Request.format)
				.AddUrlSegment(RequestBuilder.PARAMETER_LIMIT, Request.limit)
				.AddUrlSegment(RequestBuilder.PARAMETER_SORT, Request.sort)
				.AddUrlSegment(RequestBuilder.PARAMETER_TYPE, Request.type)
				.GetRequest();
		}

        internal RestRequest GetSeriesInfo(FanartTV.V1.FanartTVSeriesRequest Request, object userState = null)
        {
            return GetBuilder(REQUEST_SERIES)
                .SetUserState(userState)
                .AddUrlSegment(RequestBuilder.PARAMETER_ID, Request.SeriesID )
                .AddUrlSegment(RequestBuilder.PARAMETER_FORMAT, Request.format)
                .AddUrlSegment(RequestBuilder.PARAMETER_LIMIT, Request.limit)
                .AddUrlSegment(RequestBuilder.PARAMETER_SORT, Request.sort)
                .AddUrlSegment(RequestBuilder.PARAMETER_TYPE, Request.type)
                .GetRequest();
        }

		#endregion
	}
}
