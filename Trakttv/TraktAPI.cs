using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Web;
using Trakttv.TraktAPI.Model;
using NLog;
namespace Trakttv
{

    /// <summary>
    /// Class for communication with Trakt API
    /// </summary>
    public class TrakttvAPI
    {
        public static Logger logger = NLog.LogManager.GetCurrentClassLogger();

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
            return new TraktAuthentication { Username = TraktSettings.Username, Password = TraktSettings.Password }.ToJSON();
        }

        /// <summary>
        /// Login to trakt and request a token for user for all subsequent requests
        /// </summary>
        /// <returns></returns>
        public static TraktToken Login(string loginData = null)
        {
            // clear User Token if set
            TraktSettings.Token = null;

           var response = SENDToTrakt(TraktURIs.Login, loginData ?? GetUserAuthentication(), false);
            return response.FromJSON<TraktToken>();
        }


        // Changes for v2 Trakt.tv API:
       // Since we are working with token, old transmit method could not be used anymore. 2 new methods: READFromTrakt and SENDToTrakt

        /// <summary>
        ///  GET Requests to trakt.tv API
        /// </summary>
        /// <param name="address">The URI to use</param>
        /// <param name="type">The type of request GET or DELETE, default: GET</param>
        /// <param name="oAuth">The token needed or not, default: yes</param>
        static string READFromTrakt(string address, string type = "GET", bool oAuth = true)
        
        {
            logger.Info("[READFromTrakt] Address: " + address);

            if (OnDataSend != null)
                OnDataSend(address, null);

            var request = WebRequest.Create(address) as HttpWebRequest;

            request.KeepAlive = true;
            request.Method = type;
            request.ContentLength = 0;
            request.Timeout = 120000;
            request.ContentType = "application/json";
            request.UserAgent = TraktSettings.UserAgent;

            // v2 API, add required headers for authorisation
            request.Headers.Add("trakt-api-version", "2");
            request.Headers.Add("trakt-api-key", TraktSettings.ApiKey);

            // if we want to get all data, we need oAuth
            if (oAuth)
            {
                request.Headers.Add("trakt-user-login", TraktSettings.Username ?? string.Empty);
                logger.Info("[READFromTrakt] trakt-user-login: " + TraktSettings.Username);
                request.Headers.Add("trakt-user-token", TraktSettings.Token ?? string.Empty);
                logger.Info("[READFromTrakt] trakt-user-token: " + TraktMethods.MaskSensibleString(TraktSettings.Token));  
            }
            logger.Info("[READFromTrakt] Header: " + request.Headers);
            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                logger.Info("[READFromTrakt] Waiting for response...");
                if (response == null)
                {
                   logger.Info("[READFromTrakt] Response is null");
                    return null;
                }
                Stream stream = response.GetResponseStream();
                StreamReader reader = new StreamReader(stream);
                string strResponse = reader.ReadToEnd();
                logger.Info("[READFromTrakt] Response: " + strResponse);
                if (type == "DELETE")
                {
                    strResponse = response.StatusCode.ToString();
                }

                if (OnDataReceived != null)
                    OnDataReceived(strResponse);
  
                stream.Close();
                reader.Close();
                response.Close();

                return strResponse;
            }
            catch (WebException ex)
            {  
                string errorMessage = ex.Message;
                logger.Error("[READFromTrakt] Error during Request! ", ex);
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    var response = ex.Response as HttpWebResponse;
                    errorMessage = string.Format("API error! Code = '{0}', Description = '{1}'", (int)response.StatusCode, response.StatusDescription);
                }

                if (OnDataError != null)
                    OnDataError(ex.Message);

