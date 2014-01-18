using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp;

namespace WatTmdb.Utilities
{
    internal partial class RequestGenerator
    {
        #region Properties

        private Method Method { get; set; }
        private string DefaultLanguage { get; set; }
        private string ApiKey { get; set; }

        #endregion

        public RequestGenerator(string apiKey, string language, Method method = Method.GET)
        {
            DefaultLanguage = language;
            Method = method;
            ApiKey = apiKey;
        }

        private RequestBuilder GetBuilder(string request)
        {
            return new RequestBuilder(request, Method, DefaultLanguage)
                        .AddParameter(RequestBuilder.PARAMETER_APIKEY, ApiKey);
        }

        #region Authentication Methods

        internal RestRequest GetAuthToken(object userState = null)
        {
            return GetBuilder(REQUEST_AUTH_TOKEN)
                .SetUserState(userState)
                .GetRequest();
        }

        internal RestRequest GetAuthSession(string token, object userState = null)
        {
            return GetBuilder(REQUEST_AUTH_SESSION)
                .SetUserState(userState)
                .AddParameter(RequestBuilder.PARAMETER_REQUESTOKEN, token)
                .GetRequest();
        }

        internal RestRequest GetGuestSession(object userState = null)
        {
            return GetBuilder(REQUEST_AUTH_GUESTSESSION)
                .SetUserState(userState)
                .GetRequest();
        }

        #endregion


        #region Account Methods

        internal RestRequest GetAccountInfo(string SessionID, object userState = null)
        {
            return GetBuilder(REQUEST_ACCOUNT)
                .SetUserState(userState)
                .AddParameter(RequestBuilder.PARAMETER_SESSIONID, SessionID)
                .GetRequest();
        }

        internal RestRequest GetAccountLists(int AccountID, string SessionID, int? page, string language, object userState = null)
        {
            return GetBuilder(REQUEST_ACCOUNT_LISTS)
                .SetUserState(userState)
                .AddUrlSegment(RequestBuilder.PARAMETER_ID, AccountID)
                .AddParameter(RequestBuilder.PARAMETER_SESSIONID, SessionID)
                .AddParameter(RequestBuilder.PARAMETER_PAGE, page)
                .AddParameter(RequestBuilder.PARAMETER_LANGUAGE, language)
                .GetRequest();
        }

        internal RestRequest GetAccountFavouriteMovies(int AccountID, string SessionID, int? page, string language, object userState = null)
        {
            return GetBuilder(REQUEST_ACCOUNT_FAVOURITE)
                .SetUserState(userState)
                .AddUrlSegment(RequestBuilder.PARAMETER_ID, AccountID)
                .AddParameter(RequestBuilder.PARAMETER_SESSIONID, SessionID)
                .AddParameter(RequestBuilder.PARAMETER_PAGE, page)
                .AddParameter(RequestBuilder.PARAMETER_LANGUAGE, language)
                .GetRequest();
        }

        internal RestRequest GetAccountRatedMovies(int AccountID, string SessionID, int? page, string language, object userState = null)
        {
            return GetBuilder(REQUEST_ACCOUNT_RATED)
                .SetUserState(userState)
                .AddUrlSegment(RequestBuilder.PARAMETER_ID, AccountID)
                .AddParameter(RequestBuilder.PARAMETER_SESSIONID, SessionID)
                .AddParameter(RequestBuilder.PARAMETER_PAGE, page)
                .AddParameter(RequestBuilder.PARAMETER_LANGUAGE, language)
                .GetRequest();
        }

        internal RestRequest GetAccountWatchlistMovies(int AccountID, string SessionID, int? page, string language, object userState = null)
        {
            return GetBuilder(REQUEST_ACCOUNT_WATCHLIST)
                .SetUserState(userState)
                .AddUrlSegment(RequestBuilder.PARAMETER_ID, AccountID)
                .AddParameter(RequestBuilder.PARAMETER_SESSIONID, SessionID)
                .AddParameter(RequestBuilder.PARAMETER_PAGE, page)
                .AddParameter(RequestBuilder.PARAMETER_LANGUAGE, language)
                .GetRequest();
        }

        #endregion


        #region Configuration Methods

        internal RestRequest GetConfiguration(object userState = null)
        {
            return GetBuilder(REQUEST_CONFIG)
                .SetUserState(userState)
                .GetRequest();
        }

        #endregion


        #region Search Methods

        internal RestRequest SearchMovie(string query, int page, string language, object userState = null, bool? includeAdult = null, int? year = null)
        {
            return GetBuilder(REQUEST_SEARCH_MOVIE)
                .SetUserState(userState)
                .AddParameter(RequestBuilder.PARAMETER_QUERY, query)
                .AddParameter(RequestBuilder.PARAMETER_PAGE, page)
                .AddParameter(RequestBuilder.PARAMETER_LANGUAGE, language)
                .AddParameter(RequestBuilder.PARAMETER_ADULT, includeAdult)
                .AddParameter(RequestBuilder.PARAMETER_YEAR, year)
                .GetRequest();
        }

