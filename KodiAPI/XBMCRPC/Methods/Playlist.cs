using System;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
namespace XBMCRPC.Methods
{
   public partial class Playlist
   {
        private readonly Client _client;
          public Playlist(Client client)
          {
              _client = client;
          }

                /// <summary>
                /// Add item(s) to playlist
                /// </summary>
        public async Task<string> Add(XBMCRPC.Playlist.ItemFile item, int playlistid=0)
        {
            var jArgs = new JObject();
             if (playlistid != null)
             {
                 var jpropplaylistid = JToken.FromObject(playlistid, _client.Serializer);
                 jArgs.Add(new JProperty("playlistid", jpropplaylistid));
             }
             if (item != null)
             {
                 var jpropitem = JToken.FromObject(item, _client.Serializer);
                 jArgs.Add(new JProperty("item", jpropitem));
             }
            return await _client.GetData<string>("Playlist.Add", jArgs);
        }

                /// <summary>
                /// Add item(s) to playlist
                /// </summary>
        public async Task<string> Add(XBMCRPC.Playlist.Item1 item, int playlistid=0)
        {
            var jArgs = new JObject();
             if (playlistid != null)
             {
                 var jpropplaylistid = JToken.FromObject(playlistid, _client.Serializer);
                 jArgs.Add(new JProperty("playlistid", jpropplaylistid));
             }
             if (item != null)
             {
                 var jpropitem = JToken.FromObject(item, _client.Serializer);
                 jArgs.Add(new JProperty("item", jpropitem));
             }
            return await _client.GetData<string>("Playlist.Add", jArgs);
        }

                /// <summary>
                /// Add item(s) to playlist
                /// </summary>
        public async Task<string> Add(XBMCRPC.Playlist.ItemMovieid item, int playlistid=0)
        {
            var jArgs = new JObject();
             if (playlistid != null)
             {
                 var jpropplaylistid = JToken.FromObject(playlistid, _client.Serializer);
                 jArgs.Add(new JProperty("playlistid", jpropplaylistid));
             }
             if (item != null)
             {
                 var jpropitem = JToken.FromObject(item, _client.Serializer);
                 jArgs.Add(new JProperty("item", jpropitem));
             }
            return await _client.GetData<string>("Playlist.Add", jArgs);
        }

                /// <summary>
                /// Add item(s) to playlist
                /// </summary>
        public async Task<string> Add(XBMCRPC.Playlist.ItemEpisodeid item, int playlistid=0)
        {
            var jArgs = new JObject();
             if (playlistid != null)
             {
                 var jpropplaylistid = JToken.FromObject(playlistid, _client.Serializer);
                 jArgs.Add(new JProperty("playlistid", jpropplaylistid));
             }
             if (item != null)
             {
                 var jpropitem = JToken.FromObject(item, _client.Serializer);
                 jArgs.Add(new JProperty("item", jpropitem));
             }
            return await _client.GetData<string>("Playlist.Add", jArgs);
        }

                /// <summary>
                /// Add item(s) to playlist
                /// </summary>
        public async Task<string> Add(XBMCRPC.Playlist.ItemMusicvideoid item, int playlistid=0)
        {
            var jArgs = new JObject();
             if (playlistid != null)
             {
                 var jpropplaylistid = JToken.FromObject(playlistid, _client.Serializer);
                 jArgs.Add(new JProperty("playlistid", jpropplaylistid));
             }
             if (item != null)
             {
                 var jpropitem = JToken.FromObject(item, _client.Serializer);
                 jArgs.Add(new JProperty("item", jpropitem));
             }
            return await _client.GetData<string>("Playlist.Add", jArgs);
        }

                /// <summary>
                /// Add item(s) to playlist
                /// </summary>
        public async Task<string> Add(XBMCRPC.Playlist.ItemArtistid item, int playlistid=0)
        {
            var jArgs = new JObject();
             if (playlistid != null)
             {
                 var jpropplaylistid = JToken.FromObject(playlistid, _client.Serializer);
                 jArgs.Add(new JProperty("playlistid", jpropplaylistid));
             }
             if (item != null)
             {
                 var jpropitem = JToken.FromObject(item, _client.Serializer);
                 jArgs.Add(new JProperty("item", jpropitem));
             }
            return await _client.GetData<string>("Playlist.Add", jArgs);
        }

