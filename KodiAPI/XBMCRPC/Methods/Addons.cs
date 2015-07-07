using System;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
namespace XBMCRPC.Methods
{
   public partial class Addons
   {
        private readonly Client _client;
          public Addons(Client client)
          {
              _client = client;
          }

                /// <summary>
                /// Executes the given addon with the given parameters (if possible)
                /// </summary>
        public async Task<string> ExecuteAddon(XBMCRPC.Addons.ExecuteAddon_params1 params_arg, string addonid=null, bool wait=false)
        {
            var jArgs = new JObject();
             if (addonid != null)
             {
                 var jpropaddonid = JToken.FromObject(addonid, _client.Serializer);
                 jArgs.Add(new JProperty("addonid", jpropaddonid));
             }
             if (params_arg != null)
             {
                 var jpropparams_arg = JToken.FromObject(params_arg, _client.Serializer);
                 jArgs.Add(new JProperty("params_arg", jpropparams_arg));
             }
             if (wait != null)
             {
                 var jpropwait = JToken.FromObject(wait, _client.Serializer);
                 jArgs.Add(new JProperty("wait", jpropwait));
             }
            return await _client.GetData<string>("Addons.ExecuteAddon", jArgs);
        }

                /// <summary>
                /// Executes the given addon with the given parameters (if possible)
                /// </summary>
        public async Task<string> ExecuteAddon(global::System.Collections.Generic.List<string> params_arg, string addonid=null, bool wait=false)
        {
            var jArgs = new JObject();
             if (addonid != null)
             {
                 var jpropaddonid = JToken.FromObject(addonid, _client.Serializer);
                 jArgs.Add(new JProperty("addonid", jpropaddonid));
             }
             if (params_arg != null)
             {
                 var jpropparams_arg = JToken.FromObject(params_arg, _client.Serializer);
                 jArgs.Add(new JProperty("params_arg", jpropparams_arg));
             }
             if (wait != null)
             {
                 var jpropwait = JToken.FromObject(wait, _client.Serializer);
                 jArgs.Add(new JProperty("wait", jpropwait));
             }
            return await _client.GetData<string>("Addons.ExecuteAddon", jArgs);
        }

                /// <summary>
                /// Executes the given addon with the given parameters (if possible)
                /// </summary>
        public async Task<string> ExecuteAddon(string params_arg, string addonid=null, bool wait=false)
        {
            var jArgs = new JObject();
             if (addonid != null)
             {
                 var jpropaddonid = JToken.FromObject(addonid, _client.Serializer);
                 jArgs.Add(new JProperty("addonid", jpropaddonid));
             }
             if (params_arg != null)
             {
                 var jpropparams_arg = JToken.FromObject(params_arg, _client.Serializer);
                 jArgs.Add(new JProperty("params_arg", jpropparams_arg));
             }
             if (wait != null)
             {
                 var jpropwait = JToken.FromObject(wait, _client.Serializer);
                 jArgs.Add(new JProperty("wait", jpropwait));
             }
            return await _client.GetData<string>("Addons.ExecuteAddon", jArgs);
        }

                /// <summary>
                /// Executes the given addon with the given parameters (if possible)
                /// </summary>
        public async Task<string> ExecuteAddon(string addonid=null, bool wait=false)
        {
            var jArgs = new JObject();
             if (addonid != null)
             {
                 var jpropaddonid = JToken.FromObject(addonid, _client.Serializer);
                 jArgs.Add(new JProperty("addonid", jpropaddonid));
             }
             if (wait != null)
             {
                 var jpropwait = JToken.FromObject(wait, _client.Serializer);
                 jArgs.Add(new JProperty("wait", jpropwait));
             }
            return await _client.GetData<string>("Addons.ExecuteAddon", jArgs);
        }

                /// <summary>
                /// Gets the details of a specific addon
                /// </summary>
        public async Task<XBMCRPC.Addons.GetAddonDetailsResponse> GetAddonDetails(string addonid=null, XBMCRPC.Addon.Fields properties=null)
        {
            var jArgs = new JObject();
             if (addonid != null)
             {
                 var jpropaddonid = JToken.FromObject(addonid, _client.Serializer);
                 jArgs.Add(new JProperty("addonid", jpropaddonid));
             }
             if (properties != null)
             {
                 var jpropproperties = JToken.FromObject(properties, _client.Serializer);
                 jArgs.Add(new JProperty("properties", jpropproperties));
             }
            return await _client.GetData<XBMCRPC.Addons.GetAddonDetailsResponse>("Addons.GetAddonDetails", jArgs);
        }

