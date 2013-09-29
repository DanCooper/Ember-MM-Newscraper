using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Web;
using Trakttv.TraktAPI.Model;

namespace Trakttv.TraktAPI
{
    #region Enumerables - used for building URIs!

    /// <summary>
    /// List of Item Types
    /// </summary>
    public enum TraktItemType
    {
        episode,
        season,
        show,
        movie
    }

    /// <summary>
    /// Privacy Level for Lists
    /// </summary>
    public enum PrivacyLevel
    {
        Public,
        Private,
        Friends
    }

    /// <summary>
    /// List of Scrobble States
    /// </summary>
    public enum TraktScrobbleStates
    {
        watching,
        scrobble,
        cancelwatching
    }

    /// <summary>
    /// List of Transmission Modes
    /// </summary>
    public enum TraktTransmissionMode
    {
        library,
        seen,
        unlibrary,
        unseen,
        watchlist,
        unwatchlist
    }
    #endregion

    /// <summary>
    /// Class for communication with Trakt API
    /// </summary>
    public class TrakttvAPI
    {
        #region Settings
        // those settings are set in Trakt.Settings class
        public static string ApiKey { get; set; }
        public static string Username { get; set; }
        public static string Password { get; set; }
        public static string UserAgent { get; set; }
        #endregion

        #region Communication Eventhandler
        // these events are used for handling data transfer between trakt <-> client
        internal delegate void OnDataSendDelegate(string url, string postData);
        internal delegate void OnDataReceivedDelegate(string response);
        internal delegate void OnDataErrorDelegate(string error);

        internal static event OnDataSendDelegate OnDataSend;
        internal static event OnDataReceivedDelegate OnDataReceived;
        internal static event OnDataErrorDelegate OnDataError;
        #endregion

        #region Communication Client <-> Trakttv Webservice

        /// <summary>
        /// Gets a User Authentication object
        /// </summary>       
        /// <returns>The User Authentication json string</returns>
        private static string GetUserAuthentication()
        {
            return new TraktAuthentication { Username = TrakttvAPI.Username, Password = TrakttvAPI.Password }.ToJSON();
        }

        /// <summary>
        ///  WebClient Logic - Communicates to and from Trakt
        /// </summary>
        /// <param name="address">The URI to use</param>
        /// <param name="data">The Data to Send</param>
        /// <returns>The response from Trakt</returns>
        private static string Transmit(string address, string data)
        {

           // address.Replace("  ", "");
            if (OnDataSend != null) OnDataSend(address, data);

            try
            {
                ServicePointManager.Expect100Continue = false;
                WebClient client = new WebClient();
                client.Encoding = Encoding.UTF8;
                client.Headers.Add("user-agent", TrakttvAPI.UserAgent);

                // wait for a response from the server
                string response = client.UploadString(address, data);

                // received data, pass it back
                if (OnDataReceived != null) OnDataReceived(response);
                return response;
            }
            catch (WebException e)
            {
                if (OnDataError != null) OnDataError(e.Message);

                if (e.Status == WebExceptionStatus.ProtocolError)
                {
                    var response = ((HttpWebResponse)e.Response);
                    try
                    {
                        using (var stream = response.GetResponseStream())
                        {
                            using (var reader = new StreamReader(stream))
                            {
                                return reader.ReadToEnd();
                            }
                        }
                    }
                    catch { }
                }

                // create a proper response object
                TraktResponse error = new TraktResponse
                {
                    Status = "failure",
                    Error = e.Message
                };
                // not using at moment
                //  throw new TraktException(error.Message);
                return error.ToJSON();
            
            }
        }

        #endregion


        #region GET User Movie data