                /// <summary>
                /// Add item(s) to playlist
                /// </summary>
        public async Task<string> Add(XBMCRPC.Playlist.ItemAlbumid item, int playlistid=0)
        {
            var jArgs = new JObject();
             if (playlistid != null)
             {
                 var jpropplaylistid = JToken.FromObject(playlistid, _client.Serializer);
                 jArgs.Add(new JProperty("playlistid", jpropplaylistid));
             }
             if (item != null)
             {
                 var jpropitem = JToken.FromObject(item, _client.Serializer);
                 jArgs.Add(new JProperty("item", jpropitem));
             }
            return await _client.GetData<string>("Playlist.Add", jArgs);
        }

                /// <summary>
                /// Add item(s) to playlist
                /// </summary>
        public async Task<string> Add(XBMCRPC.Playlist.ItemSongid item, int playlistid=0)
        {
            var jArgs = new JObject();
             if (playlistid != null)
             {
                 var jpropplaylistid = JToken.FromObject(playlistid, _client.Serializer);
                 jArgs.Add(new JProperty("playlistid", jpropplaylistid));
             }
             if (item != null)
             {
                 var jpropitem = JToken.FromObject(item, _client.Serializer);
                 jArgs.Add(new JProperty("item", jpropitem));
             }
            return await _client.GetData<string>("Playlist.Add", jArgs);
        }

                /// <summary>
                /// Add item(s) to playlist
                /// </summary>
        public async Task<string> Add(XBMCRPC.Playlist.ItemGenreid item, int playlistid=0)
        {
            var jArgs = new JObject();
             if (playlistid != null)
             {
                 var jpropplaylistid = JToken.FromObject(playlistid, _client.Serializer);
                 jArgs.Add(new JProperty("playlistid", jpropplaylistid));
             }
             if (item != null)
             {
                 var jpropitem = JToken.FromObject(item, _client.Serializer);
                 jArgs.Add(new JProperty("item", jpropitem));
             }
            return await _client.GetData<string>("Playlist.Add", jArgs);
        }

                /// <summary>
                /// Add item(s) to playlist
                /// </summary>
        public async Task<string> Add(global::System.Collections.Generic.List<object> item, int playlistid=0)
        {
            var jArgs = new JObject();
             if (playlistid != null)
             {
                 var jpropplaylistid = JToken.FromObject(playlistid, _client.Serializer);
                 jArgs.Add(new JProperty("playlistid", jpropplaylistid));
             }
             if (item != null)
             {
                 var jpropitem = JToken.FromObject(item, _client.Serializer);
                 jArgs.Add(new JProperty("item", jpropitem));
             }
            return await _client.GetData<string>("Playlist.Add", jArgs);
        }

                /// <summary>
                /// Add item(s) to playlist
                /// </summary>
        public async Task<string> Add(int playlistid=0)
        {
            var jArgs = new JObject();
             if (playlistid != null)
             {
                 var jpropplaylistid = JToken.FromObject(playlistid, _client.Serializer);
                 jArgs.Add(new JProperty("playlistid", jpropplaylistid));
             }
            return await _client.GetData<string>("Playlist.Add", jArgs);
        }

                /// <summary>
                /// Clear playlist
                /// </summary>
        public async Task<string> Clear(int playlistid=0)
        {
            var jArgs = new JObject();
             if (playlistid != null)
             {
                 var jpropplaylistid = JToken.FromObject(playlistid, _client.Serializer);
                 jArgs.Add(new JProperty("playlistid", jpropplaylistid));
             }
            return await _client.GetData<string>("Playlist.Clear", jArgs);
        }

                /// <summary>
                /// Get all items from playlist
                /// </summary>
        public async Task<XBMCRPC.Playlist.GetItemsResponse> GetItems(int playlistid=0, XBMCRPC.List.Fields.All properties=null, XBMCRPC.List.Limits limits=null, XBMCRPC.List.Sort sort=null)
        {
            var jArgs = new JObject();
             if (playlistid != null)
             {
                 var jpropplaylistid = JToken.FromObject(playlistid, _client.Serializer);
                 jArgs.Add(new JProperty("playlistid", jpropplaylistid));
             }
             if (properties != null)
             {
                 var jpropproperties = JToken.FromObject(properties, _client.Serializer);
                 jArgs.Add(new JProperty("properties", jpropproperties));
             }
             if (limits != null)
             {
                 var jproplimits = JToken.FromObject(limits, _client.Serializer);
                 jArgs.Add(new JProperty("limits", jproplimits));
             }
             if (sort != null)
             {
                 var jpropsort = JToken.FromObject(sort, _client.Serializer);
                 jArgs.Add(new JProperty("sort", jpropsort));
             }
            return await _client.GetData<XBMCRPC.Playlist.GetItemsResponse>("Playlist.GetItems", jArgs);
        }

