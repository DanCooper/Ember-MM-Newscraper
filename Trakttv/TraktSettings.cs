using System;
using System.Collections.Generic;
using System.Reflection;
using Trakttv.TraktAPI;
using Trakttv.TraktAPI.Model;

namespace Trakttv
{
    //  This class handles Setter and Getter of trakt.tv Wrapper
    public class TraktSettings
    {   
        public const string UserAgentName = "TrakttvforEmber";
        // APIKEY - can be replaced by user APIkey through setter!
        static string _apiKey = "ce4d4ac977084c873da8738f949d380776756b82";
        //Login-Data needs to be set from outside trakttv class, i.e:
        //  TraktSettings.Username = SomeTextboxUsername.Text;
        //  TraktSettings.Password = SomeTextboxPassword.Text;
        static string _password = String.Empty;
        static string _username = String.Empty;

        #region Properties

        #region ApiKey
        public static string ApiKey
        {
            get
            {
                return _apiKey;
            }
            set
            {
                _apiKey = value;
                TrakttvAPI.Username = _apiKey;
            }
        }
        #endregion

        #region UserAgent
        /// <summary>
        /// Trakt.tv defined UserAgent used for Web Requests
        /// </summary>
        public static string UserAgent
        {
            get
            {
              //  also use version info of Trakttv Wrapper
                return UserAgentName + "|" + Assembly.GetCallingAssembly().GetName().Version.ToString();
            }
        }
         #endregion

        #region Username
        public static string Username
        {
            get
            {
                return _username;
            }
            set
            {
                _username = value;
               TrakttvAPI.Username = _username;
            }
        }
        #endregion

        #region Password
        public static string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                TrakttvAPI.Password = _password;
            }
        }
        #endregion

        #endregion

    }
}