        /// <summary>
        /// Returns list of watched movies by a user
        /// </summary>
        /// <param name="user">The user to get</param>
        public static IEnumerable<TraktWatchedMovie> GetUserWatchedMovies(string user)
        {
            string watchedMovies = Transmit(string.Format(TraktURIs.GETUserWatchedMovies, user), GetUserAuthentication());
            TraktResponse response = watchedMovies.FromJSON<TraktResponse>();
            if (response == null || response.Error != null) return null;
            return watchedMovies.FromJSONArray<TraktWatchedMovie>();
        }

        /// <summary>
        /// Gets all movies for a user from trakt
        /// </summary>
        /// <param name="user">The user to get</param>
        /// <returns>The trakt movie library</returns>
        public static IEnumerable<TraktLibraryMovies> GetAllMoviesForUser(string user)
        {
            //Getting user's movies from trakt
            string moviesForUser = Transmit(string.Format(TraktURIs.GETUserMoviesAll, user), GetUserAuthentication());
            TraktResponse response = moviesForUser.FromJSON<TraktResponse>();
            if (response == null || response.Error != null) return null;
            return moviesForUser.FromJSONArray<TraktLibraryMovies>();
        }

        /// <summary>
        /// Gets the trakt movie library for a user
        /// </summary>
        /// <param name="user">The user to get</param>
        /// <returns>The trakt movie library</returns>
        public static IEnumerable<TraktLibraryMovies> GetMovieCollectionForUser(string user)
        {
            //Get the movie collection from trakt
            string moviesForUser = Transmit(string.Format(TraktURIs.GETUserMoviesCollection, user), GetUserAuthentication());
            TraktResponse response = moviesForUser.FromJSON<TraktResponse>();
            if (response == null || response.Error != null) return null;
            return moviesForUser.FromJSONArray<TraktLibraryMovies>();
        }

        #endregion

        #region GET User Show/episode data

        /// <summary>
        /// Returns list of watched episodes by a user
        /// </summary>
        /// <param name="user">The user to get</param>
        public static IEnumerable<TraktWatchedEpisode> GetUserWatchedEpisodes(string user)
        {
            string watchedEpisodes = Transmit(string.Format(TraktURIs.GETUserWatchedEpisodes, user), GetUserAuthentication());
            TraktResponse response = watchedEpisodes.FromJSON<TraktResponse>();
            if (response == null || response.Error != null) return null;
            return watchedEpisodes.FromJSONArray<TraktWatchedEpisode>();
        }

        /// <summary>
        /// Gets the trakt watched/seen episodes for a user
        /// </summary>
        /// <param name="user">The user to get</param>

        /// <returns>The trakt episode library</returns>
        public static IEnumerable<TraktLibraryShow> GetWatchedEpisodesForUser(string user)
        {
            // Getting user's 'watched/seen' episodes from trakt
            string showsForUser = Transmit(string.Format(TraktURIs.GETUserWatchedShows, user), GetUserAuthentication());
            TraktResponse response = showsForUser.FromJSON<TraktResponse>();
            if (response == null || response.Error != null) return null;
            return showsForUser.FromJSONArray<TraktLibraryShow>();
        }

        /// <summary>
        /// Gets the trakt episode library for a user
        /// </summary>
        /// <param name="user">The user to get</param>
        /// <returns>The trakt episode library</returns>
        public static IEnumerable<TraktLibraryShow> GetLibraryEpisodesForUser(string user)
        {
            // Getting user's 'library' episodes from trakt
            string showsForUser = Transmit(string.Format(TraktURIs.GETUserEpisodesCollection, user), GetUserAuthentication());
          
            // if we timeout we will return an error response
            TraktResponse response = showsForUser.FromJSON<TraktResponse>();
            if (response == null || response.Error != null) return null;
            return showsForUser.FromJSONArray<TraktLibraryShow>();
        }

        public static IEnumerable<TraktLibraryShow> GetUnSeenEpisodesForUser(string user)
        { 
            // Getting user's 'unseen' episodes from trakt
            string showsForUser = Transmit(string.Format(TraktURIs.GETUserEpisodesUnSeen, user), GetUserAuthentication());
            TraktResponse response = showsForUser.FromJSON<TraktResponse>();
            if (response == null || response.Error != null) return null;
            return showsForUser.FromJSONArray<TraktLibraryShow>();
        }

