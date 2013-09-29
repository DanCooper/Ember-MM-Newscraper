using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trakttv
{
    [Serializable]
    public class TraktException : Exception
    {
        public string ErrorMessage
        {
            get
            {
                return base.Message;
            }
        }

        public TraktException()
        {

        }

        public TraktException(string errorMessage)
            : base(errorMessage) { }

        public TraktException(string errorMessage, Exception innerEx)
            : base(errorMessage, innerEx) { }
    }
}