                return null;
            }
        }

        /// <summary>
        /// POST Requests to trakt.tv API
        /// </summary>
        /// <param name="address">The URI to use</param>
        /// <param name="uploadstring">The text to post</param>
        /// <param name="oAuth">The token needed or not, default: yes</param>
        /// <param name="method">The type of request POST or PUT, default: POST</param>
        static string SENDToTrakt(string address, string uploadstring, bool oAuth = true, string method = "POST")
        
        {
            logger.Info("[SENDToTrakt] Address: " + address);
            logger.Info("[SENDToTrakt] Post: " + uploadstring);
            if (OnDataSend != null && oAuth)
                OnDataSend(address, uploadstring);

            byte[] data = new UTF8Encoding().GetBytes(uploadstring);

            var request = WebRequest.Create(address) as HttpWebRequest;
            request.KeepAlive = true;

            request.Method = method;
            request.ContentLength = data.Length;
            request.Timeout = 120000;
            request.ContentType = "application/json";
            request.UserAgent = TraktSettings.UserAgent;

            // add required headers for authorisation
            request.Headers.Add("trakt-api-version", "2");
            request.Headers.Add("trakt-api-key", TraktSettings.ApiKey);

            // if we're logging in, we don't need to add these headers
            if (!string.IsNullOrEmpty(TraktSettings.Token))
            {
                request.Headers.Add("trakt-user-login", TraktSettings.Username);
                request.Headers.Add("trakt-user-token", TraktSettings.Token);
            }

            try
            {
                // post to trakt
                Stream postStream = request.GetRequestStream();
                postStream.Write(data, 0, data.Length);

                // get the response
                var response = (HttpWebResponse)request.GetResponse();
                logger.Info("[SENDToTrakt] Waiting for response...");
                if (response == null)
                {
                    logger.Info("[SENDToTrakt] Response is null");
                    return null;
                }
                Stream responseStream = response.GetResponseStream();
                var reader = new StreamReader(responseStream);
                string strResponse = reader.ReadToEnd();
                logger.Info("[SENDToTrakt] Response: " + strResponse);
                if (OnDataReceived != null)
                    OnDataReceived(strResponse);

                // cleanup
                postStream.Close();
                responseStream.Close();
                reader.Close();
                response.Close();

                return strResponse;
            }
            catch (WebException ex)
            {
                string errorMessage = ex.Message;
                logger.Error("[SENDToTrakt] Error during Request! ", ex);
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    var response = ex.Response as HttpWebResponse;
                    errorMessage = string.Format("API error! Code = '{0}', Description = '{1}'", (int)response.StatusCode, response.StatusDescription);
                }

                if (OnDataError != null)
                    OnDataError(ex.Message);

                return null;
            }
        }


        static string ReplaceOnTrakt(string address, string postData)
        {
            return SENDToTrakt(address, postData, true, "PUT");
        }

        static bool DeleteFromTrakt(string address)
        {
            var response = READFromTrakt(address, "DELETE");
            return response != null;
        }

        //OUTDATED V1 logic, not used anymore and replaced by 2 methods (GET../SEND..) above
        /// <summary>
        ///  WebClient Logic - Communicates to and from Trakt
        /// </summary>
        /// <param name="address">The URI to use</param>
        /// <param name="data">The Data to Send</param>
        /// <returns>The response from Trakt</returns>
        //private static string Transmit(string address, string data)
        //{

        //   // address.Replace("  ", "");
        //    if (OnDataSend != null) OnDataSend(address, data);

        //    try
        //    {
        //        ServicePointManager.Expect100Continue = false;
        //        WebClient client = new WebClient();
        //        client.Encoding = Encoding.UTF8;
        //        client.Headers.Add("user-agent", TrakttvAPI.UserAgent);

        //        // wait for a response from the server
        //        string response = client.UploadString(address, data);

        //        // received data, pass it back
        //        if (OnDataReceived != null) OnDataReceived(response);
        //        return response;
        //    }
        //    catch (WebException e)
        //    {
        //        if (OnDataError != null) OnDataError(e.Message);

        //        if (e.Status == WebExceptionStatus.ProtocolError)
        //        {
        //            var response = ((HttpWebResponse)e.Response);
        //            try
        //            {
        //                using (var stream = response.GetResponseStream())
        //                {
        //                    using (var reader = new StreamReader(stream))
        //                    {
        //                        return reader.ReadToEnd();
        //                    }
        //                }
        //            }
        //            catch { }
        //        }

        //        // create a proper response object
        //        TraktResponse error = new TraktResponse
        //        {
        //            Status = "failure",
        //            Error = e.Message
        //        };
        //        // not using at moment
        //        //  throw new TraktException(error.Message);
        //        return error.ToJSON();

        //    }
        //}


        #endregion

        #region GET Collection (Movie/Episodes)

        public static IEnumerable<TraktMovieCollected> GetCollectedMovies()
        {
            var response = READFromTrakt(TraktURIs.GETCollectionMovies);

            if (response == null) return null;
            return response.FromJSONArray<TraktMovieCollected>();
        }

        public static IEnumerable<TraktEpisodeCollected> GetCollectedEpisodes()
        {
            var response = READFromTrakt(TraktURIs.GETCollectionEpisodes);

            if (response == null) return null;
            return response.FromJSONArray<TraktEpisodeCollected>();
        }

        #endregion

        #region GET Watched Movies/Episodes

        /// <summary>
        /// Returns list of watched movies
        /// </summary>
        public static IEnumerable<TraktMovieWatched> GetWatchedMovies()
        {
            var response = READFromTrakt(TraktURIs.GETWatchedMovies);

            if (response == null) return null;
            return response.FromJSONArray<TraktMovieWatched>();
        }

        /// <summary>
        /// Returns list of watched episodes
        /// </summary>
        public static IEnumerable<TraktEpisodeWatched> GetWatchedEpisodes()
        {
            var response = READFromTrakt(TraktURIs.GETWatchedEpisodes);

            if (response == null) return null;
            return response.FromJSONArray<TraktEpisodeWatched>();
        }
 
        #endregion

        #region GET List(s)

        /// <summary>
        /// Returns a list of lists created by user
        /// </summary>
        public static IEnumerable<TraktListDetail> GetUserLists(string username)
        {
            var response = READFromTrakt(string.Format(TraktURIs.GETUserLists, username));
            return response.FromJSONArray<TraktListDetail>();
        }

        /// <summary>
        /// Returns the contents of a list
        /// </summary>
        public static IEnumerable<TraktListItem> GetUserListItems(string username, string listId, string extendedInfoParams = "min")
        {
            var response = READFromTrakt(string.Format(TraktURIs.GETUserListItems, username, listId, extendedInfoParams));
            return response.FromJSONArray<TraktListItem>();
        }

       #endregion

        #region GET Ratings (only movies for now...)
        /// <summary>
        /// Returns the users rated movies
        /// </summary>
        public static IEnumerable<TraktMovieRated> GetRatedMovies()
        {
            var response = READFromTrakt(TraktURIs.GETRatedMovies);

            if (response == null) return null;
            return response.FromJSONArray<TraktMovieRated>();
        }

        #endregion

        #region GET Watchlists (Movies/TVShows/Episodes)

        /// <summary>
        /// Returns the users watchlists of movies
        /// </summary>
        /// <param name="user">The user to get</param>
        public static IEnumerable<TraktMovieWatchList> GetWatchListMovies(string username, string extendedInfoParams = "min")
        {
            var response = READFromTrakt(string.Format(TraktURIs.GETWatchlistMovies, username, extendedInfoParams));
            return response.FromJSONArray<TraktMovieWatchList>();
        }

        /// <summary>
        /// Returns the users watchlists of shows
        /// </summary>
        /// <param name="user">The user to get</param>
        public static IEnumerable<TraktShowWatchList> GetWatchListShows(string username)
        {
            var response = READFromTrakt(string.Format(TraktURIs.GETWatchlistShows, username));
            return response.FromJSONArray<TraktShowWatchList>();
        }

        /// <summary>
        /// Returns the users watchlists of episodes
        /// </summary>
        /// <param name="user">The user to get</param>
        public static IEnumerable<TraktEpisodeWatchList> GetWatchListEpisodes(string username)
        {
            var response = READFromTrakt(string.Format(TraktURIs.GETWatchlistEpisodes, username));
            return response.FromJSONArray<TraktEpisodeWatchList>();
        }

        #endregion

        #region POST User List

        /// <summary>
        /// Create new personal list on trakt
        /// </summary>
        /// <param name="user">The user to get</param>
        public static TraktListDetail CreateCustomList(TraktList list, string username)
        {
            var response = SENDToTrakt(string.Format(TraktURIs.SENDListAdd, username), list.ToJSON());
            return response.FromJSON<TraktListDetail>();
        }

        /// <summary>
        /// Updates existing list on trakt
        /// </summary>
        /// <param name="user">The user to get</param>
        public static TraktListDetail UpdateCustomList(TraktListDetail list, string username)
        {
            var response = ReplaceOnTrakt(string.Format(TraktURIs.SENDListEdit, username), list.ToJSON());
            return response.FromJSON<TraktListDetail>();
        }

        /// <summary>
        /// Add new list entries on trakt
        /// </summary>
        /// <param name="user">The user to get</param>
        public static TraktResponse AddItemsToList(string username, string id, TraktSynchronize items)
        {
            var response = SENDToTrakt(string.Format(TraktURIs.SENDListItemsAdd, username, id), items.ToJSON());
            return response.FromJSON<TraktResponse>();
        }

        /// <summary>
        /// Delete list items on trakt
        /// </summary>
        /// <param name="user">The user to get</param>
        public static TraktResponse RemoveItemsFromList(string username, string id, TraktSynchronize items)
        {
            var response = SENDToTrakt(string.Format(TraktURIs.SENDListItemsRemove, username, id), items.ToJSON());
            return response.FromJSON<TraktResponse>();
        }

        /// <summary>
        /// Delete existing list on trakt
        /// </summary>
        /// <param name="user">The user to get</param>
        public static bool DeleteUserList(string username, string listId)
        {
            return DeleteFromTrakt(string.Format(TraktURIs.SENDListDelete, username, listId));
        }

        #endregion

        #region POST Rating (only movies for now...)

        /// <summary>
        /// Rate a single movie on trakt.tv
        /// </summary>
        public static TraktResponse AddMovieToRatings(TraktSyncMovieRated movie)
        {
            var movies = new TraktSyncMoviesRated
            {
                Movies = new List<TraktSyncMovieRated>() { movie }
            };

            return AddMoviesToRatings(movies);
        }

        /// <summary>
        /// UnRate a single movie on trakt.tv
        /// </summary>
        public static TraktResponse RemoveMovieFromRatings(TraktMovie movie)
        {
            var movies = new TraktSyncMovies
            {
                Movies = new List<TraktMovie>() { new TraktMovie { Ids = movie.Ids } }
            };

            return RemoveMoviesFromRatings(movies);
        }


        /// <summary>
        /// Sends movies rate data to Trakt
        /// </summary>
        public static TraktResponse AddMoviesToRatings(TraktSyncMoviesRated movies)
        {
            var response = SENDToTrakt(TraktURIs.SENDRatingsAdd, movies.ToJSON());
            return response.FromJSON<TraktResponse>();
        }

        public static TraktResponse RemoveMoviesFromRatings(TraktSyncMovies movies)
        {
            var response = SENDToTrakt(TraktURIs.SENDRatingsRemove, movies.ToJSON());
            return response.FromJSON<TraktResponse>();
        }

        #endregion

        #region POST Watchlist

        public static TraktResponse AddMoviesToWatchlist(TraktSyncMovies movies)
        {
            var response = SENDToTrakt(TraktURIs.SENDWatchlistAdd, movies.ToJSON());
            return response.FromJSON<TraktResponse>();
        }
        public static TraktResponse AddMovieToWatchlist(TraktMovie movie)
        {
            var movies = new TraktSyncMovies
            {
                Movies = new List<TraktMovie>() { movie }
            };

            return AddMoviesToWatchlist(movies);
        }

        public static TraktResponse RemoveMoviesFromWatchlist(TraktSyncMovies movies)
        {
            var response = SENDToTrakt(TraktURIs.SENDWatchlistRemove, movies.ToJSON());
            return response.FromJSON<TraktResponse>();
        }
        public static TraktResponse RemoveMovieFromWatchlist(TraktMovie movie)
        {
            var movies = new TraktSyncMovies
            {
                Movies = new List<TraktMovie>() { movie }
            };

            return RemoveMoviesFromWatchlist(movies);
        }

        public static TraktResponse AddShowsToWatchlist(TraktSyncShows shows)
        {
            var response = SENDToTrakt(TraktURIs.SENDWatchlistAdd, shows.ToJSON());
            return response.FromJSON<TraktResponse>();
        }
        public static TraktResponse AddShowToWatchlist(TraktShow show)
        {
            var shows = new TraktSyncShows
            {
                Shows = new List<TraktShow>() { show }
            };

            return AddShowsToWatchlist(shows);
        }
        public static TraktResponse RemoveShowsFromWatchlist(TraktSyncShows shows)
        {
            var response = SENDToTrakt(TraktURIs.SENDWatchlistRemove, shows.ToJSON());
            return response.FromJSON<TraktResponse>();
        }
        public static TraktResponse RemoveShowFromWatchlist(TraktShow show)
        {
            var shows = new TraktSyncShows
            {
                Shows = new List<TraktShow>() { show }
            };

            return RemoveShowsFromWatchlist(shows);
        }
        public static TraktResponse AddEpisodesToWatchlist(TraktSyncEpisodes episodes)
        {
            var response = SENDToTrakt(TraktURIs.SENDWatchlistAdd, episodes.ToJSON());
            return response.FromJSON<TraktResponse>();
        }
        public static TraktResponse AddEpisodeToWatchlist(TraktEpisode episode)
        {
            var episodes = new TraktSyncEpisodes
            {
                Episodes = new List<TraktEpisode>() { episode }
            };

            return AddEpisodesToWatchlist(episodes);
        }
        public static TraktResponse RemoveEpisodesFromWatchlist(TraktSyncEpisodes episodes)
        {
            var response = SENDToTrakt(TraktURIs.SENDWatchlistRemove, episodes.ToJSON());
            return response.FromJSON<TraktResponse>();
        }
        public static TraktResponse RemoveEpisodeFromWatchlist(TraktEpisode episode)
        {
            var episodes = new TraktSyncEpisodes
            {
                Episodes = new List<TraktEpisode>() { episode }
            };

            return RemoveEpisodesFromWatchlist(episodes);
        }

        #endregion

        #region POST Watched History

        public static TraktResponse AddMoviesToWatchedHistory(TraktSyncMoviesWatched movies)
        {
            var response = SENDToTrakt(TraktURIs.SENDWatchedHistoryAdd, movies.ToJSON());
            return response.FromJSON<TraktResponse>();
        }

        public static TraktResponse RemoveMoviesFromWatchedHistory(TraktSyncMovies movies)
        {
            var response = SENDToTrakt(TraktURIs.SENDWatchedHistoryRemove, movies.ToJSON());
            return response.FromJSON<TraktResponse>();
        }

        public static TraktResponse AddShowsToWatchedHistory(TraktSyncShows shows)
        {
            var response = SENDToTrakt(TraktURIs.SENDWatchedHistoryAdd, shows.ToJSON());
            return response.FromJSON<TraktResponse>();
        }

        public static TraktResponse RemoveShowsFromWatchedHistory(TraktSyncShows shows)
        {
            var response = SENDToTrakt(TraktURIs.SENDWatchedHistoryRemove, shows.ToJSON());
            return response.FromJSON<TraktResponse>();
        }

        public static TraktResponse AddEpisodesToWatchedHistory(TraktSyncEpisodesWatched episodes)
        {
            var response = SENDToTrakt(TraktURIs.SENDWatchedHistoryAdd, episodes.ToJSON());
            return response.FromJSON<TraktResponse>();
        }

        public static TraktResponse RemoveEpisodesFromWatchedHistory(TraktSyncEpisodes episodes)
        {
            var response = SENDToTrakt(TraktURIs.SENDWatchedHistoryRemove, episodes.ToJSON());
            return response.FromJSON<TraktResponse>();
        }

        public static TraktResponse AddShowsToWatchedHistoryEx(TraktSyncShowsEx shows)
        {
            var response = SENDToTrakt(TraktURIs.SENDWatchedHistoryAdd, shows.ToJSON());
            return response.FromJSON<TraktResponse>();
        }

        public static TraktResponse AddShowsToWatchedHistoryEx(TraktSyncShowsWatchedEx shows)
        {
            var response = SENDToTrakt(TraktURIs.SENDWatchedHistoryAdd, shows.ToJSON());
            return response.FromJSON<TraktResponse>();
        }

        public static TraktResponse RemoveShowsFromWatchedHistoryEx(TraktSyncShowsEx shows)
        {
            var response = SENDToTrakt(TraktURIs.SENDWatchedHistoryRemove, shows.ToJSON());
            return response.FromJSON<TraktResponse>();
        }

        public static TraktResponse AddMovieToWatchedHistory(TraktSyncMovieWatched movie)
        {
            var movies = new TraktSyncMoviesWatched
            {
                Movies = new List<TraktSyncMovieWatched>() { movie }
            };

            return AddMoviesToWatchedHistory(movies);
        }

        public static TraktResponse RemoveMovieFromWatchedHistory(TraktMovie movie)
        {
            var movies = new TraktSyncMovies
            {
                Movies = new List<TraktMovie>() { movie }
            };

            return RemoveMoviesFromWatchedHistory(movies);
        }

        public static TraktResponse AddShowToWatchedHistory(TraktShow show)
        {
            var shows = new TraktSyncShows
            {
                Shows = new List<TraktShow>() { show }
            };

            return AddShowsToWatchedHistory(shows);
        }

        public static TraktResponse RemoveShowFromWatchedHistory(TraktShow show)
        {
            var shows = new TraktSyncShows
            {
                Shows = new List<TraktShow>() { show }
            };

            return RemoveShowsFromWatchedHistory(shows);
        }

        public static TraktResponse AddShowToWatchedHistoryEx(TraktSyncShowEx show)
        {
            var shows = new TraktSyncShowsEx
            {
                Shows = new List<TraktSyncShowEx>() { show }
            };

            return AddShowsToWatchedHistoryEx(shows);
        }

        public static TraktResponse RemoveShowFromWatchedHistoryEx(TraktSyncShowEx show)
        {
            var shows = new TraktSyncShowsEx
            {
                Shows = new List<TraktSyncShowEx>() { show }
            };

            return RemoveShowsFromWatchedHistoryEx(shows);
        }

        public static TraktResponse AddEpisodeToWatchedHistory(TraktSyncEpisodeWatched episode)
        {
            var episodes = new TraktSyncEpisodesWatched
            {
                Episodes = new List<TraktSyncEpisodeWatched>() { episode }
            };

            return AddEpisodesToWatchedHistory(episodes);
        }

        public static TraktResponse RemoveEpisodeFromWatchedHistory(TraktEpisode episode)
        {
            var episodes = new TraktSyncEpisodes
            {
                Episodes = new List<TraktEpisode>() { episode }
            };

            return RemoveEpisodesFromWatchedHistory(episodes);
        }

        #endregion

        #region Helper: Retrieve Show Seasons

        /// <summary>
        /// Return a list of seasons for a tv show
        /// </summary>
        /// <param name="title">The show search term, either (title-year seperate spaces with '-'), imdbid, tvdbid</param>

        public static IEnumerable<TraktSeasonSummary> GetShowSeasons(string id)
        {
            var response = READFromTrakt(string.Format(TraktURIs.ShowSeasons, id));
            return response.FromJSONArray<TraktSeasonSummary>();
        }


        /// <summary>
        /// Return a list of episodes for a tv show season
        /// </summary>
        /// <param name="title">The show search term, either (title-year seperate spaces with '-'), imdbid, tvdbid</param>
        /// <param name="season">The season, 0 for specials</param>
        public static IEnumerable<TraktEpisodeSummary> GetSeasonEpisodes(string showId, string seasonId)
        {
            var response = READFromTrakt(string.Format(TraktURIs.SeasonEpisodes, showId, seasonId));
            return response.FromJSONArray<TraktEpisodeSummary>();
        }


        #endregion


        #region TODO Missing API calls (currently not needed)


        #region GET User Data
        //public static TraktUserStatistics GetUserStatistics(string user)
        //{
        //    var response = GetFromTrakt(string.Format(TraktURIs.UserStats, user));
        //    return response.FromJSON<TraktUserStatistics>();
        //}

        //public static TraktUserSummary GetUserProfile(string user)
        //{
        //    var response = GetFromTrakt(string.Format(TraktURIs.UserProfile, user));
        //    return response.FromJSON<TraktUserSummary>();
        //}

        #endregion

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
