namespace MvcBase.WebHelper
{
    using System;
    using System.Text;
    using System.Linq;
    using System.Collections.Generic;

    /// <summary>
    /// Bool Extension class.
    /// </summary>
    internal static class IEnumerableExtension
    {
        /// <summary>
        /// This method converts ienumerable of type string to string
        /// </summary>
        /// <param name="collection">IEnumerable of string type</param>
        /// <param name="seperator">String seperator</param>
        /// <returns>Appeneded string</returns>
        public static string AppendAll(this IEnumerable<KeyValuePair<string, object>> collection, string seperator)
        {
            if (null == collection)
            {
                return string.Empty;
            }

            var builder = new StringBuilder();
            var isFirstStr = true;

            foreach (var keyVal in collection)
            {
                if (null != keyVal.Value && !string.IsNullOrEmpty(keyVal.Value.ToString()))
                {
                    if (!isFirstStr) { builder.Append(seperator ?? ""); }
                    builder.Append(keyVal.Key);
                    builder.Append("=");
                    builder.Append(keyVal.Value);
                    isFirstStr = false;
                }
            }

            return builder.ToString();
        }

        /// <summary>
        /// This method converts ienumerable of type string to querystring
        /// </summary>
        /// <param name="collection">IEnumerable of string type</param>
        /// <returns>String of querystring format</returns>
        public static string AsQueryString(this IEnumerable<KeyValuePair<string, object>> collection)
        {
            if (collection.Count() > 0)
            {
                var idVal = collection.Where(x => x.Key == "id").First();
                var querystring = collection.Where(x => x.Key != "id").AppendAll("&");
                return string.Format("/{0}?{1}", idVal.Value ?? "", querystring);
            }
            return string.Empty;
        }
    }
}