                /// <summary>
                /// Gets all available addons
                /// </summary>
        public async Task<XBMCRPC.Addons.GetAddonsResponse> GetAddons(bool enabled, XBMCRPC.Addon.Types type=0, XBMCRPC.Addon.Content content=0, XBMCRPC.Addon.Fields properties=null, XBMCRPC.List.Limits limits=null)
        {
            var jArgs = new JObject();
             if (type != null)
             {
                 var jproptype = JToken.FromObject(type, _client.Serializer);
                 jArgs.Add(new JProperty("type", jproptype));
             }
             if (content != null)
             {
                 var jpropcontent = JToken.FromObject(content, _client.Serializer);
                 jArgs.Add(new JProperty("content", jpropcontent));
             }
             if (enabled != null)
             {
                 var jpropenabled = JToken.FromObject(enabled, _client.Serializer);
                 jArgs.Add(new JProperty("enabled", jpropenabled));
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
            return await _client.GetData<XBMCRPC.Addons.GetAddonsResponse>("Addons.GetAddons", jArgs);
        }

                /// <summary>
                /// Gets all available addons
                /// </summary>
        public async Task<XBMCRPC.Addons.GetAddonsResponse> GetAddons(XBMCRPC.Addons.GetAddons_enabled2 enabled, XBMCRPC.Addon.Types type=0, XBMCRPC.Addon.Content content=0, XBMCRPC.Addon.Fields properties=null, XBMCRPC.List.Limits limits=null)
        {
            var jArgs = new JObject();
             if (type != null)
             {
                 var jproptype = JToken.FromObject(type, _client.Serializer);
                 jArgs.Add(new JProperty("type", jproptype));
             }
             if (content != null)
             {
                 var jpropcontent = JToken.FromObject(content, _client.Serializer);
                 jArgs.Add(new JProperty("content", jpropcontent));
             }
             if (enabled != null)
             {
                 var jpropenabled = JToken.FromObject(enabled, _client.Serializer);
                 jArgs.Add(new JProperty("enabled", jpropenabled));
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
            return await _client.GetData<XBMCRPC.Addons.GetAddonsResponse>("Addons.GetAddons", jArgs);
        }

                /// <summary>
                /// Gets all available addons
                /// </summary>
        public async Task<XBMCRPC.Addons.GetAddonsResponse> GetAddons(XBMCRPC.Addon.Types type=0, XBMCRPC.Addon.Content content=0, XBMCRPC.Addon.Fields properties=null, XBMCRPC.List.Limits limits=null)
        {
            var jArgs = new JObject();
             if (type != null)
             {
                 var jproptype = JToken.FromObject(type, _client.Serializer);
                 jArgs.Add(new JProperty("type", jproptype));
             }
             if (content != null)
             {
                 var jpropcontent = JToken.FromObject(content, _client.Serializer);
                 jArgs.Add(new JProperty("content", jpropcontent));
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
            return await _client.GetData<XBMCRPC.Addons.GetAddonsResponse>("Addons.GetAddons", jArgs);
        }

                /// <summary>
                /// Enables/Disables a specific addon
                /// </summary>
        public async Task<string> SetAddonEnabled(bool enabled, string addonid=null)
        {
            var jArgs = new JObject();
             if (addonid != null)
             {
                 var jpropaddonid = JToken.FromObject(addonid, _client.Serializer);
                 jArgs.Add(new JProperty("addonid", jpropaddonid));
             }
             if (enabled != null)
             {
                 var jpropenabled = JToken.FromObject(enabled, _client.Serializer);
                 jArgs.Add(new JProperty("enabled", jpropenabled));
             }
            return await _client.GetData<string>("Addons.SetAddonEnabled", jArgs);
        }

                /// <summary>
                /// Enables/Disables a specific addon
                /// </summary>
        public async Task<string> SetAddonEnabled(XBMCRPC.Global.Toggle2 enabled, string addonid=null)
        {
            var jArgs = new JObject();
             if (addonid != null)
             {
                 var jpropaddonid = JToken.FromObject(addonid, _client.Serializer);
                 jArgs.Add(new JProperty("addonid", jpropaddonid));
             }
             if (enabled != null)
             {
                 var jpropenabled = JToken.FromObject(enabled, _client.Serializer);
                 jArgs.Add(new JProperty("enabled", jpropenabled));
             }
            return await _client.GetData<string>("Addons.SetAddonEnabled", jArgs);
        }

                /// <summary>
                /// Enables/Disables a specific addon
                /// </summary>
        public async Task<string> SetAddonEnabled(string addonid=null)
        {
            var jArgs = new JObject();
             if (addonid != null)
             {
                 var jpropaddonid = JToken.FromObject(addonid, _client.Serializer);
                 jArgs.Add(new JProperty("addonid", jpropaddonid));
             }
            return await _client.GetData<string>("Addons.SetAddonEnabled", jArgs);
        }
   }
}
