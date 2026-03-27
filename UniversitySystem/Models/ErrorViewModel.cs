using System;

namespace UniversitySystem.Models
{
    public class ErrorViewModel
    {
        // The request identifier (nullable to match typical templates)
        public string? RequestId { get; set; }

        // Helper property used by the view to decide whether to display the RequestId
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}