        internal RestRequest SearchPerson(string query, int page, string language, object userState = null)
        {
            return GetBuilder(REQUEST_SEARCH_PERSON)
                .SetUserState(userState)
                .AddParameter(RequestBuilder.PARAMETER_QUERY, query)
                .AddParameter(RequestBuilder.PARAMETER_PAGE, page)
                .AddParameter(RequestBuilder.PARAMETER_LANGUAGE, language)
                .GetRequest();
        }

        internal RestRequest SearchCompany(string query, int page, object userState = null)
        {
            return GetBuilder(REQUEST_SEARCH_COMPANY)
                .SetUserState(userState)
                .AddParameter(RequestBuilder.PARAMETER_QUERY, query)
                .AddParameter(RequestBuilder.PARAMETER_PAGE, page)
                .GetRequest();
        }

        internal RestRequest SearchKeyword(string query, int page, object userState = null)
        {
            return GetBuilder(REQUEST_SEARCH_KEYWORD)
                .SetUserState(userState)
                .AddParameter(RequestBuilder.PARAMETER_QUERY, query)
                .AddParameter(RequestBuilder.PARAMETER_PAGE, page)
                .GetRequest();
        }

        internal RestRequest SearchCollection(string query, int page, string language, object userState = null)
        {
            return GetBuilder(REQUEST_SEARCH_COLLECTION)
                .SetUserState(userState)
                .AddParameter(RequestBuilder.PARAMETER_QUERY, query)
                .AddParameter(RequestBuilder.PARAMETER_PAGE, page)
                .AddParameter(RequestBuilder.PARAMETER_LANGUAGE, language)
                .GetRequest();
        }

        #endregion


        #region Collections

        internal RestRequest GetCollectionInfo(int CollectionID, string language, object userState = null)
        {
            return GetBuilder(REQUEST_COLLECTION)
                .SetUserState(userState)
                .AddParameter(RequestBuilder.PARAMETER_LANGUAGE, language)
                .AddUrlSegment(RequestBuilder.PARAMETER_ID, CollectionID)
                .GetRequest();
        }

        internal RestRequest GetCollectionImages(int CollectionID, string language, object userState = null)
        {
            return GetBuilder(REQUEST_COLLECTION_IMAGES)
                .SetUserState(userState)
                .AddParameter(RequestBuilder.PARAMETER_LANGUAGE, language)
                .AddUrlSegment(RequestBuilder.PARAMETER_ID, CollectionID)
                .GetRequest();
        }

        #endregion


        #region Movie Info

        internal RestRequest GetMovieInfo(int MovieID, string language, object userState = null)
        {
            return GetBuilder(REQUEST_MOVIE)
                .SetUserState(userState)
                .AddParameter(RequestBuilder.PARAMETER_LANGUAGE, language)
                .AddUrlSegment(RequestBuilder.PARAMETER_ID, MovieID)
                .GetRequest();
        }

        internal RestRequest GetMovieByIMDB(string imdbId, string language, object userState = null)
        {
            return GetBuilder(REQUEST_MOVIE)
                .SetUserState(userState)
                .AddParameter(RequestBuilder.PARAMETER_LANGUAGE, language)
                .AddUrlSegment(RequestBuilder.PARAMETER_ID, imdbId)
                .GetRequest();
        }

        internal RestRequest GetMovieAlternateTitles(int MovieID, string Country, object userState = null)
        {
            return GetBuilder(REQUEST_MOVIE_TITLES)
                .SetUserState(userState)
                .AddParameter(RequestBuilder.PARAMETER_COUNTRY, Country)
                .AddUrlSegment(RequestBuilder.PARAMETER_ID, MovieID)
                .GetRequest();
        }

        internal RestRequest GetMovieCast(int MovieID, object userState = null)
        {
            return GetBuilder(REQUEST_MOVIE_CAST)
                .SetUserState(userState)
                .AddUrlSegment(RequestBuilder.PARAMETER_ID, MovieID)
                .GetRequest();
        }

        internal RestRequest GetMovieImages(int MovieID, string language, object userState = null)
        {
            return GetBuilder(REQUEST_MOVIE_IMAGES)
                .SetUserState(userState)
                .AddParameter(RequestBuilder.PARAMETER_LANGUAGE, language)
                .AddUrlSegment(RequestBuilder.PARAMETER_ID, MovieID)
                .GetRequest();
        }

