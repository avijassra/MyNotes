namespace MyNotes.UI.Web.Setup.ActionApi
{
    public class AjaxResponse
    {
        public AjaxResponse()
        {
            Message = null;
            PopupView = null;
            ContentView = null;
            SlidingScreenId = null;
            RedirectUrl = null;
            Result = null;
        }

        public bool HasError { get; set; }

        public string Message { get; set; }

        public string PopupView { get; set; }

        public string ContentView { get; set; }

        public string SlidingScreenId { get; set; }

        public object Result { get; set; }

        public string RedirectUrl { get; set; }
    }
}