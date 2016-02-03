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
        public const string Login = "https://api-v2launch.trakt.tv/auth/login";

        #region SEND Data to Trakt.tv (POST URIs)!

        // Modify watched movies/episodes/shows/seasons
        public const string SENDWatchedHistoryAdd = "https://api-v2launch.trakt.tv/sync/history";
        public const string SENDWatchedHistoryRemove = "https://api-v2launch.trakt.tv/sync/history/remove";
        // Modify ratings of movies/episodes/shows/seasons
        public const string SENDRatingsAdd = "https://api-v2launch.trakt.tv/sync/ratings";
        public const string SENDRatingsRemove = "https://api-v2launch.trakt.tv/sync/ratings/remove";
        // Modify watchlist of movies/episodes/shows/seasons
        public const string SENDWatchlistAdd = "https://api-v2launch.trakt.tv/sync/watchlist";
        public const string SENDWatchlistRemove = "https://api-v2launch.trakt.tv/sync/watchlist/remove";
        // Modify collection of movies/episodes/shows/seasons
        public const string SENDCollectionAdd = "https://api-v2launch.trakt.tv/sync/collection";
        public const string SENDCollectionRemove = "https://api-v2launch.trakt.tv/sync/collection/remove";

        //List requests
        public const string SENDListDelete = "https://api-v2launch.trakt.tv/users/{0}/lists/{1}";
        public const string SENDListAdd = "https://api-v2launch.trakt.tv/users/{0}/lists";
        public const string SENDListEdit = "https://api-v2launch.trakt.tv/users/{0}/lists/{1}";
        public const string SENDListItemsAdd = "https://api-v2launch.trakt.tv/users/{0}/lists/{1}/items";
        public const string SENDListItemsRemove = "https://api-v2launch.trakt.tv/users/{0}/lists/{1}/items/remove";
        
        //Comments requests
        public const string SENDCommentAdd = "https://api-v2launch.trakt.tv/comments";
        public const string SENDCommentLike = "https://api-v2launch.trakt.tv/comments/{0}/like";
        public const string SENDCommentDelete = "https://api-v2launch.trakt.tv/comments/{0}";
        public const string SENDCommentUpdate = "https://api-v2launch.trakt.tv/comments/{0}";
        public const string SENDCommentReply = "https://api-v2launch.trakt.tv/comments/{0}/replies";

        #endregion

        #region  GET Data from Trakt.tv (GET URIs)!

        // Movie requests
        public const string GETCollectionMovies = "https://api-v2launch.trakt.tv/sync/collection/movies";
        public const string GETWatchedMovies = "https://api-v2launch.trakt.tv/sync/watched/movies";
        public const string GETRatedMovies = "https://api-v2launch.trakt.tv/sync/ratings/movies";
        public const string GETMovieRating = "https://api-v2launch.trakt.tv/movies/{0}/ratings";

        // Show requests
        public const string GETCollectionEpisodes = "https://api-v2launch.trakt.tv/sync/collection/shows";
        public const string GETWatchedEpisodes = "https://api-v2launch.trakt.tv/sync/watched/shows";
        public const string GETRatedEpisodes = "https://api-v2launch.trakt.tv/sync/ratings/episodes";
        public const string GETRatedShows = "https://api-v2launch.trakt.tv/sync/ratings/shows";
        public const string GETRatedSeasons = "https://api-v2launch.trakt.tv/sync/ratings/seasons";
        public const string GETProgressShow = "https://api-v2launch.trakt.tv/shows/{0}/progress/watched";
        public const string GETShowRating = "https://api-v2launch.trakt.tv/shows/{0}/ratings";
        public const string GETSeasonRating = "https://api-v2launch.trakt.tv/shows/{0}/seasons/{1}/ratings";
        public const string GETEpisodeRating = "https://api-v2launch.trakt.tv/shows/{0}/seasons/{1}/episodes/{2}/ratings";

        // Lists requests
        public const string GETUserLists = "https://api-v2launch.trakt.tv/users/{0}/lists";
        public const string GETUserListItems = "https://api-v2launch.trakt.tv/users/{0}/lists/{1}/items?extended={2}";

        // Watchlist requests
        public const string GETWatchlistMovies = "https://api-v2launch.trakt.tv/users/{0}/watchlist/movies?extended={1}";
        public const string GETWatchlistShows = "https://api-v2launch.trakt.tv/users/{0}/watchlist/shows?extended=full,images";
        public const string GETWatchlistEpisodes = "https://api-v2launch.trakt.tv/users/{0}/watchlist/episodes?extended=full,images";

        public const string ShowSeasons = "https://api-v2launch.trakt.tv/shows/{0}/seasons?extended=full,images";
        public const string SeasonEpisodes = "https://api-v2launch.trakt.tv/shows/{0}/seasons/{1}?extended=full,images";

        // Friends/Followers requests
        public const string GETNetworkFriends = "https://api-v2launch.trakt.tv/users/{0}/friends?extended=full,images";
        public const string GETNetworkFollowers = "https://api-v2launch.trakt.tv/users/{0}/followers?extended=full,images";
        public const string GETNetworkFollowing = "https://api-v2launch.trakt.tv/users/{0}/following?extended=full,images";
        
        // Comments requests
        public const string GETUserComments = "https://api-v2launch.trakt.tv/users/{0}/comments/{1}/{2}?extended=full,images";      
        public const string GETCommentReplies = "https://api-v2launch.trakt.tv/comments/{0}/replies";
        public const string GETComment = "https://api-v2launch.trakt.tv/comments/{0}";
      
        // Search requests
        public const string GETSearchById = "https://api-v2launch.trakt.tv/search?id_type={0}&id={1}";
        #endregion
    }
}
