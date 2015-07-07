using System;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
namespace XBMCRPC.Methods
{
   public partial class Files
   {
        private readonly Client _client;
          public Files(Client client)
          {
              _client = client;
          }

                /// <summary>
                /// Get the directories and files in the given directory
                /// </summary>
        public async Task<XBMCRPC.Files.GetDirectoryResponse> GetDirectory(string directory=null, XBMCRPC.Files.Media media=0, XBMCRPC.List.Fields.Files properties=null, XBMCRPC.List.Sort sort=null, XBMCRPC.List.Limits limits=null)
        {
            var jArgs = new JObject();
             if (directory != null)
             {
                 var jpropdirectory = JToken.FromObject(directory, _client.Serializer);
                 jArgs.Add(new JProperty("directory", jpropdirectory));
             }
             if (media != null)
             {
                 var jpropmedia = JToken.FromObject(media, _client.Serializer);
                 jArgs.Add(new JProperty("media", jpropmedia));
             }
             if (properties != null)
             {
                 var jpropproperties = JToken.FromObject(properties, _client.Serializer);
                 jArgs.Add(new JProperty("properties", jpropproperties));
             }
             if (sort != null)
             {
                 var jpropsort = JToken.FromObject(sort, _client.Serializer);
                 jArgs.Add(new JProperty("sort", jpropsort));
             }
             if (limits != null)
             {
                 var jproplimits = JToken.FromObject(limits, _client.Serializer);
                 jArgs.Add(new JProperty("limits", jproplimits));
             }
            return await _client.GetData<XBMCRPC.Files.GetDirectoryResponse>("Files.GetDirectory", jArgs);
        }

                /// <summary>
                /// Get details for a specific file
                /// </summary>
        public async Task<XBMCRPC.Files.GetFileDetailsResponse> GetFileDetails(string file=null, XBMCRPC.Files.Media media=0, XBMCRPC.List.Fields.Files properties=null)
        {
            var jArgs = new JObject();
             if (file != null)
             {
                 var jpropfile = JToken.FromObject(file, _client.Serializer);
                 jArgs.Add(new JProperty("file", jpropfile));
             }
             if (media != null)
             {
                 var jpropmedia = JToken.FromObject(media, _client.Serializer);
                 jArgs.Add(new JProperty("media", jpropmedia));
             }
             if (properties != null)
             {
                 var jpropproperties = JToken.FromObject(properties, _client.Serializer);
                 jArgs.Add(new JProperty("properties", jpropproperties));
             }
            return await _client.GetData<XBMCRPC.Files.GetFileDetailsResponse>("Files.GetFileDetails", jArgs);
        }

                /// <summary>
                /// Get the sources of the media windows
                /// </summary>
        public async Task<XBMCRPC.Files.GetSourcesResponse> GetSources(XBMCRPC.Files.Media media=0, XBMCRPC.List.Limits limits=null, XBMCRPC.List.Sort sort=null)
        {
            var jArgs = new JObject();
             if (media != null)
             {
                 var jpropmedia = JToken.FromObject(media, _client.Serializer);
                 jArgs.Add(new JProperty("media", jpropmedia));
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
            return await _client.GetData<XBMCRPC.Files.GetSourcesResponse>("Files.GetSources", jArgs);
        }

                /// <summary>
                /// Provides a way to download a given file (e.g. providing an URL to the real file location)
                /// </summary>
        public async Task<XBMCRPC.Files.PrepareDownloadResponse> PrepareDownload(string path=null)
        {
            var jArgs = new JObject();
             if (path != null)
             {
                 var jproppath = JToken.FromObject(path, _client.Serializer);
                 jArgs.Add(new JProperty("path", jproppath));
             }
            return await _client.GetData<XBMCRPC.Files.PrepareDownloadResponse>("Files.PrepareDownload", jArgs);
        }
   }
}
