using System;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
namespace XBMCRPC.Methods
{
   public partial class Player
   {
        private readonly Client _client;
          public Player(Client client)
          {
              _client = client;
          }

                /// <summary>
                /// Returns all active players
                /// </summary>
        public async Task<global::System.Collections.Generic.List<XBMCRPC.Player.GetActivePlayersResponseItem>> GetActivePlayers()
        {
            var jArgs = new JObject();
            return await _client.GetData<global::System.Collections.Generic.List<XBMCRPC.Player.GetActivePlayersResponseItem>>("Player.GetActivePlayers", jArgs);
        }

                /// <summary>
                /// Retrieves the currently played item
                /// </summary>
        public async Task<XBMCRPC.Player.GetItemResponse> GetItem(int playerid=0, XBMCRPC.List.Fields.All properties=null)
        {
            var jArgs = new JObject();
             if (playerid != null)
             {
                 var jpropplayerid = JToken.FromObject(playerid, _client.Serializer);
                 jArgs.Add(new JProperty("playerid", jpropplayerid));
             }
             if (properties != null)
             {
                 var jpropproperties = JToken.FromObject(properties, _client.Serializer);
                 jArgs.Add(new JProperty("properties", jpropproperties));
             }
            return await _client.GetData<XBMCRPC.Player.GetItemResponse>("Player.GetItem", jArgs);
        }

                /// <summary>
                /// Retrieves the values of the given properties
                /// </summary>
        public async Task<XBMCRPC.Player.Property.Value> GetProperties(int playerid=0, XBMCRPC.Player.GetProperties_properties properties=null)
        {
            var jArgs = new JObject();
             if (playerid != null)
             {
                 var jpropplayerid = JToken.FromObject(playerid, _client.Serializer);
                 jArgs.Add(new JProperty("playerid", jpropplayerid));
             }
             if (properties != null)
             {
                 var jpropproperties = JToken.FromObject(properties, _client.Serializer);
                 jArgs.Add(new JProperty("properties", jpropproperties));
             }
            return await _client.GetData<XBMCRPC.Player.Property.Value>("Player.GetProperties", jArgs);
        }

                /// <summary>
                /// Go to previous/next/specific item in the playlist
                /// </summary>
        public async Task<string> GoTo(XBMCRPC.Player.GoTo_to1 to, int playerid=0)
        {
            var jArgs = new JObject();
             if (playerid != null)
             {
                 var jpropplayerid = JToken.FromObject(playerid, _client.Serializer);
                 jArgs.Add(new JProperty("playerid", jpropplayerid));
             }
             if (to != null)
             {
                 var jpropto = JToken.FromObject(to, _client.Serializer);
                 jArgs.Add(new JProperty("to", jpropto));
             }
            return await _client.GetData<string>("Player.GoTo", jArgs);
        }

                /// <summary>
                /// Go to previous/next/specific item in the playlist
                /// </summary>
        public async Task<string> GoTo(int to, int playerid=0)
        {
            var jArgs = new JObject();
             if (playerid != null)
             {
                 var jpropplayerid = JToken.FromObject(playerid, _client.Serializer);
                 jArgs.Add(new JProperty("playerid", jpropplayerid));
             }
             if (to != null)
             {
                 var jpropto = JToken.FromObject(to, _client.Serializer);
                 jArgs.Add(new JProperty("to", jpropto));
             }
            return await _client.GetData<string>("Player.GoTo", jArgs);
        }

                /// <summary>
                /// Go to previous/next/specific item in the playlist
                /// </summary>
        public async Task<string> GoTo(int playerid=0)
        {
            var jArgs = new JObject();
             if (playerid != null)
             {
                 var jpropplayerid = JToken.FromObject(playerid, _client.Serializer);
                 jArgs.Add(new JProperty("playerid", jpropplayerid));
             }
            return await _client.GetData<string>("Player.GoTo", jArgs);
        }

                /// <summary>
                /// If picture is zoomed move viewport left/right/up/down otherwise skip previous/next
                /// </summary>
        public async Task<string> Move(int playerid=0, XBMCRPC.Player.Move_direction direction=0)
        {
            var jArgs = new JObject();
             if (playerid != null)
             {
                 var jpropplayerid = JToken.FromObject(playerid, _client.Serializer);
                 jArgs.Add(new JProperty("playerid", jpropplayerid));
             }
             if (direction != null)
             {
                 var jpropdirection = JToken.FromObject(direction, _client.Serializer);
                 jArgs.Add(new JProperty("direction", jpropdirection));
             }
            return await _client.GetData<string>("Player.Move", jArgs);
        }

