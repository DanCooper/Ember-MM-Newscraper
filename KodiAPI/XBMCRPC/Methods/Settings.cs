using System;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
namespace XBMCRPC.Methods
{
   public partial class Settings
   {
        private readonly Client _client;
          public Settings(Client client)
          {
              _client = client;
          }

                /// <summary>
                /// Retrieves all setting categories
                /// </summary>
        public async Task<XBMCRPC.Settings.GetCategoriesResponse> GetCategories(XBMCRPC.Setting.Level level=0, string section=null, XBMCRPC.Settings.GetCategories_properties properties=null)
        {
            var jArgs = new JObject();
             if (level != null)
             {
                 var jproplevel = JToken.FromObject(level, _client.Serializer);
                 jArgs.Add(new JProperty("level", jproplevel));
             }
             if (section != null)
             {
                 var jpropsection = JToken.FromObject(section, _client.Serializer);
                 jArgs.Add(new JProperty("section", jpropsection));
             }
             if (properties != null)
             {
                 var jpropproperties = JToken.FromObject(properties, _client.Serializer);
                 jArgs.Add(new JProperty("properties", jpropproperties));
             }
            return await _client.GetData<XBMCRPC.Settings.GetCategoriesResponse>("Settings.GetCategories", jArgs);
        }

                /// <summary>
                /// Retrieves all setting sections
                /// </summary>
        public async Task<XBMCRPC.Settings.GetSectionsResponse> GetSections(XBMCRPC.Setting.Level level=0, XBMCRPC.Settings.GetSections_properties properties=null)
        {
            var jArgs = new JObject();
             if (level != null)
             {
                 var jproplevel = JToken.FromObject(level, _client.Serializer);
                 jArgs.Add(new JProperty("level", jproplevel));
             }
             if (properties != null)
             {
                 var jpropproperties = JToken.FromObject(properties, _client.Serializer);
                 jArgs.Add(new JProperty("properties", jpropproperties));
             }
            return await _client.GetData<XBMCRPC.Settings.GetSectionsResponse>("Settings.GetSections", jArgs);
        }

                /// <summary>
                /// Retrieves the value of a setting
                /// </summary>
        public async Task<XBMCRPC.Settings.GetSettingValueResponse> GetSettingValue(string setting=null)
        {
            var jArgs = new JObject();
             if (setting != null)
             {
                 var jpropsetting = JToken.FromObject(setting, _client.Serializer);
                 jArgs.Add(new JProperty("setting", jpropsetting));
             }
            return await _client.GetData<XBMCRPC.Settings.GetSettingValueResponse>("Settings.GetSettingValue", jArgs);
        }

                /// <summary>
                /// Retrieves all settings
                /// </summary>
        public async Task<XBMCRPC.Settings.GetSettingsResponse> GetSettings(XBMCRPC.Settings.GetSettings_filter1 filter, XBMCRPC.Setting.Level level=0)
        {
            var jArgs = new JObject();
             if (level != null)
             {
                 var jproplevel = JToken.FromObject(level, _client.Serializer);
                 jArgs.Add(new JProperty("level", jproplevel));
             }
             if (filter != null)
             {
                 var jpropfilter = JToken.FromObject(filter, _client.Serializer);
                 jArgs.Add(new JProperty("filter", jpropfilter));
             }
            return await _client.GetData<XBMCRPC.Settings.GetSettingsResponse>("Settings.GetSettings", jArgs);
        }

                /// <summary>
                /// Retrieves all settings
                /// </summary>
        public async Task<XBMCRPC.Settings.GetSettingsResponse> GetSettings(XBMCRPC.Setting.Level level=0)
        {
            var jArgs = new JObject();
             if (level != null)
             {
                 var jproplevel = JToken.FromObject(level, _client.Serializer);
                 jArgs.Add(new JProperty("level", jproplevel));
             }
            return await _client.GetData<XBMCRPC.Settings.GetSettingsResponse>("Settings.GetSettings", jArgs);
        }

