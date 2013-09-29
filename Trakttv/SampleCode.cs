using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trakttv.TrakttvAPI;
using Trakttv.TrakttvAPI.Model;

namespace Trakttv
{
    private class SampleCode
    {    
            // Some Sample VB CODE for getting watched movies from Trakt - calling from Ember Module!
             // "Getting movies for user";

            //        'Saving 3 values in Dictionary style
            //Dim dictMovieWatched As New Dictionary(Of String, KeyValuePair(Of String, Integer))
            //Dim traktWatchedMovies As IEnumerable(Of TraktWatchedMovie) = TrakttvAPI.GETUserWatchedMovies(txtTraktUser.Text)
            //For Each Item In traktWatchedMovies
            //    'Check if information is stored...
            //    If Not Item.Movie.Title Is Nothing AndAlso Item.Movie.Title <> "" AndAlso Not Item.Movie.IMDBID Is Nothing AndAlso Item.Movie.IMDBID <> "" Then
            //        If Not dictMovieWatched.ContainsKey(Item.Movie.Title) Then
            //            'Now store imdbid, title and playcount information into dictionary (for now no other info needed...)
            //            If Item.Movie.IMDBID.Length > 2 AndAlso Item.Movie.IMDBID.Substring(0, 2) = "tt" Then
            //                'IMDBID beginning with tt -> strip tt first and save only number!
            //                dictMovieWatched.Add(Item.Movie.IMDBID, New KeyValuePair(Of String, Integer)(Item.Movie.IMDBID.Substring(2), CInt(Item.Movie.Plays)))
            //            Else
            //                'IMDBID is alright
            //                dictMovieWatched.Add(Item.Movie.IMDBID, New KeyValuePair(Of String, Integer)(Item.Movie.IMDBID, CInt(Item.Movie.Plays)))
            //            End If
            //        End If
            //    End If
            //Next


    // Some Sample VB CODE for getting watched episode from Trakt - calling from Ember Module!
//' get all episodes on trakt that are marked as 'seen' or 'watched'
//            'Saving 3 values in Dictionary style
//            Dim dictMovieWatched As New Dictionary(Of String, KeyValuePair(Of String, List(Of Seasons)))
//            Dim traktWatchedEpisodes As IEnumerable(Of TraktLibraryShow) = TrakttvAPI.GetWatchedEpisodesForUser(txtTraktUser.Text)
//            If traktWatchedEpisodes Is Nothing = False Then
//                For Each episode In traktWatchedEpisodes
//                    If Not episode.Title Is Nothing AndAlso episode.Title <> "" AndAlso Not episode.SeriesId Is Nothing AndAlso episode.SeriesId <> "" Then
//                        If Not dictMovieWatched.ContainsKey(episode.Title) Then
//                            'Now store tvdbID, title and the season-episode-list in dictionary...
//                            dictMovieWatched.Add(episode.Title, New KeyValuePair(Of String, List(Of Seasons))(episode.SeriesId, episode.Seasons))
//                        End If
//                    End If
//                Next
//            End If


       // tvshows with watched episodes in trakt library :  traktWatchedEpisodes.Count().ToString()
    }
}