                /// <summary>
                /// Start playback of either the playlist with the given ID, a slideshow with the pictures from the given directory or a single file or an item from the database.
                /// </summary>
        public async Task<string> Open(XBMCRPC.Player.Open_item1 item, XBMCRPC.Player.Open_options options=null)
        {
            var jArgs = new JObject();
             if (item != null)
             {
                 var jpropitem = JToken.FromObject(item, _client.Serializer);
                 jArgs.Add(new JProperty("item", jpropitem));
             }
             if (options != null)
             {
                 var jpropoptions = JToken.FromObject(options, _client.Serializer);
                 jArgs.Add(new JProperty("options", jpropoptions));
             }
            return await _client.GetData<string>("Player.Open", jArgs);
        }

                /// <summary>
                /// Start playback of either the playlist with the given ID, a slideshow with the pictures from the given directory or a single file or an item from the database.
                /// </summary>
        public async Task<string> Open(XBMCRPC.Playlist.ItemFile item, XBMCRPC.Player.Open_options options=null)
        {
            var jArgs = new JObject();
             if (item != null)
             {
                 var jpropitem = JToken.FromObject(item, _client.Serializer);
                 jArgs.Add(new JProperty("item", jpropitem));
             }
             if (options != null)
             {
                 var jpropoptions = JToken.FromObject(options, _client.Serializer);
                 jArgs.Add(new JProperty("options", jpropoptions));
             }
            return await _client.GetData<string>("Player.Open", jArgs);
        }

                /// <summary>
                /// Start playback of either the playlist with the given ID, a slideshow with the pictures from the given directory or a single file or an item from the database.
                /// </summary>
        public async Task<string> Open(XBMCRPC.Playlist.Item1 item, XBMCRPC.Player.Open_options options=null)
        {
            var jArgs = new JObject();
             if (item != null)
             {
                 var jpropitem = JToken.FromObject(item, _client.Serializer);
                 jArgs.Add(new JProperty("item", jpropitem));
             }
             if (options != null)
             {
                 var jpropoptions = JToken.FromObject(options, _client.Serializer);
                 jArgs.Add(new JProperty("options", jpropoptions));
             }
            return await _client.GetData<string>("Player.Open", jArgs);
        }

                /// <summary>
                /// Start playback of either the playlist with the given ID, a slideshow with the pictures from the given directory or a single file or an item from the database.
                /// </summary>
        public async Task<string> Open(XBMCRPC.Playlist.ItemMovieid item, XBMCRPC.Player.Open_options options=null)
        {
            var jArgs = new JObject();
             if (item != null)
             {
                 var jpropitem = JToken.FromObject(item, _client.Serializer);
                 jArgs.Add(new JProperty("item", jpropitem));
             }
             if (options != null)
             {
                 var jpropoptions = JToken.FromObject(options, _client.Serializer);
                 jArgs.Add(new JProperty("options", jpropoptions));
             }
            return await _client.GetData<string>("Player.Open", jArgs);
        }

                /// <summary>
                /// Start playback of either the playlist with the given ID, a slideshow with the pictures from the given directory or a single file or an item from the database.
                /// </summary>
        public async Task<string> Open(XBMCRPC.Playlist.ItemEpisodeid item, XBMCRPC.Player.Open_options options=null)
        {
            var jArgs = new JObject();
             if (item != null)
             {
                 var jpropitem = JToken.FromObject(item, _client.Serializer);
                 jArgs.Add(new JProperty("item", jpropitem));
             }
             if (options != null)
             {
                 var jpropoptions = JToken.FromObject(options, _client.Serializer);
                 jArgs.Add(new JProperty("options", jpropoptions));
             }
            return await _client.GetData<string>("Player.Open", jArgs);
        }

                /// <summary>
                /// Start playback of either the playlist with the given ID, a slideshow with the pictures from the given directory or a single file or an item from the database.
                /// </summary>
        public async Task<string> Open(XBMCRPC.Playlist.ItemMusicvideoid item, XBMCRPC.Player.Open_options options=null)
        {
            var jArgs = new JObject();
             if (item != null)
             {
                 var jpropitem = JToken.FromObject(item, _client.Serializer);
                 jArgs.Add(new JProperty("item", jpropitem));
             }
             if (options != null)
             {
                 var jpropoptions = JToken.FromObject(options, _client.Serializer);
                 jArgs.Add(new JProperty("options", jpropoptions));
             }
            return await _client.GetData<string>("Player.Open", jArgs);
        }

