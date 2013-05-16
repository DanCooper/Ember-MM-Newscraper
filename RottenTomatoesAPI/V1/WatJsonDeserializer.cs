using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp.Deserializers;

namespace RottenTomatoes.V1
{
    public class WatJsonDeserializer : IDeserializer
    {
        #region IDeserializer Members

        public T Deserialize<T>(RestSharp.IRestResponse response)
        {
            if (response.Content == null)
                return default(T);

            // 2012-07-26 - attempt to handle some of the bad json content, eg Similar Movies for id 80271 containing null values
            //    amongst the other objects in the results array
            //    eg ...."vote_average":0.0,"vote_count":0},null,null,{"backdrop_path":null,"id":73736,"original_title":...
            //                                              ^^^^ ^^^^
            //    Causes "Object reference not set to an instance of an object" exception during deserialization
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
