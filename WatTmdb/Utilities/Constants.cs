using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WatTmdb.Utilities
{
    internal partial class RequestGenerator
    {

        #region Request Constants
        public const string REQUEST_AUTH_TOKEN = "authentication/token/new";
        public const string REQUEST_AUTH_SESSION = "authentication/session/new";
        public const string REQUEST_AUTH_GUESTSESSION = "authentication/guest_session/new";

        public const string REQUEST_ACCOUNT = "account";
        public const string REQUEST_ACCOUNT_LISTS = "account/{id}/lists";
        public const string REQUEST_ACCOUNT_FAVOURITE = "account/{id}/favorite_movies";
        public const string REQUEST_ACCOUNT_RATED = "account/{id}/rated_movies";
        public const string REQUEST_ACCOUNT_WATCHLIST = "account/{id}/movie_watchlist";

        public const string REQUEST_CONFIG = "configuration";

        public const string REQUEST_SEARCH_MOVIE = "search/movie";
        public const string REQUEST_SEARCH_PERSON = "search/person";
        public const string REQUEST_SEARCH_COMPANY = "search/company";
        public const string REQUEST_SEARCH_KEYWORD = "search/keyword";
        public const string REQUEST_SEARCH_COLLECTION = "search/collection";

        public const string REQUEST_COLLECTION = "collection/{id}";
        public const string REQUEST_COLLECTION_IMAGES = "collection/{id}/images";

        public const string REQUEST_MOVIE = "movie/{id}";
        public const string REQUEST_MOVIE_TITLES = "movie/{id}/alternative_titles";
        public const string REQUEST_MOVIE_CAST = "movie/{id}/casts";
        public const string REQUEST_MOVIE_IMAGES = "movie/{id}/images";
        public const string REQUEST_MOVIE_KEYWORDS = "movie/{id}/keywords";
        public const string REQUEST_MOVIE_RELEASES = "movie/{id}/releases";
        public const string REQUEST_MOVIE_TRAILERS = "movie/{id}/trailers";
        public const string REQUEST_MOVIE_SIMILAR = "movie/{id}/similar_movies";
        public const string REQUEST_MOVIE_TRANSLATIONS = "movie/{id}/translations";
        public const string REQUEST_MOVIE_LISTS = "movie/{id}/lists";
        public const string REQUEST_MOVIE_CHANGES = "movie/{id}/changes";

        public const string REQUEST_MOVIE_LATEST = "movie/latest";
        public const string REQUEST_MOVIE_PLAYING = "movie/now_playing";
        public const string REQUEST_MOVIE_POPULAR = "movie/popular";
        public const string REQUEST_MOVIE_TOP = "movie/top_rated";
        public const string REQUEST_MOVIE_UPCOMING = "movie/upcoming";

        public const string REQUEST_PERSON = "person/{id}";
        public const string REQUEST_PERSON_CREDITS = "person/{id}/credits";
        public const string REQUEST_PERSON_IMAGES = "person/{id}/images";
        public const string REQUEST_PERSON_CHANGES = "person/{id}/changes";

        public const string REQUEST_LIST = "list/{id}";

        public const string REQUEST_COMPANY = "company/{id}";
        public const string REQUEST_COMPANY_MOVIES = "company/{id}/movies";

        public const string REQUEST_GENRE = "genre/list";
        public const string REQUEST_GENRE_MOVIES = "genre/{id}/movies";

        public const string REQUEST_KEYWORD = "keyword/{id}";
        public const string REQUEST_KEYWORD_MOVIES = "keyword/{id}/movies";

        public const string REQUEST_CHANGES_MOVIES = "movie/changes";
        public const string REQUEST_CHANGES_PERSON = "person/changes";

        #endregion
    
    }


    internal partial class RequestBuilder
    {

        #region Parameter Constants
        public const string PARAMETER_APIKEY = "api_key";
        public const string PARAMETER_QUERY = "query";
        public const string PARAMETER_PAGE = "page";
        public const string PARAMETER_LANGUAGE = "language";
        public const string PARAMETER_ADULT = "include_adult";
        public const string PARAMETER_YEAR = "year";
        public const string PARAMETER_ID = "id";
        public const string PARAMETER_COUNTRY = "country";
        public const string PARAMETER_STARTDATE = "start_date";
        public const string PARAMETER_ENDDATE = "end_date";
        public const string PARAMETER_REQUESTOKEN = "request_token";
        public const string PARAMETER_SESSIONID = "session_id";
        #endregion

    }
}