        #endregion
    
        #region GET User List(s)

        /// <summary>
        /// Returns a list of lists created by user
        /// </summary>
        /// <param name="user">The user to get</param>
        public static IEnumerable<TraktUserList> GetUserLists(string user)
        {
            string GETUserLists = Transmit(string.Format(TraktURIs.GETUserLists, user), GetUserAuthentication());
            return GETUserLists.FromJSONArray<TraktUserList>();
        }

        /// <summary>
        /// Returns the contents of a lists for a user
        /// </summary>
        /// <param name="user">The user to get</param>
        /// <param name="listname">listname (id) of list item e.g. "star-wars-collection"</param>
        public static TraktUserList GetUserList(string user, string listname)
        {
            string GETUserList = Transmit(string.Format(TraktURIs.GETUserList, user, listname), GetUserAuthentication());
            return GETUserList.FromJSON<TraktUserList>();
        }
       #endregion

        #region GET User Ratings
        /// <summary>
        /// Returns the users Rated Movies
        /// </summary>
        /// <param name="user">The user to get</param>
        public static IEnumerable<TraktUserMovieRating> GetUserRatedMovies(string user)
        {
            string ratedMovies = Transmit(string.Format(TraktURIs.GETUserRatedMoviesList, user), GetUserAuthentication());
            TraktResponse response = ratedMovies.FromJSON<TraktResponse>();
            if (response == null || response.Error != null) return null;

            return ratedMovies.FromJSONArray<TraktUserMovieRating>();
        }

        /// <summary>
        /// Returns the users Rated Shows
        /// </summary>
        /// <param name="user">The user to get</param>
        public static IEnumerable<TraktUserShowRating> GetUserRatedShows(string user)
        {
            string ratedShows = Transmit(string.Format(TraktURIs.GETUserRatedShowsList, user), GetUserAuthentication());
            TraktResponse response = ratedShows.FromJSON<TraktResponse>();
            if (response == null || response.Error != null) return null;

            return ratedShows.FromJSONArray<TraktUserShowRating>();
        }

        /// <summary>
        /// Returns the users Rated Episodes
        /// </summary>
        /// <param name="user">The user to get</param>
        public static IEnumerable<TraktUserEpisodeRating> GetUserRatedEpisodes(string user)
        {
            string ratedEpisodes = Transmit(string.Format(TraktURIs.GETUserRatedEpisodesList, user), GetUserAuthentication());
            TraktResponse response = ratedEpisodes.FromJSON<TraktResponse>();
            if (response == null || response.Error != null) return null;

            return ratedEpisodes.FromJSONArray<TraktUserEpisodeRating>();
        }

        #endregion

        #region GET Watchlists

        /// <summary>
        /// Returns the users watchlists of movies
        /// </summary>
        /// <param name="user">The user to get</param>
        public static IEnumerable<TraktWatchListMovie> GetWatchListMovies(string user)
        {
            string response = Transmit(string.Format(TraktURIs.GETUserMovieWatchList, user), GetUserAuthentication());
            return response.FromJSONArray<TraktWatchListMovie>();
        }

        /// <summary>
        /// Returns the users watchlists of shows
        /// </summary>
        /// <param name="user">The user to get</param>
        public static IEnumerable<TraktWatchListShow> GetWatchListShows(string user)
        {
            string response = Transmit(string.Format(TraktURIs.GETUserShowsWatchList, user), GetUserAuthentication());
            return response.FromJSONArray<TraktWatchListShow>();
        }

        /// <summary>
        /// Returns the users watchlists of episodes
        /// </summary>
        /// <param name="user">The user to get</param>
        public static IEnumerable<TraktWatchListEpisode> GetWatchListEpisodes(string user)
        {
            string response = Transmit(string.Format(TraktURIs.GETUserEpisodesWatchList, user), GetUserAuthentication());
            return response.FromJSONArray<TraktWatchListEpisode>();
        }

