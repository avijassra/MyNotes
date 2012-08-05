namespace MyNotes.UI.Web.Setup.ActionApi
{
    using System;

    public class ActionApiException : Exception
    {
        public string Code { get; set; }

        public ActionApiException(string message)
            : base(message)
        {
        }
    }
}