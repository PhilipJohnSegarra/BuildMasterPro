using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace BuildMasterPro.Data
{
    public class ProjectTask
    {
        [Key]
        public int TaskId { get; set; }
        [Required]
        public int ProjectId { get; set; }
        [Required]
        public string TaskName { get; set; } = string.Empty;
        [Required]
        public string TaskDescription { get; set; } = string.Empty;

        //PROPERTIES 12/01/2025
        [DataType(DataType.DateTime)]
        public DateTime PlannedStartDate { get; set; } = DateTime.Now;
        [DataType(DataType.DateTime)]
        public DateTime PlannedEndDate { get; set; } = DateTime.Now;
        [DataType(DataType.DateTime)]
        [AllowNull]
        public DateTime? ActualStartDate { get; set; }
        [DataType(DataType.DateTime)]
        [AllowNull]
        public DateTime? ActualEndDate { get; set; }
        [AllowNull]
        public string? GroupName { get; set; }
        [AllowNull]
        public int? CategoryId { get; set; }
        //PROPERTIES 12/01/2025 END

        [AllowNull]
        public string? Status { get; set; } = "Not Started";
        [AllowNull]
        public string? Priority { get; set; }
        public int Progress { get; set; } = 0;
        public bool IsDeleted { get; set; } = false;
        [JsonIgnore]
        [ForeignKey(nameof(ProjectId))]
        public Project? Project { get; set; } = default!;
        [ForeignKey(nameof(CategoryId))]
        public TaskCategory? TaskCategory { get; set; } = default!;

        public ICollection<TaskUser>? TaskUsers { get; set; }
        public ICollection<TaskActivity>? TaskActivities { get; set; }
    }
}