        #endregion

        #region POST User List

        /// <summary>
        /// Create new personal list on trakt
        /// </summary>
        /// <param name="user">The user to get</param>
        /// <returns>The response from Trakt</returns>
        public static TraktAddListResponse SendListAdd(TraktList list)
        {
            string response = Transmit(TraktURIs.SENDListAdd, list.ToJSON());
            return response.FromJSON<TraktAddListResponse>();
        }

        /// <summary>
        /// Delete existing list on trakt
        /// </summary>
        /// <param name="user">The user to get</param>
        /// <returns>The response from Trakt</returns>    
        public static TraktResponse SendListDelete(TraktList list)
        {
            string response = Transmit(TraktURIs.SENDListDelete, list.ToJSON());
            return response.FromJSON<TraktResponse>();
        }

        /// <summary>
        /// Updates existing list on trakt
        /// </summary>
        /// <param name="user">The user to get</param>
        /// <returns>The response from Trakt</returns>
        public static TraktResponse SendListUpdate(TraktList list)
        {
            string response = Transmit(TraktURIs.SENDListUpdate, list.ToJSON());
            return response.FromJSON<TraktResponse>();
        }

        /// <summary>
        /// Add new list entries on trakt
        /// </summary>
        /// <param name="user">The user to get</param>
        /// <returns>The response from Trakt</returns>
        public static TraktSyncResponse SendListAddItems(TraktList list)
        {
            string response = Transmit(TraktURIs.SENDListItemsAdd, list.ToJSON());
            return response.FromJSON<TraktSyncResponse>();
        }

        /// <summary>
        /// Delete list items on trakt
        /// </summary>
        /// <param name="user">The user to get</param>
        /// <returns>The response from Trakt</returns>
        public static TraktSyncResponse SendListDeleteItems(TraktList list)
        {
            string response = Transmit(TraktURIs.SENDListItemsDelete, list.ToJSON());
            return response.FromJSON<TraktSyncResponse>();
        }

        #endregion

        #region POST Rating

        /// <summary>
        /// Sends episode rate data to Trakt
        /// </summary>
        /// <param name="episode">The Trakt rate data to Send</param>
        /// <returns>The response from Trakt</returns>
        public static TraktRateResponse SendRatingEpisode(TraktRatingEpisode episode)
        {
            if (episode == null) return null;
            string response = Transmit(string.Format(TraktURIs.SENDRating, TraktItemType.episode.ToString()), episode.ToJSON());
            return response.FromJSON<TraktRateResponse>();
        }

        /// <summary>
        /// Sends episodes rate data to Trakt
        /// </summary>
        /// <param name="episode">The Trakt rate data to Send</param>
        /// <returns>The response from Trakt</returns>
        public static TraktRateResponse SendRatingEpisodes(TraktRatingEpisodes episodes)
        {
            if (episodes == null) return null;
            string response = Transmit(TraktURIs.SENDRatingEpisodes, episodes.ToJSON());
            return response.FromJSON<TraktRateResponse>();
        }

        /// <summary>
        /// Sends series rate data to Trakt
        /// </summary>
        /// <param name="episode">The Trakt rate data to Send</param>
        /// <returns>The response from Trakt</returns>
        public static TraktRateResponse SendRateSeries(TraktRateSeries series)
        {
            if (series == null) return null;
            string response = Transmit(string.Format(TraktURIs.SENDRating, TraktItemType.show.ToString()), series.ToJSON());
            return response.FromJSON<TraktRateResponse>();
        }

