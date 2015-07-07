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
    public interface ISocketFactory
    {
        ISocket GetSocket();
      Task<string[]> ResolveHostname(string hostname);
    }
}