                /// <summary>
                /// Start playback of either the playlist with the given ID, a slideshow with the pictures from the given directory or a single file or an item from the database.
                /// </summary>
        public async Task<string> Open(XBMCRPC.Playlist.ItemArtistid item, XBMCRPC.Player.Open_options options=null)
        {
            var jArgs = new JObject();
             if (item != null)
             {
                 var jpropitem = JToken.FromObject(item, _client.Serializer);
                 jArgs.Add(new JProperty("item", jpropitem));
             }
             if (options != null)
             {
                 var jpropoptions = JToken.FromObject(options, _client.Serializer);
                 jArgs.Add(new JProperty("options", jpropoptions));
             }
            return await _client.GetData<string>("Player.Open", jArgs);
        }

                /// <summary>
                /// Start playback of either the playlist with the given ID, a slideshow with the pictures from the given directory or a single file or an item from the database.
                /// </summary>
        public async Task<string> Open(XBMCRPC.Playlist.ItemAlbumid item, XBMCRPC.Player.Open_options options=null)
        {
            var jArgs = new JObject();
             if (item != null)
             {
                 var jpropitem = JToken.FromObject(item, _client.Serializer);
                 jArgs.Add(new JProperty("item", jpropitem));
             }
             if (options != null)
             {
                 var jpropoptions = JToken.FromObject(options, _client.Serializer);
                 jArgs.Add(new JProperty("options", jpropoptions));
             }
            return await _client.GetData<string>("Player.Open", jArgs);
        }

                /// <summary>
                /// Start playback of either the playlist with the given ID, a slideshow with the pictures from the given directory or a single file or an item from the database.
                /// </summary>
        public async Task<string> Open(XBMCRPC.Playlist.ItemSongid item, XBMCRPC.Player.Open_options options=null)
        {
            var jArgs = new JObject();
             if (item != null)
             {
                 var jpropitem = JToken.FromObject(item, _client.Serializer);
                 jArgs.Add(new JProperty("item", jpropitem));
             }
             if (options != null)
             {
                 var jpropoptions = JToken.FromObject(options, _client.Serializer);
                 jArgs.Add(new JProperty("options", jpropoptions));
             }
            return await _client.GetData<string>("Player.Open", jArgs);
        }

                /// <summary>
                /// Start playback of either the playlist with the given ID, a slideshow with the pictures from the given directory or a single file or an item from the database.
                /// </summary>
        public async Task<string> Open(XBMCRPC.Playlist.ItemGenreid item, XBMCRPC.Player.Open_options options=null)
        {
            var jArgs = new JObject();
             if (item != null)
             {
                 var jpropitem = JToken.FromObject(item, _client.Serializer);
                 jArgs.Add(new JProperty("item", jpropitem));
             }
             if (options != null)
             {
                 var jpropoptions = JToken.FromObject(options, _client.Serializer);
                 jArgs.Add(new JProperty("options", jpropoptions));
             }
            return await _client.GetData<string>("Player.Open", jArgs);
        }

                /// <summary>
                /// Start playback of either the playlist with the given ID, a slideshow with the pictures from the given directory or a single file or an item from the database.
                /// </summary>
        public async Task<string> Open(XBMCRPC.Player.Open_item2 item, XBMCRPC.Player.Open_options options=null)
        {
            var jArgs = new JObject();
             if (item != null)
             {
                 var jpropitem = JToken.FromObject(item, _client.Serializer);
                 jArgs.Add(new JProperty("item", jpropitem));
             }
             if (options != null)
             {
                 var jpropoptions = JToken.FromObject(options, _client.Serializer);
                 jArgs.Add(new JProperty("options", jpropoptions));
             }
            return await _client.GetData<string>("Player.Open", jArgs);
        }

                /// <summary>
                /// Start playback of either the playlist with the given ID, a slideshow with the pictures from the given directory or a single file or an item from the database.
                /// </summary>
        public async Task<string> Open(XBMCRPC.Player.Open_itemPartymode item, XBMCRPC.Player.Open_options options=null)
        {
            var jArgs = new JObject();
             if (item != null)
             {
                 var jpropitem = JToken.FromObject(item, _client.Serializer);
                 jArgs.Add(new JProperty("item", jpropitem));
             }
             if (options != null)
             {
                 var jpropoptions = JToken.FromObject(options, _client.Serializer);
                 jArgs.Add(new JProperty("options", jpropoptions));
             }
            return await _client.GetData<string>("Player.Open", jArgs);
        }

