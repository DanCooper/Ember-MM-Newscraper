using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp;
using System.Net;

namespace WatTmdb.V3
{
    public partial class Tmdb
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

            Error = null;

            //request.AddHeader("Accept", "application/json");
            //request.AddParameter("api_key", ApiKey);

            var resp = client.Execute<T>(request);

            ResponseContent = resp.Content;
            ResponseHeaders = resp.Headers.ToDictionary(k => k.Name, v => v.Value);

            if (resp.ResponseStatus == ResponseStatus.Completed)
            {
                if (resp.Content.Contains("status_message"))
                    Error = Deserializer.Deserialize<TmdbError>(resp);

                return resp.Data;
            }
            else
            {
                if (resp.Content.Contains("status_message"))
                    Error = Deserializer.Deserialize<TmdbError>(resp);
                else if (resp.ErrorException != null)
                    throw resp.ErrorException;
                else
                    Error = new TmdbError { status_message = resp.ErrorMessage };
            }

            return default(T);
        }

        private string ProcessRequestETag(RestRequest request)
        {
            var client = new RestClient(BASE_URL);
            if (Timeout.HasValue)
                client.Timeout = Timeout.Value;

#if !WINDOWS_PHONE
            if (Proxy != null)
                client.Proxy = Proxy;
#endif
            Error = null;

            request.Method = Method.HEAD;
            //request.AddHeader("Accept", "application/json");
            //request.AddParameter("api_key", ApiKey);

            var resp = client.Execute(request);
            ResponseContent = resp.Content;
            ResponseHeaders = resp.Headers.ToDictionary(k => k.Name, v => v.Value);

            if (resp.ResponseStatus != ResponseStatus.Completed && resp.ErrorException != null)
                throw resp.ErrorException;

            return this.ResponseETag;
        }

        #endregion


        #region Configuration
        /// <summary>
        /// Retrieve configuration data from TMDB
        /// (http://help.themoviedb.org/kb/api/configuration)
        /// </summary>
        /// <returns></returns>
        public TmdbConfiguration GetConfiguration()
        {
            //return ProcessRequest<TmdbConfiguration>(BuildGetConfigurationRequest());
            return ProcessRequest<TmdbConfiguration>(Generator.GetConfiguration());
        }

        public string GetConfigurationETag()
        {
            //return ProcessRequestETag(BuildGetConfigurationRequest());
            return ProcessRequestETag(Generator.GetConfiguration());
        }

        #endregion


        #region Authentication

        /// <summary>
        /// Generate a valid request token for user based authentication.
        /// http://docs.themoviedb.apiary.io/#get-%2F3%2Fauthentication%2Ftoken%2Fnew
        /// </summary>
        /// <returns></returns>
        public TmdbAuthToken GetAuthToken()
        {
            return ProcessRequest<TmdbAuthToken>(Generator.GetAuthToken());
        }

        /// <summary>
        /// Generate a session id for user based authentication.
        /// http://docs.themoviedb.apiary.io/#get-%2F3%2Fauthentication%2Fsession%2Fnew
        /// </summary>
        /// <param name="RequestToken"></param>
        /// <returns></returns>
        public TmdbAuthSession GetAuthSession(string RequestToken)
        {
            return ProcessRequest<TmdbAuthSession>(Generator.GetAuthSession(RequestToken));
        }

        /// <summary>
        /// Generate a Guest Session id
        /// http://docs.themoviedb.apiary.io/#get-%2F3%2Fauthentication%2Fguest_session%2Fnew
        /// </summary>
        /// <returns></returns>
        public TmdbGuestSession GetGuestSession()
        {
            return ProcessRequest<TmdbGuestSession>(Generator.GetGuestSession());
        }

        #endregion


        #region Account Methods

        /// <summary>
        /// Get the basic information for an account.
        /// http://docs.themoviedb.apiary.io/#get-%2F3%2Faccount
        /// </summary>
        /// <param name="SessionID"></param>
        /// <returns></returns>
        public TmdbAccount GetAccountInfo(string SessionID)
        {
            return ProcessRequest<TmdbAccount>(Generator.GetAccountInfo(SessionID));
        }

        /// <summary>
        /// Get the lists that you have created and marked as a favorite.
        /// http://docs.themoviedb.apiary.io/#get-%2F3%2Faccount%2F%7Bid%7D%2Flists
        /// </summary>
        /// <param name="AccountID"></param>
        /// <param name="SessionID"></param>
        /// <param name="page"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        public TmdbList GetAccountLists(int AccountID, string SessionID, int page, string language = "")
        {
            return ProcessRequest<TmdbList>(Generator.GetAccountLists(AccountID, SessionID, page, language));
        }

        /// <summary>
        /// Get the list of favourite movies for an account
        /// http://docs.themoviedb.apiary.io/#get-%2F3%2Faccount%2F%7Bid%7D%2Ffavorite_movies
        /// </summary>
        /// <param name="AccountID"></param>
        /// <param name="SessionID"></param>
        /// <param name="page"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        public TmdbAccountMovies GetAccountFavouriteMovies(int AccountID, string SessionID, int page, string language = "")
        {
            return ProcessRequest<TmdbAccountMovies>(Generator.GetAccountFavouriteMovies(AccountID, SessionID, page, language));
        }

        /// <summary>
        /// Get the list of rated movies for an account
        /// http://docs.themoviedb.apiary.io/#get-%2F3%2Faccount%2F%7Bid%7D%2Frated_movies
        /// </summary>
        /// <param name="AccountID"></param>
        /// <param name="SessionID"></param>
        /// <param name="page"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        public TmdbAccountMovies GetAccountRatedMovies(int AccountID, string SessionID, int page, string language = "")
        {
            return ProcessRequest<TmdbAccountMovies>(Generator.GetAccountRatedMovies(AccountID, SessionID, page, language));
        }

        /// <summary>
        /// Get the list of movies on an accounts watchlist
        /// http://docs.themoviedb.apiary.io/#get-%2F3%2Faccount%2F%7Bid%7D%2Fmovie_watchlist
        /// </summary>
        /// <param name="AccountID"></param>
        /// <param name="SessionID"></param>
        /// <param name="page"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        public TmdbAccountMovies GetAccountWatchlistMovies(int AccountID, string SessionID, int page, string language = "")
        {
            return ProcessRequest<TmdbAccountMovies>(Generator.GetAccountWatchlistMovies(AccountID, SessionID, page, language));
        }

        #endregion


        #region Search
        /// <summary>
        /// Search for movies that are listed in TMDB
        /// (http://help.themoviedb.org/kb/api/search-movies)
        /// </summary>
        /// <param name="query">Is your search text.</param>
        /// <param name="page">Result page to retrieve (1 based)</param>
        /// <param name="language">optional - ISO 639-1 language code</param>
        /// <param name="includeAdult">optional - include adult items in your search, (Default=false)</param>
        /// <param name="year">optional - to get a closer result</param>
        /// <returns></returns>
        public TmdbMovieSearch SearchMovie(string query, int page, string language = "", bool? includeAdult = null, int? year = null)
        {
            if (string.IsNullOrEmpty(query))
                throw new ArgumentException("Search must be supplied");
            if (year == 0)
                year = null;
            //return ProcessRequest<TmdbMovieSearch>(BuildSearchMovieRequest(query, page, language, includeAdult, year));
            return ProcessRequest<TmdbMovieSearch>(Generator.SearchMovie(query, page, language, null, includeAdult, year));
        }

        /// <summary>
        /// Search for people that are listed in TMDB.
        /// (http://help.themoviedb.org/kb/api/search-people)
        /// </summary>
        /// <param name="query">Is your search text.</param>
        /// <param name="page">Result page to retrieve (1 based)</param>
        /// <param name="language">optional - ISO 639-1 language code</param>
        /// <returns></returns>
        public TmdbPersonSearch SearchPerson(string query, int page, string language = "")
        {
            if (string.IsNullOrEmpty(query))
                throw new ArgumentException("Search must be supplied");

            //return ProcessRequest<TmdbPersonSearch>(BuildSearchPersonRequest(query, page, language));
            return ProcessRequest<TmdbPersonSearch>(Generator.SearchPerson(query, page, language));
        }

        /// <summary>
        /// Search for production companies that are part of TMDB.
        /// (http://help.themoviedb.org/kb/api/search-companies)
        /// </summary>
        /// <param name="query">Is your search text.</param>
        /// <param name="page">Result page to retrieve (1 based)</param>
        /// <returns></returns>
        public TmdbCompanySearch SearchCompany(string query, int page)
        {
            if (string.IsNullOrEmpty(query))
                throw new ArgumentException("Search must be supplied");

            //return ProcessRequest<TmdbCompanySearch>(BuildSearchCompanyRequest(query, page));
            return ProcessRequest<TmdbCompanySearch>(Generator.SearchCompany(query, page));
        }

        /// <summary>
        /// Search for keywords by name.
        /// http://docs.themoviedb.apiary.io/#get-%2F3%2Fsearch%2Fkeyword
        /// </summary>
        /// <param name="query"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public TmdbKeywordSearch SearchKeyword(string query, int page)
        {
            if (string.IsNullOrEmpty(query))
                throw new ArgumentException("Search must be supplied");

            return ProcessRequest<TmdbKeywordSearch>(Generator.SearchKeyword(query, page));
        }

        /// <summary>
        /// Search for collections by name
        /// http://docs.themoviedb.apiary.io/#get-%2F3%2Fsearch%2Fcollection
        /// </summary>
        /// <param name="query"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public TmdbCollectionSearch SearchCollection(string query, int page, string language = "")
        {
            if (string.IsNullOrEmpty(query))
                throw new ArgumentException("Search must be supplied");

            return ProcessRequest<TmdbCollectionSearch>(Generator.SearchCollection(query, page, language));
        }

        #endregion


        #region Collections
        /// <summary>
        /// Get all of the basic information about a movie collection.
        /// (http://help.themoviedb.org/kb/api/collection-info)
        /// </summary>
        /// <param name="CollectionID">Collection ID, available in TmdbMovie::belongs_to_collection</param>
        /// <param name="language">optional - ISO 639-1 language code</param>
        /// <returns></returns>
        public TmdbCollection GetCollectionInfo(int CollectionID, string language = "")
        {
            //return ProcessRequest<TmdbCollection>(BuildGetCollectionInfoRequest(CollectionID, language));
            return ProcessRequest<TmdbCollection>(Generator.GetCollectionInfo(CollectionID, language));
        }

        public string GetCollectionInfoETag(int CollectionID, string language = "")
        {
            //return ProcessRequestETag(BuildGetCollectionInfoRequest(CollectionID, language));
            return ProcessRequestETag(ETagGenerator.GetCollectionInfo(CollectionID, language));
        }

        /// <summary>
        /// Get all the images for a movie collection
        /// http://help.themoviedb.org/kb/api/collection-images
        /// </summary>
        /// <param name="CollectionID">Collection ID, available in TmdbMovie::belongs_to_collection</param>
        /// <param name="language">optional - ISO 639-1 language code</param>
        /// <returns></returns>
        public TmdbCollectionImages GetCollectionImages(int CollectionID, string language = "")
        {
            //return ProcessRequest<TmdbCollectionImages>(BuildGetCollectionImagesRequest(CollectionID, language));
            return ProcessRequest<TmdbCollectionImages>(Generator.GetCollectionImages(CollectionID, language));
        }

        public string GetCollectionImagesETag(int CollectionID, string language = "")
        {
            //return ProcessRequestETag(BuildGetCollectionImagesRequest(CollectionID, language));
            return ProcessRequestETag(ETagGenerator.GetCollectionImages(CollectionID, language));
        }

        #endregion


        #region Movie Info
        /// <summary>
        /// Retrieve all the basic movie information for a particular movie by TMDB reference.
        /// (http://help.themoviedb.org/kb/api/movie-info)
        /// </summary>
        /// <param name="MovieID">TMDB movie id</param>
        /// <param name="language">optional - ISO 639-1 language code</param>
        /// <returns></returns>
        public TmdbMovie GetMovieInfo(int MovieID, string language = "")
        {
            //return ProcessRequest<TmdbMovie>(BuildGetMovieInfoRequest(MovieID, language));
            return ProcessRequest<TmdbMovie>(Generator.GetMovieInfo(MovieID, language));
        }

        public string GetMovieInfoETag(int MovieID, string language = "")
        {
            //return ProcessRequestETag(BuildGetMovieInfoRequest(MovieID, language));
            return ProcessRequestETag(ETagGenerator.GetMovieInfo(MovieID, language));
        }

        /// <summary>
        /// Retrieve all the basic movie information for a particular movie by IMDB reference.
        /// (http://help.themoviedb.org/kb/api/movie-info)
        /// </summary>
        /// <param name="imdbId">IMDB movie id</param>
        /// <returns></returns>
        public TmdbMovie GetMovieByIMDB(string imdbId, string language = "")
        {
            if (string.IsNullOrEmpty(imdbId))
                throw new ArgumentException("IMDB_ID must be supplied");

            //return ProcessRequest<TmdbMovie>(BuildGetMovieByIMDBRequest(imdbId, language));
            return ProcessRequest<TmdbMovie>(Generator.GetMovieByIMDB(imdbId, language));
        }

        /// <summary>
        /// Get list of all the alternative titles for a particular movie.
        /// (http://help.themoviedb.org/kb/api/movie-alternative-titles)
        /// </summary>
        /// <param name="MovieID">TMDB movie id</param>
        /// <param name="Country">ISO 3166-1 country code (optional)</param>
        /// <returns></returns>
        public TmdbMovieAlternateTitles GetMovieAlternateTitles(int MovieID, string Country)
        {
            //return ProcessRequest<TmdbMovieAlternateTitles>(BuildGetMovieAlternateTitlesRequest(MovieID, Country));
            return ProcessRequest<TmdbMovieAlternateTitles>(Generator.GetMovieAlternateTitles(MovieID, Country));
        }

        public string GetMovieAlternateTitlesETag(int MovieID, string Country)
        {
            //return ProcessRequestETag(BuildGetMovieAlternateTitlesRequest(MovieID, Country));
            return ProcessRequestETag(ETagGenerator.GetMovieAlternateTitles(MovieID, Country));
        }

        /// <summary>
        /// Get list of all the cast information for a particular movie.
        /// (http://help.themoviedb.org/kb/api/movie-casts)
        /// </summary>
        /// <param name="MovieID">TMDB movie id</param>
        /// <returns></returns>
        public TmdbMovieCast GetMovieCast(int MovieID)
        {
            //return ProcessRequest<TmdbMovieCast>(BuildGetMovieCastRequest(MovieID));
            return ProcessRequest<TmdbMovieCast>(Generator.GetMovieCast(MovieID));
        }

        public string GetMovieCastETag(int MovieID)
        {
            //return ProcessRequestETag(BuildGetMovieCastRequest(MovieID));
            return ProcessRequestETag(ETagGenerator.GetMovieCast(MovieID));
        }

        /// <summary>
        /// Get list of all the images for a particular movie.
        /// (http://help.themoviedb.org/kb/api/movie-images)
        /// </summary>
        /// <param name="MovieID">TMDB movie id</param>
        /// <param name="language">optional - ISO 639-1 language code</param>
        /// <returns></returns>
        public TmdbMovieImages GetMovieImages(int MovieID, string language = "")
        {
            //return ProcessRequest<TmdbMovieImages>(BuildGetMovieImagesRequest(MovieID, language));
            return ProcessRequest<TmdbMovieImages>(Generator.GetMovieImages(MovieID, language));
        }

        public string GetMovieImagesETag(int MovieID, string language = "")
        {
            //return ProcessRequestETag(BuildGetMovieImagesRequest(MovieID, language));
            return ProcessRequestETag(ETagGenerator.GetMovieImages(MovieID, language));
        }

        /// <summary>
        /// Get list of all the keywords that have been added to a particular movie.  Only English keywords exist currently.
        /// (http://help.themoviedb.org/kb/api/movie-keywords)
        /// </summary>
        /// <param name="MovieID">TMDB movie id</param>
        /// <returns></returns>
        public TmdbMovieKeywords GetMovieKeywords(int MovieID)
        {
            //return ProcessRequest<TmdbMovieKeywords>(BuildGetMovieKeywordsRequest(MovieID));
            return ProcessRequest<TmdbMovieKeywords>(Generator.GetMovieKeywords(MovieID));
        }

        public string GetMovieKeywordsETag(int MovieID)
        {
            //return ProcessRequestETag(BuildGetMovieKeywordsRequest(MovieID));
            return ProcessRequestETag(ETagGenerator.GetMovieKeywords(MovieID));
        }

        /// <summary>
        /// Get all the release and certification data in TMDB for a particular movie
        /// (http://help.themoviedb.org/kb/api/movie-release-info)
        /// </summary>
        /// <param name="MovieID">TMDB movie id</param>
        /// <returns></returns>
        public TmdbMovieReleases GetMovieReleases(int MovieID)
        {
            //return ProcessRequest<TmdbMovieReleases>(BuildGetMovieReleasesRequest(MovieID));
            return ProcessRequest<TmdbMovieReleases>(Generator.GetMovieReleases(MovieID));
        }

        public string GetMovieReleasesETag(int MovieID)
        {
            //return ProcessRequestETag(BuildGetMovieReleasesRequest(MovieID));
            return ProcessRequestETag(ETagGenerator.GetMovieReleases(MovieID));
        }

        /// <summary>
        /// Get list of trailers for a particular movie.
        /// (http://help.themoviedb.org/kb/api/movie-trailers)
        /// </summary>
        /// <param name="MovieID">TMDB movie id</param>
        /// <param name="language">optional - ISO 639-1 language code</param>
        /// <returns></returns>
        public TmdbMovieTrailers GetMovieTrailers(int MovieID, string language = "")
        {
            //return ProcessRequest<TmdbMovieTrailers>(BuildGetMovieTrailersRequest(MovieID, language));
            return ProcessRequest<TmdbMovieTrailers>(Generator.GetMovieTrailers(MovieID, language));
        }

        public string GetMovieTrailersETag(int MovieID, string language = "")
        {
            //return ProcessRequestETag(BuildGetMovieTrailersRequest(MovieID, language));
            return ProcessRequestETag(ETagGenerator.GetMovieTrailers(MovieID, language));
        }

        /// <summary>
        /// Get list of similar movies for a particular movie.
        /// (http://help.themoviedb.org/kb/api/movie-similar-movies)
        /// </summary>
        /// <param name="MovieID">TMDB movie id</param>
        /// <param name="page">Result page to retrieve (1 based)</param>
        /// <param name="language">optional - ISO 639-1 language code</param>
        /// <returns></returns>
        public TmdbSimilarMovies GetSimilarMovies(int MovieID, int page, string language = null)
        {
            //return ProcessRequest<TmdbSimilarMovies>(BuildGetSimilarMoviesRequest(MovieID, page, language));
            return ProcessRequest<TmdbSimilarMovies>(Generator.GetSimilarMovies(MovieID, page, language));
        }

        /// <summary>
        /// Get list of all available translations for a specific movie.
        /// (http://help.themoviedb.org/kb/api/movie-translations)
        /// </summary>
        /// <param name="MovieID">TMDB movie id</param>
        /// <returns></returns>
        public TmdbTranslations GetMovieTranslations(int MovieID)
        {
            //return ProcessRequest<TmdbTranslations>(BuildGetMovieTranslationsRequest(MovieID));
            return ProcessRequest<TmdbTranslations>(Generator.GetMovieTranslations(MovieID));
        }

        public string GetMovieTranslationsETag(int MovieID)
        {
            //return ProcessRequestETag(BuildGetMovieTranslationsRequest(MovieID));
            return ProcessRequestETag(ETagGenerator.GetMovieTranslations(MovieID));
        }

        /// <summary>
        /// Get the lists that the movie belongs to.
        /// http://docs.themoviedb.apiary.io/#get-%2F3%2Fmovie%2F%7Bid%7D%2Flists
        /// </summary>
        /// <param name="MovieID"></param>
        /// <param name="page"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        public TmdbList GetMovieLists(int MovieID, int page, string language = null)
        {
            return ProcessRequest<TmdbList>(Generator.GetMovieLists(MovieID, page, language));
        }

        /// <summary>
        /// Get list of changes for a specific movie, grouped by key and ordered by date in descending order.
        /// (http://docs.themoviedb.apiary.io/#get-%2F3%2Fmovie%2F%7Bid%7D%2Fchanges)
        /// </summary>
        /// <param name="MovieID">TMDB Movie ID</param>
        /// <returns></returns>
        public TmdbMovieChanges GetMovieChanges(int MovieID)
        {
            return ProcessRequest<TmdbMovieChanges>(Generator.GetMovieChanges(MovieID));
        }

        public string GetMovieChangesETag(int MovieID)
        {
            return ProcessRequestETag(ETagGenerator.GetMovieChanges(MovieID));
        }
        #endregion


        #region Person Info
        /// <summary>
        /// Get all of the basic information for a person.
        /// (http://help.themoviedb.org/kb/api/person-info)
        /// </summary>
        /// <param name="PersonID">Person ID</param>
        /// <returns></returns>
        public TmdbPerson GetPersonInfo(int PersonID)
        {
            //return ProcessRequest<TmdbPerson>(BuildGetPersonInfoRequest(PersonID));
            return ProcessRequest<TmdbPerson>(Generator.GetPersonInfo(PersonID));
        }

        public string GetPersonInfoETag(int PersonID)
        {
            //return ProcessRequestETag(BuildGetPersonInfoRequest(PersonID));
            return ProcessRequestETag(ETagGenerator.GetPersonInfo(PersonID));
        }

        /// <summary>
        /// Get list of cast and crew information for a person.
        /// (http://help.themoviedb.org/kb/api/person-credits)
        /// </summary>
        /// <param name="PersonID">Person ID</param>
        /// <param name="language">optional - ISO 639-1 language code</param>
        /// <returns></returns>
        public TmdbPersonCredits GetPersonCredits(int PersonID, string language = null)
        {
            //return ProcessRequest<TmdbPersonCredits>(BuildGetPersonCreditsRequest(PersonID, language));
            return ProcessRequest<TmdbPersonCredits>(Generator.GetPersonCredits(PersonID, language));
        }

        public string GetPersonCreditsETag(int PersonID, string language = null)
        {
            //return ProcessRequestETag(BuildGetPersonCreditsRequest(PersonID, language));
            return ProcessRequestETag(ETagGenerator.GetPersonCredits(PersonID, language));
        }

        /// <summary>
        /// Get list of images for a person.
        /// (http://help.themoviedb.org/kb/api/person-images)
        /// </summary>
        /// <param name="PersonID">Person ID</param>
        /// <returns></returns>
        public TmdbPersonImages GetPersonImages(int PersonID)
        {
            //return ProcessRequest<TmdbPersonImages>(BuildGetPersonImagesRequest(PersonID));
            return ProcessRequest<TmdbPersonImages>(Generator.GetPersonImages(PersonID));
        }

        public string GetPersonImagesETag(int PersonID)
        {
            //return ProcessRequestETag(BuildGetPersonImagesRequest(PersonID));
            return ProcessRequestETag(ETagGenerator.GetPersonImages(PersonID));
        }

        /// <summary>
        /// Get the list of changes for a specific person.  Changes are grouped by key and ordered by date in descending order.
        /// (http://docs.themoviedb.apiary.io/#get-%2F3%2Fperson%2F%7Bid%7D%2Fchanges)
        /// </summary>
        /// <param name="PersonID"></param>
        /// <returns></returns>
        public TmdbPersonChanges GetPersonChanges(int PersonID)
        {
            return ProcessRequest<TmdbPersonChanges>(Generator.GetPersonChanges(PersonID));
        }

        public string GetPersonChangesETag(int PersonID)
        {
            return ProcessRequestETag(ETagGenerator.GetPersonChanges(PersonID));
        }
        #endregion


        #region Miscellaneous Movie
        /// <summary>
        /// Get the newest movie added to the TMDB.
        /// (http://help.themoviedb.org/kb/api/latest-movie)
        /// </summary>
        /// <returns></returns>
        public TmdbLatestMovie GetLatestMovie()
        {
            //return ProcessRequest<TmdbLatestMovie>(BuildGetLatestMovieRequest());
            return ProcessRequest<TmdbLatestMovie>(Generator.GetLatestMovies());
        }

        public string GetLatestMovieETag()
        {
            //return ProcessRequestETag(BuildGetLatestMovieRequest());
            return ProcessRequestETag(ETagGenerator.GetLatestMovies());
        }

        /// <summary>
        /// Get the list of movies currently in theatres.  Response will contain 20 movies per page.
        /// (http://help.themoviedb.org/kb/api/now-playing-movies)
        /// </summary>
        /// <param name="page">Result page to retrieve (1 based)</param>
        /// <param name="language">optional - ISO 639-1 language code</param>
        /// <returns></returns>
        public TmdbNowPlaying GetNowPlayingMovies(int page, string language = "")
        {
            //return ProcessRequest<TmdbNowPlaying>(BuildGetNowPlayingMoviesRequest(page, language));
            return ProcessRequest<TmdbNowPlaying>(Generator.GetNowPlayingMovies(page, language));
        }

        public string GetNowPlayingMoviesETag(int page, string language = null)
        {
            //return ProcessRequestETag(BuildGetNowPlayingMoviesRequest(page, language));
            return ProcessRequestETag(ETagGenerator.GetNowPlayingMovies(page, language));
        }

        /// <summary>
        /// Get the daily popularity list of movies.  Response will contain 20 movies per page.
        /// (http://help.themoviedb.org/kb/api/popular-movie-list)
        /// </summary>
        /// <param name="page">Result page to retrieve (1 based)</param>
        /// <param name="language">optional - ISO 639-1 language code</param>
        /// <returns></returns>
        public TmdbPopular GetPopularMovies(int page, string language = null)
        {
            //return ProcessRequest<TmdbPopular>(BuildGetPopularMoviesRequest(page, language));
            return ProcessRequest<TmdbPopular>(Generator.GetPopularMovies(page, language));
        }

        public string GetPopularMoviesETag(int page, string language = null)
        {
            //return ProcessRequestETag(BuildGetPopularMoviesRequest(page, language));
            return ProcessRequestETag(ETagGenerator.GetPopularMovies(page, language));
        }

        /// <summary>
        /// Get list of movies that have over 10 votes on TMDB.  Response will contain 20 movies per page.
        /// (http://help.themoviedb.org/kb/api/top-rated-movies)
        /// </summary>
        /// <param name="page">Result page to retrieve (1 based)</param>
        /// <param name="language">optional - ISO 639-1 language code</param>
        /// <returns></returns>
        public TmdbTopRated GetTopRatedMovies(int page, string language = null)
        {
            //return ProcessRequest<TmdbTopRated>(BuildGetTopRatedMoviesRequest(page, language));
            return ProcessRequest<TmdbTopRated>(Generator.GetTopRatedMovies(page, language));
        }

        public string GetTopRatedMoviesETag(int page, string language = null)
        {
            //return ProcessRequestETag(BuildGetTopRatedMoviesRequest(page, language));
            return ProcessRequestETag(ETagGenerator.GetTopRatedMovies(page, language));
        }

        /// <summary>
        /// Get list of movies that are arriving to theatres in the next few weeks.
        /// (http://help.themoviedb.org/kb/api/upcoming-movies)
        /// </summary>
        /// <param name="page">Result page to retrieve (1 based)</param>
        /// <param name="language">optional - ISO 639-1 language code</param>
        /// <returns></returns>
        public TmdbUpcoming GetUpcomingMovies(int page, string language = null)
        {
            //return ProcessRequest<TmdbUpcoming>(BuildGetUpcomingMoviesRequest(page, language));
            return ProcessRequest<TmdbUpcoming>(Generator.GetUpcomingMovies(page, language));
        }

        public string GetUpcomingMoviesETag(int page, string language = null)
        {
            //return ProcessRequestETag(BuildGetUpcomingMoviesRequest(page, language));
            return ProcessRequestETag(ETagGenerator.GetUpcomingMovies(page, language));
        }

        #endregion


        #region Lists Methods

        /// <summary>
        /// Get a list by id
        /// http://docs.themoviedb.apiary.io/#get-%2F3%2Flist%2F%7Bid%7D
        /// </summary>
        /// <param name="ListID"></param>
        /// <returns></returns>
        public TmdbListItem GetList(string ListID)
        {
            return ProcessRequest<TmdbListItem>(Generator.GetList(ListID));
        }

        #endregion


        #region Company Info
        /// <summary>
        /// Get basic information about a production company from TMDB.
        /// (http://help.themoviedb.org/kb/api/company-info)
        /// </summary>
        /// <param name="CompanyID">Company ID</param>
        /// <returns></returns>
        public TmdbCompany GetCompanyInfo(int CompanyID)
        {
            //return ProcessRequest<TmdbCompany>(BuildGetCompanyInfoRequest(CompanyID));
            return ProcessRequest<TmdbCompany>(Generator.GetCompanyInfo(CompanyID));
        }

        public string GetCompanyInfoETag(int CompanyID)
        {
            //return ProcessRequestETag(BuildGetCompanyInfoRequest(CompanyID));
            return ProcessRequestETag(ETagGenerator.GetCompanyInfo(CompanyID));
        }

        /// <summary>
        /// Get list of movies associated with a company.  Response will contain 20 movies per page.
        /// (http://help.themoviedb.org/kb/api/company-movies)
        /// </summary>
        /// <param name="CompanyID">Company ID</param>
        /// <param name="page">Result page to retrieve (1 based)</param>
        /// <param name="language">optional - ISO 639-1 language code</param>
        /// <returns></returns>
        public TmdbCompanyMovies GetCompanyMovies(int CompanyID, int page, string language = null)
        {
            //return ProcessRequest<TmdbCompanyMovies>(BuildGetCompanyMoviesRequest(CompanyID, page, language));
            return ProcessRequest<TmdbCompanyMovies>(Generator.GetCompanyMovies(CompanyID, page, language));
        }

        public string GetCompanyMoviesETag(int CompanyID, int page, string language = null)
        {
            //return ProcessRequestETag(BuildGetCompanyMoviesRequest(CompanyID, page, language));
            return ProcessRequestETag(ETagGenerator.GetCompanyMovies(CompanyID, page, language));
        }

        #endregion


        #region Genre Info
        /// <summary>
        /// Get list of genres used in TMDB.  The ids will correspond to those found in movie calls.
        /// (http://help.themoviedb.org/kb/api/genre-list)
        /// </summary>
        /// <param name="language">optional - ISO 639-1 language code</param>
        /// <returns></returns>
        public TmdbGenre GetGenreList(string language = null)
        {
            //return ProcessRequest<TmdbGenre>(BuildGetGenreListRequest(language));
            return ProcessRequest<TmdbGenre>(Generator.GetGenreList(language));
        }

        public string GetGenreListETag(string language = null)
        {
            //return ProcessRequestETag(BuildGetGenreListRequest(language));
            return ProcessRequestETag(ETagGenerator.GetGenreList(language));
        }

        /// <summary>
        /// Get list of movies in a Genre.  Note that only movies with more than 10 votes get listed.
        /// (http://help.themoviedb.org/kb/api/genre-movies)
        /// </summary>
        /// <param name="GenreID">TMDB Genre ID</param>
        /// <param name="page">Result page to retrieve (1 based)</param>
        /// <param name="language">optional - ISO 639-1 language code</param>
        /// <returns></returns>
        public TmdbGenreMovies GetGenreMovies(int GenreID, int page, string language = null)
        {
            //return ProcessRequest<TmdbGenreMovies>(BuildGetGenreMoviesRequest(GenreID, page, language));
            return ProcessRequest<TmdbGenreMovies>(Generator.GetGenreMovies(GenreID, page, language));
        }

        public string GetGenreMoviesETag(int GenreID, int page, string language = null)
        {
            //return ProcessRequestETag(BuildGetGenreMoviesRequest(GenreID, page, language));
            return ProcessRequestETag(ETagGenerator.GetGenreMovies(GenreID, page, language));
        }

        #endregion


        #region Keyword Methods

        /// <summary>
        /// Get the basic information for a specific keyword id.
        /// http://docs.themoviedb.apiary.io/#get-%2F3%2Fkeyword%2F%7Bid%7D
        /// </summary>
        /// <param name="KeywordID"></param>
        /// <returns></returns>
        public TmdbKeyword GetKeyword(int KeywordID)
        {
            return ProcessRequest<TmdbKeyword>(Generator.GetKeyword(KeywordID));
        }

        /// <summary>
        /// Get the list of movies for a particular keyword by id.
        /// http://docs.themoviedb.apiary.io/#get-%2F3%2Fkeyword%2F%7Bid%7D%2Fmovies
        /// </summary>
        /// <param name="KeywordID"></param>
        /// <param name="page"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        public TmdbKeywordMovies GetKeywordMovies(int KeywordID, int page, string language = "")
        {
            return ProcessRequest<TmdbKeywordMovies>(Generator.GetKeywordMovies(KeywordID, page, language));
        }

        #endregion


        #region Changes methods

        /// <summary>
        /// Get a list of movie ids that have been edited.  By default results include the last 24 hours with 
        /// 100 items per page.  Then use the method GetMovieChanges to get the data that has changed.
        /// (http://docs.themoviedb.apiary.io/#get-%2F3%2Fmovie%2Fchanges)
        /// </summary>
        /// <param name="page"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        public TmdbChanges GetChangesByMovie(int page, DateTime? StartDate = null, DateTime? EndDate = null)
        {
            return ProcessRequest<TmdbChanges>(Generator.GetChangesByMovie(page, StartDate, EndDate));
        }

        /// <summary>
        /// Get a list of person ids that have been edited.  By default results include the last 24 hours with
        /// 100 items per page.  Then use the method GetPersonChanges to get the data that has changed.
        /// (http://docs.themoviedb.apiary.io/#get-%2F3%2Fperson%2Fchanges)
        /// </summary>
        /// <param name="page"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        public TmdbChanges GetChangesByPerson(int page, DateTime? StartDate = null, DateTime? EndDate = null)
        {
            return ProcessRequest<TmdbChanges>(Generator.GetChangesByPerson(page, StartDate, EndDate));
        }

        #endregion
    }
}
