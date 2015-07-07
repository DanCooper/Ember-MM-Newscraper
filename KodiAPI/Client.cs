using System;
using System.Linq;
using System.Net;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;


namespace XBMCRPC
{
    /// <summary>
    /// This partial class contains some additional XBMC specific methods
    /// </summary>
    public partial class Client
    {
        public Client(IPlatformServices platformServices, string host, int port = 8080, string userName = "Kodi", string password = "")
            : this(new ConnectionSettings(host, port, userName, password), platformServices)
        {
        }

        /// <summary>
        /// Gets a stream of the image thumbnail.
        /// </summary>
        /// <param name="thumbnailUri">The thumbnail URI.</param>
        /// <returns></returns>
        public async Task<Stream> GetImageStream(string thumbnailUri)
        {
            var downloadUri = await GetImageUri(thumbnailUri);

            var request = WebRequest.Create(downloadUri);
            request.Credentials = new NetworkCredential(_settings.UserName, _settings.Password);

            var response = await request.GetResponseAsync();
            return response.GetResponseStream();
        }

        public async Task<Uri> GetImageUri(string thumbnailUri)
        {
            string downloadPath;
            if (thumbnailUri.StartsWith("image:"))
            {
                var thumbnailEncoded = Uri.EscapeDataString(thumbnailUri);
                downloadPath = "image/" + thumbnailEncoded;
            }
            else
            {
                var download = await Files.PrepareDownload(thumbnailUri);
                downloadPath = ((JObject)download.details)["path"].ToString();
            }
            var downloadUri = new Uri(_settings.BaseAddress + downloadPath);
            return downloadUri;
        }

        ///// <summary>
        ///// Saves a downloaded image to a file
        ///// </summary>
        ///// <param name="thumbnailUri">The thumbnail URI</param>
        ///// <param name="downloadFile">Path of downloaded file</param>
        ///// <returns></returns>
        //public async Task DownloadImage(string thumbnailUri, string downloadFile)
        //{
        //    var httpStream = await GetImageStream(thumbnailUri);
        //    var fileStream = File.OpenWrite(downloadFile);
        //    await httpStream.CopyToAsync(fileStream);
        //    fileStream.Close();
        //    httpStream.Close();
        //}

        /// <summary>
        /// Returns all values of an enum in an array
        /// </summary>
        /// <typeparam name="TEnum">Type name of the enum</typeparam>
        /// <returns></returns>
        public static TEnum[] AllValues<TEnum>()
        {
            return Enum.GetValues(typeof(TEnum)).Cast<TEnum>().ToArray();
        }

        public async Task<Uri> PrepareDownload(string path)
        {
            string downloadPath;

            var download = await Files.PrepareDownload(path);
            downloadPath = ((JObject)download.details)["path"].ToString();

            var uri = new Uri(_settings.BaseAddress + downloadPath);

            return uri;
        }
    }
}