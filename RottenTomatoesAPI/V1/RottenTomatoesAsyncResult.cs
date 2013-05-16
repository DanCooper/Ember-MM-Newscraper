using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RottenTomatoes.V1
{
	public class RottenTomatoesAsyncResult<T>
    {
        public T Data { get; set; }
        public string Error { get; set; }
        public object UserState { get; set; }
    }

    public class TmdbAsyncETagResult
    {
        public string ETag { get; set; }
        public object UserState { get; set; }
    }
}
