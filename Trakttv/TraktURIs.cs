using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trakttv
{
    /// <summary>
    /// List of URIs for the Trakt API v2
    /// </summary>
    public static class TraktURIs
    {

        // Login for API Requests v2
        public const string Login = "http://api.trakt.tv/auth/login";

        #region SEND Data to Trakt.tv (POST URIs)!

        // Modify watched movies/episodes/shows/seasons
        public const string SENDWatchedHistoryAdd = "http://api.trakt.tv/sync/history";
        public const string SENDWatchedHistoryRemove = "http://api.trakt.tv/sync/history/remove";
        // Modify ratings of movies/episodes/shows/seasons
        public const string SENDRatingsAdd = "http://api.trakt.tv/sync/ratings";
        public const string SENDRatingsRemove = "http://api.trakt.tv/sync/ratings/remove";
        // Modify watchlist of movies/episodes/shows/seasons
        public const string SENDWatchlistAdd = "http://api.trakt.tv/sync/watchlist";
        public const string SENDWatchlistRemove = "http://api.trakt.tv/sync/watchlist/remove";
        // Modify collection of movies/episodes/shows/seasons
        public const string SENDCollectionAdd = "http://api.trakt.tv/sync/collection";
        public const string SENDCollectionRemove = "http://api.trakt.tv/sync/collection/remove";

        //List requests
        public const string SENDListDelete = "http://api.trakt.tv/users/{0}/lists/{1}";
        public const string SENDListAdd = "http://api.trakt.tv/users/{0}/lists";
        public const string SENDListEdit = "http://api.trakt.tv/users/{0}/lists/{1}";
        public const string SENDListItemsAdd = "http://api.trakt.tv/users/{0}/lists/{1}/items";
        public const string SENDListItemsRemove = "http://api.trakt.tv/users/{0}/lists/{1}/items/remove";

        #endregion

        #region  GET Data from Trakt.tv (GET URIs)!

        // Movie requests
        public const string GETCollectionMovies = "http://api.trakt.tv/sync/collection/movies";
        public const string GETWatchedMovies = "http://api.trakt.tv/sync/watched/movies";
        public const string GETRatedMovies = "http://api.trakt.tv/sync/ratings/movies";

        // Show requests
        public const string GETCollectionEpisodes = "http://api.trakt.tv/sync/collection/shows";
        public const string GETWatchedEpisodes = "http://api.trakt.tv/sync/watched/shows";
        public const string GETRatedEpisodes = "http://api.trakt.tv/sync/ratings/episodes";
        public const string GETRatedShows = "http://api.trakt.tv/sync/ratings/shows";

        // Lists requests
        public const string GETUserLists = "http://api.trakt.tv/users/{0}/lists";
        public const string GETUserListItems = "http://api.trakt.tv/users/{0}/lists/{1}/items?extended={2}";

        // Watchlist requests
        public const string GETWatchlistMovies = "http://api.trakt.tv/users/{0}/watchlist/movies?extended={1}";
        public const string GETWatchlistShows = "http://api.trakt.tv/users/{0}/watchlist/shows?extended=full,images";
        public const string GETWatchlistEpisodes = "http://api.trakt.tv/users/{0}/watchlist/episodes?extended=full,images";

        public const string ShowSeasons = "http://api.trakt.tv/shows/{0}/seasons?extended=full,images";
        public const string SeasonEpisodes = "http://api.trakt.tv/shows/{0}/seasons/{1}?extended=full,images";
        #endregion
    }
}
