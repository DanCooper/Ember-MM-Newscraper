using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FanartTV.Utilities
{
    /// <summary>
    /// Retrieve all the basic movie information for a particular movie by FanartTV reference.
    /// http://fanart.tv/api-docs/movie-api/
    /// </summary>
    /// <param name="apikey">FanartTV movie id</param>
    /// <param name="id">optional - ISO 639-1 language code</param>
    /// The following parameters are optional but if you want to change any from the default value you must specify all optional parameters before the one you are changing (so if you are changing just format you don’t have to specify type, but if you want to change the sort you MUST also specify the format and type)</param>
    /// <param name="format">Returns the results in the requested format - json (default) / php (returns a php serialized object)</param>
    /// <param name="type">Returns the requested image types - all (default) / movielogo / movieart / moviedisc</param>
    /// <param name="sort">1 – Sorted by most popular image then newest(default) / 2 – Sorted by newest uploaded image / 3 – Sorted by oldest uploaded image</param>
    /// <param name="limit">Value is either 1 (1 image) or 2 (all images – default), for example, when automatically downloading images you might only want to return the first result so the user doesn’t have to provide input, whereas with a manual download you might want the user to see all the options.</param>
    /// <returns></returns>
    internal partial class RequestGenerator
    {

        #region Request Constants
        public const string REQUEST_MOVIE = "movie/{api_key}/{id}/{format}/{type}/{sort}/{limit}";
        public const string REQUEST_SERIES = "series/{api_key}/{id}/{format}/{type}/{sort}/{limit}";
        #endregion

    }


    internal partial class RequestBuilder
    {

        #region Parameter Constants
        public const string PARAMETER_APIKEY = "api_key";
        public const string PARAMETER_FORMAT = "format";
        public const string PARAMETER_TYPE = "type";
        public const string PARAMETER_SORT = "sort";
        public const string PARAMETER_ID = "id";
        public const string PARAMETER_LIMIT = "limit";
        #endregion

    }
}
