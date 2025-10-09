namespace PlantsCatalog.Models
{
    // Used to display diagnostic info on the error page
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        // Show the ID only if it exists (helps trace specific errors)
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