                /// <summary>
                /// Returns all existing playlists
                /// </summary>
        public async Task<global::System.Collections.Generic.List<XBMCRPC.Playlist.GetPlaylistsResponseItem>> GetPlaylists()
        {
            var jArgs = new JObject();
            return await _client.GetData<global::System.Collections.Generic.List<XBMCRPC.Playlist.GetPlaylistsResponseItem>>("Playlist.GetPlaylists", jArgs);
        }

                /// <summary>
                /// Retrieves the values of the given properties
                /// </summary>
        public async Task<XBMCRPC.Playlist.Property.Value> GetProperties(int playlistid=0, XBMCRPC.Playlist.GetProperties_properties properties=null)
        {
            var jArgs = new JObject();
             if (playlistid != null)
             {
                 var jpropplaylistid = JToken.FromObject(playlistid, _client.Serializer);
                 jArgs.Add(new JProperty("playlistid", jpropplaylistid));
             }
             if (properties != null)
             {
                 var jpropproperties = JToken.FromObject(properties, _client.Serializer);
                 jArgs.Add(new JProperty("properties", jpropproperties));
             }
            return await _client.GetData<XBMCRPC.Playlist.Property.Value>("Playlist.GetProperties", jArgs);
        }

                /// <summary>
                /// Insert item(s) into playlist. Does not work for picture playlists (aka slideshows).
                /// </summary>
        public async Task<string> Insert(XBMCRPC.Playlist.ItemFile item, int playlistid=0, int position=0)
        {
            var jArgs = new JObject();
             if (playlistid != null)
             {
                 var jpropplaylistid = JToken.FromObject(playlistid, _client.Serializer);
                 jArgs.Add(new JProperty("playlistid", jpropplaylistid));
             }
             if (position != null)
             {
                 var jpropposition = JToken.FromObject(position, _client.Serializer);
                 jArgs.Add(new JProperty("position", jpropposition));
             }
             if (item != null)
             {
                 var jpropitem = JToken.FromObject(item, _client.Serializer);
                 jArgs.Add(new JProperty("item", jpropitem));
             }
            return await _client.GetData<string>("Playlist.Insert", jArgs);
        }

                /// <summary>
                /// Insert item(s) into playlist. Does not work for picture playlists (aka slideshows).
                /// </summary>
        public async Task<string> Insert(XBMCRPC.Playlist.Item1 item, int playlistid=0, int position=0)
        {
            var jArgs = new JObject();
             if (playlistid != null)
             {
                 var jpropplaylistid = JToken.FromObject(playlistid, _client.Serializer);
                 jArgs.Add(new JProperty("playlistid", jpropplaylistid));
             }
             if (position != null)
             {
                 var jpropposition = JToken.FromObject(position, _client.Serializer);
                 jArgs.Add(new JProperty("position", jpropposition));
             }
             if (item != null)
             {
                 var jpropitem = JToken.FromObject(item, _client.Serializer);
                 jArgs.Add(new JProperty("item", jpropitem));
             }
            return await _client.GetData<string>("Playlist.Insert", jArgs);
        }

                /// <summary>
                /// Insert item(s) into playlist. Does not work for picture playlists (aka slideshows).
                /// </summary>
        public async Task<string> Insert(XBMCRPC.Playlist.ItemMovieid item, int playlistid=0, int position=0)
        {
            var jArgs = new JObject();
             if (playlistid != null)
             {
                 var jpropplaylistid = JToken.FromObject(playlistid, _client.Serializer);
                 jArgs.Add(new JProperty("playlistid", jpropplaylistid));
             }
             if (position != null)
             {
                 var jpropposition = JToken.FromObject(position, _client.Serializer);
                 jArgs.Add(new JProperty("position", jpropposition));
             }
             if (item != null)
             {
                 var jpropitem = JToken.FromObject(item, _client.Serializer);
                 jArgs.Add(new JProperty("item", jpropitem));
             }
            return await _client.GetData<string>("Playlist.Insert", jArgs);
        }

                /// <summary>
                /// Insert item(s) into playlist. Does not work for picture playlists (aka slideshows).
                /// </summary>
        public async Task<string> Insert(XBMCRPC.Playlist.ItemEpisodeid item, int playlistid=0, int position=0)
        {
            var jArgs = new JObject();
             if (playlistid != null)
             {
                 var jpropplaylistid = JToken.FromObject(playlistid, _client.Serializer);
                 jArgs.Add(new JProperty("playlistid", jpropplaylistid));
             }
             if (position != null)
             {
                 var jpropposition = JToken.FromObject(position, _client.Serializer);
                 jArgs.Add(new JProperty("position", jpropposition));
             }
             if (item != null)
             {
                 var jpropitem = JToken.FromObject(item, _client.Serializer);
                 jArgs.Add(new JProperty("item", jpropitem));
             }
            return await _client.GetData<string>("Playlist.Insert", jArgs);
        }

