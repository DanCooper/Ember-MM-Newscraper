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

            request.AddHeader("Accept", "application/json");
            request.AddParameter("api_key", ApiKey);

            var resp = client.Execute<T>(request);

            ResponseContent = resp.Content;
            ResponseHeaders = resp.Headers.ToDictionary(k => k.Name, v => v.Value);

            if (resp.ResponseStatus == ResponseStatus.Completed)
            {
                if (resp.Content.Contains("status_message"))
                    Error = jsonDeserializer.Deserialize<TmdbError>(resp);

                return resp.Data;
            }
            else
            {
                if (resp.Content.Contains("status_message"))
                    Error = jsonDeserializer.Deserialize<TmdbError>(resp);
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
            request.AddHeader("Accept", "application/json");
            request.AddParameter("api_key", ApiKey);

            var resp = client.Execute(request);
            ResponseContent = resp.Content;
            ResponseHeaders = resp.Headers.ToDictionary(k => k.Name, v => v.Value);

            if (resp.ResponseStatus != ResponseStatus.Completed && resp.ErrorException != null)
                throw resp.ErrorException;

            return this.ResponseETag;
        }


        #region Configuration
        /// <summary>
        /// Retrieve configuration data from TMDB
        /// (http://help.themoviedb.org/kb/api/configuration)
        /// </summary>
        /// <returns></returns>
        public TmdbConfiguration GetConfiguration()
        {
            return ProcessRequest<TmdbConfiguration>(BuildGetConfigurationRequest());
        }

        public string GetConfigurationETag()
        {
            return ProcessRequestETag(BuildGetConfigurationRequest());
        }

        #endregion


        #region Authentication
        //public TmdbAuthToken GetAuthToken()
        //{
        //    var request = new RestRequest("authentication/token/new", Method.GET);
        //    return ProcessRequest<TmdbAuthToken>(request);
        //}

        //public TmdbAuthSession GetAuthSession(string RequestToken)
        //{
        //    var request = new RestRequest("authentication/session/new", Method.GET);
        //    request.AddParameter("request_token", RequestToken);
        //    return ProcessRequest<TmdbAuthSession>(request);
        //}
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
        public TmdbMovieSearch SearchMovie(string query, int page, string language, bool? includeAdult = null, int? year = null)
        {
            if (string.IsNullOrEmpty(query))
                throw new ArgumentException("Search must be supplied");

            return ProcessRequest<TmdbMovieSearch>(BuildSearchMovieRequest(query, page, language, includeAdult, year));
        }

        /// <summary>
        /// Search for movies that are listed in TMDB
        /// (http://help.themoviedb.org/kb/api/search-movies)
        /// </summary>
        /// <param name="query">Is your search text.</param>
        /// <param name="page">Result page to retrieve (1 based)</param>
        /// <returns></returns>
        public TmdbMovieSearch SearchMovie(string query, int page)
        {
            return SearchMovie(query, page, Language);
        }

        /// <summary>
        /// Search for people that are listed in TMDB.
        /// (http://help.themoviedb.org/kb/api/search-people)
        /// </summary>
        /// <param name="query">Is your search text.</param>
        /// <param name="page">Result page to retrieve (1 based)</param>
        /// <param name="language">optional - ISO 639-1 language code</param>
        /// <returns></returns>
        public TmdbPersonSearch SearchPerson(string query, int page, string language)
        {
            if (string.IsNullOrEmpty(query))
                throw new ArgumentException("Search must be supplied");

            return ProcessRequest<TmdbPersonSearch>(BuildSearchPersonRequest(query, page, language));
        }

        /// <summary>
        /// Search for people that are listed in TMDB.
        /// (http://help.themoviedb.org/kb/api/search-people)
        /// </summary>
        /// <param name="query">Is your search text.</param>
        /// <param name="page">Result page to retrieve (1 based)</param>
        /// <returns></returns>
        public TmdbPersonSearch SearchPerson(string query, int page)
        {
            return SearchPerson(query, page, Language);
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

            return ProcessRequest<TmdbCompanySearch>(BuildSearchCompanyRequest(query, page));
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
        public TmdbCollection GetCollectionInfo(int CollectionID, string language)
        {
            return ProcessRequest<TmdbCollection>(BuildGetCollectionInfoRequest(CollectionID, language));
        }

        public string GetCollectionInfoETag(int CollectionID, string language)
        {
            return ProcessRequestETag(BuildGetCollectionInfoRequest(CollectionID, language));
        }

        /// <summary>
        /// Get all of the basic information about a movie collection.
        /// (http://help.themoviedb.org/kb/api/collection-info)
        /// </summary>
        /// <param name="CollectionID">Collection ID, available in TmdbMovie::belongs_to_collection</param>
        /// <returns></returns>
        public TmdbCollection GetCollectionInfo(int CollectionID)
        {
            return GetCollectionInfo(CollectionID, Language);
        }

        public string GetCollectionInfoETag(int CollectionID)
        {
            return GetCollectionInfoETag(CollectionID, Language);
        }

        /// <summary>
        /// Get all the images for a movie collection
        /// http://help.themoviedb.org/kb/api/collection-images
        /// </summary>
        /// <param name="CollectionID">Collection ID, available in TmdbMovie::belongs_to_collection</param>
        /// <param name="language">optional - ISO 639-1 language code</param>
        /// <returns></returns>
        public TmdbCollectionImages GetCollectionImages(int CollectionID, string language)
        {
            return ProcessRequest<TmdbCollectionImages>(BuildGetCollectionImagesRequest(CollectionID, language));
        }

        public string GetCollectionImagesETag(int CollectionID, string language)
        {
            return ProcessRequestETag(BuildGetCollectionImagesRequest(CollectionID, language));
        }

        /// <summary>
        /// Get all the images for a movie collection
        /// http://help.themoviedb.org/kb/api/collection-images
        /// </summary>
        /// <param name="CollectionID">Collection ID, available in TmdbMovie::belongs_to_collection</param>
        /// <returns></returns>
        public TmdbCollectionImages GetCollectionImages(int CollectionID)
        {
            return GetCollectionImages(CollectionID, Language);
        }

        public string GetCollectionImagesETag(int CollectionID)
        {
            return GetCollectionImagesETag(CollectionID, Language);
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
        public TmdbMovie GetMovieInfo(int MovieID, string language)
        {
            return ProcessRequest<TmdbMovie>(BuildGetMovieInfoRequest(MovieID, language));
        }

        public string GetMovieInfoETag(int MovieID, string language)
        {
            return ProcessRequestETag(BuildGetMovieInfoRequest(MovieID, language));
        }

        /// <summary>
        /// Retrieve all the basic movie information for a particular movie by TMDB reference.
        /// (http://help.themoviedb.org/kb/api/movie-info)
        /// </summary>
        /// <param name="MovieID">TMDB movie id</param>
        /// <returns></returns>
        public TmdbMovie GetMovieInfo(int MovieID)
        {
            return GetMovieInfo(MovieID, Language);
        }

        public string GetMovieInfoETag(int MovieID)
        {
            return GetMovieInfoETag(MovieID, Language);
        }

        /// <summary>
        /// Retrieve all the basic movie information for a particular movie by IMDB reference.
        /// (http://help.themoviedb.org/kb/api/movie-info)
        /// </summary>
        /// <param name="IMDB_ID">IMDB movie id</param>
        /// <returns></returns>
        public TmdbMovie GetMovieByIMDB(string IMDB_ID, string language)
        {
            if (string.IsNullOrEmpty(IMDB_ID))
                throw new ArgumentException("IMDB_ID must be supplied");

            return ProcessRequest<TmdbMovie>(BuildGetMovieByIMDBRequest(IMDB_ID, language));
        }

        public TmdbMovie GetMovieByIMDB(string IMDB_ID)
        {
            return GetMovieByIMDB(IMDB_ID, Language);
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
            return ProcessRequest<TmdbMovieAlternateTitles>(BuildGetMovieAlternateTitlesRequest(MovieID, Country));
        }

        public string GetMovieAlternateTitlesETag(int MovieID, string Country)
        {
            return ProcessRequestETag(BuildGetMovieAlternateTitlesRequest(MovieID, Country));
        }

        /// <summary>
        /// Get list of all the cast information for a particular movie.
        /// (http://help.themoviedb.org/kb/api/movie-casts)
        /// </summary>
        /// <param name="MovieID">TMDB movie id</param>
        /// <returns></returns>
        public TmdbMovieCast GetMovieCast(int MovieID)
        {
            return ProcessRequest<TmdbMovieCast>(BuildGetMovieCastRequest(MovieID));
        }

        public string GetMovieCastETag(int MovieID)
        {
            return ProcessRequestETag(BuildGetMovieCastRequest(MovieID));
        }

        /// <summary>
        /// Get list of all the images for a particular movie.
        /// (http://help.themoviedb.org/kb/api/movie-images)
        /// </summary>
        /// <param name="MovieID">TMDB movie id</param>
        /// <param name="language">optional - ISO 639-1 language code</param>
        /// <returns></returns>
        public TmdbMovieImages GetMovieImages(int MovieID, string language)
        {
            return ProcessRequest<TmdbMovieImages>(BuildGetMovieImagesRequest(MovieID, language));
        }

        public string GetMovieImagesETag(int MovieID, string language)
        {
            return ProcessRequestETag(BuildGetMovieImagesRequest(MovieID, language));
        }

        /// <summary>
        /// Get list of all the images for a particular movie.
        /// (http://help.themoviedb.org/kb/api/movie-images)
        /// </summary>
        /// <param name="MovieID">TMDB movie id</param>
        /// <returns></returns>
        public TmdbMovieImages GetMovieImages(int MovieID)
        {
            return GetMovieImages(MovieID, Language);
        }

        public string GetMovieImagesETag(int MovieID)
        {
            return GetMovieImagesETag(MovieID, Language);
        }

        /// <summary>
        /// Get list of all the keywords that have been added to a particular movie.  Only English keywords exist currently.
        /// (http://help.themoviedb.org/kb/api/movie-keywords)
        /// </summary>
        /// <param name="MovieID">TMDB movie id</param>
        /// <returns></returns>
        public TmdbMovieKeywords GetMovieKeywords(int MovieID)
        {
            return ProcessRequest<TmdbMovieKeywords>(BuildGetMovieKeywordsRequest(MovieID));
        }

        public string GetMovieKeywordsETag(int MovieID)
        {
            return ProcessRequestETag(BuildGetMovieKeywordsRequest(MovieID));
        }

        /// <summary>
        /// Get all the release and certification data in TMDB for a particular movie
        /// (http://help.themoviedb.org/kb/api/movie-release-info)
        /// </summary>
        /// <param name="MovieID">TMDB movie id</param>
        /// <returns></returns>
        public TmdbMovieReleases GetMovieReleases(int MovieID)
        {
            return ProcessRequest<TmdbMovieReleases>(BuildGetMovieReleasesRequest(MovieID));
        }

        public string GetMovieReleasesETag(int MovieID)
        {
            return ProcessRequestETag(BuildGetMovieReleasesRequest(MovieID));
        }

        /// <summary>
        /// Get list of trailers for a particular movie.
        /// (http://help.themoviedb.org/kb/api/movie-trailers)
        /// </summary>
        /// <param name="MovieID">TMDB movie id</param>
        /// <param name="language">optional - ISO 639-1 language code</param>
        /// <returns></returns>
        public TmdbMovieTrailers GetMovieTrailers(int MovieID, string language)
        {
            return ProcessRequest<TmdbMovieTrailers>(BuildGetMovieTrailersRequest(MovieID, language));
        }

        public string GetMovieTrailersETag(int MovieID, string language)
        {
            return ProcessRequestETag(BuildGetMovieTrailersRequest(MovieID, language));
        }

        /// <summary>
        /// Get list of trailers for a particular movie.
        /// (http://help.themoviedb.org/kb/api/movie-trailers)
        /// </summary>
        /// <param name="MovieID">TMDB movie id</param>
        /// <returns></returns>
        public TmdbMovieTrailers GetMovieTrailers(int MovieID)
        {
            return GetMovieTrailers(MovieID, Language);
        }

        public string GetMovieTrailersETag(int MovieID)
        {
            return GetMovieTrailersETag(MovieID, Language);
        }

        /// <summary>
        /// Get list of similar movies for a particular movie.
        /// (http://help.themoviedb.org/kb/api/movie-similar-movies)
        /// </summary>
        /// <param name="MovieID">TMDB movie id</param>
        /// <param name="page">Result page to retrieve (1 based)</param>
        /// <param name="language">optional - ISO 639-1 language code</param>
        /// <returns></returns>
        public TmdbSimilarMovies GetSimilarMovies(int MovieID, int page, string language)
        {
            return ProcessRequest<TmdbSimilarMovies>(BuildGetSimilarMoviesRequest(MovieID, page, language));
        }

        /// <summary>
        /// Get list of similar movies for a particular movie.
        /// (http://help.themoviedb.org/kb/api/movie-similar-movies)
        /// </summary>
        /// <param name="MovieID">TMDB movie id</param>
        /// <param name="page">Result page to retrieve (1 based)</param>
        /// <returns></returns>
        public TmdbSimilarMovies GetSimilarMovies(int MovieID, int page)
        {
            return GetSimilarMovies(MovieID, page, Language);
        }

        /// <summary>
        /// Get list of all available translations for a specific movie.
        /// (http://help.themoviedb.org/kb/api/movie-translations)
        /// </summary>
        /// <param name="MovieID">TMDB movie id</param>
        /// <returns></returns>
        public TmdbTranslations GetMovieTranslations(int MovieID)
        {
            return ProcessRequest<TmdbTranslations>(BuildGetMovieTranslationsRequest(MovieID));
        }

        public string GetMovieTranslationsETag(int MovieID)
        {
            return ProcessRequestETag(BuildGetMovieTranslationsRequest(MovieID));
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
            return ProcessRequest<TmdbPerson>(BuildGetPersonInfoRequest(PersonID));
        }

        public string GetPersonInfoETag(int PersonID)
        {
            return ProcessRequestETag(BuildGetPersonInfoRequest(PersonID));
        }

        /// <summary>
        /// Get list of cast and crew information for a person.
        /// (http://help.themoviedb.org/kb/api/person-credits)
        /// </summary>
        /// <param name="PersonID">Person ID</param>
        /// <param name="language">optional - ISO 639-1 language code</param>
        /// <returns></returns>
        public TmdbPersonCredits GetPersonCredits(int PersonID, string language)
        {
            return ProcessRequest<TmdbPersonCredits>(BuildGetPersonCreditsRequest(PersonID, language));
        }

        public string GetPersonCreditsETag(int PersonID, string language)
        {
            return ProcessRequestETag(BuildGetPersonCreditsRequest(PersonID, language));
        }

        /// <summary>
        /// Get list of cast and crew information for a person.
        /// (http://help.themoviedb.org/kb/api/person-credits)
        /// </summary>
        /// <param name="PersonID">Person ID</param>
        /// <returns></returns>
        public TmdbPersonCredits GetPersonCredits(int PersonID)
        {
            return GetPersonCredits(PersonID, Language);
        }

        public string GetPersonCreditsETag(int PersonID)
        {
            return GetPersonCreditsETag(PersonID, Language);
        }

        /// <summary>
        /// Get list of images for a person.
        /// (http://help.themoviedb.org/kb/api/person-images)
        /// </summary>
        /// <param name="PersonID">Person ID</param>
        /// <returns></returns>
        public TmdbPersonImages GetPersonImages(int PersonID)
        {
            return ProcessRequest<TmdbPersonImages>(BuildGetPersonImagesRequest(PersonID));
        }

        public string GetPersonImagesETag(int PersonID)
        {
            return ProcessRequestETag(BuildGetPersonImagesRequest(PersonID));
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
            return ProcessRequest<TmdbLatestMovie>(BuildGetLatestMovieRequest());
        }

        public string GetLatestMovieETag()
        {
            return ProcessRequestETag(BuildGetLatestMovieRequest());
        }

        /// <summary>
        /// Get the list of movies currently in theatres.  Response will contain 20 movies per page.
        /// (http://help.themoviedb.org/kb/api/now-playing-movies)
        /// </summary>
        /// <param name="page">Result page to retrieve (1 based)</param>
        /// <param name="language">optional - ISO 639-1 language code</param>
        /// <returns></returns>
        public TmdbNowPlaying GetNowPlayingMovies(int page, string language)
        {
            return ProcessRequest<TmdbNowPlaying>(BuildGetNowPlayingMoviesRequest(page, language));
        }

        public string GetNowPlayingMoviesETag(int page, string language)
        {
            return ProcessRequestETag(BuildGetNowPlayingMoviesRequest(page, language));
        }

        /// <summary>
        /// Get the list of movies currently in theatres.  Response will contain 20 movies per page.
        /// (http://help.themoviedb.org/kb/api/now-playing-movies)
        /// </summary>
        /// <param name="page">Result page to retrieve (1 based)</param>
        /// <returns></returns>
        public TmdbNowPlaying GetNowPlayingMovies(int page)
        {
            return GetNowPlayingMovies(page, Language);
        }

        public string GetNowPlayingMoviesETag(int page)
        {
            return GetNowPlayingMoviesETag(page, Language);
        }

        /// <summary>
        /// Get the daily popularity list of movies.  Response will contain 20 movies per page.
        /// (http://help.themoviedb.org/kb/api/popular-movie-list)
        /// </summary>
        /// <param name="page">Result page to retrieve (1 based)</param>
        /// <param name="language">optional - ISO 639-1 language code</param>
        /// <returns></returns>
        public TmdbPopular GetPopularMovies(int page, string language)
        {
            return ProcessRequest<TmdbPopular>(BuildGetPopularMoviesRequest(page, language));
        }

        public string GetPopularMoviesETag(int page, string language)
        {
            return ProcessRequestETag(BuildGetPopularMoviesRequest(page, language));
        }

        /// <summary>
        /// Get the daily popularity list of movies.  Response will contain 20 movies per page.
        /// (http://help.themoviedb.org/kb/api/popular-movie-list)
        /// </summary>
        /// <param name="page">Result page to retrieve (1 based)</param>
        /// <returns></returns>
        public TmdbPopular GetPopularMovies(int page)
        {
            return GetPopularMovies(page, Language);
        }

        public string GetPopularMoviesETag(int page)
        {
            return GetPopularMoviesETag(page, Language);
        }

        /// <summary>
        /// Get list of movies that have over 10 votes on TMDB.  Response will contain 20 movies per page.
        /// (http://help.themoviedb.org/kb/api/top-rated-movies)
        /// </summary>
        /// <param name="page">Result page to retrieve (1 based)</param>
        /// <param name="language">optional - ISO 639-1 language code</param>
        /// <returns></returns>
        public TmdbTopRated GetTopRatedMovies(int page, string language)
        {
            return ProcessRequest<TmdbTopRated>(BuildGetTopRatedMoviesRequest(page, language));
        }

        public string GetTopRatedMoviesETag(int page, string language)
        {
            return ProcessRequestETag(BuildGetTopRatedMoviesRequest(page, language));
        }

        /// <summary>
        /// Get list of movies that have over 10 votes on TMDB.  Response will contain 20 movies per page.
        /// (http://help.themoviedb.org/kb/api/top-rated-movies)
        /// </summary>
        /// <param name="page">Result page to retrieve (1 based)</param>
        /// <returns></returns>
        public TmdbTopRated GetTopRatedMovies(int page)
        {
            return GetTopRatedMovies(page, Language);
        }

        public string GetTopRatedMoviesETag(int page)
        {
            return GetTopRatedMoviesETag(page, Language);
        }

        /// <summary>
        /// Get list of movies that are arriving to theatres in the next few weeks.
        /// (http://help.themoviedb.org/kb/api/upcoming-movies)
        /// </summary>
        /// <param name="page">Result page to retrieve (1 based)</param>
        /// <param name="language">optional - ISO 639-1 language code</param>
        /// <returns></returns>
        public TmdbUpcoming GetUpcomingMovies(int page, string language)
        {
            return ProcessRequest<TmdbUpcoming>(BuildGetUpcomingMoviesRequest(page, language));
        }

        public string GetUpcomingMoviesETag(int page, string language)
        {
            return ProcessRequestETag(BuildGetUpcomingMoviesRequest(page, language));
        }

        /// <summary>
        /// Get list of movies that are arriving to theatres in the next few weeks.
        /// (http://help.themoviedb.org/kb/api/upcoming-movies)
        /// </summary>
        /// <param name="page">Result page to retrieve (1 based)</param>
        /// <returns></returns>
        public TmdbUpcoming GetUpcomingMovies(int page)
        {
            return GetUpcomingMovies(page, Language);
        }

        public string GetUpcomingMoviesETag(int page)
        {
            return GetUpcomingMoviesETag(page, Language);
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
            return ProcessRequest<TmdbCompany>(BuildGetCompanyInfoRequest(CompanyID));
        }

        public string GetCompanyInfoETag(int CompanyID)
        {
            return ProcessRequestETag(BuildGetCompanyInfoRequest(CompanyID));
        }

        /// <summary>
        /// Get list of movies associated with a company.  Response will contain 20 movies per page.
        /// (http://help.themoviedb.org/kb/api/company-movies)
        /// </summary>
        /// <param name="CompanyID">Company ID</param>
        /// <param name="page">Result page to retrieve (1 based)</param>
        /// <param name="language">optional - ISO 639-1 language code</param>
        /// <returns></returns>
        public TmdbCompanyMovies GetCompanyMovies(int CompanyID, int page, string language)
        {
            return ProcessRequest<TmdbCompanyMovies>(BuildGetCompanyMoviesRequest(CompanyID, page, language));
        }

        public string GetCompanyMoviesETag(int CompanyID, int page, string language)
        {
            return ProcessRequestETag(BuildGetCompanyMoviesRequest(CompanyID, page, language));
        }

        /// <summary>
        /// Get list of movies associated with a company.  Response will contain 20 movies per page.
        /// (http://help.themoviedb.org/kb/api/company-movies)
        /// </summary>
        /// <param name="CompanyID">Company ID</param>
        /// <param name="page">Result page to retrieve (1 based)</param>
        /// <returns></returns>
        public TmdbCompanyMovies GetCompanyMovies(int CompanyID, int page)
        {
            return GetCompanyMovies(CompanyID, page, Language);
        }

        public string GetCompanyMoviesETag(int CompanyID, int page)
        {
            return GetCompanyMoviesETag(CompanyID, page, Language);
        }
        #endregion


        #region Genre Info
        /// <summary>
        /// Get list of genres used in TMDB.  The ids will correspond to those found in movie calls.
        /// (http://help.themoviedb.org/kb/api/genre-list)
        /// </summary>
        /// <param name="language">optional - ISO 639-1 language code</param>
        /// <returns></returns>
        public TmdbGenre GetGenreList(string language)
        {
            return ProcessRequest<TmdbGenre>(BuildGetGenreListRequest(language));
        }

        public string GetGenreListETag(string language)
        {
            return ProcessRequestETag(BuildGetGenreListRequest(language));
        }

        /// <summary>
        /// Get list of genres used in TMDB.  The ids will correspond to those found in movie calls.
        /// (http://help.themoviedb.org/kb/api/genre-list)
        /// </summary>
        /// <returns></returns>
        public TmdbGenre GetGenreList()
        {
            return GetGenreList(Language);
        }

        public string GetGenreListETag()
        {
            return GetGenreListETag(Language);
        }

        /// <summary>
        /// Get list of movies in a Genre.  Note that only movies with more than 10 votes get listed.
        /// (http://help.themoviedb.org/kb/api/genre-movies)
        /// </summary>
        /// <param name="GenreID">TMDB Genre ID</param>
        /// <param name="page">Result page to retrieve (1 based)</param>
        /// <param name="language">optional - ISO 639-1 language code</param>
        /// <returns></returns>
        public TmdbGenreMovies GetGenreMovies(int GenreID, int page, string language)
        {
            return ProcessRequest<TmdbGenreMovies>(BuildGetGenreMoviesRequest(GenreID, page, language));
        }

        public string GetGenreMoviesETag(int GenreID, int page, string language)
        {
            return ProcessRequestETag(BuildGetGenreMoviesRequest(GenreID, page, language));
        }

        /// <summary>
        /// Get list of movies in a Genre.  Note that only movies with more than 10 votes get listed.
        /// (http://help.themoviedb.org/kb/api/genre-movies)
        /// </summary>
        /// <param name="GenreID">TMDB Genre ID</param>
        /// <param name="page">Result page to retrieve (1 based)</param>
        /// <returns></returns>
        public TmdbGenreMovies GetGenreMovies(int GenreID, int page)
        {
            return GetGenreMovies(GenreID, page, Language);
        }

        public string GetGenreMoviesETag(int GenreID, int page)
        {
            return GetGenreMoviesETag(GenreID, page, Language);
        }
        #endregion
    }
}
