using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp;
using System.Globalization;

namespace FanartTV.Utilities
{
    internal partial class RequestBuilder
    {
        public RequestBuilder(string resource, Method method)
        {
            Request = new RestRequest(resource, method);
            Request.AddHeader("Accept", "application/json");
        }

        #region Properties

        private RestRequest Request { get; set; }

        private Method Method
        {
            get { return Request.Method; }
            set { Request.Method = value; }
        }

        private object UserState
        {
            get { return Request.UserState; }
            set { if (value != null) Request.UserState = value; }
        }

        #endregion

        public RestRequest GetRequest()
        {
            return Request;
        }

        public RequestBuilder SetUserState(object userState)
        {
            UserState = userState;
            return this;
        }

        public RequestBuilder AddParameter(string name, bool? value)
        {
            if (!value.HasValue) return this;

            Request.AddParameter(name, value.Value ? "true" : "false");
            return this;
        }

        public RequestBuilder AddParameter(string name, int? value)
        {
            if (!value.HasValue) return this;

            Request.AddParameter(name, value.Value);
            return this;
        }

        public RequestBuilder AddParameter(string name, string value)
        {
            if (string.IsNullOrEmpty(value)) return this;
            value = value.EscapeString();

            Request.AddParameter(name, value);
            return this;
        }

        public RequestBuilder AddParameter(string name, DateTime? value)
        {
            if (!value.HasValue) return this;

            Request.AddParameter(name, value.Value.ToString("yyyy-MM-dd"));
            return this;
        }

        public RequestBuilder AddParameter(string name, object value)
        {
            if (value == null) return this;

            Request.AddParameter(name, value);
            return this;
        }

        public RequestBuilder AddUrlSegment(string name, int? value)
        {
            if (!value.HasValue) return this;

            Request.AddUrlSegment(name, value.Value.ToString(CultureInfo.InvariantCulture));
            return this;
        }

        public RequestBuilder AddUrlSegment(string name, string value)
        {
            if (string.IsNullOrEmpty(value)) return this;
            value = value.EscapeString();

            Request.AddUrlSegment(name, value);
            return this;
        }
    }
}
