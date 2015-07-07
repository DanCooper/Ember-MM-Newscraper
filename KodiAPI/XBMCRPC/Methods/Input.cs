using System;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
namespace XBMCRPC.Methods
{
   public partial class Input
   {
        private readonly Client _client;
          public Input(Client client)
          {
              _client = client;
          }

                /// <summary>
                /// Goes back in GUI
                /// </summary>
        public async Task<string> Back()
        {
            var jArgs = new JObject();
            return await _client.GetData<string>("Input.Back", jArgs);
        }

                /// <summary>
                /// Shows the context menu
                /// </summary>
        public async Task<string> ContextMenu()
        {
            var jArgs = new JObject();
            return await _client.GetData<string>("Input.ContextMenu", jArgs);
        }

                /// <summary>
                /// Navigate down in GUI
                /// </summary>
        public async Task<string> Down()
        {
            var jArgs = new JObject();
            return await _client.GetData<string>("Input.Down", jArgs);
        }

                /// <summary>
                /// Execute a specific action
                /// </summary>
        public async Task<string> ExecuteAction(XBMCRPC.Input.Action action=0)
        {
            var jArgs = new JObject();
             if (action != null)
             {
                 var jpropaction = JToken.FromObject(action, _client.Serializer);
                 jArgs.Add(new JProperty("action", jpropaction));
             }
            return await _client.GetData<string>("Input.ExecuteAction", jArgs);
        }

                /// <summary>
                /// Goes to home window in GUI
                /// </summary>
        public async Task<string> Home()
        {
            var jArgs = new JObject();
            return await _client.GetData<string>("Input.Home", jArgs);
        }

                /// <summary>
                /// Shows the information dialog
                /// </summary>
        public async Task<string> Info()
        {
            var jArgs = new JObject();
            return await _client.GetData<string>("Input.Info", jArgs);
        }

                /// <summary>
                /// Navigate left in GUI
                /// </summary>
        public async Task<string> Left()
        {
            var jArgs = new JObject();
            return await _client.GetData<string>("Input.Left", jArgs);
        }

                /// <summary>
                /// Navigate right in GUI
                /// </summary>
        public async Task<string> Right()
        {
            var jArgs = new JObject();
            return await _client.GetData<string>("Input.Right", jArgs);
        }

                /// <summary>
                /// Select current item in GUI
                /// </summary>
        public async Task<string> Select()
        {
            var jArgs = new JObject();
            return await _client.GetData<string>("Input.Select", jArgs);
        }

                /// <summary>
                /// Send a generic (unicode) text
                /// </summary>
        public async Task<string> SendText(string text=null, bool done=false)
        {
            var jArgs = new JObject();
             if (text != null)
             {
                 var jproptext = JToken.FromObject(text, _client.Serializer);
                 jArgs.Add(new JProperty("text", jproptext));
             }
             if (done != null)
             {
                 var jpropdone = JToken.FromObject(done, _client.Serializer);
                 jArgs.Add(new JProperty("done", jpropdone));
             }
            return await _client.GetData<string>("Input.SendText", jArgs);
        }

                /// <summary>
                /// Show codec information of the playing item
                /// </summary>
        public async Task<string> ShowCodec()
        {
            var jArgs = new JObject();
            return await _client.GetData<string>("Input.ShowCodec", jArgs);
        }

                /// <summary>
                /// Show the on-screen display for the current player
                /// </summary>
        public async Task<string> ShowOSD()
        {
            var jArgs = new JObject();
            return await _client.GetData<string>("Input.ShowOSD", jArgs);
        }

                /// <summary>
                /// Navigate up in GUI
                /// </summary>
        public async Task<string> Up()
        {
            var jArgs = new JObject();
            return await _client.GetData<string>("Input.Up", jArgs);
        }

        public delegate void OnInputFinishedDelegate(string sender=null, object data=null);
        public event OnInputFinishedDelegate OnInputFinished;
        internal void RaiseOnInputFinished(string sender=null, object data=null)
        {
            if (OnInputFinished != null)
            {
                OnInputFinished.BeginInvoke(sender, data, null, null);
            }
        }

        public delegate void OnInputRequestedDelegate(string sender=null, XBMCRPC.Input.OnInputRequested_data data=null);
        public event OnInputRequestedDelegate OnInputRequested;
        internal void RaiseOnInputRequested(string sender=null, XBMCRPC.Input.OnInputRequested_data data=null)
        {
            if (OnInputRequested != null)
            {
                OnInputRequested.BeginInvoke(sender, data, null, null);
            }
        }
   }
}
