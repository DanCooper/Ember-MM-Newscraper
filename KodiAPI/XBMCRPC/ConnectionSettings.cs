using System;

namespace XBMCRPC
{
    public class ConnectionSettings
    {
        public string Host;
        public int Port;
		public int TcpPort=9090;
        public string UserName;
        public string Password;
        public Uri JsonInterfaceAddress;
        public Uri BaseAddress;
        

        public ConnectionSettings(string host, int port, string userName, string password)
        {
            Host = host;
            Port = port;
            UserName = userName;
            Password = password;
            JsonInterfaceAddress = new Uri(String.Format("http://{0}:{1}/jsonrpc", host, port));
            BaseAddress = new Uri(String.Format("http://{0}:{1}", host, port));
        }
    }
}
