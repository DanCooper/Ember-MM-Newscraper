using System;
using System.Net;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace XBMCRPC
{

    public interface ISocket : IDisposable
    {
        Task ConnectAsync(string hostName, int port);
        Stream GetInputStream();
    }
}