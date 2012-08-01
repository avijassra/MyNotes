namespace MyNotes.UI.Web.ViewModels.Shared
{
    public class ErrorMessageInfo
    {
        public ErrorMessageInfo()
            : this("Error.", "An error occurred while processing your request.")
        {
        }

        public ErrorMessageInfo(string heading, string message)
        {
            Heading = heading;
            Message = message;
        }

        public string Heading { get; set; }

        public string Message { get; set; }
    }
}