                /// <summary>
                /// Start playback of either the playlist with the given ID, a slideshow with the pictures from the given directory or a single file or an item from the database.
                /// </summary>
        public async Task<string> Open(XBMCRPC.Player.Open_itemChannelid item, XBMCRPC.Player.Open_options options=null)
        {
            var jArgs = new JObject();
             if (item != null)
             {
                 var jpropitem = JToken.FromObject(item, _client.Serializer);
                 jArgs.Add(new JProperty("item", jpropitem));
             }
             if (options != null)
             {
                 var jpropoptions = JToken.FromObject(options, _client.Serializer);
                 jArgs.Add(new JProperty("options", jpropoptions));
             }
            return await _client.GetData<string>("Player.Open", jArgs);
        }

                /// <summary>
                /// Start playback of either the playlist with the given ID, a slideshow with the pictures from the given directory or a single file or an item from the database.
                /// </summary>
        public async Task<string> Open(XBMCRPC.Player.Open_options options=null)
        {
            var jArgs = new JObject();
             if (options != null)
             {
                 var jpropoptions = JToken.FromObject(options, _client.Serializer);
                 jArgs.Add(new JProperty("options", jpropoptions));
             }
            return await _client.GetData<string>("Player.Open", jArgs);
        }

                /// <summary>
                /// Pauses or unpause playback and returns the new state
                /// </summary>
        public async Task<XBMCRPC.Player.Speed> PlayPause(bool play, int playerid=0)
        {
            var jArgs = new JObject();
             if (playerid != null)
             {
                 var jpropplayerid = JToken.FromObject(playerid, _client.Serializer);
                 jArgs.Add(new JProperty("playerid", jpropplayerid));
             }
             if (play != null)
             {
                 var jpropplay = JToken.FromObject(play, _client.Serializer);
                 jArgs.Add(new JProperty("play", jpropplay));
             }
            return await _client.GetData<XBMCRPC.Player.Speed>("Player.PlayPause", jArgs);
        }

                /// <summary>
                /// Pauses or unpause playback and returns the new state
                /// </summary>
        public async Task<XBMCRPC.Player.Speed> PlayPause(XBMCRPC.Global.Toggle2 play, int playerid=0)
        {
            var jArgs = new JObject();
             if (playerid != null)
             {
                 var jpropplayerid = JToken.FromObject(playerid, _client.Serializer);
                 jArgs.Add(new JProperty("playerid", jpropplayerid));
             }
             if (play != null)
             {
                 var jpropplay = JToken.FromObject(play, _client.Serializer);
                 jArgs.Add(new JProperty("play", jpropplay));
             }
            return await _client.GetData<XBMCRPC.Player.Speed>("Player.PlayPause", jArgs);
        }

                /// <summary>
                /// Pauses or unpause playback and returns the new state
                /// </summary>
        public async Task<XBMCRPC.Player.Speed> PlayPause(int playerid=0)
        {
            var jArgs = new JObject();
             if (playerid != null)
             {
                 var jpropplayerid = JToken.FromObject(playerid, _client.Serializer);
                 jArgs.Add(new JProperty("playerid", jpropplayerid));
             }
            return await _client.GetData<XBMCRPC.Player.Speed>("Player.PlayPause", jArgs);
        }

                /// <summary>
                /// Rotates current picture
                /// </summary>
        public async Task<string> Rotate(int playerid=0, XBMCRPC.Player.Rotate_value value=0)
        {
            var jArgs = new JObject();
             if (playerid != null)
             {
                 var jpropplayerid = JToken.FromObject(playerid, _client.Serializer);
                 jArgs.Add(new JProperty("playerid", jpropplayerid));
             }
             if (value != null)
             {
                 var jpropvalue = JToken.FromObject(value, _client.Serializer);
                 jArgs.Add(new JProperty("value", jpropvalue));
             }
            return await _client.GetData<string>("Player.Rotate", jArgs);
        }

                /// <summary>
                /// Seek through the playing item
                /// </summary>
        public async Task<XBMCRPC.Player.SeekResponse> Seek(double value, int playerid=0)
        {
            var jArgs = new JObject();
             if (playerid != null)
             {
                 var jpropplayerid = JToken.FromObject(playerid, _client.Serializer);
                 jArgs.Add(new JProperty("playerid", jpropplayerid));
             }
             if (value != null)
             {
                 var jpropvalue = JToken.FromObject(value, _client.Serializer);
                 jArgs.Add(new JProperty("value", jpropvalue));
             }
            return await _client.GetData<XBMCRPC.Player.SeekResponse>("Player.Seek", jArgs);
        }