                /// <summary>
                /// Insert item(s) into playlist. Does not work for picture playlists (aka slideshows).
                /// </summary>
        public async Task<string> Insert(XBMCRPC.Playlist.ItemMusicvideoid item, int playlistid=0, int position=0)
        {
            var jArgs = new JObject();
             if (playlistid != null)
             {
                 var jpropplaylistid = JToken.FromObject(playlistid, _client.Serializer);
                 jArgs.Add(new JProperty("playlistid", jpropplaylistid));
             }
             if (position != null)
             {
                 var jpropposition = JToken.FromObject(position, _client.Serializer);
                 jArgs.Add(new JProperty("position", jpropposition));
             }
             if (item != null)
             {
                 var jpropitem = JToken.FromObject(item, _client.Serializer);
                 jArgs.Add(new JProperty("item", jpropitem));
             }
            return await _client.GetData<string>("Playlist.Insert", jArgs);
        }

                /// <summary>
                /// Insert item(s) into playlist. Does not work for picture playlists (aka slideshows).
                /// </summary>
        public async Task<string> Insert(XBMCRPC.Playlist.ItemArtistid item, int playlistid=0, int position=0)
        {
            var jArgs = new JObject();
             if (playlistid != null)
             {
                 var jpropplaylistid = JToken.FromObject(playlistid, _client.Serializer);
                 jArgs.Add(new JProperty("playlistid", jpropplaylistid));
             }
             if (position != null)
             {
                 var jpropposition = JToken.FromObject(position, _client.Serializer);
                 jArgs.Add(new JProperty("position", jpropposition));
             }
             if (item != null)
             {
                 var jpropitem = JToken.FromObject(item, _client.Serializer);
                 jArgs.Add(new JProperty("item", jpropitem));
             }
            return await _client.GetData<string>("Playlist.Insert", jArgs);
        }

                /// <summary>
                /// Insert item(s) into playlist. Does not work for picture playlists (aka slideshows).
                /// </summary>
        public async Task<string> Insert(XBMCRPC.Playlist.ItemAlbumid item, int playlistid=0, int position=0)
        {
            var jArgs = new JObject();
             if (playlistid != null)
             {
                 var jpropplaylistid = JToken.FromObject(playlistid, _client.Serializer);
                 jArgs.Add(new JProperty("playlistid", jpropplaylistid));
             }
             if (position != null)
             {
                 var jpropposition = JToken.FromObject(position, _client.Serializer);
                 jArgs.Add(new JProperty("position", jpropposition));
             }
             if (item != null)
             {
                 var jpropitem = JToken.FromObject(item, _client.Serializer);
                 jArgs.Add(new JProperty("item", jpropitem));
             }
            return await _client.GetData<string>("Playlist.Insert", jArgs);
        }

                /// <summary>
                /// Insert item(s) into playlist. Does not work for picture playlists (aka slideshows).
                /// </summary>
        public async Task<string> Insert(XBMCRPC.Playlist.ItemSongid item, int playlistid=0, int position=0)
        {
            var jArgs = new JObject();
             if (playlistid != null)
             {
                 var jpropplaylistid = JToken.FromObject(playlistid, _client.Serializer);
                 jArgs.Add(new JProperty("playlistid", jpropplaylistid));
             }
             if (position != null)
             {
                 var jpropposition = JToken.FromObject(position, _client.Serializer);
                 jArgs.Add(new JProperty("position", jpropposition));
             }
             if (item != null)
             {
                 var jpropitem = JToken.FromObject(item, _client.Serializer);
                 jArgs.Add(new JProperty("item", jpropitem));
             }
            return await _client.GetData<string>("Playlist.Insert", jArgs);
        }

                /// <summary>
                /// Insert item(s) into playlist. Does not work for picture playlists (aka slideshows).
                /// </summary>
        public async Task<string> Insert(XBMCRPC.Playlist.ItemGenreid item, int playlistid=0, int position=0)
        {
            var jArgs = new JObject();
             if (playlistid != null)
             {
                 var jpropplaylistid = JToken.FromObject(playlistid, _client.Serializer);
                 jArgs.Add(new JProperty("playlistid", jpropplaylistid));
             }
             if (position != null)
             {
                 var jpropposition = JToken.FromObject(position, _client.Serializer);
                 jArgs.Add(new JProperty("position", jpropposition));
             }
             if (item != null)
             {
                 var jpropitem = JToken.FromObject(item, _client.Serializer);
                 jArgs.Add(new JProperty("item", jpropitem));
             }
            return await _client.GetData<string>("Playlist.Insert", jArgs);
        }