        /// <summary>
        /// Sends multiple series rate data to Trakt
        /// </summary>
        /// <param name="episode">The Trakt rate data to Send</param>
        /// <returns>The response from Trakt</returns>
        public static TraktRateResponse SendRateSeries(TraktSENDRatingShows shows)
        {
            if (shows == null) return null;
            string response = Transmit(TraktURIs.SENDRatingShows, shows.ToJSON());
            return response.FromJSON<TraktRateResponse>();
        }

        /// <summary>
        /// Sends movie rate data to Trakt
        /// </summary>
        /// <param name="episode">The Trakt rate data to Send</param>
        /// <returns>The response from Trakt</returns>
        public static TraktRateResponse SendRatingMovie(TraktRatingMovie movie)
        {
            if (movie == null) return null;
            string response = Transmit(string.Format(TraktURIs.SENDRating, TraktItemType.movie.ToString()), movie.ToJSON());
            return response.FromJSON<TraktRateResponse>();
        }

        /// <summary>
        /// Sends movies rate data to Trakt
        /// </summary>
        /// <param name="episode">The Trakt rate data to Send</param>
        /// <returns>The response from Trakt</returns>
        public static TraktRateResponse SendRatingMovies(TraktRatingMovies movies)
        {
            if (movies == null) return null;
            string response = Transmit(TraktURIs.SENDRatingMovies, movies.ToJSON());
            return response.FromJSON<TraktRateResponse>();
        }
        #endregion

        #region Helper: Retrieve Show Seasons

        /// <summary>
        /// Return a list of seasons for a tv show
        /// </summary>
        /// <param name="title">The show search term, either (title-year seperate spaces with '-'), imdbid, tvdbid</param>
        public static IEnumerable<TraktShowSeason> GetShowSeasons(string title)
        {
            string response = Transmit(string.Format(TraktURIs.SENDShowSeasons, title), string.Empty);
            return response.FromJSONArray<TraktShowSeason>();
        }

        /// <summary>
        /// Return a list of episodes for a tv show season
        /// </summary>
        /// <param name="title">The show search term, either (title-year seperate spaces with '-'), imdbid, tvdbid</param>
        /// <param name="season">The season, 0 for specials</param>
        public static IEnumerable<TraktEpisode> GetSeasonEpisodes(string title, string season)
        {
            string response = Transmit(string.Format(TraktURIs.SENDSeasonEpisodes, title, season), GetUserAuthentication());
            return response.FromJSONArray<TraktEpisode>();
        }

        #endregion

        #region Helper: Add / Remove List Items

        public static void AddRemoveItemInList(string listname, TraktListItem item, bool remove)
        {
            AddRemoveItemInList(new List<string> { listname }, new List<TraktListItem>() { item }, remove);
        }

        internal static void AddRemoveItemInList(List<string> listnames, TraktListItem item, bool remove)
        {
            AddRemoveItemInList(listnames, new List<TraktListItem>() { item }, remove);
        }

        internal static void AddRemoveItemInList(List<string> listnames, List<TraktListItem> items, bool remove)
        {
            Thread listThread = new Thread(delegate(object obj)
            {
                foreach (var listname in listnames)
                {
                    TraktList list = new TraktList
                    {
                        UserName = TraktSettings.Username,
                        Password = TraktSettings.Password,
                        Slug = listname,
                        Items = items
                    };
                    TraktSyncResponse response = null;
                    if (!remove)
                        response = SendListAddItems(list);
                    else
                        response = SendListDeleteItems(list);

                    if (response.Status == "success")
                    {
                      // all fine!
                    }
                }
            })
            {
                Name = remove ? "RemoveList" : "AddList",
                IsBackground = true
            };

            listThread.Start();
        }

        #endregion

        #region TODO Missing API calls (currently not needed)

        #region Friends / Network
        #endregion

        #region Trending
        #endregion

        #region Recommendation
        #endregion

        #region Summary
        #endregion

        #region Comments
        #endregion

        #region Search
        #endregion

        #region Activity
        #endregion

        #region Related
        #endregion

        #region Syncing
        #endregion

        #endregion

    }
}
