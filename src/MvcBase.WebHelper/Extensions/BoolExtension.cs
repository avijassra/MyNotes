namespace MvcBase.WebHelper
{
    using System;

    internal static class BoolExtension
    {
        public static string ToJavascriptString(this Boolean boolean)
        {
            return (boolean ? "true" : "false");
        }
    }
}