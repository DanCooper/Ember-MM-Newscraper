using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trakttv.TraktAPI.Model;
using System.Globalization;
using System.Runtime.Serialization.Json;
using System.Text.RegularExpressions;
using NLog;

namespace Trakttv
{
    public class TraktMethods
    {
        public static Logger logger = NLog.LogManager.GetCurrentClassLogger();


        /// <summary>
        ///  Trakt-Login process for using v2 API (Token based authentification) 
        /// </summary>
        /// <param name="_traktuser">trakt.tv Username</param>
        /// <param name="_traktpassword">trakt.tv Password</param>
        /// <param name="_trakttoken">Optional: trakt.tv Token, may be empty (then it will be generated)</param>
        /// <returns>(new) trakt.tv Token</returns>
        /// <remarks>
        /// 2015/01/17 Cocotus - First implementation of new V2 Authentification process for trakt.tv API
        /// </remarks>
        public static string LoginToTrakt(string _traktuser, string _traktpassword, string _trakttoken= "")
        {
            // Use Trakttv wrapper
            TraktToken response = null;
            TraktAPI.Model.TraktAuthentication account = new TraktAPI.Model.TraktAuthentication();
            account.Username = _traktuser;
            account.Password = _traktpassword;
            TraktSettings.Password = _traktpassword;
            TraktSettings.Username = _traktuser;

            if (string.IsNullOrEmpty(_trakttoken))
            {
               response = Trakttv.TraktMethods.LoginToAccount(account);
                if ((response != null))
                {
                    _trakttoken = response.Token;
                }
                else
                {
                    _trakttoken = "";
                }
            }
            else
            {
                TraktSettings.Token = _trakttoken;
            }
            return _trakttoken;
        }


        /// <summary>
        /// Login to trak.tv, this is called from other modules in Ember to connect to trakt.tv, Entry point
        /// </summary>
        /// <param name="account">Account data</param>
        /// <returns>Retrieved Token from trakt.tv API</returns>
        public static TraktToken LoginToAccount(TraktAuthentication account)
        {
            TraktToken response = null;

            response = Trakttv.TrakttvAPI.Login(account.ToJSON());

            if (response == null || string.IsNullOrEmpty(response.Token))
            {
                // not good, process failed
                logger.Warn("[LoginToAccount] Invalid Response!");
            }
            else
            {
                // Save User Token
                TraktSettings.Token = response.Token;
                // Save New Account Settings
                TraktSettings.Username = account.Username;
                TraktSettings.Password = account.Password;

            }
            return response;
        }


        /// <summary>
        /// Convert a given "title year" combination to trakt.tv slug style
        /// </summary>
        /// <param name="phrase">string to modify</param>
        /// <returns>slugged name!</returns>
        public static string ConvertToSlug(string phrase)
            // example:
           //string.Format("{0} {1}", Title, Year).ToSlug();
        {
            if (phrase == null) return string.Empty;

            var s = CleanHyphen(phrase).ToLower();
            s = Regex.Replace(s, @"[^a-z0-9\s-]", string.Empty);        // remove invalid characters
            s = Regex.Replace(s, @"\s+", " ").Trim();                   // single space
            s = s.Substring(0, s.Length <= 45 ? s.Length : 45).Trim();  // cut and trim
            s = Regex.Replace(s, @"\s", "-");                           // insert hyphens
            return s.ToLower();
        }

        /// <summary>
        /// Remove unallowed critical characters from string
        /// </summary>
        /// <param name="phrase">string to modify</param>
        /// <returns>cleaned string</returns>
        public static string CleanHyphen(string text)
        {
            if (text == null) return string.Empty;

            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }

        /// <summary>
        /// Used to mask sensible personal data as passwords in logging
        /// </summary>
        /// <param name="phrase">string to modify</param>
        /// <returns>masked string</returns>
      public static string MaskSensibleString(string text)
       {
           if (!string.IsNullOrEmpty(text) && text.Length > 4)  
         {
		string helpstring = "";
        helpstring = text.Remove(3) + "***";
          return text;
         }
         else
         {
         return text;
         }
       }
 }
}