        internal RestRequest GetMovieKeywords(int MovieID, object userState = null)
        {
            return GetBuilder(REQUEST_MOVIE_KEYWORDS)
                .SetUserState(userState)
                .AddUrlSegment(RequestBuilder.PARAMETER_ID, MovieID)
                .GetRequest();
        }

        internal RestRequest GetMovieReleases(int MovieID, object userState = null)
        {
            return GetBuilder(REQUEST_MOVIE_RELEASES)
                .SetUserState(userState)
                .AddUrlSegment(RequestBuilder.PARAMETER_ID, MovieID)
                .GetRequest();
        }

        internal RestRequest GetMovieTrailers(int MovieID, string language, object userState = null)
        {
            return GetBuilder(REQUEST_MOVIE_TRAILERS)
                .SetUserState(userState)
                .AddParameter(RequestBuilder.PARAMETER_LANGUAGE, language)
                .AddUrlSegment(RequestBuilder.PARAMETER_ID, MovieID)
                .GetRequest();
        }

        internal RestRequest GetSimilarMovies(int MovieID, int page, string language, object userState = null)
        {
            return GetBuilder(REQUEST_MOVIE_SIMILAR)
                .SetUserState(userState)
                .AddParameter(RequestBuilder.PARAMETER_PAGE, page)
                .AddParameter(RequestBuilder.PARAMETER_LANGUAGE, language)
                .AddUrlSegment(RequestBuilder.PARAMETER_ID, MovieID)
                .GetRequest();
        }

        internal RestRequest GetMovieTranslations(int MovieID, object userState = null)
        {
            return GetBuilder(REQUEST_MOVIE_TRANSLATIONS)
                .SetUserState(userState)
                .AddUrlSegment(RequestBuilder.PARAMETER_ID, MovieID)
                .GetRequest();
        }

        internal RestRequest GetMovieLists(int MovieID, int page, string language, object userState = null)
        {
            return GetBuilder(REQUEST_MOVIE_LISTS)
                .SetUserState(userState)
                .AddUrlSegment(RequestBuilder.PARAMETER_ID, MovieID)
                .AddParameter(RequestBuilder.PARAMETER_PAGE, page)
                .AddParameter(RequestBuilder.PARAMETER_LANGUAGE, language)
                .GetRequest();
        }

        internal RestRequest GetMovieChanges(int MovieID, object userState = null)
        {
            return GetBuilder(REQUEST_MOVIE_CHANGES)
                .SetUserState(userState)
                .AddUrlSegment(RequestBuilder.PARAMETER_ID, MovieID)
                .GetRequest();
        }

        #endregion


        #region Person Info

        internal RestRequest GetPersonInfo(int PersonID, object userState = null)
        {
            return GetBuilder(REQUEST_PERSON)
                .SetUserState(userState)
                .AddUrlSegment(RequestBuilder.PARAMETER_ID, PersonID)
                .GetRequest();
        }

        internal RestRequest GetPersonCredits(int PersonID, string language, object userState = null)
        {
            return GetBuilder(REQUEST_PERSON_CREDITS)
                .SetUserState(userState)
                .AddParameter(RequestBuilder.PARAMETER_LANGUAGE, language)
                .AddUrlSegment(RequestBuilder.PARAMETER_ID, PersonID)
                .GetRequest();
        }

        internal RestRequest GetPersonImages(int PersonID, object userState = null)
        {
            return GetBuilder(REQUEST_PERSON_IMAGES)
                .SetUserState(userState)
                .AddUrlSegment(RequestBuilder.PARAMETER_ID, PersonID)
                .GetRequest();
        }

        internal RestRequest GetPersonChanges(int PersonID, object userState = null)
        {
            return GetBuilder(REQUEST_PERSON_CHANGES)
                .SetUserState(userState)
                .AddUrlSegment(RequestBuilder.PARAMETER_ID, PersonID)
                .GetRequest();
        }

        #endregion


        #region Miscellaneous Movie

        internal RestRequest GetLatestMovies(object userState = null)
        {
            return GetBuilder(REQUEST_MOVIE_LATEST)
                .SetUserState(userState)
                .GetRequest();
        }

        internal RestRequest GetNowPlayingMovies(int page, string language, object userState = null)
        {
            return GetBuilder(REQUEST_MOVIE_PLAYING)
                .SetUserState(userState)
                .AddParameter(RequestBuilder.PARAMETER_LANGUAGE, language)
                .AddParameter(RequestBuilder.PARAMETER_PAGE, page)
                .GetRequest();
        }

        internal RestRequest GetPopularMovies(int page, string language, object userState = null)
        {
            return GetBuilder(REQUEST_MOVIE_POPULAR)
                .SetUserState(userState)
                .AddParameter(RequestBuilder.PARAMETER_LANGUAGE, language)
                .AddParameter(RequestBuilder.PARAMETER_PAGE, page)
                .GetRequest();
        }