                /// <summary>
                /// Seek through the playing item
                /// </summary>
        public async Task<XBMCRPC.Player.SeekResponse> Seek(XBMCRPC.Player.Position.Time value, int playerid=0)
        {
            var jArgs = new JObject();
             if (playerid != null)
             {
                 var jpropplayerid = JToken.FromObject(playerid, _client.Serializer);
                 jArgs.Add(new JProperty("playerid", jpropplayerid));
             }
             if (value != null)
             {
                 var jpropvalue = JToken.FromObject(value, _client.Serializer);
                 jArgs.Add(new JProperty("value", jpropvalue));
             }
            return await _client.GetData<XBMCRPC.Player.SeekResponse>("Player.Seek", jArgs);
        }

                /// <summary>
                /// Seek through the playing item
                /// </summary>
        public async Task<XBMCRPC.Player.SeekResponse> Seek(XBMCRPC.Player.Seek_value1 value, int playerid=0)
        {
            var jArgs = new JObject();
             if (playerid != null)
             {
                 var jpropplayerid = JToken.FromObject(playerid, _client.Serializer);
                 jArgs.Add(new JProperty("playerid", jpropplayerid));
             }
             if (value != null)
             {
                 var jpropvalue = JToken.FromObject(value, _client.Serializer);
                 jArgs.Add(new JProperty("value", jpropvalue));
             }
            return await _client.GetData<XBMCRPC.Player.SeekResponse>("Player.Seek", jArgs);
        }

                /// <summary>
                /// Seek through the playing item
                /// </summary>
        public async Task<XBMCRPC.Player.SeekResponse> Seek(int playerid=0)
        {
            var jArgs = new JObject();
             if (playerid != null)
             {
                 var jpropplayerid = JToken.FromObject(playerid, _client.Serializer);
                 jArgs.Add(new JProperty("playerid", jpropplayerid));
             }
            return await _client.GetData<XBMCRPC.Player.SeekResponse>("Player.Seek", jArgs);
        }

                /// <summary>
                /// Set the audio stream played by the player
                /// </summary>
        public async Task<string> SetAudioStream(XBMCRPC.Player.SetAudioStream_stream1 stream, int playerid=0)
        {
            var jArgs = new JObject();
             if (playerid != null)
             {
                 var jpropplayerid = JToken.FromObject(playerid, _client.Serializer);
                 jArgs.Add(new JProperty("playerid", jpropplayerid));
             }
             if (stream != null)
             {
                 var jpropstream = JToken.FromObject(stream, _client.Serializer);
                 jArgs.Add(new JProperty("stream", jpropstream));
             }
            return await _client.GetData<string>("Player.SetAudioStream", jArgs);
        }

                /// <summary>
                /// Set the audio stream played by the player
                /// </summary>
        public async Task<string> SetAudioStream(int stream, int playerid=0)
        {
            var jArgs = new JObject();
             if (playerid != null)
             {
                 var jpropplayerid = JToken.FromObject(playerid, _client.Serializer);
                 jArgs.Add(new JProperty("playerid", jpropplayerid));
             }
             if (stream != null)
             {
                 var jpropstream = JToken.FromObject(stream, _client.Serializer);
                 jArgs.Add(new JProperty("stream", jpropstream));
             }
            return await _client.GetData<string>("Player.SetAudioStream", jArgs);
        }

                /// <summary>
                /// Set the audio stream played by the player
                /// </summary>
        public async Task<string> SetAudioStream(int playerid=0)
        {
            var jArgs = new JObject();
             if (playerid != null)
             {
                 var jpropplayerid = JToken.FromObject(playerid, _client.Serializer);
                 jArgs.Add(new JProperty("playerid", jpropplayerid));
             }
            return await _client.GetData<string>("Player.SetAudioStream", jArgs);
        }

                /// <summary>
                /// Turn partymode on or off
                /// </summary>
        public async Task<string> SetPartymode(bool partymode, int playerid=0)
        {
            var jArgs = new JObject();
             if (playerid != null)
             {
                 var jpropplayerid = JToken.FromObject(playerid, _client.Serializer);
                 jArgs.Add(new JProperty("playerid", jpropplayerid));
             }
             if (partymode != null)
             {
                 var jproppartymode = JToken.FromObject(partymode, _client.Serializer);
                 jArgs.Add(new JProperty("partymode", jproppartymode));
             }
            return await _client.GetData<string>("Player.SetPartymode", jArgs);
        }

