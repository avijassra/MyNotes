namespace MvcBase.WebHelper
{
    public enum HashFormate
    {
        /// <summary>
        /// Formates the url to {controller}#!{action}/{id}
        /// </summary>
        Standard = 0,

        /// <summary>
        /// Formates the url to {controller}#!{id}
        /// </summary>
        NoAction,

        /// <summary>
        /// Formates the url to {controller}#!{id}/{action}
        /// </summary>
        ActionAfterId,
    }
}