        internal RestRequest GetTopRatedMovies (int page, string language, object userState = null)
        {
            return GetBuilder (REQUEST_MOVIE_TOP)
                .SetUserState (userState)
                .AddParameter(RequestBuilder.PARAMETER_LANGUAGE, language)
                .AddParameter(RequestBuilder.PARAMETER_PAGE, page)
                .GetRequest();
        }

        internal RestRequest GetUpcomingMovies(int page, string language, object userState = null)
        {
            return GetBuilder(REQUEST_MOVIE_UPCOMING)
                .SetUserState(userState)
                .AddParameter(RequestBuilder.PARAMETER_LANGUAGE, language)
                .AddParameter(RequestBuilder.PARAMETER_PAGE, page)
                .GetRequest();
        }

        #endregion


        #region Lists

        internal RestRequest GetList(string ListID, object userState = null)
        {
            return GetBuilder(REQUEST_LIST)
                .SetUserState(userState)
                .AddUrlSegment(RequestBuilder.PARAMETER_ID, ListID)
                .GetRequest();
        }

        #endregion


        #region Company Info

        internal RestRequest GetCompanyInfo(int CompanyID, object userState = null)
        {
            return GetBuilder(REQUEST_COMPANY)
                .SetUserState(userState)
                .AddUrlSegment(RequestBuilder.PARAMETER_ID, CompanyID)
                .GetRequest();
        }

        internal RestRequest GetCompanyMovies(int CompanyID, int page, string language, object userState = null)
        {
            return GetBuilder(REQUEST_COMPANY_MOVIES)
                .SetUserState(userState)
                .AddParameter(RequestBuilder.PARAMETER_LANGUAGE, language)
                .AddParameter(RequestBuilder.PARAMETER_PAGE, page)
                .AddUrlSegment(RequestBuilder.PARAMETER_ID, CompanyID)
                .GetRequest();
        }

        #endregion


        #region Genre Info

        internal RestRequest GetGenreList(string language, object userState = null)
        {
            return GetBuilder(REQUEST_GENRE)
                .SetUserState(userState)
                .AddParameter(RequestBuilder.PARAMETER_LANGUAGE, language)
                .GetRequest();
        }

        internal RestRequest GetGenreMovies(int GenreID, int page, string language, object userState = null)
        {
            return GetBuilder(REQUEST_GENRE_MOVIES)
                .SetUserState(userState)
                .AddParameter(RequestBuilder.PARAMETER_LANGUAGE, language)
                .AddParameter(RequestBuilder.PARAMETER_PAGE, page)
                .AddUrlSegment(RequestBuilder.PARAMETER_ID, GenreID)
                .GetRequest();
        }

        #endregion


        #region Keyword Methods

        internal RestRequest GetKeyword(int KeywordID, object userState = null)
        {
            return GetBuilder(REQUEST_KEYWORD)
                .SetUserState(userState)
                .AddUrlSegment(RequestBuilder.PARAMETER_ID, KeywordID)
                .GetRequest();
        }

        internal RestRequest GetKeywordMovies(int KeywordID, int page, string language, object userState = null)
        {
            return GetBuilder(REQUEST_KEYWORD_MOVIES)
                .SetUserState(userState)
                .AddUrlSegment(RequestBuilder.PARAMETER_ID, KeywordID)
                .AddParameter(RequestBuilder.PARAMETER_PAGE, page)
                .AddParameter(RequestBuilder.PARAMETER_LANGUAGE, language)
                .GetRequest();
        }

        #endregion


        #region Changes Methods

        internal RestRequest GetChangesByMovie(int page, DateTime? StartDate, DateTime? EndDate, object userState = null)
        {
            return GetBuilder(REQUEST_CHANGES_MOVIES)
                .SetUserState(userState)
                .AddParameter(RequestBuilder.PARAMETER_PAGE, page)
                .AddParameter(RequestBuilder.PARAMETER_STARTDATE, StartDate)
                .AddParameter(RequestBuilder.PARAMETER_ENDDATE, EndDate)
                .GetRequest();
        }

        internal RestRequest GetChangesByPerson(int page, DateTime? StartDate, DateTime? EndDate, object userState = null)
        {
            return GetBuilder(REQUEST_CHANGES_PERSON)
                .SetUserState(userState)
                .AddParameter(RequestBuilder.PARAMETER_PAGE, page)
                .AddParameter(RequestBuilder.PARAMETER_STARTDATE, StartDate)
                .AddParameter(RequestBuilder.PARAMETER_ENDDATE, EndDate)
                .GetRequest();
        }

        #endregion
    }
}