                /// <summary>
                /// Turn partymode on or off
                /// </summary>
        public async Task<string> SetPartymode(XBMCRPC.Global.Toggle2 partymode, int playerid=0)
        {
            var jArgs = new JObject();
             if (playerid != null)
             {
                 var jpropplayerid = JToken.FromObject(playerid, _client.Serializer);
                 jArgs.Add(new JProperty("playerid", jpropplayerid));
             }
             if (partymode != null)
             {
                 var jproppartymode = JToken.FromObject(partymode, _client.Serializer);
                 jArgs.Add(new JProperty("partymode", jproppartymode));
             }
            return await _client.GetData<string>("Player.SetPartymode", jArgs);
        }

                /// <summary>
                /// Turn partymode on or off
                /// </summary>
        public async Task<string> SetPartymode(int playerid=0)
        {
            var jArgs = new JObject();
             if (playerid != null)
             {
                 var jpropplayerid = JToken.FromObject(playerid, _client.Serializer);
                 jArgs.Add(new JProperty("playerid", jpropplayerid));
             }
            return await _client.GetData<string>("Player.SetPartymode", jArgs);
        }

                /// <summary>
                /// Set the repeat mode of the player
                /// </summary>
        public async Task<string> SetRepeat(XBMCRPC.Player.Repeat repeat, int playerid=0)
        {
            var jArgs = new JObject();
             if (playerid != null)
             {
                 var jpropplayerid = JToken.FromObject(playerid, _client.Serializer);
                 jArgs.Add(new JProperty("playerid", jpropplayerid));
             }
             if (repeat != null)
             {
                 var jproprepeat = JToken.FromObject(repeat, _client.Serializer);
                 jArgs.Add(new JProperty("repeat", jproprepeat));
             }
            return await _client.GetData<string>("Player.SetRepeat", jArgs);
        }

                /// <summary>
                /// Set the repeat mode of the player
                /// </summary>
        public async Task<string> SetRepeat(XBMCRPC.Player.SetRepeat_repeat1 repeat, int playerid=0)
        {
            var jArgs = new JObject();
             if (playerid != null)
             {
                 var jpropplayerid = JToken.FromObject(playerid, _client.Serializer);
                 jArgs.Add(new JProperty("playerid", jpropplayerid));
             }
             if (repeat != null)
             {
                 var jproprepeat = JToken.FromObject(repeat, _client.Serializer);
                 jArgs.Add(new JProperty("repeat", jproprepeat));
             }
            return await _client.GetData<string>("Player.SetRepeat", jArgs);
        }

                /// <summary>
                /// Set the repeat mode of the player
                /// </summary>
        public async Task<string> SetRepeat(int playerid=0)
        {
            var jArgs = new JObject();
             if (playerid != null)
             {
                 var jpropplayerid = JToken.FromObject(playerid, _client.Serializer);
                 jArgs.Add(new JProperty("playerid", jpropplayerid));
             }
            return await _client.GetData<string>("Player.SetRepeat", jArgs);
        }

                /// <summary>
                /// Shuffle/Unshuffle items in the player
                /// </summary>
        public async Task<string> SetShuffle(bool shuffle, int playerid=0)
        {
            var jArgs = new JObject();
             if (playerid != null)
             {
                 var jpropplayerid = JToken.FromObject(playerid, _client.Serializer);
                 jArgs.Add(new JProperty("playerid", jpropplayerid));
             }
             if (shuffle != null)
             {
                 var jpropshuffle = JToken.FromObject(shuffle, _client.Serializer);
                 jArgs.Add(new JProperty("shuffle", jpropshuffle));
             }
            return await _client.GetData<string>("Player.SetShuffle", jArgs);
        }

                /// <summary>
                /// Shuffle/Unshuffle items in the player
                /// </summary>
        public async Task<string> SetShuffle(XBMCRPC.Global.Toggle2 shuffle, int playerid=0)
        {
            var jArgs = new JObject();
             if (playerid != null)
             {
                 var jpropplayerid = JToken.FromObject(playerid, _client.Serializer);
                 jArgs.Add(new JProperty("playerid", jpropplayerid));
             }
             if (shuffle != null)
             {
                 var jpropshuffle = JToken.FromObject(shuffle, _client.Serializer);
                 jArgs.Add(new JProperty("shuffle", jpropshuffle));
             }
            return await _client.GetData<string>("Player.SetShuffle", jArgs);
        }

                /// <summary>
                /// Shuffle/Unshuffle items in the player
                /// </summary>
        public async Task<string> SetShuffle(int playerid=0)
        {
            var jArgs = new JObject();
             if (playerid != null)
             {
                 var jpropplayerid = JToken.FromObject(playerid, _client.Serializer);
                 jArgs.Add(new JProperty("playerid", jpropplayerid));
             }
            return await _client.GetData<string>("Player.SetShuffle", jArgs);
        }

