using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trakttv.TraktAPI
{
    /// <summary>
    /// List of URIs for the Trakt API
    /// </summary>
    public static class TraktURIs
    {

        // APIKEY - For now define here!
        public const string ApiKey = "ce4d4ac977084c873da8738f949d380776756b82";


        #region SEND Data to Trakt.tv (POST URIs)!

        // Account requests
        public const string AccountSettings = @"http://api.trakt.tv/account/settings/" + ApiKey;

        // Movie requests
        //{0] being one of:  library,seen,unlibrary,unseen,watchlist,unwatchlist)
        public const string SENDMovieData = @"http://api.trakt.tv/movie/{0}/" + ApiKey;
        //{0] being one of:  scrobble,watching,cancelwatching )
        public const string SENDMovieScrobble = @"http://api.trakt.tv/movie/{0}/" + ApiKey;

        //for now replaced by flexible URI...
        //public const string SENDMovieSeen = @"http://api.trakt.tv/movie/seen/" + ApiKey;
        //public const string SENDMovieLibrary = @"http://api.trakt.tv/movie/library/" + ApiKey;
        //public const string SENDMovieUnLibrary = @"http://api.trakt.tv/movie/unlibrary/" + ApiKey;
        //public const string SENDMovieUnSeen = @"http://api.trakt.tv/movie/unseen/" + ApiKey;
        //public const string SENDMovieUnWatchList = @"http://api.trakt.tv/movie/unwatchlist/" + ApiKey;
        //public const string SENDMovieWatching = @"http://api.trakt.tv/movie/watching/" + ApiKey;
        //public const string SENDMovieWatchList = @"http://api.trakt.tv/movie/watchlist/" + ApiKey;
        //public const string SENDMovieScrobble = @"http://api.trakt.tv/movie/watching/" + ApiKey;

        // TV Show requests
        //{0] being one of:  library,seen,unlibrary,unseen,watchlist,unwatchlist)
        public const string SENDEpisodeData = @"http://api.trakt.tv/show/episode/{0}/" + ApiKey;
        //{0] being one of:  library,seen,unlibrary,unseen,watchlist,unwatchlist)
        public const string SENDSeasonData = @"http://api.trakt.tv/show/season/{0}/" + ApiKey;
        //{0] being one of:  library,seen,unlibrary,unseen,watchlist,unwatchlist)
        public const string SENDShowData = @"http://api.trakt.tv/show/{0}/" + ApiKey;
        public const string SENDShowSeasons = @"http://api.trakt.tv/show/seasons.json/" + ApiKey + @"/{0}";
        public const string SENDSeasonEpisodes = @"http://api.trakt.tv/show/season.json/" + ApiKey + @"/{0}/{1}";
        //{0] being one of:  scrobble,watching,cancelwatching )
        public const string SENDScrobbleShow = @"http://api.trakt.tv/show/{0}/" + ApiKey;
        //for now replaced by flexible URI...
        //public const string SENDEpisodeLibrary = @"http://api.trakt.tv/show/episode/library/" + ApiKey;
        //public const string SENDEpisodeSeen = @"http://api.trakt.tv/show/episode/seen/" + ApiKey;
        //public const string SENDEpisodeUnLibrary = @"http://api.trakt.tv/show/episode/unlibrary/" + ApiKey;
        //public const string SENDEpisodeUnSeen = @"http://api.trakt.tv/show/episode/unseen/" + ApiKey;
        //public const string SENDEpisodeUnWatchList = @"http://api.trakt.tv/show/episode/unwatchlist/" + ApiKey;
        //public const string SENDEpisodeWatchList = @"http://api.trakt.tv/show/episode/watchlist/" + ApiKey;
        //public const string SENDShowUnLibrary = @"http://api.trakt.tv/show/unlibrary/"  + ApiKey;
        //public const string SENDShowUnWatchList = @"http://api.trakt.tv/show/unwatchlist/"  + ApiKey;
        //public const string SENDShowWatchList = @"http://api.trakt.tv/show/watchlist/"  + ApiKey;
        //public const string SENDShowSeen = @"http://api.trakt.tv/show/seen/" + ApiKey;
        //public const string SENDShowLibrary = @"http://api.trakt.tv/show/library/" + ApiKey;
        //public const string SENDSeasonSeen = @"http://api.trakt.tv/show/season/seen/" + ApiKey;
        //public const string SENDSeasonLibrary = @"http://api.trakt.tv/show/season/library/" + ApiKey;

        //List requests
        public const string SENDListAdd = @"http://api.trakt.tv/lists/add/" + ApiKey;
        public const string SENDListDelete = @"http://api.trakt.tv/lists/delete/" + ApiKey;
        public const string SENDListItemsAdd = @"http://api.trakt.tv/lists/items/add/" + ApiKey;
        public const string SENDListItemsDelete = @"http://api.trakt.tv/lists/items/delete/" + ApiKey;
        public const string SENDListUpdate = @"http://api.trakt.tv/lists/update/" + ApiKey;

        // Friends requests
        public const string Friends = @"http://api.trakt.tv/friends/all/" + ApiKey;
        public const string FriendRequests = @"http://api.trakt.tv/friends/requests/" + ApiKey;
        public const string FriendAdd = @"http://api.trakt.tv/friends/add/" + ApiKey;
        public const string FriendApprove = @"http://api.trakt.tv/friends/approve/" + ApiKey;
        public const string FriendDeny = @"http://api.trakt.tv/friends/deny/" + ApiKey;
        public const string FriendDelete = @"http://api.trakt.tv/friends/delete/" + ApiKey;

        // Rate
        //{0] being one of:   episode, season,show,movie)
        public const string SENDRating = @"http://api.trakt.tv/rate/{0}/" + ApiKey;
        public const string SENDRatingMovies = @"http://api.trakt.tv/rate/movies/" + ApiKey;
        public const string SENDRatingShows = @"http://api.trakt.tv/rate/shows/" + ApiKey;
        public const string SENDRatingEpisodes = @"http://api.trakt.tv/rate/episodes/" + ApiKey;
        //for now replaced by flexible URI...
        // public const string RateEpisode = @"http://api.trakt.tv/rate/episode/" + ApiKey;
        //public const string RateSeason = @"http://api.trakt.tv/rate/season/" + ApiKey;
        //public const string RateMovie= @"http://api.trakt.tv/rate/movie/" + ApiKey;
        // public const string RateMovie= @"http://api.trakt.tv/rate/show/" + ApiKey;

        // Recommendations requests
        public const string SENDMovieRecommendations = @"http://api.trakt.tv/recommendations/movies/" + ApiKey;
        public const string SENDShowsRecommendations = @"http://api.trakt.tv/recommendations/shows/" + ApiKey;
        public const string SENDDismissMovieRecommendation = @"http://api.trakt.tv/recommendations/movies/dismiss/" + ApiKey;
        public const string SENDDismissShowRecommendation = @"http://api.trakt.tv/recommendations/shows/dismiss/" + ApiKey;

        // Comment requests
        public const string SENDCommentEpisode = @"http://api.trakt.tv/comment/episode/" + ApiKey;
        public const string SENDCommentMovie = @"http://api.trakt.tv/comment/movie/" + ApiKey;
        public const string SENDCommentShow = @"http://api.trakt.tv/comment/show/" + ApiKey;

        //Network requests
        public const string SENDNetworkRequests = @"http://api.trakt.tv/network/requests/" + ApiKey;
        public const string SENDNetworkFollow = @"http://api.trakt.tv/network/follow/" + ApiKey;
        public const string SENDNetworkUnFollow = @"http://api.trakt.tv/network/unfollow/" + ApiKey;
        public const string SENDNetworkApprove = @"http://api.trakt.tv/network/approve/" + ApiKey;
        public const string SENDNetworkDeny = @"http://api.trakt.tv/network/deny/" + ApiKey;
        #endregion

        #region  GET Data from Trakt.tv (GET URIs)!

        // Movie requests
        public const string GETMovieSummary = @"http://api.trakt.tv/movie/summary.json/" + ApiKey + @"/{0}";
        public const string GETMoviesTrending = @"http://api.trakt.tv/movies/trending.json/" + ApiKey;
        public const string GETMovieComments = @"http://api.trakt.tv/movie/comments.json/" + ApiKey + @"/{0}";
        public const string GETMoviesRelated = @"http://api.trakt.tv/movie/related.json/" + ApiKey + @"/{0}{1}";

        // Show requests
        public const string GETShowSummary = @"http://api.trakt.tv/show/summary.json/" + ApiKey + @"/{0}";
        public const string GETShowSummaryExtended = @"http://api.trakt.tv/show/summary.json/" + ApiKey + @"/{0}/extended";
        public const string GETShowComments = @"http://api.trakt.tv/show/comments.json/" + ApiKey + @"/{0}";
        public const string GETShowsTrending = @"http://api.trakt.tv/shows/trending.json/" + ApiKey;
        public const string GETEpisodeComments = @"http://api.trakt.tv/show/episode/comments.json/" + ApiKey + @"/{0}/{1}/{2}";
        public const string GETShowsRelated = @"http://api.trakt.tv/show/related.json/" + ApiKey + @"/{0}{1}";

        // User requestshttp://api.trakt.tv/user/library/movies/watched
        public const string GETUserMoviesAll = @"http://api.trakt.tv/user/library/movies/all.json/" + ApiKey + @"/{0}{1}";
        public const string GETUserWatchedMovies = @"http://api.trakt.tv/user/library/movies/watched.json/" + ApiKey + @"/{0}";
        public const string GETUserWatchedEpisodes = @"http://api.trakt.tv/user/library/shows/watched.json/" + ApiKey + @"/{0}";
        public const string GETUserWatchedShows = @"http://api.trakt.tv/user/library/shows/watched.json/" + ApiKey + @"/{0}{1}";
        public const string GETUserMoviesCollection = @"http://api.trakt.tv/user/library/movies/collection.json/" + ApiKey + @"/{0}";
        public const string GETUserEpisodesCollection = @"http://api.trakt.tv/user/library/shows/collection.json/" + ApiKey + @"/{0}{1}";
        public const string GETUserEpisodesUnSeen = @"http://api.trakt.tv/user/library/shows/unseen.json/" + ApiKey + @"/{0}{1}";
        public const string GETUserFriends = @"http://api.trakt.tv/user/network/friends.json/" + ApiKey + @"/{0}";
        public const string GETUserFriendsExtended = @"http://api.trakt.tv/user/network/friends.json/" + ApiKey + @"/{0}/extended";
        public const string GETUserList = @"http://api.trakt.tv/user/list.json/" + ApiKey + @"/{0}/{1}";
        public const string GETUserLists = @"http://api.trakt.tv/user/lists.json/" + ApiKey + @"/{0}";
        public const string GETUserProfile = @"http://api.trakt.tv/user/profile.json/" + ApiKey + @"/{0}";
        public const string GETUserMovieWatchList = @"http://api.trakt.tv/user/watchlist/movies.json/" + ApiKey + @"/{0}";
        public const string GETUserShowsWatchList = @"http://api.trakt.tv/user/watchlist/shows.json/" + ApiKey + @"/{0}";
        public const string GETUserEpisodesWatchList = @"http://api.trakt.tv/user/watchlist/episodes.json/" + ApiKey + @"/{0}";
        public const string GETUserRatedMoviesList = @"http://api.trakt.tv/user/ratings/movies.json/" + ApiKey + @"/{0}";
        public const string GETUserRatedShowsList = @"http://api.trakt.tv/user/ratings/shows.json/" + ApiKey + @"/{0}";
        public const string GETUserRatedEpisodesList = @"http://api.trakt.tv/user/ratings/episodes.json/" + ApiKey + @"/{0}";
        public const string GETUserNetworkFriends = @"http://api.trakt.tv/user/network/friends.json/" + ApiKey + "/{0}";
        public const string GETUserFollowers = @"http://api.trakt.tv/user/network/followers.json/" + ApiKey + "/{0}";
        public const string GETUserFollowing = @"http://api.trakt.tv/user/network/following.json/" + ApiKey + "/{0}";

        //Activity requests
        public const string GETActivityUser = @"http://api.trakt.tv/activity/user.json/" + ApiKey + @"/{0}/{1}/{2}";
        public const string GETActivityFriends = @"http://api.trakt.tv/activity/friends.json/" + ApiKey + @"/{0}/{1}{2}";
        public const string GETActivityMovies = @"http://api.trakt.tv/activity/movies.json/" + ApiKey + @"/{0}/{1}{2}";
        public const string GETActivityCommunity = @"http://api.trakt.tv/activity/community.json/" + ApiKey + @"/{0}/{1}{2}";

        //Search requests
        public const string GETSearchUsers = @"http://api.trakt.tv/search/users.json/" + ApiKey + @"/{0}/{1}";
        public const string GETSearchMovies = @"http://api.trakt.tv/search/movies.json/" + ApiKey + @"/{0}/{1}";
        public const string GETSearchShows = @"http://api.trakt.tv/search/shows.json/" + ApiKey + @"/{0}/{1}";
        public const string GETSearchEpisodes = @"http://api.trakt.tv/search/episodes.json/" + ApiKey + @"/{0}/{1}";
        public const string GETSearchPeople = @"http://api.trakt.tv/search/people.json/" + ApiKey + @"/{0}/{1}";

        //Calendar requests
        public const string GETCalendarUserShows = @"http://api.trakt.tv/user/calendar/shows.json/" + ApiKey + @"/{0}/{1}/{2}";
        public const string GETCalendarPremieres = @"http://api.trakt.tv/calendar/premieres.json/" + ApiKey + @"/{0}/{1}";
        public const string GETCalendarAllShows = @"http://api.trakt.tv/calendar/shows.json/" + ApiKey + @"/{0}/{1}";
        #endregion
    }
}
