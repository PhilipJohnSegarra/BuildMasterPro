using System.ComponentModel.DataAnnotations;

namespace BuildMasterPro.Data
{
    public class Notification
    {
        [Key]
        public int Id { get; set; }

        public string? UserId { get; set; }           // Target user
        public string? Title { get; set; }            // Short headline
        public string? Message { get; set; }          // Full message or body

        public bool IsRead { get; set; } = false;    // For unread indicators
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public string? LinkUrl { get; set; }         // Optional link to relevant page
        public bool IsEmailSent { get; set; } = false;  // To track if it was emailed

        // Type (optional): Info, Warning, Error, Success, etc.
        public string? Type { get; set; } = "Info";

        //Notifation types: Info, Warning, Success, Error
    }
}
