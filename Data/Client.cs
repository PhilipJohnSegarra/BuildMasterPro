using System.ComponentModel.DataAnnotations;

namespace BuildMasterPro.Data
{
    public class Client
    {
        [Key]
        public int Id { get; set; }

        // Basic Company Information
        [Required]
        public string CompanyName { get; set; } = string.Empty;
        public string? CompanyEmail { get; set; }

        [Required]
        public string CompanyNumber1 { get; set; } = string.Empty;
        public string? CompanyNumber2 { get; set; }

        public string? CompanyAddress { get; set; }
        public string? CompanyWebsite { get; set; }
        public string? IndustryType { get; set; } // e.g., Residential, Commercial, Infrastructure

        // Primary Contact Person
        [Required]
        public string PersonName { get; set; } = string.Empty;
        public string? PersonEmail { get; set; }
        public string? PersonPhone { get; set; }  // Optional additional contact number

        // Secondary Contact (if applicable)
        public string? AlternatePersonName { get; set; }
        public string? AlternatePersonEmail { get; set; }
        public string? AlternatePersonPhone { get; set; }

        // Additional Company Details
        public DateTime? DateEstablished { get; set; }
        public string? RegistrationNumber { get; set; } // Business Registration ID
        public string? TaxIdentificationNumber { get; set; } // TIN

        // Status & Notes
        public bool IsActive { get; set; } = true;  // Whether the client is active in the system
        public string? Notes { get; set; }  // Any additional remarks about the client


        public ICollection<Project>? Projects { get; set; }
    }
}
