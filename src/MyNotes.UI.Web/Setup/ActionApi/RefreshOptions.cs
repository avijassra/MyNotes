namespace MyNotes.UI.Web.Setup.ActionApi
{
    public class RefreshOptions
    {
        public bool HasError { get; set; }

        public string Message { get; set; }

        public string ContentViewName { get; set; }

        public object ContentViewModel { get; set; }

        public string SlidingScreenId { get; set; }

        public string PopupViewName { get; set; }

        public object PopupViewModel { get; set; }

        public string ResultViewName { get; set; }

        public object ResultViewModel { get; set; }

        public string RedirectUrl { get; set; }
    }
}