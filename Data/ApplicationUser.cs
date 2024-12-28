using Humanizer;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Eventing.Reader;

namespace BuildMasterPro.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        [AllowNull]
        public string FirstName { get; set; } = string.Empty;
        [AllowNull]
        public string LastName { get; set; } = string.Empty;
        [AllowNull]
        public string MiddleMName { get; set; } = string.Empty;
        [AllowNull]
        public string Phone { get; set; } = string.Empty;
        [DataType(DataType.DateTime)]
        public DateTime DateCreated { get; set; } = DateTime.Today.Date;
        [DataType(DataType.DateTime)]
        public DateTime DateUpdated { get; set; } = DateTime.Today.Date;
        [AllowNull]
        public bool IsActive { get; set; } = true;
        [AllowNull]
        public string? Address { get; set; } = string.Empty;
        [AllowNull]
        public string ProfilePictureUrl { get; set; } = string.Empty;
        [AllowNull]
        public string JobTitle { get; set; } = string.Empty;
        [AllowNull]
        public string Department { get; set; } = string.Empty;
        [DataType(DataType.DateTime)]
        [AllowNull]
        public DateTime BirthDate { get; set; } = DateTime.Today.AddYears(-18);
        [AllowNull]
        public string Gender { get; set; } = "Prefer Not to Say";
        [DataType(DataType.DateTime)]
        [AllowNull]
        public DateTime LastLoginDate { get; set; } = DateTime.Today.Date;
        [AllowNull]
        public bool IsDeleted { get; set; } = false;

    }

}