                /// <summary>
                /// Resets the value of a setting
                /// </summary>
        public async Task<string> ResetSettingValue(string setting=null)
        {
            var jArgs = new JObject();
             if (setting != null)
             {
                 var jpropsetting = JToken.FromObject(setting, _client.Serializer);
                 jArgs.Add(new JProperty("setting", jpropsetting));
             }
            return await _client.GetData<string>("Settings.ResetSettingValue", jArgs);
        }

                /// <summary>
                /// Changes the value of a setting
                /// </summary>
        public async Task<bool> SetSettingValue(bool value, string setting=null)
        {
            var jArgs = new JObject();
             if (setting != null)
             {
                 var jpropsetting = JToken.FromObject(setting, _client.Serializer);
                 jArgs.Add(new JProperty("setting", jpropsetting));
             }
             if (value != null)
             {
                 var jpropvalue = JToken.FromObject(value, _client.Serializer);
                 jArgs.Add(new JProperty("value", jpropvalue));
             }
            return await _client.GetData<bool>("Settings.SetSettingValue", jArgs);
        }

                /// <summary>
                /// Changes the value of a setting
                /// </summary>
        public async Task<bool> SetSettingValue(int value, string setting=null)
        {
            var jArgs = new JObject();
             if (setting != null)
             {
                 var jpropsetting = JToken.FromObject(setting, _client.Serializer);
                 jArgs.Add(new JProperty("setting", jpropsetting));
             }
             if (value != null)
             {
                 var jpropvalue = JToken.FromObject(value, _client.Serializer);
                 jArgs.Add(new JProperty("value", jpropvalue));
             }
            return await _client.GetData<bool>("Settings.SetSettingValue", jArgs);
        }

                /// <summary>
                /// Changes the value of a setting
                /// </summary>
        public async Task<bool> SetSettingValue(double value, string setting=null)
        {
            var jArgs = new JObject();
             if (setting != null)
             {
                 var jpropsetting = JToken.FromObject(setting, _client.Serializer);
                 jArgs.Add(new JProperty("setting", jpropsetting));
             }
             if (value != null)
             {
                 var jpropvalue = JToken.FromObject(value, _client.Serializer);
                 jArgs.Add(new JProperty("value", jpropvalue));
             }
            return await _client.GetData<bool>("Settings.SetSettingValue", jArgs);
        }

                /// <summary>
                /// Changes the value of a setting
                /// </summary>
        public async Task<bool> SetSettingValue(string value, string setting=null)
        {
            var jArgs = new JObject();
             if (setting != null)
             {
                 var jpropsetting = JToken.FromObject(setting, _client.Serializer);
                 jArgs.Add(new JProperty("setting", jpropsetting));
             }
             if (value != null)
             {
                 var jpropvalue = JToken.FromObject(value, _client.Serializer);
                 jArgs.Add(new JProperty("value", jpropvalue));
             }
            return await _client.GetData<bool>("Settings.SetSettingValue", jArgs);
        }

                /// <summary>
                /// Changes the value of a setting
                /// </summary>
        public async Task<bool> SetSettingValue(global::System.Collections.Generic.List<object> value, string setting=null)
        {
            var jArgs = new JObject();
             if (setting != null)
             {
                 var jpropsetting = JToken.FromObject(setting, _client.Serializer);
                 jArgs.Add(new JProperty("setting", jpropsetting));
             }
             if (value != null)
             {
                 var jpropvalue = JToken.FromObject(value, _client.Serializer);
                 jArgs.Add(new JProperty("value", jpropvalue));
             }
            return await _client.GetData<bool>("Settings.SetSettingValue", jArgs);
        }

                /// <summary>
                /// Changes the value of a setting
                /// </summary>
        public async Task<bool> SetSettingValue(string setting=null)
        {
            var jArgs = new JObject();
             if (setting != null)
             {
                 var jpropsetting = JToken.FromObject(setting, _client.Serializer);
                 jArgs.Add(new JProperty("setting", jpropsetting));
             }
            return await _client.GetData<bool>("Settings.SetSettingValue", jArgs);
        }
   }
}
