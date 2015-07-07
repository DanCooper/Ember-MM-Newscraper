using System;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
namespace XBMCRPC.Methods
{
   public partial class Application
   {
        private readonly Client _client;
          public Application(Client client)
          {
              _client = client;
          }

                /// <summary>
                /// Retrieves the values of the given properties
                /// </summary>
        public async Task<XBMCRPC.Application.Property.Value> GetProperties(XBMCRPC.Application.GetProperties_properties properties=null)
        {
            var jArgs = new JObject();
             if (properties != null)
             {
                 var jpropproperties = JToken.FromObject(properties, _client.Serializer);
                 jArgs.Add(new JProperty("properties", jpropproperties));
             }
            return await _client.GetData<XBMCRPC.Application.Property.Value>("Application.GetProperties", jArgs);
        }

                /// <summary>
                /// Quit application
                /// </summary>
        public async Task<string> Quit()
        {
            var jArgs = new JObject();
            return await _client.GetData<string>("Application.Quit", jArgs);
        }

                /// <summary>
                /// Toggle mute/unmute
                /// </summary>
        public async Task<bool> SetMute(bool mute)
        {
            var jArgs = new JObject();
             if (mute != null)
             {
                 var jpropmute = JToken.FromObject(mute, _client.Serializer);
                 jArgs.Add(new JProperty("mute", jpropmute));
             }
            return await _client.GetData<bool>("Application.SetMute", jArgs);
        }

                /// <summary>
                /// Toggle mute/unmute
                /// </summary>
        public async Task<bool> SetMute(XBMCRPC.Global.Toggle2 mute)
        {
            var jArgs = new JObject();
             if (mute != null)
             {
                 var jpropmute = JToken.FromObject(mute, _client.Serializer);
                 jArgs.Add(new JProperty("mute", jpropmute));
             }
            return await _client.GetData<bool>("Application.SetMute", jArgs);
        }

                /// <summary>
                /// Toggle mute/unmute
                /// </summary>
        public async Task<bool> SetMute()
        {
            var jArgs = new JObject();
            return await _client.GetData<bool>("Application.SetMute", jArgs);
        }

                /// <summary>
                /// Set the current volume
                /// </summary>
        public async Task<int> SetVolume(int volume)
        {
            var jArgs = new JObject();
             if (volume != null)
             {
                 var jpropvolume = JToken.FromObject(volume, _client.Serializer);
                 jArgs.Add(new JProperty("volume", jpropvolume));
             }
            return await _client.GetData<int>("Application.SetVolume", jArgs);
        }

                /// <summary>
                /// Set the current volume
                /// </summary>
        public async Task<int> SetVolume(XBMCRPC.Global.IncrementDecrement volume)
        {
            var jArgs = new JObject();
             if (volume != null)
             {
                 var jpropvolume = JToken.FromObject(volume, _client.Serializer);
                 jArgs.Add(new JProperty("volume", jpropvolume));
             }
            return await _client.GetData<int>("Application.SetVolume", jArgs);
        }

                /// <summary>
                /// Set the current volume
                /// </summary>
        public async Task<int> SetVolume()
        {
            var jArgs = new JObject();
            return await _client.GetData<int>("Application.SetVolume", jArgs);
        }

        public delegate void OnVolumeChangedDelegate(string sender=null, XBMCRPC.Application.OnVolumeChanged_data data=null);
        public event OnVolumeChangedDelegate OnVolumeChanged;
        internal void RaiseOnVolumeChanged(string sender=null, XBMCRPC.Application.OnVolumeChanged_data data=null)
        {
            if (OnVolumeChanged != null)
            {
                OnVolumeChanged.BeginInvoke(sender, data, null, null);
            }
        }
   }
}
