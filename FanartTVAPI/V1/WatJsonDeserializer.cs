using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp.Deserializers;

namespace FanartTV.V1
{
    public class WatJsonDeserializer : IDeserializer
    {
        #region IDeserializer Members

        public T Deserialize<T>(RestSharp.IRestResponse response)
        {
            if (response.Content == null)
                return default(T);
			if (response.Content == "null")
				return default(T);

            // handle custom answer
			string astr = @"{""title"":" + response.Content.Substring(1, response.Content.Length - 1);
			int pos = astr.IndexOf(@""":{""tmdb_id""");
			astr = astr.Substring(0, pos+1) + @",""movieinfo" + astr.Substring(pos, astr.Length - pos);
			response.Content = astr;

            var deserializer = new JsonDeserializer();
            var data = deserializer.Deserialize<T>(response);

            return data;
        }

        public string DateFormat { get; set; }
        public string Namespace { get; set; }
        public string RootElement { get; set; }

        #endregion
    }
}