                /// <summary>
                /// Insert item(s) into playlist. Does not work for picture playlists (aka slideshows).
                /// </summary>
        public async Task<string> Insert(global::System.Collections.Generic.List<object> item, int playlistid=0, int position=0)
        {
            var jArgs = new JObject();
             if (playlistid != null)
             {
                 var jpropplaylistid = JToken.FromObject(playlistid, _client.Serializer);
                 jArgs.Add(new JProperty("playlistid", jpropplaylistid));
             }
             if (position != null)
             {
                 var jpropposition = JToken.FromObject(position, _client.Serializer);
                 jArgs.Add(new JProperty("position", jpropposition));
             }
             if (item != null)
             {
                 var jpropitem = JToken.FromObject(item, _client.Serializer);
                 jArgs.Add(new JProperty("item", jpropitem));
             }
            return await _client.GetData<string>("Playlist.Insert", jArgs);
        }

                /// <summary>
                /// Insert item(s) into playlist. Does not work for picture playlists (aka slideshows).
                /// </summary>
        public async Task<string> Insert(int playlistid=0, int position=0)
        {
            var jArgs = new JObject();
             if (playlistid != null)
             {
                 var jpropplaylistid = JToken.FromObject(playlistid, _client.Serializer);
                 jArgs.Add(new JProperty("playlistid", jpropplaylistid));
             }
             if (position != null)
             {
                 var jpropposition = JToken.FromObject(position, _client.Serializer);
                 jArgs.Add(new JProperty("position", jpropposition));
             }
            return await _client.GetData<string>("Playlist.Insert", jArgs);
        }

                /// <summary>
                /// Remove item from playlist. Does not work for picture playlists (aka slideshows).
                /// </summary>
        public async Task<string> Remove(int playlistid=0, int position=0)
        {
            var jArgs = new JObject();
             if (playlistid != null)
             {
                 var jpropplaylistid = JToken.FromObject(playlistid, _client.Serializer);
                 jArgs.Add(new JProperty("playlistid", jpropplaylistid));
             }
             if (position != null)
             {
                 var jpropposition = JToken.FromObject(position, _client.Serializer);
                 jArgs.Add(new JProperty("position", jpropposition));
             }
            return await _client.GetData<string>("Playlist.Remove", jArgs);
        }

                /// <summary>
                /// Swap items in the playlist. Does not work for picture playlists (aka slideshows).
                /// </summary>
        public async Task<string> Swap(int playlistid=0, int position1=0, int position2=0)
        {
            var jArgs = new JObject();
             if (playlistid != null)
             {
                 var jpropplaylistid = JToken.FromObject(playlistid, _client.Serializer);
                 jArgs.Add(new JProperty("playlistid", jpropplaylistid));
             }
             if (position1 != null)
             {
                 var jpropposition1 = JToken.FromObject(position1, _client.Serializer);
                 jArgs.Add(new JProperty("position1", jpropposition1));
             }
             if (position2 != null)
             {
                 var jpropposition2 = JToken.FromObject(position2, _client.Serializer);
                 jArgs.Add(new JProperty("position2", jpropposition2));
             }
            return await _client.GetData<string>("Playlist.Swap", jArgs);
        }

        public delegate void OnAddDelegate(string sender=null, XBMCRPC.Playlist.OnAdd_data data=null);
        public event OnAddDelegate OnAdd;
        internal void RaiseOnAdd(string sender=null, XBMCRPC.Playlist.OnAdd_data data=null)
        {
            if (OnAdd != null)
            {
                OnAdd.BeginInvoke(sender, data, null, null);
            }
        }

        public delegate void OnClearDelegate(string sender=null, XBMCRPC.Playlist.OnClear_data data=null);
        public event OnClearDelegate OnClear;
        internal void RaiseOnClear(string sender=null, XBMCRPC.Playlist.OnClear_data data=null)
        {
            if (OnClear != null)
            {
                OnClear.BeginInvoke(sender, data, null, null);
            }
        }

        public delegate void OnRemoveDelegate(string sender=null, XBMCRPC.Playlist.OnRemove_data data=null);
        public event OnRemoveDelegate OnRemove;
        internal void RaiseOnRemove(string sender=null, XBMCRPC.Playlist.OnRemove_data data=null)
        {
            if (OnRemove != null)
            {
                OnRemove.BeginInvoke(sender, data, null, null);
            }
        }
   }
}