                /// <summary>
                /// Set the speed of the current playback
                /// </summary>
        public async Task<XBMCRPC.Player.Speed> SetSpeed(int speed, int playerid=0)
        {
            var jArgs = new JObject();
             if (playerid != null)
             {
                 var jpropplayerid = JToken.FromObject(playerid, _client.Serializer);
                 jArgs.Add(new JProperty("playerid", jpropplayerid));
             }
             if (speed != null)
             {
                 var jpropspeed = JToken.FromObject(speed, _client.Serializer);
                 jArgs.Add(new JProperty("speed", jpropspeed));
             }
            return await _client.GetData<XBMCRPC.Player.Speed>("Player.SetSpeed", jArgs);
        }

                /// <summary>
                /// Set the speed of the current playback
                /// </summary>
        public async Task<XBMCRPC.Player.Speed> SetSpeed(XBMCRPC.Global.IncrementDecrement speed, int playerid=0)
        {
            var jArgs = new JObject();
             if (playerid != null)
             {
                 var jpropplayerid = JToken.FromObject(playerid, _client.Serializer);
                 jArgs.Add(new JProperty("playerid", jpropplayerid));
             }
             if (speed != null)
             {
                 var jpropspeed = JToken.FromObject(speed, _client.Serializer);
                 jArgs.Add(new JProperty("speed", jpropspeed));
             }
            return await _client.GetData<XBMCRPC.Player.Speed>("Player.SetSpeed", jArgs);
        }

                /// <summary>
                /// Set the speed of the current playback
                /// </summary>
        public async Task<XBMCRPC.Player.Speed> SetSpeed(int playerid=0)
        {
            var jArgs = new JObject();
             if (playerid != null)
             {
                 var jpropplayerid = JToken.FromObject(playerid, _client.Serializer);
                 jArgs.Add(new JProperty("playerid", jpropplayerid));
             }
            return await _client.GetData<XBMCRPC.Player.Speed>("Player.SetSpeed", jArgs);
        }

                /// <summary>
                /// Set the subtitle displayed by the player
                /// </summary>
        public async Task<string> SetSubtitle(XBMCRPC.Player.SetSubtitle_subtitle1 subtitle, int playerid=0, bool enable=false)
        {
            var jArgs = new JObject();
             if (playerid != null)
             {
                 var jpropplayerid = JToken.FromObject(playerid, _client.Serializer);
                 jArgs.Add(new JProperty("playerid", jpropplayerid));
             }
             if (subtitle != null)
             {
                 var jpropsubtitle = JToken.FromObject(subtitle, _client.Serializer);
                 jArgs.Add(new JProperty("subtitle", jpropsubtitle));
             }
             if (enable != null)
             {
                 var jpropenable = JToken.FromObject(enable, _client.Serializer);
                 jArgs.Add(new JProperty("enable", jpropenable));
             }
            return await _client.GetData<string>("Player.SetSubtitle", jArgs);
        }

                /// <summary>
                /// Set the subtitle displayed by the player
                /// </summary>
        public async Task<string> SetSubtitle(int subtitle, int playerid=0, bool enable=false)
        {
            var jArgs = new JObject();
             if (playerid != null)
             {
                 var jpropplayerid = JToken.FromObject(playerid, _client.Serializer);
                 jArgs.Add(new JProperty("playerid", jpropplayerid));
             }
             if (subtitle != null)
             {
                 var jpropsubtitle = JToken.FromObject(subtitle, _client.Serializer);
                 jArgs.Add(new JProperty("subtitle", jpropsubtitle));
             }
             if (enable != null)
             {
                 var jpropenable = JToken.FromObject(enable, _client.Serializer);
                 jArgs.Add(new JProperty("enable", jpropenable));
             }
            return await _client.GetData<string>("Player.SetSubtitle", jArgs);
        }

                /// <summary>
                /// Set the subtitle displayed by the player
                /// </summary>
        public async Task<string> SetSubtitle(int playerid=0, bool enable=false)
        {
            var jArgs = new JObject();
             if (playerid != null)
             {
                 var jpropplayerid = JToken.FromObject(playerid, _client.Serializer);
                 jArgs.Add(new JProperty("playerid", jpropplayerid));
             }
             if (enable != null)
             {
                 var jpropenable = JToken.FromObject(enable, _client.Serializer);
                 jArgs.Add(new JProperty("enable", jpropenable));
             }
            return await _client.GetData<string>("Player.SetSubtitle", jArgs);
        }

