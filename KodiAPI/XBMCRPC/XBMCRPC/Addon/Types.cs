using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
namespace XBMCRPC.Addon
{
   public enum Types
   {
       unknown,
       [global::System.Runtime.Serialization.EnumMember(Value="xbmc.metadata.scraper.albums")]
       Xbmc_metadata_scraper_albums,
       [global::System.Runtime.Serialization.EnumMember(Value="xbmc.metadata.scraper.artists")]
       Xbmc_metadata_scraper_artists,
       [global::System.Runtime.Serialization.EnumMember(Value="xbmc.metadata.scraper.movies")]
       Xbmc_metadata_scraper_movies,
       [global::System.Runtime.Serialization.EnumMember(Value="xbmc.metadata.scraper.musicvideos")]
       Xbmc_metadata_scraper_musicvideos,
       [global::System.Runtime.Serialization.EnumMember(Value="xbmc.metadata.scraper.tvshows")]
       Xbmc_metadata_scraper_tvshows,
       [global::System.Runtime.Serialization.EnumMember(Value="xbmc.ui.screensaver")]
       Xbmc_ui_screensaver,
       [global::System.Runtime.Serialization.EnumMember(Value="xbmc.player.musicviz")]
       Xbmc_player_musicviz,
       [global::System.Runtime.Serialization.EnumMember(Value="xbmc.python.pluginsource")]
       Xbmc_python_pluginsource,
       [global::System.Runtime.Serialization.EnumMember(Value="xbmc.python.script")]
       Xbmc_python_script,
       [global::System.Runtime.Serialization.EnumMember(Value="xbmc.python.weather")]
       Xbmc_python_weather,
       [global::System.Runtime.Serialization.EnumMember(Value="xbmc.python.subtitles")]
       Xbmc_python_subtitles,
       [global::System.Runtime.Serialization.EnumMember(Value="xbmc.python.lyrics")]
       Xbmc_python_lyrics,
       [global::System.Runtime.Serialization.EnumMember(Value="xbmc.gui.skin")]
       Xbmc_gui_skin,
       [global::System.Runtime.Serialization.EnumMember(Value="xbmc.gui.webinterface")]
       Xbmc_gui_webinterface,
       [global::System.Runtime.Serialization.EnumMember(Value="xbmc.pvrclient")]
       Xbmc_pvrclient,
       [global::System.Runtime.Serialization.EnumMember(Value="xbmc.addon.video")]
       Xbmc_addon_video,
       [global::System.Runtime.Serialization.EnumMember(Value="xbmc.addon.audio")]
       Xbmc_addon_audio,
       [global::System.Runtime.Serialization.EnumMember(Value="xbmc.addon.image")]
       Xbmc_addon_image,
       [global::System.Runtime.Serialization.EnumMember(Value="xbmc.addon.executable")]
       Xbmc_addon_executable,
       [global::System.Runtime.Serialization.EnumMember(Value="xbmc.service")]
       Xbmc_service,
       [global::System.Runtime.Serialization.EnumMember(Value="xbmc.subtitle.module")]
       Xbmc_subtitle_module,
   }
}
