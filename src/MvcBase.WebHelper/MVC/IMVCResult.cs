﻿namespace MvcBase.WebHelper.Mvc
{
    using System.Web.Routing;

    internal interface IMvcResult
    {
        string Action { get; set; }

        string Controller { get; set; }

        RouteValueDictionary RouteValueDictionary { get; set; }
    } 
}