                /// <summary>
                /// Stops playback
                /// </summary>
        public async Task<string> Stop(int playerid=0)
        {
            var jArgs = new JObject();
             if (playerid != null)
             {
                 var jpropplayerid = JToken.FromObject(playerid, _client.Serializer);
                 jArgs.Add(new JProperty("playerid", jpropplayerid));
             }
            return await _client.GetData<string>("Player.Stop", jArgs);
        }

                /// <summary>
                /// Zoom current picture
                /// </summary>
        public async Task<string> Zoom(XBMCRPC.Player.Zoom_zoom1 zoom, int playerid=0)
        {
            var jArgs = new JObject();
             if (playerid != null)
             {
                 var jpropplayerid = JToken.FromObject(playerid, _client.Serializer);
                 jArgs.Add(new JProperty("playerid", jpropplayerid));
             }
             if (zoom != null)
             {
                 var jpropzoom = JToken.FromObject(zoom, _client.Serializer);
                 jArgs.Add(new JProperty("zoom", jpropzoom));
             }
            return await _client.GetData<string>("Player.Zoom", jArgs);
        }

                /// <summary>
                /// Zoom current picture
                /// </summary>
        public async Task<string> Zoom(int zoom, int playerid=0)
        {
            var jArgs = new JObject();
             if (playerid != null)
             {
                 var jpropplayerid = JToken.FromObject(playerid, _client.Serializer);
                 jArgs.Add(new JProperty("playerid", jpropplayerid));
             }
             if (zoom != null)
             {
                 var jpropzoom = JToken.FromObject(zoom, _client.Serializer);
                 jArgs.Add(new JProperty("zoom", jpropzoom));
             }
            return await _client.GetData<string>("Player.Zoom", jArgs);
        }

                /// <summary>
                /// Zoom current picture
                /// </summary>
        public async Task<string> Zoom(int playerid=0)
        {
            var jArgs = new JObject();
             if (playerid != null)
             {
                 var jpropplayerid = JToken.FromObject(playerid, _client.Serializer);
                 jArgs.Add(new JProperty("playerid", jpropplayerid));
             }
            return await _client.GetData<string>("Player.Zoom", jArgs);
        }

        public delegate void OnPauseDelegate(string sender=null, XBMCRPC.Player.Notifications.Data data=null);
        public event OnPauseDelegate OnPause;
        internal void RaiseOnPause(string sender=null, XBMCRPC.Player.Notifications.Data data=null)
        {
            if (OnPause != null)
            {
                OnPause.BeginInvoke(sender, data, null, null);
            }
        }

        public delegate void OnPlayDelegate(string sender=null, XBMCRPC.Player.Notifications.Data data=null);
        public event OnPlayDelegate OnPlay;
        internal void RaiseOnPlay(string sender=null, XBMCRPC.Player.Notifications.Data data=null)
        {
            if (OnPlay != null)
            {
                OnPlay.BeginInvoke(sender, data, null, null);
            }
        }

        public delegate void OnPropertyChangedDelegate(string sender=null, XBMCRPC.Player.OnPropertyChanged_data data=null);
        public event OnPropertyChangedDelegate OnPropertyChanged;
        internal void RaiseOnPropertyChanged(string sender=null, XBMCRPC.Player.OnPropertyChanged_data data=null)
        {
            if (OnPropertyChanged != null)
            {
                OnPropertyChanged.BeginInvoke(sender, data, null, null);
            }
        }

        public delegate void OnSeekDelegate(string sender=null, XBMCRPC.Player.OnSeek_data data=null);
        public event OnSeekDelegate OnSeek;
        internal void RaiseOnSeek(string sender=null, XBMCRPC.Player.OnSeek_data data=null)
        {
            if (OnSeek != null)
            {
                OnSeek.BeginInvoke(sender, data, null, null);
            }
        }

        public delegate void OnSpeedChangedDelegate(string sender=null, XBMCRPC.Player.Notifications.Data data=null);
        public event OnSpeedChangedDelegate OnSpeedChanged;
        internal void RaiseOnSpeedChanged(string sender=null, XBMCRPC.Player.Notifications.Data data=null)
        {
            if (OnSpeedChanged != null)
            {
                OnSpeedChanged.BeginInvoke(sender, data, null, null);
            }
        }

        public delegate void OnStopDelegate(string sender=null, XBMCRPC.Player.OnStop_data data=null);
        public event OnStopDelegate OnStop;
        internal void RaiseOnStop(string sender=null, XBMCRPC.Player.OnStop_data data=null)
        {
            if (OnStop != null)
            {
                OnStop.BeginInvoke(sender, data, null, null);
            }
        }
   }
}
